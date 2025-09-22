using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace CliTool.YamlHandling;

public static class YamlHandler
{
    private static IDeserializer deserializer = new DeserializerBuilder()
        .WithNamingConvention(CamelCaseNamingConvention.Instance)
        .Build();
    
    
}