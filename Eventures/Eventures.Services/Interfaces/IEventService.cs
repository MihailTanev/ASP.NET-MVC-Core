namespace Eventures.Services.Interfaces
{
    using System;

    public interface IEventService
    {
        void CreateEvent(string name, string place, DateTime start, DateTime end, int totalTickets, decimal pricePerTicket);

    }
}