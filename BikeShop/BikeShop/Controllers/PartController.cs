using BikeShop.BL.Interfaces;
using BikeShop.BL.Services;
using BikeShop.DTO.DTO;
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllParts()
        {
            try
            {
                var result = _partService.GetAllParts();

                if (result == null)
                {
                    return Ok();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Cannot fetch parts. {ex.Message} | {ex.StackTrace}");
                return BadRequest();
            }


        }

        [HttpGet("getPartById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetPartById([FromQuery] string id)
        {
            if(string.IsNullOrEmpty(id))
            {
                return BadRequest(new { message = "Id cannot be null or empty." });
            } 
            else if(!Regex.IsMatch(id, "^[0-9a-f]{24}$"))
            {
                return BadRequest(new { message = "Id is not valid." });
            }

            try
            {
                var result = _partService.GetPartById(id);

                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Cannot fetch part by id {id}. {ex.Message} | {ex.StackTrace}");
                return BadRequest();
            }

        }

        [HttpPost("addNewPart")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddPart([FromBody] AddPartRequest part)
        {
            var partDto = _mapper.Map<Part>(part);

            try
            {
                var result = _partService.AddPart(partDto);

                if (result == null)
                {
                    return BadRequest(result);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Cannot add part. {ex.Message} | {ex.StackTrace}");
                return BadRequest();
            }
        }

        [HttpPut("updatePart")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdatePartById([FromQuery] string id, [FromBody] AddPartRequest part)
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
                var result = _partService.UpdatePartById(id, partDto);

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Cannot update part by id {id}. {ex.Message} | {ex.StackTrace}");
                return BadRequest();
            }
        }

        [HttpDelete("deletePart")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeletePartById([FromQuery] string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest(new { message = "Id can't be null or empty." });
            }
            else if (!Regex.IsMatch(id, "^[0-9a-f]{24}$"))
            {
                return BadRequest(new { message = "Id is not valid." });
            }


            _partService.DeletePartById(id);

            return Ok();
        }
    }
}
