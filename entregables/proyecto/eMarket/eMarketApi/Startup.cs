using AutoMapper;
using eMarketApi.Repositories;
using eMarketApi.Repositories.Impl;
using eMarketDB.Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace eMarketApi
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
            // Adding CORS support
            services.AddCors(opt => opt.AddDefaultPolicy(builder => {
                builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
            }));

            //Adding swagger support
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });

            services.AddSingleton<ICategoryRepository, CategoryRepository>();
            services.AddSingleton<IProductRepository, ProductRepository>();
            services.AddSingleton<IReviewRepository, ReviewRepository>();
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddControllers();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddDbContext<eMarketDBContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("eMarketDatabase")), ServiceLifetime.Transient);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "api";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseCors();
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
