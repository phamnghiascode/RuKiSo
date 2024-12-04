using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> GetAll()
        {
            var domainProducts = await dbContext.Products.ToListAsync();

            var products = domainProducts.Select(product => product.ToDTO()).ToList();

            return Ok(products);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var domainProduct = await dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (domainProduct == null)
                return NotFound();

            var product = domainProduct.ToDTO();
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductRequest productRequest)
        {
            var domainProduct = productRequest.ToDomain();

            await dbContext.Products.AddAsync(domainProduct);
            await dbContext.SaveChangesAsync();

            var productResponse = domainProduct.ToDTO();

            return CreatedAtAction(nameof(GetById), new { id = productResponse.Id }, productResponse);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] ProductRequest productRequest)
        {
            var domainProduct = await dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (domainProduct == null)
                return NotFound();

            domainProduct.Name = productRequest.Name;
            domainProduct.Description = productRequest.Description;
            domainProduct.Price = productRequest.Price;
            domainProduct.Quantity = productRequest.Quantity;

            await dbContext.SaveChangesAsync();
            return Ok(domainProduct.ToDTO());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var domainProduct = await dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (domainProduct == null)
                return NotFound();

            dbContext.Products.Remove(domainProduct);
            await dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
