using ArdenHotel.Entities;
using ArdenHotel.Repository;
using System.Collections.Generic;

namespace ArdenHotel.Areas.SysAdmin.Services
{
    public class MediaService : IMediaService
    {
        private IGenericRepository<Media> _mediaRepository;

        public MediaService(IGenericRepository<Media> mediaRepository)
        {
            _mediaRepository = mediaRepository;
        }

        public Media InsertMedia(Media media)
        {
            if (media != null)
            {
                _mediaRepository.Add(media);
                return media;
            }
            return null;
        }

        public void DeleteMedia(int id)
        {
            if (id != 0)
            {
                _mediaRepository.Delete(id);
            }
        }

        public List<Media> GetAllMedia()
        {
            var list = _mediaRepository.GetList();
            if (list != null)
            {
                return (List<Media>)list;
            }
            return new List<Media>();
        }

        public Media GetMediaById(int id)
        {
            if (id != 0)
            {
                var data = _mediaRepository.Get(x => x.MediaID == id);
                return data;
            }
            return null;
        }

        public Media UpdateMedia(Media media)
        {
            if (media != null)
            {
                _mediaRepository.Update(media);
                return media;
            }
            return null;
        }
    }
}
