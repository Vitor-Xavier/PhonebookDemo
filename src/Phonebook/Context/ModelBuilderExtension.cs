using Microsoft.EntityFrameworkCore;
using System;

namespace Phonebook.Context
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.ContactType>().HasData(
                new Models.ContactType
                {
                    ContactTypeId = 1,
                    Name = "Email",
                    Deleted = false,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                });

                modelBuilder.Entity<Models.ContactType>().HasData(
                new Models.ContactType
                {
                    ContactTypeId = 2,
                    Name = "Cellphone",
                    Deleted = false,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                });

            modelBuilder.Entity<Models.ContactType>().HasData(
                new Models.ContactType
                {
                    ContactTypeId = 3,
                    Name = "Telephone",
                    Deleted = false,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                });

            modelBuilder.Entity<Models.ContactType>().HasData(
                new Models.ContactType
                {
                    ContactTypeId = 4,
                    Name = "Discord",
                    Deleted = false,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                });

            modelBuilder.Entity<Models.ContactType>().HasData(
                new Models.ContactType
                {
                    ContactTypeId = 5,
                    Name = "Skype",
                    Deleted = false,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                });

            modelBuilder.Entity<Models.Person>().HasData(
                new Models.Person
                {
                    PersonId = 1,
                    Name = "Vitor Xavier de Souza",
                    BirthDate = new DateTime(1997, 01, 06),
                    Deleted = false,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                });

            modelBuilder.Entity<Models.Contact>().HasData(
                new Models.Contact
                {
                    ContactId = 1,
                    PersonId = 1,
                    Text = "vitorvxs@live.com",
                    ContactTypeId = 1,
                    Deleted = false,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                });
        }
    }
}
