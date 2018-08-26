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
        private Animation animation;
        public Animation Animation
        {
            set
            {
                animation = value;
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
            }
        }
        private int timer = 0;

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
                timer1.Interval = 1000 / value;
                Interval = timer1.Interval;
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

        public AnimationPlayer()
        {
            InitializeComponent();
            
            timer1.Tick += tick;
            previus.Click += previusClick;
            play.Click += playClick;
            next.Click += nextClick;
        }

        private void nextClick(object sender, EventArgs e)
        {
            if (playing) Playing = false;
            else
            {
                if (index + 1 < animation.Length)
                    Index = index + 1;
            }
        }

        private void playClick(object sender, EventArgs e)
        {
            Playing = !playing;
        }

        private void previusClick(object sender, EventArgs e)
        {
            if (index - 1 >= 0)
                Index = index - 1;
        }

        private void tick(object sender, EventArgs e)
        {
            if (frames == null || frames.Length <= 0) return;

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
        }
    }
}
