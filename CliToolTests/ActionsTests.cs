using CliTool.Utils;
using Microsoft.Extensions.Logging;
using YamlDotNet.Core;

namespace CliToolTests;

public class ActionsTests
{
  public string ValidYaml { get; set; }
  public string InvalidYamlParamIsMissing { get; set; }
  
  public string InvalidYamlActionReturnsFalse { get; set; }

  [SetUp]
  public void SetupInvalidActions()
  {
    InvalidYamlParamIsMissing = @"Actions:
  - Type: Log";

    InvalidYamlActionReturnsFalse = @"Actions:
  - Type: PrintVariable
    Name: TotalPrice";
  }

  [SetUp]
  public void SetupValidActions()
  {
    GlobalVariables.SetVariable("LoggerLevel", LogLevel.Debug);

    ValidYaml = @"Actions:
  - Type: Log
    Message: ""Hello-World!""
    
  - Type: Delay
    Duration: 100
    
  - Type: Assert
    Condition: ""1 + 2 == 3""
  
  - Type: Http
    Method: GET
    Url: ""https://www.google.com""
      
  - Type: SetVariable
    Name: Price
    Value: 100
    
  - Type: PrintVariable
    Name: Price
  
  - Type: Parallel
    Actions:
      - Type: Log
        Message: ""Hello-World!""
      - Type: Log
        Message: ""Hello-World!""
        
  - Type: Retry
    Times: 3
    Action:
      Type: Log
      Message: ""Hello-World!""
  
  - Type: Shell
    Command: echo hello-world!
      
  - Type: Condition
    Condition: ""{{Price}} == 100""
    Then:
      - Type: Log
        Message: ""True""
    Else:
      - Type: Log
        Message: ""False""
        
  - Type: Import
    Path: /Users/idan.beren/Desktop/SteamProject/CliTool/CliTool/Yaml/ImportedActions.yaml";
  }

  [Test]
  public void TestAllActionsValid()
  {
    var actions = ActionDeserializer.Deserialize(ValidYaml);
    Assert.That(actions.Count, Is.EqualTo(11));
    foreach (var result in actions.Select(action => action.Act().Result))
      Assert.That(result, Is.True);
  }

  [Test]
  public void TestInvalidActionWhenParamIsMissing()
  {
    var ex = Assert.Catch<YamlException>(() => ActionDeserializer.Deserialize(InvalidYamlParamIsMissing));
    Assert.That(ex.Message, Is.EqualTo("Validation failed for Log action: Message is required"));
  }

  [Test]
  public void TestInvalidActionWhenActionReturnsFalse()
  {
    var actions = ActionDeserializer.Deserialize(InvalidYamlActionReturnsFalse);
    Assert.That(actions.Count, Is.EqualTo(1));
    Assert.That(actions[0].Act().Result, Is.False);
  }
}