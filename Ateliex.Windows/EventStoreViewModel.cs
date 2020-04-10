using System;

namespace Ateliex
{
    public class EventStoreViewModel
    {
        public string Name { get; set; }

        public long Version { get; set; }

        public DateTime Date { get; set; }

        public object Data { get; set; }

        public int DataLength { get; set; }
    }
}
