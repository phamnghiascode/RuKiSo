using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RuKiSoBackEnd.Data;
using RuKiSoBackEnd.Models.DTOs;
using RuKiSoBackEnd.Util;

namespace YourNamespace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly RuKiSoDataContext dbContext;

        public TransactionController(RuKiSoDataContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var domainTransactions = await dbContext.Transactions
                .Include(t => t.Ingredient)
                .Include(t => t.Product)
                .ToListAsync();

            var transactions = domainTransactions.Select(t =>
            {
                t.Name = t.TranType
                    ? dbContext.Products.Find(t.ProductId)?.Name
                    : dbContext.Ingredients.Find(t.IngredientId)?.Name;
                return t.ToDTO();
            }).ToList();

            return Ok(transactions);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var domainTransaction = await dbContext.Transactions
                .Include(t => t.Ingredient)
                .Include(t => t.Product)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (domainTransaction == null)
                return NotFound();

            // Xử lý tên trong controller
            domainTransaction.Name = domainTransaction.TranType
                ? dbContext.Products.Find(domainTransaction.ProductId)?.Name
                : dbContext.Ingredients.Find(domainTransaction.IngredientId)?.Name;

            var transaction = domainTransaction.ToDTO();
            return Ok(transaction);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TransactionReq transactionRequest)
        {
            // Validate transaction type and required IDs
            if (!transactionRequest.TranType && !transactionRequest.IngredientId.HasValue)
                return BadRequest("IngredientId is required for import transactions");

            if (transactionRequest.TranType && !transactionRequest.ProductId.HasValue)
                return BadRequest("ProductId is required for export transactions");

            var domainTransaction = transactionRequest.ToDomain();

            // Cập nhật số lượng nguyên liệu hoặc sản phẩm
            if (!transactionRequest.TranType) // Nhập
            {
                var ingredient = await dbContext.Ingredients.FindAsync(transactionRequest.IngredientId);
                ingredient.Quantity += transactionRequest.Quantity;
                domainTransaction.Name = ingredient.Name; // Đặt tên transaction là tên nguyên liệu
            }
            else // Xuất
            {
                var product = await dbContext.Products.FindAsync(transactionRequest.ProductId);

                if (product.Quantity < transactionRequest.Quantity)
                    return BadRequest("Không đủ số lượng sản phẩm để xuất");

                product.Quantity -= transactionRequest.Quantity;
                domainTransaction.Name = product.Name; // Đặt tên transaction là tên sản phẩm
            }

            await dbContext.Transactions.AddAsync(domainTransaction);
            await dbContext.SaveChangesAsync();

            var transactionResponse = domainTransaction.ToDTO();
            return CreatedAtAction(nameof(GetById), new { id = transactionResponse.Id }, transactionResponse);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] TransactionReq transactionRequest)
        {
            var domainTransaction = await dbContext.Transactions.FirstOrDefaultAsync(t => t.Id == id);

            if (domainTransaction == null)
                return NotFound();

            // Hoàn nguyên số lượng của giao dịch cũ
            if (!domainTransaction.TranType) // Giao dịch nhập cũ
            {
                var oldIngredient = await dbContext.Ingredients.FindAsync(domainTransaction.IngredientId);
                oldIngredient.Quantity -= domainTransaction.Quantity;
            }
            else // Giao dịch xuất cũ
            {
                var oldProduct = await dbContext.Products.FindAsync(domainTransaction.ProductId);
                oldProduct.Quantity += domainTransaction.Quantity;
            }

            // Cập nhật giao dịch mới
            if (!transactionRequest.TranType) // Nhập
            {
                if (!transactionRequest.IngredientId.HasValue)
                    return BadRequest("IngredientId is required for import transactions");

                var ingredient = await dbContext.Ingredients.FindAsync(transactionRequest.IngredientId);
                ingredient.Quantity += transactionRequest.Quantity;

                domainTransaction.IngredientId = transactionRequest.IngredientId;
                domainTransaction.ProductId = null;
                domainTransaction.Name = ingredient.Name;
            }
            else // Xuất
            {
                if (!transactionRequest.ProductId.HasValue)
                    return BadRequest("ProductId is required for export transactions");

                var product = await dbContext.Products.FindAsync(transactionRequest.ProductId);

                if (product.Quantity < transactionRequest.Quantity)
                    return BadRequest("Không đủ số lượng sản phẩm để xuất");

                product.Quantity -= transactionRequest.Quantity;

                domainTransaction.ProductId = transactionRequest.ProductId;
                domainTransaction.IngredientId = null;
                domainTransaction.Name = product.Name;
            }

            domainTransaction.TranType = transactionRequest.TranType;
            domainTransaction.Quantity = transactionRequest.Quantity;
            domainTransaction.Value = transactionRequest.Value;
            domainTransaction.TranDate = transactionRequest.TranDate ?? DateTime.Now;

            await dbContext.SaveChangesAsync();
            return Ok(domainTransaction.ToDTO());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var domainTransaction = await dbContext.Transactions.FirstOrDefaultAsync(t => t.Id == id);

            if (domainTransaction == null)
                return NotFound();

            // Hoàn nguyên số lượng
            if (!domainTransaction.TranType) // Giao dịch nhập
            {
                var ingredient = await dbContext.Ingredients.FindAsync(domainTransaction.IngredientId);
                ingredient.Quantity -= domainTransaction.Quantity;
            }
            else // Giao dịch xuất
            {
                var product = await dbContext.Products.FindAsync(domainTransaction.ProductId);
                product.Quantity += domainTransaction.Quantity;
            }

            dbContext.Transactions.Remove(domainTransaction);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}