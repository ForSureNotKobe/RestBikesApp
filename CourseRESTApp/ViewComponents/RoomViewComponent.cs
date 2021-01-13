using CourseRESTApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CourseRESTApp.ViewComponents
{
    public class RoomViewComponent : ViewComponent
    {
        private ChatContext _ctx;
        public RoomViewComponent(ChatContext ctx)
        {
            _ctx = ctx;
        }

        public IViewComponentResult Invoke()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var chats = _ctx.ChatUsers
                .Include(x => x.Chat)
                .Where(x => x.User.Id == userId)
                .Select(x => x.Chat)
                .ToList();

            return View(chats);
        }
    }
}
