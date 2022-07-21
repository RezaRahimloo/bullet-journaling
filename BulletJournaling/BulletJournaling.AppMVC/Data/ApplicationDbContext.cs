using BulletJournaling.AppMVC.Data.Convertors;
using BulletJournaling.AppMVC.Data.DatabaseModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BulletJournaling.AppMVC.Data
{

    public class AppDb : IdentityDbContext<AppUser>
    {
        public AppDb(DbContextOptions<AppDb> options): base(options)
        {
            
        }
        protected override void ConfigureConventions(ModelConfigurationBuilder builder)
        {
            builder.Properties<DateOnly>()
                .HaveConversion<DateOnlyConverter>()
                .HaveColumnType("date");
            builder.Properties<DateOnly?>()
                .HaveConversion<NullableDateOnlyConverter>()
                .HaveColumnType("date");
        }
        public DbSet<DayLog> DayLogs { get; set; }
        public DbSet<Mba> Mbas { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<Smoking> Smokings { get; set; }
    }
}