using Bliss.CSharp.Interact.Keyboards;
using Sparkle.CSharp;

namespace Pixelis.CSharp.Controls;

public static class KeyBindinds
{
    public const KeyboardKey DefaultMoveLeft = KeyboardKey.A;
    public const KeyboardKey DefaultMoveRight = KeyboardKey.D;
    public const KeyboardKey DefaultJump = KeyboardKey.Space;

    private const string MoveLeftKey = "KeybindMoveLeft";
    private const string MoveRightKey = "KeybindMoveRight";
    private const string JumpKey = "KeybindJump";

    public static KeyboardKey GetMoveLeft()
    {
        return GetOrDefault(MoveLeftKey, DefaultMoveLeft);
    }

    public static KeyboardKey GetMoveRight()
    {
        return GetOrDefault(MoveRightKey, DefaultMoveRight);
    }

    public static KeyboardKey GetJump()
    {
        return GetOrDefault(JumpKey, DefaultJump);
    }

    public static void SetMoveLeft(KeyboardKey key)
    {
        SetKey(MoveLeftKey, key);
    }

    public static void SetMoveRight(KeyboardKey key)
    {
        SetKey(MoveRightKey, key);
    }

    public static void SetJump(KeyboardKey key)
    {
        SetKey(JumpKey, key);
    }

    public static void ResetToDefaults()
    {
        SetMoveLeft(DefaultMoveLeft);
        SetMoveRight(DefaultMoveRight);
        SetJump(DefaultJump);
    }

    private static KeyboardKey GetOrDefault(string configKey, KeyboardKey fallback)
    {
        PixelisGame game = (PixelisGame)Game.Instance!;
        string? raw = game.OptionsConfig.GetValue<string>(configKey);

        if (!string.IsNullOrWhiteSpace(raw) && Enum.TryParse(raw, out KeyboardKey parsed))
        {
            return parsed;
        }

        game.OptionsConfig.SetValue(configKey, fallback.ToString());
        return fallback;
    }

    private static void SetKey(string configKey, KeyboardKey key)
    {
        PixelisGame game = (PixelisGame)Game.Instance!;
        game.OptionsConfig.SetValue(configKey, key.ToString());
    }
}
