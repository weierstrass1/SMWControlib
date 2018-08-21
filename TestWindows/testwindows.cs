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
            animationEditor1.AddClick += AnimationEditor1_AddClick;
        }

        private void AnimationEditor1_AddClick(AnimationEditor obj)
        {
            animationEditor1.Selection = frameSelector1.GetSelection();
        }

        private void selectedIndexChanged(object sender, System.EventArgs e)
        {
            frameSelector1.Frames = frameCreator1.Frames;
            frameSelector1.BuildTable();
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
