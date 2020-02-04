using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AnemicPizza.API.DTO.Ingredient;
using AnemicPizza.Core.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AnemicPizza.API.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class IngredientsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IIngredientService _ingredientService;

        public IngredientsController(IMapper mapper, IIngredientService ingredientService)
        {
            _mapper = mapper;
            _ingredientService = ingredientService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyList<IngredientDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetAll()
        {
            var result = await _ingredientService.GetAllIngredientsAsync();

            var mappedResult = 
                result.Select(i => _mapper.Map<IngredientDTO>(i));

            return Ok(mappedResult);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(IngredientDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> Get(int id)
        {
            var ingredient = await _ingredientService.GetIngredientByIdAsync(id);
            if (ingredient == null)
            {
                return NotFound();
            }

            var ingredientDto = _mapper.Map<IngredientDTO>(ingredient);

            return Ok(ingredientDto);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(IngredientDTO), (int)HttpStatusCode.Created)]
        public async Task<ActionResult> Create([FromBody] CreateIngredientDTO ingredientDto)
        {
            var result = await _ingredientService.CreateIngredientAsync(
                ingredientDto.Name,
                ingredientDto.Description,
                ingredientDto.UnitPrice,
                ingredientDto.AvailableQuantity,
                ingredientDto.IsSpicy,
                ingredientDto.IsVegetarian,
                ingredientDto.IsVegan);

            var resultDto = _mapper.Map<IngredientDTO>(result);

            return CreatedAtAction(nameof(Get), new {resultDto.Id}, resultDto);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            await _ingredientService.DeleteIngredientAsync(id);

            return NoContent();
        }

        [HttpPatch("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> Update(int id, [FromBody]UpdateIngredientDTO dto)
        {
            await _ingredientService.UpdateIngredientAsync(
                id,
                dto.Name,
                dto.Description,
                dto.UnitPrice,
                dto.AvailableQuantity,
                dto.IsSpicy,
                dto.IsVegetarian,
                dto.IsVegan);

            return NoContent();
        }
    }
}