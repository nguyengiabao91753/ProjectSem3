using Microsoft.AspNetCore.Mvc;
using ProjectSem3.DTOs;
using ProjectSem3.Services.TripService;

namespace ProjectSem3.Controllers;
[Route("api/trip")]
[ApiController]
public class TripController : Controller
{
    private TripService tripService;

    public TripController(TripService tripService)
    {
        this.tripService = tripService;
    }
    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPost("create")]
    public IActionResult Create([FromBody] TripDTO tripDTO)
    {
        try
        {
            bool result = tripService.Create(tripDTO);

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
    [HttpGet("getall")]
    public IActionResult GetAll()
    {
        try
        {
            var result = tripService.GetAll();

            return Ok(result);



            throw new Exception();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpGet("trips-with-locations")]
    public ActionResult<List<TripDTO>> GetTripsWithLocations()
    {
        var trips = tripService.GetTripsWithLocationNames();
        return Ok(trips);
    }

    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPost("delete")]
    public IActionResult Delete(int id)
    {
        try
        {
            var result = tripService.Delete(id);

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
    public IActionResult Update([FromBody] TripDTO tripDTO)
    {
        try
        {
            bool result = tripService.Update(tripDTO);

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
