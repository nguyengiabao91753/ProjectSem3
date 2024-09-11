using Microsoft.AspNetCore.Mvc;
using ProjectSem3.DTOs;
using ProjectSem3.Services.BusTypeService;

namespace ProjectSem3.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BusTypeController : ControllerBase
{
    private readonly BusTypeService busTypeService;

    public BusTypeController(BusTypeService _busTypeService)
    {
        busTypeService = _busTypeService;
    }

    [HttpGet("get-all-bustype")]
    public IActionResult GetAll()
    {
        try
        {
            var result = busTypeService.GetAll();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPost("create-bustype")]
    public IActionResult Create([FromBody] BusTypeDTO busTypeDTO)
    {
        try
        {
            bool result = busTypeService.Create(busTypeDTO);

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
    [HttpPut("update-bustype")]
    public IActionResult Update([FromBody] BusTypeDTO busTypeDTO)
    {
        try
        {
            bool result = busTypeService.Update(busTypeDTO);

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
    [HttpPost("disable-bustype")]
    public IActionResult Disable(int id)
    {
        try
        {
            var result = busTypeService.Disable(id);

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
