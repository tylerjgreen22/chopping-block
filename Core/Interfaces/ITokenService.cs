using Core.Entities;

namespace Core.Interfaces
{
    // Interface defining the contract that implementations of Token Service must follow
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}