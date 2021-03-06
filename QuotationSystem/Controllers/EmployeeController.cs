﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuotationSystem.Models;
using System.Web.Security;
using Newtonsoft.Json;
using QuotationSystem.Common;

namespace QuotationSystem.Controllers
{
    
    public class EmployeeController : Controller
    {
        QSDbContext db = new QSDbContext();

        // GET: Employee
        [UserFilter]
        public ActionResult Index()
        {
            List<Employee> model = new List<Employee>();
            if (db.Employees.Count() > 0)
            {
                model = db.Employees.ToList();
            }

            return View(model);
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

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
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

        [HttpPost]
        public ActionResult Login(EmployeeLoginViewModel emp)
        {
            if (ModelState.IsValid)
            {
                //判断是否登录成功
                if (db.Employees.Count(p=>p.Account==emp.Account&&p.Password==emp.Password) == 1)
                {

                    Session["User"] = emp.Account;
                    var userType = (from user in db.Employees
                                    where user.Account == emp.Account
                                    select user.Department.Name).First();

                    Session["UserType"] = userType;

                    var cookie= SetCookie(emp.Account, userType.ToString());

                    Response.Cookies.Add(cookie);

                    var userloghis = new UserLogHis { Account=emp.Account,Cookie=cookie.Value,LoginTime=DateTime.Now };
                    db.UserLoginHistory.Add(userloghis);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }

            return Redirect("/Employee/login");
        }

        public ActionResult Logout()
        {
            Session["User"] = null;

            Response.Cookies.Remove(FormsAuthentication.FormsCookieName);

            return RedirectToAction("Login");
        }

        private HttpCookie SetCookie(string Account,string userType)
        {
            var userData = new { Account, userType };
            //序列化用户信息
            string empData = JsonConvert.SerializeObject(userData);

            //保存用户身份信息
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, userData.Account, DateTime.Now, 
                DateTime.Now.AddHours(1), false, empData);
            
            //加密身份信息，保存至Cookie
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));

            return cookie;

        }
    }
}