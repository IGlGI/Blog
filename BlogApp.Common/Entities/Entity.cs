using System;

namespace BlogApp.Common.Entities
{
    public abstract record Entity<T>
    {
        public virtual T Id { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }
    }
}
