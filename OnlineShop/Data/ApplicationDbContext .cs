using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Models;

namespace OnlineShop.Data
{
    public class ApplicationDbContext:IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options)
            : base(options)
        {
        }
        public DbSet<Pitch>  Pitches { get; set; }
        public DbSet<PitchType>  PitchTypes { get; set; }
        public DbSet<SpecialTag> specialTags { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<ApplicationUser> applicationUsers { get; set; }
        public DbSet<Complaint> complaints { get; set; }
         

    }
}
