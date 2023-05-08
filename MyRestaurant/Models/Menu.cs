using System;
using System.Collections.Generic;

namespace MyRestaurant.Models
{
    public partial class Menu
    {
        public Menu()
        {
            MenuCategories = new HashSet<MenuCategory>();
        }

        public int MenuId { get; set; }
        public string MenuName { get; set; } = null!;
        public string? MenuImage { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<MenuCategory> MenuCategories { get; set; }
    }
}
