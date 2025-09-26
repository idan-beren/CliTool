using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Serialization.NodeDeserializers;

namespace CliTool.Utils;

public static class YamlDeserializer
{
    private static readonly IDeserializer Deserializer = new DeserializerBuilder()
        .WithNamingConvention(PascalCaseNamingConvention.Instance)
        .WithNodeDeserializer<ActionTypeResolver>(inner => new ActionTypeResolver(inner), s => s.InsteadOf<ObjectNodeDeserializer>())
        .Build();

    public static T Deserialize<T>(string yaml)
    {
        return Deserializer.Deserialize<T>(yaml);
    }
}