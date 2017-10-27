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
                        Buyer = p.Buyer.Account,
                        Commision = p.Commision,
                        Id = p.Id,
                        CommisionType = p.CommissionType,
                        ExchangeRate = p.ExchangeRate,
                        Fob = p.Fob,
                        Other = p.Other,
                        PurchaseMemo = p.PurchaseMemo,
                        QDate = p.QDate,
                        Sales = p.Sales.Account,
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
            string account = Session["User"].ToString();
            model.Sales = (from emp in db.Employees
                                  where emp.Account == account
                                  select emp).First().Account;

            var BuyerList = new List<SelectListItem>();

            var buyers = (from buyer in db.Employees
                          where buyer.Department.Name == "采购部"
                          select buyer).ToList();

            

            //获取采购员列表
            if (buyers.Count > 0)
            {
                buyers.ForEach(p =>
                {
                    BuyerList.Add(new SelectListItem { Text = p.Name, Value = p.Id.ToString() });
                });

                ViewBag.BuyerList = BuyerList;
            }

            var SelectClass = new List<SelectListItem>();
            //获取产品类别列表
            var classes = (from item in db.ProductClasses
                           select item).ToList();

            if (classes.Count > 0)
            {
                classes.ForEach(p =>
                {
                    SelectClass.Add(
                        new SelectListItem { Text=p.Name,Value=p.Id.ToString()}
                        );
                });

                ViewBag.SelectClass = classes;
            }

            //获取暗佣类别
            var CommisionList = new List<SelectListItem>() { new SelectListItem { Text = CommisionType.百份比.ToString(), Value = CommisionType.百份比.ToString() },
            new SelectListItem { Text=CommisionType.金额.ToString(),Value=CommisionType.金额.ToString() } };

            ViewBag.CommisionList = CommisionList;

            return View(model);
        }

        [HttpPost]
        public ActionResult CreateQuotation(SalesQuotationViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Details == null || model.Details.Count == 0)
                {
                    ViewBag.ErrorMessage = "报价要有明细";
                    return View(model);
                }

                //头部赋值
                QuotationHeader header = new QuotationHeader()
                {
                    Buyer = (from emp in db.Employees
                             where emp.Id == Convert.ToInt32(model.Buyer)
                             select emp).First(),
                    CommissionType = model.CommisionType,
                    Commision = model.Commision,
                    ExchangeRate = model.ExchangeRate,
                    Fob = model.Fob,
                    SeaCost = model.SeaCost,
                    Sales = (from emp in db.Employees
                             where emp.Account == model.Sales
                             select emp).First(),
                    SalesMemo = model.SalesMemo,
                    PurchaseMemo = model.PurchaseMemo,
                    Other = model.Other,
                    QDate = DateTime.Now,
                    Status=model.Status
                };

                db.QHeaders.Add(header);

                List<QuotationDetail> details = new List<QuotationDetail>();

                //明细赋值
                model.Details.ForEach(p =>
                {
                    QuotationDetail item = new QuotationDetail()
                    {
                        Header = header,
                        Class = (from c in db.ProductClasses
                                 where c.Name == p.ProductClass
                                 select c).First(),
                        Metal=p.Metal,
                        Shape=p.Shape,
                        Model=p.Model,
                        Technology=p.Technology,
                        Qty=p.Qty,
                        Profit=Convert.ToDecimal(
                            (from c in db.ProductClasses
                             where c.Name==p.ProductClass
                             select c.Profit).First()
                            ),
                        PurchasePrice=0,
                        Memo=p.Memo,
                        SalesPrice=0,
                        CommissionCost=0
                    };

                    details.Add(item);

                });

                details.ForEach(p =>
                {
                    db.QDetails.Add(p);
                });

                db.SaveChanges();
            }

            return View(model);
        }

        public ActionResult BuyIndex(int page = 1)
        {
            return View();
        }
    }
}