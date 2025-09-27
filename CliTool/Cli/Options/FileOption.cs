using System.CommandLine;
using CliTool.Actions;
using CliTool.Utils;

namespace CliTool.Cli.Options;

public class FileOption : Option<FileInfo>
{
    private new const string Name = "--file";
    private new const string Description = "Path to the YAML file.";

    public FileOption() : base(name: Name, description: Description)
    {
        IsRequired =  true;
    }

    public List<BaseAction> Apply(FileInfo file)
    {
        var yaml = File.ReadAllText(file.FullName);
        return ActionDeserializer.Deserialize(yaml);
    }
}
