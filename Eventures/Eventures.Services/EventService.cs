namespace Eventures.Services
{
    using System;
    using Data;
    using Interfaces;
    using Domain;

    public class EventService : IEventService
    {
        private readonly EventuresDbContext context;

        public EventService(EventuresDbContext context)
        {
            this.context = context;
        }

        public void CreateEvent(string name, string place, DateTime start, DateTime end, int totalTickets, decimal pricePerTicket)
        {
            var addEvent = new Event
            {
                Name = name,
                Place = place,
                Start = start,
                End = end,
                TotalTickets = totalTickets,
                PricePerTicket = pricePerTicket
            };

            this.context.Events.Add(addEvent);
            this.context.SaveChanges();
        }

    }
}
