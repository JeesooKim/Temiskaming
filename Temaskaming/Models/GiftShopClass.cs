using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Temiskaming.Models
{
    public class GiftShopClass
    {
        //class name 
        databaseDataContext objGifts = new databaseDataContext();


        public IQueryable<Gift> getGifts()
        {
            //get gifts 
            var allGifts = from x in objGifts.Gifts select x;
            return allGifts;
        }

        //get the gifts by id
        public Gift getGiftsById(int _id)
        {

            var allGifts = objGifts.Gifts.SingleOrDefault( x => x.ItemId == _id);
            return allGifts; 
        }

   


        //add new gifts 

        public bool commitInsert(Gift Gt)
        {
            using (objGifts)
            {
                objGifts.Gifts.InsertOnSubmit(Gt);
                objGifts.SubmitChanges();
                return true; 
            }
        }//end of insert

        public bool commitUpdate(int _ItemId, string _Item, string _Description, decimal _Price, string _Image, decimal _Inventory)
        {
            using(objGifts)
            {
               //first get id 
                var GiftUp = objGifts.Gifts.Single(x => x.ItemId == _ItemId);
                GiftUp.Item = _Item;
                GiftUp.Description = _Description;
                GiftUp.Price = _Price;
                GiftUp.Image = _Image;
                GiftUp.Inventory = _Inventory;
                objGifts.SubmitChanges();
                return true; 
            }
        }//close commit update

        //delete

        public bool commitDelete(int _id)
        {
            using (objGifts)
            {
                var GiftsDel = 
                    objGifts.Gifts.Single(x => x.ItemId == _id);
                objGifts.Gifts.DeleteOnSubmit(GiftsDel);
                objGifts.SubmitChanges();
                return true; 
            }
        }



    }//close public class GiftShop Class
}//close name space