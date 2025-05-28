using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ostatniezadanie_s27359.Models
{
    [Table("Patient")]
    public class Patient
    {
        [Key]
        public int IdPatient { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; } = string.Empty;
        
        [Required]
        public DateTime Birthdate { get; set; }
        
        public virtual ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
    }
} 