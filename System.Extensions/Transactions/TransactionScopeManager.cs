using System.Threading.Tasks;

namespace System.Transactions
{
    public class TransactionScopeManager : IUnitOfWork
    {
        private TransactionScope scope;

        public async Task BeginTransaction()
        {
            scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            //scope = new TransactionScope(TransactionScopeOption.Required, new TimeSpan(0, 2, 0));

            await Task.CompletedTask;
        }

        public async Task Commit()
        {
            scope.Complete();

            scope.Dispose();

            scope = null;

            await Task.CompletedTask;
        }

        public async Task Rollback()
        {
            scope.Dispose();

            scope = null;

            await Task.CompletedTask;
        }
    }
}
