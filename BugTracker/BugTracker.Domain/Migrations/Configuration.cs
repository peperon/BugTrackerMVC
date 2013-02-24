namespace BugTracker.Domain.Migrations
{
    using BugTracker.Domain.Models;
    using BugTracker.Domain.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BugTracker.Domain.RepositoryConcrete.EFDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BugTracker.Domain.RepositoryConcrete.EFDbContext context)
        {
            var user = new User
             {
                 UserId = 0,
                 UserName = "admin",
                 Password = PasswordUtility.HashPassword("12adminpass21"),
                 Email = "pekostov@gmail.com",
                 FirstName = "Peter",
                 LastName = "Kostov",
                 Phone = "08236652",
                 Role = (int)UserRole.Admin,
             };
            if (context.Users.FirstOrDefault(u => u.UserName == user.UserName) == null)
                context.Users.Add(user);
        }
    }
}
