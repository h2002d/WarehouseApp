using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using WareHouse.DAO;
using ZXing;

namespace WareHouse.Models
{
    public class Product
    {
        #region Properties
        public int? Id { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public bool isDeleted { get; set; }

        private ProductDAO dao = new ProductDAO();
        #endregion

        /// <summary>Save is a method in the Product class that saves product
        /// </summary>
        internal int Save() => dao.saveProduct(this);

        /// <summary>GetProducts is a method in the Product class that returns product
        /// </summary>
        /// <param name="id">Product id, NULL to get all products</param>
        internal List<Product> GetProducts(int? id) => dao.getProducts(id);

        /// <summary>Delete is a method in the Product class that marks product as deleted
        /// </summary>
        /// <param name="id">Product id that need to be deleted</param>
        internal void Delete() => dao.deleteProduct(Convert.ToInt32(this.Id));

        internal byte[] QR()
        {
           
            var QCwriter = new BarcodeWriter();
            QCwriter.Format = BarcodeFormat.QR_CODE;
            var result = QCwriter.Write($"Name: {this.Name}, Description:{this.Description}, Price:{this.Price}");
            var barcodeBitmap = new Bitmap(result);

            using (MemoryStream memory = new MemoryStream())
            {
                barcodeBitmap.Save(memory, ImageFormat.Jpeg);
                byte[] bytes = memory.ToArray();
                return bytes;
            }

        }
    }
}