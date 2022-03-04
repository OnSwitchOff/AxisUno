using System;
using System.Collections.Generic;
using System.Text;

namespace AxisUno.Events
{
    public record DomainEventBase : IDomainEvent
    {
        /// <inheritdoc/>
        public DateTime OccurredOn
        {
            get => DateTime.Now;
        }
    }
}
