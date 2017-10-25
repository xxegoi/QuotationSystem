using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuotationSystem.Models
{
    public class BuyerQuotationViewModel
    {
        public QuotationHeaderBuyViewModel Header { get; set; }
        public List<QuotationDetailBuyViewModel> Details { get; set; }
    }
}