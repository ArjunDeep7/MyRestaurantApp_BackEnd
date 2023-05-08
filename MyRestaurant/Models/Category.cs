using System;
using System.Collections.Generic;

namespace MyRestaurant.Models
{
    public partial class Category
    {
        public Category()
        {
            CategoryDishes = new HashSet<CategoryDish>();
            MenuCategories = new HashSet<MenuCategory>();
        }

        public int CatId { get; set; }
        public string CatName { get; set; } = null!;
        public string? CatImage { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<CategoryDish> CategoryDishes { get; set; }
        public virtual ICollection<MenuCategory> MenuCategories { get; set; }
    }
}
