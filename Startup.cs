using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.API.Data;
using DatingApp.API.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            //This would tell when to use development db
            services.AddDbContext<DataContext>(x =>
            x.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

            ConfigureServices(services);
        }

        public void ConfigureProductionServices(IServiceCollection services)
        {
            //This would tell when to use production db
            services.AddDbContext<DataContext>(x =>
            x.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            ConfigureServices(services);
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //add a service to be run for data connection
            //cmd>dotnet ef migrations add InitialCreate... That commmand creates the migration files
            //cmd>dotnet ef database update... That command uses migration files to create stuff on DB
            //services.AddDbContext<DataContext>(x => 
            //x.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

            //Newtonsoft to get json responses in a proper format
            //Got a loop error Newtonsoft.Json.Serialization.JsonSerializerInternalWriter.CheckForCircularReference so 
            //fix it telling it to ignore the error
            services.AddControllers().AddNewtonsoftJson(opt =>
            {
                opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            //Automapper injected to use mapping between normal clases and DTOs to present filtered data from DB
            //This gives error for ambiguous callings, so next line solves it telling it which assembly to use
            //services.AddAutoMapper();
            services.AddAutoMapper(typeof(DatingRepository).Assembly);

            //Cors for allowing API calls cross domain
            services.AddCors();

            //Single instance of the repository using singleton for concurrent requests, just create once
            //AddTransient is useful for creating each time it is requested, lightweight services
            //AddScoped, creates the instance once per scope
            //This will inject the IAuthRepository would be injected on all controllers
            services.AddScoped<IAuthRepository, AuthRepository>();

            services.AddScoped<IDatingRepository, DatingRepository>();

            //Add authorization as a service so the controllers know how to deal with authorization attribute [Authorize]
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    //Get the key that is stored in the configuration, use that one as on appsettings:token
                    //The key is a string so need to encode to byte array
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            //Service to link Cloudinary settings from appsettings to the class
            services.Configure<CloudinarySettings>(Configuration.GetSection("CloudinarySettings"));


            services.AddControllers();

            // Because need to create a new instance of the pull request
            services.AddScoped<LogUserActivity>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else {
                app.UseExceptionHandler(builder => {
                    builder.Run(async context =>
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                        var error = context.Features.Get<IExceptionHandlerFeature>();

                        if (error! == null)
                        {
                            //this extends the response isung the Helpers/Extensions methods to include the headers
                            //put it before the response
                            context.Response.AddApplicationError(error.Error.Message);
                            //This message returned is missing headers (misleaing response)
                            await context.Response.WriteAsync(error.Error.Message);
                        }
                    });
                });
            }

            //app.UseHttpsRedirection();

            //Add cors to the pipeline
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            // Adding support to static files
            // Use default files inside wwwroot folder
            app.UseDefaultFiles();

            // Add the hability to use does files
            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapFallbackToController("Index", "Fallback");
            });
        }
    }
}
