using Lab2.Entities;
using Lab2.Worker.Dal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

using System.Text;
using System.Text.Json;

namespace Lab2.Worker
{
    public class QueueWorker : BackgroundService
    {
        private readonly ILogger _logger;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IServiceScope _scope;

        public QueueWorker(IConfiguration configuration, IServiceProvider provider, ILogger<QueueWorker> logger)
        {
            _logger = logger;
            _scope = provider.CreateScope();

            var factory = new ConnectionFactory()
            {
                Uri = new Uri(configuration.GetConnectionString("KnifesMq")!)
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "knifes", durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var dbContext = _scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (ch, ea) =>
            {
                var message = Encoding.UTF8.GetString(ea.Body.ToArray());
                _logger.LogInformation("Recieved message: {message}", message);
                _channel.BasicAck(ea.DeliveryTag, false);

                var knife = JsonSerializer.Deserialize<Knife>(message)!;

                try
                {
                    if (!dbContext.Knifes.Any(p => p.Name == knife.Name))
                    {
                        dbContext.Add(knife);
                    }
                    else
                    {
                        dbContext.Update(knife);
                    }
                    dbContext.SaveChanges();
                }
                catch (Exception ex) 
                {
                    _logger.LogError(ex, "Unable to save knife to the database");
                }
            };

            _channel.BasicConsume("knifes", false, consumer);

            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            _scope.Dispose();
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}