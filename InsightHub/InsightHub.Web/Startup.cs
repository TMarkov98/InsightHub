using Azure.Storage.Blobs;
using InsightHub.Web.Middlewares;
using InsightHub.Data;
using InsightHub.Data.Entities;
using InsightHub.Models;
using InsightHub.Services;
using InsightHub.Services.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;

namespace InsightHub.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<InsightHubContext>(options =>
                    options.UseSqlServer(
                        Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<Role>()
                .AddEntityFrameworkStores<InsightHubContext>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
            });


            services.AddMvc();

            services.AddControllersWithViews();

            services.AddRazorPages();

            services.AddSingleton(x =>
                new BlobServiceClient(Configuration.GetValue<string>("AzureBlobStorageConnectionString")));
            services.AddSingleton<IBlobServices, BlobServices>();
            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<IIndustryServices, IndustryServices>();
            services.AddScoped<IReportServices, ReportServices>();
            services.AddScoped<ITagServices, TagServices>();
            services.AddScoped<IEmailSenderServices, EmailSenderServices>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(errorApp =>
                {
                    errorApp.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        context.Response.ContentType = "text/html";

                        await context.Response.WriteAsync("<html lang=\"en\"><body>\r\n");
                        await context.Response.WriteAsync("Error!<br><br>\r\n");

                        var exceptionHandlerPathFeature =
                            context.Features.Get<IExceptionHandlerPathFeature>();

                        // Use exceptionHandlerPathFeature to process the exception (for example, 
                        // logging), but do NOT expose sensitive error information directly to 
                        // the client.

                        if (exceptionHandlerPathFeature?.Error is FileNotFoundException)
                        {
                            await context.Response.WriteAsync("File error thrown!<br><br>\r\n");
                        }
                        else if (exceptionHandlerPathFeature?.Error is ArgumentNullException)
                        {
                            await context.Response.WriteAsync($"{exceptionHandlerPathFeature.Error.Message}\r\n");
                        }
                        else if (exceptionHandlerPathFeature?.Error is ArgumentException)
                        {
                            await context.Response.WriteAsync($"{exceptionHandlerPathFeature.Error.Message}\r\n");
                        }

                        await context.Response.WriteAsync("<a href=\"/\">Return to Home</a><br>\r\n");
                        await context.Response.WriteAsync("</body></html>\r\n");
                        await context.Response.WriteAsync(new string(' ', 512)); // IE padding
                    });
                });
                app.UseHsts();
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseCookiePolicy();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<NotFoundMiddleware>();
            app.UseMiddleware<ArgumentExceptionMiddleware>();
            app.UseMiddleware<UserLockedOutMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
