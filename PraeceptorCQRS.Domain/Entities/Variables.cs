using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using PraeceptorCQRS.Domain.Errors;

namespace PraeceptorCQRS.Domain.Entities
{
    public class Group
    {
        public Group()
        {
            Values = new HashSet<GroupValue>();
            Variables = new HashSet<Variable>();
        }

        [Key]
        public Guid Id { get; set; }
        [Required, MaxLength(20)]
        public string Code { get; set; } = default!;

        public Guid InstituteId { get; set; }
        [Required, ForeignKey("InstituteId")]
        public virtual Institute Institute { get; set; } = null!;
        public virtual ICollection<GroupValue> Values { get; set; }
        public virtual ICollection<Variable> Variables { get; set; }
    }

    public class GroupValue
    {
        public GroupValue()
        {
            VariableValues = new HashSet<VariableValue>();
        }

        [Key]
        public Guid Id { get; set; }
        public string Value { get; set; } = default!;

        public Guid GroupId { get; set; }
        [Required, ForeignKey("GroupId")]
        public virtual Group Group { get; set; } = default!;

        public virtual ICollection<VariableValue> VariableValues { get; set; }
    }

    public class Variable
    {
        public Variable()
        {
            Values = new HashSet<VariableValue>();
        }

        [Key]
        public Guid Id { get; set; }
        [Required, MaxLength(20)]
        public string Code { get; set; } = default!;

        public Guid GroupId { get; set; }
        [Required, ForeignKey("GroupId")]
        public virtual Group Group { get; set; } = default!;
        public virtual ICollection<VariableValue> Values { get; set; }
    }

    public class VariableValue
    {
        [Key]
        public Guid Id { get; set; }
        public string Value { get; set; } = default!;

        public Guid GroupValueId { get; set; }
        [Required, ForeignKey("GroupValueId")]
        public virtual GroupValue GroupValue { get; set; } = default!;

        public Guid VariableId { get; set; }
        [Required, ForeignKey("VariableId")]
        public virtual Variable Variable { get; set; } = default!;
    }
}
