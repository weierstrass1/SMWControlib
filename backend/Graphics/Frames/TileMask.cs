using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend.Graphics.Frames
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
                    dirty = true;
                    palette = value;
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
                    dirty = true;
                    flipX = value;
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
                    dirty = true;
                    flipY = value;
                }
            }
        }
        private Bitmap graphics;
        private bool dirty = true;
        private Tile tile;

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

        public TileMask(TileSP SP, Tile Tile)
        {
            sp = SP;
            xDisp = 0;
            yDisp = 0;
            tile = Tile;
        }

        public Bitmap GetBitmap(Zoom zoom)
        {
            if (!dirty) return graphics;
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
            dirty = false;
            return graphics;
        }
    }
}
