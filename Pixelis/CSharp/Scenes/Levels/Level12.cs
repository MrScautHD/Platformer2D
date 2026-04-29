using System.Numerics;
using Bliss.CSharp.Colors;
using Pixelis.CSharp.Entities;
using Pixelis.CSharp.GUIs.Loading;
using Sparkle.CSharp.Scenes;
using Sparkle.CSharp.Utils.Async;
using Transform = Bliss.CSharp.Transformations.Transform;

namespace Pixelis.CSharp.Scenes.Levels;

public class Level12 : LevelScene
{
    public Level12() : base("Level 12") {}

    protected override void Init()
    {
        base.Init();
        this.Background = ContentRegistry.Background11;

        // --- START ---
        this.CreatePlatform(0, 0, 4);
        this.CreateTreeDead(0, -1);
        this.CreateBushDead(2, -1);

        // --- SECTION 1: TIGHT OPENING ---
        this.CreatePlatform(7, -1, 1);
        this.CreatePlatform(10, -2, 1);
        this.CreatePlatform(13, -3, 1);
        this.CreatePlatform(16, -2, 1);
        this.CreateMovingPlatform(19, -2, 1, 19, -7, 1.05f);
        this.CreatePlatform(23, -7, 2);
        this.CreatePlantFlowerRed(24, -8);

        // --- SECTION 2: ASCENT WITH FEW SAFETY NETS ---
        this.CreatePlatform(29, -8, 1);
        this.CreatePlatform(33, -9, 1);
        this.CreateMovingPlatform(36, -9, 1, 42, -13, 0.95f);
        this.CreatePlatform(45, -13, 2);
        this.CreateRockWithGrass(45, -14);
        this.CreateBushDead(46, -14);

        // --- SECTION 3: VERTICAL RESET ROUTE ---
        this.CreateMovingPlatform(49, -13, 1, 49, -8, 1.15f);
        this.CreatePlatform(53, -8, 1);
        this.CreatePlatform(56, -9, 1);
        this.CreatePlatform(59, -10, 1);
        this.CreatePlatform(62, -11, 1);
        this.CreatePortal(64, -12, 72, -6, Color.DarkRed, 0.5f);

        // --- SECTION 4: FAST MID SECTION ---
        this.CreatePlatform(72, -6, 1);
        this.CreatePlatform(75, -7, 1);
        this.CreateMovingPlatform(78, -7, 1, 83, -11, 1.2f);
        this.CreatePlatform(86, -11, 2);
        this.CreateStair(90, -10, 3, StairType.Up);
        this.CreatePlatform(95, -13, 2);
        this.CreatePlantSunFlower(96, -14);

        // --- SECTION 5: FALSE CALM ---
        this.CreateMovingPlatform(99, -13, 1, 99, -6, 1.25f);
        this.CreatePlatform(103, -6, 1);
        this.CreatePlatform(107, -7, 1);
        this.CreatePlatform(111, -8, 1);
        this.CreatePortal(113, -9, 121, -15, Color.LightGray, 0.5f);

        // --- SECTION 6: FINAL CLIMB ---
        this.CreatePlatform(121, -15, 1);
        this.CreatePlatform(124, -16, 1);
        this.CreateMovingPlatform(127, -16, 1, 132, -20, 0.9f);
        this.CreatePlatform(135, -20, 2);
        this.CreatePlatform(140, -21, 1);
        this.CreateMovingPlatform(143, -21, 1, 143, -25, 0.85f);
        this.CreatePlatform(147, -25, 2);
        this.CreateFlowerOrange(147, -26);
        this.CreatePlantSunFlower(148, -26);

        // --- FINISH ---
        this.CreatePlatform(152, -26, 2);
        this.CreateWinFlag(153, -27);
        this.CreateTreeDead(152, -27, 0.45f);
    }

    protected override void Update(double delta)
    {
        base.Update(delta);
    }

    protected override void OnLevelWon()
    {
        if (NetworkManager.Client == null || !NetworkManager.Client.IsConnected)
        {
            AsyncOperation op = SceneManager.LoadSceneAsync(new Level1(), new ProgressBarLoadingGui("Loading"));
            op.Completed += success =>
            {
                Player player = new Player(new Transform()
                {
                    Translation = new Vector3(0, -16 * 2, 0)
                });

                SceneManager.ActiveScene?.AddEntity(player);
            };
        }
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (!NetworkManager.IsLevelTransition)
            {
                NetworkManager.Cleanup();
            }

            base.Dispose(disposing);
        }
    }
}
