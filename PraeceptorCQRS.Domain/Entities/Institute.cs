using PraeceptorCQRS.Domain.Base;
using PraeceptorCQRS.Domain.DomainEvents;
using PraeceptorCQRS.Domain.Values;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PraeceptorCQRS.Domain.Entities
{
    public class Institute : AggregateRoot
    {
        private Institute(Guid id)
            : base(id)
        {
            Classes = new HashSet<Class>();
            Courses = new HashSet<Course>();
            ClassTypes = new HashSet<ClassType>();
            Preceptors = new HashSet<Preceptor>();
            PreceptorDegreeTypes = new HashSet<PreceptorDegreeType>();
            PreceptorRegimeTypes = new HashSet<PreceptorRegimeType>();
            Groups = new HashSet<Group>();
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

        public virtual ICollection<Class> Classes { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<ClassType> ClassTypes { get; set; }
        public virtual ICollection<Preceptor> Preceptors { get; set; }
        public virtual ICollection<PreceptorDegreeType> PreceptorDegreeTypes { get; set; }
        public virtual ICollection<PreceptorRegimeType> PreceptorRegimeTypes { get; set; }
        public virtual ICollection<Group> Groups { get; set; }

    }
}

