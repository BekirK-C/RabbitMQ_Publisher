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
channel.ExchangeDeclare(exchange: "header-exchange-example", type: ExchangeType.Headers);

// Take header parameters from user
Console.WriteLine("Enter consumer header parameters: ");
string value = Console.ReadLine()!;

// Create queue
string queueName = channel.QueueDeclare().QueueName;

// Create binding
channel.QueueBind(queue: queueName, exchange: "header-exchange-example", routingKey: string.Empty, arguments: new Dictionary<string, object>()
{
    ["yes"] = value
});

// Create consumer
EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

// Receive message
consumer.Received += (sender, eventArgs) =>
{
    byte[] body = eventArgs.Body.ToArray();
    string message = Encoding.UTF8.GetString(body);
    Console.WriteLine(message);
};

Console.Read();