using Microsoft.AspNetCore.Authorization;

namespace RoutePlanning.Client.Web.Authorization;

public sealed record TokenRequirement(string Token) : IAuthorizationRequirement;

public sealed class TokenRequirementHandler : AuthorizationHandler<TokenRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, TokenRequirement requirement)
    {
        if (context.Resource is HttpContext httpContext)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
