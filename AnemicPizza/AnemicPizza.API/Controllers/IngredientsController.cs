using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AnemicPizza.API.DTO.Ingredient;
using AnemicPizza.Core;
using AnemicPizza.Core.Models.Products;
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
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public IngredientsController(IMapper mapper, IIngredientService ingredientService,
            IUnitOfWorkFactory unitOfWorkFactory)
        {
            _mapper = mapper;
            _ingredientService = ingredientService;
            _unitOfWorkFactory = unitOfWorkFactory;
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
        public async Task<ActionResult> Create([FromBody] CreateIngredientDTO dto)
        {
            using var uow = _unitOfWorkFactory.Create();

            var result = await _ingredientService.CreateIngredientAsync(
                dto.Name,
                dto.Description,
                dto.UnitPrice,
                dto.AvailableQuantity,
                dto.IsSpicy,
                dto.IsVegetarian,
                dto.IsVegan);

            uow.Commit();

            var resultDto = _mapper.Map<IngredientDTO>(result);

            return CreatedAtAction(nameof(Get), new {resultDto.Id}, resultDto);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            using var uow = _unitOfWorkFactory.Create();

            await _ingredientService.DeleteIngredientAsync(id);

            uow.Commit();

            return NoContent();
        }

        [HttpPatch("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> Update(int id, [FromBody]UpdateIngredientDTO dto)
        {
            using var uow = _unitOfWorkFactory.Create();

            await _ingredientService.UpdateIngredientAsync(
                id,
                dto.Name,
                dto.Description,
                dto.UnitPrice,
                dto.AvailableQuantity,
                dto.IsSpicy,
                dto.IsVegetarian,
                dto.IsVegan);
                
            uow.Commit();

            return NoContent();
        }
    }
}