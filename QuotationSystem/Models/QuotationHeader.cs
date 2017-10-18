using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace QuotationSystem.Models
{
    public class QuotationHeader
    {
        public int Id { get; set; }

        [Display(Name ="状态")]
        public int Status { get; set; }

        [Display(Name ="报价日期")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime QDate { get; set; }

        public Employee Emp { get; set; }
        [Display(Name ="FOB费")]
        [Required]
        public decimal Fob { get; set; }

        [Required]
        [Display(Name ="海运费")]
        public decimal SeaCost { get; set; }

        [Required]
        [Display(Name ="暗佣类别")]
        public int CommissionType { get; set; }

        [Required]
        [Display(Name ="暗佣")]
        public decimal Commission { get; set; }

        [Required]
        [Display(Name ="其它费用")]
        public decimal Other { get; set; }
    }
}