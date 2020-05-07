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
    //[Route("api/Product")]
    public class CategoriController : ControllerBase
    {

        private NorthwindContext db = new NorthwindContext();


        [HttpGet("Categori")]
        public async Task<IActionResult> GetCategori()
        {
            var Categori = await db.Categories.ToListAsync();
            return Ok(Categori);
        }
        [EnableCors]
        [Produces("application/json")]
        [HttpGet("Product")]
        public async Task<IActionResult> GetProduct(int CategoryId)
        {
            var product = await db.Products.Where(i => i.CategoryId == CategoryId).ToListAsync();

            return Ok(product);

        }
    }
}