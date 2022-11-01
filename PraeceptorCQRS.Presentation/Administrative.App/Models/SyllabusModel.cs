namespace Administrative.App.Models
{
    public class SyllabusModel : Entity
    {
        public SyllabusModel()
        {
            Seasons = new HashSet<SeasonModel>();
        }

        public int Curriculum { get; set; }
        public Guid CourseId { get; set; }
        public virtual ICollection<SeasonModel> Seasons { get; set; }
    }
}
