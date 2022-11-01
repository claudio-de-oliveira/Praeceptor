using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

using System.Text;

using Word.Models;

namespace Word
{
    public abstract partial class WordDocument
    {
        protected WordprocessingDocument? WDocument { get; set; }
        private MemoryStream? _stream;

        public readonly Dictionary<string, string> variables = new();
        public readonly Dictionary<string, string> codes = new();

        private readonly string _author = "Systema Praeceptor";
        private readonly string _initials = "SP";

        public MainDocumentPart? TheMainDocumentPart
            => WDocument?.MainDocumentPart;
        public Body? TheBody()
            => WDocument is not null ? TheMainDocumentPart?.Document.Body : null;

        public virtual void Create(byte[] template, bool main = false)
        {
            _stream = new();

            _stream.Write(template, 0, template.Length);

            WDocument = WordprocessingDocument.Open(_stream, true);

            WDocument.ChangeDocumentType(WordprocessingDocumentType.Document);

            TheBody()?.RemoveAllChildren();
        }

        public abstract Task<ImageModel> GetImageByCode(int instituteId, string code);
        public abstract Task<TableModel> GetTableByCode(int instituteId, string code);

        #region Save
        private void CloseAndDisposeOfDocument()
        {
            if (WDocument != null)
            {
                WDocument.Close();
                WDocument.Dispose();
                WDocument = null;
            }
        }

        public void SaveToFile(string fileName)
        {
            if (WDocument is not null)
                CloseAndDisposeOfDocument();

            if (_stream is null)
                throw new ArgumentException("This object has already been disposed of so you cannot save it!");

            using (var fs = File.Create(fileName))
            {
                _stream.WriteTo(fs);
            }
            byte[] txt = Encoding.ASCII.GetBytes(
                "<w:p>\n" +
                "  <w:r>\n" +
                "    <w:t>Text</w:t>\n" +
                "  </w:r>\n" +
                "  <w:fldSimple w:instr = \"AUTHOR\">\n" +
                "    <w:r>\n" +
                "      <w:t>Author Name</w:t>\n" +
                "    </w:r>\n" +
                "  </w:fldSimple>\n" +
                "</w:p>\n");

            _stream.Write(txt, 0, txt.Length);
        }
        #endregion
    }
}
