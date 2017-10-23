using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuotationSystem.Models
{
    public enum CommisionType
    {
        百份比,
        金额
    }

    public enum QuotationStatus
    {
        草稿,
        采购报价中,
        报价完成
    }
}