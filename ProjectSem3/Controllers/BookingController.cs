﻿using Microsoft.AspNetCore.Mvc;
using ProjectSem3.DTOs;
using ProjectSem3.Services.BookingService;

namespace ProjectSem3.Controllers;
[Route("api/booking")]
[ApiController]
public class BookingController : Controller
{
    private BookingService bookingService;
    public BookingController(BookingService bookingService)
    {
        this.bookingService = bookingService;
    }
    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPost("create")]
    public IActionResult Create([FromBody] BookingRequest bookingRequest)
    {
        try
        {
            bool result = bookingService.Create(bookingRequest.BookingDTO, bookingRequest.BookingDetailDTOs);

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

    [HttpGet("get-by-booking-id/{id}")]
    public IActionResult GetByBookingId(int id)
    {
        try
        {
            var result = bookingService.GetBookingById(id);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}

public class BookingRequest
{
    public BookingDTO BookingDTO { get; set; }
    public List<BookingDetailDTO> BookingDetailDTOs { get; set; }
}
