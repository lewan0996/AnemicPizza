using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnemicPizza.API.DTO.Orders;
using AnemicPizza.Core;
using AnemicPizza.Core.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AnemicPizza.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryController : ControllerBase
    {
        private readonly IDeliveryService _deliveryService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public DeliveryController(IDeliveryService deliveryService, IMapper mapper,
            IUnitOfWorkFactory unitOfWorkFactory)
        {
            _deliveryService = deliveryService;
            _mapper = mapper;
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        [HttpGet("orders/{supplierId:int}")]
        public async Task<IActionResult> GetSupplierOrders(int supplierId)
        {
            var supplierOrders = await _deliveryService.GetSupplierOrdersAsync(supplierId);

            var supplierOrderDTOs = supplierOrders
                .Select(o => _mapper.Map<OrderDTO>(o));

            return Ok(supplierOrderDTOs);
        }

        [HttpPatch("order/{orderId:int}/finish")]
        public async Task<IActionResult> FinishOrderDelivery(int orderId)
        {
            using var uow = _unitOfWorkFactory.Create();

            await _deliveryService.FinishOrderDeliveryAsync(orderId);

            uow.Commit();

            return NoContent();
        }
    }
}