using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuotationSystem.Models;

namespace QuotationSystem.Controllers
{
    public class QuotationController : Controller
    {
        QSDbContext db = new QSDbContext();
        // GET: Quotation
        public ActionResult Index()
        {
            


            return View();
        }

        
    }
}