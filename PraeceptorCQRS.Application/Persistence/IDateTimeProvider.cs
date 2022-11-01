namespace PraeceptorCQRS.Application.Services
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
    }
}

