using System.Numerics;
using Pixelis.CSharp.Entities;
using Pixelis.CSharp.GUIs.Loading;
using Sparkle.CSharp.Scenes;
using Sparkle.CSharp.Utils.Async;
using Transform = Bliss.CSharp.Transformations.Transform;

namespace Pixelis.CSharp.Scenes.Levels;

public class Level11 : LevelScene
{
    public Level11() : base("Level 11") {}

    protected override void Init()
    {
        base.Init();
        this.Background = ContentRegistry.Background;

        // --- BLOCKS ---
        this.CreatePlatform(96, -8, 1);
        this.CreatePlatform(97, -8, 1);
        this.CreatePlatform(98, -8, 1);
        this.CreatePlatform(103, -8, 1);
        this.CreatePlatform(104, -8, 1);
        this.CreatePlatform(105, -8, 1);
        this.CreatePlatform(106, -8, 1);
        this.CreatePlatform(107, -8, 1);
        this.CreatePlatform(108, -8, 1);
        this.CreatePlatform(109, -8, 1);
        this.CreatePlatform(110, -8, 1);

        this.CreatePlatform(39, -7, 1);
        this.CreatePlatform(40, -7, 1);
        this.CreatePlatform(41, -7, 1);
        this.CreatePlatform(42, -7, 1);
        this.CreatePlatform(43, -7, 1);
        this.CreatePlatform(44, -7, 1);
        this.CreatePlatform(45, -7, 1);
        this.CreatePlatform(46, -7, 1);
        this.CreatePlatform(47, -7, 1);
        this.CreatePlatform(50, -7, 1);
        this.CreatePlatform(51, -7, 1);
        this.CreatePlatform(52, -7, 1);

        this.CreatePlatform(90, -7, 1);
        this.CreatePlatform(91, -7, 1);
        this.CreatePlatform(92, -7, 1);
        this.CreatePlatform(93, -7, 1);

        this.CreatePlatform(74, -5, 1);
        this.CreatePlatform(75, -5, 1);
        this.CreatePlatform(76, -5, 1);
        this.CreatePlatform(77, -5, 1);
        this.CreatePlatform(78, -5, 1);

        this.CreatePlatform(67, -4, 1);
        this.CreatePlatform(68, -4, 1);
        this.CreatePlatform(69, -4, 1);
        this.CreatePlatform(70, -4, 1);

        this.CreatePlatform(61, -3, 1);
        this.CreatePlatform(62, -3, 1);
        this.CreatePlatform(63, -3, 1);
        this.CreatePlatform(64, -3, 1);
        this.CreatePlatform(65, -3, 1);

        this.CreatePlatform(32, -1, 1);
        this.CreatePlatform(33, -1, 1);
        this.CreatePlatform(34, -1, 1);
        this.CreatePlatform(35, -1, 1);
        this.CreatePlatform(36, -1, 1);

        this.CreatePlatform(-3, 2, 1);
        this.CreatePlatform(-2, 2, 1);
        this.CreatePlatform(-1, 2, 1);
        this.CreatePlatform(0, 2, 1);
        this.CreatePlatform(1, 2, 1);
        this.CreatePlatform(2, 2, 1);
        this.CreatePlatform(3, 2, 1);
        this.CreatePlatform(4, 2, 1);
        this.CreatePlatform(5, 2, 1);
        this.CreatePlatform(6, 2, 1);
        this.CreatePlatform(7, 2, 1);
        this.CreatePlatform(8, 2, 1);
        this.CreatePlatform(9, 2, 1);
        this.CreatePlatform(10, 2, 1);

        this.CreatePlatform(21, 2, 1);
        this.CreatePlatform(22, 2, 1);
        this.CreatePlatform(23, 2, 1);

        this.CreatePlatform(11, 3, 1);
        this.CreatePlatform(20, 3, 1);

        this.CreatePlatform(12, 4, 1);
        this.CreatePlatform(17, 4, 1);
        this.CreatePlatform(18, 4, 1);

        // --- MOVING BLOCKS ---
        this.CreateMovingPlatform(54, -7, 1, 60, -3, 1);
        this.CreateMovingPlatform(80, -5, 1, 89, -7, 1);
        this.CreateMovingPlatform(38, -1, 1, 38, -7, 1);
        this.CreateMovingPlatform(27, 1, 1, 31, -1, 1);
        this.CreateMovingPlatform(14, 2, 1, 14, 4, 1);

        // --- PORTAL ---
        this.CreatePortal(90, -3, 106, -19, null, 0.5f);

        // --- WIN ---
        this.CreateWinFlag(109, -9);

        // --- DEKO ---
        this.CreatePlantSunFlower(96, -9);
        this.CreateFlowerOrange(97, -9);
        this.CreateFlowerOrange(98, -9);
        this.CreateTreeDead(104, -9);
        this.CreateFlowerOrange(105, -9);
        this.CreatePlantSunFlower(108, -9);
        this.CreateFlowerOrange(110, -9);

        this.CreateTreeDead(41, -8);
        this.CreateBushDead(43, -8);
        this.CreateTree(46, -8);
        this.CreateOakLog(51, -8);
        this.CreateBushDead(52, -8);

        this.CreateTree(91, -8);
        this.CreateBushDead(93, -8);

        this.CreateRockWithGrass(74, -6);
        this.CreateBush(76, -6);
        this.CreateBush(77, -6);

        this.CreateRockWithGrass(68, -5);
        this.CreatePlantSunFlower(69, -5);

        this.CreateRockWithGrass(62, -4);
        this.CreateBushDead(63, -4);
        this.CreateRockWithGrass(64, -4);
        this.CreateBushDead(65, -4);

        this.CreateBush(34, -2);
        this.CreateFlowerOrange(35, -2);
        this.CreateFlowerOrange(36, -2);
        
        this.CreateFlowerOrange(-2, 1);
        this.CreateTree(0, 1, 0.5f);
        this.CreateTree(1, 1, 0.6f);
        this.CreateTree(2, 1, 0.5f);

        this.CreatePlantFlowerRed(6, 1);
        this.CreateTreeDead(7, 1, 0.5f);
        this.CreateRockWithGrass(8, 1);

        this.CreateRockWithGrass(22, 1);
        this.CreateRockWithGrass(23, 1);
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