using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RuKiSoBackEnd.Data;
using RuKiSoBackEnd.Models.DTOs;
using RuKiSoBackEnd.Util;

namespace RuKiSoBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BatchController : ControllerBase
    {
        private readonly RuKiSoDataContext dbContext;

        public BatchController(RuKiSoDataContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var batches = await dbContext.Batches
                .Include(b => b.Product)
                .Include(b => b.BatchIngredients)
                .ToListAsync();

            var batchesResponse = batches.Select(batch => batch.ToDTO()).ToList();
            return Ok(batchesResponse);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var batch = await dbContext.Batches
                .Include(b => b.Product)
                .Include(b => b.BatchIngredients)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (batch == null)
                return NotFound();

            return Ok(batch.ToDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BatchRequest batchRequest)
        {
            using var transaction = await dbContext.Database.BeginTransactionAsync();
            try
            {
                var batchDomain = batchRequest.ToDomain();

                // Verify if product exists
                var product = await dbContext.Products.FindAsync(batchRequest.ProductId);
                if (product == null)
                    return BadRequest("Product not found");

                // Verify and update ingredient quantities
                foreach (var ingredient in batchRequest.BatchIngredients)
                {
                    var inventoryIngredient = await dbContext.Ingredients.FindAsync(ingredient.IngredientId);
                    if (inventoryIngredient == null)
                        return BadRequest($"Ingredient with ID {ingredient.IngredientId} not found");

                    if (inventoryIngredient.Quantity < ingredient.Quantity)
                        return BadRequest($"Insufficient quantity for ingredient {inventoryIngredient.Name}");

                    inventoryIngredient.Quantity -= ingredient.Quantity;
                }

                await dbContext.Batches.AddAsync(batchDomain);
                await dbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                return CreatedAtAction(nameof(GetById), new { id = batchDomain.Id }, batchDomain.ToDTO());
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] BatchRequest batchRequest)
        {
            using var transaction = await dbContext.Database.BeginTransactionAsync();
            try
            {
                var batch = await dbContext.Batches
                    .Include(b => b.Product)
                    .FirstOrDefaultAsync(b => b.Id == id);

                if (batch == null)
                    return NotFound();

                // Cập nhật thông tin cơ bản của batch
                batch.StartDate = batchRequest.StartDate;
                batch.EstimateEndDate = batchRequest.EstimateEndDate;
                batch.ProductId = batchRequest.ProductId;

                // Nếu có cập nhật Yield, tức là hoàn thành mẻ
                if (batchRequest.Yield > 0)
                {
                    // Kiểm tra xem mẻ đã hoàn thành chưa
                    if (batch.Yield > 0)
                        return BadRequest("Batch already completed");

                    batch.Yield = batchRequest.Yield;

                    // Cập nhật số lượng sản phẩm
                    if (batch.Product != null)
                    {
                        batch.Product.Quantity += batchRequest.Yield;
                    }
                }

                await dbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(batch.ToDTO());
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var batch = await dbContext.Batches
                .Include(b => b.BatchIngredients)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (batch == null)
                return NotFound();

            if (batch.Yield > 0)
                return BadRequest("Cannot delete completed batch");

            // Return ingredients to inventory
            foreach (var batchIngredient in batch.BatchIngredients)
            {
                var ingredient = await dbContext.Ingredients.FindAsync(batchIngredient.IngredientId);
                if (ingredient != null)
                {
                    ingredient.Quantity += batchIngredient.Quantity;
                }
            }

            dbContext.Batches.Remove(batch);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}