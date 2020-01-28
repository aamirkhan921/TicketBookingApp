using System;
using System.Collections.Generic;

namespace TicketBookingApp.Models
{
    public partial class Buyer
    {
        public int BuyerId { get; set; }
        public int EventId { get; set; }
        public string TesterKey { get; set; }
        public string BuyerName { get; set; }

        public virtual Event Event { get; set; }
    }
}
