using System;

namespace Domain.Context.Entities
{
    public abstract class Entity 
    {
        public Entity()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }
    }
}