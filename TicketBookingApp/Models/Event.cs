using System;
using System.Collections.Generic;

namespace TicketBookingApp.Models
{
    public partial class Event
    {
        public Event()
        {
            Buyer = new HashSet<Buyer>();
        }

        public int EventId { get; set; }
        public string Name { get; set; }
        public int TimeoutInSeconds { get; set; }

        public virtual ICollection<Buyer> Buyer { get; set; }
    }
}
