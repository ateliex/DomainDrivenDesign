using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.DomainModel
{
    public interface IRepository<TAggregateRoot> where TAggregateRoot : AggregateRoot
    {
        Task AddAsync(TAggregateRoot entity);

        Task UpdateAsync(TAggregateRoot entity);

        Task RemoveAsync(TAggregateRoot entity);
    }
}
