﻿using Microsoft.AspNetCore.Mvc;
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

    [Produces("application/json")]
    [HttpGet("find-by-id/{id}")]
    public IActionResult FindById(int id)
    {
        try
        {
            var result = tripService.findById(id);
            return Ok(result);
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

    /*    [HttpGet("check-duplicate-trip")]
        public IActionResult checkDuplicateTrip([FromQuery] int departureLocationId, [FromQuery] int arrivalLocationId, [FromQuery] DateTime dateStart)
        {
            try
            {
                // Gọi phương thức CheckTripsAsync để kiểm tra xem chuyến đi đã tồn tại hay chưa
                var exists = tripService.checkDuplicateTrip(departureLocationId, arrivalLocationId, dateStart);
                return Ok(new { exists });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }*/
}
