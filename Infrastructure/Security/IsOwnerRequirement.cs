using System.Security.Claims;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Security
{
    public class IsOwnerRequirement : IAuthorizationRequirement { }

    public class IsOwnerRequirementHandler : AuthorizationHandler<IsOwnerRequirement>
    {
        // Injecting the db context and the http context accessor via constructor
        private readonly DataContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public IsOwnerRequirementHandler(DataContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        // Override of the HandleRequirementAsync method from the AuthorizationHandler class
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsOwnerRequirement requirement)
        {
            // Find the user making the request using the name identifier claim from the token. If not found returns Task with no completion (Forbidden)
            var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Task.CompletedTask;

            // Gets the set id from the request route values, pulled from the http context
            var postId = _httpContextAccessor.HttpContext?.Request.RouteValues.SingleOrDefault(x => x.Key == "id").Value?.ToString();

            // Find the set that is being modified, includes the app user. If not found return null.
            var post = _dbContext.Posts.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == postId).Result;
            if (post == null) return Task.CompletedTask;

            // If the app user id on the set matches the user id from the token, add success to the requirement.
            if (post.UserId == userId) context.Succeed(requirement);
            _dbContext.Entry(post).State = EntityState.Detached;

            // Return completed task. If the previous check passed, this will allow the user to make the change to the resource
            return Task.CompletedTask;
        }
    }
}
