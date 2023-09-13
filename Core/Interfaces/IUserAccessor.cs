using Core.Entities;

namespace Core.Interfaces
{
    // Interface for the user accessor, defines the methods that the user accessor must implement.
    public interface IUserAccessor
    {
        Task<AppUser> GetUser();
    }
}