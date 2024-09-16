using Microsoft.AspNetCore.Mvc;
using ProjectSem3.DTOs;
using ProjectSem3.Services.LocationService;

namespace ProjectSem3.Controllers;
[Route("api/location")]
[ApiController]
public class LocationController : Controller
{
    private LocationService locationService;

    public LocationController(LocationService locationService)
    {
        this.locationService = locationService;
    }

    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPost("create")]
    public IActionResult Create([FromBody] LocationDTO locationDTO)
    {
        try
        {
            bool result = locationService.Create(locationDTO);

            return Ok(new
            {
                status = result
            }
            );



            throw new Exception();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpGet("getall")]
    public IActionResult GetAll()
    {
        try
        {
            var result = locationService.GetAll();

            return Ok(result);



            throw new Exception();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPost("delete")]
    public IActionResult Delete(int id)
    {
        try
        {
            var result = locationService.Delete(id);

            return Ok(new
            {
                status = result
            }
             );



            throw new Exception();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPost("update")]
    public IActionResult Update([FromBody] LocationDTO locationDTO)
    {
        try
        {
            bool result = locationService.Update(locationDTO);

            return Ok(new
            {
                status = result
            }
            );



            throw new Exception();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
