using RestRoomTemp.Models;

namespace RestRoomTemp.Repos
{
    public interface IRestRoomRepoDB
    {
        RoomTemp Add(RoomTemp roomTemp);
        List<RoomTemp> GetAll();
        RoomTemp? GetById(int id);
        public RoomTemp Delete(int id);
    }
}