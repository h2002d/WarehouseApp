using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WareHouse.DAO;

namespace WareHouse.Models
{
    public class StockReportViewModel
    {
        public ProductModel Product { get; set; }
        public decimal Quantity { get; set; }
        //public ReportViewModel()
        //{
        //    Product = new Product();
        //}

        ReportDAO dao = new ReportDAO();
        internal List<StockReportViewModel> GetStockReport(int? productId)
        {
            return dao.getStockReport(productId);
        }
    }
}