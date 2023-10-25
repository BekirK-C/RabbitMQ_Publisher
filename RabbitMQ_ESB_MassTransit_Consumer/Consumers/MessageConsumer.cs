using MassTransit;
using RabbitMQ_ESB_MassTransit_Shared.Messages;

namespace RabbitMQ_ESB_MassTransit_Consumer.Consumers;
public class MessageConsumer : IConsumer<IMessage>
{
    public Task Consume(ConsumeContext<IMessage> context)
    {
        Console.WriteLine($"Message Received: {context.Message.Text}");
        return Task.CompletedTask;
    }
}