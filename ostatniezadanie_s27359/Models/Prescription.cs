using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ostatniezadanie_s27359.Models
{
    [Table("Prescription")]
    public class Prescription
    {
        [Key]
        public int IdPrescription { get; set; }
        
        [Required]
        public DateTime Date { get; set; }
        
        [Required]
        public DateTime DueDate { get; set; }
        
        [Required]
        [ForeignKey("Patient")]
        public int IdPatient { get; set; }
        
        [Required]
        [ForeignKey("Doctor")]
        public int IdDoctor { get; set; }
        
        public virtual Patient Patient { get; set; } = null!;
        public virtual Doctor Doctor { get; set; } = null!;
        public virtual ICollection<PrescriptionMedicament> PrescriptionMedicaments { get; set; } = new List<PrescriptionMedicament>();
    }
} 