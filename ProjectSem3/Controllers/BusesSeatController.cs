using Microsoft.AspNetCore.Mvc;
using ProjectSem3.DTOs;
using ProjectSem3.Services.BusesSeatService;

namespace ProjectSem3.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BusesSeatController : ControllerBase
{
    private readonly BusesSeatService busesSeatService;

    public BusesSeatController(BusesSeatService _busesSeatService)
    {
        busesSeatService = _busesSeatService;
    }

    [HttpGet("get-all-buses-seat")]
    public IActionResult GetAll()
    {
        try
        {
            var result = busesSeatService.GetAll();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Produces("application/json")]
    [HttpGet("find-seats-by-busId/{busId}")]
    public IActionResult FindSeatsByBusId(int busId)
    {
        try
        {
            var result = busesSeatService.GetSeatsByBusId(busId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    //[Consumes("application/json")]
    //[Produces("application/json")]
    //[HttpPost("create-buses-seat")]
    //public IActionResult Create([FromBody] BusesSeatDTO busesSeatDTO)
    //{
    //    try
    //    {
    //        bool result = busesSeatService.Create(busesSeatDTO);
    //        return Ok(new
    //        {
    //            status = result
    //        });
    //    }
    //    catch (Exception ex)
    //    {
    //        return BadRequest(ex.Message);
    //    }
    //}

    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPut("update-buses-seat")]
    public IActionResult Update([FromBody] BusesSeatDTO busesSeatDTO)
    {
        try
        {
            bool result = busesSeatService.Update(busesSeatDTO);
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
    [HttpGet("count-seat-remaining")]
    public IActionResult CountSeatReamaining(int id)
    {
        try
        {
            int result = busesSeatService.CountSeatRemain(id);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPost("disable-buses-seat")]
    public IActionResult Disable(int id)
    {
        try
        {
            bool result = busesSeatService.Disable(id);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
