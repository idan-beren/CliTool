using CliTool.Actions;
using CliTool.Utils;
using CliTool.YamlHandling;

var yaml = File.ReadAllText("/Users/idan.beren/Desktop/SteamProject/CliTool/CliTool/YamlHandling/Actions.yaml");
var deserializedYaml = YamlDeserializer.Deserialize(yaml);

List<IAction> actions = Extractor.ExtractActions(deserializedYaml);

foreach (var action in actions)
{
    action.Act();
}