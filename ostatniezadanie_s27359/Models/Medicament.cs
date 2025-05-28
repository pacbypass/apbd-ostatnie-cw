using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ostatniezadanie_s27359.Models
{
    [Table("Medicament")]
    public class Medicament
    {
        [Key]
        public int IdMedicament { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(100)]
        public string Description { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(100)]
        public string Type { get; set; } = string.Empty;
        
        public virtual ICollection<PrescriptionMedicament> PrescriptionMedicaments { get; set; } = new List<PrescriptionMedicament>();
    }
} 