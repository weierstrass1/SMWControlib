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
using backend.Graphics.Frames;

namespace TestWindows
{
    public partial class testwindows : Form
    {
        public testwindows()
        {
            InitializeComponent();
            ColorPalette.GeneratePalette("Doom3.pal", 16);
            ColorPalette.SelectedPalette = PaletteId.p5;
            gfxButton1.target = gfxBox1;
            gfxButton2.target = gfxBox1;
            gfxBox1.SelectionChanged += GfxBox1_SelectionChanged;
        }

        private void GfxBox1_SelectionChanged()
        {
            TileMask[] tms = gfxBox1.GetBitmapsFromSelectedTiles(false,false,TilePriority.AboveAllLayersP0);
            if (tms == null) return;
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Bitmap bp;
            for (int i = 0; i < tms.Length; i++)
            {
                bp = tms[i].GetBitmap(gfxBox1.TileZoom);
                using (Graphics g = Graphics.FromImage(pictureBox1.Image))
                {
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                    g.DrawImage(bp, tms[i].xDisp, tms[i].yDisp,
                        bp.Width, bp.Height);
                    
                }
            }
        }
    }
}
