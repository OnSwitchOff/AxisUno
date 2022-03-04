using AxisUno.Events;

namespace AxisUno.DataBase.Enteties.ProductGroups.Events

{
    public record ProductGroupDiscountChangedEvent(int ProductGroupId, decimal Discount) : DomainEventBase;
}

