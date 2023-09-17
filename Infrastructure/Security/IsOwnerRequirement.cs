using System.Security.Claims;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Security
{
    public class IsOwnerRequirement : IAuthorizationRequirement { }

    public class IsOwnerRequirementHandler : AuthorizationHandler<IsOwnerRequirement>
    {
        // Injecting the db context and the http context accessor via constructor
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public IsOwnerRequirementHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
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
            var spec = new PostSpecification(postId);
            var post = _unitOfWork.Repository<Post>().GetEntityWithSpecAsync(spec).Result;
            if (post == null) return Task.CompletedTask;

            // If the app user id on the set matches the user id from the token, add success to the requirement.
            if (post.UserId == userId) context.Succeed(requirement);

            // Return completed task. If the previous check passed, this will allow the user to make the change to the resource
            return Task.CompletedTask;
        }
    }
}
