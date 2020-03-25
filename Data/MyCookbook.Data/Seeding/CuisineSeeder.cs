namespace MyCookbook.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MyCookbook.Data.Models;

    public class CuisineSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Cuisines.Any())
            {
                return;
            }

            var cuisines = new List<(string Name, string ImageUrl)>
            {
                ("Българска кухня", "https://upload.wikimedia.org/wikipedia/commons/9/9a/Flag_of_Bulgaria.svg"),
                ("Американска кухня", "https://images-na.ssl-images-amazon.com/images/I/41RWt%2BhqrpL._AC_SX425_.jpg"),
                ("Турска кухня", "https://upload.wikimedia.org/wikipedia/commons/b/b4/Flag_of_Turkey.svg"),
                ("Гръцка кухня", "https://upload.wikimedia.org/wikipedia/commons/5/5c/Flag_of_Greece.svg"),
                ("Китайска кухня", "https://upload.wikimedia.org/wikipedia/commons/f/fa/Flag_of_the_People%27s_Republic_of_China.svg"),
                ("Италианска кухня", "https://upload.wikimedia.org/wikipedia/commons/0/03/Flag_of_Italy.svg"),
                ("Руска кухня", "https://upload.wikimedia.org/wikipedia/commons/f/f3/Flag_of_Russia.svg"),
                ("Македонска кухня", "https://upload.wikimedia.org/wikipedia/commons/7/79/Flag_of_North_Macedonia.svg"),
                ("Френска кухня", "https://upload.wikimedia.org/wikipedia/commons/c/c3/Flag_of_France.svg"),
            };

            foreach (var cuisine in cuisines)
            {
                await dbContext.Cuisines.AddAsync(new Cuisine
                {
                    Name = cuisine.Name,
                    ImageUrl = cuisine.ImageUrl,
                });
            }
        }
    }
}
