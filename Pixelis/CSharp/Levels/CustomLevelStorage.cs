using System.Text;
using System.Text.Json;
using Pixelis.CSharp.Scenes;
using Sparkle.CSharp.Scenes;

namespace Pixelis.CSharp.Levels;

public static class CustomLevelStorage
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true
    };

    public static string DirectoryPath
        => Path.Combine(AppContext.BaseDirectory, "custom-levels");

    public static IReadOnlyList<CustomLevelData> LoadAll()
    {
        EnsureDirectory();

        List<CustomLevelData> levels = [];

        foreach (string filePath in Directory.GetFiles(DirectoryPath, "*.json"))
        {
            try
            {
                string json = File.ReadAllText(filePath);
                CustomLevelData? data = JsonSerializer.Deserialize<CustomLevelData>(json, JsonOptions);

                if (data == null)
                {
                    continue;
                }

                data.Id = GetIdFromPath(filePath);
                data.Name = string.IsNullOrWhiteSpace(data.Name) ? data.Id : data.Name.Trim();
                data.NextLevelName = data.NextLevelName?.Trim() ?? string.Empty;
                data.Blocks ??= [];
                levels.Add(data);
            }
            catch
            {
                // Skip broken level files so one bad save does not break the menu.
            }
        }

        return levels
            .OrderBy(level => level.Name, StringComparer.OrdinalIgnoreCase)
            .ToList();
    }

    public static CustomLevelData? LoadByName(string levelName)
    {
        return LoadAll().FirstOrDefault(level =>
            string.Equals(level.Name, levelName, StringComparison.OrdinalIgnoreCase));
    }

    public static string? ExportLevelPayload(string levelName)
    {
        CustomLevelData? data = LoadByName(levelName);
        return data == null ? null : JsonSerializer.Serialize(data, JsonOptions);
    }

    public static CustomLevelData? ImportLevelPayload(string payload)
    {
        if (string.IsNullOrWhiteSpace(payload))
        {
            return null;
        }

        try
        {
            CustomLevelData? data = JsonSerializer.Deserialize<CustomLevelData>(payload, JsonOptions);
            if (data == null)
            {
                return null;
            }

            data.Name = string.IsNullOrWhiteSpace(data.Name) ? "Custom Level" : data.Name.Trim();
            data.Id = string.IsNullOrWhiteSpace(data.Id) ? NormalizeName(data.Name) : data.Id.Trim();
            data.NextLevelName = data.NextLevelName?.Trim() ?? string.Empty;
            data.Blocks ??= [];
            return data;
        }
        catch
        {
            return null;
        }
    }

    public static CustomLevelData CreateNew(string name = "New Level")
    {
        return new CustomLevelData
        {
            Id = NormalizeName(name),
            Name = name.Trim(),
            Blocks = []
        };
    }

    public static CustomLevelData Save(CustomLevelData data, string? previousId = null)
    {
        EnsureDirectory();

        string levelName = data.Name.Trim();
        if (string.IsNullOrWhiteSpace(levelName))
        {
            throw new InvalidOperationException("Level name cannot be empty.");
        }

        if (LevelFactory.IsBuiltInLevelName(levelName))
        {
            throw new InvalidOperationException("This level name is already used by a built-in level.");
        }

        string normalizedId = NormalizeName(levelName);
        string targetPath = Path.Combine(DirectoryPath, $"{normalizedId}.json");

        data.Id = normalizedId;
        data.Name = levelName;
        data.NextLevelName = data.NextLevelName?.Trim() ?? string.Empty;
        data.Blocks = data.Blocks
            .Where(block => !string.IsNullOrWhiteSpace(block.Type))
            .GroupBy(block => (block.X, block.Y))
            .Select(group => group.Last())
            .Select(block => new CustomLevelBlockData
            {
                Type = block.Type.Trim(),
                X = block.X,
                Y = block.Y,
                TargetX = block.TargetX,
                TargetY = block.TargetY,
                Speed = block.Speed
            })
            .OrderBy(block => block.Y)
            .ThenBy(block => block.X)
            .ToList();

        File.WriteAllText(targetPath, JsonSerializer.Serialize(data, JsonOptions));

        if (!string.IsNullOrWhiteSpace(previousId)
            && !string.Equals(previousId, normalizedId, StringComparison.OrdinalIgnoreCase))
        {
            string previousPath = Path.Combine(DirectoryPath, $"{previousId}.json");
            if (File.Exists(previousPath))
            {
                File.Delete(previousPath);
            }
        }

        return data;
    }

    public static List<string> GetCustomLevelNames()
    {
        return LoadAll().Select(level => level.Name).ToList();
    }

    public static bool DeleteByName(string levelName)
    {
        CustomLevelData? level = LoadByName(levelName);
        if (level == null)
        {
            return false;
        }

        string filePath = Path.Combine(DirectoryPath, $"{level.Id}.json");
        if (!File.Exists(filePath))
        {
            return false;
        }

        File.Delete(filePath);
        return true;
    }

    public static string NormalizeName(string value)
    {
        string trimmed = value.Trim();
        if (string.IsNullOrWhiteSpace(trimmed))
        {
            return "new-level";
        }

        StringBuilder builder = new();

        foreach (char character in trimmed)
        {
            if (char.IsLetterOrDigit(character))
            {
                builder.Append(char.ToLowerInvariant(character));
            }
            else if (character is ' ' or '-' or '_')
            {
                builder.Append('-');
            }
        }

        string normalized = builder.ToString().Trim('-');
        return string.IsNullOrWhiteSpace(normalized) ? "new-level" : normalized;
    }

    private static void EnsureDirectory()
    {
        Directory.CreateDirectory(DirectoryPath);
    }

    private static string GetIdFromPath(string filePath)
    {
        return Path.GetFileNameWithoutExtension(filePath);
    }
}
