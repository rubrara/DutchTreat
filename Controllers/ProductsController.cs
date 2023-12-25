using DutchTreat.Data;
using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DutchTreat.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ProductsController : Controller
    {

        private readonly IDutchRepository _repository;

        public ProductsController(IDutchRepository dutchRepository)
        {
            _repository = dutchRepository;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Get()
        {
            return Ok(_repository.GetAllProducts());
        }
    }
}
