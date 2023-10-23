using System.Text;
using RabbitMQ.Client;


// Create connection 
ConnectionFactory factory = new();
factory.Uri = new Uri("amqps://xgtppcak:9E7tafT5XUlGRnKeOPZTnMn1L4-OtIvE@jackal.rmq.cloudamqp.com/xgtppcak");

// Activate connection and create channel
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

// Create Exchange
channel.ExchangeDeclare(exchange: "fanout-exhange-example", type: ExchangeType.Fanout);

// Send message
for (int i = 0; i < 100; i++)
{
    await Task.Delay(200);

    byte[] messageBody = Encoding.UTF8.GetBytes($"Message {i}");
    channel.BasicPublish(exchange: "fanout-exhange-example", routingKey: string.Empty, body: messageBody);
}

Console.Read();