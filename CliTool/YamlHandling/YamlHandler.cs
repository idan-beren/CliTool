using CliTool.Actions;
using CliTool.Utils;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace CliTool.YamlHandling;

public static class YamlHandler
{
    private static readonly IDeserializer Deserializer = new DeserializerBuilder()
        .WithNamingConvention(NullNamingConvention.Instance)
        .Build();

    public static List<IAction> Deserialize(string yaml)
    {
        var root = Deserializer.Deserialize<dynamic>(yaml)!;
        return Extractor.ExtractActions(root);
    }
}