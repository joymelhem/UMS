using DomainLibrary.Interfaces;

namespace UMS.Api.Middleware;

public class TenantMiddleware
{
    private readonly RequestDelegate _next;

    public TenantMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ITenantContext tenantContext)
    {
        var branchIdClaim = context.User.FindFirst("branchid");
        if (branchIdClaim != null && long.TryParse(branchIdClaim.Value, out var branchId))
        {
            tenantContext.branchid = branchId; 
        }

        await _next(context);
    }
}
