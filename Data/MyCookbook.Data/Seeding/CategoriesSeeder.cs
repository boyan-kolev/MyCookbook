namespace MyCookbook.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MyCookbook.Data.Models;

    public class CategoriesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Categories.Any())
            {
                return;
            }

            var categories = new List<(string Name, string ImageUrl)>
            {
                ("Салати", "https://receptnik.net/wp-content/uploads/2013/06/shopska-salata-575x262.jpg"),
                ("Предястия", "https://www.glutenfreepalate.com/wp-content/uploads/2019/02/Sausage-and-Bacon-Appertizer2.3-720x405.jpg"),
                ("Супи", "https://www.monro.gocegid.com/wp-content/uploads/2018/01/Pil.supa_-600x384.png"),
                ("Ястия с месо", "https://thenypost.files.wordpress.com/2018/01/shutterstock_763039075.jpg?quality=80&strip=all"),
                ("Безмесни ястия", "https://www.pizzapliska.org/wp-content/uploads/2015/05/pecheni_zelenchuci2.jpg"),
                ("Ястия с риба", "https://lh3.googleusercontent.com/proxy/n1UwLIhohR9GajB9lT38RK03DTMvH_IGK8WJQrR4Pw8e-R7hMsup9USwAmr9hLJfwzE9BlpE_hZXwII_3wQ-olxOPqDiyxicu_vDB1mRslDkTtw"),
                ("Десерти", "https://m.netinfo.bg/media/images/29240/29240159/640-420-edin-ot-najvkusnite-solenosladki-deserti-chestva-svoia-praznik.jpg"),
                ("Други", "https://img.bg.sof.cmestatic.com/media/images/640x/Sep2015/2110881258.jpg"),
            };

            foreach (var category in categories)
            {
                await dbContext.Categories.AddAsync(new Category
                {
                    Name = category.Name,
                    ImageUrl = category.ImageUrl,
                });
            }
        }
    }
}
