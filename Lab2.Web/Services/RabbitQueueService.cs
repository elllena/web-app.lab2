using Lab2.Web.Services.Abstraction;

using System.Text;
using System.Text.Json;

using RabbitMQ.Client;

namespace Lab2.Web.Services
{
    public class RabbitQueueService : IQueueService, IDisposable
    {
        private readonly ILogger _logger;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitQueueService(IConfiguration configuration, ILogger<RabbitQueueService> logger)
        {
            _logger = logger;
            var factory = new ConnectionFactory()
            {
                Uri = new Uri(configuration.GetConnectionString("KnifesMq")!)
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "knifes", durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        public Task Enqueue<T>(T item, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var message = JsonSerializer.Serialize(item);

            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: "",
                           routingKey: "knifes",
                           basicProperties: null,
                           body: body);

            _logger.LogInformation("Message was succesfully published: {message}", message);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _channel.Close();
            _connection.Close();
        }
    }
}
