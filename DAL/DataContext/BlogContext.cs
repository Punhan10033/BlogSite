using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.DataContext
{   
    public class BlogContext:DbContext
    {
        public BlogContext(DbContextOptions<BlogContext>options):base(options)
        {
            
        }
        public DbSet<About> Abouts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<UserAuthentication> UserAuthentication { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<FriendRequest> FriendRequests { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Team>Teams { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Message2> Messages2 { get; set; }
        public DbSet<Like> Likes { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Match>()
                .HasOne(x => x.HomeTeam)
                .WithMany(y => y.HomeMatches)
                .HasForeignKey(z => z.HomeTeamId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<Match>()
                .HasOne(x => x.GuestTeam)
                .WithMany(y => y.AwayMatches)
                .HasForeignKey(z => z.GuestTeamId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<Message2>()
                .HasOne(x => x.MessageSender)
                .WithMany(y => y.UserSender)
                .HasForeignKey(z => z.SenderId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<Message2>()
               .HasOne(x => x.MessageReceiever)
               .WithMany(y => y.UserReceiever)
               .HasForeignKey(z => z.ReceieverId)
			   .OnDelete(DeleteBehavior.ClientSetNull);


          

            builder.Entity<User>()
                .HasMany(x => x.NotificationReceiever)
                .WithOne(x => x.UserReceiever)
                .HasForeignKey(x => x.UserReceieverId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<Like>()
                .HasOne(x => x.Blog)
                .WithMany(x => x.Likes)
                .HasForeignKey(x => x.BlogId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<Like>()
                .HasOne(x => x.User)
                .WithMany(x => x.Likes)
                .HasForeignKey(x => x.LikeSender)
                .OnDelete(DeleteBehavior.ClientSetNull);
			//HomeMatches-->UserSender
			//AwayMatches-->UserReceiever

			//HomeTeam-->MessageSender
			//GuestTeam-->MessageReceiever

		}
	}
}
