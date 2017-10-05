using System;
using System.Security.Claims;
using System.Threading.Tasks;
using alfrek.api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace alfrek.api.Authorization
{
    public class SolutionAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, Solution>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            OperationAuthorizationRequirement requirement, Solution resource)
        {
            if (context.User.FindFirstValue(ClaimTypes.NameIdentifier).Equals(resource.Author.Email))
            {
                context.Succeed(requirement);
            }

            return Task.FromResult(0);
        }
    }
}