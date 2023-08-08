,using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseEL.ViewModels
{
    public class SicilOgrenimVM : BaseNumericVM
    {
        public int SicilNo { get; set; }

        public string OgrenimDurumu { get; set; }

        public string OkulAdi { get; set; }

        public DateTime OgrenimBaslangicTarihi { get; set; }

        public DateTime? OgrenimBitisTarihi { get; set; }

        public SicilVM Sicil { get; set; }
    }

}
