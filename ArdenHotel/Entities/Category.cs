using System.Collections.Generic;

namespace ArdenHotel.Entities
{
    public class Category
    {
        public Category()
        {
        
        }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public bool IsActive { get; set; }
   
    }
}
