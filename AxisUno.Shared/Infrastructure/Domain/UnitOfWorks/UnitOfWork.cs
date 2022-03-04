using HarabaSourceGenerators.Common.Attributes;
using AxisUno.Infrastructure.Domain.UnitOfWorks.DataStorage;
using AxisUno.Infrastructure.Domain.UnitOfWorks.DomainEventsDispatching;
using System.Threading;
using System.Threading.Tasks;

namespace AxisUno.Infrastructure.Domain.UnitOfWorks;

[Inject]
public partial class UnitOfWork : IUnitOfWork
{
    private readonly IDataStorage _dataStorageDispatcher;
    private readonly IDomainEventsDispatcher _domainEventsDispatcher;

    public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
    {
        await _domainEventsDispatcher.DispatchEventsAsync(cancellationToken);

        return await _dataStorageDispatcher.SaveChangesAsync(cancellationToken);
    }
}