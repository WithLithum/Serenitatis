using System.Text.Json.Serialization;
using MineJason;

namespace Serenitatis.Engine.Factions;

public readonly record struct FactionModel
{
    [JsonConstructor]
    public FactionModel(IChatColor color)
    {
        Color = color;
    }
    
    public IChatColor Color { get; }
}