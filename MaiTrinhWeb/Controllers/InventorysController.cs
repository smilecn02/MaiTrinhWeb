using MaiTrinhWeb.Data;
using MaiTrinhWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MaiTrinhWeb.Controllers
{
    public class InventorysController : Controller
    {
        private MaiTrinhWebContext db = new MaiTrinhWebContext();

        public ActionResult Index()
        {
            var importProducts = db.ImportProducts.GroupBy(i => new { i.ProductId, i.Product.Name })
                .Select(i => new InventoryViewModel { ProductId = i.Key.ProductId, ProductName = i.Key.Name
                                                        , SumQuantity = i.Sum(j => j.Quantity) })
                .ToList();

            var shellProducts = db.SellProducts.GroupBy(i => new { i.ProductId, i.Product.Name })
                .Select(i => new InventoryViewModel { ProductId = i.Key.ProductId, ProductName = i.Key.Name
                                                        , SumQuantity = i.Sum(j => j.Quantity) })
                .ToList();

            foreach(var item in importProducts)
            {
                var findSellProduct = shellProducts.FirstOrDefault(i => i.ProductId == item.ProductId);
                if (findSellProduct != null) item.SumQuantity -= findSellProduct.SumQuantity;
            }

            return View(importProducts.ToList());
        }
    }
}