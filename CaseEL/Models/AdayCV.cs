using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseEL.Models
{
    [Table("AdayCvler")]
    public class AdayCv
    {
        [Key]
        [Column(Order = 1)]
        public int CvNo { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Ad { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Soyad { get; set; }

        public DateTime DogumTarihi { get; set; }

        public DateTime CvOlusturmaTarihi { get; set; } = DateTime.Now;

        public string OgrenimDurumu { get; set; }

        public string OkulAdi { get; set; }

        public string Bolum { get; set; }

        public DateTime OgrenimBaslangicTarihi { get; set; }

        public DateTime? OgrenimBitisTarihi { get; set; }

        public string? IsYeriAdi { get; set; }

        public string? IsDetayi { get; set; }

        public bool IseAlindiMi { get; set; }

    }

}
