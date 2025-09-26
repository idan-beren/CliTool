using CliTool.Actions;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace CliTool.Utils;

public class ActionTypeResolver(INodeDeserializer inner) : INodeDeserializer
{
    public bool Deserialize(
        IParser reader,
        Type expectedType,
        Func<IParser, Type, object?> nestedObjectDeserializer,
        out object? value,
        ObjectDeserializer rootDeserializer)
    {
        value = null;
        if (expectedType != typeof(BaseAction))
            return inner.Deserialize(reader, expectedType, nestedObjectDeserializer, out value, rootDeserializer);

        if (!reader.Accept<MappingStart>(out _))
            throw new YamlException("Expected a mapping for an Action");

        var buffer = new List<ParsingEvent> { reader.Consume<MappingStart>() };
        string? actionType = null;

        while (!reader.Accept<MappingEnd>(out _))
        {
            var key = reader.Consume<Scalar>();
            buffer.Add(key);

            if (key.Value == "Type")
            {
                var typeVal = reader.Consume<Scalar>();
                buffer.Add(typeVal);
                actionType = typeVal.Value;
            }
            else
            {
                BufferNode(reader, buffer);
            }
        }

        buffer.Add(reader.Consume<MappingEnd>());

        if (actionType == null)
            throw new YamlException("Action does not define a 'Type' field");

        value = nestedObjectDeserializer(new ReplayParser(buffer), ActionFactory.Resolve(actionType));
        return true;
    }

    private static void BufferNode(IParser reader, List<ParsingEvent> buffer)
    {
        if (reader.Accept<Scalar>(out _))
        {
            buffer.Add(reader.Consume<Scalar>());
        }
        else if (reader.Accept<MappingStart>(out _))
        {
            buffer.Add(reader.Consume<MappingStart>());
            while (!reader.Accept<MappingEnd>(out _))
            {
                buffer.Add(reader.Consume<Scalar>());
                BufferNode(reader, buffer);
            }
            buffer.Add(reader.Consume<MappingEnd>());
        }
        else if (reader.Accept<SequenceStart>(out _))
        {
            buffer.Add(reader.Consume<SequenceStart>());
            while (!reader.Accept<SequenceEnd>(out _)) BufferNode(reader, buffer);
            buffer.Add(reader.Consume<SequenceEnd>());
        }
        else
        {
            throw new YamlException("Unexpected node while copying");
        }
    }
}

public class ReplayParser(IEnumerable<ParsingEvent> events) : IParser
{
    private readonly IEnumerator<ParsingEvent> _events = events.GetEnumerator();
    public ParsingEvent Current { get; private set; } = null!;
    public bool MoveNext() => _events.MoveNext() && (Current = _events.Current) != null;
}
