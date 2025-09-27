namespace CliTool.Actions;

public abstract class BaseAction : IAction
{
    public string? Type { get; set; }

    public abstract Task<bool> Act();

    public override string ToString()
    {
        return ToIndentedString(0);
    }

    private string ToIndentedString(int indentLevel)
    {
        var indent = new string(' ', indentLevel * 2);
        var props = GetType()
            .GetProperties()
            .Select(p =>
            {
                var value = p.GetValue(this);

                if (value is IEnumerable<BaseAction> list)
                {
                    var listValues = string.Join("\n", list.Select(a => a.ToIndentedString(indentLevel + 1)));
                    return $"{indent}{p.Name}:\n{listValues}";
                }

                if (value is BaseAction action)
                    return $"{indent}{p.Name}:\n{action.ToIndentedString(indentLevel + 1)}";

                return $"{indent}{p.Name}: {value}";
            });

        return $"{indent}{GetType().Name}\n{string.Join("\n", props)}";
    }
}