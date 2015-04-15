using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Temiskaming.Models; 

namespace Temiskaming.Controllers
{
    public class GiftShopController : Controller
    {
        //
        // GET: /GiftShop/

        GiftShopClass objGift = new GiftShopClass();
        OrderClass objOrder = new OrderClass();
        

        //home page
        public ActionResult Index() 
        {
            ViewBag.Group = "GiftShop";
            var items = objGift.getGifts(); 
            return View(items);
        }

       //admin page
        public ActionResult AdminGiftShop()
        {
            ViewBag.Group = "GiftShop";
            var Gif = objGift.getGifts();
            if(Gif == null)
            {
                return View("AdminGiftShop");
            }
            else
            {
                return View(Gif);
            }
        }
        
        //insert
        public ActionResult InsertGiftShop()
        {
            return View();
        }



        [HttpPost]
        public ActionResult InsertGiftShop(Gift Gt, HttpPostedFileBase file )
       {
           ViewBag.Group = "GiftShop";
           
            if( file !=null)
            {
                string pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/content/images/GiftShop"), pic);
            //file is uploaded
                file.SaveAs(path);
            }
           
            
            if (ModelState.IsValid)
            {
                try
                {
                    objGift.commitInsert(Gt);
                    return RedirectToAction("AdminGiftShop");
                }//end try
                catch
                {
                    return View();
                }//end catch

            }//end if
            return View(); 
       }//end public insert item 


        //update
        public ActionResult UpdateGiftShop(int ItemId)
        {
            var Gif = objGift.getGiftsById(ItemId);
            if (Gif == null)
            {
                return View("AdminGiftShop"); 
            }
            else
            {
                return View(Gif); 
            }
        }//end public updateitem 


        [HttpPost]
        public ActionResult UpdateGiftShop(int ItemId, Gift Gt)
        {
            ViewBag.Group = "GiftShop";
            if(ModelState.IsValid)
            {
                try
                {
                    objGift.commitUpdate(ItemId, Gt.Item, Gt.Description, Gt.Price );
                    return RedirectToAction("AdminGiftShop"); 
                }//end try
                catch
                {
                    return View(); 
                }
            }//end if
            return View(); 
        }//end update item 

        //DELETE
        public ActionResult DeleteGiftShop(int id)
        {
            var Gif = objGift.getGiftsById(id);
            if (Gif == null)
            {
                return View("itemAdmin");
            }//end if
            else
            {
                return View(Gif);
            }//end else

        }//end delete item

        //start http post
        [HttpPost]
        public ActionResult DeleteGiftShop(int id, Gift Gif)
        {
            ViewBag.Group = "GiftShop";
            try
            {
                objGift.commitDelete(id);
                return RedirectToAction("AdmingiftShop");
            }//end try
            catch
            {
                return View();
            }//end catch
        }//end deleteitem 


        //PURCHASE
        public ActionResult Purchase(string PassItem, string PassPrice)
        {
            //Pass the values to the next page
            @ViewBag.PassItem = PassItem;
            @ViewBag.PassPrice = PassPrice;
            return View();
        }

        [HttpPost]
        public ActionResult Purchase(string PassItem, string PassPrice,  Order Ord)
        {
            //it has pass so assign the values to the table items
            ViewBag.PassItem = PassItem;
            ViewBag.PassPrice = PassPrice;
            Ord.Item = PassItem;
            Ord.Price = Convert.ToDecimal(PassPrice);
            ViewBag.Group = "Gift Shop";
            if (ModelState.IsValid)
            {
                try//try
                {
                    objOrder.CommitInsert(Ord);
                    return RedirectToAction("ThankYou");
                }
                catch
                {
                    return View("Index");
                }
            }
            else//if fails 
            {
                return View();
            }
        }
        //thank you page
        public ActionResult ThankYou()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ThankYou(Order ord)
        {
            return View();
        }

         //ORDER
        public ActionResult Order()
        {
            var Od = objOrder.getOrder();
            ViewBag.Group = "Gift Shop";
            return View(Od);
        }

        //get detail for each entry

        public ActionResult OrderDetail(int OrderId)
        {
           
     
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

 


