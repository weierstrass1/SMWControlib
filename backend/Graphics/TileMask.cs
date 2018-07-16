using System;
using System.Drawing;

namespace SMWControlibBackend.Graphics
{
    public enum TileSP { SP01 = 0, SP23 = 1 };
    public enum TilePriority { BehindLayer3 = 0, BehindLayer3P1NotForcedAbove = 16, AboveAllLayersP0 = 32, AboveAllExceptLayer3P1ForcedAbove = 48};
    public class TileMask
    {
        public int xDisp;
        public int yDisp;
        private TileSP sp = TileSP.SP23;
        private PaletteId palette = PaletteId.pF;

        public PaletteId Palette
        {
            get
            {
                return palette;
            }
            set
            {
                if(palette != value)
                {
                    palette = value;
                    Dirty = true;
                }
            }
        }

        public TilePriority Priority { get; set; } = TilePriority.AboveAllLayersP0;

        private bool flipX = false;

        public bool FlipX
        {
            get
            {
                return flipX;
            }
            set
            {
                if(flipX != value)
                {
                    flipX = value;
                    Dirty = true;
                }
            }
        }

        private bool flipY = false;

        public bool FlipY
        {
            get
            {
                return flipY;
            }
            set
            {
                if (flipY != value)
                {
                    flipY = value;
                    Dirty = true;
                }
            }
        }

        public bool IsSelected { get; set; } = false;
        public bool UseGlobalPalette { get; set; } = true;
        private Bitmap graphics;

        private bool dirty = true;
        private bool Dirty
        {
            get
            {
                return dirty;
            }
            set
            {
                if(dirty!=value)
                {
                    dirty = value;
                    IsDirty?.Invoke();
                }
            }
        }

        private Zoom zoom = 2;
        public Zoom Zoom
        {
            get
            {
                return zoom;
            }
            set
            {
                if(zoom!=value)
                {
                    zoom = value;
                    Dirty = true;
                }
            }
        }

        private Tile tile;
        public event Action IsDirty;

        public string Properties
        {
            get
            {
                return BitConverter.ToString(properties);
            }
        }
        private byte[] properties
        {
            get
            {
                int pal = ((int)palette << 1) & 0b00001110;
                byte[] prop = new byte[1];
                prop[0] = (byte)((int)Priority | pal | (int)sp);
                if (flipX) prop[0] += 64;
                if (flipY) prop[0] += 128;
                return prop;
            }
        }

        public TileMask(TileSP SP, Tile Tile, Zoom Zoom, bool FlipX, bool FlipY)
        {
            sp = SP;
            xDisp = 0;
            yDisp = 0;
            zoom = Zoom;
            flipX = FlipX;
            flipY = FlipY;
            tile = Tile;
            palette = ColorPalette.SelectedPalette;
            ColorPalette.SelectedGlobalPaletteChange += ColorPalette_SelectedGlobalPaletteChange;
        }

        private void ColorPalette_SelectedGlobalPaletteChange()
        {
            if (graphics == null) return;
            if (!IsSelected && UseGlobalPalette) return;

            palette = ColorPalette.SelectedPalette;
        }

        public Bitmap GetBitmap()
        {
            if (!Dirty) return graphics;
            if (tile == null)
            {
                graphics = null;
                return graphics;
            }
            Bitmap bp = tile.GetImage(palette, zoom);
            if (bp == null)
            {
                graphics = null;
                return graphics;
            }

            graphics = new Bitmap(bp.Width, bp.Height);

            using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(graphics))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                g.DrawImage(bp, 0, 0);
            }
            RotateFlipType r = RotateFlipType.RotateNoneFlipNone;
            if (flipX && flipY) r = RotateFlipType.RotateNoneFlipXY;
            else if (flipX) r = RotateFlipType.RotateNoneFlipX;
            else if (flipY) r = RotateFlipType.RotateNoneFlipY;

            if (r != RotateFlipType.RotateNoneFlipNone) graphics.RotateFlip(r);
            Dirty = false;
            return graphics;
        }
    }
}
