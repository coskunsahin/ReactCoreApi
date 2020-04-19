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
    [Route("api/Product")]
    public class ProductController : Controller
    {
        private NorthwindContext db = new NorthwindContext();

        [HttpGet("findall")]
        public async Task<IActionResult> FindAll()
        {
            try
            {
                var products = await db.Products.Select(p => new
                {
                    id = p.ProductId,
                    name = p.ProductName,
                    quality = p.UnitsInStock,
                    price = p.UnitPrice,
                    categoriid = p.CategoryId,
                    categoriname = p.Category.CategoryName,


                }).ToListAsync();
                return Ok(products);
            }
            catch
            {
                return BadRequest();
            }

        }
        [Produces("application/json")]
        [HttpGet("Search/{keyword}")]
        public async Task<IActionResult> Search(string keyword)
        {
            try
            {
                var products =await db.Products.Where(p => p.ProductName.Contains(keyword)).Select(p => new
                {
                    id = p.ProductId,
                    name = p.ProductName,
                    quality = p.UnitsInStock,
                    price = p.UnitPrice,
                    categoriid = p.CategoryId,
                    categoriname = p.Category.CategoryName,

                }).ToListAsync();
                return Ok(products);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("Between/{min}/{max}")]

        public async Task<ActionResult<Products>>  Between(decimal min, decimal max)

        {
            try
            {
                var products =await db.Products.Where(p => p.UnitPrice >= min && p.UnitPrice <= max).Select(p => new
                {
                    id = p.ProductId,
                    name = p.ProductName,
                    quality = p.UnitsInStock,
                    price = p.UnitPrice,
                    categoriid = p.CategoryId,
                    categoriname = p.Category.CategoryName,

                }).ToListAsync();
                return Ok(products);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("Find/{id}")]
        public async Task<IActionResult> Find(int id)
        {
            try
            {
                var products = await db.Products.Where(p => p.ProductId == id).Select(p => new
                {
                    id = p.ProductId,
                    name = p.ProductName,
                    quality = p.UnitsInStock,
                    price = p.UnitPrice,
                    categoriid = p.CategoryId,
                    categoriname = p.Category.CategoryName,


                }).SingleOrDefaultAsync();
                return Ok(products);
            }
            catch
            {
                return BadRequest();
            }
        }
        [Consumes("application/json")]
        [Produces("application/json")]
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] Productentity productentity)
        {
            try
            {
                var product = new Products()
                {
                    ProductName = productentity.Name,
                    UnitPrice = productentity.Price,

                    UnitsInStock = productentity.Quantity,
                    CategoryId = productentity.Categoriid


                };


                db.Products.Add(product);

               await db.SaveChangesAsync();
                return Ok(product);
            }
            catch
            {
                return BadRequest();

            }
        }
        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] Productentity productentity)
        {
            try
            {
                var product =  db.Products.Find(productentity.Id);

                product.ProductName = productentity.Name;
                product.UnitsInStock = productentity.Quantity;
                product.UnitPrice = productentity.Price;
                product.CategoryId = productentity.Categoriid;



               await db.SaveChangesAsync();
                return Ok(product);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var product = db.Products.Find(id);

                db.Products.Remove(product);

           await     db.SaveChangesAsync();
                return Ok(product);
            }
            catch
            {
                return BadRequest();
            }
        }
    
        [Produces("application/json")]
        [HttpGet]
        public async Task<IActionResult> Getproduct(int ProductId)
        {
            var product =await db.Products.Where(p => p.ProductId == ProductId).Select(p => new
            {
                id = p.ProductId,
                name = p.ProductName,
                quality = p.UnitsInStock,
                price = p.UnitPrice,
                categoriid = p.CategoryId,
                categoriname = p.Category.CategoryName,


            }).SingleOrDefaultAsync();
            ;


            return Ok(product);
        }
        [Produces("application/json")]
        [HttpGet("productname")]
        public async Task<IActionResult> Getproductname(string productname)
        {
            var product =await db.Products.Where(i => i.ProductName == productname).Select(p => new
            {
                id = p.ProductId,
                name = p.ProductName,
                quality = p.UnitsInStock,
                price = p.UnitPrice,
                categoriid = p.CategoryId,
                categoriname = p.Category.CategoryName,


            }).SingleOrDefaultAsync();
            ;

            return Ok(product);
        }
    }
}
