using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuotationSystem.Models
{
    public class UserLogHis
    {
        public int Id { get; set; }
        public string Account { get; set; }
        public string Cookie { get; set; }
        public DateTime LoginTime { get; set; }
    }
}