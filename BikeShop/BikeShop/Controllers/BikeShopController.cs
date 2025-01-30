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
        public IActionResult GetAllBikes()
        {
            var result = _bikeService.GetAllBikes();

            if (result == null)
            {
                return Ok();
            }

            return Ok(result);
        }

        [HttpGet("getBikeById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetBikeById([FromQuery] string id)
        {
            if(string.IsNullOrEmpty(id)) 
            {
                return BadRequest(new { message = "Id can't be null or empty."});
            }

            var result = _bikeService.GetBikeById(id);

            if(result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost("addBike")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddBike([FromBody] AddBikeRequest bike)
        {
            var bikeDto = _mapper.Map<Bike>(bike);

            var result = _bikeService.AddBike(bikeDto);

            if(result == null)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPut("updateBike")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateBikeById([FromQuery] string id, [FromBody] AddBikeRequest bike)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest(new { message = "Id can't be null or empty." });
            }

            var bikeDto = _mapper.Map<Bike>(bike);

            var result = _bikeService.UpdateBikeById(id, bikeDto);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpDelete("deleteBike")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteBikeById([FromQuery] string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest(new { message = "Id can't be null or empty." });
            }

            _bikeService.DeleteBikeById(id);

            return Ok();
        }
    }
}
