using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactCoreWebApi.Models
{
    public class Orderentity
    {
        public int orderId { get; set; }
        public string customerId { get; set; }
        public int? employeeId { get; set; }
        public DateTime? orderDate { get; set; }
        public DateTime? requiredDate { get; set; }
        public DateTime? shippedDate { get; set; }
        public string shipPostalCode { get; set; }
    }
}
