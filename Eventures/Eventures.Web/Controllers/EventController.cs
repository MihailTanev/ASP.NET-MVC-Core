namespace Eventures.Web.Controllers
{
    using Eventures.Data;
    using Eventures.Domain;
    using Eventures.Web.ViewModels.Events;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System.Collections.Generic;
    using System.Linq;

    public class EventController : Controller
    {
        private readonly EventuresDbContext context;

        private readonly ILogger logger;


        public EventController(EventuresDbContext context, ILogger<EventController> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Create(CreateEventViewModel model)
        {
            if (ModelState.IsValid)
            {
                Event eventDb = new Event
                {
                    Name = model.Name,
                    Place = model.Place,
                    End = model.End,
                    PricePerTicket = model.PricePerTicket,
                    Start = model.Start,
                    TotalTickets = model.TotalTickets,
                };
                context.Events.Add(eventDb);
                context.SaveChanges();

                this.logger.LogInformation($"Event created: {model.Name}", model);

                return RedirectToAction("All");
            }
            return this.View();
        }

        [HttpGet]
        public IActionResult All()
        {
            List<EventViewModel> events = context.Events
                .Select(eventDb => new EventViewModel
                {
                    Name = eventDb.Name,
                    Place = eventDb.Place,
                    Start = eventDb.Start,
                    End = eventDb.End
                })
                .OrderByDescending(e => e.Start)
                .ToList();

            return this.View(events);
        }
    }
}