namespace Ateliex
{
    public class EventStoreTable
    {
        public string Name { get; set; }

        public long Version { get; set; }

        public byte[] Data { get; set; }
    }
}
