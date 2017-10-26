using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuotationSystem.Models;
using QuotationSystem.Common;


namespace QuotationSystem.Controllers
{
    
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

        public ActionResult CreateQuotation()
        {
            
            SalesQuotationViewModel model = new SalesQuotationViewModel();
            
            model.BuyerList = new List<SelectListItem>();

            var buyers = (from buyer in db.Employees
                          where buyer.Department.Name == "采购部"
                          select buyer).ToList();
            //获取采购员列表
            if (buyers.Count > 0)
            {
                buyers.ForEach(p =>
                {
                    model.BuyerList.Add(new SelectListItem { Text = p.Name, Value = p.Id.ToString() });
                });
            }

            model.SelectClass = new List<SelectListItem>();
            //获取产品类别列表
            var classes = (from item in db.ProductClasses
                           select item).ToList();

            if (classes.Count > 0)
            {
                classes.ForEach(p =>
                {
                    model.SelectClass.Add(
                        new SelectListItem { Text=p.Name,Value=p.Id.ToString()}
                        );
                });
            }

            //获取暗佣类别
            model.CommisionList = new List<SelectListItem>() { new SelectListItem { Text = CommisionType.百份比.ToString(), Value = CommisionType.百份比.ToString() },
            new SelectListItem { Text=CommisionType.金额.ToString(),Value=CommisionType.金额.ToString() } };

            return View(model);
        }

        [HttpPost]
        public ActionResult CreateQuotation(SalesQuotationViewModel model)
        {
            if (ModelState.IsValid)
            {

            }
            return View(model);
        }

        public ActionResult BuyIndex(int page = 1)
        {
            return View();
        }
    }
}