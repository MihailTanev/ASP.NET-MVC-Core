namespace Eventures.Web.Controllers
{
    using Eventures.Data;
    using Eventures.Domain;
    using Eventures.Services.Interfaces;
    using Eventures.Web.Filters;
    using Eventures.Web.ViewModels.Events;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System.Collections.Generic;
    using System.Linq;

    public class EventController : Controller
    {
        private readonly EventuresDbContext context;
        private readonly ILogger logger;
        private readonly IEventService eventsService;


        public EventController(EventuresDbContext context, ILogger<EventController> logger, IEventService eventsService)
        {
            this.eventsService = eventsService;
            this.context = context;
            this.logger = logger;
        }

        [Authorize]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        [ServiceFilter(typeof(AdminActivityLoggerFilter))]
        public IActionResult Create(CreateEventViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                this.eventsService.CreateEvent(model.Name, model.Place, model.Start, model.End, model.TotalTickets, model.PricePerTicket);
                this.logger.LogInformation($"Event created: {model.Name}", model);
                return RedirectToAction("All");
            }
            else
            {
                return this.View(model);
            }
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