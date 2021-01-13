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
    public class SignalRController : Controller
    {
        private IHubContext<ChatHub> _chat;

        public SignalRController(IHubContext<ChatHub> chat)
        {
            _chat = chat;
        }

        public async Task<IActionResult> JoinChat(string connectionId, string roomName)
        {
            await _chat.Groups.AddToGroupAsync(connectionId, roomName);
            return Ok();
        }

        public async Task<IActionResult> LeaveChat(string connectionId, string roomName)
        {
            await _chat.Groups.RemoveFromGroupAsync(connectionId, roomName);
            return Ok();
        }
    }
