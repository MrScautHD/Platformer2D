using Pixelis.CSharp.Levels;
using Pixelis.CSharp.Scenes.Levels;
using Sparkle.CSharp.Scenes;

namespace Pixelis.CSharp.Scenes;

public static class LevelFactory
{
    public static readonly string[] BuiltInLevelNames =
    [
        "Level 1",
        "Level 2",
        "Level 3",
        "Level 4",
        "Level 5",
        "Level 6",
        "Level 7",
        "Level 8",
        "Level 9",
        "Level 10",
        "Level 11"
    ];

    public static bool IsBuiltInLevelName(string name)
    {
        return BuiltInLevelNames.Contains(name, StringComparer.OrdinalIgnoreCase);
    }

    public static List<string> GetMenuLevelNames()
    {
        List<string> names = [.. BuiltInLevelNames];
        names.AddRange(CustomLevelStorage.GetCustomLevelNames());
        return names;
    }

    public static Scene? CreateByName(string levelName)
    {
        return levelName switch
        {
            "Level 1" => new Level1(),
            "Level 2" => new Level2(),
            "Level 3" => new Level3(),
            "Level 4" => new Level4(),
            "Level 5" => new Level5(),
            "Level 6" => new Level6(),
            "Level 7" => new Level7(),
            "Level 8" => new Level8(),
            "Level 9" => new Level9(),
            "Level 10" => new Level10(),
            "Level 11" => new Level11(),
            _ => CreateCustomLevel(levelName)
        };
    }

    private static Scene? CreateCustomLevel(string levelName)
    {
        CustomLevelData? data = CustomLevelStorage.LoadByName(levelName);
        return data == null ? null : new CustomLevelScene(data, false);
    }
}
