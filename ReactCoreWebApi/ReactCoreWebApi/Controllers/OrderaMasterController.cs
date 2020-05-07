using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactCoreWebApi.Models;
namespace ReactCoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderaMasterController : ControllerBase
    {
        


        private NorthwindContext db = new NorthwindContext();

        [HttpGet("findall")]
        public async Task<ActionResult> FindAll()
        {
            try
            {
                var order = await db.OrderDetails.Include("Order").Select(p => new
                {
                    orderId = p.OrderId,
                    employeeId = p.Order.EmployeeId,
                    orderDate = p.Order.OrderDate,
                    shippedDate = p.Order.ShippedDate,
                    shipPostalCode = p.Order.ShipPostalCode,
                    price =p.UnitPrice,
                    quantity=p.Quantity,

                   productid=p.ProductId
                    
                     


                
                       
                    

              






                }).ToListAsync();
                return Ok(order);
            }
            catch
            {
                return BadRequest();
            }

        }
        [HttpGet("find/{orderId}")]
        public async Task<ActionResult> Find(int orderId)
        {
            try
            {
                var order = await db.OrderDetails.Include("Order").Where(p=>p.OrderId== orderId).Select(p => new
                {
                    orderId = p.OrderId,
                    employeeId = p.Order.EmployeeId,
                    orderDate = p.Order.OrderDate,
                    shippedDate = p.Order.ShippedDate,
                    shipPostalCode = p.Order.ShipPostalCode,
                    price = p.UnitPrice,
                    quantity = p.Quantity,

                    productid = p.ProductId















                }).ToListAsync();
                return Ok(order);
            }
            catch
            {
                return BadRequest();
            }

        }
        [Consumes("application/json")]
        [Produces("application/json")]
        [HttpPost("Create")]

        public async Task<ActionResult> Create([FromBody] OrderMasterdentity orderentity, List<OrderDetails> orderDetails)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {

                    var orders = new Orders()
                    {
                        OrderId = orderentity.orderId,
                        EmployeeId = orderentity.employeeId,
                        OrderDate = orderentity.orderDate,

                        ShippedDate = orderentity.shippedDate,
                        ShipPostalCode = orderentity.shipPostalCode,
                       
                    };


                    db.Orders.Add(orders);



                    //var orderdetails = new List<OrderDetails>()
                    //{
                        
                    //    OrderId = orderentity.orderdetayId,
                    //    ProductId = orderentity.productId,
                    //    Quantity = orderentity.quantity,
                    //    UnitPrice = orderentity.unitPrice,

                    //};


                    //db.OrderDetails.Add(orderdetails);
                    await db.SaveChangesAsync();

                    transaction.Commit();
                    return Ok(orders);
                }
                catch
                {
                    transaction.Rollback();
                    return BadRequest();

                }
            }
        }

    }





}