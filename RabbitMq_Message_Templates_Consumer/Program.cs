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

//// Read message
//var consumer = new EventingBasicConsumer(channel);
//channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
//consumer.Received += (sender, e) =>
//{
//    var body = e.Body.ToArray();
//    var message = Encoding.UTF8.GetString(body);
//    Console.WriteLine($"Message received: {message}");
//};

#endregion

#region Publish/Subscribe (Pub/Sub) Template

//// Declare exchange
//var exchangeName = "PubSub_Exchange";
//channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Fanout);

//// Create queue
//var queueName2 = channel.QueueDeclare().QueueName;

//// Bind queue to exchange
//channel.QueueBind(queue: queueName2, exchange: exchangeName, routingKey: string.Empty);

//// Read message
//var consumer2 = new EventingBasicConsumer(channel);
//channel.BasicConsume(queue: queueName2, autoAck: true, consumer: consumer2);
//consumer2.Received += (sender, e) =>
//{
//    var body = e.Body.ToArray();
//    var message = Encoding.UTF8.GetString(body);
//    Console.WriteLine($"Message received: {message}");
//};

#endregion

#region Work Queue Template

//// Declare queue
//string queueName3 = "WorkQueue_Queue";
//channel.QueueDeclare(queue: queueName3, durable: false, exclusive: false, autoDelete: false);

//// scaling
//channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

//// Read message
//var consumer3 = new EventingBasicConsumer(channel);
//channel.BasicConsume(queue: queueName3, autoAck: true, consumer: consumer3);
//consumer3.Received += (sender, e) =>
//{
//    var body = e.Body.ToArray();
//    var message = Encoding.UTF8.GetString(body);
//    Console.WriteLine($"Message received: {message}");
//};

#endregion

#region Request/Response Template

// Declare request queue 
var requestQueueName = "request-response-queueu";
channel.QueueDeclare(queue: requestQueueName, durable: false, exclusive: false, autoDelete: false);

// Read message
var consumer4 = new EventingBasicConsumer(channel);
channel.BasicConsume(queue: requestQueueName, autoAck: true, consumer: consumer4);
consumer4.Received += (sender, e) =>
{
    var body = e.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($"Message received: {message}");

    // Declare response queue
    var responseQueueName = channel.QueueDeclare().QueueName;

    // Corelation ID
    var correlationId = e.BasicProperties.CorrelationId;

    // Create properties
    IBasicProperties properties = channel.CreateBasicProperties();
    properties.ReplyTo = responseQueueName;
    properties.CorrelationId = correlationId;

    // Create response message
    byte[] body4 = Encoding.UTF8.GetBytes($"Mission Completed! {message}");
    channel.BasicPublish(exchange: string.Empty, routingKey: responseQueueName, basicProperties: properties, body: body4);
};

#endregion


Console.Read();