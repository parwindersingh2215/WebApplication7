using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebApplication7.Models
{
    public class OrderHistory
    {
        [Key]
        public int Id { get; set; }
        public string OrderStatus { get; set; }
        public int OrderId { get; set; }

        [Column (TypeName ="Date")]
        public DateTime LastUpdated { get; set; }
        

        [ForeignKey("OrderId")]
        [JsonIgnore]
        public virtual CustomerOrder CustomerOrder { get; set; }

    }
}
