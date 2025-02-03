using BikeShop.BL.Interfaces;
using BikeShop.DTO.DTO;
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllBikes()
        {
            try
            {
                var result = _bikeService.GetAllBikes();

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Cannot fetch bikes. {ex.Message} | {ex.StackTrace}");
                return BadRequest();
            }
        }

        [HttpGet("getBikeById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetBikeById([FromQuery] string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest(new { message = "Id can't be null or empty." });
            }
            else if (!Regex.IsMatch(id, "^[0-9a-f]{24}$"))
            {
                return BadRequest(new { message = "Id is not valid." });
            }
            

            try
            {
                var result = _bikeService.GetBikeById(id);

                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Cannot fetch bike by {id}. {ex.Message} | {ex.StackTrace}");
                return BadRequest();
            }
        }

        [HttpPost("addBike")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddBike([FromBody] AddBikeRequest bike)
        {
            var bikeDto = _mapper.Map<Bike>(bike);

            try
            {
                var result = _bikeService.AddBike(bikeDto);

                if (result == null)
                {
                    return BadRequest(result);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Cannot add bike. {ex.Message} | {ex.StackTrace}");
                return BadRequest();
            }
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
            else if (!Regex.IsMatch(id, "^[0-9a-f]{24}$"))
            {
                return BadRequest(new { message = "Id is not valid." });
            }

            var bikeDto = _mapper.Map<Bike>(bike);

            try
            {
                var result = _bikeService.UpdateBikeById(id, bikeDto);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Cannot update bike by id {id}. {ex.Message} | {ex.StackTrace}");
                return BadRequest();
            }
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
            else if (!Regex.IsMatch(id, "^[0-9a-f]{24}$"))
            {
                return BadRequest(new { message = "Id is not valid." });
            }


            try
            {
                _bikeService.DeleteBikeById(id);

                return Ok();
            } 
            catch (Exception ex)
            {
                _logger.LogError($"Cannot delete bike by id {id}. {ex.Message} | {ex.StackTrace}");
                return BadRequest();
            }
            
        }
    }
}
