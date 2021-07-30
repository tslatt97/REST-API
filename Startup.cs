using Employees.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;

namespace Employees
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
            // Connection til SQL Databasen (Se appsettings.json)
            services.AddDbContext<EmployeeContext>(opt => opt.UseSqlServer
                (Configuration.GetConnectionString("EmployeesConnection")));

            // Extention added to add support for NewtonsoftJson
            services.AddControllers().AddNewtonsoftJson(s =>
            {
                s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IEmployeesRepo, SqlEmployeesRepo>();

            


            // S W A G G E R
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v0.1.1",
                    Title = "Employees C# API",
                    Description = "API Testing - Bachelorprosjekt",
                    Contact = new OpenApiContact
                    {
                        Name = "Bachelor Gruppe",
                        Email = string.Empty,
                        Url = new Uri("https://bachelor.illeris.no/webapplikasjon-med-api/"),
                    },
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            // Aktiver mellomvare for å tjene genererte Swagger som et JSON-sluttpunkt.
            app.UseSwagger();

            // Aktiver mellomvare for å sende swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            // Spesifiserer SWAGGER JSON endpoint
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });




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
