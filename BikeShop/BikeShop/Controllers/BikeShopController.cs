using BikeShop.BL.Interfaces;
using BikeShop.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BikeShop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BikeShopController : ControllerBase
    {
        private readonly IBikeService _bikeService;

        public BikeShopController(IBikeService bikeService)
        {
            _bikeService = bikeService;
        }

        [HttpGet("getAllBikes")]
        public List<Bike> GetAllBikes()
        {  
            return _bikeService.GetAllBikes();
        }
    }
}
