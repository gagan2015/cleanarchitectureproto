using TopupPortal.Domain.Common;
using TopupPortal.Domain.Entities;

namespace TopupPortal.Domain.Events
{
    public class TodoItemCompletedEvent : DomainEvent
    {
        public TodoItemCompletedEvent(Product item)
        {
            Item = item;
        }

        public Product Item { get; }
    }
}
