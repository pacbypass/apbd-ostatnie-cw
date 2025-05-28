using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ostatniezadanie_s27359.Models
{
    [Table("Prescription_Medicament")]
    public class PrescriptionMedicament
    {
        [Key, Column(Order = 0)]
        [ForeignKey("Medicament")]
        public int IdMedicament { get; set; }
        
        [Key, Column(Order = 1)]
        [ForeignKey("Prescription")]
        public int IdPrescription { get; set; }
        
        public int? Dose { get; set; }
        
        [MaxLength(100)]
        public string? Details { get; set; }
        
        public virtual Medicament Medicament { get; set; } = null!;
        public virtual Prescription Prescription { get; set; } = null!;
    }
} 