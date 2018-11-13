using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HMS.Entities.Models
{
    public partial class HMSContext : DbContext
    {
        public HMSContext()
        {
        }

        public HMSContext(DbContextOptions<HMSContext> options)
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
        public virtual DbSet<TEmployee> TEmployee { get; set; }
        public virtual DbSet<TEmployeeType> TEmployeeType { get; set; }
        public virtual DbSet<THospital> THospital { get; set; }
        public virtual DbSet<TMedicalRecord> TMedicalRecord { get; set; }
        public virtual DbSet<TMedicalRecordStatus> TMedicalRecordStatus { get; set; }
        public virtual DbSet<TPatient> TPatient { get; set; }
        public virtual DbSet<TProvince> TProvince { get; set; }
        public virtual DbSet<TRight> TRight { get; set; }
        public virtual DbSet<TRole> TRole { get; set; }
        public virtual DbSet<TTreatment> TTreatment { get; set; }
        public virtual DbSet<TTreatmentDisease> TTreatmentDisease { get; set; }
        public virtual DbSet<TUser> TUser { get; set; }
        public virtual DbSet<TUserEmployee> TUserEmployee { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //Connection string
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

            modelBuilder.Entity<TEmployee>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

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
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.DateOfIssue).HasColumnType("date");

                entity.Property(e => e.Description).HasMaxLength(512);

                entity.Property(e => e.Email)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.FatherIdentifyCardNo)
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FatherName).HasMaxLength(48);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(48);

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

                entity.Property(e => e.MotherIdentifyCardNo)
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.MotherName).HasMaxLength(48);

                entity.Property(e => e.NativeAddress)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(24)
                    .IsUnicode(false);

                entity.Property(e => e.PlaceOfIssue).HasMaxLength(256);

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

            modelBuilder.Entity<TProvince>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

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
                entity.Property(e => e.Content).HasMaxLength(1000);

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.Note).HasMaxLength(1000);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

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

                entity.HasOne(d => d.Treatment)
                    .WithMany(p => p.TTreatmentDisease)
                    .HasForeignKey(d => d.TreatmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TTreatmentDisease_TTreatment");
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
