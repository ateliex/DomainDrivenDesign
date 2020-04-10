using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.DomainModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Ateliex
{
    public class EventStoreViewModelCollection : ObservableCollection<EventStoreViewModel>
    {
        private readonly EventStoreDbContext db;

        private readonly BinaryFormatter formatter = new BinaryFormatter();

        public EventStoreViewModelCollection(EventStoreDbContext db)
        {
            this.db = db;

            var events = db.EventStore;

            foreach (var eventRecord in events)
            {
                var buffer = new StringBuilder();

                var @event = DesserializeEvent(eventRecord.Data);

                var eventType = @event.GetType();

                buffer.AppendLine(eventType.Name);

                var eventProperties = eventType.GetProperties();

                foreach (var eventProperty in eventProperties)
                {
                    buffer.AppendLine($"{eventProperty.Name}: {eventProperty.GetValue(@event)}");
                }

                var item = new EventStoreViewModel
                {
                    Name = eventRecord.Name,
                    Version = eventRecord.Version,
                    Date = eventRecord.Date,
                    Data = buffer.ToString(),
                    DataLength = eventRecord.Data.Length
                };

                Items.Add(item);
            }

            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        private Event DesserializeEvent(byte[] data)
        {
            using (var stream = new MemoryStream(data))
            {
                return (Event)formatter.Deserialize(stream);
            }
        }
    }
}
