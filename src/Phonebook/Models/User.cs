using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Phonebook.Models
{
    public class User : BaseEntity
    {
        public int UserId { get; set; }

        [Required, StringLength(40)]
        public string Name { get; set; }

        [Required, StringLength(40)]
        public string Username { get; set; }

        [Required, StringLength(60)]
        public string Email { get; set; }

        [Required, StringLength(80)]
        public string Password { get; set; }

        public string AvatarSource { get; set; }
        
        [NotMapped]
        public string Token { get; set; }

        public virtual ICollection<Person> People { get; set; }

        public override bool Equals(object obj) =>
            obj is User user && UserId == user.UserId;

        public override int GetHashCode() => HashCode.Combine(UserId);
    }
}
