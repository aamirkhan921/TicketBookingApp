using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TicketBookingApp.Models;


namespace TicketBookingApp.Controllers
{
    public class TicketBookingController : Controller
    {
        public TicketBookingController()
        {

        }

        public IActionResult Index()
        {
            using (var db = new dbInterviewContext())
            {
                List<Event> events = db.Event.Where(p => p.Buyer.FirstOrDefault() == null).ToList();
                ViewBag.Message = TempData["Message"];
                TempData["Message"] = "";
                return View(events);
            }
        }

        public IActionResult TicketBuy(int Id)
        {
            if (Id > 0)
            {
                using (var db = new dbInterviewContext())
                {
                    EventViewModel eventViewModel = db.Event.Where(p => p.EventId == Id).Select(p => new EventViewModel()
                    {
                        EventId = p.EventId,
                        TimeoutInSeconds = p.TimeoutInSeconds,
                        IsTickedAlreadyBought = p.Buyer.FirstOrDefault() == null ? false : true
                    }).FirstOrDefault();

                    ViewBag.Message = TempData["Message"];
                    TempData["Message"] = "";

                    if (eventViewModel != null)
                        return View(eventViewModel);
                }


            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult TicketBuy(int EventId, string BuyerName)
        {
            TempData["Message"] = "";
            if (ModelState.IsValid)
            {
                using (var db = new dbInterviewContext())
                {
                    bool IsBuy = db.Buyer.Any(p => p.EventId == EventId);
                    if (!IsBuy)
                    {
                        Event _event = db.Event.FirstOrDefault(p => p.EventId == EventId);
                        if (_event != null)
                        {
                            Buyer buyer = new Buyer()
                            {
                                EventId = EventId,
                                BuyerName = BuyerName,
                                TesterKey = "ak"
                            };

                            db.Buyer.Add(buyer);
                            db.SaveChanges();
                        }

                        TempData["Message"] = "Ticket Bought";
                    }
                    else
                    {
                        TempData["Message"] = "Too late!";
                    }
                }
            }
            return RedirectToAction("TicketBuy", EventId);
        }

    }
}