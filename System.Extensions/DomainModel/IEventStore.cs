using System.Collections.Generic;

namespace System.DomainModel
{
    public interface IEventStore
    {
        IEnumerable<Event> LoadAllEvents();

        IEnumerable<Event> LoadEvents(IIdentity id);

        IEnumerable<Event> LoadEvents(IIdentity id, long skipEvents, long maxCount = 0);

        void AppendToStream(IIdentity id, long expectedVersion, IEnumerable<Event> events);
    }

    public class EventStoreConcurrencyException : Exception
    {
        /// <summary>
        /// Actual Events.
        /// </summary>
        public Event[] StoreEvents { get; set; }

        /// <summary>
        /// Actual Version.
        /// </summary>
        public long StoreVersion { get; set; }
    }

    public class RealConcurrencyException : Exception
    {
        public RealConcurrencyException(EventStoreConcurrencyException ex)
            : base(null, ex)
        {

        }
    }
}
