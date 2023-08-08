﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseEL.Models
{
    [Table("SicilUcretler")]
    public class SicilUcret : BaseNumeric
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Sicil")] 
        public int SicilNo { get; set; }

        public string UcretTipi { get; set; } 

        public decimal UcretTutari { get; set; }

        public DateTime GecerlilikBaslangicTarihi { get; set; }

        public virtual Sicil Sicil { get; set; }
    }
}
