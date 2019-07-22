using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace CommonLibrary.Messages
{
    public abstract class EventHandler 
        : INotificationHandler<Event>
    {
        public abstract Task Handle(
            Event notification, 
            CancellationToken token);
    }
}