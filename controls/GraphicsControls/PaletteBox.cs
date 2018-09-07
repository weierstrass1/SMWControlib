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
                paletteBoxClick(null, mae);
            }
        }
        private Zoom zoom = SMWControlibBackend.Graphics.Zoom.X1;
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
                        getBehindMap();
                    }
                    catch
                    { }
                    reDraw();
                }
            }
        }
        private Bitmap behindBitmap { get; set; }
        public PaletteBox()
        {
            InitializeComponent();
            selection = new Rectangle(0, 0, zoom * 16, zoom);
            SizeChanged += paletteBoxSizeChanged;
            ColorPalette.GlobalPalletesChange += globalPalletesChange;
            ColorPalette.OneGlobalPaletteChange += oneGlobalPaletteChange;
            Click += paletteBoxClick;
        }

        private void paletteBoxClick(object sender, EventArgs e)
        {
            int y = ((MouseEventArgs)e).Y;
            if (y >= Height)
                y = Height - 1;
            if (y < 0) y = 0;

            y /= zoom;
            selection.Y = y * zoom;
            if (y <= (int)PaletteId.PF)
                ColorPalette.SelectedPalette = (PaletteId)(y + FirstPaletteToShow);
            reDraw();
        }

        private void globalPalletesChange()
        {
            getBehindMap();
            reDraw();
        }
        private void oneGlobalPaletteChange(PaletteId obj)
        {
            getBehindMap();
            reDraw();
        }

        private void paletteBoxSizeChanged(object sender, EventArgs e)
        {
            Image = new Bitmap(Width, Height);
            reDraw();
        }

        public void LoadPalette(string path)
        {
            if (File.Exists(path) && Path.GetExtension(path) == ".pal")
            {
                ColorPalette.GenerateGlobalPalettes(path, 16);
            }
        }

        private void getBehindMap()
        {
            int w = ColorPalette.GlobalPaletteSize * Zoom;
            int h = (ColorPalette.GlobalPalettesLength - FirstPaletteToShow) * Zoom;
            behindBitmap = new Bitmap(w, h);
            Brush br;
            for (byte i = 0; i < ColorPalette.GlobalPaletteSize; i++)
            {
                for (int j = FirstPaletteToShow, k = 0; j < ColorPalette.GlobalPalettesLength; j++, k++)
                {
                    using (Graphics g = Graphics.FromImage(behindBitmap))
                    {
                        g.InterpolationMode = InterpolationMode.NearestNeighbor;
                        br = new SolidBrush(ColorPalette.GetGlobalColor(i, (PaletteId)j));
                        g.FillRectangle(br, i * Zoom, k * Zoom, Zoom, Zoom);
                    }
                }
            }
        }

        private void reDraw()
        {
            Image = new Bitmap(Width, Height);
            using (Graphics g = Graphics.FromImage(Image as Bitmap))
            {
                g.InterpolationMode = InterpolationMode.NearestNeighbor;
                if (behindBitmap != null) g.DrawImage(behindBitmap, 0, 0);
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
