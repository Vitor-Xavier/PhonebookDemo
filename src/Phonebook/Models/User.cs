using System;
using System.Collections.Generic;

namespace Phonebook.Models
{
    public class User : BaseEntity
    {
        public int UserId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string AvatarSource { get; set; }

        public virtual ICollection<Person> People { get; set; }

        public override bool Equals(object obj) =>
            obj is User user && UserId == user.UserId;

        public override int GetHashCode() => HashCode.Combine(UserId);
    }
}
