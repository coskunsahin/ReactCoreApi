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
    public class OrderdetaisController : ControllerBase
    {
        private NorthwindContext db = new NorthwindContext();
        [HttpGet("Getorders")]
        public IQueryable<OrderMasterdentity> Getorders()
        {
            var orderdetails = from o in db.OrderDetails
                               select new OrderMasterdentity()

                               {
                                   orderId = o.OrderId,
                                   orderDate = o.Order.OrderDate,
                                   orderdetayId = o.Order.OrderId,
                                   employeeId = o.Order.EmployeeId,
                                   productId = o.ProductId,
                                   shippedDate = o.Order.ShippedDate,
                                   quantity = o.Quantity,
                                   unitPrice = o.UnitPrice,
                                   shipPostalCode = o.Order.ShipPostalCode,





                               };

            return orderdetails;
        }
        [Consumes("application/json")]
        [Produces("application/json")]
        [HttpPost("Create")]

        public async Task<ActionResult> Create([FromBody] OrderMasterdentity orderentity)
        {

            using (var transaction = db.Database.BeginTransaction())
            {

                try
                {




                    var orderdetails = new OrderDetails()
                    {

                        OrderId = orderentity.orderdetayId,
                        ProductId = orderentity.productId,
                        Quantity = orderentity.quantity,
                        UnitPrice = orderentity.unitPrice,

                    };


                    db.OrderDetails.Add(orderdetails);
                    await db.SaveChangesAsync();

                    transaction.Commit();
                    return Ok();
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