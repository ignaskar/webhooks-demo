using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TravelAgentWeb.Dtos
{
    public class FlightDetailUpdateDto
    {
        public string Publisher { get; set; }
        
        public string Secret { get; set; }
        
        public string FlightCode { get; set; }
        
        public decimal OldPricePerSeat { get; set; }
        
        public decimal NewPricePerSeat { get; set; }
        
        public string WebhookType { get; set; }
    }
}
