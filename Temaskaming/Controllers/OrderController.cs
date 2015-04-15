using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Temiskaming.Models;

namespace Temiskaming.Controllers
{
    public class OrderController : Controller
    {
        //
        // GET: /OrderDetail/

        OrderClass objOrder = new OrderClass();

        GiftShopClass objGift = new GiftShopClass();

        
        public ActionResult Order()
        {
            var Od = objOrder.getOrder();
            ViewBag.Group = "Gift Shop";
            return View(Od);
        }

        //get detail for each entry

        public ActionResult OrderDetail(int OrderId)
        {
            var allDetail = from i in objOrder.getOrder() select i; 
            var getDetail = (from order in objOrder.getOrder()
                            join gift in objGift.getGifts()
                            on order.Item equals gift.Item
                            select new { gift.Item,
                                gift.Price, 
                                order.FirstName,
                                order.LastName,
                                order.ToPatient, 
                                order.From,
                                order.Message });

           foreach( var Det in getDetail)
           {
               Console.WriteLine(Det);
           }
           Console.ReadLine();

            ViewBag.Group = "Gift Shop";
            var Ord = objOrder.getOrderById(OrderId);
            if(Ord == null)
            {
                return View("Order");
            }
            else
            {
                return View(Ord);
            }
        }

        //insert when purchasing

        //Delete
        public ActionResult OrderDetailDelete(int OrderId)
        {
            var Ord = objOrder.getOrderById(OrderId);
            if(Ord == null)
            {
                return View("Order");
            }
            else
            {
                return View(Ord);
            }
        }
          [HttpPost]
        public ActionResult OrderDetailDelete(int OrderId, Order Or)
        {
            ViewBag.Group = "Gift Shop";
            try
            {
                objOrder.commitDelete(OrderId);
                return RedirectToAction("Order");
            }
              catch
            {
                return View();
            }
        }
          
    }
}
