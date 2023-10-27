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

// Declare queue
channel.QueueDeclare(queue: "example-queue", exclusive: false, durable: true, autoDelete: false);

// Declare properties
IBasicProperties properties = channel.CreateBasicProperties();
properties.Persistent = true;

// Read message from queue
EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(queue: "example-queue", autoAck: true, consumer: consumer);
consumer.Received += (sender, eventArgs) =>
{
    byte[] body = eventArgs.Body.ToArray();
    string message = Encoding.UTF8.GetString(body);
    Console.WriteLine(message);
};

Console.Read();