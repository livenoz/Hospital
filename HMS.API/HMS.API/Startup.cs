using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using HMS.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using HMS.Business.Interfaces;
using HMS.Business;
using HMS.Repository.Interfaces;
using HMS.Repository;
using AutoMapper;

namespace HMS.API
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Inject DB connection
            services.AddDbContext<HealthContext>(options 
                => options.UseSqlServer(Configuration.GetConnectionString("HealthConnection"), 
                        sqlServerOptions => sqlServerOptions.UseRowNumberForPaging()), ServiceLifetime.Scoped);
            services.AddAutoMapper();
            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<IPatientBusiness, PatientBusiness>();
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
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
