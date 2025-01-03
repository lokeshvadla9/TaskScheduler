using TaskScheduler.Agent.Interfaces;
using TaskScheduler.Helper.Interfaces;

namespace TaskScheduler.API.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
                _next = next;
        }

        public async Task Invoke(HttpContext context,IUserAgent userAgent, IJwtUtils jwtUtils)
        {
            var token= context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            var userId=jwtUtils.ValidateToken(token);
            if (userId != null)
            {
                context.Items["UserId"] = userId;
            }

            await _next(context);

        }
    }
}
