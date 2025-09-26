using CliTool.Actions;
using CliTool.Utils;

var yaml = File.ReadAllText("/Users/idan.beren/Desktop/SteamProject/CliTool/CliTool/Yaml/Actions.yaml");

var actions = YamlDeserializer.Deserialize<Dictionary<string, List<BaseAction>>>(yaml)["Actions"];

foreach (var action in actions)
    await action.Act();

foreach (var action in actions)
{
    Console.WriteLine();
    Console.WriteLine(action);
}