using MediatR;

namespace CommonLibrary.Messages
{
    public abstract class Event
        : Message, INotification
    {
    }
}