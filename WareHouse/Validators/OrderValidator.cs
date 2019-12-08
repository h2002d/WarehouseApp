using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WareHouse.Models;

namespace WareHouse.Validators
{
    public class OrderValidator
    {
        /// <summary>OutOfStockValidator is a method in the OrderValidator class 
        /// that validates that the product is not out of stock 
        /// </summary>
        /// <param name="quantity">Product quantity for validation</param>
        /// <param name="productId">Product id for validation</param>
        public bool OutOfStockValidator(OrderViewModel order)
        {
            var stockCount = new StockReportViewModel().GetStockReport(order.ProductId).FirstOrDefault().Quantity;
            return stockCount + order.Quantity >= 0;
        }
    }
}