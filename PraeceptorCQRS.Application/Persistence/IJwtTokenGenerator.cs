namespace PraeceptorCQRS.Application.Authentication
{
    public interface IJwtTokenGenerator
    {
        string StringGenerator(Domain.Entities.ApplicationUser user);
    }
}

