using Eventures.Web.Models.BindingModels;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace Eventures.Web.Filters
{
    public class EventsLogActionFilter : ActionFilterAttribute
    {
        private readonly ILogger logger;
        private EventCreateBindingModel model;

        public EventsLogActionFilter(ILoggerFactory loggerFactory)
        {
            this.logger = loggerFactory.CreateLogger<EventsLogActionFilter>();
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            this.model = context.ActionArguments.Values.OfType<EventCreateBindingModel>().Single();

            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (this.model != null)
            {
                var User = context.HttpContext.User.Identity.Name;
                var EventName = this.model.Name;
                var EventStart = this.model.Start;
                var EventEnd = this.model.End;
                this.logger.LogInformation($"[{DateTime.UtcNow}] Administrator {User} create event {EventName} ({EventStart} / {EventEnd}).");
            }

            base.OnActionExecuted(context);
        }
    }
}
