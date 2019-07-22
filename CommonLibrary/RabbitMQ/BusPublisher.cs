using System.Text;
using System.Threading.Tasks;
using CommonLibrary.Messages;
using CommonLibrary.SeedOfWork;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace CommonLibrary.RabbitMQ
{
    public class BusPublisher : IBusPublisher
    {
        private readonly EndpointConfiguration _endpointConfiguration;
        private readonly IModel _model;

        public BusPublisher(EndpointConfiguration endpointConfiguration)
        {
            _endpointConfiguration = endpointConfiguration;
            _model = _endpointConfiguration.Model;
        }

        public Task Send<TCommand>(
            TCommand command,
            Enumeration commandType,
            ExchangeType exchangeType,
            string routingKey,
            bool durable = true,
            bool autoDelete = false,
            IBasicProperties properties = null)
            where TCommand : Command
        {
            var exchange = $"{_endpointConfiguration.InitialName}" +
                                  $".{command.GetType().Name}" +
                                  $".{commandType.Name}" +
                                  ".Command";

            _model.ExchangeDeclare(exchange, exchangeType.Name, durable, autoDelete);
            _model.BasicPublish(
                exchange,
                routingKey,
                properties,
                MessageToBytes(command));

            return Task.CompletedTask;
        }

        public Task Publish<TEvent>(
            TEvent @event,
            Enumeration eventType,
            bool autoDelete = true,
            IBasicProperties properties = null)
            where TEvent : Event
        {
            var exchange = $"{_endpointConfiguration.InitialName}" +
                           $".{@event.GetType().Name}" +
                                $".{eventType.Name}" +
                                ".Event";

            _model.ExchangeDeclare(exchange, "fanout", false, autoDelete);
            _model.BasicPublish(
                exchange,
                string.Empty,
                properties,
                MessageToBytes(@event));

            return Task.CompletedTask;
        }

        private static byte[] MessageToBytes<TMessage>(TMessage message)
            where TMessage : Message
        {
            var json = JsonConvert.SerializeObject(
                message,
                new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
            return Encoding.UTF8.GetBytes(json);
        }
    }
}
