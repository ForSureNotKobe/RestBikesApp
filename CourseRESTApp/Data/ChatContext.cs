using CourseRESTApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CourseRESTApp.Data
{
    public class ChatContext : IdentityDbContext<User>
    {
        public ChatContext(DbContextOptions<ChatContext> options) : base(options) { }

        public DbSet<Chat> Chats { get; set; }

        public DbSet<Message> Messages { get; set; }
        public DbSet<ChatUser> ChatUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ChatUser>()
                .HasKey(x => new { x.ChatId, x.UserId });
        }
    }
}
