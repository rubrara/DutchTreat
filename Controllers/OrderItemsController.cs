using AutoMapper;
using DutchTreat.Data;
using DutchTreat.Data.Entities;
using DutchTreat.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DutchTreat.Controllers
{
    [Route("/api/orders/{orderId}/items")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme) ]
    public class OrderItemsController : Controller
    {
        private readonly IDutchRepository repository;
        private readonly ILogger<OrderItemsController> logger;
        private readonly IMapper mapper;

        public OrderItemsController(IDutchRepository repository, ILogger<OrderItemsController> logger, IMapper mapper)
        {
            this.repository = repository;
            this.logger = logger;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get(Guid orderId) {
            var order = repository.GetOrderById(User.Identity.Name, orderId);

            var res = mapper.Map<IEnumerable<OrderItem>, IEnumerable<OrderItemViewModel>>(order.Items);

            if (res != null)
            {
                return Ok(res);
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid orderId, Guid id)
        {
            var item = repository.GetOrderById(User.Identity.Name, orderId).Items.First(item => item.Id == id);

            var res = mapper.Map<OrderItem, OrderItemViewModel>(item);

            if (res != null)
            {
                return Ok(res);
            }
            return NotFound();
        }
    }
}
