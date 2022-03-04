using AxisUno.Events;
using System.Collections.Generic;

namespace AxisUno.Infrastructure.Domain.UnitOfWorks.DomainEventsDispatching;

public interface IDomainEventsProvider
{
    IReadOnlyCollection<IDomainEvent> GetAllDomainEvents();

    void ClearAllDomainEvents();
}