using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using alfrek.api.Authorization;
using alfrek.api.Configuration;
using alfrek.api.Interfaces;
using alfrek.api.Models;
using alfrek.api.Persistence;
using alfrek.api.Repositories;
using alfrek.api.Repositories.Interfaces;
using alfrek.api.Services;
using alfrek.api.Services.Interfaces;
using alfrek.api.Storage;
using alfrek.api.Storage.Interfaces;
using Amazon;
using Amazon.Runtime.CredentialManagement;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;

namespace alfrek.api
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
            services.AddDbContext<AlfrekDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Default")));

/*
            services.AddDbContext<UserDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Default")));
*/

            services.AddIdentity<ApplicationUser, IdentityRole>(x =>
                {
                    x.Password.RequiredLength = 8;
                    x.Password.RequireUppercase = false;
                    x.Password.RequireLowercase = false;
                    x.Password.RequireNonAlphanumeric = false;
                    x.Password.RequireDigit = true;
                })
                .AddEntityFrameworkStores<AlfrekDbContext>();

            services.Configure<TokenConfiguration>(Configuration.GetSection("Token"));
            
            services.Configure<AwsConfiguration>(Configuration.GetSection("AWS"));

            services.AddTransient<ITokenService,TokenService>();

            services.AddSingleton<IAuthorizationHandler, SolutionAuthorizationHandler>();

            services.AddSingleton<ICloudStorage, AwsStorage>();

            services.AddTransient<ISolutionRepository, SolutionRepository>();
            services.AddTransient<IResourceRepository, ResourceRepository>();

            var secretKey = Configuration.GetSection("Token").GetValue<string>("Secret");
            
            services.AddAuthentication(options =>
                {
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false; //TODO: Refactor to appsettings
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidIssuer = Configuration.GetSection("Token").GetValue<string>("Issuer"),
                        ValidAudience = Configuration.GetSection("Token").GetValue<string>("Audience"),
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                    };
                    options.Validate();
                });

            services.AddCors();
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Alfrek API", Version = "v1"});
            });
            
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, 
            IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
               /* var netSDKFile = new NetSDKCredentialsFile();
                CredentialProfile basicProfile;
                
                if (netSDKFile.TryGetProfile(Configuration.GetSection("AWS").GetValue<string>("ProfileName"), out basicProfile))
                {
                    basicProfile.Region = RegionEndpoint.EUCentral1;
                    basicProfile.Options.AccessKey = Configuration.GetSection("AWS").GetValue<string>("Key");
                    basicProfile.Options.SecretKey = Configuration.GetSection("AWS").GetValue<string>("Secret");
                    
                    netSDKFile.RegisterProfile((basicProfile));
                    Console.WriteLine("SUCCESS: CREATED PROFILE");
                }*/
                app.UseDeveloperExceptionPage();
            }
            CreateRoles(serviceProvider).Wait();

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            
            app.UseAuthentication();


            app.UseCors(builder =>
                builder.WithOrigins("http://localhost:8080")
                .AllowAnyHeader()
                .AllowAnyMethod()
                );

            using (var context = serviceProvider.GetService<AlfrekDbContext>())
            {
                if (context.AllMigrationsApplied())
                {
                    context.EnsureSeeded();
                }
            }
            
            // Add application roles
            //TODO: Not when doing Migraitons

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Alfrek API V1");
            });

            app.UseMvc();
        }
        
        
        // Add application roles
        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            string[] roleNames = {"Admin", "Researcher", "Member"};
            IdentityResult roleResult;

            var email = Configuration.GetSection("Administration").GetValue<string>("Email");
            var password = Configuration.GetSection("Administration").GetValue<string>("Password");

            foreach (var role in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(role);
                if (!roleExist)
                {
                    roleResult = await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
            
            // Check if admin user exists
            var existingAdmin = await userManager.FindByEmailAsync(email);
            
            // If not, create the admin
            if (existingAdmin == null)
            {
                var admin = new ApplicationUser
                {
                    UserName = email,
                    Email = email
                };
                var result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }       
            
        }
    }
}