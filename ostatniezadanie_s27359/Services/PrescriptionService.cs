using Microsoft.EntityFrameworkCore;
using ostatniezadanie_s27359.Data;
using ostatniezadanie_s27359.DTOs;
using ostatniezadanie_s27359.Models;

namespace ostatniezadanie_s27359.Services
{
    public class PrescriptionService : IPrescriptionService
    {
        private readonly ApplicationDbContext _context;

        public PrescriptionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreatePrescriptionAsync(CreatePrescriptionRequest request)
        {
            // check if DueDate is greater than or equal to Date
            if (request.Prescription.DueDate < request.Prescription.Date)
            {
                throw new ArgumentException("DueDate must be greater than or equal to Date");
            }

            // max 10 medicaments
            if (request.Medicaments.Count > 10)
            {
                throw new ArgumentException("A prescription can include a maximum of 10 medications");
            }

            // check if all medicaments exist
            var medicamentIds = request.Medicaments.Select(m => m.IdMedicament).ToList();
            var existingMedicaments = await _context.Medicaments
                .Where(m => medicamentIds.Contains(m.IdMedicament))
                .Select(m => m.IdMedicament)
                .ToListAsync();

            var nonExistentMedicaments = medicamentIds.Except(existingMedicaments).ToList();
            if (nonExistentMedicaments.Any())
            {
                throw new ArgumentException($"The following medicaments do not exist: {string.Join(", ", nonExistentMedicaments)}");
            }

            // check if doctor exists
            var doctorExists = await _context.Doctors.AnyAsync(d => d.IdDoctor == request.Prescription.IdDoctor);
            if (!doctorExists)
            {
                throw new ArgumentException($"Doctor with ID {request.Prescription.IdDoctor} does not exist");
            }

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // find or create patient
                var patient = await _context.Patients
                    .FirstOrDefaultAsync(p => p.FirstName == request.Patient.FirstName &&
                                            p.LastName == request.Patient.LastName &&
                                            p.Birthdate.Date == request.Patient.Birthdate.Date);

                if (patient == null)
                {
                    patient = new Patient
                    {
                        FirstName = request.Patient.FirstName,
                        LastName = request.Patient.LastName,
                        Birthdate = request.Patient.Birthdate
                    };
                    _context.Patients.Add(patient);
                    await _context.SaveChangesAsync();
                }

                // create prescription
                var prescription = new Prescription
                {
                    Date = request.Prescription.Date,
                    DueDate = request.Prescription.DueDate,
                    IdPatient = patient.IdPatient,
                    IdDoctor = request.Prescription.IdDoctor
                };

                _context.Prescriptions.Add(prescription);
                await _context.SaveChangesAsync();

                // add prescription medicaments
                foreach (var medicamentDto in request.Medicaments)
                {
                    var prescriptionMedicament = new PrescriptionMedicament
                    {
                        IdPrescription = prescription.IdPrescription,
                        IdMedicament = medicamentDto.IdMedicament,
                        Dose = medicamentDto.Dose,
                        Details = medicamentDto.Details
                    };
                    _context.PrescriptionMedicaments.Add(prescriptionMedicament);
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return prescription.IdPrescription;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<PatientDetailsResponse?> GetPatientDetailsAsync(int patientId)
        {
            var patient = await _context.Patients
                .Include(p => p.Prescriptions
                    .OrderBy(pr => pr.DueDate))
                    .ThenInclude(pr => pr.Doctor)
                .Include(p => p.Prescriptions)
                    .ThenInclude(pr => pr.PrescriptionMedicaments)
                    .ThenInclude(pm => pm.Medicament)
                .FirstOrDefaultAsync(p => p.IdPatient == patientId);

            if (patient == null)
                return null;

            var response = new PatientDetailsResponse
            {
                Patient = new PatientInfo
                {
                    IdPatient = patient.IdPatient,
                    FirstName = patient.FirstName,
                    LastName = patient.LastName,
                    Birthdate = patient.Birthdate
                },
                Prescriptions = patient.Prescriptions.Select(p => new PrescriptionInfo
                {
                    IdPrescription = p.IdPrescription,
                    Date = p.Date,
                    DueDate = p.DueDate,
                    Doctor = new DoctorInfo
                    {
                        IdDoctor = p.Doctor.IdDoctor,
                        FirstName = p.Doctor.FirstName,
                        LastName = p.Doctor.LastName,
                        Email = p.Doctor.Email
                    },
                    Medicaments = p.PrescriptionMedicaments.Select(pm => new MedicamentInfo
                    {
                        IdMedicament = pm.Medicament.IdMedicament,
                        Name = pm.Medicament.Name,
                        Description = pm.Medicament.Description,
                        Type = pm.Medicament.Type,
                        Dose = pm.Dose,
                        Details = pm.Details
                    }).ToList()
                }).ToList()
            };

            return response;
        }
    }
} 