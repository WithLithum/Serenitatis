using System.Text.Json;
using Serenitatis.Config;
using Serenitatis.Engine.Characters;
using Serenitatis.Engine.Factions;
using Serilog;

namespace Serenitatis.Engine;

public class SerenitatisEngine
{
    public static readonly JsonSerializerOptions SerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };
    
    public SerenitatisEngine(string basePath, SerenitatisConfig config)
    {
        BasePath = basePath;
        Config = config;

        CharacterManager = new CharacterManager(this);
    }
    
    public string BasePath { get; }
    public SerenitatisConfig Config { get; }
    public CharacterManager CharacterManager { get; }
    public FactionManager FactionManager { get; } = new();

    public void Load()
    {
        var factionPath = Path.Combine(BasePath, Config.FactionsDirectoryPath);
        if (Directory.Exists(factionPath))
        {
            Log.Information("Loading factions");
            FactionManager.Load(factionPath);
        }
        
        var characterPath = Path.Combine(BasePath, Config.CharactersDirectoryPath);
        if (Directory.Exists(characterPath))
        {
            Log.Information("Loading characters");
            CharacterManager.Load(characterPath);
        }
    }
}