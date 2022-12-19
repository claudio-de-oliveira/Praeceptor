using PraeceptorCQRS.Domain.Base;
using PraeceptorCQRS.Domain.DomainEvents;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PraeceptorCQRS.Domain.Entities
{
    public class Institute : AggregateRoot
    {
        private Institute(Guid id)
            : base(id)
        {
            AxisTypes = new HashSet<AxisType>();
            Classes = new HashSet<Class>();
            Courses = new HashSet<Course>();
            ClassTypes = new HashSet<ClassType>();
            Preceptors = new HashSet<Preceptor>();
            PreceptorDegreeTypes = new HashSet<PreceptorDegreeType>();
            PreceptorRegimeTypes = new HashSet<PreceptorRegimeType>();
            PreceptorRoleTypes = new HashSet<PreceptorRoleType>();
            Documents = new HashSet<Document>();
            Chapters = new HashSet<Chapter>();
            Sections = new HashSet<Section>();
            SubSections = new HashSet<SubSection>();
            SubSubSections = new HashSet<SubSubSection>();
            SimpleTables = new HashSet<SimpleTable>();
        }

        public static Institute Create(
            string acronym,
            string name,
            string? address,
            Guid holdingId,
            DateTime created,
            string? createdBy
            )
        {
            var institute = new Institute(Guid.Empty)
            {
                Acronym = acronym,
                Name = name,
                Address = address,
                HoldingId = holdingId,
                Created = created,
                CreatedBy = createdBy,
                LastModified = created,
                LastModifiedBy = createdBy
            };

            institute.RaiseDomainEvent(new InstituteHasBeenCreatedDomainEvent(Guid.NewGuid(), institute.Id, holdingId));

            return institute;
        }

        public void Update(
            string name,
            string? address,
            DateTime lastModified,
            string? lastModifiedBy
            )
        {
            var updated = new Institute(this.Id)
            {
                Acronym = this.Acronym,
                Name = name,
                Address = address,
                HoldingId = this.HoldingId,
                Created = this.Created,
                CreatedBy = this.CreatedBy,
                LastModified = lastModified,
                LastModifiedBy = lastModifiedBy
            };

            updated.RaiseDomainEvent(new InstituteHasBeenUpdatedDomainEvent(Guid.NewGuid(), updated.Id));
        }

        public void Delete()
        {
            RaiseDomainEvent(new InstituteHasBeenDeletedDomainEvent(Guid.NewGuid(), Id));
        }

        [Required, MaxLength(20)]
        public string Acronym { get; set; } = null!;

        [Required, MaxLength(250)]
        public string Name { get; set; } = null!;

        [MaxLength(4000)]
        public string? Address { get; set; }

        public Guid HoldingId { get; set; }

        [Required, ForeignKey("HoldingId")]
        public virtual Holding Holding { get; set; } = null!;

        public virtual ICollection<AxisType> AxisTypes { get; set; }
        public virtual ICollection<Class> Classes { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<ClassType> ClassTypes { get; set; }
        public virtual ICollection<Preceptor> Preceptors { get; set; }
        public virtual ICollection<PreceptorDegreeType> PreceptorDegreeTypes { get; set; }
        public virtual ICollection<PreceptorRegimeType> PreceptorRegimeTypes { get; set; }
        public virtual ICollection<PreceptorRoleType> PreceptorRoleTypes { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
        public virtual ICollection<Chapter> Chapters { get; set; }
        public virtual ICollection<Section> Sections { get; set; }
        public virtual ICollection<SubSection> SubSections { get; set; }
        public virtual ICollection<SubSubSection> SubSubSections { get; set; }
        public virtual ICollection<SimpleTable> SimpleTables { get; set; }
    }
}