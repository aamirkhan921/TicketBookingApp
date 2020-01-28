using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketBookingApp.Models;

namespace TicketBookingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SSEController : ControllerBase
    {
        [HttpGet]
        public async Task Get()
        {
            Response.Headers.Add("Content-Type", "text/event-stream");

            Microsoft.AspNetCore.Http.Headers.RequestHeaders headers = Request.GetTypedHeaders();
            bool returnEmpty = true;
            if (headers.Referer.OriginalString.Contains("TicketBuy", StringComparison.InvariantCultureIgnoreCase))
            {
                int eventId = 0;
                if (Int32.TryParse(headers.Referer.Segments[headers.Referer.Segments.Length - 1], out eventId))
                {
                    if (eventId > 0)
                    {
                        bool isTicketBooked = false;

                        using (var db = new dbInterviewContext())
                        {
                            for (int i = 0; i < 4; i++)
                            {
                                await Task.Delay(TimeSpan.FromSeconds(2));
                                bool IsBuy = db.Buyer.Any(p => p.EventId == eventId);
                                if (IsBuy == true)
                                {
                                    //ticket booked

                                    returnEmpty = false;
                                    string dataItem = $"data: {"TICKET_BOOKED"}\n\n";

                                    byte[] dataItemBytes = ASCIIEncoding.ASCII.GetBytes(dataItem);
                                    await Response.Body.WriteAsync(dataItemBytes, 0, dataItemBytes.Length);
                                    await Response.Body.FlushAsync();

                                    break;
                                }
                            }

                        }

                    }
                }

            }

            if (returnEmpty)
            {
                string dataItem = $"data: {""}\n\n";

                byte[] dataItemBytes = ASCIIEncoding.ASCII.GetBytes(dataItem);
                await Response.Body.WriteAsync(dataItemBytes, 0, dataItemBytes.Length);
                await Response.Body.FlushAsync();
            }

        }
    }
}