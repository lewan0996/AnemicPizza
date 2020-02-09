using System.Threading.Tasks;
using AnemicPizza.API.DTO.Basket;
using AnemicPizza.Core;
using AnemicPizza.Core.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
#pragma warning disable 1591

namespace AnemicPizza.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public BasketController(IBasketService basketService, IMapper mapper, IUnitOfWorkFactory unitOfWorkFactory)
        {
            _basketService = basketService;
            _mapper = mapper;
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var basketId = GetBasketId();

            if (basketId == null)
            {
                return NotFound();
            }

            var basket = await _basketService.GetBasketAsync(basketId.Value);

            var basketDTO = _mapper.Map<BasketDTO>(basket);

            return Ok(basketDTO);
        }

        /// <summary>
        /// Adds an item to the current user's basket
        /// </summary>
        [HttpPost("Items")]
        public async Task<ActionResult> AddItemToBasket(AddItemToBasketDTO dto)
        {
            var basketId = GetBasketId();

            using var uow = _unitOfWorkFactory.Create();

            var basket = await _basketService.AddItemToBasketAsync(basketId, dto.ProductId, dto.Quantity);

            uow.Commit();

            if (basketId == null)
            {
                CreateBasketCookie(basket.Id);
            }

            var basketDTO = _mapper.Map<BasketDTO>(basket);

            return CreatedAtAction(nameof(Get), basketDTO);
        }

        /// <summary>
        /// Creates a new order based on customer's basket
        /// </summary>
        /// <param name="dto">Checkout data</param>
        [HttpPost("Checkout")]
        public async Task<IActionResult> Checkout(CheckoutDTO dto)
        {
            var basketId = GetBasketId();

            if (basketId == null)
            {
                return BadRequest();
            }

            using var uow = _unitOfWorkFactory.Create();

            await _basketService.CheckoutAsync(
                basketId.Value,
                dto.FirstName,
                dto.LastName,
                dto.EmailAddress,
                dto.PhoneNumber,
                dto.City,
                dto.AddressLine1,
                dto.AddressLine2,
                dto.ZipCode);

            uow.Commit();

            return NoContent();
        }

        private int? GetBasketId()
        {
            var stringValueFromCookie = Request.Cookies["BasketId"];
            if (stringValueFromCookie == null)
            {
                return null;
            }

            return int.Parse(stringValueFromCookie);
        }

        private void CreateBasketCookie(int basketId)
        {
            Response.Cookies.Append("BasketId", basketId.ToString());
        }
    }
}