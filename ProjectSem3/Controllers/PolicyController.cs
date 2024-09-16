using Microsoft.AspNetCore.Mvc;
using ProjectSem3.DTOs;
using ProjectSem3.Services.AgeGroupService;
using ProjectSem3.Services.PolicyService;

namespace ProjectSem3.Controllers;
[Route("api/policy")]
[ApiController]
public class PolicyController : Controller
{
    private PolicyService policyService;
    public PolicyController(PolicyService policyService)
    {
        this.policyService = policyService;
    }

    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPost("create")]
    public IActionResult Create([FromBody] PolicyDTO policyDTO)
    {
        try
        {
            bool result = policyService.create(policyDTO);

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
    public IActionResult Update([FromBody] PolicyDTO policyDTO)
    {
        try
        {
            bool result = policyService.update(policyDTO);

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
            var result = policyService.getall();

            return Ok(result);



            throw new Exception();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
