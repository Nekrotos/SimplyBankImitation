using CommonLibrary.SeedOfWork;

namespace CommonLibrary.RabbitMQ
{
    public abstract class ExchangeType : Enumeration 
    {
        public static ExchangeType Fanout = new FanoutType();
        public static ExchangeType Direct = new DirectType();
        public static ExchangeType Topic = new TopicType();

        protected ExchangeType(int id, string name) : base(id, name)
        {
        }

        private class FanoutType : ExchangeType
        {
            public FanoutType() : base(1, "fanout")
            {
            }
        }

        private class DirectType : ExchangeType
        {
            public DirectType() : base(2, "direct")
            {
            }
        }

        private class TopicType : ExchangeType
        {
            public TopicType() : base(3, "topic")
            {
            }
        }
    }
}
