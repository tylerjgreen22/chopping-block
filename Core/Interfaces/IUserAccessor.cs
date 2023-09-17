using Core.Entities;

namespace Core.Interfaces
{
    // Interface for the user accessor which is responsible for getting the user from the http context
    public interface IUserAccessor
    {
        Task<AppUser> GetUser();
    }
}