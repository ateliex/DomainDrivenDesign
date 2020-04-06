using System.Collections.Generic;

namespace System.DomainModel
{
    public abstract class Entity
    {
        public long Version { get; set; }

        public ICollection<IEvent> Changes { get; private set; }

        public Entity()
        {
            Changes = new HashSet<IEvent>();
        }

        public void Replay(IEnumerable<IEvent> events)
        {
            foreach (var @event in events)
            {
                Mutate(@event);
            }
        }

        public void Apply(IEvent e)
        {
            Changes.Add(e);

            Mutate(e);
        }

        protected void Mutate(IEvent e)
        {
            ((dynamic)this).When((dynamic)e);
        }
    }
}
