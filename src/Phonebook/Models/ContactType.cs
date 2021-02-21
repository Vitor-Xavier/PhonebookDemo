using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Phonebook.Models
{
    public class ContactType : BaseEntity
    {
        public int ContactTypeId { get; set; }

        [Required, StringLength(40)]
        public string Name { get; set; }

        [StringLength(80)]
        public string IconSource { get; set; }

        [JsonIgnore]
        public virtual ICollection<Contact> Contacts { get; set; }

        public override bool Equals(object obj) =>
            obj is ContactType contact && ContactTypeId == contact.ContactTypeId;

        public override int GetHashCode() => HashCode.Combine(ContactTypeId);
    }
}
