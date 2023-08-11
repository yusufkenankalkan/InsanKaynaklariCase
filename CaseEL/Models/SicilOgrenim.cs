using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseEL.Models
{
    [Table("SicilOgrenimler")]
    public class SicilOgrenim : BaseNumeric
    {
        
        public int SicilNo { get; set; }
        public string OgrenimDurumu { get; set; }
        public string OkulAdi { get; set; }
        public DateTime OgrenimBaslangicTarihi { get; set; } 

        public DateTime? OgrenimBitisTarihi { get; set; }

        [ForeignKey("SicilNo")]
        public virtual Sicil Sicil { get; set; }
    }
}
