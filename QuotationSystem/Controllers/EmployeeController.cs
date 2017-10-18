using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuotationSystem.Models;

namespace QuotationSystem.Controllers
{
    public class EmployeeController : Controller
    {
        QSDbContext db = new QSDbContext();

        // GET: Employee
        public ActionResult Index()
        {
            return View(db.Employees.ToList());
        }

        public ActionResult Register()
        {
            var model = new EmployeeRegViewModel();
            model.Departments = new List<SelectListItem>();
            db.Departments.ToList().ForEach(p =>
            {
                model.Departments.Add(new SelectListItem {Text=p.Name,Value=p.Id.ToString() });
            });

            return View(model);
        }

        [HttpPost]
        public ActionResult Register(FormCollection collection)
        {

            if (ModelState.IsValid)
            {
                var account = collection["Account"];
                if (db.Employees.Count(e => e.Account == account) == 0)
                {
                    Employee model = new Employee();
                    model.Name = collection["Name"];
                    model.Password = collection["Password"];
                    model.Account = collection["Account"];
                    model.Department = db.Departments.Find(Convert.ToInt32(collection["Departments"]));
                    model.RegisterTime = DateTime.Now;
                    model.LastLogTime = DateTime.Now;

                    db.Employees.Add(model);
                    db.SaveChanges();
                }
                else
                {
                    ContentResult content = new ContentResult();
                    content.Content = "<script type='text/javascript'>alert('账号重复')</script>";
                    return content;
                }
                
            }

            return RedirectToAction("Index");
        }

        public ActionResult Login()
        {
            return View();
        }
    }
}