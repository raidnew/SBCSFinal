using API.Auth;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using WebClient.Auth;
using WebClient.Models;

namespace WebClient
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
            services.AddControllersWithViews();

            services.AddMvc();
            services.AddHttpContextAccessor();

            const string signingSecurityKey = "111111111111111111111111111111111111111111111111";
            var signingKey = new SigningSymmetricKey(signingSecurityKey);
            services.AddSingleton<IJwtSigningEncodingKey>(signingKey);
            const string jwtSchemeName = "TestScheme";

            var signingDecodingKey = (IJwtSigningDecodingKey)signingKey;

            /*
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie();
            */
            /*
            var builder = services.AddIdentityCore<AppUser>();
            var identityBuilder = new IdentityBuilder(builder.UserType, builder.Services);
            identityBuilder.AddSignInManager<SignInManager<AppUser>>();
            */
            /*
            services.AddAuthorization(options =>
            {
                options.AddPolicy("customauth", policy => policy.RequireClaim(ClaimTypes.Authentication));
            });
            */
            services.AddAuthentication(MyAuthenticationOptions.DefaultScheme)
                .AddScheme<MyAuthenticationOptions, MyAuthenticationHandler> 
                    (
                    MyAuthenticationOptions.DefaultScheme,
                    options => {
                        //options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    }
                    ).AddCookie();

            services.AddAuthorization();

            /*
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = jwtSchemeName;
                    options.DefaultChallengeScheme = jwtSchemeName;
                    options.DefaultScheme = jwtSchemeName;
                })
                .AddJwtBearer(jwtSchemeName, options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = "testi",
                        ValidateAudience = true,
                        ValidAudience = "testa",
                        ValidateLifetime = true,
                        IssuerSigningKey = signingDecodingKey.GetKey(),
                        ValidateIssuerSigningKey = false,
                    };
                });
            */
            /*
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 1;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Lockout.AllowedForNewUsers = true;
            });
            */
            services.AddSession();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseSession();
            app.UseAuthentication(); ;

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
           
        }

    }
}
