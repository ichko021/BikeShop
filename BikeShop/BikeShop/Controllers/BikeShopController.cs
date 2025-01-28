using BikeShop.BL.Interfaces;
using BikeShop.DTO.DTO;
using BikeShop.DTO.Requests;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;

namespace BikeShop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BikeShopController : ControllerBase
    {
        private readonly IBikeService _bikeService;
        private readonly IMapper _mapper;
        private readonly ILogger<BikeShopController> _logger;

        public BikeShopController(IBikeService bikeService, IMapper mapper, ILogger<BikeShopController> logger)
        {
            _bikeService = bikeService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("getAllBikes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public List<Bike> GetAllBikes()
        {
            return _bikeService.GetAllBikes();
        }

        [HttpGet("getBikeById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Bike? GetBikeById([FromQuery] string id)
        {
            return _bikeService.GetBikeById(id);
        }

        [HttpPost("addNewBike")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddBike([FromBody] AddBikeRequest bike)
        {
            try
            {
                var bikeDto = _mapper.Map<Bike>(bike);

                _bikeService.AddBike(bikeDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error adding bike {ex.Message} | {ex.StackTrace}");
            }

            return Ok();
        }

        [HttpPut("updateBike")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateBikeById([FromQuery] string id, [FromBody] AddBikeRequest bike)
        {
            try
            {
                var bikeDto = _mapper.Map<Bike>(bike);

                _bikeService.UpdateBikeById(id, bikeDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating bike {ex.Message} | {ex.StackTrace}");
            }

            return Ok();
        }

        [HttpDelete("deleteBike")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteBikeById([FromQuery] string id)
        {
            try
            { 
                _bikeService.DeleteBikeById(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting bike {ex.Message} | {ex.StackTrace}");
            }

            return Ok();
        }
    }
}
