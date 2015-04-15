using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Temiskaming.Models
{
    public class OrderClass
    {
        databaseDataContext objOrder = new databaseDataContext();
        


        public IQueryable<Order> getOrder()
        {
            var allOrders = from x in objOrder.Orders select x;
            return allOrders;
        }//end of get order

        //get the id 
        public Order getOrderById(int _id)
        {
            var allOrders = objOrder.Orders.SingleOrDefault(x => x.OrderId == _id);
            return allOrders; 
        }//end get order by id

        public bool CommitInsert(Order Ord)
        {
            using (objOrder)
            {
                
                objOrder.Orders.InsertOnSubmit(Ord);
                objOrder.SubmitChanges();
                return true; 
            }
        }

        public bool commitDelete(int _id)
        {
            using (objOrder)
            {
                var ORDDel = objOrder.Orders.Single(x => x.OrderId == _id);
                objOrder.Orders.DeleteOnSubmit(ORDDel);
                objOrder.SubmitChanges();
                return true; 
            }//end using

        }//ending delete

 
      
    }
}