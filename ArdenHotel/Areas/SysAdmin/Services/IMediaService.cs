using ArdenHotel.Entities;
using System.Collections.Generic;

namespace ArdenHotel.Areas.SysAdmin.Services
{
    public interface IMediaService
    {
        Media InsertMedia(Media media);
        Media UpdateMedia(Media media);
        void DeleteMedia(int id);
        Media GetMediaById(int id);
        List<Media> GetAllMedia();
    }
}
