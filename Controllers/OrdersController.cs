using AutoMapper;
using DutchTreat.Data;
using DutchTreat.Data.Entities;
using DutchTreat.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DutchTreat.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrdersController : Controller
    {
        private readonly IDutchRepository _repository;
        private readonly ILogger<OrdersController> _logger;

        private readonly IMapper _mapper;
        private readonly UserManager<StoreUser> userManager;

        public OrdersController(IDutchRepository dutchRepository, 
            ILogger<OrdersController> logger,
            IMapper mapper,
            UserManager<StoreUser> userManager
            )
        {
            _repository = dutchRepository;
            _logger = logger;
            _mapper = mapper;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult Get(bool includeItems = true)
        {
            try
            {
                var username = User.Identity.Name;

                _logger.LogInformation("Vlegovme vo Get All Orders!");
                var orders = _repository.GetAllOrdersByUser(username, includeItems);

                var res = _mapper.Map<IEnumerable<OrderViewModel>>(orders);

                return Ok(res);

            } catch(Exception ex)
            {
                _logger.LogError($"Failed to get orders: {ex}");
                return BadRequest("Failed to get orders");
            }
        }

        [HttpGet("details")]
        public IActionResult GetDetails()
        {
            return Ok("TEST!");

        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                var order = _repository.GetOrderById(User.Identity.Name, id);

                var res = _mapper.Map<Order, OrderViewModel>(order);

                if (res != null)
                    return Ok(res);
                return NotFound("The Order was not found in the database, try other id");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get orders: {ex}");
                return BadRequest("Failed to get orders");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrderViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newOrder = _mapper.Map<Order>(model);

                    if (newOrder.OrderDate == DateTime.MinValue)
                    {
                        newOrder.OrderDate = DateTime.Now;
                    }
                    var currentUser = await userManager.FindByNameAsync(User.Identity.Name);

                    newOrder.User = currentUser;

                    _repository.AddEntity(newOrder);

                    if (_repository.SaveAll())
                    {
                        var vm = _mapper.Map<OrderViewModel>(newOrder);

                        return Created($"/api/orders/{vm.OrderId}", vm);
                    }
                }

                    return BadRequest(ModelState);
                
            } catch(Exception ex)
            {
                _logger.LogError($"\n" +
                    $"------------------------------------" +
                    $"\nFailed to save a new order: {ex}\n" +
                    $"------------------------------------\n");

                return BadRequest("Failed to create new order");
            }



        }


    }
}
