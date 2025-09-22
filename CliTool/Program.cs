using CliTool.YamlHandling;

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

var actions = YamlHandler.Deserialize(yaml);
foreach (var action in actions)
{
    action.Act();
}