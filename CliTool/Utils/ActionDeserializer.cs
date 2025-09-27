using CliTool.Actions;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Serialization.NodeDeserializers;

namespace CliTool.Utils;

public static class ActionDeserializer
{
    private static readonly IDeserializer Deserializer = new DeserializerBuilder()
        .WithNamingConvention(PascalCaseNamingConvention.Instance)
        .WithNodeDeserializer<ActionTypeResolver>(inner => new ActionTypeResolver(inner),
            s => s.InsteadOf<ObjectNodeDeserializer>()).Build();

    public static List<BaseAction> Deserialize(string yaml)
    {
        return Deserializer.Deserialize<Dictionary<string, List<BaseAction>>>(yaml)["Actions"];
    }
}