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

    [HttpGet("get-all-bus-type")]
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

    [HttpGet("check-bus-type-name-exist/{name}")]
    public IActionResult CheckBusTypeNameExist(string name)
    {
        try
        {
            var result = busTypeService.FindByName(name);
            if (result != null) {

                return Ok(new
                {
                    exist = true
                });
            }
            return Ok(new
            {
                exist = false
            });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPost("create-bus-type")]
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
    [HttpPost("update-bus-type")]
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
    [HttpPost("disable-bus-type")]
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
