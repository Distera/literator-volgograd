using LiteratorVolgograd.Models;
using LiteratorVolgograd.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LiteratorVolgograd
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
            services.AddDbContext<ApplicationContext>(x => x.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            services
                .AddIdentity<IdentityUser, IdentityRole>(opts =>
                {
                    opts.User.RequireUniqueEmail = true;
                    opts.Password.RequiredLength = 6;
                    opts.Password.RequireNonAlphanumeric = false;
                    opts.Password.RequireLowercase = false;
                    opts.Password.RequireUppercase = false;
                    opts.Password.RequireDigit = false;
                })
                .AddEntityFrameworkStores<ApplicationContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "auth";
                options.Cookie.HttpOnly = true;
                options.LoginPath = "/account/login";
                options.SlidingExpiration = true;
            });

            const string adminPolicyName = "AdminPolicy";
            services.AddAuthorization(x => x.AddPolicy(adminPolicyName, policy => policy.RequireRole("admin")));

            services.AddMvc(x =>
                {
                    x.Conventions.Add(new AdminAuthorization(adminPolicyName));
                }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ApplicationContext dataContext)
        {
            dataContext.Database.Migrate();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                   name: "news",
                   template: "{controller=News}/{action=News}/{id?}");

                routes.MapRoute(
                    name: "authors",
                    template: "{controller=Authors}/{action=Authors}/{id?}");

                routes.MapRoute(
                    name: "projects",
                    template: "{controller=Projects}/{action=Projects}/{id?}");

                routes.MapRoute(
                    name: "publications",
                    template: "{controller=Publications}/{action=Publications}/{id?}");
            });
        }
    }
}
