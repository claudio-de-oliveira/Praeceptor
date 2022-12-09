using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

using ErrorOr;

using PraeceptorCQRS.Domain.Entities;

using PraeceptorCQRS.Application.Entities.SimpleTable.Common;

using System.Text;

namespace PraeceptorCQRS.Application.Entities.ToWord.Models
{
    public abstract partial class WordDocument
    {
        protected WordprocessingDocument WDocument { get; set; } = default!;
        public MemoryStream _stream = default!;

        private readonly string _author = "Systema Praeceptor";
        private readonly string _initials = "SP";

        public MainDocumentPart TheMainDocumentPart
            => WDocument.MainDocumentPart!;

        public byte[] GetData() => _stream.GetBuffer();

        public Body TheBody()
            => WDocument is not null ? TheMainDocumentPart.Document.Body! : null!;

        public readonly Dictionary<string, string> codes = new();
        public readonly Dictionary<string, string> tables = new();
        public readonly Dictionary<string, string> variables = new();

        public virtual void Create(byte[] template, bool main = false)
        {
            _stream = new();

            _stream.Write(template, 0, template.Length);

            WDocument = WordprocessingDocument.Open(_stream, true);

            WDocument.ChangeDocumentType(WordprocessingDocumentType.Document);

            TheBody().RemoveAllChildren();
        }

        public abstract Task<ErrorOr<SqlFileStream>> GetImageByCode(Guid instituteId, string code);

        public abstract Task<ErrorOr<Domain.Entities.SimpleTable>> GetTableByCode(Guid instituteId, string code);

        #region Save

        private void CloseAndDisposeOfDocument()
        {
            if (WDocument != null)
            {
                WDocument.Close();
                WDocument.Dispose();
                WDocument = null!;
            }
        }

        public void SaveToFile()
        {
            if (WDocument is not null)
                CloseAndDisposeOfDocument();

            if (_stream is null)
                throw new ArgumentException("This object has already been disposed of so you cannot save it!");

            // using (var fs = File.Create($"{Guid.NewGuid()}.docx"))
            // {
            //     Console.WriteLine("S A L V A N D O   E M   U M   A R Q U I V O   T E M P O R Á R I O");
            //     _stream.WriteTo(fs);
            // }
        }

        #endregion Save
    }
}