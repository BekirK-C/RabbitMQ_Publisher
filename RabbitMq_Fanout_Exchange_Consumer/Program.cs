using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

// Create connection 
ConnectionFactory factory = new();
factory.Uri = new Uri("amqps://xgtppcak:9E7tafT5XUlGRnKeOPZTnMn1L4-OtIvE@jackal.rmq.cloudamqp.com/xgtppcak");

// Activate connection and create channel
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

// Exact ExchangeDeclare from publisher
channel.ExchangeDeclare(exchange: "direct-exchange-example", type: ExchangeType.Direct);

// Create queue
var queueName = channel.QueueDeclare().QueueName;

// Bind queue to exchange
channel.QueueBind(queue: queueName, exchange: "direct-exchange-example", routingKey: "direct-queue-example");

// Create consumer
EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

// Handle message
consumer.Received += (sender, eventArgs) =>
{
    var message = Encoding.UTF8.GetString(eventArgs.Body.ToArray());
    Console.WriteLine($"Message received: {message}");
};

Console.Read();