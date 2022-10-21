using ArdenHotel.Areas.SysAdmin.Models;
using ArdenHotel.Data;
using ArdenHotel.Entities;
using ArdenHotel.Models;
using ArdenHotel.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
namespace ArdenHotel.Areas.SysAdmin.Services
{
    public class RoomService : IRoomService
    {
        private IGenericRepository<Room> _roomsRepository;
        private ApplicationDbContext applicationDbContext;
        public RoomService(IGenericRepository<Room> roomsRepository, ApplicationDbContext applicationDbContext)
        {
            _roomsRepository = roomsRepository;
            this.applicationDbContext = applicationDbContext;
        }

        public void DeleteRoom(int id)
        {
            if (id != 0)
            {
                _roomsRepository.Delete(id);
            }
        }

        public List<RoomMediaViewModel> GetAllRooms()
        {
            var data = (from a in applicationDbContext.Set<Room>()
                        join m in applicationDbContext.Set<Media>() on a.RoomID equals m.RoomId
                        into temp
                        from t in temp.DefaultIfEmpty()

                        select new RoomMediaViewModel
                        {
                            RoomID = a.RoomID,
                            RoomName = a.RoomName,
                            Capacity = a.Capacity,
                            Price = a.Price,
                            CreatedDateUtc = a.CreatedDateUtc,
                            ModifiedDateUtc = a.ModifiedDateUtc,
                            IsCheckOut = a.IsCheckOut,
                            IsAvaliable = a.IsAvaliable,
                            Description = a.Description,
                            Category = a.Category,
                            CategoryID = a.CategoryID,
                            RoomCount = a.RoomCount,
                            Type = a.Type,
                            Media = t
                        }).OrderByDescending(c => c.CreatedDateUtc).ToList();

            var result = data.GroupBy(x => x.RoomID).Select(g => g.First());


            if (result != null)
            {
                return result.ToList();

            }
            return null;
        }

        public Room GetRoom(int id)
        {
            if (id != 0)
            {
                var data = _roomsRepository.Get(x => x.RoomID == id);
                data.Media = applicationDbContext.Set<Media>().Where(x => x.RoomId == data.RoomID).FirstOrDefault();
                data.Category = applicationDbContext.Set<Category>().Where(x => x.CategoryID == data.CategoryID).FirstOrDefault();
                return data;
            }
            return null;
        }

        public RoomCommentViewModel GetRoomComment(int id)
        {
            RoomCommentViewModel roomCommentViewModel = null;
            if (id != 0)
            {
                var data = _roomsRepository.Get(x => x.RoomID == id);
                data.Media = applicationDbContext.Set<Media>().Where(x => x.RoomId == data.RoomID).FirstOrDefault();
                data.Category = applicationDbContext.Set<Category>().Where(x => x.CategoryID == data.CategoryID).FirstOrDefault();
                var commentList = applicationDbContext.Set<Comment>().Where(x => x.RoomID == data.RoomID).ToList();
                roomCommentViewModel = new RoomCommentViewModel() { Room = data, Comments = commentList };
            }
            return roomCommentViewModel;
        }

        public Room InsertRoom(Room room)
        {
            if (room != null)
            {
                _roomsRepository.Add(room);
            }
            return null;
        }

        public Room UpdateRoom(Room room)
        {
            if (room != null)
            {
                _roomsRepository.Update(room);
            }
            return null;
        }
    }
}
