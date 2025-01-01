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
                    .ThenInclude(bi => bi.Ingredient)
                .ToListAsync();

            List<BatchRes> batchesResponse = batches.Select(batch => batch.ToDTO()).ToList();
            return Ok(batchesResponse);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var batch = await dbContext.Batches
                .Include(b => b.Product)
                .Include(b => b.BatchIngredients)
                    .ThenInclude(bi => bi.Ingredient)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (batch == null)
                return NotFound();

            return Ok(batch.ToDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BatchReq batchRequest)
        {
            // Validate dates
            if (batchRequest.StartDate >= batchRequest.EstimateEndDate)
                return BadRequest("Start date must be before estimate end date");

            // New batch should not have yield
            if (batchRequest.Yield > 0)
                return BadRequest("New batch cannot have yield");

            using var transaction = await dbContext.Database.BeginTransactionAsync();
            try
            {
                // Verify if product exists
                var product = await dbContext.Products.FindAsync(batchRequest.ProductId);
                if (product == null)
                    return BadRequest("Product not found");

                // Create new batch
                var batch = new Batches
                {
                    StartDate = batchRequest.StartDate,
                    EstimateEndDate = batchRequest.EstimateEndDate,
                    ProductId = batchRequest.ProductId,
                    Yield = 0
                };

                // Verify and update ingredient quantities
                foreach (var ingredientRequest in batchRequest.BatchIngredients)
                {
                    var inventoryIngredient = await dbContext.Ingredients.FindAsync(ingredientRequest.IngredientId);
                    if (inventoryIngredient == null)
                        return BadRequest($"Ingredient with ID {ingredientRequest.IngredientId} not found");

                    if (inventoryIngredient.Quantity < ingredientRequest.Quantity)
                        return BadRequest($"Insufficient quantity for ingredient {inventoryIngredient.Name}");

                    inventoryIngredient.Quantity -= ingredientRequest.Quantity;

                    batch.BatchIngredients.Add(new BatchIngredient
                    {
                        IngredientId = ingredientRequest.IngredientId,
                        Quantity = ingredientRequest.Quantity
                    });
                }

                await dbContext.Batches.AddAsync(batch);
                await dbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                // Reload batch with all relations for response
                batch = await dbContext.Batches
                    .Include(b => b.Product)
                    .Include(b => b.BatchIngredients)
                        .ThenInclude(bi => bi.Ingredient)
                    .FirstAsync(b => b.Id == batch.Id);

                return CreatedAtAction(nameof(GetById), new { id = batch.Id }, batch.ToDTO());
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] BatchReq batchRequest)
        {
            if (batchRequest.StartDate >= batchRequest.EstimateEndDate)
                return BadRequest("Start date must be before estimate end date");

            using var transaction = await dbContext.Database.BeginTransactionAsync();
            try
            {
                var batch = await dbContext.Batches
                    .Include(b => b.Product)
                    .Include(b => b.BatchIngredients)
                    .FirstOrDefaultAsync(b => b.Id == id);

                if (batch == null)
                    return NotFound();

                // Update batch info
                batch.StartDate = batchRequest.StartDate;
                batch.EstimateEndDate = batchRequest.EstimateEndDate;

                // Handle product change if needed
                if (batch.ProductId != batchRequest.ProductId)
                {
                    var newProduct = await dbContext.Products.FindAsync(batchRequest.ProductId);
                    if (newProduct == null)
                        return BadRequest("Product not found");
                    batch.ProductId = batchRequest.ProductId;
                }

                // Handle yield update
                if (batchRequest.Yield > 0)
                {
                    if (batch.Yield > 0)
                        return BadRequest("Batch already completed");

                    batch.Yield = batchRequest.Yield;

                    // Update product quantity
                    if (batch.Product != null)
                    {
                        batch.Product.Quantity += batchRequest.Yield;
                    }
                }

                // Update ingredients if batch is not completed
                if (batch.Yield == 0)
                {
                    // Return current ingredients to inventory
                    foreach (var oldIngredient in batch.BatchIngredients)
                    {
                        var inventoryIngredient = await dbContext.Ingredients.FindAsync(oldIngredient.IngredientId);
                        if (inventoryIngredient != null)
                        {
                            inventoryIngredient.Quantity += oldIngredient.Quantity;
                        }
                    }

                    // Clear current ingredients
                    batch.BatchIngredients.Clear();

                    // Add new ingredients
                    foreach (var ingredientRequest in batchRequest.BatchIngredients)
                    {
                        var inventoryIngredient = await dbContext.Ingredients.FindAsync(ingredientRequest.IngredientId);
                        if (inventoryIngredient == null)
                            return BadRequest($"Ingredient with ID {ingredientRequest.IngredientId} not found");

                        if (inventoryIngredient.Quantity < ingredientRequest.Quantity)
                            return BadRequest($"Insufficient quantity for ingredient {inventoryIngredient.Name}");

                        inventoryIngredient.Quantity -= ingredientRequest.Quantity;

                        batch.BatchIngredients.Add(new BatchIngredient
                        {
                            BatchId = batch.Id,
                            IngredientId = ingredientRequest.IngredientId,
                            Quantity = ingredientRequest.Quantity
                        });
                    }
                }

                await dbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                // Reload batch with all relations for response
                batch = await dbContext.Batches
                    .Include(b => b.Product)
                    .Include(b => b.BatchIngredients)
                        .ThenInclude(bi => bi.Ingredient)
                    .FirstAsync(b => b.Id == batch.Id);

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
            using var transaction = await dbContext.Database.BeginTransactionAsync();
            try
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
                await transaction.CommitAsync();

                return Ok();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}