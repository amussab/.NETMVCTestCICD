using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PatientManagementSystem.Models
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<PatientInsurancePolicy> PatientInsurancePolicies { get; set; }
        public DbSet<PatientAudit> PatientAudits { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Patient>()
                .HasKey(p => p.medicalNumber);

            modelBuilder.Entity<Patient>()
                .HasMany(p => p.InsurancePolicies)
                .WithOne(p => p.Patient)
                .HasForeignKey(p => p.PatientMedicalNumber);

            modelBuilder.Entity<Patient>()
                .Property(p => p.medicalNumber)
                .IsRequired()
                .HasMaxLength(10);

            modelBuilder.Entity<PatientInsurancePolicy>()
                .Property(p => p.PatientMedicalNumber)
                .IsRequired();
        }
    }
}