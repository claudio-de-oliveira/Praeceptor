using Serilog;

using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Text;

namespace PraeceptorCQRS.Utilities
{
    public class FileStreamOperations
    {
        private readonly string _connectionString;

        public FileStreamOperations(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int GetTableCount(string stmt)
        {
            int count = 0;

            try
            {
                using (SqlConnection thisConnection = new(_connectionString))
                {
                    using SqlCommand cmdCount = new(stmt, thisConnection);
                    thisConnection.Open();
                    count = (int)cmdCount.ExecuteScalar();
                }
                return count;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return 0;
            }
        }

        public int DeleteContent(string commandText)
        {
            SqlCommand command;
            int rowsDeleted;

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (command = connection.CreateCommand())
                {
                    command.CommandText = commandText;
                    rowsDeleted = command.ExecuteNonQuery();
                    command.CommandText = "CHECKPOINT";
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }

            return rowsDeleted;
        }

        public DataTable ListData(string commandText)
        {
            var data = new DataSet();
            var dataAdapter = new SqlDataAdapter();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = commandText;
                    dataAdapter.SelectCommand = command;
                    dataAdapter.Fill(data);
                }

                connection.Close();
            }

            return data.Tables[0];
        }

        private static string ExtractTableNameFromInsertCommand(string commandText)
        {
            string[] str = commandText.Split(new char[] { ' ', '(' });

            if (str.Length < 3)
                return string.Empty;
            if (str[0].ToUpper() != "INSERT")
                return string.Empty;
            if (str[1].ToUpper() != "INTO")
                return string.Empty;
            return str[2];
        }

        public Guid StoreFile(string commandText, List<SqlParameter> parameters, byte[] fileData)
        {
            object transactionContext;
            SqlParameter parameter;
            SqlFileStream sqlFileStream;
            string filePathInServer;
            int rowsInserted;
            var guid = Guid.NewGuid();

            var fileStream = ExtractTableNameFromInsertCommand(commandText);

            if (fileStream == null)
                return Guid.Empty;

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var insertCommand = connection.CreateCommand())
                {
                    using (var helperCommand = connection.CreateCommand())
                    {
                        insertCommand.CommandText = commandText;
                        insertCommand.CommandType = CommandType.Text;

                        foreach (var param in parameters)
                            insertCommand.Parameters.Add(param);

                        insertCommand.Transaction = connection.BeginTransaction();
                        helperCommand.Transaction = insertCommand.Transaction;

                        helperCommand.CommandText = "SELECT GET_FILESTREAM_TRANSACTION_CONTEXT()";
                        transactionContext = helperCommand.ExecuteScalar();

                        helperCommand.CommandText = "SELECT Data.PathName() FROM " + fileStream + " WHERE [Id] = @Id";
                        parameter = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
                        helperCommand.Parameters.Add(parameter);
                        insertCommand.Parameters["@Id"].Value = guid;
                        rowsInserted = insertCommand.ExecuteNonQuery();
                        helperCommand.Parameters["@Id"].Value = insertCommand.Parameters["@Id"].Value;
                        filePathInServer = (string)helperCommand.ExecuteScalar();

                        sqlFileStream = new SqlFileStream(filePathInServer, (byte[])transactionContext, FileAccess.Write);
                        sqlFileStream.Write(fileData, 0, fileData.Length);
                        sqlFileStream.Close();

                        insertCommand.Transaction.Commit();
                    }
                }
                connection.Close();
            }

            return guid;
        }

        public async Task<object[]> FetchFile(string commandText, List<SqlParameter> parameters)
        {
            object[] values;

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.Transaction = connection.BeginTransaction();

                    command.CommandType = CommandType.Text;
                    command.CommandText = "SELECT GET_FILESTREAM_TRANSACTION_CONTEXT()";
                    object transactionContext = command.ExecuteScalar();

                    command.CommandType = CommandType.Text;
                    command.CommandText = commandText;

                    foreach (var param in parameters)
                        command.Parameters.Add(param);

                    try
                    {
                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        _ = await reader.ReadAsync();
                        int fields = reader.FieldCount;
                        values = new object[fields];
                        for (int i = 0; i < fields - 1; i++)
                            values[i] = reader.GetValue(i);
                        string filePathInServer = (string)reader["FilePath"];
                        reader.Close();

                        SqlFileStream sqlFileStream = new SqlFileStream(filePathInServer, (byte[])transactionContext, FileAccess.Read);
                        byte[] data = new byte[sqlFileStream.Length];
                        sqlFileStream.Seek(0L, SeekOrigin.Begin);
                        int byteAmount = sqlFileStream.Read(data, 0, (int)sqlFileStream.Length);
                        sqlFileStream.Close();
                        command.Transaction.Commit();
                        values[fields - 1] = data;
                    }
                    catch (SqlException ex)
                    {
                        var errorMessages = new StringBuilder();

                        for (int i = 0; i < ex.Errors.Count; i++)
                        {
                            errorMessages.Append("Index #" + i + "\n" +
                                "Message: " + ex.Errors[i].Message + "\n" +
                                "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                                "Source: " + ex.Errors[i].Source + "\n" +
                                "Procedure: " + ex.Errors[i].Procedure + "\n");
                        }

                        values = Array.Empty<object>();
                    }
                }
                connection.Close();
            }

            return values;
        }
    }
}
