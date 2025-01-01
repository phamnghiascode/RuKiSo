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
    public class IngredientController : ControllerBase
    {
        private readonly RuKiSoDataContext dbContext;

        public IngredientController(RuKiSoDataContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var respones = await dbContext.Ingredients.ToListAsync();

            var Ingredients = respones.Select(respones => respones.ToDTO()).ToList();

            return Ok(Ingredients);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            Ingredients? ingredient = await dbContext.Ingredients.FirstOrDefaultAsync(x => x.Id == id);
            if (ingredient == null)
                return NotFound();
            return Ok(ingredient.ToDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] IngredientRequestAPI ingredientRequest)
        {
            Ingredients ingredientDomain = ingredientRequest.ToDomain();
            await dbContext.Ingredients.AddAsync(ingredientDomain);
            await dbContext.SaveChangesAsync();
            var ingredientRespone = ingredientDomain.ToDTO();
            return CreatedAtAction(nameof(GetById), new { id = ingredientRespone.Id }, ingredientRespone);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id) 
        {
            var domainIngredient = await dbContext.Ingredients.FirstOrDefaultAsync(p => p.Id == id);
            if (domainIngredient == null)
                return NotFound();

            dbContext.Ingredients.Remove(domainIngredient);
            await dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] IngredientRequestAPI ingredientRequest)
        {
            var updateIngredient = await dbContext.Ingredients.FirstOrDefaultAsync(i => i.Id == id);
            if(updateIngredient == null)
                return NotFound();
            updateIngredient.Name = ingredientRequest.Name;
            updateIngredient.PurchasePrice = ingredientRequest.PurchasePrice;
            updateIngredient.Unit = ingredientRequest.Unit;
            updateIngredient.Quantity = ingredientRequest.Quantity;

            await dbContext.SaveChangesAsync();
            return Ok(updateIngredient.ToDTO());
        }

    }
}
