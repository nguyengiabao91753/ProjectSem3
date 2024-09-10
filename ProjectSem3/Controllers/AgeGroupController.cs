using Microsoft.AspNetCore.Mvc;
using ProjectSem3.DTOs;
using ProjectSem3.Services.AgeGroupService;

namespace ProjectSem3.Controllers;
[Route("api/agegroup")]
[ApiController]
public class AgeGroupController : Controller
{
    private AgeGroupService ageGroupService;

    public AgeGroupController(AgeGroupService ageGroupService)
    {
        this.ageGroupService = ageGroupService;
    }

    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPost("create")]
    public IActionResult Create([FromBody] AgeGroupDTO ageGroupDTO)
    {
        try
        {
            bool result = ageGroupService.Create(ageGroupDTO);

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
    public IActionResult Update([FromBody] AgeGroupDTO ageGroupDTO)
    {
        try
        {
            bool result = ageGroupService.Update(ageGroupDTO);

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
            var result = ageGroupService.GetAll();

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
            var result = ageGroupService.Delete(id);

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
