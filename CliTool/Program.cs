using CliTool.YamlHandling;

var yaml = @"  
Actions:  
  - Name: PrintHelloWorld 
    Type: Log    
    Configuration:      
        Message: ""Hello-world!""  
  - Name: Parallel
    Type: Parallel
    Configuration:
        Actions:
            - Name: PrintHelloWorld 
              Type: Log    
              Configuration:      
                  Message: ""Hello-world1!""  
            - Name: PrintHelloWorld 
              Type: Log    
              Configuration:      
                  Message: ""Hello-world2!""  
";  

var actions = YamlHandler.Deserialize(yaml);
foreach (var action in actions)
{
    action.Act();
}