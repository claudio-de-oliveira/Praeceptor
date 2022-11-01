using System.Net;

namespace PraeceptorCQRS.Application.Errors
{
    public interface IServiceException
    {
        public HttpStatusCode StatusCode { get; }
        public string ErrorMessage { get; }
    }
}

