using System;
using System.Collections.Generic;

namespace MyRestaurant.Models
{
    public partial class MenuCategory
    {
        public int Id { get; set; }
        public int? MenuId { get; set; }
        public int? CatId { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Category? Cat { get; set; }
        public virtual Menu? Menu { get; set; }
    }
}
