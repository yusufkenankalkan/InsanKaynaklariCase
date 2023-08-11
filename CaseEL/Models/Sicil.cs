using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseEL.Models
{
    [Table("Siciller")]
    public class Sicil
    {
        [Key]
        [Column(Order = 1)]
        public int SicilNo { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Ad { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Soyad { get; set; }
        public DateTime BaslamaTarihi { get; set; }
        public DateTime? BitisTarihi { get; set; }
        public DateTime DogumTarihi { get; set; }
        public bool AktifMi { get; set; } = true;
    }
}
