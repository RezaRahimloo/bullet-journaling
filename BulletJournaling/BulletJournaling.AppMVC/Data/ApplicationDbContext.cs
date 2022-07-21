using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BulletJournaling.AppMVC.Data
{

    public class AppDb : IdentityDbContext<AppUser>
    {
        public AppDb(DbContextOptions<AppDb> options)
            : base(options)
        {
        }
    }
}