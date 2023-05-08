using System;
using System.Collections.Generic;

namespace MyRestaurant.Models
{
    public partial class CategoryDish
    {
        public int Id { get; set; }
        public int? CatId { get; set; }
        public int? DishId { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Category? Cat { get; set; }
        public virtual Dish? Dish { get; set; }
    }
}
