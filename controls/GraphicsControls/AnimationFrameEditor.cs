using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SMWControlibBackend.Graphics.Frames;
using SMWControlibBackend.Graphics;

namespace SMWControlibControls.GraphicsControls
{
    public partial class AnimationFrameEditor : UserControl
    {
        private FrameMask frameMask;
        public FrameMask FrameMask
        {
            get
            {
                return frameMask;
            }
            set
            {
                frameMask = value;
                reDraw();
            }
        }
        public event Action<AnimationFrameEditor> AddClick, 
            RemoveClick, ExchangeClick;

        public AnimationFrameEditor()
        {
            InitializeComponent();
            imageButton2.Enabled = false;
            imageButton3.Enabled = false;
            imageButton1.Init();
            imageButton2.Init();
            imageButton3.Init();
            imageButton1.Click += addClick;
            imageButton2.Click += removeClick;
            imageButton3.Click += exchangeClick;
            numericUpDown1.ValueChanged += valueChanged;
        }

        private void valueChanged(object sender, EventArgs e)
        {
            if (FrameMask != null)
                FrameMask.Time = (int)numericUpDown1.Value;
        }

        private void addClick(object sender, EventArgs e)
        {
            AddClick?.Invoke(this);
        }

        private void removeClick(object sender, EventArgs e)
        {
            RemoveClick?.Invoke(this);
        }

        private void exchangeClick(object sender, EventArgs e)
        {
            ExchangeClick?.Invoke(this);
        }

        private void reDraw()
        {
            if (frameMask == null || frameMask.Next == null)
            {
                imageButton2.Enabled = false;
                imageButton3.Enabled = false;
                imageButton2.Init();
                imageButton3.Init();
            }
            else
            {
                imageButton2.Enabled = true;
                imageButton3.Enabled = true;
                imageButton2.Init();
                imageButton3.Init();
            }
            if (frameMask != null) label1.Text = frameMask.Frame.Name;
            pictureBox1.Image = new Bitmap(pictureBox1.Width - 3, 
                pictureBox1.Height - 3);
            Brush b = new SolidBrush(ColorPalette.GetGlobalColor(0));
            Bitmap bp = FrameMask.Frame.GetBitmap();
            float per = pictureBox1.Image.Width / (float)bp.Width;
            if (bp.Width < bp.Height)
            {
                per = pictureBox1.Image.Height / (float)bp.Height;
            }
            Font f = new Font("Consolas", 11, FontStyle.Bold);
            using (Graphics g = Graphics.FromImage(pictureBox1.Image))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                g.FillRectangle(b, 0, 0,
                    pictureBox1.Image.Width, pictureBox1.Image.Height);
                if (FrameMask != null)
                    g.DrawImage(bp, 0, 0,
                    bp.Width * per, bp.Height * per);
                g.DrawString("" + frameMask.Index, f,
                        Brushes.Black, 1, 1);
            }
        }
    }
}
