using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using controls;
using backend;

namespace TestWindows
{
    public partial class testwindows : Form
    {
        public testwindows()
        {
            InitializeComponent();
            ColorPalette.GeneratePalette("Doom3.pal", 16);
            ColorPalette.SelectedPalette = PaletteId.p5;
            gfxBox1.GetTiles("Doom.bin", TileSize.Size16x16, BaseTile.Top);
            gfxBox1.GetTiles("Doom3.bin", TileSize.Size16x16, BaseTile.Botton);
            gfxBox1.SelectionChanged += GfxBox1_SelectionChanged;
        }

        private void GfxBox1_SelectionChanged()
        {
            Tuple<int, int, Bitmap>[] tuples = gfxBox1.GetBitmapsFromSelectedTiles();
            if (tuples == null) return;
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            for (int i = 0; i < tuples.Length; i++)
            {
                using (Graphics g = Graphics.FromImage(pictureBox1.Image))
                {
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                    g.DrawImage(tuples[i].Item3, tuples[i].Item1, tuples[i].Item2,
                        tuples[i].Item3.Width, tuples[i].Item3.Height);
                    
                }
            }
        }
    }
}
