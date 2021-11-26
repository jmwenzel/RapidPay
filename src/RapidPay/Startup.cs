using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using RapidPay.App.DependencyInjection;
using RapidPay.Infrastructure.Core;
using RapidPay.Infrastructure.Core.Middleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using RapidPay.Data.Models;
using Microsoft.EntityFrameworkCore;
using Autofac;

namespace RapidPay
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
            services.AddControllers(options => {
                options.Filters.Add(typeof(GlobalExceptionFilterAttribute));
            });
            services.AddDependendcies(Configuration);

            services.AddDbContext<RepositoryContext>(options => 
                options.UseSqlServer(Configuration.GetConnectionString("RapidPay")));

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", options => { })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidIssuer = Configuration.GetValue<string>("OAuth:Issuer"),
                        ValidAudience = Configuration.GetValue<string>("OAuth:Audience"),
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetValue<string>("OAuth:SymmetricSecurityKey"))),
                        ClockSkew = TimeSpan.Zero
                    };
                });
                

            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build();
                options.AddPolicy("BasicAuthentication", 
                    new AuthorizationPolicyBuilder("BasicAuthentication").RequireAuthenticatedUser().Build());
            });

            services.RegisterSwagger();
        }

        /// <summary>
        /// Using Autofac here because ASP.NET Core DI does not support constrained open generics,
        /// </summary>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new MediatorModule());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger(setup =>
                    setup.RouteTemplate = "rapidpay-api/swagger/{documentName}/swagger.json"
                );

                app.UseSwaggerUI(
                    options =>
                    {
                        foreach (var description in provider.ApiVersionDescriptions)
                        {
                            options.SwaggerEndpoint($"/rapidpay-api/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                            options.RoutePrefix = "rapidpay-api/swagger";
                        }
                    }
                );
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
