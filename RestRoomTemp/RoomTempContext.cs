using Microsoft.EntityFrameworkCore;
using RestRoomTemp.Models;

namespace RestRoomTemp
{
    public class RoomTempContext:DbContext
    {


        public RoomTempContext(DbContextOptions<RoomTempContext> options) : base(options) { }


        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(DbConn.Conn);
        }

        public DbSet<RoomTemp> RoomTemp { get; set; }



    }
}
