using System;

namespace AirlineSendAgent.Dtos
{
    public class NotificationMessageDto
    {
        public string Id { get; }

        public string WebhookType { get; set; }

        public string FlightCode { get; set; }

        public decimal OldPricePerSeat { get; set; }

        public decimal NewPricePerSeat { get; set; }
    }
}