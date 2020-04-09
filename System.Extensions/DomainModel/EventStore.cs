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

        public IEnumerable<IEvent> GetAllEvents()
        {
            var records = appendOnlyStore.ReadRecords(0, long.MaxValue);

            var events = new List<IEvent>();

            foreach (var tapeRecord in records)
            {
                events.AddRange(DesserializeEvent(tapeRecord.Data));
            }

            return events;
        }

        public EventStream LoadEventStream(IIdentity id)
        {
            throw new NotImplementedException();
        }

        public EventStream LoadEventStream(IIdentity id, long skip, long take)
        {
            var name = IdentityToString(id);

            var records = appendOnlyStore.ReadRecords(name, skip, take).ToList();

            var stream = new EventStream();

            foreach (var tapeRecord in records)
            {
                stream.Events.AddRange(DesserializeEvent(tapeRecord.Data));

                stream.Version = tapeRecord.Version;
            }

            return stream;
        }

        private IEvent[] DesserializeEvent(byte[] data)
        {
            using (var stream = new MemoryStream(data))
            {
                return (IEvent[])formatter.Deserialize(stream);
            }
        }

        public void AppendToStream(IIdentity id, long originalVersion, ICollection<IEvent> events)
        {
            if (events.Count == 0)
            {
                return;
            }

            var name = IdentityToString(id);

            var data = SerializeEvent(events.ToArray());

            try
            {
                appendOnlyStore.Append(name, data, originalVersion);
            }
            catch (AppendOnlyStoreConcurrencyException ex)
            {
                var server = LoadEventStream(id, 0, long.MaxValue);

                throw OptimisticConcurrencyException.Create(server.Version, ex.ExpectedVersion, id, server.Events);
            }
        }

        private byte[] SerializeEvent(IEvent[] events)
        {
            using (var stream = new MemoryStream())
            {
                formatter.Serialize(stream, events);

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
        public static OptimisticConcurrencyException Create(long serverVersion, long expectedVersion, IIdentity id, IEnumerable<IEvent> serverEvents)
        {
            return new OptimisticConcurrencyException();
        }
    }
}
