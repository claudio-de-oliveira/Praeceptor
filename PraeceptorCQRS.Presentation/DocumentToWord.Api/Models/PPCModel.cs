using DocumentToWord.Api.Parser;
using DocumentToWord.Api.Word;

namespace DocumentToWord.Api.Models
{
    public class PPCModel : WordDocument
    {
        public override Task<ImageModel> GetImageByCode(int instituteId, string code)
        {
            throw new NotImplementedException();
        }

        public override Task<TableModel> GetTableByCode(int instituteId, string code)
        {
            throw new NotImplementedException();
        }
    }
}
