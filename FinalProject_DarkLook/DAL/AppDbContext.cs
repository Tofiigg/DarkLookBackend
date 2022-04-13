using FinalProject_DarkLook.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject_DarkLook.DAL
{
    public class AppDbContext:IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Slider > Sliders { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Future> Futures { get; set; }
        public DbSet<WatchCard> WatchCards { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<BrandLogo> BrandLogos { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogCategory> BlogCategories { get; set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet<Skills> Skills { get; set; }
        public DbSet<OurTeam> OurTeams { get; set; }
        public DbSet<Setting> Setting { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Colour> Colours { get; set; }
        public DbSet<WatchColour> WatchColours { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<WatchSize> watchSizes { get; set; }
        public DbSet<BasketWatch> BasketWatches { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<BildingAdress> BildingAdresses { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }




















    }
}
