using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HMS.Entities.Models
{
    public partial class HealthContext : DbContext
    {
        public HealthContext()
        {
        }

        public HealthContext(DbContextOptions<HealthContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TAccessRight> TAccessRight { get; set; }
        public virtual DbSet<TBranch> TBranch { get; set; }
        public virtual DbSet<TBranchType> TBranchType { get; set; }
        public virtual DbSet<TCountry> TCountry { get; set; }
        public virtual DbSet<TDepartment> TDepartment { get; set; }
        public virtual DbSet<TDepartmentEmployee> TDepartmentEmployee { get; set; }
        public virtual DbSet<TDisease> TDisease { get; set; }
        public virtual DbSet<TDiseaseGroup> TDiseaseGroup { get; set; }
        public virtual DbSet<TDistrict> TDistrict { get; set; }
        public virtual DbSet<TDrug> TDrug { get; set; }
        public virtual DbSet<TDrugGroup> TDrugGroup { get; set; }
        public virtual DbSet<TDrugUse> TDrugUse { get; set; }
        public virtual DbSet<TEmployee> TEmployee { get; set; }
        public virtual DbSet<TEmployeeType> TEmployeeType { get; set; }
        public virtual DbSet<THospital> THospital { get; set; }
        public virtual DbSet<TMedicalRecord> TMedicalRecord { get; set; }
        public virtual DbSet<TMedicalRecordStatus> TMedicalRecordStatus { get; set; }
        public virtual DbSet<TPatient> TPatient { get; set; }
        public virtual DbSet<TPrescription> TPrescription { get; set; }
        public virtual DbSet<TPrescriptionDetail> TPrescriptionDetail { get; set; }
        public virtual DbSet<TProvince> TProvince { get; set; }
        public virtual DbSet<TRight> TRight { get; set; }
        public virtual DbSet<TRole> TRole { get; set; }
        public virtual DbSet<TTreatment> TTreatment { get; set; }
        public virtual DbSet<TTreatmentDetail> TTreatmentDetail { get; set; }
        public virtual DbSet<TTreatmentDisease> TTreatmentDisease { get; set; }
        public virtual DbSet<TUnit> TUnit { get; set; }
        public virtual DbSet<TUser> TUser { get; set; }
        public virtual DbSet<TUserEmployee> TUserEmployee { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TAccessRight>(entity =>
            {
                entity.HasKey(e => new { e.RoleId, e.RightId });

                entity.HasOne(d => d.Right)
                    .WithMany(p => p.TAccessRight)
                    .HasForeignKey(d => d.RightId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TAccessRight_TRight");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.TAccessRight)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TAccessRight_TAccessRight");
            });

            modelBuilder.Entity<TBranch>(entity =>
            {
                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(512);

                entity.Property(e => e.Fax)
                    .HasMaxLength(48)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Telephone)
                    .IsRequired()
                    .HasMaxLength(48)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.TBranch)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TBranch_TCountry");

                entity.HasOne(d => d.District)
                    .WithMany(p => p.TBranch)
                    .HasForeignKey(d => d.DistrictId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TBranch_TDistrict");

                entity.HasOne(d => d.Hospital)
                    .WithMany(p => p.TBranch)
                    .HasForeignKey(d => d.HospitalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TBranch_THospital");

                entity.HasOne(d => d.Province)
                    .WithMany(p => p.TBranch)
                    .HasForeignKey(d => d.ProvinceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TBranch_TProvince");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.TBranch)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TBranch_TBranchType");
            });

            modelBuilder.Entity<TBranchType>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<TCountry>(entity =>
            {
                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(48);
            });

            modelBuilder.Entity<TDepartment>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(512);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.TDepartment)
                    .HasForeignKey(d => d.BranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TDepartment_TBranch");
            });

            modelBuilder.Entity<TDepartmentEmployee>(entity =>
            {
                entity.HasKey(e => new { e.DepartmentId, e.EmployeeId });
            });

            modelBuilder.Entity<TDisease>(entity =>
            {
                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(2000);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Symptoms)
                    .IsRequired()
                    .HasMaxLength(2000);

                entity.Property(e => e.Treatment)
                    .IsRequired()
                    .HasMaxLength(2000);

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.TDisease)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TDisease_TDiseaseGroup");
            });

            modelBuilder.Entity<TDiseaseGroup>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(512);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<TDistrict>(entity =>
            {
                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(48);

                entity.HasOne(d => d.Province)
                    .WithMany(p => p.TDistrict)
                    .HasForeignKey(d => d.ProvinceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TDistrict_TProvince");
            });

            modelBuilder.Entity<TDrug>(entity =>
            {
                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(48);

                entity.Property(e => e.Description).HasMaxLength(512);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.Note).HasMaxLength(512);

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.TDrug)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TDrug_TDrugGroup");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.TDrug)
                    .HasForeignKey(d => d.UnitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TDrug_TUnit");

                entity.HasOne(d => d.Use)
                    .WithMany(p => p.TDrug)
                    .HasForeignKey(d => d.UseId)
                    .HasConstraintName("FK_TDrug_TDrugUse");
            });

            modelBuilder.Entity<TDrugGroup>(entity =>
            {
                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(24);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(48);
            });

            modelBuilder.Entity<TDrugUse>(entity =>
            {
                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(48);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128);
            });

            modelBuilder.Entity<TEmployee>(entity =>
            {
                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Birthday).HasColumnType("date");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.DateOfIssue).HasColumnType("date");

                entity.Property(e => e.Description).HasMaxLength(512);

                entity.Property(e => e.Email)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(48);

                entity.Property(e => e.IdentityCardNo)
                    .IsRequired()
                    .HasMaxLength(48)
                    .IsUnicode(false);

                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(48);

                entity.Property(e => e.NativeAddress)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Phone2)
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.PlaceOfIssue)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.TEmployee)
                    .HasForeignKey(d => d.BranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TEmployee_TBranch");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.TEmployeeCountry)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TEmployee_TCountry");

                entity.HasOne(d => d.District)
                    .WithMany(p => p.TEmployeeDistrict)
                    .HasForeignKey(d => d.DistrictId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TEmployee_TDistrict");

                entity.HasOne(d => d.NativeCountry)
                    .WithMany(p => p.TEmployeeNativeCountry)
                    .HasForeignKey(d => d.NativeCountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TEmployee_TCountry_Native");

                entity.HasOne(d => d.NativeDistrict)
                    .WithMany(p => p.TEmployeeNativeDistrict)
                    .HasForeignKey(d => d.NativeDistrictId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TEmployee_TDistrict_Native");

                entity.HasOne(d => d.NativeProvince)
                    .WithMany(p => p.TEmployeeNativeProvince)
                    .HasForeignKey(d => d.NativeProvinceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TEmployee_TProvince_Native");

                entity.HasOne(d => d.Province)
                    .WithMany(p => p.TEmployeeProvince)
                    .HasForeignKey(d => d.ProvinceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TEmployee_TProvince");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.TEmployee)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TEmployee_TEmployeeType");
            });

            modelBuilder.Entity<TEmployeeType>(entity =>
            {
                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(48);
            });

            modelBuilder.Entity<THospital>(entity =>
            {
                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(512);

                entity.Property(e => e.Fax)
                    .HasMaxLength(48)
                    .IsUnicode(false);

                entity.Property(e => e.Mail)
                    .HasMaxLength(48)
                    .IsUnicode(false);

                entity.Property(e => e.Mobile)
                    .HasMaxLength(48)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Telephone)
                    .HasMaxLength(48)
                    .IsUnicode(false);

                entity.Property(e => e.Website)
                    .HasMaxLength(48)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TMedicalRecord>(entity =>
            {
                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.Reason).HasMaxLength(50);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.TMedicalRecord)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TMedicalRecord_TPatient");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.TMedicalRecord)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TMedicalRecord_TMedicalRecordStatus");
            });

            modelBuilder.Entity<TMedicalRecordStatus>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(512);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TPatient>(entity =>
            {
                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Birthday).HasColumnType("date");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.DateOfIssue).HasColumnType("date");

                entity.Property(e => e.Description).HasMaxLength(512);

                entity.Property(e => e.DiseaseHistories).HasMaxLength(2048);

                entity.Property(e => e.Email)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(48);

                entity.Property(e => e.FirstRelativeDescription).HasMaxLength(256);

                entity.Property(e => e.FirstRelativeIdentifyCardNo)
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FirstRelativeName).HasMaxLength(48);

                entity.Property(e => e.FirstRelativePhone)
                    .HasMaxLength(24)
                    .IsUnicode(false);

                entity.Property(e => e.IdentifyCardNo)
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(48);

                entity.Property(e => e.MiddleName).HasMaxLength(48);

                entity.Property(e => e.NativeAddress)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(24)
                    .IsUnicode(false);

                entity.Property(e => e.PlaceOfIssue).HasMaxLength(256);

                entity.Property(e => e.SecondRelativeDescription).HasMaxLength(256);

                entity.Property(e => e.SecondRelativeIdentifyCardNo)
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.SecondRelativeName).HasMaxLength(48);

                entity.Property(e => e.SecondRelativePhone)
                    .HasMaxLength(24)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.TPatientCountry)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TPatient_TCountry");

                entity.HasOne(d => d.District)
                    .WithMany(p => p.TPatientDistrict)
                    .HasForeignKey(d => d.DistrictId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TPatient_TDistrict");

                entity.HasOne(d => d.NativeCountry)
                    .WithMany(p => p.TPatientNativeCountry)
                    .HasForeignKey(d => d.NativeCountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TPatient_TCountry_Native");

                entity.HasOne(d => d.NativeDistrict)
                    .WithMany(p => p.TPatientNativeDistrict)
                    .HasForeignKey(d => d.NativeDistrictId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TPatient_TDistrict_Native");

                entity.HasOne(d => d.NativeProvince)
                    .WithMany(p => p.TPatientNativeProvince)
                    .HasForeignKey(d => d.NativeProvinceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TPatient_TProvince_Native");

                entity.HasOne(d => d.Province)
                    .WithMany(p => p.TPatientProvince)
                    .HasForeignKey(d => d.ProvinceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TPatient_TProvince");
            });

            modelBuilder.Entity<TPrescription>(entity =>
            {
                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.Diagnose)
                    .IsRequired()
                    .HasMaxLength(512);

                entity.Property(e => e.Note).HasMaxLength(512);

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.TPrescription)
                    .HasForeignKey(d => d.DoctorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TPrescription_TEmployee");

                entity.HasOne(d => d.MedicalRecord)
                    .WithMany(p => p.TPrescription)
                    .HasForeignKey(d => d.MedicalRecordId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TPrescription_TMedicalRecord");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.TPrescription)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TPrescription_TPatient");

                entity.HasOne(d => d.Treatment)
                    .WithMany(p => p.TPrescription)
                    .HasForeignKey(d => d.TreatmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TPrescription_TTreatment");
            });

            modelBuilder.Entity<TPrescriptionDetail>(entity =>
            {
                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.Diagnose).HasMaxLength(512);

                entity.Property(e => e.Note).HasMaxLength(512);

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");

                entity.HasOne(d => d.Drug)
                    .WithMany(p => p.TPrescriptionDetail)
                    .HasForeignKey(d => d.DrugId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TPrescriptionDetail_TDrug");

                entity.HasOne(d => d.MedicalRecord)
                    .WithMany(p => p.TPrescriptionDetail)
                    .HasForeignKey(d => d.MedicalRecordId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TPrescriptionDetail_TMedicalRecord");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.TPrescriptionDetail)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TPrescriptionDetail_TPatient");

                entity.HasOne(d => d.TreatmentDetail)
                    .WithMany(p => p.TPrescriptionDetail)
                    .HasForeignKey(d => d.TreatmentDetailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TPrescriptionDetail_TTreatmentDetail");

                entity.HasOne(d => d.Treatment)
                    .WithMany(p => p.TPrescriptionDetail)
                    .HasForeignKey(d => d.TreatmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TPrescriptionDetail_TTreatment");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.TPrescriptionDetail)
                    .HasForeignKey(d => d.UnitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TPrescriptionDetail_TUnit");

                entity.HasOne(d => d.Use)
                    .WithMany(p => p.TPrescriptionDetail)
                    .HasForeignKey(d => d.UseId)
                    .HasConstraintName("FK_TPrescriptionDetail_TDrugUse");
            });

            modelBuilder.Entity<TProvince>(entity =>
            {
                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(48);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.TProvince)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TProvince_TCountry");
            });

            modelBuilder.Entity<TRight>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(512);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128);
            });

            modelBuilder.Entity<TRole>(entity =>
            {
                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128);
            });

            modelBuilder.Entity<TTreatment>(entity =>
            {
                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Content).HasMaxLength(1024);

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.Note).HasMaxLength(1024);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.Title).HasMaxLength(256);

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.TTreatmentDoctor)
                    .HasForeignKey(d => d.DoctorId)
                    .HasConstraintName("FK_TTreatment_TEmployee_Doctor");

                entity.HasOne(d => d.MedicalRecord)
                    .WithMany(p => p.TTreatment)
                    .HasForeignKey(d => d.MedicalRecordId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TTreatment_TMedicalRecord");

                entity.HasOne(d => d.Nurse)
                    .WithMany(p => p.TTreatmentNurse)
                    .HasForeignKey(d => d.NurseId)
                    .HasConstraintName("FK_TTreatment_TEmployee_Nurse");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.TTreatment)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TTreatment_TPatient");
            });

            modelBuilder.Entity<TTreatmentDetail>(entity =>
            {
                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Content).HasMaxLength(1024);

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.Note).HasMaxLength(1024);

                entity.Property(e => e.Result).HasMaxLength(1024);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.TTreatmentDetailDoctor)
                    .HasForeignKey(d => d.DoctorId)
                    .HasConstraintName("FK_TTreatmentDetail_Doctor");

                entity.HasOne(d => d.MedicalRecord)
                    .WithMany(p => p.TTreatmentDetail)
                    .HasForeignKey(d => d.MedicalRecordId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TTreatmentDetail_TMedicalRecord");

                entity.HasOne(d => d.Nurse)
                    .WithMany(p => p.TTreatmentDetailNurse)
                    .HasForeignKey(d => d.NurseId)
                    .HasConstraintName("FK_TTreatmentDetail_Nurse");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.TTreatmentDetail)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TTreatmentDetail_TPatient");

                entity.HasOne(d => d.Treatment)
                    .WithMany(p => p.TTreatmentDetail)
                    .HasForeignKey(d => d.TreatmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TTreatmentDetail_TTreatment");
            });

            modelBuilder.Entity<TTreatmentDisease>(entity =>
            {
                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.Note).HasMaxLength(1000);

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");

                entity.HasOne(d => d.Disease)
                    .WithMany(p => p.TTreatmentDisease)
                    .HasForeignKey(d => d.DiseaseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TTreatmentDisease_TDisease");

                entity.HasOne(d => d.MedicalRecord)
                    .WithMany(p => p.TTreatmentDisease)
                    .HasForeignKey(d => d.MedicalRecordId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TTreatmentDisease_TMedicalRecord");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.TTreatmentDisease)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TTreatmentDisease_TPatient");

                entity.HasOne(d => d.Treatment)
                    .WithMany(p => p.TTreatmentDisease)
                    .HasForeignKey(d => d.TreatmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TTreatmentDisease_TTreatment");
            });

            modelBuilder.Entity<TUnit>(entity =>
            {
                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(24);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(48);
            });

            modelBuilder.Entity<TUser>(entity =>
            {
                entity.Property(e => e.AccessToken)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName).HasMaxLength(48);

                entity.Property(e => e.LastName).HasMaxLength(48);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(48)
                    .IsUnicode(false);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.TUser)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TUser_TRole");
            });

            modelBuilder.Entity<TUserEmployee>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.EmployeeId });
            });
        }
    }
}
