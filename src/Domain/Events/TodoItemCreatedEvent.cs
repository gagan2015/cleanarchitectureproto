using TopupPortal.Domain.Common;
using TopupPortal.Domain.Entities;

namespace TopupPortal.Domain.Events
{
    public class TodoItemCreatedEvent : DomainEvent
    {
        public TodoItemCreatedEvent(Product item)
        {
            Item = item;
        }

        public Product Item { get; }
    }
}
