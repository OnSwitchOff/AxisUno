using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AxisUno.Infrastructure.Domain.UnitOfWorks
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// Logical finishing of a domain operation.
        /// <para>By default, it rises all the domain events from domain entity objects. And saves changes, made in the repositories.</para>
        /// </summary>
        /// <param name="cancellationToken">Cancellation token for operation discarding.</param>
        /// <returns>Status of operation.</returns>
        Task<int> CommitAsync(CancellationToken cancellationToken = default);
    }
}
