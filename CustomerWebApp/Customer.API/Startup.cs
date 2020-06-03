using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CustomerDemo.Context;
using CustomerDemo.Repositories;
using CustomerDemo.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;

 namespace CustomerDemo.API
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
            services.AddControllers();

            services.AddTransient<ICustomerService, CustomerService>();

            services.AddSingleton<ICustomerRepository, CustomerRepository>();

            //  var databaseName = Configuration["EntityFramework:DatabaseName"];

            services.AddDbContext<CustomerDbContext>(options => options.UseInMemoryDatabase(databaseName: "Customers"));

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Customer Service API",
                    Version = "v1",
                    Description = "Sample Customer service",
                });
            });

          //  services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
 
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            app.UseSwagger();
            app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "Customer Services"));
        }
    }
}
