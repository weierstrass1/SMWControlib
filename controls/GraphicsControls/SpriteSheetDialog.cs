using SMWControlibBackend.Graphics;
using SMWControlibBackend.Graphics.Frames;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMWControlibControls.GraphicsControls
{
    public partial class SpriteSheetDialog : Form
    {
        Bitmap[] bps;
        Bitmap ss;
        public static List<Frame> NewFrames { get; private set; } = new List<Frame>();
        Dictionary<Int32, byte> pal;
        public static int FrameWidth { get; private set; } = 0;
        public static int FrameHeight { get; private set; } = 0;
        public SpriteSheetDialog()
        {
            InitializeComponent();
            accept.Click += Accept_Click;
            lss.Click += Lss_Click;
            openFileDialog1.Filter = "Image Files |*.png;*.bmp;*.jpg";
            openFileDialog1.FileName = "";
            lfs.Click += Lfs_Click;
            frameSelector.SelectedIndexChanged += FrameSelector_SelectedIndexChanged;
        }

        private void FrameSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            pictureBox1.Width = (bps[frameSelector.SelectedIndex].Width * pictureBox1.Height) / bps[frameSelector.SelectedIndex].Height;
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            using (Graphics g = Graphics.FromImage(pictureBox1.Image))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                g.DrawImage(bps[frameSelector.SelectedIndex], 0, 0, pictureBox1.Width, pictureBox1.Height);
            }
        }

        private void Lfs_Click(object sender, EventArgs e)
        {
            pal = ImageProcessor.GetPalette(ss, ColorPalette.SelectedPalette);
        }

        private void Lss_Click(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog(this)==DialogResult.OK)
            {
                ss = (Bitmap)Image.FromFile(openFileDialog1.FileName);
                bps = ImageProcessor.GetFrames(ss, (int)w.Value, (int)h.Value);
                frameRefresh();
                pictureBox1.Width = (ss.Width * pictureBox1.Height) / ss.Height;
                pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                using (Graphics g = Graphics.FromImage(pictureBox1.Image))
                {
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                    g.DrawImage(ss, 0, 0, pictureBox1.Width, pictureBox1.Height);
                }
            }
        }

        void frameRefresh()
        {
            frameSelector.Items.Clear();
            if (bps == null || bps.Length <= 0)
            {
                frameSelector.Refresh();
                return;
            }
            for (int i = 0; i < bps.Length; i++)
            {
                frameSelector.Items.Add(i);
            }
            frameSelector.SelectedIndex = 0;
            frameSelector.Refresh();
        }

        private void Accept_Click(object sender, EventArgs e)
        {
            NewFrames.Clear();
            NewFrames = ImageProcessor.FromSpriteSheetToFrames(bps, pal, DynamicSpriteSizeDialog.DynSize, ColorPalette.SelectedPalette);
            DialogResult = DialogResult.OK;
            FrameWidth = (int)w.Value;
            FrameHeight = (int)h.Value;
            Dispose();
        }

        public static new DialogResult Show(IWin32Window Owner)
        {
            SpriteSheetDialog ssd = new SpriteSheetDialog();
            return ssd.ShowDialog(Owner);
        }
    }
}
