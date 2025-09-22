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
var actions = (IEnumerable)deserializer.Deserialize(yaml)!;  

foreach (var action in actions)  
{  
    var actions2 = ((KeyValuePair<object, object>)action).Value;  
    foreach (var item in (IEnumerable)actions2)  
    {        
        var itemDict = (Dictionary<object, object>)item;

        var logAction = new LogAction();
        logAction.Name = itemDict["Name"].ToString();
        logAction.Type = itemDict["Type"].ToString();
        logAction.Configuration = new LogActionConfig();
        
        foreach (var item3 in (Dictionary<object, object>)itemDict["Configuration"])
        {
            logAction.Configuration.Message =  item3.Value.ToString();
        }

        logAction.Act();
    }
}