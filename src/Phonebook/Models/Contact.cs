using System;
using System.ComponentModel.DataAnnotations;

namespace Phonebook.Models
{
    public class Contact : BaseEntity
    {
        public int ContactId { get; set; }

        [Required, StringLength(40)]
        public string Text { get; set; }

        public ContactType ContactType { get; set; }

        public int PersonId { get; set; }

        public virtual Person Person { get; set; }

        public override bool Equals(object obj) =>
            obj is Contact contact && ContactId == contact.ContactId;

        public override int GetHashCode() => HashCode.Combine(ContactId);
    }
}
