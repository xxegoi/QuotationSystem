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

        public ActionResult SalesIndex(int page = 1, int size = 30)
        {
            var headers = db.QHeaders.Skip(page - 1 * size).Take(size).Where(p => p.Sales.Account == Session["User"].ToString()).ToList();

            List<QuotationHeaderSalesViewModel> models = new List<QuotationHeaderSalesViewModel>(); ;

            if (headers.Count() > 0)
            {

                headers.ForEach(p =>
                {
                    QuotationHeaderSalesViewModel item = new QuotationHeaderSalesViewModel
                    {
                        Buyer = p.Buyer,
                        Commision = p.Commision,
                        Id = p.Id,
                        CommisionType = p.CommissionType,
                        ExchangeRate = p.ExchangeRate,
                        Fob = p.Fob,
                        Other = p.Other,
                        PurchaseMemo = p.PurchaseMemo,
                        QDate = p.QDate,
                        Sales = p.Sales,
                        SalesMemo = p.SalesMemo,
                        SeaCost = p.SeaCost,
                        Status = p.Status
                    };

                    models.Add(item);
                });
            }

            return View(models);
        }

        public ActionResult BuyIndex(int page=1)
        {
            return View();
        }
    }
}