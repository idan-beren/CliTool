using CliTool.Utils;

var actions =
    Extractor.ExtractActions("/Users/idan.beren/Desktop/SteamProject/CliTool/CliTool/YamlHandling/Actions.yaml");

foreach (var action in actions)
{
    action.Act();
}