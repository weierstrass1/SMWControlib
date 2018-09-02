using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SMWControlibBackend.Graphics.Frames;
using SMWControlibBackend.Graphics;

namespace SMWControlibControls.GraphicsControls
{
    public partial class AnimationPlayer : UserControl
    {
        private Animation animation = null;
        public Animation Animation
        {
            set
            {
                animation = value;
                if (animation == null) return;
                frames = animation.GetFrameMasks();
                framesBitmaps = animation.GetBitmaps();
                if (framesBitmaps == null || framesBitmaps.Length <= 0) return;
                Bitmap bp;
                for (int i = 0; i < framesBitmaps.Length; i++)
                {
                    bp = new Bitmap(framesBitmaps[i].Width * zoom,
                        framesBitmaps[i].Height * zoom);

                    using (Graphics g = Graphics.FromImage(bp))
                    {
                        g.InterpolationMode = InterpolationMode.NearestNeighbor;
                        g.DrawImage(framesBitmaps[i], 0, 0, bp.Width, bp.Height);
                    }
                    framesBitmaps[i] = bp;
                }
                if (frames != null && frames.Length > 0)
                {
                    int w = framesBitmaps[0].Width + 4;
                    int h = framesBitmaps[0].Height + 4;
                    player.Size = new Size(w, h);
                    if (index < 0)
                        index = 0;
                    else if (index >= frames.Length)
                        index = frames.Length - 1;

                    Index = index;

                } else Index = 0;
            }
        }
        private Zoom zoom = 2;
        public int Zoom
        {
            set
            {
                zoom = value;
                bool p = playing;
                Animation = animation;
                if (p) Playing = true;
            }
        }
        private FrameMask[] frames;
        private Bitmap[] framesBitmaps;
        private bool playing;
        public bool Playing
        {
            get
            {
                return playing;
            }
            private set
            {
                playing = value;
                timer1.Stop();
                if (playing)
                {
                    previus.Enabled = false;
                    previus.Source = Properties.Resources.previus;
                    play.Source = Properties.Resources.pause;
                    next.Source = Properties.Resources.stop;
                    timer1.Start();
                }
                else
                {
                    previus.Enabled = true;
                    previus.Source = Properties.Resources.previus;
                    play.Source = Properties.Resources.play;
                    next.Source = Properties.Resources.next;
                }
                    
            }
        }
        private int index = 0;
        public int Index
        {
            get
            {
                return index;
            }
            private set
            {
                Playing = false;
                index = value;
                if (frames != null && index >= 0 && index < frames.Length)
                {
                    timer = frames[index].Time;
                    player.Size = new Size(framesBitmaps[index].Width + 4
                        , framesBitmaps[index].Height + 4);
                    player.Image = framesBitmaps[index];
                }
                TimeChanged?.Invoke(index, timer);
            }
        }
        private int timer = 0;

        private int initInterval = 17;
        public int SpeedFPS
        {
            get
            {
                return (int)(1000f / timer1.Interval);
            }
            set
            {
                if (value < 1) value = 1;
                if (value > 1000) value = 1000;
                timer1.Interval = (int)Math.Round(1000f / value, 0);
                Interval = timer1.Interval;
                initInterval = Interval;
            }
        }
        public int Interval
        {
            get
            {
                return timer1.Interval;
            }
            set
            {
                timer1.Interval = value;
            }
        }

        public event Action<int, int> TimeChanged;

        public AnimationPlayer()
        {
            InitializeComponent();
            
            timer1.Tick += tick;
            previus.Click += previusClick;
            play.Click += playClick;
            next.Click += nextClick;
            zoomBox.SelectedIndexChanged += zoomBoxSelectedIndexChanged;
            speedBox.SelectedIndexChanged += speedBoxSelectedIndexChanged;
            player.SizeChanged += sizeChanged;
            SizeChanged += sizeChanged;
            zoomBox.SelectedIndex = 1;
            speedBox.SelectedIndex = 4;
        }

        public void Reset()
        {
            Index = 0;
        }

        private void sizeChanged(object sender, EventArgs e)
        {
            int w = Width - player.Width;
            w /= 2;
            int h = top.Height - player.Height;
            h /= 2;

            midTop.AutoScroll = false;
            if (w < 0)
            {
                w = 0;
                midTop.AutoScroll = true;
            }
            if (h < 0)
            {
                h = 0;
                midTop.AutoScroll = true;
            }

            topLeft.Width = w;
            topRight.Width = w;
            topTop.Height = h;
            bottomTop.Height = h;

            w = Width - bottom.Width;
            w /= 2;

            if (w < 0) w = 0;

            bottomLeft.Width = w;
            bottomRight.Width = w;
        }

        float[] intervals = { 0.125f, 0.25f, 0.5f, 0.75f, 1f,
            1.5f, 2f, 2.5f, 3f, 3.5f, 4 };
        private void speedBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            timer1.Interval = (int)(initInterval /
                intervals[speedBox.SelectedIndex]);
        }

        private void zoomBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            Zoom = zoomBox.SelectedIndex + 1;
        }

        private void nextClick(object sender, EventArgs e)
        {
            if (animation == null) return;
            if (playing)
            {
                Playing = false;
                Index = 0;
            }
            else if (index + 1 >= animation.Length)
                Index = 0;
            else
                Index = index + 1;
        }

        private void playClick(object sender, EventArgs e)
        {
            if (animation == null) return;
            Playing = !playing;
        }

        private void previusClick(object sender, EventArgs e)
        {
            if (animation == null) return;
            if (index - 1 < 0)
                Index = animation.Length - 1;
            else
                Index = index - 1;
        }

        private void tick(object sender, EventArgs e)
        {
            if (frames == null || frames.Length <= 0) return;
            if (animation == null) return;

            timer--;
            if(timer <= 0)
            {
                index++;
                if (index >= animation.Length)
                {
                    index = 0;
                    if (animation.PlayType == PlayType.OnlyOnce)
                        index = animation.Length - 1;
                }
                timer = frames[index].Time;
                player.Size = new Size(framesBitmaps[index].Width + 4, framesBitmaps[index].Height + 4);
                player.Image = framesBitmaps[index];
            }

            TimeChanged?.Invoke(index, timer);
        }
    }
}
