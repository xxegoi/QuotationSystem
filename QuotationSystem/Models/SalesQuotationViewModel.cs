using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuotationSystem.Models
{
    public class SalesQuotationViewModel
    {
       public QuotationHeaderSalesViewModel Header { get; set; }
       public List<QuotationDetailSalesViewModel> Details { get; set; }
    }
}