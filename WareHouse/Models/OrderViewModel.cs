using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WareHouse.DAO;
using WareHouse.Validators;

namespace WareHouse.Models
{
    public class OrderViewModel
    {
        public enum OrderTypeEnum { Import, Export }
        #region Properties
        public int Id { get; set; }
        public int ProductId { get; set; }
        private Product _product;
        public Product Product
        {
            get
            {
                if (_product == null)
                    _product = new Product().GetProducts(ProductId).FirstOrDefault();
                return _product;
            }
        }
        public DateTime OrderDate { get; set; }
        public decimal Quantity { get; set; }
        public decimal Sum { get; set; }
        public string Username { get; set; }

        public OrderTypeEnum OrderType { get; set; }

        private OrderValidator validator = new OrderValidator();
        #endregion

        public List<Product> AllProducts { get { return new Product().GetProducts(null); } }

        private OrderDAO dao = new OrderDAO();

        internal int Save()
        {
            this.Quantity = OrderType.Equals(OrderTypeEnum.Export) ? this.Quantity * (-1) : this.Quantity;

            if (!validator.OutOfStockValidator(this))
                throw new Exception("Product is out of stock");

            return dao.saveOrder(this);
        }

        /// <summary>GetOrders is a method in the OrderViewModel class that returns product
        /// </summary>
        /// <param name="id">Product id, NULL to get all Orders</param>
        internal List<OrderViewModel> GetOrders(int? id) => dao.getOrders(id);

        internal List<OrderViewModel> GetOrdersByDate(int? productId,DateTime? startDate, DateTime? endDate) => dao.getOrdersForReport(productId,startDate, endDate);

        /// <summary>CalculateSum is a method in the OrderViewModel class that calculates sum 
        /// </summary>
        /// <param name="quantity">Product quantity for calculation</param>
        /// <param name="productId">Product id for calculation</param>
        internal decimal CalculateSum(decimal quantity, int productId)
        {
            var product = new Product().GetProducts(productId).FirstOrDefault();
            if (product != null)
            {
                return product.Price * quantity;
            }
            else
            {
                return 0;
            }
        }
    }
}