using System;
using System.Drawing;
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
                numericUpDown1.Value = frameMask.Time;
                FlipX = frameMask.FlipX;
                FlipY = frameMask.FlipY;
                reDraw();
            }
        }
        public event Action<AnimationFrameEditor> AddClick,
            RemoveClick, ExchangeClick, FlipXChanged, 
            FlipYChanged, TimeChanged;

        public bool FlipX
        {
            set
            {
                checkBox1.Checked = value;
            }
        }


        public bool FlipY
        {
            set
            {
                checkBox2.Checked = value;
            }
        }

        private PictureBox rectangle, border;
        private int borderSize = 3;
        private Bitmap borderImg;

        public AnimationFrameEditor()
        {
            InitializeComponent();
            imageButton2.Enabled = false;
            imageButton3.Enabled = false;
            imageButton1.Click += addClick;
            imageButton2.Click += removeClick;
            imageButton3.Click += exchangeClick;
            numericUpDown1.ValueChanged += valueChanged;
            checkBox1.CheckedChanged += flipXCheckedChanged;
            checkBox2.CheckedChanged += flipYCheckedChanged;

            border = new PictureBox()
            {
                Margin = new Padding(0, 0, 0, 0),
                Padding = new Padding(0, 0, 0, 0),
                Parent = pictureBox1,
                Location = new Point(0, 0),
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(0, 0, 0, 0)
            };

            rectangle = new PictureBox()
            {
                Margin = new Padding(0, 0, 0, 0),
                Padding = new Padding(0, 0, 0, 0),
                Parent = border,
                Location = new Point(0, 0),
                Width = 0,
                Dock = DockStyle.Left,
                BackColor = Color.FromArgb(90, 0, 0, 255)
            };

            Brush b = new SolidBrush(Color.FromArgb(0, 0, 0, 0));
            borderImg = new Bitmap(border.Width, border.Height);
            using (Graphics g = Graphics.FromImage(borderImg))
            {
                g.FillRectangle(Brushes.Red, 0, 0, border.Width, borderSize);
                g.FillRectangle(Brushes.Red, 0, 0, borderSize, border.Height);
                g.FillRectangle(Brushes.Red, 0, border.Height - borderSize
                    , border.Width, borderSize);
                g.FillRectangle(Brushes.Red, border.Width - borderSize, 0
                    , borderSize, border.Height);
            }
        }

        public void SetBorderVisible(bool visible)
        {
            if (visible)
                border.Image = borderImg;
            else
                border.Image = null;
        }
        public void SetCurrentTimer()
        {
            rectangle.Width = 0;
        }

        public void SetCurrentTimer(int time)
        {
            float perc = (frameMask.Time - time) / (float)frameMask.Time;
            if (perc < 0) perc = 0;

            rectangle.Width = (int)(pictureBox1.Width * perc);
        }

        private void flipYCheckedChanged(object sender, EventArgs e)
        {
            if (frameMask == null)
            {
                FlipYChanged?.Invoke(this);
                return;
            }
            frameMask.FlipY = checkBox2.Checked;
            reDraw();
            FlipYChanged?.Invoke(this);
        }

        private void flipXCheckedChanged(object sender, EventArgs e)
        {
            if (frameMask == null)
            {
                FlipXChanged?.Invoke(this);
                return;
            }
            frameMask.FlipX = checkBox1.Checked;
            reDraw();
            FlipXChanged?.Invoke(this);
        }

        private void valueChanged(object sender, EventArgs e)
        {
            if (FrameMask != null)
            {
                FrameMask.Time = (int)numericUpDown1.Value;
                TimeChanged?.Invoke(this);
            }
            else
            {
                TimeChanged?.Invoke(this);
            }
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
                imageButton3.Enabled = false;
            }
            else
            {
                imageButton3.Enabled = true;
            }
            if (frameMask != null)
            {
                imageButton2.Enabled = true;
                label1.Text = frameMask.Frame.Name;
            }
            else
            {
                imageButton2.Enabled = false;
                imageButton2.Refresh();
                label1.Text = "Frame0";
            }
            pictureBox1.Image = new Bitmap(pictureBox1.Width - 3, 
                pictureBox1.Height - 3);
            Brush b = new SolidBrush(ColorPalette.GetGlobalColor(0));
            Bitmap bp = null;
            float per = 1;
            Font f = null;
            if (FrameMask != null)
            {
                bp = FrameMask.GetBitmap();
                if (bp != null)
                {
                    per = pictureBox1.Image.Width / (float)bp.Width;

                    if (bp.Width < bp.Height)
                    {
                        per = pictureBox1.Image.Height / (float)bp.Height;
                    }
                }
                
                f = new Font("Consolas", 11, FontStyle.Bold);
            }
            using (Graphics g = Graphics.FromImage(pictureBox1.Image))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                g.FillRectangle(b, 0, 0,
                    pictureBox1.Image.Width, pictureBox1.Image.Height);
                if (FrameMask != null)
                {
                    if (bp != null)
                    {
                        g.DrawImage(bp, 0, 0,
                            bp.Width * per, bp.Height * per);
                    }
                    g.DrawString("" + frameMask.Index, f,
                            Brushes.Black, 1, 1);
                }
            }
        }
    }
}
