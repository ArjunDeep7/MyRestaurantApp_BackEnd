using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyRestaurant.Models;

namespace MyRestaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly RestaurantDBContext _context;

        public CategoriesController(RestaurantDBContext context)
        {
            _context = context;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            if (_context.Categories == null)
            {
                return NotFound();
            }
            
            List<Category> categoryList = await _context.Categories.ToListAsync();
            foreach (Category category in categoryList)
            {
                if(category.IsDeleted == false)
                {
                    categoryList.Add(category);
                }
            }
            return categoryList;
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
          if (_context.Categories == null)
          {
              return NotFound();
          }
            var category = await _context.Categories.FindAsync(id);

            if ((category == null) || (category.IsDeleted == true))
            {
                return NotFound();
            }

            return category;
        }

        // GET: using MenuID
        [HttpGet("menuId = {menuId}")]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories(int menuId)
        {
            if (_context.Categories == null)
            {
                return NotFound();
            }



            List<MenuCategory> menuCategory = await _context.MenuCategories.ToListAsync();
            List<int?> filteredList = new List<int?>();



            menuCategory.FindAll(menuCategory => menuCategory.IsDeleted == false && menuCategory.MenuId == menuId).
            ForEach(menuCat => filteredList.Add(menuCat.CatId));



            List<Category> categories = new List<Category>();
            await _context.Categories.ForEachAsync(category =>
            {
                if (category.IsDeleted == false && filteredList.Contains(category.CatId))
                {
                    categories.Add(category);
                }
            });



            return Ok(categories);
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, Category category)
        {
            if (id != category.CatId)
            {
                return BadRequest();
            }

            _context.Entry(category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(int menuId, Category category)
        {
            if (_context.Categories == null)
            {
                return Problem("Entity set 'RestaurantDBContext.Categories'  is null.");
            }

            _context.Categories.Add(category);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CategoryExists(category.CatId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

           // updating the menu_category table
            MenuCategory menuCategory = new MenuCategory();
            menuCategory.MenuId = menuId;
            menuCategory.CatId = category.CatId;
            _context.MenuCategories.Add(menuCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategory", new { id = category.CatId }, category);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            if (_context.Categories == null)
            {
                return NotFound();
            }
            var category = await _context.Categories.FindAsync(id);
            if ((category == null) || (category.IsDeleted == true))
            {
                return NotFound();
            }

            List<CategoryDish> categoryDishes = await _context.CategoryDishes.ToListAsync();
            List<int?> filteredDishes = new List<int?>();
            foreach (var catDish in categoryDishes)
            {
                if (catDish.CatId == id && catDish.IsDeleted == false)
                {
                    filteredDishes.Add(catDish.DishId);
                    catDish.IsDeleted = true;
                    _context.CategoryDishes.Update(catDish);
                    _context.SaveChanges();
                }
            }

            List<Dish> dishes = await _context.Dishes.ToListAsync();
            dishes.ForEach(dish =>
            {
                if (filteredDishes.Contains(dish.DishId))
                {
                    dish.IsDeleted = true;
                    _context.Dishes.Update(dish);
                    _context.SaveChanges();
                }
            });

            MenuCategory menuCategory = _context.MenuCategories.ToList().Find(mc => mc.Id == id);
            if (menuCategory != null)
            {
                menuCategory.IsDeleted = true;
                _context.MenuCategories.Update(menuCategory);
                _context.SaveChanges();
            }

            category.IsDeleted = true;
            _context.Categories.Update(category);
            _context.SaveChanges();

            return NoContent();
        }

        private bool CategoryExists(int id)
        {
            return (_context.Categories?.Any(e => e.CatId == id)).GetValueOrDefault();
        }
    }
}
