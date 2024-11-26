using Microsoft.AspNetCore.Mvc;
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
        public IActionResult GetAll()
        {
            var Ingredients = dbContext.Ingredients.ToList();
            return Ok(Ingredients);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById([FromRoute]int id)
        {
            Ingredients? ingredient = dbContext.Ingredients.FirstOrDefault(x => x.Id == id);
            if (ingredient == null) 
                return NotFound();
            return Ok(ingredient);
        }

        [HttpPost]
        public IActionResult Create([FromBody] IngredientRequest ingredientRequest)
        {
            Ingredients ingredientDomain = ingredientRequest.ToDomain();
            dbContext.Ingredients.Add(ingredientDomain);
            return Ok(ingredientDomain.ToDTO());
        }
    }
}
