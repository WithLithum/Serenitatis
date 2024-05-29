using System.Text.Json.Serialization;
using MineJason;

namespace Serenitatis.Engine.Characters;

public readonly record struct CharacterModel
{
    [JsonConstructor]
    public CharacterModel(string name, string faction)
    {
        Name = name;
        Faction = faction;
    }
    
    public string Name { get; }
    public string Faction { get; }
}