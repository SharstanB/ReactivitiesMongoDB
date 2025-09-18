using Domain.Absractions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistence.Services.DBServices;
using System.Security.Claims;


namespace Application.Middlewares
{
    public class DeviceIdMiddleWare
    {
        public readonly RequestDelegate _next;

        public DeviceIdMiddleWare(RequestDelegate next) => _next = next;
          
        public async Task InvokeAsync(HttpContext context, IDeviceContext deviceContext, 
            MongoDBService appDBContext, ITokenBlacklistService blacklistService)
        {
            var path = context.Request.Path.Value?.ToLowerInvariant();

            if (path != null && (path.Contains("/swagger") || path.Contains("/index") || 
                path.Contains("/signup") || path.Contains("/signin")))
            {
                await _next(context);
                return;
            }
            if (!context.Request.Headers.TryGetValue("Device-Id", out var deviceId))
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("Missing Device-Id header");
                return;
               
            }
            deviceContext.DeviceId = context.Request.Headers["Device-Id"].ToString();

            var userId = new Guid(context.User.Claims.FirstOrDefault()!.Value);
          
            if (userId == Guid.Empty)
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync("invalid user");
                return;
            }
            var user = userId != Guid.Empty ? appDBContext.Users
                       .Include(user => user.UserDevices)
                       .FirstOrDefault(user => user.Id == userId) : null;

            var usertoken = user!.UserDevices!
                .FirstOrDefault(token => token.DeviceId == deviceContext.DeviceId);

            deviceContext.Initialize(userId);

            var tokenIsBlocked = await blacklistService.IsBlacklistedAsync(usertoken!.Id);

            if (usertoken is null ||  deviceContext.DeviceId != usertoken.DeviceId || tokenIsBlocked)
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync("invalid device, try to login again");
                return;
            }

            await _next(context);
        }


        //private string GenerateFingerPrint(HttpContext httpContext , string? userEmail)
        //{
        //    string fingerprintString = "";
        //    var ip = httpContext.Connection.RemoteIpAddress.ToString();
        //    var userAgent = httpContext.Request.Headers["User-Agent"].ToString();
        //    var timezone = httpContext.Request.Headers["Time-zone"].ToString();
            
        //    fingerprintString = userAgent + ip + timezone + userEmail;

        //    return fingerprintString;

        //}

       
    }
}
