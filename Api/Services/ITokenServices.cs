

namespace Api.Services
{
    public interface ITokenServices
    {
        string CriarToken(string id, string email);
    }
}