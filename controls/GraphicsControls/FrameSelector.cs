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
using System.Runtime.InteropServices;
using System.Drawing.Text;

namespace SMWControlibControls.GraphicsControls
{
    public partial class FrameSelector : UserControl
    {
        public Frame[] Frames;

        private int framesPerRow = 8;
        public int FramesPerRow
        {
            get
            {
                return framesPerRow;
            }
            set
            {
                framesPerRow = value;
                BuildTable();
            }
        }
        private List<int> selection;
        public FrameSelector()
        {
            InitializeComponent();
            selection = new List<int>();
        }

        public Frame[] GetSelection()
        {
            Frame[] fs = new Frame[selection.Count];
            int j = 0;
            foreach(int i in selection)
            {
                fs[j] = Frames[i];
                j++;
            }
            return fs;
        }

        public void BuildTable()
        {
            if (Frames == null || Frames.Length <= 0)
            {
                tableLayoutPanel1.Controls.Clear();
                tableLayoutPanel1.RowStyles.Clear();
                tableLayoutPanel1.ColumnStyles.Clear();
                return;
            }
            int rows = Frames.Length / FramesPerRow;
            if (Frames.Length % FramesPerRow > 0) rows++;
            tableLayoutPanel1.Controls.Clear();
            tableLayoutPanel1.RowStyles.Clear();
            tableLayoutPanel1.ColumnStyles.Clear();

            tableLayoutPanel1.RowCount = rows * 2;
            tableLayoutPanel1.ColumnCount = FramesPerRow;
            tableLayoutPanel1.Width = tableLayoutPanel1.ColumnCount * 128;
            tableLayoutPanel1.Height = tableLayoutPanel1.RowCount * 72;


            for (int i = 0; i < tableLayoutPanel1.RowCount; i++)
            {
                if (i % 2 == 0)
                {
                    tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 128));
                }
                else
                {
                    tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 16));
                }
            }

            for (int i = 0; i < tableLayoutPanel1.ColumnCount; i++)
            {
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 128));
            }

            PictureBox pb;
            Label lab;
            int x, y;
            Brush b = new SolidBrush(ColorPalette.GetGlobalColor(0));
            for (int i = 0; i < Frames.Length; i++)
            {
                x = i % FramesPerRow;
                y = i / FramesPerRow;
                y *= 2;
                pb = new PictureBox
                {
                    Dock = DockStyle.Fill,
                    BorderStyle = BorderStyle.Fixed3D
                };
                pb.Click += pictureBoxClick;
                tableLayoutPanel1.Controls.Add(pb, x, y);
                pb.Image = new Bitmap(pb.Width - 3, pb.Height - 3);
                
                using (Graphics g = Graphics.FromImage(pb.Image))
                {
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                    g.FillRectangle(b, 0, 0, pb.Image.Width, pb.Image.Height);
                    g.DrawImage(Frames[i].GetBitmap(), 0, 0, pb.Image.Width, pb.Image.Height);
                }
                lab = new Label
                {
                    TextAlign = ContentAlignment.MiddleCenter,
                    Text = Frames[i].Name
                };
                tableLayoutPanel1.Controls.Add(lab, x, y + 1);
            }
        }

        private void pictureBoxClick(object sender, EventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            int row = tableLayoutPanel1.GetRow(pb) / 2;
            int column = tableLayoutPanel1.GetColumn(pb);
            int index = (row * FramesPerRow) + column;
            PictureBox pbaux;
            int r, col;
            Brush b = new SolidBrush(ColorPalette.GetGlobalColor(0));
            bool isSelected = false;
            foreach(int i in selection)
            {
                if (i == index) isSelected = true;
            }
            if (ModifierKeys != Keys.Control)
            {
                foreach (int i in selection)
                {
                    r = i / FramesPerRow;
                    col = i % FramesPerRow;
                    pbaux =
                        (PictureBox)tableLayoutPanel1.
                        GetControlFromPosition(col, r * 2);
                    pbaux.Image = new Bitmap(pbaux.Width - 3,
                        pbaux.Height - 3);
                    using (Graphics g = Graphics.FromImage(pbaux.Image))
                    {
                        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                        g.FillRectangle(b, 0, 0, 
                            pbaux.Image.Width, pbaux.Image.Height);
                        g.DrawImage(Frames[i].GetBitmap(),
                        0, 0, pbaux.Image.Width, pbaux.Image.Height);
                    }
                }
                selection.Clear();
            }

            if (!isSelected)
            {
                Font f = new Font("Consolas", 11, FontStyle.Bold);
                Color c = Color.FromArgb(120, 0, 0, 128);
                Image bp = pb.Image;
                pb.Image = new Bitmap(pb.Width - 3, pb.Height - 3);
                string numb = "" + (selection.Count + 1);
                while (numb.Length < 3) numb = "0" + numb;

                using (Graphics g = Graphics.FromImage(pb.Image))
                {
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                    g.FillRectangle(b, 0, 0,
                                pb.Image.Width, pb.Image.Height);
                    g.DrawImage(Frames[index].GetBitmap(),
                        0, 0, pb.Image.Width, pb.Image.Height);
                    g.FillRectangle(new SolidBrush(c), 0, 0,
                        pb.Image.Width, pb.Image.Height);
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bilinear;
                    g.FillEllipse(Brushes.LightGray, 0, 0, 32, 18);
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                    g.DrawString(numb, f,
                        Brushes.Black, 1, 1);
                }
                selection.Add(index);
            }
            
        }
    }
}
