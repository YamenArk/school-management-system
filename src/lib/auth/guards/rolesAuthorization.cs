using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Auth.Jwt
{
    public class RolesAuthorization : AuthorizeAttribute, IAuthorizationFilter
    {
        private readonly UserType[] _allowedRoles;

        public RolesAuthorization(params UserType[] allowedRoles)
        {
            _allowedRoles = allowedRoles;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            if (user?.Identity is not ClaimsIdentity identity || !identity.IsAuthenticated)
            {
                context.Result = new UnauthorizedResult();
                return;
            }
            var userIdClaim = identity.FindFirst(ClaimTypes.NameIdentifier) ?? identity.FindFirst("sub");
            if (userIdClaim == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var userId = userIdClaim.Value;

            var roleClaim = identity.FindFirst(ClaimTypes.Role)?.Value;
            if (string.IsNullOrEmpty(roleClaim)
                || !Enum.TryParse<UserType>(roleClaim, ignoreCase: true, out var userRole))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            if (!_allowedRoles.Contains(userRole))
            {
                context.Result = new ForbidResult();
                return;
            }

            context.HttpContext.Items["UserId"] = userId;
            context.HttpContext.Items["UserRole"] = userRole;
        }
    }
}
