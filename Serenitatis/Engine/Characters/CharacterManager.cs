using System.Text.Json;
using MineJason;
using Serenitatis.Util;
using Serilog;

namespace Serenitatis.Engine.Characters;

public class CharacterManager
{
    private readonly SerenitatisEngine _engine;
    private readonly Dictionary<string, CharacterModel> _characters = new();

    public CharacterManager(SerenitatisEngine engine)
    {
        _engine = engine;
    }
    
    /// <summary>
    /// Registers a character.
    /// </summary>
    /// <param name="id">The ID of the character.</param>
    /// <param name="model">The character to register.</param>
    /// <exception cref="ArgumentException">Character is invalid.</exception>
    public void Register(string id, CharacterModel model)
    {
        if (!_engine.FactionManager.ContainsKey(model.Faction))
        {
            throw new ArgumentException($"Faction '{model.Faction}' not found");
        }
        
        _characters.Add(id, model);
    }
    
    public void Load(string path)
    {
        if (!Directory.Exists(path))
        {
            throw new DirectoryNotFoundException(path);
        }

        foreach (var file in Directory.EnumerateFiles(path, "*.json", SearchOption.AllDirectories))
        {
            LoadCharacterFromFile(path, file);
        }
        
        Log.Information("Loaded {Count} characters", _characters.Count);
    }

    private void LoadCharacterFromFile(string path, string file)
    {
        var name = SysPaths.GetIdFromPath(file, path);

        try
        {
            using var stream = File.OpenRead(file);

            Register(name, JsonSerializer.Deserialize<CharacterModel>(stream,
                SerenitatisEngine.SerializerOptions));
        }
        catch (JsonException e)
        {
            Log.Warning("In character {Name}: {Message}", name, e.Message);
        }
        catch (Exception e)
        {
            Log.Warning(e, "Unable to load character '{Name}'", name);
        }
    }
}