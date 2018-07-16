using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using SMWControlibBackend.Graphics;

namespace SMWControlibControls.GraphicsControls
{
    public partial class PaletteBox : PictureBox
    {
        private Rectangle selection;
        private PaletteId firstPaletteToShow;
        public int FirstPaletteToShow
        { get
            {
                return (int)firstPaletteToShow;
            }
         set
            {
                if (value > 15) value = 15;
                if (value < 0) value = 0;

                firstPaletteToShow = (PaletteId)value;
                MouseEventArgs mae = new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0);
                PaletteBox_Click(null, mae);
            }
        }
        private Zoom zoom = SMWControlibBackend.Graphics.Zoom.x1;
        public int Zoom
        { get
            {
                return zoom;
            }
          set
            {
                if (zoom != value)
                {
                    selection.X /= zoom;
                    selection.Y /= zoom;
                    selection.Width /= zoom;
                    selection.Height /= zoom;
                    zoom = value;
                    selection.X *= zoom;
                    selection.Y *= zoom;
                    selection.Width *= zoom;
                    selection.Height *= zoom;
                    try
                    {
                        GetBehindMap();
                    }
                    catch
                    { }
                    ReDraw();
                }
            }
        }
        private Bitmap BehindBitmap { get; set; }
        public PaletteBox()
        {
            InitializeComponent();
            selection = new Rectangle(0, 0, zoom * 16, zoom);
            SizeChanged += PaletteBox_SizeChanged;
            ColorPalette.GlobalPalletesChange += GlobalPalletesChange;
            ColorPalette.OneGlobalPaletteChange += OneGlobalPaletteChange;
            Click += PaletteBox_Click;
        }

        private void PaletteBox_Click(object sender, EventArgs e)
        {
            int y = ((MouseEventArgs)e).Y;
            if (y >= Height)
                y = Height - 1;
            if (y < 0) y = 0;

            y /= zoom;
            selection.Y = y * zoom;
            if (y <= (int)PaletteId.pF)
                ColorPalette.SelectedPalette = (PaletteId)(y + FirstPaletteToShow);
            ReDraw();
        }

        private void GlobalPalletesChange()
        {
            GetBehindMap();
            ReDraw();
        }
        private void OneGlobalPaletteChange(PaletteId obj)
        {
            GetBehindMap();
            ReDraw();
        }

        private void PaletteBox_SizeChanged(object sender, EventArgs e)
        {
            Image = new Bitmap(Width, Height);
            ReDraw();
        }

        public void LoadPalette(string path)
        {
            if (File.Exists(path) && Path.GetExtension(path) == ".pal")
            {
                ColorPalette.GenerateGlobalPalettes(path, 16);
            }
        }

        private void GetBehindMap()
        {
            int w = ColorPalette.GlobalPaletteSize * Zoom;
            int h = (ColorPalette.GlobalPalettesLength - FirstPaletteToShow) * Zoom;
            BehindBitmap = new Bitmap(w, h);
            Brush br;
            for (byte i = 0; i < ColorPalette.GlobalPaletteSize; i++)
            {
                for (int j = FirstPaletteToShow, k = 0; j < ColorPalette.GlobalPalettesLength; j++, k++)
                {
                    using (Graphics g = Graphics.FromImage(BehindBitmap))
                    {
                        g.InterpolationMode = InterpolationMode.NearestNeighbor;
                        br = new SolidBrush(ColorPalette.GetGlobalColor(i, (PaletteId)j));
                        g.FillRectangle(br, i * Zoom, k * Zoom, Zoom, Zoom);
                    }
                }
            }
        }

        private void ReDraw()
        {
            Image = new Bitmap(Width, Height);
            using (Graphics g = Graphics.FromImage(Image as Bitmap))
            {
                g.InterpolationMode = InterpolationMode.NearestNeighbor;
                if (BehindBitmap != null) g.DrawImage(BehindBitmap, 0, 0);
                g.DrawRectangle(Pens.White, selection);
            }
            Refresh();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
    }
}
