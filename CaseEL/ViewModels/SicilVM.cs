﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseEL.ViewModels
{
    public class SicilVM : BaseNumericVM
    {
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Ad { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Soyad { get; set; }
    }
}