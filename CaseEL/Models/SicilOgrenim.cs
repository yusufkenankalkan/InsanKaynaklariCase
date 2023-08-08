using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseEL.Models
{
    public class SicilOgrenim
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Sicil")]
        public int SicilNo { get; set; }
        public string OgrenimDurumu { get; set; }
        public string OkulAdi { get; set; }
        public DateTime OgrenimBaslangicTarihi { get; set; } 

        public DateTime? OgrenimBitisTarihi { get; set; } 
        public virtual Sicil Sicil { get; set; }
    }
}
