using Microsoft.EntityFrameworkCore;
using VetClinikModels.Modules;

namespace VetClinikModels
{
    class VetClinicDataBase : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(@"Data Source=POWERWARRIOR-ПК\SQLEXPRESS01;Initial Catalog=VetClinic;Integrated Security=True;MultipleActiveResultSets=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }
        public virtual DbSet<Medication> Medications { set; get; }
        public virtual DbSet<Medicine> Medicines { set; get; }
        public virtual DbSet<MedicationMedicine> MedicationMedicines { set; get; }
        public virtual DbSet<Service> Services { set; get; }
        public virtual DbSet<MedicationService> MedicationServices { set; get; }
        public virtual DbSet<DoctorVisit> DoctorVisits { set; get; }
        public virtual DbSet<DoctorVisitService> DoctorVisitServices { set; get; }
    }
}
