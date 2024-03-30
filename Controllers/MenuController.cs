using Microsoft.AspNetCore.Mvc;
using FoodMenuApp.Data;
using FoodMenuApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodMenuApp.Controllers
{
    public class MenuController : Controller
    {
        private readonly MenuContext _context;
        public MenuController(MenuContext context) 
        {
            _context = context;
        }
        public async Task<IActionResult> Index(string searchString)
        {
            var dishes = from d in _context.Dishes
                       select d;
            if(!string.IsNullOrEmpty(searchString))
            {
                dishes = dishes.Where(d => d.Name.ToLower().Contains(searchString.ToLower()));
                return View(await dishes.ToListAsync());


            }
            return View( await dishes.ToListAsync());
        }

        public async Task<IActionResult> Details (int? id)
        {
            var dish = await _context.Dishes
                .Include(di => di.DishIngredients)
                .ThenInclude(i => i.Ingredient)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (dish == null) 
            {
                return NotFound();
            }
            return View(dish);
        }
    }
}
