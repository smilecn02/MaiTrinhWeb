using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MaiTrinhWeb.Data;

namespace MaiTrinhWeb.Controllers
{
    public class SellProductsController : Controller
    {
        private MaiTrinhWebContext db = new MaiTrinhWebContext();

        // GET: SellProducts
        public ActionResult Index()
        {
            var sellProducts = db.SellProducts.Include(s => s.Customer).Include(s => s.Product);
            return View(sellProducts.ToList());
        }

        // GET: SellProducts/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SellProduct sellProduct = db.SellProducts.Find(id);
            if (sellProduct == null)
            {
                return HttpNotFound();
            }
            return View(sellProduct);
        }

        // GET: SellProducts/Create
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name");
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name");
            return View();
        }

        // POST: SellProducts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ExportDate,ProductId,Quantity,Price,Description,CustomerId")] SellProduct sellProduct)
        {
            var importProducts = db.ImportProducts.GroupBy(i => i.ProductId)
                    .Select(i => new { ProductId = i.Key, Quantity = i.Sum(j => j.Quantity) });
            var findProduct = importProducts.FirstOrDefault(i => i.ProductId == sellProduct.ProductId);

            if (findProduct != null && findProduct.Quantity < sellProduct.Quantity)
                ModelState.AddModelError("Quantity", "Số lượng sản phẩm bán không được lớn hơn số lượng sản phẩm nhập.");

            if (ModelState.IsValid)
            {
                sellProduct.Id = Guid.NewGuid();
                db.SellProducts.Add(sellProduct);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", sellProduct.CustomerId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", sellProduct.ProductId);
            return View(sellProduct);
        }

        // GET: SellProducts/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SellProduct sellProduct = db.SellProducts.Find(id);
            if (sellProduct == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", sellProduct.CustomerId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", sellProduct.ProductId);
            return View(sellProduct);
        }

        // POST: SellProducts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ExportDate,ProductId,Quantity,Price,Description,CustomerId")] SellProduct sellProduct)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sellProduct).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", sellProduct.CustomerId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", sellProduct.ProductId);
            return View(sellProduct);
        }

        // GET: SellProducts/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SellProduct sellProduct = db.SellProducts.Find(id);
            if (sellProduct == null)
            {
                return HttpNotFound();
            }
            return View(sellProduct);
        }

        // POST: SellProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            SellProduct sellProduct = db.SellProducts.Find(id);
            db.SellProducts.Remove(sellProduct);
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
