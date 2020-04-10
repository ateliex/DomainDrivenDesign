namespace System.DomainModel
{
    public class EventRecord
    {
        public string Name { get; }

        public long Version { get; }

        public DateTime Date { get; }

        public byte[] Data { get; }

        public EventRecord(string name, long version, DateTime date, byte[] data)
        {
            Name = name;

            Version = version;

            Date = date;

            Data = data;
        }

        private EventRecord()
        {

        }
    }
}
