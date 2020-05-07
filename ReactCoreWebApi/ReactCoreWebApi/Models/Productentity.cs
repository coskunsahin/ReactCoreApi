using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactCoreWebApi.Models
{
    
    public class Productentity
    {
        public int id { get; set; }
        public string name { get; set; }

        public short? quantity { get; set; }
        public decimal price { get; set; }
        public int categoriid { get; set; }
        public string  categoriname { get; set; }
    }
}
