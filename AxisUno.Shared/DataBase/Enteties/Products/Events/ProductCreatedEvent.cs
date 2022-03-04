using AxisUno.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace AxisUno.DataBase.Enteties.Products.Events
{
    public record ProductCreatedEvent(Product Product) : DomainEventBase;
}
