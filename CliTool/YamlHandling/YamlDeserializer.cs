using CliTool.Actions;
using CliTool.Utils;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace CliTool.YamlHandling;

public static class YamlDeserializer
{
    private static readonly IDeserializer Deserializer = new DeserializerBuilder()
        .WithNamingConvention(NullNamingConvention.Instance)
        .Build();

    public static dynamic Deserialize(string yaml)
    {
        return Deserializer.Deserialize<dynamic>(yaml);
    }
}