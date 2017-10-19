using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuotationSystem.Models
{
    public class QuotationDetail
    {
        [Key]
        public int Id { get; set; }
        
        public QuotationHeader Header { get; set; }

        public string Metal { get; set; }

        public string Shape { get; set; }

        public string Model { get; set; }

        public string Technology { get; set; }

        public decimal Qty { get; set; }

        public decimal CommissionCost { get; set; }

        public decimal PurchasePrice { get; set; }

        public decimal SalesPrice { get; set; }

        public string Memo { get; set; }
    }

    public class QuotationDetailSalesViewModel:QuotationDetailBuyerViewModel
    {
        [Required]
        [Display(Name = "销售报价")]
        public decimal SalesPrice { get; set; }

        [Required]
        [Display(Name = "暗佣")]
        public decimal CommissionCost { get; set; }
    }

    public class QuotationDetailBuyerViewModel
    {
        public int Id { get; set; }
        [Required]
        public QuotationHeader Header { get; set; }
        [Display(Name = "材质")]
        [Required]
        [MaxLength(30)]
        public string Metal { get; set; }

        [Required]
        [Display(Name = "形态")]
        [MaxLength(20)]
        public string Shape { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "规格型号")]
        public string Model { get; set; }

        [Required]
        [MaxLength(30)]
        [Display(Name = "工序")]
        public string Technology { get; set; }

        [Required]
        [Display(Name = "数量")]
        public decimal Qty { get; set; }

        [Required]
        [Display(Name = "采购报价")]
        public decimal PurchasePrice { get; set; }

        [Display(Name = "备注")]
        public string Momo { get; set; }
    }
}