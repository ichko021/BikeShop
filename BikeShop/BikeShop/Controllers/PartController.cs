using BikeShop.BL.Interfaces;
using BikeShop.DTO.DTO;
using BikeShop.DTO.Requests;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;

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
        public List<Part> GetAllParts()
        {
            return _partService.GetAllParts();
        }

        [HttpGet("getPartById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Part? GetPartById([FromQuery] string id)
        {
            return _partService.GetPartById(id);
        }

        [HttpPost("addNewPart")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddPart([FromBody] AddPartRequest part)
        {
            try
            {
                var partDto = _mapper.Map<Part>(part);

                _partService.AddPart(partDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error adding part. {ex.Message} | {ex.StackTrace}");
            }

            return Ok();
        }

        [HttpPut("updatePart")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdatePartById([FromQuery] string id, [FromBody] AddPartRequest part)
        {
            try
            {
                var partDto = _mapper.Map<Part>(part);

                _partService.UpdatePartById(id, partDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating part {ex.Message} | {ex.StackTrace}");
            }

            return Ok();
        }

        [HttpDelete("deletePart")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeletePartById([FromQuery] string id)
        {
            try
            {
                _partService.DeletePartById(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting part {ex.Message} | {ex.StackTrace}");
            }

            return Ok();
        }
    }
}
