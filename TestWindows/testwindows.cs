using System.Windows.Forms;
using SMWControlibBackend.Graphics;

namespace TestWindows
{
    public partial class testwindows : Form
    {
        public testwindows()
        {
            InitializeComponent();
            gfxButton1.target = gfxBox1;
            gfxButton2.target = gfxBox1;
            paletteButton1.target = paletteBox1;
            gfxBox1.SelectionChanged += selectionChanged;
            frameCreator1.SelectionChanged += frameCreator1_SelectionChanged;
        }

        private void frameCreator1_SelectionChanged()
        {
            if (frameCreator1.SelectedFrame == null)
                resizeableSpriteGridController1.Tiles = null;
            else
                resizeableSpriteGridController1.Tiles = frameCreator1.SelectedFrame.Tiles;
        }

        private void selectionChanged()
        {
            resizeableSpriteGridController1.NewTiles = 
                gfxBox1.GetBitmapsFromSelectedTiles(false, false, TilePriority.AboveAllLayersP0);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if(keyData==Keys.Delete)
            {
                resizeableSpriteGridController1.DeleteSelection();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
