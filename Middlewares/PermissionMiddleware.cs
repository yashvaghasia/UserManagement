using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using UserManagement.Attributes;
using UserManagement.Permissions;

public class PermissionMiddleware
{
    private readonly RequestDelegate _next;

    public PermissionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var endpoint = context.GetEndpoint();

        if (endpoint != null)
        {
            var requiredPermission = endpoint.Metadata.GetMetadata<RequiresPermissionAttribute>()?.Permission;

            if (!string.IsNullOrEmpty(requiredPermission))
            {
                var userRole = context.User.FindFirst(ClaimTypes.Role)?.Value;

                if (userRole == null || !Permissions.RolePermissions.TryGetValue(userRole, out var allowedPermissions) || !allowedPermissions.Contains(requiredPermission))
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    await context.Response.WriteAsync("Access Denied");
                    return;
                }
            }
        }

        await _next(context);
    }
}
