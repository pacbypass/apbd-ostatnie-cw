using System.ComponentModel.DataAnnotations;

namespace ostatniezadanie_s27359.DTOs
{
    public class CreatePrescriptionRequest
    {
        [Required]
        public PatientDto Patient { get; set; } = null!;
        
        [Required]
        public PrescriptionDto Prescription { get; set; } = null!;
        
        [Required]
        [MinLength(1)]
        [MaxLength(10, ErrorMessage = "A prescription can include a maximum of 10 medications")]
        public List<MedicamentPrescriptionDto> Medicaments { get; set; } = new List<MedicamentPrescriptionDto>();
    }

    public class PatientDto
    {
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; } = string.Empty;
        
        [Required]
        public DateTime Birthdate { get; set; }
    }

    public class PrescriptionDto
    {
        [Required]
        public DateTime Date { get; set; }
        
        [Required]
        public DateTime DueDate { get; set; }
        
        [Required]
        public int IdDoctor { get; set; }
    }

    public class MedicamentPrescriptionDto
    {
        [Required]
        public int IdMedicament { get; set; }
        
        public int? Dose { get; set; }
        
        [MaxLength(100)]
        public string? Details { get; set; }
    }
} 