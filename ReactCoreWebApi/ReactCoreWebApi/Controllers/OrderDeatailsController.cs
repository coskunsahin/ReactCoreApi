using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using ReactCoreWebApi.Models;
namespace ReactCoreWebApi.Controllers
{

    [EnableCors("_myAllowSpecificOrigins")]
    [ApiController]
    [Produces("application/json")]
    [Route("api/OrderDetails")]
    public class OrderDeatailsController : ControllerBase
    {


        private NorthwindContext db = new NorthwindContext();
        [HttpGet("findall")]

        public async Task<ActionResult> Findall()

        {
            try
            {
                var orderD = await db.OrderDetails.Select(a => new
                {
                    orderId = a.OrderId,
                    producId = a.ProductId,
                    unitPrice = a.UnitPrice,
                    quantity = a.Quantity,
                    employeeId = a.Order.EmployeeId,
                    orderDate = a.Order.OrderDate,
                    shippedDate = a.Order.ShippedDate,

                    shipPostalCode = a.Order.OrderDate


                }).ToListAsync();
                return Ok(orderD);
            }
            catch
            {
                return BadRequest();
            }


        }



        [HttpGet("Find")]
        public async Task<IActionResult> Find(int OrderId)
        {
            try
            {
                var orderd = await db.OrderDetails.Where(p => p.OrderId == OrderId).Select(a => new
                {
                    orderId = a.OrderId,
                    producId = a.ProductId,
                    unitPrice = a.UnitPrice,
                    quantity = a.Quantity,
                    employeeId = a.Order.EmployeeId,
                    orderDate = a.Order.OrderDate,
                    shippedDate = a.Order.ShippedDate,

                    shipPostalCode = a.Order.OrderDate


                }).ToListAsync();
                return Ok(orderd);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}