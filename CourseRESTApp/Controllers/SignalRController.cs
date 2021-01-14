using CourseRESTApp.Data;
using CourseRESTApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalRChat.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseRESTApp.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class SignalRController : Controller
    {
        private IHubContext<ChatHub> _chat;

        public SignalRController(IHubContext<ChatHub> chat)
        {
            _chat = chat;
        }

        [HttpPost("[action]/{connectionId}/{roomName}")]
        public async Task<IActionResult> JoinRoom(string connectionId, string roomName)
        {
            await _chat.Groups.AddToGroupAsync(connectionId, roomName);
            return Ok();
        }

        [HttpPost("[action]/{connectionId}/{roomName}")]
        public async Task<IActionResult> LeaveRoom(string connectionId, string roomName)
        {
            await _chat.Groups.RemoveFromGroupAsync(connectionId, roomName);
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> SendMessage(
            string message,
            int chatId,
            string roomName,
            [FromServices] ChatContext ctx)
        {
            var Message = new Message
            {
                ChatId = chatId,
                Text = message,
                Name = User.Identity.Name,
                TImestamp = DateTime.Now
            };

            ctx.Messages.Add(Message);
            await ctx.SaveChangesAsync();

            await _chat.Clients.Group(roomName)
                .SendAsync("RecieveMessage", Message);
            return Ok();
        }
    }
}
