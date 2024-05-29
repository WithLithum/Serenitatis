using System.Text.Json;
using Serenitatis.Engine.Characters;
using Serenitatis.Util;
using Serilog;

namespace Serenitatis.Engine.Factions;

public class FactionManager
{
    private readonly Dictionary<string, FactionModel> _factions = new();

    public bool ContainsKey(string id)
    {
        return _factions.ContainsKey(id);
    }
    
    public void Load(string path)
    {
        if (!Directory.Exists(path))
        {
            throw new DirectoryNotFoundException(path);
        }

        foreach (var file in Directory.EnumerateFiles(path, "*.json", SearchOption.AllDirectories))
        {
            LoadFactionFromFile(path, file);
        }
        
        Log.Information("Loaded {Count} factions", _factions.Count);
    }
    
    private void LoadFactionFromFile(string path, string file)
    {
        var name = SysPaths.GetIdFromPath(file, path);

        try
        {
            using var stream = File.OpenRead(file);

            _factions.Add(name, JsonSerializer.Deserialize<FactionModel>(stream,
                SerenitatisEngine.SerializerOptions));
        }
        catch (JsonException e)
        {
            Log.Warning("Syntax error in character");
        }
        catch (Exception e)
        {
            Log.Warning(e, "Unable to load character file {File}", file);
        }
    }
}