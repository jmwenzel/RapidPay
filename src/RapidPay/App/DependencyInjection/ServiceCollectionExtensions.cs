using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using RapidPay.Data.Repositories;
using RapidPay.Infrastructure.Core.Settings;
using RapidPay.Service.Services;
using System;
using System.IO;
using System.Reflection;

namespace RapidPay.App.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add Dependencies
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddDependendcies(this IServiceCollection services,
            IConfiguration configuration)
        {
            //Settings
            services.Configure<OAuthSettings>(configuration.GetSection("OAuth"));

            services.AddSingleton<IUniversalFeeExchange, UniversalFeeExchange>();

            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<ICardRepository, CardRepository>();
            
            services.AddTransient<ICardService, CardService>();
            services.AddTransient<IPaymentService, PaymentService>();

            // Automapper
            services.AddAutoMapper(typeof(Startup));
        }

        public static void RegisterSwagger(this IServiceCollection services)
        {
            // Register api versioning support
            services.AddApiVersioning(options => options.ReportApiVersions = true);
            services.AddVersionedApiExplorer(
                options =>
                {
                    //The format of the version added to the route URL  
                    options.GroupNameFormat = "'v'VVV";
                    //Tells swagger to replace the version in the controller route  
                    options.SubstituteApiVersionInUrl = true;
                });

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Rapid Pay Web API",
                    Version = "v1"
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

    }
}
