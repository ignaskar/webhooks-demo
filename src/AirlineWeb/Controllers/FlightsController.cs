using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AirlineWeb.Data;
using AirlineWeb.Dtos;
using AirlineWeb.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AirlineWeb.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class FlightsController : ControllerBase
    {
        private readonly AirlineDbContext _context;
        private readonly IMapper _mapper;

        public FlightsController(AirlineDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("{flightCode}", Name = "GetFlightDetailsByFlightCode")]
        public ActionResult<FlightDetailReadDto> GetFlightDetailsById(string flightCode)
        {
            var flight = _context.FlightDetails.FirstOrDefault(f => f.FlightCode == flightCode);

            if (flight == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<FlightDetailReadDto>(flight));
        }

        [HttpPost]
        public ActionResult<FlightDetailReadDto> CreateFlight(FlightDetailCreateDto flightToCreate)
        {
            var flight = _context.FlightDetails.FirstOrDefault(f => f.FlightCode == flightToCreate.FlightCode);

            if (flight != null)
            {
                return NoContent();
            }

            flight = _mapper.Map<FlightDetail>(flightToCreate);

            try
            {
                _context.FlightDetails.Add(flight);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            var flightToReturn = _mapper.Map<FlightDetailReadDto>(flight);

            return CreatedAtRoute("GetFlightDetailsByFlightCode", new {flightCode = flightToReturn.FlightCode}, flightToReturn);
        }

        [HttpPut("{flightCode}")]
        public IActionResult UpdateFlightDetail(string flightCode, FlightDetailUpdateDto flightToUpdate)
        {
            var flight = _context.FlightDetails.FirstOrDefault(f => f.FlightCode == flightCode);

            if (flight == null)
            {
                return NotFound();
            }

            _mapper.Map(flightToUpdate, flight);

            _context.SaveChanges();

            return NoContent();
        }

    }
}
