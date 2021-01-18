using CourseRESTApp.Data;
using CourseRESTApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CourseRESTApp.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        private ChatContext _ctx;

        public ChatController(ChatContext ctx) => _ctx = ctx;

        [HttpGet]
        public IActionResult Index(string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                var chats = _ctx.Chats
                    .Include(x => x.Users)
                    .Where(x => !x.Users
                    .Any(y => y.UserId == User.FindFirst(ClaimTypes.NameIdentifier).Value))
                    .ToList();

                return View(chats);
            }
            else
            {
                var chats = _ctx.Chats
                    .Where(x => x.Name.Contains(search))
                    .ToList();

                return View(chats);
            }            
        }

        [HttpGet]
        public IActionResult Chatroom(int id)
        {
            var chat = _ctx.Chats
                .Include(x => x.Messages)
                .FirstOrDefault(x => x.Id == id);
            return View(chat);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage(int chatId, string message)
        {
            var Message = new Message
            {
                ChatId = chatId,
                Text = message,
                Name = User.Identity.Name,
                TImestamp = DateTime.Now
            };

            _ctx.Messages.Add(Message);
            await _ctx.SaveChangesAsync();

            return RedirectToAction("Chatroom", new { id = chatId });
        }


        [HttpPost]
        public async Task<IActionResult> CreateChatroom(string name)
        {
            var chat = new Chat
            {
                Name = name,
                Type = ChatType.Room
            };

            chat.Users.Add(new ChatUser
            {
                UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value,
                Role = UserRole.Admin
            });

            _ctx.Chats.Add(chat);
            await _ctx.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> JoinRoom(int id)
        {
            var chatUser = new ChatUser
            {
                ChatId = id,
                UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value,
                Role = UserRole.Member
            };

            _ctx.ChatUsers.Add(chatUser);

            await _ctx.SaveChangesAsync();

            return RedirectToAction("Chatroom", "Chat", new { id = id });
        }
    }
}
