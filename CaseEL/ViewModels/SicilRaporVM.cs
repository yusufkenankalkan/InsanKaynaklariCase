using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseEL.ViewModels
{
    public class SicilRaporVM
    {
        public int SicilNo { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public DateTime BaslamaTarihi { get; set; }
        public DateTime? BitisTarihi { get; set; }
        public string? OgrenimDurumu { get; set; }
        public bool AktifMi { get; set; } = true;

    }
}
