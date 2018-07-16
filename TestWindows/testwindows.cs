using System.Drawing;
using System.Windows.Forms;
using SMWControlibBackend.Graphics;
using System.Drawing.Drawing2D;

namespace TestWindows
{
    public partial class testwindows : Form
    {
        TileMask[] tms;
        int counter = 0;
        public testwindows()
        {
            InitializeComponent();
            gfxButton1.target = gfxBox1;
            gfxButton2.target = gfxBox1;
            paletteButton1.target = paletteBox1;
            gfxBox1.SelectionChanged += GfxBox1_SelectionChanged;
        }

        private void GfxBox1_SelectionChanged()
        {
            if(tms!=null)
            {
                for (int i = 0; i < tms.Length; i++)
                {
                    tms[i].IsDirty -= IsDirty;
                }
            }
            
            tms = gfxBox1.GetBitmapsFromSelectedTiles(false,false,TilePriority.AboveAllLayersP0);
            if (tms == null) return;
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Bitmap bp;
            for (int i = 0; i < tms.Length; i++)
            {
                bp = tms[i].GetBitmap();
                tms[i].IsDirty += IsDirty;
                tms[i].IsSelected = true;
                if (bp != null)
                {
                    using (Graphics g = Graphics.FromImage(pictureBox1.Image))
                    {
                        g.InterpolationMode = InterpolationMode.NearestNeighbor;
                        g.DrawImage(bp, tms[i].xDisp, tms[i].yDisp,
                            bp.Width, bp.Height);
                    }
                }
            }
        }

        private void IsDirty(TileMask tm)
        {
            Bitmap bp = tm.GetBitmap();
            using (Graphics g = Graphics.FromImage(pictureBox1.Image))
            {
                g.InterpolationMode = InterpolationMode.NearestNeighbor;

                g.DrawImage(bp, tm.xDisp, tm.yDisp,
                bp.Width, bp.Height);
            }
            counter++;
            if (counter == tms.Length)
            {
                counter = 0;
                Refresh();
            }
        }
    }
}
