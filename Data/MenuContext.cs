using Microsoft.EntityFrameworkCore;
using FoodMenuApp.Models;

namespace FoodMenuApp.Data
{
    public class MenuContext : DbContext
    {
        public MenuContext( DbContextOptions<MenuContext> options ) : base(options) 
        { 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DishIngredient>().HasKey(di => new
            {
                di.DishId,
                di.IngredientId
            });
            //one dish has many dishIngrediets
            //one ingreditent has many dishIngredients
            modelBuilder.Entity<DishIngredient>().HasOne(d => d.Dish).WithMany(di => di.DishIngredients).HasForeignKey(d => d.DishId);
            modelBuilder.Entity<DishIngredient>().HasOne(i => i.Ingredient).WithMany(di => di.DishIngredients).HasForeignKey(i => i.IngredientId);

            //Seed data
            modelBuilder.Entity<Dish>().HasData(
                new Dish { Id=1, Name= "Margheritta", Price= 7.50, ImageUrl= "https://cdn.shopify.com/s/files/1/0205/9582/articles/20220211142347-margherita-9920_ba86be55-674e-4f35-8094-2067ab41a671.jpg?crop=center&height=915&v=1644590192&width=1200" },
                new Dish { Id=2, Name= "Pepperoni", Price= 8.50, ImageUrl= "https://www.shutterstock.com/image-photo/tasty-pepperoni-pizza-cooking-ingredients-260nw-1239982861.jpg" }
                );
            modelBuilder.Entity<Ingredient>().HasData(
                new Ingredient { Id= 1, Name="Tomato Sauce"},
                new Ingredient { Id = 2, Name = "Mozzarella" }
                );
            modelBuilder.Entity<DishIngredient>().HasData(
                new DishIngredient { DishId=1, IngredientId=1},
                new DishIngredient { DishId = 1, IngredientId = 2 }
                );

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Dish> Dishes { get; set; }  
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<DishIngredient> DishIngredients { get; set; }
    }
}
