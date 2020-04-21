using System.Threading.Tasks;

namespace System.Transactions
{
    public class DummyUnitOfWork : IUnitOfWork
    {
        public async Task BeginTransaction()
        {
            await Task.Run(() => { });
        }

        public async Task Commit()
        {
            await Task.Run(() => { });
        }

        public async Task Rollback()
        {
            await Task.Run(() => { });
        }
    }
}
