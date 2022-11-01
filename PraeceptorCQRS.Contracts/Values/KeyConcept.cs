namespace PraeceptorCQRS.Contracts.Values
{
    public class KeyConcept
    {
        public Guid Key { get; }
        public string Description { get; } = string.Empty;
        public List<string> Contents { get; } = new();
    }
}
