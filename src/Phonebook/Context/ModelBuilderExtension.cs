using Microsoft.EntityFrameworkCore;
using System;

namespace Phonebook.Context
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Person>().HasData(
                new Models.Person
                {
                    PersonId = 1,
                    Name = "Vitor Xavier de Souza",
                    BirthDate = new DateTime(1997, 01, 06)
                });

            modelBuilder.Entity<Models.Contact>().HasData(
                new Models.Contact
                {
                    ContactId = 1,
                    PersonId = 1,
                    Text = "vitorvxs@live.com",
                    ContactType = Models.ContactType.Email
                });
        }
    }
}
