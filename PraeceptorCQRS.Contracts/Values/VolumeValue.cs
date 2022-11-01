namespace PraeceptorCQRS.Contracts.Values;

public class VolumeValue : ValueObject
{
    public VolumeNumberValue? VolumeNumber { get; }
    public VolumeTitleValue? VolumeTitle { get; }

    public VolumeValue(VolumeNumberValue? volumeNumber, VolumeTitleValue? volumeTitle)
    {
        this.VolumeNumber = volumeNumber;
        this.VolumeTitle = volumeTitle;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        if (VolumeNumber is not null) 
            yield return VolumeNumber;
        if (VolumeTitle is not null)
            yield return VolumeTitle;
    }
}
