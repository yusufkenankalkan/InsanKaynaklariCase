﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseEL.Models
{
    public class BaseNumeric
    {
        [Key]
        [Column(Order = 1)]
        public int SicilNo { get; set; }
        public DateTime BaslamaTarihi { get; set; } 
        public DateTime? BitisTarihi { get; set; }
        public DateTime DogumTarihi { get; set; }
        public bool AktifMi { get; set; }
        public BaseNumeric()
        {
            SicilNo = GenerateRandomId();
            BaslamaTarihi = DateTime.Now;
        }

        private int GenerateRandomId()
        {
            Random random = new Random();
            return random.Next(10000, 99999);
        }
    }
}
