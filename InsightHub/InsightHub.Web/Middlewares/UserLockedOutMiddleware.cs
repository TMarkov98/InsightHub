using InsightHub.Data.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsightHub.Web.Middlewares
{
    public class UserLockedOutMiddleware
    {
        private readonly RequestDelegate _next;

        public UserLockedOutMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext,
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            if (!string.IsNullOrEmpty(httpContext.User.Identity.Name))
            {
                var user = await userManager.FindByNameAsync(httpContext.User.Identity.Name);

                if (user.IsBanned)
                {
                    await signInManager.SignOutAsync();
                    httpContext.Response.Redirect("/Home/UserBanned");
                }
                if (user.IsPending)
                {
                    await signInManager.SignOutAsync();
                    httpContext.Response.Redirect("/Home/UserPending");
                }
            }
            await _next(httpContext);
        }
    }
}
