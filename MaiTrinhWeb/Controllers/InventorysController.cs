using MaiTrinhWeb.Data;
using MaiTrinhWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace MaiTrinhWeb.Controllers
{
    [Authorize(Users = "khactrinhcn02@gmail.com,thaingocmaitrinh@gmail.com")]
    public class InventorysController : Controller
    {
        private MaiTrinhWebContext db = new MaiTrinhWebContext();

        public ActionResult Index(int? page, string searchData, string filterValue)
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


            ViewBag.SumProfit = query != null && query.Any() ? query.Sum(i => i.Profit) : 0;

            ViewBag.SumProfitJanuary = getProfitByMonth(1);
            ViewBag.SumProfitFebruary = getProfitByMonth(2);
            ViewBag.SumProfitMarch = getProfitByMonth(3);
            ViewBag.SumProfitApril = getProfitByMonth(4);
            ViewBag.SumProfitMay = getProfitByMonth(5);

            ViewBag.SumProfitJune = getProfitByMonth(6);
            ViewBag.SumProfitJuly = getProfitByMonth(7);
            ViewBag.SumProfitAugust = getProfitByMonth(8);
            ViewBag.SumProfitSeptember = getProfitByMonth(9);
            ViewBag.SumProfitOctober = getProfitByMonth(10);

            ViewBag.SumProfitNovember = getProfitByMonth(11);
            ViewBag.SumProfitDecember = getProfitByMonth(12);


            ViewBag.SumImportProfit = db.ImportProducts.Any() ? db.ImportProducts.Sum(i => i.Price * i.Quantity) : 0;
            ViewBag.SumSellProfit = db.SellProducts.Any() ? db.SellProducts.Sum(i => i.Price * i.Quantity - i.ShipPrice) : 0;

            if (searchData != null)
            {
                page = 1;
            }
            else
            {
                searchData = filterValue;
            }

            ViewBag.FilterValue = searchData;

            int pageSize = 15;
            int pageNumber = (page ?? 1);

            return View(inventoryQuery2.Where(i => i.SumQuantity > 0)
                .OrderBy(i => i.ProductName)
                .Where(i => string.IsNullOrEmpty(searchData)
                    || i.ProductName.ToLower().Contains(searchData.ToLower()))
                .ToPagedList(pageNumber, pageSize));
        }

        private decimal getProfitByMonth(int month)
        {
            var currentYear = DateTime.Now.Year;

            var query = from j1 in db.ImportProducts
                             join j2 in db.SellProducts.Where(i => i.SellDate.Month == month && i.SellDate.Year == currentYear)
                             on j1.ProductId equals j2.ProductId
                             select new { Profit = (j2.Quantity * j2.Price) - j2.ShipPrice - (j2.Quantity * j1.Price) };

            return (query != null && query.Any()) ? query.Sum(i => i.Profit) : 0;
        }
    }
}