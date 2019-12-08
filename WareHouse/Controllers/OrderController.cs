using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WareHouse.Hubs;
using WareHouse.Models;

namespace WareHouse.Controllers
{

    [Authorize]
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            OrderViewModel order = new OrderViewModel();
            return View(order.GetOrders(null));
        }

        [HttpGet]
        public ActionResult Create()
        {
            OrderViewModel product = new OrderViewModel();
            return View(product);
        }

        [HttpPost]
        public ActionResult Create(OrderViewModel order)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    order.Username = User.Identity.Name;
                    order.Save();
                    OrderHub hub = new OrderHub();
                    
                    hub.OrderMade(order.Product.Name, order.Quantity);
                    //SendMessage(order);
                    return RedirectToAction("Index");
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError("Quantity", ex.Message);
                    return View(order);
                }
            }
            else
            {
                return View(order);
            }
        }

        [HttpPost]
        public JsonResult CalculateSum(decimal quantity,int productId)
        {
            var order = new OrderViewModel();
            var sum = order.CalculateSum(quantity,productId);
            return Json(sum, JsonRequestBehavior.AllowGet);
        }
    }
}