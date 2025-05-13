using System.Text;
using System.Text.Json;
using Confluent.Kafka;

namespace Kafka.Example;

public class MessageSerializer<TMessage> : ISerializer<TMessage>
{
    public byte[] Serialize(TMessage data, SerializationContext context)
    {
        return data switch
        {
            string => Encoding.UTF8.GetBytes(JsonSerializer.Serialize(data)),
            _ => Encoding.UTF8.GetBytes(JsonSerializer.Serialize(data))
        };
    }
}