﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseEL.ViewModels
{
    public class BaseNumericVM
    {
        public int SicilNo { get; set; }
        public DateTime BaslamaTarihi { get; set; }
        public DateTime? BitisTarihi { get; set; }
        public DateTime DogumTarihi { get; set; }
        public bool AktifMi { get; set; }
    }
}