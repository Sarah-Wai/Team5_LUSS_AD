using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Team5_LUSS.Models.ViewModels
{
    public class DHeadMonth
    {
        public string Month { get; set; }
        public int? YearOne { get; set; }
        public int? YearTwo { get; set; }


    }
}
