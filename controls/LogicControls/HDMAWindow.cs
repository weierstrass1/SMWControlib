using SMWControlibBackend.Graphics;
using SMWControlibBackend.Logic.HDMA;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SMWControlibControls.LogicControls
{
    public partial class HDMAWindow : PictureBox
    {
        Zoom zoom = 1;

        public int Zoom
        {
            get
            {
                return zoom;
            }
            set
            {
                zoom = value;
                Width = (256 * zoom) + 3;
                Height = (224 * zoom) + 3;

                l1buffer = updateBuffer(layer1);
                l2buffer = updateBuffer(layer2);
                l3buffer = updateBuffer(layer3);
                spbuffer = updateBuffer(sprites);

                updatePicture(l1pb, l1buffer);
                updatePicture(l2pb, l2buffer);
                updatePicture(l3pb, l3buffer);
                updatePicture(sppb, spbuffer);
            }
        }
        List<Tuple<int, int, Color>> layer1;
        List<Tuple<int, int, Color>> layer2;
        List<Tuple<int, int, Color>> layer3;
        List<Tuple<int, int, Color>> sprites;

        Bitmap l1buffer, l2buffer, l3buffer, spbuffer;

        PictureBox l1pb, l2pb, l3pb, sppb;

        public HDMA HDMA;

        public HDMAWindow(HDMA hdma)
        {
            HDMA = hdma;
            InitializeComponent();
            l2pb = new PictureBox
            {
                Width = 256 * Zoom,
                Height = 224 * Zoom,
                Parent = this,
                Margin = new Padding(0, 0, 0, 0),
                Padding = new Padding(0, 0, 0, 0),
                Location = new Point(0, 0),
                BackColor = Color.FromArgb(0, 0, 0, 0)
            };
            l1pb = new PictureBox
            {
                Width = 256 * Zoom,
                Height = 224 * Zoom,
                Parent = l2pb,
                Margin = new Padding(0, 0, 0, 0),
                Padding = new Padding(0, 0, 0, 0),
                Location = new Point(0, 0),
                BackColor = Color.FromArgb(0, 0, 0, 0)
            };
            sppb = new PictureBox
            {
                Width = 256 * Zoom,
                Height = 224 * Zoom,
                Parent = l1pb,
                Margin = new Padding(0, 0, 0, 0),
                Padding = new Padding(0, 0, 0, 0),
                Location = new Point(0, 0),
                BackColor = Color.FromArgb(0, 0, 0, 0)
            };
            l3pb = new PictureBox
            {
                Width = 256 * Zoom,
                Height = 224 * Zoom,
                Parent = sppb,
                Margin = new Padding(0, 0, 0, 0),
                Padding = new Padding(0, 0, 0, 0),
                Location = new Point(0, 0),
                BackColor = Color.FromArgb(0, 0, 0, 0)
            };

            try
            {
                layer1 = loadImage(@"Images\Layer1\0.png");
                layer2 = loadImage(@"Images\Layer2\0.png");
                layer3 = loadImage(@"Images\Layer3\0.png");
                sprites = loadImage(@"Images\Sprites\0.png");

                l1buffer = updateBuffer(layer1);
                l2buffer = updateBuffer(layer2);
                l3buffer = updateBuffer(layer3);
                spbuffer = updateBuffer(sprites);

                updatePicture(l1pb, l1buffer);
                updatePicture(l2pb, l2buffer);
                updatePicture(l3pb, l3buffer);
                updatePicture(sppb, spbuffer);
            }
            catch { }
           
        }

        void updatePicture(PictureBox pb, Bitmap bp)
        {
            pb.Width = 256 * Zoom;
            pb.Height = 224 * Zoom;

            pb.Image = new Bitmap(pb.Width, pb.Height);

            using (Graphics g = Graphics.FromImage(pb.Image))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                g.DrawImage(bp, 0, 0, pb.Width, pb.Height);
            }
        }

        Bitmap updateBuffer(List<Tuple<int, int, Color>> img)
        {
            Bitmap bp = new Bitmap(256, 224);

            Color pC;
            int b;
            HDMALine hl;
            int R, G, B;
            foreach (Tuple<int, int, Color> T in img)
            {
                hl = HDMA[T.Item2];
                b = 15;
                if (hl != null)
                {
                    if (HDMA.Effect.Type == EffectType.Brightness)
                    {
                        if ((hl.Values[0, 0] & 0x80) != 0) b = 0;
                        else b = hl.Values[0, 0];
                    }
                }
                R = (int)(T.Item3.R * (b / 15f));
                G = (int)(T.Item3.G * (b / 15f));
                B = (int)(T.Item3.B * (b / 15f));

                pC = Color.FromArgb(R, G, B);

                bp.SetPixel(T.Item1, T.Item2, pC);
            }

            return bp;
        }

        List<Tuple<int, int, Color>> loadImage(string path)
        {
            List<Tuple<int, int, Color>> image = new List<Tuple<int, int, Color>>();

            FileStream fs = new FileStream(path, FileMode.Open);
            Bitmap bp = (Bitmap)Bitmap.FromStream(fs);

            Color c;

            for (int i = 0; i < bp.Width; i++)
            {
                for (int j = 0; j < bp.Height; j++)
                {
                    c = bp.GetPixel(i, j);
                    if(c.A == 255)
                    {
                        image.Add(new Tuple<int, int, Color>(i, j, c));
                    }
                    
                }
            }
            return image;
        }

        public void UpdateLayers()
        {
            l1buffer = updateBuffer(layer1);
            l2buffer = updateBuffer(layer2);
            l3buffer = updateBuffer(layer3);
            spbuffer = updateBuffer(sprites);

            updatePicture(l1pb, l1buffer);
            updatePicture(l2pb, l2buffer);
            updatePicture(l3pb, l3buffer);
            updatePicture(sppb, spbuffer);
        }
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
    }
}
