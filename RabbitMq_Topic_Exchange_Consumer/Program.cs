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

// Declare exchange
channel.ExchangeDeclare(exchange: "topic-exchange-example", type: ExchangeType.Topic);

// Take topic parameter from user
Console.WriteLine("Enter consumer topic: ");
string topic = Console.ReadLine();

// Declare queue
string queueName = channel.QueueDeclare().QueueName;

// Bind queue to exchange
channel.QueueBind(queue: queueName, exchange: "topic-exchange-example", routingKey: topic);

// Read message
EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

consumer.Received += (sender, e) =>
{
    byte[] body = e.Body.ToArray();
    string message = Encoding.UTF8.GetString(body);
    Console.WriteLine($"Message received: {message}");
};


Console.Read();