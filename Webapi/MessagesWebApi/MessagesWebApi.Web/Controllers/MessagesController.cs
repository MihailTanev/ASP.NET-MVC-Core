namespace MessagesWebApi.Web.Controllers
{
    using MessagesWebApi.Data;
    using MessagesWebApi.Models;
    using MessagesWebApi.Web.ViewModels;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly MessagesWebApiDbContext context;

        public MessagesController(MessagesWebApiDbContext context)
        {
            this.context = context;
        }

        [HttpGet(Name ="All")]
        [Route("all")]
        public async Task <ActionResult<IEnumerable<Message>>> All()
        {
            return this.context.Messages
                .OrderBy(x => x.CreatedOn)
                .ToList();
        }

        [HttpPost(Name ="Create")]
        [Route("create")]
        public async Task <ActionResult> Create(MessagesViewModel model)
        {
            Message message = new Message
            {
                Content = model.Content,
                User = model.User,
                CreatedOn = DateTime.UtcNow
            };

            await this.context.AddAsync(message);
            await this.context.SaveChangesAsync();

            return this.Ok();
        }
    }
}
