using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Task_API.Models;
using Task_API.Repository;

namespace Task_API
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });
            services.AddMvc(); 
            services.AddControllers();
            services.AddTransient<IStudentRepository, StudentRepository>();
            services.AddAuthentication(MyCustomTokenAuthOptions.DefaultScemeName)
             .AddScheme<MyCustomTokenAuthOptions, MyCustomTokenAuthHandler>(
                 MyCustomTokenAuthOptions.DefaultScemeName,
                 opts => {
               opts.TokenHeaderName = "THarendra";
        }
             );

        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            string path = @"C:\\Task_logs";
            var date = new DateTime();
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            loggerFactory.AddFile("C:\\Task_logs\\Log.txt");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");

            });
        }
    }
}
