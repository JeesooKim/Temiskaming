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
                            on order.itemId equals gift.ItemId
                            select new { gift.Item,
                                gift.Price, 
                                order.FirstName,
                                order.LastName,
                                order.ToPatient, 
                                order.From,
                                order.Message });

           

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
          public ActionResult Purchase(string ItemId)
          {
              @ViewBag.valurl = ItemId; 
              return View();
          }

          [HttpPost]
          public ActionResult Purchase(Order Ord)
          {
              
              ViewBag.Group = "Gift Shop";
              if (ModelState.IsValid)
              {
                  try
                  {
                      objOrder.CommitInsert(Ord);
                      return RedirectToAction("ThankYou");
                  }
                  catch
                  {
                      return View("error");
                  }
              }
              return View();
          }


    }
}
