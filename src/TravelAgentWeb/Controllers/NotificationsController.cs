using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TravelAgentWeb.Data;
using TravelAgentWeb.Dtos;

namespace TravelAgentWeb.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class NotificationsController : ControllerBase
    {
        private readonly TravelAgentDbContext _context;

        public NotificationsController(TravelAgentDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult FlightChanged(FlightDetailUpdateDto updatedFlight)
        {
            Console.WriteLine($"Webhook Received from {updatedFlight.Publisher}");

            var secretModel = _context.SubscriptionSecrets.FirstOrDefault(s => 
                s.Secret == updatedFlight.Secret &&
                s.Publisher == updatedFlight.Publisher);

            if (secretModel == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid secret - Ignore Webhook");
                Console.ResetColor();
                return Ok();
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Valid Webhook!");
            Console.WriteLine($"Old Price: {updatedFlight.OldPricePerSeat}, New Price: {updatedFlight.NewPricePerSeat}");
            Console.ResetColor();
            return Ok();
        }
    }
}
