using MaiTrinhWeb.Data;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PagedList;

namespace MaiTrinhWeb.Controllers
{
    [Authorize(Users = "khactrinhcn02@gmail.com,thaingocmaitrinh@gmail.com")]
    public class SellProductsController : Controller
    {
        private SelectList GetProductId(Guid? selectedProductId = null)
        {
            return new SelectList(db.ImportProducts
                .Where(i => i.Quantity > 0)
                .Select(x => x.Product)
                .OrderBy(i => i.Name)
                , "Id", "Name", selectedProductId ?? Guid.Empty);
        }

        private MaiTrinhWebContext db = new MaiTrinhWebContext();

        // GET: SellProducts
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

            var sellProducts = db.SellProducts
                .Include(s => s.Customer)
                .Include(s => s.Product)
                .Where(i => string.IsNullOrEmpty(searchData)
                    || i.Product.Name.ToLower().Contains(searchData.ToLower())
                    || i.Customer.Name.ToLower().Contains(searchData.ToLower()));

            int pageSize = 15;
            int pageNumber = (page ?? 1);

            return View(sellProducts.OrderByDescending(i => i.SellDate).ThenBy(i => i.Product.Name).ToPagedList(pageNumber, pageSize));
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
            ViewBag.CustomerId = new SelectList(db.Customers.OrderBy(i => i.Name) , "Id", "Name");
            ViewBag.ProductId = GetProductId();

            return View();
        }

        // POST: SellProducts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SellProduct sellProduct)
        {
            var importProducts = db.ImportProducts.GroupBy(i => i.ProductId)
                    .Select(i => new { ProductId = i.Key, Quantity = i.Sum(j => j.Quantity) });

            var shellProducts = db.SellProducts.GroupBy(i => i.ProductId)
                    .Select(i => new { ProductId = i.Key, Quantity = i.Sum(j => j.Quantity) });

            var findImportProduct = importProducts.FirstOrDefault(i => i.ProductId == sellProduct.ProductId);
            var findShellProduct = shellProducts.FirstOrDefault(i => i.ProductId == sellProduct.ProductId);

            int existProductCount = (findImportProduct != null ? findImportProduct.Quantity : 0) -
                        (findShellProduct != null ? findShellProduct.Quantity : 0);

            if (sellProduct.Quantity > existProductCount)
                ModelState.AddModelError("Quantity", "Số lượng sản phẩm bán không được lớn hơn số lượng sản phẩm nhập.");

            if (ModelState.IsValid)
            {
                sellProduct.Id = Guid.NewGuid();
                db.SellProducts.Add(sellProduct);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList(db.Customers.OrderBy(i => i.Name), "Id", "Name", sellProduct.CustomerId);

            ViewBag.ProductId = GetProductId(sellProduct.ProductId);

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
            ViewBag.CustomerId = new SelectList(db.Customers.OrderBy(i => i.Name), "Id", "Name", sellProduct.CustomerId);

            ViewBag.ProductId = GetProductId(sellProduct.ProductId);

            return View(sellProduct);
        }

        // POST: SellProducts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SellProduct sellProduct)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sellProduct).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.Customers.OrderBy(i => i.Name), "Id", "Name", sellProduct.CustomerId);

            ViewBag.ProductId = GetProductId(sellProduct.ProductId);

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
