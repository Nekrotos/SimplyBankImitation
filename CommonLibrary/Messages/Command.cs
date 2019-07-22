using MediatR;

namespace CommonLibrary.Messages
{
    public abstract class Command<TResponse>
        : Message, IRequest<TResponse>
    {
    }

    public abstract class Command 
        : Message, IRequest
    {
    }
}