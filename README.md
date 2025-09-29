# YAML-Based Action Runner CLI

A **C# command-line tool** for running workflows defined in **YAML files**.
This tool is designed to be **extensible, structured, and developer-friendly**, making it easy to automate tasks, integrate external services, and control execution flow with **variables, conditions, retries, imports, and more**.

## 📂 YAML Structure

A workflow YAML file is simply a **list of actions**. Each action has a `Type` and its own parameters.

### 🔹 What is an Action?

An **action** = a single executable step in the workflow.
Examples: logging a message, waiting for a duration, calling an API, or running a shell command.

### 🔹 Example Workflow

```yaml
Actions:
  - Type: Log
    Message: "Hello-World!"
    
  - Type: Delay
    Duration: 100
    
  - Type: Assert
    Condition: "1 + 2 == 3"
  
  - Type: Http
    Method: GET
    Url: "https://www.google.com"
      
  - Type: SetVariable
    Name: Price
    Value: 100
    
  - Type: PrintVariable
    Name: Price
  
  - Type: Parallel
    Actions:
      - Type: Log
        Message: "Task A"
      - Type: Log
        Message: "Task B"
        
  - Type: Retry
    Times: 3
    Action:
      Type: Log
      Message: "Retrying..."
  
  - Type: Shell
    Command: echo hello-world!
      
  - Type: Condition
    Condition: "{{Price}} == 100"
    Then:
      - Type: Log
        Message: "True"
    Else:
      - Type: Log
        Message: "False"
        
  - Type: Import
    Path: ./Yaml/ImportedActions.yaml
```

---

## ⚡ Supported Actions

| Action Type | Description                                        | Fields                             |
| ----------- | -------------------------------------------------- |------------------------------------|
| `log`       | Logs a message to the console.                     | `Message`                          |
| `delay`     | Waits for a specified duration (ms).               | `Duration`                         |
| `assert`    | Evaluates a condition. Stops execution if false.   | `Condition`                        |
| `http`      | Sends an HTTP GET/POST request.                    | `Method`, `Url`, `Body (optional)` |
| `set-var`   | Stores a variable in memory.                       | `Name`, `Value`                    |
| `print-var` | Prints the value of a stored variable.             | `Name`                             |
| `parallel`  | Runs multiple actions concurrently.                | `Actions` (list of steps)          |
| `retry`     | Retries a step N times if it fails.                | `Times`, `Action`                  |
| `shell`     | Runs a shell command and logs output.              | `Command`                          |
| `condition` | Conditionally runs actions based on an expression. | `Condition`, `Then`, `Else`        |
| `import`    | Loads another YAML file into the current workflow. | `Path`                             |

---

## 💻 CLI Commands

### ▶️ Run Command

Executes actions defined in a YAML file.

**Usage:**

```bash
dotnet run -- run --file example.yaml
```

### Options

| Option                     | Description                                  |
| -------------------------- | -------------------------------------------- |
| `--file <file>` (REQUIRED) | Path to the YAML file.                       |
| `--dry`                    | Prints all actions without executing them.   |
| `--verbose`                | Shows detailed information during execution. |
| `-?, -h, --help`           | Displays help and usage info.                |

### Exit Codes

* **0** → Success
* **Non-zero** → Failure (execution error or assertion failure)

---

## 🛠️ Building and Running

```bash
# Clone the repository
git clone <repo-url>
cd CliTool

# Build the project (requires .NET 8)
dotnet build

# Run with an example YAML file
dotnet run -- run --file example.yaml
```

---

## 🚀 Quick Start Example

1. Create a file `example.yaml`:

   ```yaml
   Actions:
     - Type: Log
       Message: "Hello from YAML Runner!"
   ```
2. Run it:

   ```bash
   dotnet run -- run --file example.yaml
   ```

Expected output:

```
[Log] Hello from YAML Runner!
```

---
