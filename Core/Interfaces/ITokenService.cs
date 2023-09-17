using Core.Entities;

namespace Core.Interfaces
{
    // Interface for the token service, responsible for creating tokens
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}