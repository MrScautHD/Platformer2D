using System.Numerics;
using Bliss.CSharp.Colors;
using Bliss.CSharp.Transformations;
using Pixelis.CSharp.Scenes;
using Sparkle.CSharp.Graphics;
using Sparkle.CSharp.GUI;
using Sparkle.CSharp.GUI.Elements;
using Sparkle.CSharp.GUI.Elements.Data;
using Veldrid;

namespace Pixelis.CSharp.GUIs;

public class LevelEditorPlayGui : Gui
{
    private readonly CustomLevelScene _scene;

    public LevelEditorPlayGui(CustomLevelScene scene) : base("LevelEditorPlayOverlay", null)
    {
        _scene = scene;
    }

    protected override void Init()
    {
        base.Init();

        TextureButtonData buttonData = new TextureButtonData(
            ContentRegistry.UiButton,
            hoverColor: Color.LightGray,
            resizeMode: ResizeMode.NineSlice,
            borderInsets: new BorderInsets(12));

        this.AddElement("Editor-Button", new TextureButtonElement(
            buttonData,
            new LabelData(ContentRegistry.Fontoe, "Editor", 18, hoverColor: Color.White),
            Anchor.TopRight,
            new Vector2(-20, 20),
            size: new Vector2(150, 40),
            textOffset: new Vector2(0, 1),
            clickFunc: _ =>
            {
                _scene.SuppressPlacementUntilMouseRelease();
                _scene.ReturnToEditorMode();
                return true;
            }));
    }

    protected override void Draw(GraphicsContext context, Framebuffer framebuffer)
    {
        context.PrimitiveBatch.Begin(context.CommandList, framebuffer.OutputDescription);
        context.PrimitiveBatch.DrawFilledRectangle(
            new RectangleF(GlobalGraphicsAssets.Window.GetWidth() - 200, 0, 200, 84),
            color: new Color(15, 15, 15, 160));
        context.PrimitiveBatch.End();

        base.Draw(context, framebuffer);
    }
}
