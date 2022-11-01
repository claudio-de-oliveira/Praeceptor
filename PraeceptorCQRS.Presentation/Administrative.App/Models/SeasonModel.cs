namespace Administrative.App.Models
{
    public class SeasonModel
    {
        public SeasonModel()
        {
            Components = new List<ComponentModel>();
        }

        public int Order { get; set; }
        public List<ComponentModel> Components { get; set; }

        public int Practice
        {
            get
            {
                int total = 0;
                Components.Where(o => !o.Optative).ToList()
                    .ForEach(c => total += c.Class.Practice);
                return total;
            }
        }
        public int Theory
        {
            get
            {
                int total = 0;
                Components.Where(o => !o.Optative).ToList()
                    .ForEach(c => total += c.Class.Theory);
                return total;
            }
        }

        public override string ToString()
        {
            return $"{Order + 1}º Período";
        }
    }
}
