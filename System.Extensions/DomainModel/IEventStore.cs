using System.Collections.Generic;

namespace System.DomainModel
{
    public interface IEventStore
    {
        EventStream LoadEventStream(IIdentity id);

        EventStream LoadEventStream(IIdentity id, long skipEvents, long maxCount = 0);

        IEnumerable<IEvent> GetAllEvents();

        void AppendToStream(IIdentity id, long expectedVersion, ICollection<IEvent> events);
    }

    public class EventStream
    {
        public long Version;

        public List<IEvent> Events = new List<IEvent>();
    }

    public class EventStoreConcurrencyException : Exception
    {
        /// <summary>
        /// Actual Events.
        /// </summary>
        public List<IEvent> StoreEvents { get; set; }

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
