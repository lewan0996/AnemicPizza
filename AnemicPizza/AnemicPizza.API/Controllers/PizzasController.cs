using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AnemicPizza.API.DTO.Ingredient;
using AnemicPizza.API.DTO.Pizza;
using AnemicPizza.Core.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AnemicPizza.API.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class PizzasController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPizzaService _pizzaService;

        public PizzasController(IMapper mapper, IPizzaService pizzaService)
        {
            _mapper = mapper;
            _pizzaService = pizzaService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyList<PizzaDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetAll()
        {
            var result = await _pizzaService.GetAllPizzasAsync();

            var mappedResult = 
                result.Select(i => _mapper.Map<PizzaDTO>(i));

            return Ok(mappedResult);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PizzaDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> Get(int id)
        {
            var pizza = await _pizzaService.GetPizzaByIdAsync(id);
            if (pizza == null)
            {
                return NotFound();
            }

            var pizzaDTO = _mapper.Map<PizzaDTO>(pizza);

            return Ok(pizzaDTO);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(PizzaDTO), (int)HttpStatusCode.Created)]
        public async Task<ActionResult> Create([FromBody] CreatePizzaDTO dto)
        {
            var result = await _pizzaService.CreatePizzaAsync(
                dto.Name,
                dto.Description,
                dto.UnitPrice,
                dto.IngredientIds);

            var resultDto = _mapper.Map<PizzaDTO>(result);

            return CreatedAtAction(nameof(Get), new {resultDto.Id}, resultDto);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            await _pizzaService.DeletePizzaAsync(id);

            return NoContent();
        }

        [HttpPatch("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> Update(int id, [FromBody]UpdatePizzaDTO dto)
        {
            await _pizzaService.UpdatePizzaAsync(
                id,
                dto.Name,
                dto.Description,
                dto.UnitPrice,
                dto.IngredientIds);

            return NoContent();
        }
    }
}