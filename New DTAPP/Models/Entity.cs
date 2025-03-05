using System.Diagnostics;

namespace New_DTAPP.Models
{
    public abstract class Entity<TEntityId>
    {
        protected Entity(TEntityId id)
        {
            Id = id;
        }

        public TEntityId? Id { get; init; }

        protected Entity () 
        { }

    }
}
