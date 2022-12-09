using PraeceptorCQRS.Domain.Base;

namespace PraeceptorCQRS.Domain.Values.PeaValues;

public class VolumeItem : ValueObject
{
    public string Text1 { get; set; }
    public string Text2 { get; set; }

    public VolumeItem()
    {
        this.Text1 = string.Empty;
        this.Text2 = string.Empty;
    }

    public VolumeItem(string text1, string text2)
    {
        this.Text1 = text1.Trim();
        this.Text2 = text2.Trim();
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Text1;
        yield return Text2;
    }
}