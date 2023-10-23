using System.Text;
using RabbitMQ.Client;


// Create connection 
ConnectionFactory factory = new();
factory.Uri = new Uri("amqps://xgtppcak:9E7tafT5XUlGRnKeOPZTnMn1L4-OtIvE@jackal.rmq.cloudamqp.com/xgtppcak");

// Activate connection and create channel
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

// Declare queue
channel.QueueDeclare(queue: "example-queue", exclusive: false);

// Send message to queue
for (int i = 0; i < 100; i++)
{
    Task.Delay(100).Wait();
    byte[] message = Encoding.UTF8.GetBytes($"{i}. Hello World");
    channel.BasicPublish(exchange: "", routingKey: "example-queue", body: message);
}

Console.Read();