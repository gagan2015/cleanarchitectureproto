using System.Threading.Tasks;
using TopupPortal.Domain.Common;

namespace TopupPortal.Application.Common.Interfaces
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}
