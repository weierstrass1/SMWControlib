using System;
using System.Drawing;

namespace SMWControlibBackend.Graphics
{
    public enum TileSP { SP01 = 0, SP23 = 1 };
    public enum TilePriority { BehindLayer3 = 0, BehindLayer3P1NotForcedAbove = 16, AboveAllLayersP0 = 32, AboveAllExceptLayer3P1ForcedAbove = 48};

    public class TileMask
    {
        public int XDisp;
        public string XDispString
        {
            get
            {
                int X = XDisp / Zoom;
                X -= 128;
                string sx = Convert.ToString(X, 16);
                if (sx.Length <= 1) sx = "0" + sx;
                sx = sx.Substring(sx.Length - 2, 2);
                sx = sx.ToUpper();
                sx = "$" + sx;
                return sx;
            }
        }
        public int YDisp;
        public string YDispString
        {
            get
            {
                int Y = YDisp / Zoom;
                Y -= 112;
                string sy = Convert.ToString(Y, 16);
                if (sy.Length <= 1) sy = "0" + sy;
                sy = sy.Substring(sy.Length - 2, 2);
                sy = sy.ToUpper();
                sy = "$" + sy;
                return sy;
            }
        }
        public TileSP SP { get; private set; } = TileSP.SP23;
        private PaletteId palette = PaletteId.PF;
        public string SizeString
        {
            get
            {
                if (Size == 8) return "$00";
                return "$02";
            }
        }
        public int Size
        {
            get
            {
                if (tile != null)
                    return tile.Size;
                return 0;
            }
        }
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
                    _Dirty = true;
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
                    _Dirty = true;
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
                    _Dirty = true;
                }
            }
        }

        public bool IsSelected { get; set; } = false;
        public bool UseGlobalPalette { get; set; } = true;
        private Bitmap graphics;

        private bool dirty = true;
        private bool _Dirty
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
                    if (dirty)
                        IsDirty?.Invoke(this);
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
                    _Dirty = true;
                }
            }
        }

        private readonly Tile tile;
        public string Tile
        {
            get
            {
                return tile.Code;
            }
        }
        public event Action<TileMask> IsDirty;

        public string Properties
        {
            get
            {
                return "$" + BitConverter.ToString(properties);
            }
        }
        private byte[] properties
        {
            get
            {
                int pal = ((int)palette << 1) & 0b00001110;
                byte[] prop = new byte[1];
                prop[0] = (byte)((int)Priority | pal | (int)SP);
                if (flipX) prop[0] += 64;
                if (flipY) prop[0] += 128;
                return prop;
            }
        }

        public TileMask(TileSP SP, Tile Tile, Zoom Zoom, bool FlipX, bool FlipY)
        {
            this.SP = SP;
            XDisp = 0;
            YDisp = 0;
            zoom = Zoom;
            flipX = FlipX;
            flipY = FlipY;
            tile = Tile;
            palette = ColorPalette.SelectedPalette;
            ColorPalette.SelectedGlobalPaletteChange += colorPalette_SelectedGlobalPaletteChange;
        }

        private void colorPalette_SelectedGlobalPaletteChange()
        {
            if (!IsSelected || !UseGlobalPalette) return;

            Palette = ColorPalette.SelectedPalette;
        }

        public Bitmap GetBitmap()
        {
            if (!_Dirty) return graphics;
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
            _Dirty = false;
            return graphics;
        }
        private void tileDirty(Tile t)
        {
            _Dirty = true;
            GetBitmap();
        }
        public void RemoveDirtyEvent()
        {
            tile.IsFullyDirty -= tileDirty;
        }
        public TileMask Clone()
        {
            TileMask tm = new TileMask(SP, tile, zoom, flipX, flipY)
            {
                XDisp = XDisp,
                YDisp = YDisp,
                palette = palette,
                Priority = Priority
            };
            tm.tile.IsFullyDirty += tm.tileDirty;
            return tm;
        }
    }
}