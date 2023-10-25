using MassTransit;
using RabbitMQ_ESB_MassTransit_Consumer.Consumers;

string rabbitMQUri = "amqps://xgtppcak:9E7tafT5XUlGRnKeOPZTnMn1L4-OtIvE@jackal.rmq.cloudamqp.com/xgtppcak";

string queueName = "example-queue";

var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
{
    cfg.Host(rabbitMQUri);
    cfg.ReceiveEndpoint(queueName, endpoint =>
    {
        endpoint.Consumer<MessageConsumer>();
    });
});

await bus.StartAsync();

Console.Read();
