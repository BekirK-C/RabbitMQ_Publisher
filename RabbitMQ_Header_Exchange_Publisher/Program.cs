// Create connection 
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

// Create connection 
ConnectionFactory factory = new();
factory.Uri = new Uri("amqps://xgtppcak:9E7tafT5XUlGRnKeOPZTnMn1L4-OtIvE@jackal.rmq.cloudamqp.com/xgtppcak");

// Activate connection and create channel
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

// Create Exchange
channel.ExchangeDeclare(exchange: "header-exchange-example", type: ExchangeType.Headers);

// Take header parameters from user
Console.WriteLine("Enter publisher header parameters: ");
string value = Console.ReadLine()!;

// Create properties
IBasicProperties properties = channel.CreateBasicProperties();
properties.Headers = new Dictionary<string, object>()
{
    ["yes"] = value
};

// Create message
for (int i = 0; i < 100; i++)
{
    await Task.Delay(500);
    string message = $"Message {i}";
    byte[] body = Encoding.UTF8.GetBytes(message);
    channel.BasicPublish(exchange: "header-exchange-example", routingKey: string.Empty, body: body);
}

Console.Read();