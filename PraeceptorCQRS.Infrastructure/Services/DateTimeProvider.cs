using PraeceptorCQRS.Application.Services;

namespace PraeceptorCQRS.Infrastructure.Services
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}

