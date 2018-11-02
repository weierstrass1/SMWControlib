using SMWControlibBackend.Graphics;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SMWControlibControls.GraphicsControls
{
    public partial class GFXBox : PictureBox
    {
        #region Properties
        protected byte[,] colorMatrix;
        private Bitmap behindBitmap;
        private int zoom = 1;

        [
            Category("Image"),
            Description("The main image loaded from gfx.")
        ]
        public Bitmap BehindBitmap
        {
            get
            {
                return behindBitmap;
            }
            set
            {
                setBehindBitmap(value);
            }
        }

        [
            Category("Zoom"),
            Description("Used to applify gfx.")
        ]
        public int Zoom
        {
            get
            {
                return zoom;
            }
            set
            {
                setZoom(value);
            }
        }

        [
            Category("Size"),
            Description("")
        ]
        public int ImageHeigth
        {
            get
            {
                if (colorMatrix != null)
                    return colorMatrix.GetLength(1);
                return 0;
            }
            set
            {
                if (value < 1) value = 1;

                if (colorMatrix == null)
                {
                    colorMatrix = new byte[Math.Max(ImageWidth, 1),
                        value];
                }
                else
                {
                    byte[,] newcm = new byte[Math.Max(ImageWidth, 1),
                        value];

                    for (int i = 0; i < newcm.GetLength(0) &&
                        i < colorMatrix.GetLength(0); i++)
                    {
                        for (int j = 0; j < newcm.GetLength(1) &&
                        j < colorMatrix.GetLength(1); j++)
                        {
                            newcm[i, j] = colorMatrix[i, j];
                        }
                    }
                    colorMatrix = newcm;
                }
            }
        }

        [
            Category("Size"),
            Description("")
        ]
        public int ImageWidth
        {
            get
            {
                if (colorMatrix != null)
                    return colorMatrix.GetLength(0);
                return 0;
            }
            set
            {
                if (value < 1) value = 1;
                if (value > 128) value = 128;

                if (colorMatrix == null)
                {
                    colorMatrix = new byte[value,
                        Math.Max(ImageHeigth, 1)];
                }
                else
                {
                    byte[,] newcm = new byte[value,
                        Math.Max(ImageHeigth, 1)];

                    for (int i = 0; i < newcm.GetLength(0) && 
                        i < colorMatrix.GetLength(0); i++)
                    {
                        for (int j = 0; j < newcm.GetLength(1) &&
                        i < colorMatrix.GetLength(1); j++)
                        {
                            newcm[i, j] = colorMatrix[i, j];
                        }
                    }
                    colorMatrix = newcm;
                }
            }
        }
        #endregion

        public GFXBox()
        {
            InitializeComponent();
            BehindBitmap = new Bitmap(Size.Width, Size.Height);
            SizeChanged += sizeChanged;
            ColorPalette.OneGlobalPaletteChange += oneGlobalPaletteChange;
            ColorPalette.GlobalPalletesChange += globalPaletteChange;
            ColorPalette.SelectedGlobalPaletteChange += globalPaletteChange;
        }

        protected void oneGlobalPaletteChange(PaletteId obj)
        {
            if(ColorPalette.SelectedPalette == obj)
                generateBehindImage();
        }

        protected void globalPaletteChange()
        {
            generateBehindImage();
        }

        private void sizeChanged(object sender, EventArgs e)
        {
            BehindBitmap = new Bitmap(Size.Width, Size.Height);
        }

        private void setBehindBitmap(Bitmap newBitmap)
        {
            if (newBitmap == null) newBitmap = new Bitmap(Width, Height);
            behindBitmap = newBitmap;
            Image = behindBitmap;
            reDraw();
        }

        protected virtual void setZoom(int newZoom)
        {
            if (newZoom <= 0) return;

            zoom = newZoom;
            generateBehindImage();
        }

        public byte[] GetGFX()
        {
            return SnesGraphics.GetGFXFromColorMatrix(colorMatrix);
        }

        public virtual void LoadGFX(string path, int position)
        {
            if(position > ImageHeigth)
            {
                throw new Exception("Position is higher than the Image Height.");
            }
            byte[,] colors = SnesGraphics.GenerateGFX(path);

            for (int i = 0; i < colors.GetLength(0)
                && i < colorMatrix.GetLength(0); i++)
            {
                for (int j = position, k = 0; k < colors.GetLength(1)
                    && j < colorMatrix.GetLength(1); j++, k++)
                {
                    colorMatrix[i, j] = colors[i, k];
                }
            }

            generateBehindImage();
        }

        protected virtual void generateBehindImage()
        {
            if (ImageWidth == 0 || ImageHeigth == 0) return;
            behindBitmap = new Bitmap(ImageWidth * Zoom,
                ImageHeigth * Zoom);
            Color c;
            using (Graphics g = Graphics.FromImage(behindBitmap))
            {
                for (int i = 0; i < colorMatrix.GetLength(0); i++)
                {
                    for (int j = 0; j < colorMatrix.GetLength(1); j++)
                    {
                        if (colorMatrix[i, j] != 0)
                        {
                            c = ColorPalette.GetGlobalColor(colorMatrix[i, j]);
                            g.FillRectangle(new SolidBrush(c), i * Zoom, j * Zoom,
                                Zoom, Zoom);
                        }
                    }
                }
            }
            reDraw();
        }

        protected virtual void reDraw()
        {
            Image = new Bitmap(Image.Width, Image.Height);
            try
            {
                using (Graphics g = Graphics.FromImage(Image))
                {
                    Brush br = new SolidBrush(ColorPalette.GetGlobalColor(0));
                    g.FillRectangle(br, 0, 0, Width, Height);
                }
            }
            catch { }

            using (Graphics g = Graphics.FromImage(Image))
            {
                g.DrawImage(behindBitmap, 0, 0);
            }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
    }
}
