using System;
using System.Collections.Generic;
using System.Drawing;
using SMWControlibBackend.Graphics.Frames;

namespace SMWControlibBackend.Graphics
{
    public class ImageProcessor
    {
        public static Tile[,] Tiles8;
        public static Tile[,] Tiles16;
        public static TilePriority Priority;
        public static Frame FromSpriteSheetToFrames(Bitmap b, int midX, int midY, Dictionary<Int32, byte> Pal, string name, DynamicSize ds, PaletteId curId)
        {
            if (name == "" || name == null) name = "Frame";

            int tiles8x8; 
            List<TileOptimizer.Tile> tiles = TileOptimizer.TileOptimizer.Optimize(b, 8, 16, out tiles8x8);
            TileOptimizer.TileOptimizer.Reduce16x16Tiles(b, tiles);
            Frame f = FillGFX(b, midX, midY, curId, tiles, ds, Pal, tiles8x8);
            f.Name = name;
            f.MidX = midX;
            f.MidY = midY;

            return f;
        }

        public static Bitmap[] GetFrames(Bitmap bp, int FrameWidth, int FrameHeight)
        {
            int nw = bp.Width / FrameWidth;

            if (bp.Width % FrameWidth != 0) nw ++;

            int nh = bp.Height / FrameHeight;

            if (bp.Height % FrameHeight != 0) nh++;

            Bitmap[] bps = new Bitmap[nw * nh];
            int k = 0;
 
            for (int i = 0; i < nw; i++)
            {
                for (int j = 0; j < nh; j++)
                {
                    bps[k] = new Bitmap(FrameWidth, FrameHeight);
                    using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bps[k]))
                    {
                        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                        g.DrawImage(bp,
                            new Rectangle(0, 0, FrameWidth, FrameHeight),
                            new Rectangle(i * FrameWidth, j * FrameHeight, FrameWidth, FrameHeight),
                            GraphicsUnit.Pixel);
                    }
                    k++;
                }
            }
            return bps;
        }

        public static Dictionary<int, byte> GetPalette(Bitmap bp,PaletteId pid)
        {
            Dictionary<int, byte> ret = new Dictionary<int, byte>();

            List<Color> cols = new List<Color>();
            Color c;
            int r, g, b;
            for (int i = 0; i < bp.Width; i++)
            {
                for (int j = 0; j < bp.Height; j++)
                {
                    c = bp.GetPixel(i, j);
                    c = getSNESColor(c);
                    if (c.A == 255 && !cols.Contains(c))
                        cols.Add(c);
                }
            }

            Color minCol = Color.FromArgb(0, 0, 0, 0);
            float min;
            float cur;

            Color pcol;
            List<byte> findMin = new List<byte>();

            for (byte i = 1; i < 16; i++)
            {
                if (cols.Count <= 0)
                    continue;
                min = float.MaxValue;
                pcol = ColorPalette.GetGlobalColor(i, pid);
                pcol = getSNESColor(pcol);
                foreach (Color col in cols)
                {
                    cur = ColorDist(col, pcol);
                    if (cur < min)
                    {
                        minCol = col;
                        min = cur;
                    }
                }

                if (min == 0 && !ret.ContainsKey(minCol.ToArgb()))
                {
                    ret.Add(minCol.ToArgb(), i);
                    cols.Remove(minCol);
                }
                else
                    findMin.Add(i);
            }
            float cur1, min1;
            byte mini;
            while (findMin.Count > 0)
            {
                min1 = float.MaxValue;
                mini = 255;
                foreach (byte i in findMin)
                {
                    if (cols.Count <= 0)
                        continue;
                    pcol = ColorPalette.GetGlobalColor(i, pid);
                    pcol = getSNESColor(pcol);
                    foreach (Color col in cols)
                    {
                        if (!ret.ContainsKey(col.ToArgb()))
                        {
                            cur1 = ColorDist(col, pcol);
                            if (cur1 < min1)
                            {
                                minCol = col;
                                min1 = cur1;
                                mini = i;
                            }
                        }
                    }

                }
                if (mini == 255)
                    break;
                ret.Add(minCol.ToArgb(), mini);
                cols.Remove(minCol);
                findMin.Remove(mini);
            }

            foreach (KeyValuePair<int,byte> kvp in ret)
            {
                ColorPalette.SetGlobalColor(kvp.Value, Color.FromArgb(kvp.Key),pid);
            }
            
            return ret;
        }
        private static float ColorDist(Color col1, Color col2)
        {
            float r = (col1.R - col2.R) / 248f;
            float r2 = (col1.R + col2.R) / 496f;
            r *= r;
            float g = (col1.G - col2.G) / 248f;
            g *= g;
            float b = (col1.B - col2.B) / 248f;
            b *= b;

            /*float l = col1.GetBrightness() - col2.GetBrightness();
            l *= l;
            float s = col2.GetSaturation() - col1.GetSaturation();
            s *= s;
            float h = HueDist(col1, col2);*/
            //return ((2 + r2) * r) + (4 * g) + ((2 + (1 - r2)) * b);
            return r2 < 0.5 ?
                    (2 * r) + (4 * g) + (3 * b) :
                    (3 * r) + (4 * g) + (2 * b);

        }
        private static float HueDist(Color c1, Color c2)
        {
            float h1 = c1.GetHue();
            float h2 = c2.GetHue();

            float delta = Math.Min(h1 - h2, Math.Min(h1 - h2 - 360, h1 + 360 - h2));
            delta /= 360f;
            return delta * delta;
        }
        private static Color getSNESColor(Color c)
        {
            if (c.A != 255)
                return Color.FromArgb(0, 0, 0, 0);
            int r = c.R / 4;
            r *= 4;
            if (r % 8 > 3)
                r += 4;
            if (r > 248)
                r = 248;
            int g = c.G / 4;
            g *= 4;
            if (g % 8 > 3)
                g += 4;
            if (g > 248)
                g = 248;
            int b = c.B / 4;
            b *= 4;
            if (b % 8 > 3)
                b += 4;
            if (b > 248)
                b = 248;
            return Color.FromArgb(255, r, g, b);
        }
        public static int CompBright(Color c1, Color c2)
        {
            float b1 = c1.GetBrightness();
            float b2 = c2.GetBrightness();
            if (b1 < b2) return -1;
            if (b1 > b2) return 1;
            return 0;
        }

        public static Frame FillGFX(Bitmap bp, int midx, int midy, PaletteId pid, List<TileOptimizer.Tile> tiles, DynamicSize DZ, Dictionary<Int32, byte> pal, int tiles8x8)
        {
            Frame f = new Frame()
            {
                Dynamic = true,
                DynSize = DZ,
                MidX = midx,
                MidY = midy
            };
            byte[,] gfx = new byte[128, f.DynSize.Height];


            List<TileOptimizer.Tile> allTiles8x8 = tiles.FindAll(t => t.Width == 8);
            List<TileOptimizer.Tile> allTiles16x16 = tiles.FindAll(t => t.Width == 16);

            allTiles8x8.Sort(tilesorter);

            allTiles16x16.Sort(tilesorter);

            int x;
            int y;
            int i = 0;
            Color c;

            foreach (var tile in allTiles16x16)
            {
                x = (i % 16) * 8;
                y = (i / 16) * 16;

                for (int q = 0; q < 16 && tile.X + q < bp.Width; q++)
                {
                    for (int p = 0; p < 16 && tile.Y + p < bp.Height; p++)
                    {
                        c = bp.GetPixel(tile.X + q, tile.Y + p);
                        c = getSNESColor(c);
                        if (pal.ContainsKey(c.ToArgb()))
                            gfx[x + q,y + p] = pal[c.ToArgb()];
                    }
                }

                f.AddTile(new TileMask(TileSP.SP23, Tiles16[x / 8, y / 8], 2, false, false)
                {
                    XDisp = tile.X * 2,
                    YDisp = tile.Y * 2
                });

                i += 2;
            }

            int alternator = 0;

            foreach (var tile in allTiles8x8)
            {
                x = ((i % 16) * 8) + ((alternator % 2) * 8);
                y = ((i / 16) * 16) + (((alternator % 4) / 2) * 8);

                for (int q = 0; q < 8 && tile.X + q < bp.Width; q++)
                {
                    for (int p = 0; p < 8 && tile.Y + p < bp.Height; p++)
                    {
                        c = bp.GetPixel(tile.X + q, tile.Y + p);
                        c = getSNESColor(c);
                        if (pal.ContainsKey(c.ToArgb()))
                            gfx[x + q, y + p] = pal[c.ToArgb()];
                    }
                }

                f.AddTile(new TileMask(TileSP.SP23, Tiles8[x / 8, y / 8], 2, false, false)
                {
                    XDisp = tile.X * 2,
                    YDisp = tile.Y * 2
                });

                alternator++;

                if (alternator % 4 == 0)
                    i += 2;
            }

            f.GFX = SnesGraphics.GetGFXFromColorMatrix(gfx);
            f.Sort(tilesorter);
            return f;
        }

        static readonly int[] space24x16 = { 1, 1, 1 };
        static int tilesorter(TileMask tm1, TileMask tm2)
        {
            if (tm1.XDisp < tm2.XDisp) return -1;
            else if (tm1.XDisp > tm2.XDisp) return 1;

            if (tm1.YDisp < tm2.YDisp) return -1;
            else if (tm1.YDisp > tm2.YDisp) return 1;

            return 0;
        }

        static int tilesorter(TileOptimizer.Tile t1, TileOptimizer.Tile t2)
        {
            if (t1.Y < t2.Y)
                return -1;
            if (t1.Y > t2.Y)
                return 1;
            if (t1.X < t2.X)
                return -1;
            if (t1.X > t2.X)
                return 1;
            return 0;
        }
    }
}
