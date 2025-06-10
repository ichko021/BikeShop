using BikeShop.BL.Interfaces;
using BikeShop.DTO.POCO;
using BikeShop.DTO.Requests;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

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
        public async Task<IActionResult> GetAllBikes()
        {
            try
            {
                var result = await _bikeService.GetAllBikes();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Cannot fetch bikes. {ex.Message} | {ex.StackTrace}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("getLocations")]
        public async Task<IActionResult> GetLocations()
        {
            try
            {
                var result = await _bikeService.GetLocations();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Cannot fetch locations. {ex.Message} | {ex.StackTrace}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("getBikeById")]
        public async Task<IActionResult> GetBikeById([FromQuery] string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest(new { message = "Id can't be null or empty." });

            if (!Regex.IsMatch(id, "^[0-9a-f]{24}$"))
                return BadRequest(new { message = "Id is not valid." });

            try
            {
                var result = await _bikeService.GetBikeById(id);
                if (result == null)
                    return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Cannot fetch bike by id {id}. {ex.Message} | {ex.StackTrace}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("addBike")]
        public async Task<IActionResult> AddBike([FromBody] AddBikeRequest bike)
        {
            var bikeDto = _mapper.Map<Bike>(bike);

            try
            {
                var result = await _bikeService.AddBike(bikeDto);

                if (result == null)
                    return BadRequest("Failed to add bike");

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Cannot add bike. {ex.Message} | {ex.StackTrace}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("updateBike")]
        public async Task<IActionResult> UpdateBikeById([FromQuery] string id, [FromBody] AddBikeRequest bike)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest(new { message = "Id can't be null or empty." });

            if (!Regex.IsMatch(id, "^[0-9a-f]{24}$"))
                return BadRequest(new { message = "Id is not valid." });

            var bikeDto = _mapper.Map<Bike>(bike);

            try
            {
                var result = await _bikeService.UpdateBikeById(id, bikeDto);

                if (result == null)
                    return NotFound("Bike not found to update");

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Cannot update bike by id {id}. {ex.Message} | {ex.StackTrace}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("deleteBike")]
        public async Task<IActionResult> DeleteBikeById([FromQuery] string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest(new { message = "Id can't be null or empty." });

            if (!Regex.IsMatch(id, "^[0-9a-f]{24}$"))
                return BadRequest(new { message = "Id is not valid." });

            try
            {
                await _bikeService.DeleteBikeById(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Cannot delete bike by id {id}. {ex.Message} | {ex.StackTrace}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
