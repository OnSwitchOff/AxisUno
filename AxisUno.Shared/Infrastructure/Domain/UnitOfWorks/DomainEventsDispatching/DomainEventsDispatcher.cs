using HarabaSourceGenerators.Common.Attributes;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AxisUno.Infrastructure.Domain.UnitOfWorks.DomainEventsDispatching;

/// <inheritdoc cref="IDomainEventsDispatcher"/>
[Inject]
public partial class DomainEventsDispatcher : IDomainEventsDispatcher
{
    private readonly IMediator _mediator;
    private readonly IDomainEventsProvider _domainEventsProvider;

    public async Task DispatchEventsAsync(CancellationToken cancellationToken = default)
    {
        var domainEvents = _domainEventsProvider.GetAllDomainEvents();
        _domainEventsProvider.ClearAllDomainEvents();

        foreach (var domainEvent in domainEvents)
        {
            await _mediator.Publish(domainEvent, cancellationToken);
        }
    }
}