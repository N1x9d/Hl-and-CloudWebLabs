using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.Hosting;
using System.Text;
using System.Diagnostics;
using System;

namespace WebApplicationLab.RabbitMQ
{
  public class RabbitMqListener : BackgroundService
  {
    private IConnection _connection;
    private IModel _channel;

    public RabbitMqListener()
    {
      // Не забудьте вынести значения "localhost" и "MyQueue"
      // в файл конфигурации
      var factory = new ConnectionFactory { HostName = "localhost" };
      _connection = factory.CreateConnection();
      _channel = _connection.CreateModel();
      _channel.QueueDeclare(queue: "MyQueue", durable: false, exclusive: false, autoDelete: false, arguments: null);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
      stoppingToken.ThrowIfCancellationRequested();

      var consumer = new EventingBasicConsumer(_channel);
      consumer.Received += (ch, ea) =>
      {
        var content = Encoding.UTF8.GetString(ea.Body.ToArray());
        Thread.Sleep(5000);
        // Каким-то образом обрабатываем полученное сообщение
        Console.WriteLine($"Получено сообщение: {content} обработано в {DateTime.Now.ToString()}");

        _channel.BasicAck(ea.DeliveryTag, false);
      };

      _channel.BasicConsume("MyQueue", false, consumer);

      return Task.CompletedTask;
    }

    public override void Dispose()
    {
      _channel.Close();
      _connection.Close();
      base.Dispose();
    }
  }
}
