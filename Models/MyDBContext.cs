using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication7.Models
{
    public class MyDBContext : DbContext
    {
        //
        public MyDBContext(DbContextOptions<MyDBContext> options) : base(options)
        {

        }
        public DbSet<CustomerOrder> CustomerOrders { get; set; }
        public DbSet<OrderHistory> OrderHistorys { get; set; }
    }
}
