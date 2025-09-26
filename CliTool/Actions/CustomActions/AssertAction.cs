using CliTool.Utils;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace CliTool.Actions.CustomActions;

public class AssertAction : BaseAction
{
    public string? Condition { get; set; }

    public override async Task<bool> Act()
    {
        var result = await Assert();

        Console.WriteLine(result);
        return result;
    }

    protected async Task<bool> Assert()
    {
        if (string.IsNullOrWhiteSpace(Condition))
        {
            Console.WriteLine("AssertAction: No condition provided.");
            return false;
        }

        var processedCondition = Condition!;

        foreach (var kv in GlobalVariables.GetAllVariables())
            processedCondition = processedCondition.Replace($"{{{{{kv.Key}}}}}", kv.Value?.ToString());

        bool result;
        try
        {
            var references = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => !string.IsNullOrWhiteSpace(a.Location))
                .Select(a => Microsoft.CodeAnalysis.MetadataReference.CreateFromFile(a.Location));

            result = await CSharpScript.EvaluateAsync<bool>(
                processedCondition,
                ScriptOptions.Default
                    .WithImports("System")
                    .WithReferences(references)
            );
        }
        catch (Exception ex)
        {
            Console.WriteLine($"AssertAction: Failed to evaluate condition '{processedCondition}' - {ex.Message}");
            return false;
        }

        return result;
    }
}