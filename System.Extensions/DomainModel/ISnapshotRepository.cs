namespace System.DomainModel
{
    public interface ISnapshotRepository
    {
        bool TryGetSnapshotById<TAggregate>(IIdentity id, out TAggregate snapshot, out long version);

        void SaveSnapshot<TAggregate>(IIdentity id, TAggregate snapshot, long version);
    }
}
