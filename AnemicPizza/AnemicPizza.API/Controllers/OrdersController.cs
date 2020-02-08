using System.Linq;
using System.Threading.Tasks;
using AnemicPizza.API.DTO.Orders;
using AnemicPizza.Core;
using AnemicPizza.Core.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AnemicPizza.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderingService _orderingService;
        private readonly IDeliveryService _deliveryService;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IMapper _mapper;

        public OrdersController(IOrderingService orderingService, IDeliveryService deliveryService, IMapper mapper, IUnitOfWorkFactory unitOfWorkFactory)
        {
            _orderingService = orderingService;
            _deliveryService = deliveryService;
            _mapper = mapper;
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> GetOrdersByEmail(string email)
        {
            var orders = await _orderingService.GetOrdersByUserEmailAsync(email);

            if (!orders.Any())
            {
                return NotFound();
            }

            var orderDTOs = orders
                .Select(o => _mapper.Map<OrderDTO>(o));

            return Ok(orderDTOs);
        }

        [HttpPost("ship/{id}")]
        public async Task<IActionResult> ShipOrder(int id)
        {
            using var uow = _unitOfWorkFactory.Create();

            await _deliveryService.StartDeliveryAsync(id);

            uow.Commit();

            return NoContent();
        }
    }
}