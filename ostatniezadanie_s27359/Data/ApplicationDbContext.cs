using Microsoft.EntityFrameworkCore;
using ostatniezadanie_s27359.Models;

namespace ostatniezadanie_s27359.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Medicament> Medicaments { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // klucz zlozony dla PrescriptionMedicament
            modelBuilder.Entity<PrescriptionMedicament>()
                .HasKey(pm => new { pm.IdMedicament, pm.IdPrescription });

            // konfiguracja relacji
            modelBuilder.Entity<PrescriptionMedicament>()
                .HasOne(pm => pm.Medicament)
                .WithMany(m => m.PrescriptionMedicaments)
                .HasForeignKey(pm => pm.IdMedicament)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PrescriptionMedicament>()
                .HasOne(pm => pm.Prescription)
                .WithMany(p => p.PrescriptionMedicaments)
                .HasForeignKey(pm => pm.IdPrescription)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Prescription>()
                .HasOne(p => p.Patient)
                .WithMany(pt => pt.Prescriptions)
                .HasForeignKey(p => p.IdPatient)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Prescription>()
                .HasOne(p => p.Doctor)
                .WithMany(d => d.Prescriptions)
                .HasForeignKey(p => p.IdDoctor)
                .OnDelete(DeleteBehavior.Cascade);

            // seedujemy poczatkowe dane
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // lekrarze
            modelBuilder.Entity<Doctor>().HasData(
                new Doctor { IdDoctor = 1, FirstName = "Jan", LastName = "Kowalski", Email = "jan.kowalski@bbbb.pl" },
                new Doctor { IdDoctor = 2, FirstName = "Anna", LastName = "Nowak", Email = "anna.nowak@aaaa.pl" },
                new Doctor { IdDoctor = 3, FirstName = "Piotr", LastName = "Wiśniewski", Email = "piotr.wisniewski@cccc.pl" }
            );

            // sedujemy pacjentow
            modelBuilder.Entity<Patient>().HasData(
                new Patient { IdPatient = 1, FirstName = "Andrzej", LastName = "Duda", Birthdate = new DateTime(1972, 5, 16) },
                new Patient { IdPatient = 2, FirstName = "Agnieszka", LastName = "Kowalczyk", Birthdate = new DateTime(1985, 3, 22) },
                new Patient { IdPatient = 3, FirstName = "Marek", LastName = "Lewandowski", Birthdate = new DateTime(1990, 7, 10) }
            );

            // seedujemy leki 
            modelBuilder.Entity<Medicament>().HasData(
                new Medicament { IdMedicament = 1, Name = "Aspiryna", Description = "Lek przeciwbólowy i przeciwzapalny", Type = "Analgetyk" },
                new Medicament { IdMedicament = 2, Name = "Amoksycylina", Description = "Antybiotyk z grupy penicylin", Type = "Antybiotyk" },
                new Medicament { IdMedicament = 3, Name = "Ibuprofen", Description = "Niesteroidowy lek przeciwzapalny", Type = "NLPZ" },
                new Medicament { IdMedicament = 4, Name = "Paracetamol", Description = "Lek przeciwbólowy i przeciwgorączkowy", Type = "Analgetyk" },
                new Medicament { IdMedicament = 5, Name = "Ketonal", Description = "Lek przeciwbólowy i przeciwzapalny", Type = "NLPZ" }
            );

            // seedujemy recepty
            modelBuilder.Entity<Prescription>().HasData(
                new Prescription { IdPrescription = 1, Date = new DateTime(2024, 1, 15), DueDate = new DateTime(2024, 2, 15), IdPatient = 1, IdDoctor = 1 },
                new Prescription { IdPrescription = 2, Date = new DateTime(2024, 1, 20), DueDate = new DateTime(2024, 2, 20), IdPatient = 2, IdDoctor = 2 },
                new Prescription { IdPrescription = 3, Date = new DateTime(2024, 1, 25), DueDate = new DateTime(2024, 2, 25), IdPatient = 3, IdDoctor = 3 }
            );

            // seedujemy leki na recepte - leki na receptach
            modelBuilder.Entity<PrescriptionMedicament>().HasData(
                new PrescriptionMedicament { IdPrescription = 1, IdMedicament = 1, Dose = 2, Details = "2 razy dziennie po posiłku" },
                new PrescriptionMedicament { IdPrescription = 1, IdMedicament = 4, Dose = 1, Details = "1 raz dziennie w razie bólu" },
                
                new PrescriptionMedicament { IdPrescription = 2, IdMedicament = 2, Dose = 3, Details = "3 razy dziennie przed posiłkami przez 7 dni" },
                
                new PrescriptionMedicament { IdPrescription = 3, IdMedicament = 3, Dose = 2, Details = "2 razy dziennie w razie bólu" },
                new PrescriptionMedicament { IdPrescription = 3, IdMedicament = 5, Dose = 1, Details = "1 raz dziennie w razie potrzeby" }
            );
        }
    }
} 