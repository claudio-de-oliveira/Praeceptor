namespace PraeceptorCQRS.Contracts.Values;

public class NameValue : ValueObject
{
    public FirstNameValue? FirstName { get; }
    public SecondNameValue? SecondName { get; }

    public NameValue(FirstNameValue? firstName, SecondNameValue? secondName)
    {
        FirstName = firstName;
            SecondName = secondName;
    }

    public override string? ToString()
    {
        if (!string.IsNullOrEmpty(FirstName?.Text) && !string.IsNullOrEmpty(SecondName?.Text))
            return $"{SecondName.Text.ToUpper()}, {FirstName.Text}";
        if (!string.IsNullOrEmpty(FirstName?.Text))
            return FirstName.Text;
        return SecondName?.Text;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        if (FirstName is not null)
            yield return FirstName;
        if (SecondName is not null)
            yield return SecondName;
    }
}
