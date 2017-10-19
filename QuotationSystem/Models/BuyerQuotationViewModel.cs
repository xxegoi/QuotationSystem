using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuotationSystem.Models
{
    public class BuyerQuotationViewModel
    {
        public QuotationHeaderBuyerViewModel Header { get; set; }
        public List<QuotationDetailBuyerViewModel> Details { get; set; }
    }
}