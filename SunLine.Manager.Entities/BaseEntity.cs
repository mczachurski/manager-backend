using System;

namespace SunLine.Manager.Entities
{
    public abstract class BaseEntity
    {
        public virtual int Id { get; set; }

        public virtual int Version { get; set; }

        public virtual DateTime CreationDate { get; set; }

        public virtual DateTime? ModificationDate { get; set; }

        public override string ToString()
        {
            return Id.ToString();
        }
    }
}