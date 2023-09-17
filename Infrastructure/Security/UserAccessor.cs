using System.Security.Claims;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Security
{
    // User accessor for gaining access to the http context accessor
    public class UserAccessor : IUserAccessor
    {
        // Injecting the http context accessor via the constructor
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly DataContext _context;
        public UserAccessor(IHttpContextAccessor httpContextAccessor, DataContext context)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        // Method that gets the user from the token claims
        public async Task<AppUser> GetUser()
        {
            // Check auth header is present on request. If missing return null
            var authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(authorizationHeader)) return null;

            // Find user via email from token claims
            var email = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);

            return user;
        }
    }
}