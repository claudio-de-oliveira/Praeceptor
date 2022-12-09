namespace Document.App.Models
{
    public class ListOfNomesModel
    {
        private readonly List<NameValueModel> _list = new();

        public List<NameValueModel> Nomes => _list;
    }
}