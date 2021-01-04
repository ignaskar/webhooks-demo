﻿namespace AirlineSendAgent.Dtos
{
    public class FlightDetailChangePayloadDto
    {
        public string WebhookUri { get; set; }
        
        public string Publisher { get; set; }
        
        public string Secret { get; set; }
        
        public string FlightCode { get; set; }
        
        public decimal OldPricePerSeat { get; set; }
        
        public decimal NewPricePerSeat { get; set; }
        
        public string WebhookType { get; set; }
    }
}
