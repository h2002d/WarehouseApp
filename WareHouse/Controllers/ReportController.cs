using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WareHouse.Models;

namespace WareHouse.Controllers
{

    [Authorize(Roles = "Administrators")]
    public class ReportController : Controller
    {
        // GET: Report
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Warehouse()
        {
            var productList = new Product().GetProducts(null);
            return View(productList);
        }

        [HttpGet]
        public PartialViewResult _StockReport(int? id)
        {
            var reportView = new StockReportViewModel().GetStockReport(id);
            return PartialView(reportView);
        }

        [HttpGet]
        public PartialViewResult _OrderReport(int? productId,DateTime? startDate,DateTime? endDate)
        {
            var reportView = new OrderViewModel().GetOrdersByDate(productId,startDate, endDate);
            ViewBag.ProductList = new Product().GetProducts(null);
            return PartialView(reportView);
        }


    }
}