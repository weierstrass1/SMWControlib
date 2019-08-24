using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using SMWControlibBackend.Graphics.Frames;

namespace SMWControlibBackend.Graphics
{
    public class ImageNode : IComparable<ImageNode>
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public int Size { get; private set; }
        int[] min, max;
        public int Cost { get; private set; }
        public int HeuristicBlocks { get; private set; }
        public int Heuristic { get; private set; }
        public int Length8 { get; private set; }
        public int Length16 { get; private set; }
        public ImageNode Father { get; private set; }

        private static int getLeft(int[] min)
        {
            int Left = -1;
            for (int i = 0; i < min.Length; i++)
            {
                if (min[i] >= 0)
                {
                    Left = i;
                    break;
                }
            }
            return Left;
        }

        static int getRight(int[] min)
        {
            int Right = -1;
            for (int i = min.Length - 1; i >= 0; i--) 
            {
                if (min[i] >= 0)
                {
                    Right = i;
                    break;
                }
            }
            return Right;
        }

        void getCost()
        {
            bool found = false;
            ImageNode aux = Father;
            if (Size == 16)
            {
                while (aux != null)
                {
                    if (aux.Y == Y && aux.Size==16 && Math.Abs(X - aux.X) <= 8)
                    {
                        found = true;
                        break;
                    }
                    
                    aux = aux.Father;
                }
            }
            if (Father != null)
            {
                Length8 = Father.Length8;
                Length16 = Father.Length16;
                Cost = Father.Cost;
            }
            else
            {
                Cost = 0;
            }
            if (Size == 16)
            {
                Length16++;
                if (!found)
                    Cost += 256;
                else
                    Cost += 128;
            }
            else
            {
                Length8++;
                Cost += 64;
            }
        }

        void getHeuristic()
        {
            int pixels = 0;
            for (int i = 0; i < min.Length; i++)
            {
                if (min[i] >= 0)
                {
                    pixels += max[i] + 1;
                    pixels -= min[i];
                }
            }

            int p = pixels / 256;
            if (pixels % 256 != 0) p++;
            HeuristicBlocks = p;
            p = pixels / 64;
            if (pixels % 64 != 0) p++;
            Heuristic += (p * 64);
        }

        private static void getMinMax(out int MinY, out int MaxY,
            int Left, int[] min, int size)
        {
            MinY = min[Left];
            MaxY = min[Left];

            for (int i = Left; i < Left + size && i < min.Length; i++)
            {
                if(min[i] < MinY)
                {
                    MinY = min[i];
                    if (MinY <= Math.Max(min[Left] - size + 1, 0))
                    {
                        MinY = Math.Max(min[Left] - size + 1, 0);
                        break;
                    }
                }
            }
        }

        static LinkedList<ImageNode> getNodes(ImageNode r, int Left, int Right,
            int[] min, int[] max, int size, int MinY, int MaxY)
        {
            LinkedList<ImageNode> imns =
                new LinkedList<ImageNode>();
            ImageNode imn;

            for (int i = MinY; i <= MaxY; i++)
            {
                imn = new ImageNode()
                {
                    X = Math.Min(Left, Right - size + 1),
                    Y = i,
                    Father = r,
                    Length8 = 0,
                    min = new int[min.Length],
                    max = new int[max.Length],
                    Size = size
                };
                imn.getCost();
                
                Buffer.BlockCopy(min, 0, imn.min, 0, min.Length * sizeof(int));
                Buffer.BlockCopy(max, 0, imn.max, 0, max.Length * sizeof(int));
                for (int j = Left; j < Left + size && j < min.Length; j++) 
                {
                    if (imn.min[j] >= 0 && imn.max[j] >= 0)
                    {
                        if (i <= imn.min[j])
                        {
                            imn.min[j] = i + size;
                        }
                        if (i + size - 1 >= imn.max[j])
                        {
                            imn.max[j] = i - 1;
                        }
                        if (imn.max[j] < imn.min[j])
                        {
                            imn.min[j] = -1;
                            imn.max[j] = -1;
                        }
                    }
                }
                imn.getHeuristic();
                imns.AddLast(imn);
            }
            return imns;
        }

        public static LinkedList<ImageNode>
            GetNodes(int[] min,int[] max)
        {
            int Left = getLeft(min);
            int Right = getRight(min);

            if (Left < 0) return null;

            getMinMax(out int MinY, out int MaxY,
                Left, min, 8);

            LinkedList<ImageNode> imns =
                getNodes(null, Left, Right, min, max, 8, MinY, MaxY);

            getMinMax(out MinY, out MaxY,
                Left, min, 16);

            LinkedList<ImageNode> imns2 = 
                getNodes(null, Left, Right, min, max, 16, MinY, MaxY);
            foreach (ImageNode imn in imns2)
            {
                imns.AddLast(imn);
            }

            return imns;
        }

        public static LinkedList<ImageNode>
            GetNodes(ImageNode root)
        {
            int Left = getLeft(root.min);
            int Right = getRight(root.min);

            if (Left < 0) return null;

            getMinMax(out int MinY, out int MaxY,
                Left, root.min, 8);

            LinkedList<ImageNode> imns =
                getNodes(root, Left, Right, root.min, root.max, 8, MinY, MaxY);

            getMinMax(out MinY, out MaxY,
                Left, root.min, 16);

            LinkedList<ImageNode> imns2 =
                getNodes(root, Left, Right, root.min, root.max, 16, MinY, MaxY);
            foreach (ImageNode imn in imns2)
            {
                imns.AddLast(imn);
            }

            return imns;
        }

        public int CompareTo(ImageNode other)
        {
            int blocks = Length8 + Length16 + HeuristicBlocks;
            int blockso = other.Length8 + other.Length16 + other.HeuristicBlocks;
            if (blocks < blockso) return -1;
            else if (blocks > blockso) return 1;

            if (Length16 < other.Length16) return -1;
            else if (Length16 > other.Length16) return 1;

            int f = Cost + Heuristic;
            int fo = other.Cost + other.Heuristic;
            if (f < fo) return -1;
            else if (f > fo) return 1;
            return 0;
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(ImageNode))
                return false;
            ImageNode imn = (ImageNode)obj;

            return X == imn.X && Y == imn.Y && Size == imn.Size && Length8 == imn.Length8 && Length16 == imn.Length16 && Cost == imn.Cost && Heuristic == imn.Heuristic;
        }

        public override int GetHashCode()
        {
            var hashCode = -66000694;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            hashCode = hashCode * -1521134295 + Size.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<int[]>.Default.GetHashCode(min);
            hashCode = hashCode * -1521134295 + EqualityComparer<int[]>.Default.GetHashCode(max);
            hashCode = hashCode * -1521134295 + Cost.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<ImageNode>.Default.GetHashCode(Father);
            return hashCode;
        }
    }
    public class ImageProcessor
    {
        public static Tile[,] Tiles8;
        public static Tile[,] Tiles16;
        public static TilePriority Priority;
        public static List<Frame> FromSpriteSheetToFrames(Bitmap[] b, Dictionary<Int32, byte> Pal, DynamicSize ds, PaletteId curId)
        {
            Frame f;
            List<Frame> frames = new List<Frame>();

            for (int i = 0; i < b.Length; i++)
            {
                ImageNode imn = CropFrameInTiles(b[i]);
                f = FillGFX(b[i], curId, imn, ds, Pal);
                f.Name = "Frame" + i;
                frames.Add(f);
            }

            return frames;
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

        public static Dictionary<Int32,byte> GetPalette(Bitmap bp,PaletteId pid)
        {
            Dictionary<Int32, byte> ret = new Dictionary<int, byte>();

            List<Color> cols = new List<Color>();
            Color c;
            for (int i = 0; i < bp.Width; i++)
            {
                for (int j = 0; j < bp.Height; j++)
                {
                    c = bp.GetPixel(i, j);
                    if (c.A == 255 && !cols.Contains(c)) 
                    {
                        cols.Add(c);
                    }
                }
            }

            Color minCol = Color.FromArgb(0, 255, 255, 255);
            int min;
            int cur;
            int r, g, b;

            Color pcol;

            for (byte i = 1; i < 16; i++)
            {
                if (cols.Count > 0)
                {
                    min = -1;
                    pcol = ColorPalette.GetGlobalColor(i, pid);
                    foreach (Color col in cols)
                    {
                        r = col.R - pcol.R;
                        r *= r;
                        g = col.G - pcol.G;
                        g *= g;
                        b = col.B - pcol.B;
                        b *= b;
                        cur = r + g + b;
                        if (min < 0 || cur < min)
                        {
                            minCol = col;
                            min = cur;
                        }
                    }
                    ret.Add(minCol.ToArgb(), i);
                    cols.Remove(minCol);
                }
            }

            foreach(KeyValuePair<int,byte> kvp in ret)
            {
                ColorPalette.SetGlobalColor(kvp.Value, Color.FromArgb(kvp.Key),pid);
            }
            
            return ret;
        }

        public static int CompBright(Color c1, Color c2)
        {
            float b1 = c1.GetBrightness();
            float b2 = c2.GetBrightness();
            if (b1 < b2) return -1;
            if (b1 > b2) return 1;
            return 0;
        }

        public static Frame FillGFX(Bitmap bp,PaletteId pid,ImageNode imn,DynamicSize DZ, Dictionary<Int32, byte> pal)
        {
            Frame f = new Frame()
            {
                Dynamic = true,
                DynSize= DZ
            };
            byte[,] gfx = new byte[128, f.DynSize.Height];

            ImageNode[] FollowsX = new ImageNode[imn.Length16];

            ImageNode imnaux = imn;
            ImageNode imnaux2;
            int next = 0;
            while(imnaux != null)
            {
                imnaux2 = imnaux.Father;
                while (imnaux2 != null && imnaux.Size == 16) 
                {
                    if (imnaux2.Size == 16 && imnaux.Y == imnaux2.Y 
                        && Math.Abs(imnaux.X - imnaux2.X) <= 8) 
                    {
                        if (imnaux.X < imnaux2.X)
                        {
                            FollowsX[next] = imnaux;
                            FollowsX[next + 1] = imnaux2;
                        }
                        else
                        {
                            FollowsX[next] = imnaux2;
                            FollowsX[next + 1] = imnaux;
                        }
                        next += 2;
                    }
                    imnaux2 = imnaux2.Father;
                }
                imnaux = imnaux.Father;
            }

            ImageNode[] imns16 = new ImageNode[imn.Length16];
            ImageNode[] imns8 = new ImageNode[imn.Length8];

            imnaux = imn;
            int next16 = 0;
            int next8 = 0;

            while (imnaux != null)
            {
                if(imnaux.Size == 16)
                {
                    if(!FollowsX.Contains(imnaux))
                    {
                        imns16[next16] = imnaux;
                        next16++;
                    }
                }
                else
                {
                    imns8[next8] = imnaux;
                    next8++;
                }
                imnaux = imnaux.Father;
            }

            int w = (f.DynSize.Width / 8);
            int h = (f.DynSize.Height / 8);
            int[] spaceUsed = new int[w*h];
            int baseX = 0;
            int baseY = 0;
            Color c;
            int bx, by;
            TileMask tm;

            for (int i = 0; i < FollowsX.Length; i += 2)
            {
                if (FollowsX[i] == null) break;
                bx = baseX / 8;
                by = baseY / 8;
                Buffer.BlockCopy(space24x16, 0, spaceUsed, (by / w + (bx % w)) * 4, 12);
                Buffer.BlockCopy(space24x16, 0, spaceUsed, ((by / w) + w + (bx % w)) * 4, 12);
                for (int x = 0; x < 16 && x + baseX < f.DynSize.Width; x++) 
                {
                    for (int y = 0; y < 16 && y + baseY < f.DynSize.Height; y++)
                    {
                        c = bp.GetPixel(FollowsX[i].X + x, FollowsX[i].Y + y);
                        if (c.A == 255) 
                            gfx[baseX + x, baseY + y] = pal[c.ToArgb()];
                    }
                }
                tm = new TileMask(TileSP.SP23, Tiles16[bx, by], 2, false, false)
                {
                    XDisp = FollowsX[i].X * 2,
                    YDisp = FollowsX[i].Y * 2,
                    Priority = Priority,
                    Palette = pid
                };
                f.Tiles.Add(tm);
                baseX += 8;
                for (int x = Math.Abs(FollowsX[i].X- FollowsX[i+1].X); x < 16 && x + baseX < f.DynSize.Width; x++)
                {
                    for (int y = 0; y < 16 && y + baseY < f.DynSize.Height; y++)
                    {
                        c = bp.GetPixel(FollowsX[i+1].X + x, FollowsX[i+1].Y + y);
                        if (c.A == 255) 
                            gfx[baseX + x, baseY + y] = pal[c.ToArgb()];
                    }
                }
                tm = new TileMask(TileSP.SP23, Tiles16[bx + 1, by], 2, false, false)
                {
                    XDisp = (FollowsX[i].X + 8) * 2,
                    YDisp = FollowsX[i + 1].Y * 2,
                    Priority = Priority,
                    Palette = pid
                };
                f.Tiles.Add(tm);
                baseX += 16;
                if(baseX>= f.DynSize.Width)
                {
                    baseX = 0;
                    baseY += 16;
                }
            }
            bool found = false;
            for(int i = 0;i<imns16.Length;i++)
            {
                if (imns16[i] == null) break;
                found = false;
                for (int p = 0; p < w - 1; p++) 
                {
                    for (int q = 0; q < h - 1; q++)
                    {
                        if (spaceUsed[q * w + p] == 0 &&
                            spaceUsed[q * w + p + 1] == 0 &&
                            spaceUsed[q * w + p + 8] == 0 &&
                            spaceUsed[q * w + p + 9] == 0)
                        {
                            found = true;
                            baseX = p * 8;
                            baseY = q * 8;
                            spaceUsed[q * w + p] = 1;
                            spaceUsed[q * w + p + 1] = 1;
                            spaceUsed[q * w + p + 8] = 1;
                            spaceUsed[q * w + p + 9] = 1;
                            for (int x = 0; x < 16 && x + baseX < f.DynSize.Width; x++)
                            {
                                for (int y = 0; y < 16 && y + baseY < f.DynSize.Height; y++)
                                {
                                    c = bp.GetPixel(imns16[i].X + x, imns16[i].Y + y);
                                    if (c.A == 255)
                                        gfx[baseX + x, baseY + y] = pal[c.ToArgb()];
                                }
                            }
                            break;
                        }
                    }
                    if (found) break;
                }
                if (!found) return null;
                else
                {
                    bx = baseX / 8;
                    by = baseY / 8;
                    tm = new TileMask(TileSP.SP23, Tiles16[bx, by], 2, false, false)
                    {
                        XDisp = imns16[i].X * 2,
                        YDisp = imns16[i].Y * 2,
                        Priority = Priority,
                        Palette = pid
                    };
                    f.Tiles.Add(tm);
                }
            }

            for (int i = 0; i < imns8.Length; i++)
            {
                if (imns8[i] == null) break;
                found = false;
                for (int p = 0; p < w; p++) 
                {
                    for (int q = 0; q < h; q++)
                    {
                        if(spaceUsed[q * w + p] == 0)
                        {
                            found = true;
                            baseX = p * 8;
                            baseY = q * 8;
                            spaceUsed[q * w + p] = 1;
                            for (int x = 0; x < 8 && x + baseX < f.DynSize.Width; x++)
                            {
                                for (int y = 0; y < 8 && y + baseY < f.DynSize.Height; y++)
                                {
                                    c = bp.GetPixel(imns8[i].X + x, imns8[i].Y + y);
                                    if (c.A == 255)
                                        gfx[baseX + x, baseY + y] = pal[c.ToArgb()];
                                }
                            }
                            break;
                        }
                    }
                    if (found) break;
                }
                if (!found) return null;
                else
                {
                    bx = baseX / 8;
                    by = baseY / 8;
                    tm = new TileMask(TileSP.SP23, Tiles8[bx, by], 2, false, false)
                    {
                        XDisp = imns8[i].X * 2,
                        YDisp = imns8[i].Y * 2,
                        Priority = Priority,
                        Palette = pid
                    };
                    f.Tiles.Add(tm);
                }
            }

            f.GFX = SnesGraphics.GetGFXFromColorMatrix(gfx);
            f.Tiles.Sort(tilesorter);
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

        public static ImageNode CropFrameInTiles(Bitmap bp)
        {
            int[] ymax = new int[bp.Width];
            int[] ymin = new int[bp.Width];
            getMinMaxH(bp, ymax, ymin);

            LinkedList<ImageNode> imns =
                ImageNode.GetNodes(ymin, ymax);

            C5.IntervalHeap<ImageNode> ih = new C5.IntervalHeap<ImageNode>();

            foreach(ImageNode imn in imns)
            {
                ih.Add(imn);
            }

            List<ImageNode> close = new List<ImageNode>();
            ImageNode imnaux;
            LinkedList<ImageNode> newnodes;

            while (ih.Count > 0)
            {
                imnaux = ih.DeleteMin();
                close.Add(imnaux);

                newnodes = ImageNode.GetNodes(imnaux);
                if (newnodes == null)
                {
                    return imnaux;
                }
                else
                {
                    foreach (ImageNode imn in newnodes)
                    {
                        ih.Add(imn);
                    }
                }
            }
            return null;
        }

        public static void getMinMaxH(Bitmap bp, int[] max, int[] min)
        {
            bool minF;

            for (int i = 0; i < bp.Width; i++)
            {
                minF = false;
                min[i] = -1;
                max[i] = -1;
                for (int j = 0; j < bp.Height; j++)
                {
                    if (bp.GetPixel(i, j).A == 255)
                    {
                        if (!minF)
                        {
                            min[i] = j;
                            minF = true;
                        }
                        max[i] = j;
                    }
                }
            }
        }
    }
}
