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
    public class PartController : ControllerBase
    {
        private readonly IPartService _partService;
        private readonly IMapper _mapper;
        private readonly ILogger<PartController> _logger;

        public PartController(IPartService partService, IMapper mapper, ILogger<PartController> logger)
        {
            _partService = partService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("getAllParts")]
        public async Task<IActionResult> GetAllParts()
        {
            try
            {
                var result = await _partService.GetAllParts();

                if (result == null || result.Count == 0)
                {
                    return NoContent();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Cannot fetch parts.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("getPartById")]
        public async Task<IActionResult> GetPartById([FromQuery] string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest(new { message = "Id cannot be null or empty." });
            }
            else if (!Regex.IsMatch(id, "^[0-9a-f]{24}$"))
            {
                return BadRequest(new { message = "Id is not valid." });
            }

            try
            {
                var result = await _partService.GetPartById(id);

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Cannot fetch part by id {id}.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("addNewPart")]
        public async Task<IActionResult> AddPart([FromBody] AddPartRequest part)
        {
            var partDto = _mapper.Map<Part>(part);

            try
            {
                var result = await _partService.AddPart(partDto);

                if (result == null)
                {
                    return BadRequest("Failed to add part.");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Cannot add part.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("updatePart")]
        public async Task<IActionResult> UpdatePartById([FromQuery] string id, [FromBody] AddPartRequest part)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest(new { message = "Id can't be null or empty." });
            }
            else if (!Regex.IsMatch(id, "^[0-9a-f]{24}$"))
            {
                return BadRequest(new { message = "Id is not valid." });
            }

            var partDto = _mapper.Map<Part>(part);

            try
            {
                var result = await _partService.UpdatePartById(id, partDto);

                if (result == null)
                {
                    return NotFound("Part not found to update.");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Cannot update part by id {id}.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("deletePart")]
        public async Task<IActionResult> DeletePartById([FromQuery] string id)
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
                await _partService.DeletePartById(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Cannot delete part by id {id}.");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
