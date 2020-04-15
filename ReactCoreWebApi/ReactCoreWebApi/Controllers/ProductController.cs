using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using ReactCoreWebApi.Models;
namespace ReactCoreWebApi.Controllers
{
    
    [ApiController]
    [Produces("application/json")]
    [Route("api/Product")]
    public class ProductController : Controller
    {
        private NorthwindContext db = new NorthwindContext();

        [HttpGet("findall")]
        public async Task<IActionResult> findAll()
        {
            try
            {
                var products = db.Products.Select(p => new
                {
                    id = p.ProductId,
                    name = p.ProductName,
                    quality = p.UnitsInStock,
                    price = p.UnitPrice,
                    categoriid = p.CategoryId,
                    categoriname = p.Category.CategoryName,


                }).ToList();
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
                var products = db.Products.Where(p => p.ProductName.Contains(keyword)).Select(p => new
                {
                    id = p.ProductId,
                    name = p.ProductName,
                    quality = p.UnitsInStock,
                    price = p.UnitPrice,
                    categoriid = p.CategoryId,
                    categoriname = p.Category.CategoryName,

                }).ToList();
                return Ok(products);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("Between/{min}/{max}")]
        public async Task<IActionResult> between(decimal min, decimal max)
        {
            try
            {
                var products = db.Products.Where(p => p.UnitPrice >= min && p.UnitPrice <= max).Select(p => new
                {
                    id = p.ProductId,
                    name = p.ProductName,
                    quality = p.UnitsInStock,
                    price = p.UnitPrice,
                    categoriid = p.CategoryId,
                    categoriname = p.Category.CategoryName,

                }).ToList();
                return Ok(products);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("Find/{id}")]
        public async Task<IActionResult> find(int id)
        {
            try
            {
                var products = db.Products.Where(p => p.ProductId == id).Select(p => new
                {
                    id = p.ProductId,
                    name = p.ProductName,
                    quality = p.UnitsInStock,
                    price = p.UnitPrice,
                    categoriid = p.CategoryId,
                    categoriname = p.Category.CategoryName,


                }).SingleOrDefault();
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
                    ProductName = productentity.name,
                    UnitPrice = productentity.price,

                    UnitsInStock = productentity.quantity,
                    CategoryId = productentity.categoriid


                };


                db.Products.Add(product);

                db.SaveChanges();
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
                var product = db.Products.Find(productentity.id);

                product.ProductName = productentity.name;
                product.UnitsInStock = productentity.quantity;
                product.UnitPrice = productentity.price;
                product.CategoryId = productentity.categoriid;



                db.SaveChanges();
                return Ok(product);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> delete(int id)
        {
            try
            {
                var product = db.Products.Find(id);

                db.Products.Remove(product);

                db.SaveChanges();
                return Ok(product);
            }
            catch
            {
                return BadRequest();
            }
        }


    }
}
}