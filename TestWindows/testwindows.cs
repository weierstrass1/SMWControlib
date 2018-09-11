using System.Drawing;
using System.Windows.Forms;
using SMWControlibBackend.Graphics;
using SMWControlibControls.GraphicsControls;

namespace TestWindows
{
    public partial class testwindows : Form
    {
        public testwindows()
        {
            InitializeComponent();
            gfxButton1.Target = spriteGFXBox1;
            gfxButton2.Target = spriteGFXBox1;
            gfxButton3.Target = spriteGFXBox2;
            gfxButton4.Target = spriteGFXBox2;
            spriteGFXBox1.SelectionChanged += selectionChanged;
            spriteGFXBox2.SelectionChanged += selectionChanged;
            paletteButton1.target = paletteBox1;
            frameCreator1.SelectionChanged += frameCreator1_SelectionChanged;
            spriteGFXBox1.GraphicsLoaded += graphicsLoaded;
            spriteGFXBox2.GraphicsLoaded += graphicsLoaded;
            tabControl1.SelectedIndexChanged += selectedIndexChanged;
            animationEditor1.AddClick += addClick;
            animationEditor1.AnimationChanged += animationChanged;
            resizeableSpriteGridController1.MidChanged += midChanged;
            animationPlayer1.TimeChanged += playerTimeChanged;
            animationCreator1.SelectionChanged += animationCreatorSelectionChanged;
            interactionMenu1.FrameSelectionChanged += interactionMenuFrameSelectionChanged;
            interactionMenu1.HitboxSelectionChanged += interactionMenu1HitboxSelectionChanged;
            interactionMenu1.InteractionPointSelectionChanged += interactionMenuInteractionPointSelectionChanged;
            interactionMenu1.OptionChanged += interactionMenuOptionChanged;
            interactionMenu1.BorderColorChanged += interactionMenuBorderColorChanged;
            interactionMenu1.FillColorChanged += interactionMenuFillColorChanged;
            interactionMenu1.IPColorChanged += interactionMenuIPColorChanged;
            interactionMenu1.ZoomChanged += interactionMenuZoomChanged;
            interactionMenu1.CellSizeChanged += interactionMenuCellSizeChanged;
        }

        private void interactionMenuCellSizeChanged(int obj)
        {
            interactionGrid1.CellSize = obj;
        }

        private void interactionMenuZoomChanged(int obj)
        {
            interactionGrid1.Zoom = obj;
        }

        private void interactionMenuIPColorChanged(System.Drawing.Color obj)
        {
            interactionGrid1.SelectedInteractionPointColor = obj;
        }

        private void interactionMenuFillColorChanged(System.Drawing.Color obj)
        {
            interactionGrid1.SelectedHitboxFillColor = obj;
        }

        private void interactionMenuBorderColorChanged(System.Drawing.Color obj)
        {
            interactionGrid1.SelectedHitboxBorderColor = obj;
        }

        private void interactionMenuOptionChanged(bool obj)
        {
            interactionGrid1.SelectingHitbox = obj;
        }

        private void interactionMenuInteractionPointSelectionChanged()
        {
            interactionGrid1.SelectedInteractionPoint = interactionMenu1.SelectedInteractionPoint;
        }

        private void interactionMenu1HitboxSelectionChanged()
        {
            interactionGrid1.SelectedHitbox = interactionMenu1.SelectedHitBox;
        }

        private void interactionMenuFrameSelectionChanged()
        {
            interactionGrid1.SelectedFrame = interactionMenu1.SelectedFrame;
        }

        private void animationCreatorSelectionChanged()
        {
            animationEditor1.Animation = animationCreator1.SelectedAnimation;
            animationPlayer1.Reset();
        }

        private void playerTimeChanged(int arg1, int arg2)
        {
            animationEditor1.SetCurrentFrameAndTime(arg1, arg2);
        }

        private void midChanged()
        {
            frameCreator1.ChangeMid(resizeableSpriteGridController1.MidX,
                resizeableSpriteGridController1.MidY);
        }

        private void animationChanged()
        {
            animationPlayer1.Animation = animationEditor1.Animation;
        }

        private void addClick(AnimationEditor obj)
        {
            animationEditor1.Selection = frameSelector1.GetSelection();
        }

        private void selectedIndexChanged(object sender, System.EventArgs e)
        {
            frameSelector1.Frames = frameCreator1.Frames;
            frameSelector1.BuildTable();
            animationEditor1.Animation = animationEditor1.Animation;
            if (tabControl1.SelectedIndex == 2)
            {
                interactionMenu1.UpdateFrameList(frameCreator1.Frames);
            }
        }

        private void graphicsLoaded()
        {
            resizeableSpriteGridController1.ReDraw();
        }

        private void frameCreator1_SelectionChanged()
        {
            if (frameCreator1.SelectedFrame == null)
                resizeableSpriteGridController1.Tiles = null;
            else
                resizeableSpriteGridController1.Tiles = frameCreator1.SelectedFrame.Tiles;
        }

        private void selectionChanged(object sender)
        {
            SpriteGFXBox gb = (SpriteGFXBox)sender;
            resizeableSpriteGridController1.NewTiles =
                gb.GetBitmapsFromSelectedTiles(false, false, TilePriority.AboveAllLayersP0);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if(keyData==Keys.Delete)
            {
                resizeableSpriteGridController1.DeleteSelection();
            }
            if (keyData == Keys.A)
            {
                resizeableSpriteGridController1.MoveLeft();
            }
            if (keyData == Keys.W)
            {
                resizeableSpriteGridController1.MoveUp();
            }
            if (keyData == Keys.S)
            {
                resizeableSpriteGridController1.MoveDown();
            }
            if (keyData == Keys.D)
            {
                resizeableSpriteGridController1.MoveRight();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
