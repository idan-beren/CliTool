using System.Collections;
using CliTool.Actions;
using YamlDotNet.Serialization;  
using YamlDotNet.Serialization.NamingConventions;  

var yaml = @"  
Actions:  
  - Name: PrintHelloWorld1    
    Type: Log    
    Configuration:      
        Message: ""Hello-world1!""  
  - Name: PrintHelloWorld2 
    Type: Log   
    Configuration:      
        Message: ""Hello-world2!""  
";  

var deserializer = new DeserializerBuilder()  
    .WithNamingConvention(NullNamingConvention.Instance)  
    .Build();

var deserializerYaml = (IEnumerable)deserializer.Deserialize(yaml)!;

foreach (var actionsList in deserializerYaml)  
{  
    var actions = ((KeyValuePair<object, object>)actionsList).Value;
    foreach (var action in (IEnumerable)actions)  
    {        
        var actionDict = (Dictionary<object, object>)action;
        var logAction = new LogAction
        {
            Name = actionDict["Name"].ToString(),
            Type = actionDict["Type"].ToString(),
            Configuration = new LogActionConfig()
        };

        foreach (var item3 in (Dictionary<object, object>)actionDict["Configuration"])
        {
            logAction.Configuration.Message =  item3.Value.ToString();
        }

        logAction.Act();
    }
}