using MassTransit;
using RabbitMQ_ESB_MassTransit_Shared.Messages;

string rabbitMQUri = "amqps://xgtppcak:9E7tafT5XUlGRnKeOPZTnMn1L4-OtIvE@jackal.rmq.cloudamqp.com/xgtppcak";

string queueName = "example-queue";

var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
{
    cfg.Host(rabbitMQUri);
});

var sendEndPoint = await bus.GetSendEndpoint(new Uri($"{rabbitMQUri}/{queueName}"));

Console.Write("Message to Send: ");
string message = Console.ReadLine()!;

await sendEndPoint.Send<IMessage> (new Message()
{
    Text = message!
});

Console.Read();