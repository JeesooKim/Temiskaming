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
        //order
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
        public IQueryable<Order> getOrder()
        {
            var allOrders = from x in objGifts.Orders select x;
            return allOrders;
        }//end of get order

        //get the id 
        public Order getOrderById(int _id)
        {
            var allOrders = objGifts.Orders.SingleOrDefault(x => x.OrderId == _id);
            return allOrders;
        }//end get order by id

        public bool CommitInsert(Order Ord)
        {
            using (objGifts)
            {

                objGifts.Orders.InsertOnSubmit(Ord);
                objGifts.SubmitChanges();
                return true;
            }
        }

        public bool CommitDelete(int _id)
        {
            using (objGifts)
            {
                var ORDDel = objGifts.Orders.Single(x => x.OrderId == _id);
                objGifts.Orders.DeleteOnSubmit(ORDDel);
                objGifts.SubmitChanges();
                return true;
            }//end using

        }//ending delete



    }//close public class GiftShop Class
}//close name space