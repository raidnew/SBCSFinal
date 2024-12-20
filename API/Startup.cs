using API.Auth;
using API.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using API.Models;
using Microsoft.AspNetCore.Identity;
using API.Interfaces;
using API.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using CommonData.Models;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<DBContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("DBCRM")));
            ConfigureJWT(services);

            services.AddControllers();
            services.AddScoped<IEntriesStorage<Order>, OrdersEntries>();
            services.AddScoped<IEntriesStorage<OrdersStatus>, OrdersStatuses>();
            services.AddScoped<IEntriesStorage<ProjectEntry>, ProjectEntries>();
            services.AddScoped<IEntriesStorage<ServiceEntry>, ServicesEntries>();
            services.AddScoped<IEntriesStorage<ContactEntry>, ContactsEntries>();
            services.AddScoped<IEntriesStorage<BlogEntry>, BlogsEntries>();
            services.AddScoped<IEntriesStorage<MenuItem>, MenuItems>();
            services.AddScoped<IEntriesStorage<HeaderText>, HeaderTexts>();

            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<DBContext>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 1;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Lockout.AllowedForNewUsers = true;
            });
        }

        private void ConfigureJWT(IServiceCollection services)
        {
            const string signingSecurityKey = "111111111111111111111111111111111111111111111111";
            var signingKey = new SigningSymmetricKey(signingSecurityKey);
            services.AddSingleton<IJwtSigningEncodingKey>(signingKey);
            const string jwtSchemeName = "TestScheme";

            var signingDecodingKey = (IJwtSigningDecodingKey)signingKey;
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(jwtSchemeName, jwtBearerOptions => {
                    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = signingDecodingKey.GetKey(),
                        ValidateAudience = true,
                        ValidAudience = "testa",
                        ValidateIssuer = true,
                        ValidIssuer = "testi",
                        ValidateLifetime = true
                    };
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
