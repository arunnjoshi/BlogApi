using Autofac;
using AutoMapper;
using BlogApi.common;
using BlogApi.Jwt;
using BlogApi.Models;
using DataBaseLayer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BlogApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var appSettings = new AppSettings();
            Configuration.GetSection(nameof(AppSettings)).Bind(appSettings);
            // add dependency container
            var depdency = new ContainerFactory(Configuration);
            var container = depdency.container;

            services.AddControllers();
            // auto mappper
            services.AddAutoMapper(typeof(MappingProfiles));
            //configure AppSettings IMongoCURD IMongoUser
            services.AddSingleton(container.Resolve<AppSettings>());
            services.AddSingleton(container.Resolve<IMongoCURD>());
            services.AddSingleton(container.Resolve<IMongoUser>());

            //jwt injection
            services.AddSingleton(container.Resolve<IJwtAuthManager>());
            //Configure MogoCurd
            //services.AddSingleton<IMongoCURD>(new MongoCURD(dbName, connectionString));
            // Mongo Injection
            //services.AddSingleton(container.Resolve<IMongoUser>(
            //        new NamedParameter("connectionString", connectionString), new NamedParameter("dbName", dbName))
            //    );
            // jwt authtoken
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(appSettings.JwtKey)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

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