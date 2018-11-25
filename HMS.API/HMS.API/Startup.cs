﻿using Microsoft.AspNetCore.Builder;
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
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;

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
            services.AddDataProtection()
                .UseCustomCryptographicAlgorithms(
                new CngGcmAuthenticatedEncryptorConfiguration()
                {
                    // Passed to BCryptOpenAlgorithmProvider
                    EncryptionAlgorithm = "AES",
                    EncryptionAlgorithmProvider = null,


                    // Specified in bits
                    EncryptionAlgorithmKeySize = 256
                });
            services.AddScoped<IPatientBusiness, PatientBusiness>();
            services.AddScoped<IUserBusiness, UserBusiness>();

            services.AddScoped<IBranchRepository, BranchRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<IProvinceRepository, ProvinceRepository>();
            services.AddScoped<IDistrictRepository, DistrictRepository>();
            services.AddScoped<IMedicalRecordRepository, MedicalRecordRepository>();
            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<IPrescriptionDetailRepository, PrescriptionDetailRepository>();
            services.AddScoped<IPrescriptionRepository, PrescriptionRepository>();
            services.AddScoped<ITreatmentRepository, TreatmentRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
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
