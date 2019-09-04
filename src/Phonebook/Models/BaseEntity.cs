using System;

namespace Phonebook.Models
{
    public class BaseEntity
    {
        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public bool Deleted { get; set; }
    }
}
