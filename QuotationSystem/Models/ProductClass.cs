using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuotationSystem.Models
{
    [DisplayName("产品类别")]
    public class ProductClass
    {
        public int Id { get; set; }

        [Display(Name="类别名称")]
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        [Display(Name ="利润")]
        [Required]
        public decimal Profit { get; set; }


    }
}