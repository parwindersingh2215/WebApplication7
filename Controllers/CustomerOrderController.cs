using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication7.Models;

namespace WebApplication7.Controllers
{




    [Route("api/[controller]")]
    [ApiController]

    public class CustomerOrderController : ControllerBase
    {
        private MyDBContext _myDBContext;

        public CustomerOrderController(MyDBContext myDB)
        {
            _myDBContext = myDB;
        }


        /// <summary>
        ///   returns all active orders
        /// </summary>

        [HttpGet]
        public List<CustomerOrder> GetCustomerOrders()
        {

            
            List<CustomerOrder> lst = _myDBContext.CustomerOrders.Include((o => o.OrderHistories))
                .Where(customerorder => customerorder.status != "Deleted").ToList();

            //List<CustomerOrder> lst = _myDBContext.CustomerOrders.Include((o => o.OrderHistories))
            //    .Where(customerorder => customerorder.Id == 6).ToList();
         //List<CustomerOrder> lst = _myDBContext.CustomerOrders.ToList();
            //List<CustomerOrder> lst = _myDBContext.CustomerOrders.Include(x=>x.OrderHistories).ToList();
            return lst;
        }


        /// <summary>
        /// get customer Order details ( object) from request
        /// and save into database
        /// </summary>
        /// <param name="customerOrder"></param>
        /// <returns></returns>
        /// 
       
        [HttpPost]
        public CustomerOrder SaveCustomerOrder(CustomerOrder customerOrder)
        {
            try
            {   

                customerOrder.Orderdate = DateTime.Now.Date;
                _myDBContext.CustomerOrders.Add(customerOrder);
                _myDBContext.SaveChanges();


                // add order history details with default status "Pending"
                OrderHistory orderHistory = new OrderHistory();
                orderHistory.OrderId = customerOrder.Id;
                orderHistory.OrderStatus = "Pending";
                orderHistory.LastUpdated = DateTime.Now.Date;
                _myDBContext.OrderHistorys.Add(orderHistory);
                _myDBContext.SaveChanges();
                return customerOrder;
            }
            catch (Exception)
            {

                return null;
            }


        }


      
        /// this method is used to soft delete order by updating its status to  Deleted
        [HttpDelete]
        public CustomerOrder UpdateCustomerOrderStatus(int OrderId)
        {
            CustomerOrder resultdata =(CustomerOrder)_myDBContext.CustomerOrders.Find(OrderId);
            if (resultdata == null)
            {
                return null;
            }
            resultdata.status = "Deleted";
            _myDBContext.Entry(resultdata).State = EntityState.Modified;
            _myDBContext.SaveChanges();
            return resultdata;
        }


        ///
        /// this method is used to update order details
        [HttpPut]
        public CustomerOrder UpdateCustomerOrder(int Orderid,CustomerOrder customerOrder)
        {
            if (Orderid != customerOrder.Id)
            {
                return null;
            }




            _myDBContext.Entry(customerOrder).State = EntityState.Modified;
            _myDBContext.SaveChanges();
            return customerOrder;
        }
    }
}
