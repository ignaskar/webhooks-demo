using System;
using System.Collections.Generic;
using System.Linq;
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
    public class WebhookSubscriptionController : ControllerBase
    {
        private readonly AirlineDbContext _context;
        private readonly IMapper _mapper;

        public WebhookSubscriptionController(AirlineDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("{id}", Name = "GetSubscriptionById")]
        public ActionResult<WebhookSubscriptionReadDto> GetSubscriptionById(int id)
        {
            var subscription = _context.WebhookSubscriptions.FirstOrDefault(s => s.Id == id);

            if (subscription == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<WebhookSubscriptionReadDto>(subscription));
        }
        
        [HttpPost]
        public ActionResult<WebhookSubscriptionReadDto> CreateSubscription(WebhookSubscriptionCreateDto webhookToCreate)
        {
            var subscription = _context.WebhookSubscriptions.FirstOrDefault(s => s.WebhookUri == webhookToCreate.WebhookUri);

            if (subscription != null)
            {
                return NoContent();
            }

            subscription = _mapper.Map<WebhookSubscription>(webhookToCreate);

            subscription.Secret = Guid.NewGuid().ToString();
            subscription.WebhookPublisher = "PanAus";

            try
            {
                _context.WebhookSubscriptions.Add(subscription);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            var subscriptionToReturn = _mapper.Map<WebhookSubscriptionReadDto>(subscription);

            return CreatedAtRoute("GetSubscriptionById", new {id = subscriptionToReturn.Id}, subscriptionToReturn);
        }
    }
}
