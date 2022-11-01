using Ardalis.GuardClauses;

using PraeceptorCQRS.Application.Persistence;
using PraeceptorCQRS.Domain.Entities;
using PraeceptorCQRS.Utilities;

using Serilog;

using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace PraeceptorCQRS.Infrastructure.Persistence
{
    public class FileStreamRepository : IFileStreamRepository
    {
        private readonly string _connectionString;

        public FileStreamRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        private DataTable ListData(string commandText)
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

        private async Task<object[]?> ReadSystemDataSqlTypesSqlFileStream(string commandText, List<SqlParameter> parameters)
        {
            SqlDataReader reader;
            System.Data.SqlTypes.SqlFileStream sqlFileStream;
            string filePathInServer;
            object transactionContext;
            int byteAmount;
            object[] values;
            byte[] data;

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.Transaction = connection.BeginTransaction();

                    command.CommandText = "SELECT GET_FILESTREAM_TRANSACTION_CONTEXT()";
                    command.CommandType = CommandType.Text;
                    transactionContext = command.ExecuteScalar();

                    command.CommandText = commandText;
                    command.CommandType = CommandType.Text;

                    foreach (var param in parameters)
                        command.Parameters.Add(param);

                    try
                    {
                        reader = await command.ExecuteReaderAsync();
                        _ = await reader.ReadAsync();
                        int fields = reader.FieldCount;
                        values = new object[fields];
                        for (int i = 0; i < fields - 1; i++)
                            values[i] = reader.GetValue(i);
                        filePathInServer = (string)reader["FilePath"];
                        reader.Close();

                        sqlFileStream = new System.Data.SqlTypes.SqlFileStream(filePathInServer, (byte[])transactionContext, FileAccess.Read);
                        data = new byte[sqlFileStream.Length];
                        sqlFileStream.Seek(0L, SeekOrigin.Begin);
                        byteAmount = sqlFileStream.Read(data, 0, (int)sqlFileStream.Length);
                        sqlFileStream.Close();
                        command.Transaction.Commit();
                        values[fields - 1] = data;
                    }
                    catch (SqlException ex)
                    {
                        values = Array.Empty<object>();
                        data = Array.Empty<byte>();
                        byteAmount = 0;

                        var errorMessages = new StringBuilder();

                        for (int i = 0; i < ex.Errors.Count; i++)
                        {
                            errorMessages.Append("Index #" + i + "\n" +
                                "Message: " + ex.Errors[i].Message + "\n" +
                                "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                                "Source: " + ex.Errors[i].Source + "\n" +
                                "Procedure: " + ex.Errors[i].Procedure + "\n");
                        }
                    }
                }
                connection.Close();
            }

            return values;
        }

        public async Task<SqlFileStream?> GetFileInfo(Guid guid)
        {
            Log.Warning($"FileStreamRepository:GetFileInfo('{guid}')");

            await Task.CompletedTask;

            DataTable dt = ListData(
                $"SELECT [Name], [Title], [Source], [Description], [InstituteId], [ContentType], [DateCreated] FROM FileStreamTable WHERE [Id] = '{guid}'"
                );

            var name = dt.Columns["Name"];
            Guard.Against.Null(name);
            var title = dt.Columns["Title"];
            Guard.Against.Null(title);
            var source = dt.Columns["Source"];
            Guard.Against.Null(source);
            var description = dt.Columns["Description"];
            Guard.Against.Null(description);
            var instituteId = dt.Columns["InstituteId"];
            Guard.Against.Null(instituteId);
            var contentType = dt.Columns["ContentType"];
            Guard.Against.Null(contentType);
            var dateCreated = dt.Columns["DateCreated"];
            Guard.Against.Null(dateCreated);

            IEnumerable consulta = from row in dt.AsEnumerable() select row;

            List<SqlFileStream> files = new();

            foreach (DataRow row in consulta)
            {
                files.Add(new(guid)
                {
                    Name = row.Field<string>(column: name) ?? null!,
                    Title = row.Field<string>(column: title) ?? null!,
                    Source = row.Field<string>(column: source) ?? null!,
                    Description = row.Field<string>(column: description) ?? null!,
                    InstituteId = row.Field<Guid>(column: instituteId),
                    ContentType = row.Field<string>(column: contentType) ?? null!,
                    DateCreated = row.Field<DateTime>(column: dateCreated),
                });
            }

            return files.ToList().FirstOrDefault(o => o.Id == guid);
        }

        public async Task<SqlFileStream?> StoreFile(SqlFileStream fileStream)
        {
            Log.Warning($"FileStreamRepository:StoreFile('{fileStream.Name}')");

            await Task.CompletedTask;

            List<SqlParameter> parameters = new();

            SqlParameter parameter;

            parameter = new SqlParameter("@Id", SqlDbType.UniqueIdentifier) { Value = Guid.NewGuid() };
            parameters.Add(parameter);
            parameter = new SqlParameter("@Name", SqlDbType.NVarChar, 250) { Value = fileStream.Name };
            parameters.Add(parameter);
            parameter = new SqlParameter("@Title", SqlDbType.NVarChar, 4000) { Value = fileStream.Title ?? "" };
            parameters.Add(parameter);
            parameter = new SqlParameter("@Source", SqlDbType.NVarChar, 4000) { Value = fileStream.Source ?? "" };
            parameters.Add(parameter);
            parameter = new SqlParameter("@Description", SqlDbType.NVarChar, 4000) { Value = fileStream.Description ?? "" };
            parameters.Add(parameter);
            parameter = new SqlParameter("@InstituteId", SqlDbType.UniqueIdentifier) { Value = fileStream.InstituteId };
            parameters.Add(parameter);
            parameter = new SqlParameter("@ContentType", SqlDbType.NVarChar, 500) { Value = fileStream.ContentType };
            parameters.Add(parameter);
            parameter = new SqlParameter("@DateCreated", SqlDbType.DateTime) { Value = DateTime.Now };
            parameters.Add(parameter);

            object transactionContext;
            System.Data.SqlTypes.SqlFileStream sqlFileStream;
            string filePathInServer;
            int rowsInserted;
            var guid = Guid.NewGuid();

            try
            {
                using var connection = new SqlConnection(_connectionString);

                connection.Open();

                using (var insertCommand = connection.CreateCommand())
                {
                    using var helperCommand = connection.CreateCommand();

                    insertCommand.CommandText =
                        "INSERT INTO FileStreamTable ([Id], [Name], [Title], [Source], [Description], [InstituteId], [ContentType], [DateCreated], [Data]) " +
                        "   VALUES (@Id, @Name, @Title, @Source, @Description, @InstituteId, @ContentType, @DateCreated, (0x))";
                    insertCommand.CommandType = CommandType.Text;

                    foreach (var param in parameters)
                        insertCommand.Parameters.Add(param);

                    insertCommand.Transaction = connection.BeginTransaction();
                    helperCommand.Transaction = insertCommand.Transaction;

                    helperCommand.CommandText = "SELECT GET_FILESTREAM_TRANSACTION_CONTEXT()";
                    transactionContext = helperCommand.ExecuteScalar();

                    helperCommand.CommandText = "SELECT Data.PathName() FROM FileStreamTable WHERE [Id] = @Id";
                    parameter = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
                    helperCommand.Parameters.Add(parameter);
                    insertCommand.Parameters["@Id"].Value = guid;
                    rowsInserted = insertCommand.ExecuteNonQuery();
                    helperCommand.Parameters["@Id"].Value = insertCommand.Parameters["@Id"].Value;
                    filePathInServer = (string)helperCommand.ExecuteScalar();

                    sqlFileStream = new System.Data.SqlTypes.SqlFileStream(filePathInServer, (byte[])transactionContext, FileAccess.Write);
                    sqlFileStream.Write(fileStream.Data, 0, fileStream.Data.Length);
                    sqlFileStream.Close();

                    insertCommand.Transaction.Commit();
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }

            return await GetFileInfo(guid);
        }

        public async Task<SqlFileStream?> ReadFile(Guid guid)
        {
            Log.Warning($"FileStreamRepository:ReadFile('{guid}')");

            List<SqlParameter> parameters = new()
            {
                new SqlParameter("@Id", SqlDbType.UniqueIdentifier) { Value = guid }
            };

            object[]? values = await ReadSystemDataSqlTypesSqlFileStream(
                "SELECT [Name], [Title], [Source], [Description], [InstituteId], [ContentType], [DateCreated], [Data].PathName() AS FilePath FROM FileStreamTable WHERE [Id] = @Id",
                parameters
                );

            return (values is not null)
                ? new(guid)
                {
                    Name = (string)values[0],
                    Title = (string)values[1],
                    Source = (string)values[2],
                    Description = (string)values[3],
                    InstituteId = (Guid)values[4],
                    ContentType = (string)values[5],
                    DateCreated = (DateTime)values[6],
                    Data = (byte[])values[7]
                }
                : null!;
        }

        public bool Exists(Guid instituteId, string name)
        {
            Log.Warning($"FileStreamRepository:Exists('{instituteId}', '{name}')");

            DataTable dt = ListData(
                $"SELECT COUNT(*) AS RESULT FROM FileStreamTable WHERE [InstituteId] = '{instituteId}' AND [Name] = '{name}';"
                );

            var column = dt.Columns["RESULT"];
            Guard.Against.Null(column);
            var rows = dt.Rows[0].Field<int>(column);

            return rows > 0;
        }

        public async Task<SqlFileStream?> ReadFile(Guid instituteId, string name)
        {
            Log.Warning($"FileStreamRepository:ReadFile('{instituteId}', '{name}')");

            List<SqlParameter> parameters = new()
            {
                new SqlParameter("@Name", SqlDbType.NVarChar, 250) { Value = name },
                new SqlParameter("@InstituteId", SqlDbType.UniqueIdentifier) {Value = instituteId }
            };

            object[]? values = await ReadSystemDataSqlTypesSqlFileStream(
                $"SELECT [Id], [Title], [Source], [Description], [ContentType], [DateCreated], [Data].PathName() AS FilePath FROM FileStreamTable WHERE [Name] = @Name AND [InstituteId] = @InstituteId",
                parameters
                );

            return (values is not null)
                ? new((Guid)values[0])
                {
                    Name = name,
                    Title = (string)values[1],
                    Source = (string)values[2],
                    Description = (string)values[3],
                    ContentType = (string)values[4],
                    InstituteId = instituteId,
                    DateCreated = (DateTime)values[5],
                    Data = (byte[])values[6]
                }
                : null!;
        }

        public async Task<int> DeleteFile(Guid guid)
        {
            Log.Warning($"FileStreamRepository:DeleteFile('{guid}')");

            await Task.CompletedTask;

            SqlCommand command;
            int rowsDeleted;

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (command = connection.CreateCommand())
                {
                    command.CommandText = $"DELETE FROM FileStreamTable WHERE Id = '{guid}'";
                    rowsDeleted = command.ExecuteNonQuery();

                    command.CommandText = "CHECKPOINT";
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }

            return rowsDeleted;
        }

        public async Task<int> GetClassCountByInstitute(Guid instituteId)
        {
            Log.Warning($"FileStreamRepository:GetClassCountByInstitute('{instituteId}')");

            await Task.CompletedTask;

            int count = 0;

            try
            {
                using (SqlConnection thisConnection = new(_connectionString))
                {
                    using SqlCommand cmdCount = new($"SELECT COUNT(*) FROM FileStreamTable WHERE (InsituteId = {instituteId})", thisConnection);
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

        public async Task<PageOf<SqlFileStream>> GetFilesPage(
            Guid instituteId,
            int page,
            int pageSize,
            string? sort,
            bool ascending,
            string? nameFilter,
            string? titleFilter,
            string? sourceFilter,
            string? descriptionFilter,
            string? contentTypeFilter,
            string? dateCreatedFilter
            )
        {
            var files = await GetFilteredEntities(
                instituteId,
                nameFilter,
                titleFilter,
                sourceFilter,
                descriptionFilter,
                contentTypeFilter,
                dateCreatedFilter
                );

            files = SortBy(files, sort, ascending);

            int numberOfPages = (files.Count + (pageSize - 1)) / pageSize;
            int nextPage = page + 1;

            if (nextPage >= numberOfPages)
                nextPage = -1;

            List<SqlFileStream> entities = files.Count > (page + 1) * pageSize
                    ? files.GetRange(page * pageSize, pageSize)
                    : files.GetRange(page * pageSize, files.Count - page * pageSize);

            return new PageOf<SqlFileStream>(
                // CurrentPage
                page,
                // Size
                pageSize,
                // PreviousPage
                page - 1,
                // NextPage
                nextPage,
                // NumberOfPages
                numberOfPages,
                // Chapters
                entities
                );
        }

        private async Task<List<SqlFileStream>> GetFilteredEntities(
            Guid instituteId,
            string? nameFilter,
            string? titleFilter,
            string? sourceFilter,
            string? descriptionFilter,
            string? contentTypeFilter,
            string? dateCreatedFilter
            )
        {
            await Task.CompletedTask;

            DataTable dt = ListData(
                $"SELECT [Id], [Name], [Title], [Source], [Description], [InstituteId], [ContentType], [DateCreated] FROM FileStreamTable WHERE [InstituteId] = '{instituteId}'"
                );

            var id = dt.Columns["Id"];
            Guard.Against.Null(id);
            var name = dt.Columns["Name"];
            Guard.Against.Null(name);
            var title = dt.Columns["Title"];
            Guard.Against.Null(title);
            var source = dt.Columns["Source"];
            Guard.Against.Null(source);
            var description = dt.Columns["Description"];
            Guard.Against.Null(description);
            var contentType = dt.Columns["ContentType"];
            Guard.Against.Null(contentType);
            var dateCreated = dt.Columns["DateCreated"];
            Guard.Against.Null(dateCreated);

            IEnumerable consulta = from row in dt.AsEnumerable() select row;

            List<SqlFileStream> files = new();

            foreach (DataRow row in consulta)
            {
                var cntType = row.Field<string>(column: contentType);
                Guard.Against.Null(cntType);

                files.Add(new(row.Field<Guid>(column: id))
                {
                    Name = row.Field<string>(column: name) ?? null!,
                    Title = row.Field<string>(column: title) ?? null!,
                    Source = row.Field<string>(column: source) ?? null!,
                    Description = row.Field<string>(column: description) ?? null!,
                    InstituteId = instituteId,
                    ContentType = cntType,
                    DateCreated = row.Field<DateTime>(column: dateCreated),
                });
            }

            var filteredList = new List<Domain.Entities.SqlFileStream>();
            var isFiltered = false;

            foreach (var entity in files)
            {
                if (!string.IsNullOrWhiteSpace(nameFilter))
                {
                    isFiltered = true;

                    if (Global.MatchStringFilter(nameFilter, entity.Name))
                    {
                        if (filteredList.Find(o => o.Id == entity.Id) is null)
                        {
                            filteredList.Add(entity);
                        }
                        continue;
                    }
                }
                if (!string.IsNullOrWhiteSpace(titleFilter))
                {
                    isFiltered = true;

                    if (Global.MatchStringFilter(titleFilter, entity.Title))
                    {
                        if (filteredList.Find(o => o.Id == entity.Id) is null)
                        {
                            filteredList.Add(entity);
                        }
                        continue;
                    }
                }
                if (!string.IsNullOrWhiteSpace(sourceFilter))
                {
                    isFiltered = true;

                    if (Global.MatchStringFilter(sourceFilter, entity.Source))
                    {
                        if (filteredList.Find(o => o.Id == entity.Id) is null)
                        {
                            filteredList.Add(entity);
                        }
                        continue;
                    }
                }
                if (!string.IsNullOrWhiteSpace(descriptionFilter))
                {
                    isFiltered = true;

                    if (Global.MatchStringFilter(descriptionFilter, entity.Description))
                    {
                        if (filteredList.Find(o => o.Id == entity.Id) is null)
                        {
                            filteredList.Add(entity);
                        }
                        continue;
                    }
                }
                if (!string.IsNullOrWhiteSpace(contentTypeFilter))
                {
                    isFiltered = true;

                    if (Global.MatchStringFilter(contentTypeFilter, entity.ContentType))
                    {
                        if (filteredList.Find(o => o.Id == entity.Id) is null)
                        {
                            filteredList.Add(entity);
                        }
                        continue;
                    }
                }
                if (!string.IsNullOrWhiteSpace(dateCreatedFilter))
                {
                    isFiltered = true;

                    if (Global.MatchDateTimeFilter(dateCreatedFilter, entity.DateCreated))
                    {
                        if (filteredList.Find(o => o.Id == entity.Id) is null)
                        {
                            filteredList.Add(entity);
                        }
                        continue;
                    }
                }
            }

            return isFiltered ? filteredList : files;
        }

        private static List<SqlFileStream> SortBy(List<SqlFileStream> list, string? column, bool ascending)
        {
            if (string.IsNullOrWhiteSpace(column))
                return list;

            return column switch
            {
                "DateCreated" => Global.SortList(list, x => x.DateCreated, ascending),
                "Name" => Global.SortList(list, x => x.Name, ascending),
                "Title" => Global.SortList(list, x => x.Title, ascending),
                "Source" => Global.SortList(list, x => x.Source, ascending),
                "Description" => Global.SortList(list, x => x.Description, ascending),
                "ContentType" => Global.SortList(list, x => x.ContentType, ascending),
                _ => list
            };
        }
    }
}
