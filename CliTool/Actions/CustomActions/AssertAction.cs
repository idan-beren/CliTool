using System.ComponentModel.DataAnnotations;
using CliTool.Utils;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.Extensions.Logging;

namespace CliTool.Actions.CustomActions;

public class AssertAction : BaseAction
{
    [Required(ErrorMessage = "Condition is required")]
    public string? Condition { get; set; }

    public override async Task<bool> Act()
    {
        return await Assert();
    }

    protected async Task<bool> Assert()
    {
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
            Logger.LogDebug("Got assertion result");
        }
        catch (Exception ex)
        {
            Logger.LogError("Assertion error - {Condition} {Error}", processedCondition, ex.Message);
            return false;
        }
        
        Logger.LogInformation("Assertion status: {Result}", result);
        return result;
    }
}