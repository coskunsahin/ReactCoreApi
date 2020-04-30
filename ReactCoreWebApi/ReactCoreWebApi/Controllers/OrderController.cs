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
    [Route("api/Order")]

    public class OrderController : ControllerBase
    {
        private NorthwindContext db = new NorthwindContext();
       
           

            [HttpGet("SHOW")]
            public async Task<IActionResult> FindAll()
            {
                try
                {
                    var orders = await db.Orders.Select(p => new
                    {
                        orderID = p.OrderId,
                        ememployeeId = p.EmployeeId,
                        orderDate = p.OrderDate,
                        requiredDate = p.RequiredDate,
                        shipPostalCode=p.ShipPostalCode



                    }).ToListAsync();
                    return Ok(orders);
                }
                catch
                {
                    return BadRequest();
                }

            }
        [Produces("application/json")]
        [HttpGet("search")]
            public async Task<ActionResult<Orders>> search(DateTime min, DateTime max)
        {
            var orders = await db.Orders.Where(p => p.OrderDate >= min && p.OrderDate <= max)
            //    .Select(p => new
            //{
            //    orderID = p.OrderId,
            //    ememployeeId = p.EmployeeId,
            //    orderDate = p.OrderDate,
            //    requiredDate = p.RequiredDate,
            //    shipPostalCode = p.ShipPostalCode



            //})

           
            .ToListAsync();
            return Ok(orders);
        }
        }

    }
