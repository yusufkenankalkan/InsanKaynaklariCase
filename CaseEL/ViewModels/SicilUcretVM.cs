using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseEL.ViewModels
{
    public class SicilUcretVM : BaseNumericVM
    {
        public int SicilNo { get; set; }

        public string UcretTipi { get; set; }

        public decimal UcretTutari { get; set; }

        public DateTime GecerlilikBaslangicTarihi { get; set; }

        public SicilVM Sicil { get; set; }
    }
}
