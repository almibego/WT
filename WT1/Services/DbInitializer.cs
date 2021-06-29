using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebLabsV05.DAL.Data;
using WebLabsV05.DAL.Entities;

namespace WT1.Services
{
    public class DbInitializer
    {
        public static async Task Seed(ApplicationDbContext context,
                            UserManager<ApplicationUser> userManager,
                            RoleManager<IdentityRole> roleManager)
        {
            // создать БД, если она еще не создана
            context.Database.EnsureCreated();
            // проверка наличия ролей
            if (!context.Roles.Any())
            {
                var roleAdmin = new IdentityRole
                {
                    Name = "admin",
                    NormalizedName = "admin"
                };
                // создать роль admin
                await roleManager.CreateAsync(roleAdmin);
            }
            // проверка наличия пользователей
            if (!context.Users.Any())
            {
                // создать пользователя user@mail.ru
                var user = new ApplicationUser
                {
                    Email = "user@mail.ru",
                    UserName = "user@mail.ru"
                };
                await userManager.CreateAsync(user, "123456");
                // создать пользователя admin@mail.ru
                var admin = new ApplicationUser
                {
                    Email = "admin@mail.ru",
                    UserName = "admin@mail.ru"
                };
                await userManager.CreateAsync(admin, "123456");
                // назначить роль admin
                admin = await userManager.FindByEmailAsync("admin@mail.ru");
                await userManager.AddToRoleAsync(admin, "admin");
            }

            //проверка наличия групп объектов
            if (!context.PCPartGroups.Any())
            {
                context.PCPartGroups.AddRange(
                new List<PCPartGroup>
                {
                    new PCPartGroup { GroupName="Процессоры" },
                    new PCPartGroup { GroupName="SSD" },
                    new PCPartGroup { GroupName="HDD" },
                });
                await context.SaveChangesAsync();
            }
            // проверка наличия объектов
            if (!context.PCParts.Any())
            {
                context.PCParts.AddRange(
                new List<PCPart>
                {
                    new PCPart { PCPartName = "Процессор AMD Ryzen 5 3600",
                    Description="Matisse, AM4, 6 ядер, частота 4.2/3.6 ГГц, кэш 3 МБ + 32 МБ, техпроцесс 7 нм, TDP 65W",
                    Price = 475.00M, PCPartGroupId = 1, Image = "ryzen5.jpeg" },
                    new PCPart { PCPartName = "Процессор Intel Core i5-10400F",
                    Description="Comet Lake, LGA1200, 6 ядер, частота 4.3/2.9 ГГц, кэш 12 МБ, техпроцесс 14 нм, TDP 65W",
                    Price = 431.56M, PCPartGroupId = 1, Image = "corei5.jpeg" },
                    new PCPart { PCPartName = "SSD Crucial BX500 240GB",
                    Description="2.5\", SATA 3.0, контроллер Silicon Motion SM2258XT, микросхемы 3D TLC NAND, последовательный доступ: 540/500 MBps",
                    Price = 95.98M, PCPartGroupId = 2, Image = "crucialbx500.jpeg" },
                    new PCPart { PCPartName = "SSD Samsung 870 Evo 250GB",
                    Description="2.5\", SATA 3.0, контроллер Samsung MKX, микросхемы 3D TLC NAND, последовательный доступ: 560/530 MBps, случайный доступ: 98000/88000 IOps",
                    Price = 128.12M, PCPartGroupId = 2, Image = "samsung870evo.jpeg" },
                    new PCPart { PCPartName = "Жесткий диск Seagate Barracuda 2TB",
                    Description="3.5\", SATA 3.0 (6Gbps), 7200 об/мин, буфер 256 МБ",
                    Price = 157.20M, PCPartGroupId = 3, Image = "seagate.jpeg" }
                });
                await context.SaveChangesAsync();
            }
        }
    }
}
