using System;
using System.Collections.Generic;

namespace MyRestaurant.Models
{
    public partial class Dish
    {
        public Dish()
        {
            CategoryDishes = new HashSet<CategoryDish>();
        }

        public int DishId { get; set; }
        public string DishName { get; set; } = null!;
        public int DishPrice { get; set; }
        public string? DishDescription { get; set; }
        public string? DishImage { get; set; }
        public string DishNature { get; set; } = null!;
        public bool IsDeleted { get; set; }

        public virtual ICollection<CategoryDish> CategoryDishes { get; set; }
    }
}
