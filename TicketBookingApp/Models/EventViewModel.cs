using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TicketBookingApp.Models
{
    public class EventViewModel
    {
        public int EventId { get; set; }
        [Required(ErrorMessage = "Buyer Name is requried")]
        [DisplayName("Buyer Name")]
        public string BuyerName { get; set; }
        public int TimeoutInSeconds { get; set; }
        public bool IsTickedAlreadyBought { get; set; }
    }
}
