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

        public Image Source
        {
            get
            {
                return BackgroundImage;
            }
            set
            {
                if (value == null) return;

                BackgroundImage = new Bitmap(Width, Height);
                using (Graphics g = Graphics.FromImage(BackgroundImage))
                {
                    g.InterpolationMode = InterpolationMode.NearestNeighbor;
                    g.DrawImage(value, 0, 0, Width, Height);
                }

            }
        }

        public Image Border
        {
            get
            {
                return border.Image;
            }
            set
            {
                if (value == null) return;
                border.Image = new Bitmap(Width, Height);
                using (Graphics g = Graphics.FromImage(border.Image))
                {
                    g.InterpolationMode = InterpolationMode.NearestNeighbor;
                    g.DrawImage(value, 0, 0, Width, Height);
                }
            }
        }

        public Color Clicked { get; set; } = Color.FromArgb(120, 16, 16, 16);
        public Color Disabled { get; set; } = Color.FromArgb(200, 255, 255, 255);

        public Color Hovered { get; set; } = Color.FromArgb(120, 48, 48, 48);

        PictureBox pressedBox, border;

        public ImageButton()
        {
            InitializeComponent();
            pressedBox = new PictureBox
            {
                Margin = new Padding(0, 0, 0, 0),
                Padding = new Padding(0, 0, 0, 0),
                Parent = this,
                Location = new Point(0, 0),
                Width = Width,
                Height = Height,
                BackColor = Color.FromArgb(0, 0, 0, 0)
            };

            border = new PictureBox
            {
                Margin = new Padding(0, 0, 0, 0),
                Padding = new Padding(0, 0, 0, 0),
                Parent = pressedBox,
                Location = new Point(0, 0),
                Width = Width,
                Height = Height,
                BackColor = Color.FromArgb(0, 0, 0, 0)
            };

            border.MouseDown += mouseDown;
            border.MouseMove += mouseMove;
            border.MouseLeave += mouseLeave;
            EnabledChanged += enabledChanged;
        }

        private bool detectCollision(int x, int y)
        {
            if ((x >= OffSetX1 && x <= Width - OffSetX2)
                && (y >= OffSetY1 && y <= Height - OffSetY2))
                return true;

            return false;
        }
        private void drawRectangle(Color c)
        {
            pressedBox.Image = new Bitmap(Width, Height);

            using (Graphics g = Graphics.FromImage(pressedBox.Image))
            {
                g.FillRectangle(new SolidBrush(c),
                    OffSetX1, OffSetY1,
                    Width - OffSetX1 - OffSetX2, Height - OffSetY1 - OffSetY2);
            }
        }

        private void enabledChanged(object sender, EventArgs e)
        {
            if (!Enabled)
                drawRectangle(Disabled);
            else
                drawRectangle(Color.FromArgb(0, 0, 0, 0));
        }

        private void mouseDown(object sender, MouseEventArgs e)
        {
            if (detectCollision(e.X, e.Y))
            {
                PerformClick();
                drawRectangle(Clicked);
            }
        }

        private void mouseLeave(object sender, EventArgs e)
        {
            drawRectangle(Color.FromArgb(0, 0, 0, 0));
        }

        private void mouseMove(object sender, MouseEventArgs e)
        {
            if (detectCollision(e.X, e.Y)) 
            {
                drawRectangle(Hovered);
            }
            else
            {
                drawRectangle(Color.FromArgb(0, 0, 0, 0));
            }

        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
    }
}
