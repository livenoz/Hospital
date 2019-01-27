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
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using HMS.API.Extensions;

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

            // Enable CORS for call API from javascript
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllHeaders",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Inject DB connection
            services.AddDbContext<HealthContext>(options 
                => options.UseSqlServer(Configuration.GetConnectionString("HealthConnection"), 
                        sqlServerOptions => sqlServerOptions.UseRowNumberForPaging()), ServiceLifetime.Scoped);
            services.AddAutoMapper();
            services.AddResponseCompression();
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
            services.AddHttpContextAccessor();
            services.AddScoped<IPatientBusiness, PatientBusiness>();
            services.AddScoped<IUserBusiness, UserBusiness>();
            services.AddScoped<ITreatmentBusiness, TreatmentBusiness>();
            services.AddScoped<IAddressBusiness, AddressBusiness>();
            services.AddScoped<IDiseaseBusiness, DiseaseBusiness>();
            services.AddScoped<IMedicalRecordBusiness, MedicalRecordBusiness>();
            services.AddScoped<IEmployeeBusiness, EmployeeBusiness>();

            services.AddScoped<IBranchRepository, BranchRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<IProvinceRepository, ProvinceRepository>();
            services.AddScoped<IDistrictRepository, DistrictRepository>();
            services.AddScoped<IMedicalRecordRepository, MedicalRecordRepository>();
            services.AddScoped<IMedicalRecordStatusRepository, MedicalRecordStatusRepository>();
            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<IPrescriptionDetailRepository, PrescriptionDetailRepository>();
            services.AddScoped<IPrescriptionRepository, PrescriptionRepository>();
            services.AddScoped<ITreatmentRepository, TreatmentRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IDiseaseRepository, DiseaseRepository>();
            services.AddScoped<IDiseaseGroupRepository, DiseaseGroupRepository>();
            services.AddScoped<ITreatmentDiseaseRepository, TreatmentDiseaseRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddFile(Configuration.GetSection("Logging"));
            app.UseCors("AllowAllHeaders");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.ConfigureExceptionHandler(loggerFactory.CreateLogger("Configure"));
            app.UseResponseCompression();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
