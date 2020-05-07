using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using ReactCoreWebApi.Models;
using Microsoft.AspNetCore.Internal;
 
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Internal;
 
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Session;
using System.Net;

namespace ReactCoreWebApi
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {




            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder.WithOrigins("http://localhost:3000",
                                                          "http://localhost:3001")
                                         .AllowAnyHeader()
                                        .AllowAnyMethod()
                                        
                                      .WithHeaders(HeaderNames.ContentType, "x-custom-header")
                                      .WithMethods("PUT", "DELETE", "GET", "OPTIONS","POST");



                                      ;
                                      ;
                                  });
            });


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });





            services.AddMvc();
            services.AddDbContext<NorthwindContext>(options =>
options.UseSqlServer(Configuration.GetConnectionString("NorthwindConnection")));
             

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            

            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
 
            app.UseCors();

            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseCors();

             

            app.UseMvcWithDefaultRoute();
         
           

          
        }
    }
}
