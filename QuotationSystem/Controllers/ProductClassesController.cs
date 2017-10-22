using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuotationSystem.Models;
using QuotationSystem.Common;

namespace QuotationSystem.Controllers
{
    [UserFilter]
    public class ProductClassesController : Controller
    {
        private QSDbContext db = new QSDbContext();

        // GET: ProductClasses
        public ActionResult Index()
        {
            return View(db.ProductClasses.ToList());
        }

        // GET: ProductClasses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductClass productClass = db.ProductClasses.Find(id);
            if (productClass == null)
            {
                return HttpNotFound();
            }
            return View(productClass);
        }

        // GET: ProductClasses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductClasses/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Profit")] ProductClass productClass)
        {
            if (ModelState.IsValid)
            {
                db.ProductClasses.Add(productClass);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(productClass);
        }

        // GET: ProductClasses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductClass productClass = db.ProductClasses.Find(id);
            if (productClass == null)
            {
                return HttpNotFound();
            }
            return View(productClass);
        }

        // POST: ProductClasses/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Profit")] ProductClass productClass)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productClass).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productClass);
        }

        // GET: ProductClasses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductClass productClass = db.ProductClasses.Find(id);
            if (productClass == null)
            {
                return HttpNotFound();
            }

            var countUsed = (from detail in db.QDetails
                            where detail.Class.Id == (int)id
                            select detail).Count();

            if (countUsed > 0)
            {
                return Content("此类别已被使用，不能删除！");
            }

            return View(productClass);
        }

        // POST: ProductClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductClass productClass = db.ProductClasses.Find(id);
            db.ProductClasses.Remove(productClass);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
