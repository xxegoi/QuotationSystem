using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace QuotationSystem.Models
{
    public class QuotationHeader
    {
        public int Id { get; set; }

        [Display(Name ="状态")]
        public int Status { get; set; }

        [Required]
        public Employee Sales { get; set; }

        [Required] 
        public Employee Buyer { get; set; }

        [Display(Name ="报价日期")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime QDate { get; set; }

        [Required]
        [Display(Name ="汇率")]
        public decimal ExchangeRate { get; set; }

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
        [Display(Name ="其它费用")]
        public decimal Other { get; set; }

        [Display(Name ="业务备注")]
        [MaxLength(200)]
        public string SalesMemo { get; set; }

        [Display(Name ="采购备注")]
        [MaxLength(200)]
        public string PurchaseMemo { get; set; }
    }

    /// <summary>
    /// 业务员视图模型
    /// </summary>
    public class QuotationHeaderSalesViewModel: QuotationHeaderBaseViewModel
    {
        [Display(Name = "FOB费")]
        [Required]
        public decimal Fob { get; set; }

        [Required]
        [Display(Name = "汇率")]
        public decimal ExchangeRate { get; set; }

        [Required]
        [Display(Name = "海运费")]
        public decimal SeaCost { get; set; }

        [Required]
        [Display(Name = "暗佣类别")]
        public int CommissionType { get; set; }

        [Required]
        [Display(Name = "其它费用")]
        public decimal Other { get; set; }

    }

    /// <summary>
    /// 采购员界面视图模型
    /// </summary>
    public class QuotaitonHeaderBuyViewModel:QuotationHeaderBaseViewModel
    {

    }

    
    public class QuotationHeaderBaseViewModel
    {
        public int Id { get; set; }

        [Display(Name = "状态")]
        public int Status { get; set; }

        [Required]
        [Display(Name ="业务员")]
        public Employee Sales { get; set; }

        [Required]
        [Display(Name ="采购员")]
        public Employee Buyer { get; set; }

        [Display(Name = "报价日期")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime QDate { get; set; }

        [Display(Name = "业务备注")]
        [MaxLength(200)]
        public string SalesMemo { get; set; }

        [Display(Name = "采购备注")]
        [MaxLength(200)]
        public string PurchaseMemo { get; set; }

    }
}