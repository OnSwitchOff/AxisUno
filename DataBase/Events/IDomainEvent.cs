using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace AxisUno.Events
{
    public interface IDomainEvent : INotification
    {
        /// <summary>
        /// Date and time, when the event was raised.
        /// </summary>
        DateTime OccurredOn { get; }
    }
}
