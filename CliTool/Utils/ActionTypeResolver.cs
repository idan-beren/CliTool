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
        {
            return inner.Deserialize(reader, expectedType, nestedObjectDeserializer, out value, rootDeserializer);
        }

        if (!reader.Accept<MappingStart>(out _))
            throw new YamlException("Expected a mapping for an Action");

        var temp = new Dictionary<string, object?>();
        reader.Consume<MappingStart>();

        string? actionType = null;

        while (!reader.Accept<MappingEnd>(out _))
        {
            var key = reader.Consume<Scalar>().Value;

            object? val;
            if (reader.Accept<Scalar>(out _))
            {
                val = reader.Consume<Scalar>().Value;
            }
            else if (reader.Accept<MappingStart>(out _))
            {
                val = nestedObjectDeserializer(reader, typeof(Dictionary<string, object>));
            }
            else if (reader.Accept<SequenceStart>(out _))
            {
                val = nestedObjectDeserializer(reader, typeof(List<object>));
            }
            else
            {
                throw new YamlException($"Unexpected YAML node for key {key}");
            }

            temp[key] = val;

            if (key == "Type" && val is string s)
                actionType = s;
        }

        reader.Consume<MappingEnd>();

        if (actionType == null)
            throw new YamlException("Action does not define a 'Type' field");

        var targetType = ActionFactory.Resolve(actionType);

        var yaml = new SerializerBuilder().Build().Serialize(temp);
        value = new DeserializerBuilder()
            .WithNodeDeserializer(this) 
            .Build()
            .Deserialize(yaml, targetType);

        return true;
    }
}