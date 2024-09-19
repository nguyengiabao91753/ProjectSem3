using Microsoft.AspNetCore.Mvc;
using ProjectSem3.DTOs;
using ProjectSem3.Services.BusService;

namespace ProjectSem3.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BusController : ControllerBase
{
    private readonly BusService busService;

    public BusController(BusService _busService)
    {
        busService = _busService;
    }

    [HttpGet("get-all-bus")]
    public IActionResult GetAll()
    {
        try
        {
            var result = busService.GetAll();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("check-license-plate-exists")]
    public IActionResult CheckLicensePlateExists([FromQuery] string licensePlate)
    {
        try
        {
            var exists = busService.checkLicensePlateExists(licensePlate);
            return Ok(new { exists });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);

        }
    }

    [Produces("application/json")]
    [HttpGet("find-by-id/{id}")]
    public IActionResult FindById(int id)
    {
        try
        {
            var result = busService.findById(id);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPost("create-bus")]
    public IActionResult Create([FromBody] BusDTO busDTO)
    {
        try
        {
            bool result = busService.Create(busDTO);
            return Ok(new
            {
                status = result
            });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPost("update-bus")]
    public IActionResult Update([FromBody] BusDTO busDTO)
    {
        try
        {
            bool result = busService.Update(busDTO);
            return Ok(new
            {
                status = result
            });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPost("disable-bus")]
    public IActionResult Disable(int id)
    {
        try
        {
            var result = busService.Disable(id);

            return Ok(new
            {
                status = result
            });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
