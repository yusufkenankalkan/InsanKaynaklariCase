﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseEL.ViewModels
{
    public class AdayCvVM
    {
        public int CvNo { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Ad { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Soyad { get; set; }

        public DateTime DogumTarihi { get; set; }

        public DateTime CvOlusturmaTarihi { get; set; }

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
