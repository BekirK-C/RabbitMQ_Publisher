// Create connection 
using RabbitMQ.Client;
using System.Text;

// Create connection 
ConnectionFactory factory = new();
factory.Uri = new Uri("amqps://xgtppcak:9E7tafT5XUlGRnKeOPZTnMn1L4-OtIvE@jackal.rmq.cloudamqp.com/xgtppcak");

// Activate connection and create channel
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

// Declare exchange
channel.ExchangeDeclare(exchange: "topic-exchange-example", type: ExchangeType.Topic);

// Take topic parameter from user
Console.WriteLine("Enter publisher topic: ");
string topic = Console.ReadLine();

// Send message
for (int i = 0; i < 100; i++)
{
    await Task.Delay(500);
    string message = $"Message {i}";
    byte[] body = Encoding.UTF8.GetBytes(message);
    channel.BasicPublish(exchange: "topic-exchange-example", routingKey: topic, body: body);
}

Console.Read();