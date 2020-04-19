using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactCoreWebApi.Models
{
    
    public class Productentity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public short? Quantity { get; set; }
        public decimal Price { get; set; }
        public int Categoriid { get; set; }
        public int  Categoriname { get; set; }
    }
}
