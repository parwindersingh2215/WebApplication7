using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication7.Models
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderHistoryController : ControllerBase
    {
        private MyDBContext _myDBContext;

        public OrderHistoryController(MyDBContext myDB)
        {
            _myDBContext = myDB;
        }


        /// this method requires 1 parameter OrderId and return its  order history
        [HttpGet]
        public List<OrderHistory> GetCustomerOrdersHistory(int orderid)
        {
            var resultdata = (List<OrderHistory>)_myDBContext.OrderHistorys.Where(
                orderhistory => orderhistory.OrderId == orderid);
            if (resultdata == null)
            {
                return null;
            }
            return resultdata;
        }


        ///
        /// this method is used to save order history
        [HttpPost]
        public OrderHistory SaveOrderHistory(OrderHistory orderHistory)
        {
            try
            {
                
                orderHistory.LastUpdated = DateTime.Now.Date;
                _myDBContext.OrderHistorys.Add(orderHistory);
                _myDBContext.SaveChanges();
                return orderHistory;
            }
            catch (Exception)
            {

                return null;
            }

        }

        [HttpDelete]
        public OrderHistory UpdateOrderStatus(int OrderId)
        {
            OrderHistory resultdata = (OrderHistory)_myDBContext.OrderHistorys.Where
                (corder => corder.OrderId == OrderId);
            if (resultdata == null)
            {
                return null;
            }
            _myDBContext.Entry(resultdata).State = EntityState.Modified;
            _myDBContext.SaveChanges();
            return resultdata;
        }


    }

}

