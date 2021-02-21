using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Phonebook.Models
{
    public class Person : BaseEntity
    {
        public int PersonId { get; set; }

        [Required, StringLength(40)]
        public string Name { get; set; }

        public DateTime BirthDate { get; set; }

        public int UserId { get; set; }

        [JsonIgnore]
        public virtual User User { get; set; }

        [JsonIgnore]
        public virtual ICollection<Contact> Contacts { get; set; }

        public override bool Equals(object obj) =>
            obj is Person person && PersonId == person.PersonId;

        public override int GetHashCode() => HashCode.Combine(PersonId);
    }
}
