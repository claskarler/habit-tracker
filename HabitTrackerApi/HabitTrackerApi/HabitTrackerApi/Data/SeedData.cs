using HabitTrackerApi.Contexts;
using HabitTrackerApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace HabitTrackerApi.Data;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider, DataContext context)
    {
        context.Database.Migrate();

        if (!context.Icons.Any())
        {
            var icons = new List<IconEntity>
                {
                    new IconEntity { Name = "Heart", CssClass = "fa-regular fa-heart" },
                    new IconEntity { Name = "Star", CssClass = "fa-regular fa-star" },
                    new IconEntity { Name = "Water Glass", CssClass = "fa-solid fa-glass-water" },
                    new IconEntity { Name = "Seedling", CssClass = "fa-solid fa-seedling" },
                    new IconEntity { Name = "Bell", CssClass = "fa-solid fa-bell" },
                    new IconEntity { Name = "Envelope", CssClass = "fa-regular fa-envelope" },
                    new IconEntity { Name = "Pencil", CssClass = "fa-solid fa-pencil" },
                    new IconEntity { Name = "Lightbulb", CssClass = "fa-regular fa-lightbulb" },
                    new IconEntity { Name = "Trash Can", CssClass = "fa-regular fa-trash-can" },
                    new IconEntity { Name = "Car", CssClass = "fa-solid fa-car" },
                    new IconEntity { Name = "Music", CssClass = "fa-solid fa-music" },
                    new IconEntity { Name = "Bath", CssClass = "fa-solid fa-bath" },
                    new IconEntity { Name = "Money Bill", CssClass = "fa-solid fa-money-bill" },
                    new IconEntity { Name = "Bicycle", CssClass = "fa-solid fa-bicycle" },
                    new IconEntity { Name = "Shower", CssClass = "fa-solid fa-shower" },
                    new IconEntity { Name = "Paw", CssClass = "fa-solid fa-paw" },
                    new IconEntity { Name = "Pills", CssClass = "fa-solid fa-pills" },
                    new IconEntity { Name = "Book", CssClass = "fa-solid fa-book" },
                    new IconEntity { Name = "Shirt", CssClass = "fa-solid fa-shirt" },
                    new IconEntity { Name = "Gamepad", CssClass = "fa-solid fa-gamepad" },
                    new IconEntity { Name = "Mug Saucer", CssClass = "fa-solid fa-mug-saucer" },
                    new IconEntity { Name = "Utensils", CssClass = "fa-solid fa-utensils" },
                    new IconEntity { Name = "Bed", CssClass = "fa-solid fa-bed" },
                    new IconEntity { Name = "Language", CssClass = "fa-solid fa-language" },
                    new IconEntity { Name = "Shopping", CssClass = "fa-solid fa-cart-shopping" },
                    new IconEntity { Name = "Film", CssClass = "fa-solid fa-film" },
                    new IconEntity { Name = "Newspaper", CssClass = "fa-solid fa-newspaper" },
                    new IconEntity { Name = "Dumbbell", CssClass = "fa-solid fa-dumbbell" },
                };

            context.Icons.AddRange(icons);
            context.SaveChanges();
        }
    }
}
