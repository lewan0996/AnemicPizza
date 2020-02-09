using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AnemicPizza.API.DTO.Pizza;
using AnemicPizza.Core;
using AnemicPizza.Core.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
#pragma warning disable 1591

namespace AnemicPizza.API.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class PizzasController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPizzaService _pizzaService;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public PizzasController(IMapper mapper, IPizzaService pizzaService, IUnitOfWorkFactory unitOfWorkFactory)
        {
            _mapper = mapper;
            _pizzaService = pizzaService;
            _unitOfWorkFactory = unitOfWorkFactory;
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
            using var uow = _unitOfWorkFactory.Create();

            var result = await _pizzaService.CreatePizzaAsync(
                dto.Name,
                dto.Description,
                dto.UnitPrice,
                dto.IngredientIds);

            var resultDto = _mapper.Map<PizzaDTO>(result);

            uow.Commit();

            return CreatedAtAction(nameof(Get), new {resultDto.Id}, resultDto);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            using var uow = _unitOfWorkFactory.Create();

            await _pizzaService.DeletePizzaAsync(id);

            uow.Commit();

            return NoContent();
        }

        [HttpPatch("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> Update(int id, [FromBody]UpdatePizzaDTO dto)
        {
            using var uow = _unitOfWorkFactory.Create();

            await _pizzaService.UpdatePizzaAsync(
                id,
                dto.Name,
                dto.Description,
                dto.UnitPrice,
                dto.IngredientIds);

            uow.Commit();

            return NoContent();
        }
    }
}