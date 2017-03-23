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

            var inventoryQuery = from j1 in importProducts
                                 join j2 in shellProducts
                                 on j1.ProductId equals j2.ProductId 
                                 select new InventoryViewModel { ProductId = j1.ProductId, SumQuantity = j1.SumQuantity - j2.SumQuantity};

                                 //select new InventoryViewModel { ProductId = j1.ProductId, SumQuantity = j1.SumQuantity - j2.SumQuantity };

            var inventoryQuery2 = from j1 in db.ImportProducts.ToList()
                                  join j2 in inventoryQuery.ToList()
                                  on j1.ProductId equals j2.ProductId into j3
                                  from j4 in j3.DefaultIfEmpty()
                                  select new InventoryViewModel { ProductId = j1.ProductId, ProductName = j1.Product.Name,
                                        Images = j1.Product.Images,
                                        SumQuantity = j4 != null ? j4.SumQuantity : j1.Quantity };



            var query = from j1 in db.ImportProducts
                        join j2 in db.SellProducts
                        on j1.ProductId equals j2.ProductId
                        select new { Profit = (j2.Quantity * j2.Price) - j2.ShipPrice - (j2.Quantity * j1.Price) };

            if (query != null && query.Any())
            {
                ViewBag.SumProfit = query.Sum(i => i.Profit);
            }
            else
            {
                ViewBag.SumProfit = 0;
            }
            

            return View(inventoryQuery2.ToList());
        }
    }
}