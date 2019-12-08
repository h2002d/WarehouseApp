
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WareHouse.Models;
using ZXing;

namespace WareHouse.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var product = new Product();
            return View(product.GetProducts(null));
        }

        [Authorize(Roles = "Administrators")]
        [HttpGet]
        public ActionResult Create(int? id)
        {
            Product product = new Product();
            if (id != null)
                product = product.GetProducts(id).FirstOrDefault();
            return View(product);
        }

        [Authorize(Roles = "Administrators")]
        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                product.Save();
                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }
        }

        [Authorize(Roles = "Administrators")]
        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                Product product = new Product();
                product = product.GetProducts(id).FirstOrDefault();
                if (product == null)
                {
                    ModelState.AddModelError("", $"Product with {id} Id does not exist!!!");
                }
                else
                {
                    product.Delete();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return RedirectToAction("Index");
        }

        public ActionResult QRGenerator(int productId)
        {
            var product = new Product().GetProducts(productId).FirstOrDefault();
            return File(product.QR(), "application/octet-stream", "barcode.jpg");
        }
    }
}