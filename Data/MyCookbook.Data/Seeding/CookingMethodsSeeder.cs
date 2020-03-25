namespace MyCookbook.Data.Seeding
{
    using MyCookbook.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class CookingMethodsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.CookingMethods.Any())
            {
                return;
            }

            var cookingMethods = new List<(string Name, string ImageUrl)>
            {
                ("Без термична обработка", "https://meltworkout.com/wp-content/uploads/2018/12/No-Cooking-400x350.gif"),
                ("Варене", "https://images.assetsdelivery.com/compings_v2/prettyvectors/prettyvectors1605/prettyvectors160500200.jpg"),
                ("Печене", "https://image.shutterstock.com/image-vector/cartoon-kitchen-appliance-happy-smiling-260nw-176221712.jpg"),
                ("Пържене", "https://encrypted-tbn0.gstatic.com/images?q=tbn%3AANd9GcQ-O5sNDTIF1q-F_yqA0kpgITutYnAZK6h3T-r1gaZZ82wLHn4R"),
                ("Скара", "https://clipartart.com/images/animated-barbecue-clipart.png"),
            };

            foreach (var cookingMethod in cookingMethods)
            {
                await dbContext.CookingMethods.AddAsync(new CookingMethod
                {
                    Name = cookingMethod.Name,
                    ImageUrl = cookingMethod.ImageUrl,
                });
            }
        }
    }
}
