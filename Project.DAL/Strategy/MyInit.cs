using Bogus.DataSets;
using Project.COMMON.Tools;
using Project.DAL.Context;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.Strategy
{
    public class MyInit:CreateDatabaseIfNotExists<MyContext>
    {
        protected override void Seed(MyContext context)
        {
            //todo Validationları data seed edildikten sonra aktive et veya WorkAround'unu sor!

            //Admin tanımlama
            AppUser adminUser = new AppUser
            {
                UserName = "sed",
                Password = DantexCrypt.Crypt("123"),
                ConfirmPassword = DantexCrypt.Crypt("123"),
                Email = "slmdmrr@gmail.com",
                Role = ENTITIES.Enums.UserRole.Admin,
                Active = true
                
            };

            context.AppUsers.Add(adminUser);
            context.SaveChanges();

            UserProfile adminUserProfile = new UserProfile
            {
                ID = 1, 
                FirstName = new Name("tr").FirstName(),
                LastName = new Name("tr").LastName(),
                Address = new Address("tr").Locale
            };
            context.UserProfiles.Add(adminUserProfile); 
            context.SaveChanges();

            //Aktif Member tanımlama
            AppUser aktifUser = new AppUser
            {
                UserName = "sedx",
                Password = DantexCrypt.Crypt("123"),
                ConfirmPassword = DantexCrypt.Crypt("123"),
                Email = "sedx@gmail.com",
                Role = ENTITIES.Enums.UserRole.Member,
                Active = true

            };

            context.AppUsers.Add(aktifUser);
            context.SaveChanges();
            UserProfile aktifUserProfile = new UserProfile
            {
                ID = 2, 
                FirstName = new Name("tr").FirstName(),
                LastName = new Name("tr").LastName(),
                Address = new Address("tr").Locale
            };
            context.UserProfiles.Add(aktifUserProfile);
            context.SaveChanges();



            for (int i = 0; i < 5; i++)
            {
                AppUser ap = new AppUser
                {
                    UserName = new Internet("tr").UserName(),
                    Password = new Internet("tr").Password(),
                    Email = new Internet("tr").Email()
                };
                context.AppUsers.Add(ap);
            }
            context.SaveChanges();

            for (int i = 3; i < 8; i++)
            {
                UserProfile up = new UserProfile
                {
                    ID = i, //Birebir ilişki olduğundan dolayı üst tarafta olusturulan AppUser'ların ID'leri ile buraları eşleşmeli... O yüzden döngünün iterasyonunu 3'den baslattık.
                    FirstName = new Name("tr").FirstName(),
                    LastName = new Name("tr").LastName(),
                    Address = new Address("tr").Locale
                };
                context.UserProfiles.Add(up);
            }

            context.SaveChanges();

            for (int i = 0; i < 5; i++)
            {

                Category c = new Category
                {
                    CategoryName = new Commerce("tr").Categories(1)[0],
                    Description = new Lorem("tr").Sentence(10)
                };

                for (int j = 0; j < 8; j++)
                {
                    Product p = new Product
                    {
                        ProductName = new Commerce("tr").ProductName(),
                        UnitPrice = Convert.ToDecimal(new Commerce("tr").Price()),
                        UnitsInStock = 100,
                        ImagePath = new Images().Cats()
                    };
                    c.Products.Add(p);
                }

                context.Categories.Add(c);
                context.SaveChanges();

            }
        }

    }
}
