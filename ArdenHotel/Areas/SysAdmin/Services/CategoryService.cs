using ArdenHotel.Entities;
using ArdenHotel.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ArdenHotel.Areas.SysAdmin.Services
{
    public class CategoryService : ICategoryService
    {
        private IGenericRepository<Category> _categoryRepository;

        public CategoryService(IGenericRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public void DeleteCategory(int id)
        {
            if (id != 0)
            {
                _categoryRepository.Delete(id);
            }
        }

        public Category GetCategory(int id)
        {
            if (id != 0)
            {
                return _categoryRepository.Get(x => x.CategoryID == id);
            }
            return null;
        }

        public Category InsertCategory(Category category)
        {
            if (category != null)
            {
                _categoryRepository.Add(category);
                return category;
            }
            return null;
        }

        public List<Category> ListCategory()
        {
            var data = _categoryRepository.GetList();
            if (data != null)
            {
                return (List<Category>)data;
            }
            return null;
        }

        public Category UpdateCategory(Category category)
        {
            if (category != null)
            {
                _categoryRepository.Update(category);
                return category;
            }
            return null;
        }
    }
}
