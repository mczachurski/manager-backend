using System;

namespace SunLine.Manager.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }

        public int Version { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime? ModificationDate { get; set; }

        public override string ToString()
        {
            return Id.ToString();
        }
    }
}