using System;

namespace BogApp.Models
{
    public abstract class Entity<T>
    {
        public virtual T Id { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }
    }
}
