namespace ostatniezadanie_s27359.DTOs
{
    public class PatientDetailsResponse
    {
        public PatientInfo Patient { get; set; } = null!;
        public List<PrescriptionInfo> Prescriptions { get; set; } = new List<PrescriptionInfo>();
    }

    public class PatientInfo
    {
        public int IdPatient { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime Birthdate { get; set; }
    }

    public class PrescriptionInfo
    {
        public int IdPrescription { get; set; }
        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }
        public DoctorInfo Doctor { get; set; } = null!;
        public List<MedicamentInfo> Medicaments { get; set; } = new List<MedicamentInfo>();
    }

    public class DoctorInfo
    {
        public int IdDoctor { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }

    public class MedicamentInfo
    {
        public int IdMedicament { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public int? Dose { get; set; }
        public string? Details { get; set; }
    }
} 