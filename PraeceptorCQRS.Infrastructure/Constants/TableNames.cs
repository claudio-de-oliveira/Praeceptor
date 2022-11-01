namespace PraeceptorCQRS.Infrastructure.Constants
{
    public static class TableNames
    {
        public const string Documents = "DocumentTable";
        public const string SqlFileStreams = "FileStreamTable";
        public const string SubSubSections = "SubSubSectionTable";
        public const string SubSections = "SubSectionTable";
        public const string Sections = "SectionTable";
        public const string Chapters = "ChapterTable";
        public const string PreceptorDegreeTypes = "PreceptorDegreeTypeTable";
        public const string PreceptorRegimeTypes = "PreceptorRegimeTypeTable";
        public const string AxisTypes = "AxisTypeTable";
        public const string ClassTypes = "ClassTypeTable";
        public const string Preceptors = "PreceptorTable";
        public const string Courses = "CourseTable";
        public const string Institutes = "InstituteTable";
        public const string Classes = "ClassTable";
        public const string Holdings = "HoldingTable";
        public const string Nodes = "ListNodeTable";
        public const string Components = "ComponentTable";
        public const string Groups = "GroupTable";
        public const string GroupValues = "GroupValueTable";
        public const string Variables = "VariableTable";
        public const string VariableValues = "VariableValueTable";
        public const string Peas = "PeaTable";

        public const string OutboxMessages = "OutboxMessageTable";
        public const string OutboxMessageConsumers = "OutboxMessageConsumerTable";
    }
}
