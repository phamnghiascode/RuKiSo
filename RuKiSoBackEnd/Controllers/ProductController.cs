using Microsoft.AspNetCore.Mvc;
using RuKiSoBackEnd.Data;
using RuKiSoBackEnd.Models.Domains;
using RuKiSoBackEnd.Models.DTOs;
using RuKiSoBackEnd.Util;

namespace RuKiSoBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly RuKiSoDataContext dbContext;

        public ProductController(RuKiSoDataContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<Products> domainProducts = dbContext.Products.ToList();

            List<ProductRespone> Products = new List<ProductRespone>();

            foreach (var product in domainProducts)
            {
                Products.Add(product.ToDTO());
            }
            return Ok(Products);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById([FromRoute]int id)
        {
            Products? domainProduct = dbContext.Products.FirstOrDefault(p => p.Id == id);
            if (domainProduct == null)
                return NotFound();
            else
            {
                ProductRespone product = domainProduct.ToDTO(); 
                return Ok(product);
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody]ProductRequest productRequest)
        {
            Products domainProduct = productRequest.ToDomain();

            dbContext.Products.Add(domainProduct);
            dbContext.SaveChanges();

            ProductRespone productRespone = domainProduct.ToDTO();

            return CreatedAtAction(nameof(Create), new {id = productRespone.Id}, productRespone);
        }
        [HttpPut]
        [Route("{id:int}")]
        public IActionResult Update([FromRoute]int id,[FromBody] ProductRequest productRequest)
        {
            var domainProduct = dbContext.Products.FirstOrDefault(p => p.Id == id);
            if (domainProduct == null)
                return NotFound();

            domainProduct.Name = productRequest.Name;
            domainProduct.Description = productRequest.Description;
            domainProduct.Price = productRequest.Price;
            domainProduct.Quantity = productRequest.Quantity;

            dbContext.SaveChanges();
            return Ok(domainProduct.ToDTO()) ;  
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete([FromRoute]int id)
        {
            var domainProduct = dbContext.Products.FirstOrDefault(p =>p.Id == id);
            if (domainProduct == null)
                return NotFound();
            dbContext.Products.Remove(domainProduct);
            dbContext.SaveChanges();

            return Ok();
        }
    }
}
