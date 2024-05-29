using System.Text.Json;
using Serenitatis.Config;
using Serenitatis.Engine;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console(theme: AnsiConsoleTheme.Code)
    .CreateLogger();
    
var configPath = Path.Combine(Directory.GetCurrentDirectory(), "serenitatis.json");
if (args.Length == 1)
{
    configPath = args[0];
}

if (!File.Exists(configPath))
{
    Log.Fatal("Unable to find config file. Program will exit.");
    return -1;
}

Log.Information("Loading config file {ConfigPath}", configPath);

SerenitatisConfig? config;
using (var stream = File.OpenRead(configPath))
{
    config = JsonSerializer.Deserialize<SerenitatisConfig>(stream);
}

if (config == null)
{
    Log.Fatal("Invalid configuration file.");
    return -2;
}

Log.Information("Loading engine");

var engine = new SerenitatisEngine(Path.GetDirectoryName(configPath)!, config);
engine.Load();

return 0;