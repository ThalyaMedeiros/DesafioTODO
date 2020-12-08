using System;

namespace Domain.Context.Entities
{
    public abstract class Entity 
    {
        public Entity()
        {
            Id = Guid.NewGuid().ToString().Replace("-","").Substring(0,8);
        }

        public string Id { get; private set; }
    }
}