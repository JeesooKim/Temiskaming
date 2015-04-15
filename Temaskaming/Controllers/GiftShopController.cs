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


        public ActionResult Index() 
        {
            ViewBag.Group = "Gift Shop";
            var items = objGift.getGifts(); 
            return View(items);
        }

       
        public ActionResult AdminGiftShop()
        {
            ViewBag.Group = "Gift Shop";
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
        
        public ActionResult InsertGiftShop()
        {
            return View();
        }



        [HttpPost]
        public ActionResult InsertGiftShop(Gift Gt, HttpPostedFileBase file )
       {
           ViewBag.Group = "Gift Shop";
           
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
            ViewBag.Group = "Gift Shop";
            if(ModelState.IsValid)
            {
                try
                {
                    objGift.commitUpdate(ItemId, Gt.Item, Gt.Description, Gt.Price, Gt.Image, Gt.Inventory);
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
            ViewBag.Group = "Gift Shop";
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

       }
}

 


