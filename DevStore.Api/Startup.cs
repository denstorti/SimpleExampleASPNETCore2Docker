using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Infrastructure.DataContexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace DevStore.Api
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
            Console.WriteLine("ASPNETCORE_ENVIRONMENT = " + Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") != EnvironmentName.Development)
            {
                string connString = Configuration["ConnectionString"];

                services.AddDbContext<DevStoreDataContext>(optionsAction =>
                    optionsAction.UseSqlServer(connString,

                        sqlServerOptionsAction: sqlOptions =>
                        {
                            sqlOptions.MigrationsAssembly(typeof(DevStoreDataContext).GetTypeInfo().Assembly.GetName().Name);

                        //Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 
                        sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                        }
                    )
                );

                Console.WriteLine("DATABASE: Using SQL Server");
            }
            else
            {
                Console.WriteLine("DATABASE: Using In Memory database");
                
                services.AddDbContext<DevStoreDataContext>(optionsAction =>
                    optionsAction.UseInMemoryDatabase("InMemory")
                );
            }

            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Simple ASP.NET Core 2 + SQL Server on Docker", Version = "v1" });
            });


            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    var context = serviceScope.ServiceProvider.GetService<DevStoreDataContext>();

                    context.Database.Migrate();
                }

            }

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseCors(builder =>
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
            );
        }
    }
}
