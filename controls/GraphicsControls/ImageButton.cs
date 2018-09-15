using SMWControlibBackend.Graphics;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SMWControlibControls.GraphicsControls
{
    public partial class ImageButton : Button
    {
        public int OffSetX1 { get; set; }
        public int OffSetX2 { get; set; }
        public int OffSetY1 { get; set; }
        public int OffSetY2 { get; set; }
        private Zoom zoom = 1;
        public int Zoom
        {
            get
            {
                return zoom;
            }
            set
            {
                zoom = value;
                Init();
            }
        }

        private Image source;
        public Image Source
        {
            get
            {
                return source;
            }
            set
            {
                source = value;
                Init();
            }
        }

        public ImageButton()
        {
            InitializeComponent();
            MouseUp += mouseHover;
            MouseDown += mouseDown;
            MouseEnter += mouseHover;
            MouseLeave += mouseLeave;
        }

        public void Init()
        {
            if (Source == null) return;
            Size = new Size(Source.Width * zoom, Source.Height * zoom);
            BackgroundImage = new Bitmap(Width, Height);
            Text = "";
            using (Graphics g = Graphics.FromImage(BackgroundImage))
            {
                drawBackImage(g);
                if (!Enabled)
                    drawRectangles(g, new SolidBrush(Color.FromArgb(176, 255, 255, 255)));
            }
        }

        private void drawBackImage(Graphics g)
        {
            if (Source == null) return;
            g.InterpolationMode = InterpolationMode.NearestNeighbor;
            g.DrawImage(Source, 0, 0,
                Width, Height);
        }
        private void drawRectangles(Graphics g, Brush b)
        {
            int w = Width;
            int h = Height;
            g.FillRectangle(b,
                    OffSetX1, OffSetY1,
                    w - OffSetX2, h - OffSetY2);
            g.FillRectangle(Brushes.Black, OffSetX1, OffSetY1, 
                zoom, zoom);
            g.FillRectangle(Brushes.Black, w - OffSetX2 - 1, OffSetY1,
                zoom, zoom);
            g.FillRectangle(Brushes.Black, OffSetX1, h - OffSetY2 - 1,
                zoom, zoom);
            g.FillRectangle(Brushes.Black, w - OffSetX2 - 1, h - OffSetY2 - 1,
                zoom, zoom);
        }

        private void mouseDown(object sender, MouseEventArgs e)
        {
            BackgroundImage = new Bitmap(Width, Height);
            using (Graphics g = Graphics.FromImage(BackgroundImage))
            {
                drawBackImage(g);
                drawRectangles(g, 
                    new SolidBrush(Color.FromArgb(160, 0, 0, 0)));
            }
        }

        private void mouseLeave(object sender, EventArgs e)
        {
            BackgroundImage = new Bitmap(Width, Height);
            using (Graphics g = Graphics.FromImage(BackgroundImage))
            {
                drawBackImage(g);
            }
        }

        private void mouseHover(object sender, EventArgs e)
        {
            BackgroundImage = new Bitmap(Width, Height);
            using (Graphics g = Graphics.FromImage(BackgroundImage))
            {
                drawBackImage(g);
                drawRectangles(g,
                    new SolidBrush(Color.FromArgb(96, 0, 0, 0)));
            }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
    }
}
