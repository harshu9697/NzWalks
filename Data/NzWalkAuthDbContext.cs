using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZWalks.API.Data
{
    public class NzWalkAuthDbContext : IdentityDbContext
    {
        public NzWalkAuthDbContext(DbContextOptions<NzWalkAuthDbContext> options) : base(options)
        {
        }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleID = "047d323c-7798-4d58-90fe-526956608c70";
            var writerRoleID = "3ca7a579-768c-4340-a82b-77722030d97b";

            var roles = new List<IdentityRole>
            {

                new IdentityRole
                {
                    Id = readerRoleID,
                    ConcurrencyStamp = readerRoleID,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper()
                },
                new IdentityRole
                {
                    Id = writerRoleID,
                    ConcurrencyStamp = writerRoleID,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper()
                },
            };
            builder.Entity<IdentityRole>().HasData(roles);

        }
    }
}
