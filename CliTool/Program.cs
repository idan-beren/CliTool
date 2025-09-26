using CliTool.Actions;
using CliTool.Utils;

var yaml = File.ReadAllText("/Users/idan.beren/Desktop/SteamProject/CliTool/CliTool/Yaml/Actions.yaml");

var actions = ActionDeserializer.Deserialize<Dictionary<string, List<BaseAction>>>(yaml)["Actions"];

foreach (var action in actions)
    await action.Act();
    