using System.Collections.Generic;

namespace System.DomainModel
{
    public interface IAppendOnlyStore : IDisposable
    {
        void Append(string name, byte[] data, long expectedVersion = -1);

        IEnumerable<DataWithVersion> ReadRecords(string name, long afterVersion, long maxCount);

        IEnumerable<DataWithName> ReadRecords(long afterVersion, long maxCount);

        void Close();
    }

    public class DataWithVersion
    {
        public byte[] Data { get; }

        public long Version { get; }

        public DataWithVersion(byte[] data, long version)
        {
            Data = data;

            Version = version;
        }
    }

    public class DataWithName
    {
        public byte[] Data { get; }

        public string Name { get; }

        public DataWithName(byte[] data, string name)
        {
            Data = data;

            Name = name;
        }
    }
}
