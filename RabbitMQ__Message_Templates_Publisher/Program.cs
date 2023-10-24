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

#region P2P (Point-to-Point) Template

//// Declare queue
//var queueName = "P2P_Queue";
//channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false);

//// Create message
//byte[] body = Encoding.UTF8.GetBytes("Hello World!");
//channel.BasicPublish(exchange: string.Empty, routingKey: queueName, body: body);

#endregion

#region Publish/Subscribe (Pub/Sub) Template

//// Declare exchange
//var exchangeName = "PubSub_Exchange";
//channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Fanout);

//// Create message
//for (int i = 0; i < 100; i++)
//{
//    await Task.Delay(300);
//    byte[] body2 = Encoding.UTF8.GetBytes($"Hello World! {i}");
//    channel.BasicPublish(exchange: exchangeName, routingKey: string.Empty, body: body2);
//}

#endregion

#region Work Queue Template

//// Declare queue
//string queueName3 = "WorkQueue_Queue";
//channel.QueueDeclare(queue: queueName3, durable: false, exclusive: false, autoDelete: false);

//// Create message
//for (int i = 0; i < 100; i++)
//{
//    await Task.Delay(300);
//    byte[] body3 = Encoding.UTF8.GetBytes($"Hello World! {i}");
//    channel.BasicPublish(exchange: string.Empty, routingKey: queueName3, body: body3);
//}

#endregion

#region Request/Response Template

// Declare request queue 
var requestQueueName = "request-response-queueu";
channel.QueueDeclare(queue: requestQueueName, durable: false, exclusive: false, autoDelete: false);

// Declare response queue
var responseQueueName = channel.QueueDeclare().QueueName;

// Corelation ID
var correlationId = Guid.NewGuid().ToString();

// Create properties
IBasicProperties properties = channel.CreateBasicProperties();
properties.ReplyTo = responseQueueName;
properties.CorrelationId = correlationId;

// Create message
for (int i = 0; i < 100; i++)
{
    await Task.Delay(300);
    byte[] body4 = Encoding.UTF8.GetBytes($"Hello World! {i}");
    channel.BasicPublish(exchange: string.Empty, routingKey: requestQueueName, basicProperties: properties, body: body4);
}

// Read response
var consumer4 = new EventingBasicConsumer(channel);
channel.BasicConsume(queue: responseQueueName, autoAck: true, consumer: consumer4);
consumer4.Received += (sender, e) =>
{
    if (e.BasicProperties.CorrelationId == correlationId)
    {
        var body = e.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        Console.WriteLine($"Message received: {message}");
    }
};

#endregion


Console.Read();