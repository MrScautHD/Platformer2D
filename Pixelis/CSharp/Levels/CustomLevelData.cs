namespace Pixelis.CSharp.Levels;

public class CustomLevelData
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = "New Level";
    public string NextLevelName { get; set; } = string.Empty;
    public List<CustomLevelBlockData> Blocks { get; set; } = [];
}

public class CustomLevelBlockData
{
    public string Type { get; set; } = "Block";
    public int X { get; set; }
    public int Y { get; set; }
    public int? TargetX { get; set; }
    public int? TargetY { get; set; }
    public float? Speed { get; set; }
    public float? Depth { get; set; }
}
