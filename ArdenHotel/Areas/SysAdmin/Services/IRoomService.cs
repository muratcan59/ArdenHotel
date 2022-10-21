using ArdenHotel.Areas.SysAdmin.Models;
using ArdenHotel.Entities;
using ArdenHotel.Models;
using System.Collections.Generic;

namespace ArdenHotel.Areas.SysAdmin.Services
{
    public interface IRoomService
    {
        Room InsertRoom(Room room);
        Room UpdateRoom(Room room);
        void DeleteRoom(int id);
        Room GetRoom(int id);
        RoomCommentViewModel GetRoomComment(int id);
        List<RoomMediaViewModel> GetAllRooms();
    }
}
