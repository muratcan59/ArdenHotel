using ArdenHotel.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ArdenHotel.Areas.SysAdmin.Services
{
    public interface ICategoryService
    {
        Category GetCategory(int id);
        Category InsertCategory(Category category);
        Category UpdateCategory(Category category);
        List<Category> ListCategory();
        void DeleteCategory(int id);
    }
}
