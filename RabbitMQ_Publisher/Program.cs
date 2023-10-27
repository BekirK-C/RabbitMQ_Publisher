using RabbitMQ.Client;
using System.Text;

// Create connection 
ConnectionFactory factory = new();
factory.Uri = new Uri("amqps://xgtppcak:9E7tafT5XUlGRnKeOPZTnMn1L4-OtIvE@jackal.rmq.cloudamqp.com/xgtppcak");

// Activate connection and create channel
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

// Create queue
channel.QueueDeclare(queue: "example-queue");

// Send message
for (int i = 0; i < 100; i++)
{
    await Task.Delay(200);
    byte[] message = Encoding.UTF8.GetBytes("Hello World: " + i);
    channel.BasicPublish(exchange: string.Empty, routingKey: "example-queue", body: message);
}

Console.Read();