using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication7.Models
{
    public class CustomerOrder
    {

        [Key]
        public int Id { get; set; }
        public string Customername { get; set; }
        public string Mobileno { get; set; }
        public string Productname { get; set; }
        public int Qty { get; set; }
        public float Price { get; set; }

        [Column(TypeName = "Date")]
        public DateTime Orderdate { get; set; }
        public string status { get; set; }

        public ICollection<OrderHistory>? OrderHistories { get; set; }
    }
}
