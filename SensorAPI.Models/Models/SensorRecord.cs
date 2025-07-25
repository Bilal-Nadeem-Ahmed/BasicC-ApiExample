using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SensorAPI.Models.Models
{
    public class SensorRecord
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public DateTime date { get; set; } = DateTime.UtcNow;

        [Required]
        [MaxLength(100)]
        public string sensor { get; set; } = string.Empty;

        [Required]
        public float value { get; set; }

    }
}
