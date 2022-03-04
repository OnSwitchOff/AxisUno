using AxisUno.DataBase;
using AxisUno.DataBase.Enteties;
using AxisUno.Events;
using HarabaSourceGenerators.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AxisUno.Infrastructure.Domain.UnitOfWorks.DomainEventsDispatching
{
    [Inject]
    public sealed partial class DomainEventsProvider : IDomainEventsProvider
    {
        private readonly DatabaseContext _context;

        public void ClearAllDomainEvents()
        {
            var domainEntities = _context.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents is not null && x.Entity.DomainEvents.Any()).ToList();

            domainEntities
                .ForEach(entity => entity.Entity.ClearDomainEvents());
        }

        public IReadOnlyCollection<IDomainEvent> GetAllDomainEvents()
        {
            var domainEntities = _context.ChangeTracker
                    .Entries<Entity>()
                    .Where(x => x.Entity.DomainEvents is not null && x.Entity.DomainEvents.Any()).ToList();

            return domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();
        }
    }
}
