using RestRoomTemp.Models;

namespace RestRoomTemp.Repos
{
    public class RestRoomRepoDB : IRestRoomRepoDB
    {
        public RoomTempContext _context;

        public RestRoomRepoDB(RoomTempContext context)
        {
            _context = context;
        }

        public List<RoomTemp> GetAll()
        {
            return _context.RoomTemp.ToList();
        }
        public RoomTemp? GetById(int id)
        {
            return _context?.RoomTemp.FirstOrDefault(x => x.Id == id);
        }
        public RoomTemp Add(RoomTemp roomTemp)
        {
            _context.RoomTemp.Add(roomTemp);
            _context.SaveChanges();
            return roomTemp;
        }
        public RoomTemp Delete(int id)
        {
            var roomTemp = GetById(id);
            if (roomTemp == null)
            {
                throw new ArgumentNullException("RoomTemp not found");
            }
            _context.RoomTemp.Remove(roomTemp);
            _context.SaveChanges();
            return roomTemp;
        }


    }
}
