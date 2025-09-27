namespace CliTool.Actions;

public abstract class BaseAction : IAction
{
    public string? Type { get; set; }

    public abstract Task<bool> Act();

    public override string ToString()
    {
        var props = GetType()
            .GetProperties()
            .Select(p =>
            {
                var value = p.GetValue(this);
                if (value is IEnumerable<BaseAction> list)
                    return $"{p.Name}=[{string.Join(", ", list.Select(a => a.ToString()))}]";
                return $"{p.Name}={value}";
            });
        return $"{GetType().Name}({string.Join(", ", props)})";
    }
}