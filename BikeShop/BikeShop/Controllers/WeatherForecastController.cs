using BikeShop.BL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BikeShop.Controllers
{
    [ApiController]
    [Route("api/bike")]
    public class BikeShopController : ControllerBase
    {
        private readonly ILogger<BikeShopController> _logger;
        private IBikeService _bikeService;

        public BikeShopController(ILogger<BikeShopController> logger, IBikeService bikeService)
        {
            _logger = logger;
            _bikeService = bikeService;
        }

        [HttpGet("getAllBikes")]
        public IActionResult GetAllBikes()
        {  
            return Ok(_bikeService.GetAllBikes());
        }
    }
}
