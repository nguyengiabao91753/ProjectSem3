using Microsoft.AspNetCore.Mvc;
using ProjectSem3.DTOs;
using ProjectSem3.Services.BusesTripService;

namespace ProjectSem3.Controllers;
[Route("api/busestrip")]
[ApiController]
public class BusesTripController : Controller
{
    private BusesTripService busesTripService;
    public BusesTripController(BusesTripService busesTripService)
    {
        this.busesTripService = busesTripService;
    }

    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPost("create")]
    public IActionResult Create([FromBody] BusesTripDTO busesTripDTO)
    {
        try
        {
            bool result = busesTripService.Create(busesTripDTO);

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
    [HttpPut("update")]
    public IActionResult Update([FromBody] BusesTripDTO busesTripDTO)
    {
        try
        {
            bool result = busesTripService.Update(busesTripDTO);

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
            var result = busesTripService.GetAll();

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
    [HttpGet("getallactive")]
    public IActionResult GetAllActive()
    {
        try
        {
            var result = busesTripService.GetAllActive();

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
    [HttpPost("getbyid")]
    public IActionResult GetById([FromBody] int id)
    {
        try
        {
            var result = busesTripService.GetById(id);

            return Ok(result);

            throw new Exception();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
