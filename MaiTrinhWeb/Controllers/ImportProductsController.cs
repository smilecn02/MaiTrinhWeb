using MaiTrinhWeb.Data;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PagedList;

namespace MaiTrinhWeb.Controllers
{
    [Authorize(Users = "khactrinhcn02@gmail.com,thaingocmaitrinh@gmail.com")]
    public class ImportProductsController : Controller
    {
        private MaiTrinhWebContext db = new MaiTrinhWebContext();

        // GET: ImportProducts
        public ActionResult Index(int? page, string searchData, string filterValue)
        {
            if (searchData != null)
            {
                page = 1;
            }
            else
            {
                searchData = filterValue;
            }

            ViewBag.FilterValue = searchData;

            string searchDataLower = !string.IsNullOrEmpty(searchData) ? searchData.ToLower() : string.Empty;

            var importProducts = db.ImportProducts.Include(i => i.Product)
                    .Where(i => string.IsNullOrEmpty(searchData) 
                        || i.Product.Name.ToLower().Contains(searchDataLower));

            int pageSize = 15;
            int pageNumber = (page ?? 1);

            return View(importProducts.OrderByDescending(i => i.ImportDate).ThenBy(i => i.Product.Name)
                .ToPagedList(pageNumber, pageSize));
        }

        // GET: ImportProducts/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImportProduct importProduct = db.ImportProducts.Find(id);
            if (importProduct == null)
            {
                return HttpNotFound();
            }
            return View(importProduct);
        }

        // GET: ImportProducts/Create
        public ActionResult Create()
        {
            ViewBag.ProductId = new SelectList(db.Products.OrderBy(i => i.Name), "Id", "Name");
            return View();
        }

        // POST: ImportProducts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ImportProduct importProduct)
        {
            if (ModelState.IsValid)
            {
                importProduct.Id = Guid.NewGuid();
                db.ImportProducts.Add(importProduct);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var products = db.Products.OrderBy(i => i.Name).ToList();

            ViewBag.ProductId = new SelectList(products, "Id", "Name", importProduct.ProductId);
            return View(importProduct);
        }

        // GET: ImportProducts/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImportProduct importProduct = db.ImportProducts.Find(id);
            if (importProduct == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductId = new SelectList(db.Products.OrderBy(i => i.Name), "Id", "Name", importProduct.ProductId);
            return View(importProduct);
        }

        // POST: ImportProducts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ImportProduct importProduct)
        {
            if (ModelState.IsValid)
            {
                db.Entry(importProduct).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductId = new SelectList(db.Products.OrderBy(i => i.Name), "Id", "Name", importProduct.ProductId);
            return View(importProduct);
        }

        // GET: ImportProducts/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImportProduct importProduct = db.ImportProducts.Find(id);
            if (importProduct == null)
            {
                return HttpNotFound();
            }
            return View(importProduct);
        }

        // POST: ImportProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ImportProduct importProduct = db.ImportProducts.Find(id);
            db.ImportProducts.Remove(importProduct);
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
