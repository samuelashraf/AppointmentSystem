using Microsoft.EntityFrameworkCore;

namespace AppointnentSystem.Data
{
    public class DbAppontment:DbContext
    {
        public DbAppontment(DbContextOptions<DbAppontment> options) : base(options)
        {
        }
        public DbSet<Models.Appointment> Appointments { get; set; }
    }
}
