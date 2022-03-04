using AxisUno.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace AxisUno.DataBase.Enteties.Operations.Events
{
    public record SaveOperationEvent(Operation Operation) : DomainEventBase;
}
