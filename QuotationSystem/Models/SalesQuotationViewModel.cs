using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuotationSystem.Models
{
    public class SalesQuotationViewModel
    {
        public QuotationHeaderSalesViewModel Header { get; set; }
        public List<QuotationDetailSalesViewModel> Details { get; set; }

        public QuotationDetailSalesViewModel Detail { get; set; }

        
        [Display(Name = "商品类别")]
        public List<SelectListItem> SelectClass { get; set; }

        [Display(Name = "状态")]
        public List<SelectListItem> StatusList { get; set; }

        [Display(Name = "采购员")]
        public List<SelectListItem> BuyerList { get; set; }

        [Display(Name ="暗佣类别")]
        public List<SelectListItem> CommisionList { get; set; }

        public SalesQuotationViewModel()
        {
            Header = new QuotationHeaderSalesViewModel();
            Details = new List<QuotationDetailSalesViewModel>();
        }
    }
}