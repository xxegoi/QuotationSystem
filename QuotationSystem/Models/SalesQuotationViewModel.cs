using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuotationSystem.Models
{
    public class SalesQuotationViewModel: QuotationHeaderSalesViewModel
    {
        public List<QuotationDetailSalesViewModel> Details { get; set; }

        public SalesQuotationViewModel()
        {
            Details = new List<QuotationDetailSalesViewModel>();
        }
    }
}