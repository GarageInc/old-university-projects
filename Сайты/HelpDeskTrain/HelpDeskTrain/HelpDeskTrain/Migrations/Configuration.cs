using HelpDeskTrain.Models;

namespace HelpDeskTrain.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<HelpDeskTrain.Models.HelpdeskContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(HelpDeskTrain.Models.HelpdeskContext context)
        {
            context.Roles.Add( new Role { Name = "Администратор" });
            context.Roles.Add(new Role { Name = "Модератор" });
            context.Roles.Add(new Role { Name = "Исполнитель" });
            context.Roles.Add( new Role { Name = "Пользователь" });

            context.Departments.Add(new Department{Name="Департамент контроля сайта"});

            context.SaveChanges();

            var role = context.Roles.First(x => x.Name == "Администратор");

            var department = context.Departments.First();

            context.Users.Add(new User
            {
                Login = "admin@admin.ru",
                Name = "Администратор сайта",
                Password = "admin@admin.ru",
                Role = role,
                RoleId = role.Id,
                Department = department,
                DepartmentId = department.Id
            });

            context.SaveChanges();
        }
    }
}
