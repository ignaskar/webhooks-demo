﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirlineSendAgent.Models;
using Microsoft.EntityFrameworkCore;

namespace AirlineSendAgent.Data
{
    public class SendAgentDbContext : DbContext
    {
        public SendAgentDbContext(DbContextOptions<SendAgentDbContext> options) : base(options)
        {
            
        }

        public DbSet<WebhookSubscription> WebhookSubscriptions { get; set; }
    }
}
