﻿namespace Administrative.App.Models
{
    public class AxisTypeModel : Entity
    {
        public string Code { get; set; } = null!;
        public Guid InstituteId { get; set; }
    }
}