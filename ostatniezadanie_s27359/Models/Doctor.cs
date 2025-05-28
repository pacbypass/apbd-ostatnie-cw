using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ostatniezadanie_s27359.Models
{
    [Table("Doctor")]
    public class Doctor
    {
        [Key]
        public int IdDoctor { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;
        
        public virtual ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
    }
} 