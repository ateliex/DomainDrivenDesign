using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace System.DomainModel
{
    public class EventStore : IEventStore
    {
        private readonly IAppendOnlyStore appendOnlyStore;

        private readonly BinaryFormatter formatter = new BinaryFormatter();

        public EventStore(IAppendOnlyStore appendOnlyStore)
        {
            this.appendOnlyStore = appendOnlyStore;
        }

        public IEnumerable<Event> LoadAllEvents()
        {
            var records = appendOnlyStore.ReadRecords(0, long.MaxValue);

            var events = new List<Event>();

            foreach (var tapeRecord in records)
            {
                var @event = DesserializeEvent(tapeRecord.Data);

                @event.Version = tapeRecord.Version;

                @event.Date = tapeRecord.Date;

                events.Add(@event);
            }

            return events;
        }

        public IEnumerable<Event> LoadEvents(IIdentity id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Event> LoadEvents(IIdentity id, long skip, long take)
        {
            var name = IdentityToString(id);

            var records = appendOnlyStore.ReadRecords(name, skip, take).ToList();

            var events = new List<Event>();

            foreach (var tapeRecord in records)
            {
                var @event = DesserializeEvent(tapeRecord.Data);

                @event.Version = tapeRecord.Version;

                @event.Date = tapeRecord.Date;

                events.Add(@event);
            }

            return events;
        }

        private Event DesserializeEvent(byte[] data)
        {
            using (var stream = new MemoryStream(data))
            {
                return (Event)formatter.Deserialize(stream);
            }
        }

        public void AppendToStream(IIdentity id, long originalVersion, IEnumerable<Event> events)
        {
            if (!events.Any())
            {
                return;
            }

            var name = IdentityToString(id);

            var expectedVersion = originalVersion;

            foreach (var @event in events)
            {
                var data = SerializeEvent(@event);

                try
                {
                    appendOnlyStore.Append(name, @event.Date, data, expectedVersion);

                    expectedVersion++;
                }
                catch (AppendOnlyStoreConcurrencyException ex)
                {
                    var serverEvents = LoadEvents(id, 0, long.MaxValue);

                    var lastEvent = serverEvents.Last();

                    throw OptimisticConcurrencyException.Create(lastEvent.Version, ex.ExpectedVersion, id, serverEvents);
                }
            }
        }

        private byte[] SerializeEvent(Event @event)
        {
            using (var stream = new MemoryStream())
            {
                formatter.Serialize(stream, @event);

                return stream.ToArray();
            }
        }

        private string IdentityToString(IIdentity id)
        {
            return id.ToString();
        }
    }

    public class AppendOnlyStoreConcurrencyException : Exception
    {
        public long Version { get; }

        public long ExpectedVersion { get; }

        public string Name { get; }

        public AppendOnlyStoreConcurrencyException(long version, long expectedVersion, string name)
        {
            Version = version;

            ExpectedVersion = expectedVersion;

            Name = name;
        }
    }

    public class OptimisticConcurrencyException : Exception
    {
        public static OptimisticConcurrencyException Create(long serverVersion, long expectedVersion, IIdentity id, IEnumerable<Event> serverEvents)
        {
            return new OptimisticConcurrencyException();
        }
    }
}
