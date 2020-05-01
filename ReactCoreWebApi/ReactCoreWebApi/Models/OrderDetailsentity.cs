using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactCoreWebApi.Models
{
    public class OrderDetailsentity
    {
        public int orderId { get; set; }
        public int productId { get; set; }
        public decimal unitPrice { get; set; }
        public short quantity { get; set; }
        public int? employeeId { get; set; }
        public DateTime? orderDate { get; set; }
        public DateTime? shippedDate { get; set; }
        public string shipPostalCode { get; set; }

    }
}
