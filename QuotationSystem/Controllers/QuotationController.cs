using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuotationSystem.Models;
using QuotationSystem.Common;


namespace QuotationSystem.Controllers
{
    [UserFilter]
    public class QuotationController : Controller
    {
        QSDbContext db = new QSDbContext();
        // GET: Quotation
        
        public ActionResult Index()
        {
            //根据用户所在部门显示数据
            var userType = Session["UserType"].ToString();

            switch (userType)
            {
                case "业务部":
                    {
                        return RedirectToAction("SaleIndex");
                    }
                case "采购部":
                    {
                        return RedirectToAction("BuyIndex");
                    }
                default:
                    {
                        return Redirect("/Employee/login");
                    }
            }
            
        }

        public ActionResult SalesIndex(int page=1)
        {


            return View();
        }

        public ActionResult BuyIndex()
        {
            return View();
        }
    }
}