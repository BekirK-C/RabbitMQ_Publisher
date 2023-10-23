using RabbitMQ.Client;
using System.Text;


// Create connection 
ConnectionFactory factory = new();
factory.Uri = new Uri("amqps://xgtppcak:9E7tafT5XUlGRnKeOPZTnMn1L4-OtIvE@jackal.rmq.cloudamqp.com/xgtppcak");

// Activate connection and create channel
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

// Create exchange
channel.ExchangeDeclare(exchange: "direct-exchange-example", type: ExchangeType.Direct);

// Send message
while (true)
{
    Console.WriteLine("Enter your message: ");

    var message = Console.ReadLine();

    var body = Encoding.UTF8.GetBytes(message!);

    channel.BasicPublish(exchange: "direct-exchange-example", routingKey: "direct-queue-example", body: body);

    Console.WriteLine("Message sent!");
}

Console.Read();