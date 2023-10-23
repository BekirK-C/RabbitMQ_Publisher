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
channel.ExchangeDeclare(exchange: "fanout-exhange-example", type: ExchangeType.Fanout);

// Create queue
Console.WriteLine("Queue Name Please:");
string queueName = Console.ReadLine();
channel.QueueDeclare(queue: queueName, exclusive: false);

// Bind queue to exchange
channel.QueueBind(queue: queueName, exchange: "fanout-exhange-example", routingKey: string.Empty);

// Read message
EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(consumer: consumer, queue: queueName, autoAck: true);

consumer.Received += (sender, eventArgs) =>
{
    byte[] body = eventArgs.Body.ToArray();
    string message = Encoding.UTF8.GetString(body);
    Console.WriteLine(message);
};

Console.Read();