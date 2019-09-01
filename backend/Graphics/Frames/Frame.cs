using SMWControlibBackend.Interaction;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SMWControlibBackend.Graphics.Frames
{
    public class DynamicSize
    {
        int value;
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int Lines { get; private set; }
        public static DynamicSize DynamicSprite32x32 = new DynamicSize(0, 64, 16, 2);
        public static DynamicSize DynamicSprite48x48 = new DynamicSize(1, 128, 16, 2);
        public static DynamicSize DynamicSprite64x64 = new DynamicSize(2, 128, 32, 4);
        public static DynamicSize DynamicSprite80x80 = new DynamicSize(3, 128, 48, 6);
        public static DynamicSize DynamicSprite96x96 = new DynamicSize(4, 128, 64, 8);
        public static DynamicSize DynamicSprite112x112 = new DynamicSize(5, 128, 96, 12);

        DynamicSize(int val, int w, int h,int lines)
        {
            value = val;
            Width = w;
            Height = h;
            Lines = lines;
        }

        public static implicit operator int(DynamicSize dz)
        {
            return dz.value;
        }

        public static implicit operator DynamicSize(int i)
        {
            switch(i)
            {
                case 0:
                    return DynamicSprite32x32;
                case 1:
                    return DynamicSprite48x48;
                case 2:
                    return DynamicSprite64x64;
                case 3:
                    return DynamicSprite80x80;
                case 4:
                    return DynamicSprite96x96;
                case 5:
                    return DynamicSprite112x112;
                default:
                    return null;
            }
        }
    }
    public class Frame
    {
        public string Name;
        public List<HitBox> HitBoxes { get; set; }
        public List<InteractionPoint> InteractionPoints { get; private set; }
        public List<TileMask> Tiles { get; private set; }
        public int TilesLenght
        {
            get
            {
                return Tiles.Count;
            }
        }
     
        public int MidX;
        public int MidY;
        public int Index { get; private set; }
        public Frame ShareWith = null;
        public bool MustShare = false;
        public bool Dynamic = false;
        public byte[] GFX = null;
        public DynamicSize DynSize = null;
        private Bitmap bitmap;
        private bool dirty;
        public int MinX { get; private set; }
        public int MinY{ get; private set; }

        public Frame()
        {
            Tiles = new List<TileMask>();
            HitBoxes = new List<HitBox>();
            InteractionPoints = new List<InteractionPoint>();
            IsDirty();
        }

        public void AddTile(TileMask tm)
        {
            Tiles.Add(tm);
            IsDirty();
        }

        public void TilesDirty()
        {
            foreach(TileMask tm in Tiles)
            {
                tm.Dirty = false;
            }
        }

        public void IsDirty()
        {
            dirty = true;
        }

        public void Sort(Comparison<TileMask> func)
        {
            Tiles.Sort(func);
            IsDirty();
        }

        public static Tuple<string, string,string, byte[]> GetDynamicSource(Frame[] f)
        {
            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            Tuple<int[], int[]> taux;
            int lastVal = 0;
            int lns = 0;
            byte[] b = new byte[6144 * f.Length];
            for (int i = 0; i < f.Length; i++)
            {
                lns = f[i].DynSize.Lines;
                sb1.Append(f[i].Name+"_"+"ResourceOffset:\n\tdw ");
                sb2.Append(f[i].Name + "_" + "ResourceSize:\n\tdw ");
                taux = f[i].GetDynamicSource();
                for (int j = 0; j < lns; j++)
                {
                    sb1.Append("$" + (lastVal + taux.Item1[j]).ToString("X4") + ",");
                    sb2.Append("$" + (taux.Item2[j]).ToString("X4") + ",");
                    Buffer.BlockCopy(f[i].GFX, 512 * j, b, lastVal + taux.Item1[j], taux.Item2[j]);
                }
                lastVal += (taux.Item1[lns - 1] + taux.Item2[lns - 1]);
                sb1.Remove(sb1.Length - 1, 1);
                sb2.Remove(sb2.Length - 1, 1);
                sb1.Append("\n");
                sb2.Append("\n");
            }
            byte[] br = new byte[lastVal];
            Buffer.BlockCopy(b, 0, br, 0, lastVal);
            Tuple<string, string, string, byte[]> t =
                new Tuple<string, string, string, byte[]>
                (sb1.ToString(), sb2.ToString(), "$" + lns.ToString("X2"), br);
            return t;
        }

        public Tuple<int[], int[]> GetDynamicSource()
        {
            int lns = DynSize.Lines;
            int[] blocksPerLine = new int[lns];
            int curl = 0;
            int curw;

            foreach(TileMask tm in Tiles)
            {
                curw = -1;
                if(tm.Tile[1] <= '9' && tm.Tile[1] >= '0')
                {
                    curl = tm.Tile[1] - 48;
                }
                else if(tm.Tile[1] >= 'A' && tm.Tile[1] <= 'F')
                {
                    curl = tm.Tile[1] - 55;
                }

                if (tm.Tile[2] <= '9' && tm.Tile[2] >= '0')
                {
                    curw = tm.Tile[2] - 48;
                }
                else if (tm.Tile[2] >= 'A' && tm.Tile[2] <= 'F')
                {
                    curw = tm.Tile[2] - 55;
                }

                if (tm.Size == 16 && curw > 0) curw++;
                if (blocksPerLine[curl] < curw + 1) 
                    blocksPerLine[curl] = curw + 1;
                if (tm.Size == 16 && blocksPerLine[curl + 1] < curw + 1) 
                    blocksPerLine[curl + 1] = curw + 1;
            }

            int[] ResOffset = new int[lns];
            int[] Ressize = new int[lns];
            for (int i = 0; i < blocksPerLine.Length; i++)
            {
                if (i > 0)
                    ResOffset[i] = 
                        ResOffset[i - 1] + blocksPerLine[i - 1] * 32;
                else
                    ResOffset[i] = 0;
                Ressize[i]= blocksPerLine[i] * 32;
            }

            for (int i = lns - 1; i > 0; i--)
            {
                if (blocksPerLine[i - 1] >= 16)
                {
                    Ressize[i - 1] += Ressize[i];
                    Ressize[i] = 0;
                }
            }

            Tuple<int[], int[]> tuple = new Tuple<int[], int[]>(ResOffset, Ressize);
            
            return tuple;
        }

        public Frame Duplicate()
        {
            Frame frame = new Frame()
            {
                Name = ""
            };

            foreach (TileMask tm in Tiles)
            {
                frame.Tiles.Add(tm.Clone());
            }
            return frame;
        }

        public static void GetFramesIndexs(Frame[] frames)
        {
            for (int i = 0; i < frames.Length; i++)
            {
                frames[i].Index = i;
            }
        }

        public static bool HaveHitboxInteraction(Frame[] frames)
        {
            bool have = false;
            for (int i = 0; i < frames.Length; i++)
            {
                if(frames[i].HitBoxes.Count > 0)
                {
                    have = true;
                    break;
                }
            }
            return have;
        }

        public static bool ValidArray(Frame[] frames)
        {
            if (frames == null || frames.Length <= 0) return false;
            for (int i = 0; i < frames.Length; i++)
            {
                if (frames[i].Tiles != null && frames[i].Tiles.Count > 0)
                    return true;
            }
            return false;
        }

        public static bool SameLenght(Frame[] frames)
        {
            int l = frames[0].Tiles.Count;

            for (int i = 1; i < frames.Length; i++)
            {
                if (l != frames[i].Tiles.Count)
                    return false;
            }
            return true;
        }

        public static bool SameXDisp(Frame[] frames, bool FlipX)
        {
            string tm = "";
            int x, dx;
            for (int i = 0; i < frames.Length; i++)
            {
                foreach (TileMask tm1 in frames[i].Tiles)
                {
                    if (tm == "")
                        tm = tm1.XDispString;
                    if (tm != tm1.XDispString)
                        return false;
                    x = tm1.XDisp;
                    dx = frames[i].MidX - ((x / tm1.Zoom) - 128);
                    tm1.XDisp = frames[i].MidX + 128 + dx;
                    if (tm1.Size == 8) tm1.XDisp += 8;
                    tm1.XDisp *= tm1.Zoom;
                    if (tm != tm1.XDispString)
                    {
                        tm1.XDisp = x;
                        return false;
                    }
                    tm1.XDisp = x;
                }
            }
            return true;
        }

        public static string FirstXDisp(Frame[] frames)
        {
            for (int i = 0; i < frames.Length; i++)
            {
                foreach (TileMask tm in frames[i].Tiles)
                {
                    return tm.XDispString;
                }
            }
            return "";
        }

        public static bool SameYDisp(Frame[] frames, bool FlipY)
        {
            string tm = "";
            int y, dy;
            for (int i = 0; i < frames.Length; i++)
            {
                foreach (TileMask tm1 in frames[i].Tiles)
                {
                    if (tm == "")
                        tm = tm1.YDispString;
                    if (tm != tm1.YDispString)
                        return false;
                    y = tm1.YDisp;
                    dy = frames[i].MidY - ((y / tm1.Zoom) - 112);
                    tm1.YDisp = frames[i].MidY + 112 + dy;
                    if (tm1.Size == 8) tm1.YDisp += 8;
                    tm1.YDisp *= tm1.Zoom;
                    if (tm != tm1.YDispString)
                    {
                        tm1.YDisp = y;
                        return false;
                    }
                    tm1.YDisp = y;
                }
            }
            return true;
        }

        public static string FirstYDisp(Frame[] frames)
        {
            for (int i = 0; i < frames.Length; i++)
            {
                foreach (TileMask tm in frames[i].Tiles)
                {
                    return tm.YDispString;
                }
            }
            return "";
        }

        public static bool SameTile(Frame[] frames)
        {
            string tm = "";
            for (int i = 0; i < frames.Length; i++)
            {
                foreach (TileMask tm1 in frames[i].Tiles)
                {
                    if (tm == "")
                        tm = tm1.Tile;
                    if (tm != tm1.Tile)
                        return false;
                }
            }
            return true;
        }

        public static string FirstTile(Frame[] frames)
        {
            for (int i = 0; i < frames.Length; i++)
            {
                foreach (TileMask tm in frames[i].Tiles)
                {
                    return tm.Tile;
                }
            }
            return "";
        }

        public static bool SameProp(Frame[] frames)
        {
            string tm = "";
            for (int i = 0; i < frames.Length; i++)
            {
                foreach (TileMask tm1 in frames[i].Tiles)
                {
                    if (tm == "")
                        tm = tm1.Properties;
                    if (tm != tm1.Properties)
                        return false;
                }
            }
            return true;
        }

        public static string FirstProperty(Frame[] frames)
        {
            for (int i = 0; i < frames.Length; i++)
            {
                foreach (TileMask tm in frames[i].Tiles)
                {
                    return tm.Properties;
                }
            }
            return "";
        }

        public static bool SameSize(Frame[] frames)
        {
            string tm = "";
            for (int i = 0; i < frames.Length; i++)
            {
                foreach (TileMask tm1 in frames[i].Tiles)
                {
                    if (tm == "")
                        tm = tm1.SizeString;
                    if (tm != tm1.SizeString)
                        return false;
                }
            }
            return true;
        }

        public static string FirstSize(Frame[] frames)
        {
            for (int i = 0; i < frames.Length; i++)
            {
                foreach (TileMask tm in frames[i].Tiles)
                {
                    return tm.SizeString;
                }
            }
            return "";
        }

        public static string GetFramesHitboxesIndexersFromFrameList(Frame[] frames, bool FlipX, bool FlipY)
        {
            StringBuilder sb = new StringBuilder();
            int count = 0;

            for (int i = 0; i < frames.Length; i++)
            {
                if (i % 16 == 0)
                {
                    if (i != 0)
                    {
                        sb.Remove(sb.Length - 1, 1);
                        sb.Append("\n\t");
                    }
                    sb.Append("dw ");
                }
                sb.Append("$" + count.ToString("X4") + ",");
                count += frames[i].HitBoxes.Count + 1;
            }

            if (FlipX)
            {
                if (sb.Length > 0)
                    sb.Remove(sb.Length - 1, 1);
                sb.Append("\n\t");
                for (int i = 0; i < frames.Length; i++)
                {
                    if (i % 16 == 0)
                    {
                        if (i != 0)
                        {
                            sb.Remove(sb.Length - 1, 1);
                            sb.Append("\n\t");
                        }
                        sb.Append("dw ");
                    }
                    sb.Append("$" + count.ToString("X4") + ",");
                    count += frames[i].HitBoxes.Count + 1;
                }
            }
            if (FlipY)
            {
                if (sb.Length > 0)
                    sb.Remove(sb.Length - 1, 1);
                sb.Append("\n\t");
                for (int i = 0; i < frames.Length; i++)
                {
                    if (i % 16 == 0)
                    {
                        if (i != 0)
                        {
                            sb.Remove(sb.Length - 1, 1);
                            sb.Append("\n\t");
                        }
                        sb.Append("dw ");
                    }
                    sb.Append("$" + count.ToString("X4") + ",");
                    count += frames[i].HitBoxes.Count + 1;
                }
            }
            if (FlipX && FlipY)
            {
                if (sb.Length > 0)
                    sb.Remove(sb.Length - 1, 1);
                sb.Append("\n\t");
                for (int i = 0; i < frames.Length; i++)
                {
                    if (i % 16 == 0)
                    {
                        if (i != 0)
                        {
                            sb.Remove(sb.Length - 1, 1);
                            sb.Append("\n\t");
                        }
                        sb.Append("dw ");
                    }
                    sb.Append("$" + count.ToString("X4") + ",");
                    count += frames[i].HitBoxes.Count + 1;
                }
            }
            if (sb.Length > 0) 
                sb.Remove(sb.Length - 1, 1);

            return sb.ToString();
        }
        public static string GetFramesHitboxesIdsFromFrameList(Frame[] frames, HitBox[] hitboxes, bool FlipX, bool FlipY)
        {
            int leng = hitboxes.Length;
            if (FlipX) leng /= 2;
            if (FlipY) leng /= 2;

            List<int>[] fhbs = new List<int>[frames.Length];

            for (int i = 0; i < frames.Length; i++)
            {
                fhbs[i] = new List<int>();
                foreach (HitBox hb in frames[i].HitBoxes)
                {
                    for (int j = 0; j < leng; j++)
                    {
                        if (hb.Equals(hitboxes[j]))
                        {
                            fhbs[i].Add(j);
                            break;
                        }
                    }
                }
            }

            StringBuilder sb = new StringBuilder();
            int k;
            int mul = 0;
            string s;
            for (int i = 0; i < fhbs.Length; i++)
            {
                k = 0;
                if(fhbs[i].Count<=0)
                    sb.Append("db ");
                foreach (int j in fhbs[i])
                {
                    if (k % 16 == 0)
                    {
                        if (k != 0)
                        {
                            sb.Remove(sb.Length - 1, 1);
                            sb.Append("\n\t");
                        }
                        sb.Append("db ");
                    }
                    s = (j + (leng * mul)).ToString("X2");
                    s = s.Substring(s.Length - 2);
                    sb.Append("$" + s + ",");
                    k++;
                }
                sb.Append("$FF\n\t");
            }

            if (FlipX)
            {
                sb.Append("\n\t");
                mul++;
                for (int i = 0; i < fhbs.Length; i++)
                {
                    k = 0;
                    if (fhbs[i].Count <= 0)
                        sb.Append("db ");
                    foreach (int j in fhbs[i])
                    {
                        if (k % 16 == 0)
                        {
                            if (k != 0)
                            {
                                sb.Remove(sb.Length - 1, 1);
                                sb.Append("\n\t");
                            }
                            sb.Append("db ");
                        }
                        s = (j + (leng * mul)).ToString("X2");
                        s = s.Substring(s.Length - 2);
                        sb.Append("$" + s + ",");
                        k++;
                    }
                    sb.Append("$FF\n\t");
                }
            }

            if (FlipY)
            {
                sb.Append("\n\t");
                mul++;
                for (int i = 0; i < fhbs.Length; i++)
                {
                    k = 0;
                    if (fhbs[i].Count <= 0)
                        sb.Append("db ");
                    foreach (int j in fhbs[i])
                    {
                        if (k % 16 == 0)
                        {
                            if (k != 0)
                            {
                                sb.Remove(sb.Length - 1, 1);
                                sb.Append("\n\t");
                            }
                            sb.Append("db ");
                        }
                        s = (j + (leng * mul)).ToString("X2");
                        s = s.Substring(s.Length - 2);
                        sb.Append("$" + s + ",");
                        k++;
                    }
                    sb.Append("$FF\n\t");
                }
            }

            if (FlipX && FlipY) 
            {
                sb.Append("\n\t");
                mul++;
                for (int i = 0; i < fhbs.Length; i++)
                {
                    k = 0;
                    if (fhbs[i].Count <= 0)
                        sb.Append("db ");
                    foreach (int j in fhbs[i])
                    {
                        if (k % 16 == 0)
                        {
                            if (k != 0)
                            {
                                sb.Remove(sb.Length - 1, 1);
                                sb.Append("\n\t");
                            }
                            sb.Append("db ");
                        }
                        s = (j + (leng * mul)).ToString("X2");
                        s = s.Substring(s.Length - 2);
                        sb.Append("$" + s + ",");
                        k++;
                    }
                    sb.Append("$FF\n\t");
                }
            }
            return sb.ToString();
        }
        public static HitBox[] GetFramesHitboxesFromFrameList(Frame[] frames, bool FlipX, bool FlipY)
        {
            List<HitBox> hbs = new List<HitBox>();
            bool found;
            int midx = 0;
            int midy = 0;
            for (int i = 0; i < frames.Length; i++)
            {
                midx = frames[i].MidX;
                midy = frames[i].MidY;
                foreach (HitBox hb1 in frames[i].HitBoxes)
                {
                    found = false;
                    foreach (HitBox hb2 in hbs)
                    {
                        if(hb1.Equals(hb2))
                        {
                            found = true;
                            break;
                        }
                    }
                    if(!found)
                    {
                        hbs.Add(hb1);
                    }
                }
            }
            List<HitBox> add = new List<HitBox>();
            if (FlipX)
            {
                foreach (HitBox hb in hbs)
                {
                    add.Add(hb.GetFlippedBox(true, false, midx + 8, midy + 8));
                }
            }
            if (FlipY)
            {
                foreach (HitBox hb in hbs)
                {
                    add.Add(hb.GetFlippedBox(false, true, midx + 8, midy + 8));
                }
            }
            if (FlipX && FlipY)
            {
                foreach (HitBox hb in hbs)
                {
                    add.Add(hb.GetFlippedBox(true, true, midx + 8, midy + 8));
                }
            }

            foreach (HitBox hb in add)
            {
                hbs.Add(hb);
            }
            return hbs.ToArray();
        }
        public static string GetFramesLengthFromFrameList(Frame[] frames, bool FlipX, bool FlipY)
        {
            StringBuilder sb = new StringBuilder();
            int cur = 0;
            StringBuilder sb1;

            sb1 = new StringBuilder();
            for (int i = 0; i < frames.Length; i++)
            {
                cur = frames[i].Tiles.Count;

                if (i % 16 == 0)
                {
                    if (sb1.Length > 0)
                    {
                        sb1.Remove(sb1.Length - 1, 1);
                        sb1.Append("\n\t");
                    }

                    sb1.Append("dw ");
                }
                if (frames[i].Tiles.Count > 0)
                    sb1.Append("$" + (cur - 1).ToString("X4") + ",");
                else sb1.Append("$FFFF,");
            }
            if (sb1.Length > 0 && sb1[sb1.Length - 1] == ',')
                sb1.Remove(sb1.Length - 1, 1);

            sb.Append(sb1 + "\n");

            if (FlipX)
            {
                sb.Append("\t");
                sb1 = new StringBuilder();
                for (int i = 0; i < frames.Length; i++)
                {
                    cur = frames[i].Tiles.Count;

                    if (i % 16 == 0)
                    {
                        if (sb1.Length > 0)
                        {
                            sb1.Remove(sb1.Length - 1, 1);
                            sb1.Append("\n\t");
                        }

                        sb1.Append("dw ");
                    }
                    if (frames[i].Tiles.Count > 0)
                        sb1.Append("$" + (cur - 1).ToString("X4") + ",");
                    else sb1.Append("$FFFF,");
                }
                if (sb1.Length > 0 && sb1[sb1.Length - 1] == ',')
                    sb1.Remove(sb1.Length - 1, 1);

                sb.Append(sb1 + "\n");
            }

            if (FlipY)
            {
                sb.Append("\t");
                sb1 = new StringBuilder();
                for (int i = 0; i < frames.Length; i++)
                {
                    cur = frames[i].Tiles.Count;

                    if (i % 16 == 0)
                    {
                        if (sb1.Length > 0)
                        {
                            sb1.Remove(sb1.Length - 1, 1);
                            sb1.Append("\n\t");
                        }

                        sb1.Append("dw ");
                    }
                    if (frames[i].Tiles.Count > 0)
                        sb1.Append("$" + (cur - 1).ToString("X4") + ",");
                    else sb1.Append("$FFFF,");
                }
                if (sb1.Length > 0 && sb1[sb1.Length - 1] == ',')
                    sb1.Remove(sb1.Length - 1, 1);

                sb.Append(sb1 + "\n");
            }

            if (FlipX && FlipY)
            {
                sb.Append("\t");
                sb1 = new StringBuilder();
                for (int i = 0; i < frames.Length; i++)
                {
                    cur = frames[i].Tiles.Count;

                    if (i % 16 == 0)
                    {
                        if (sb1.Length > 0)
                        {
                            sb1.Remove(sb1.Length - 1, 1);
                            sb1.Append("\n\t");
                        }

                        sb1.Append("dw ");
                    }
                    if (frames[i].Tiles.Count > 0)
                        sb1.Append("$" + (cur - 1).ToString("X4") + ",");
                    else sb1.Append("$FFFF,");
                }
                if (sb1.Length > 0 && sb1[sb1.Length - 1] == ',')
                    sb1.Remove(sb1.Length - 1, 1);

                sb.Append(sb1 + "\n");
            }
            if (sb.Length > 0 && (sb[sb.Length - 1] == ',' || sb[sb.Length - 1] == '\n'))
                sb.Remove(sb.Length - 1, 1);

            return sb.ToString();
        }

        public static string GetFramesFlippersFromFrameList(Frame[] frames, bool FlipX, bool FlipY)
        {
            StringBuilder sb = new StringBuilder();

            int counter = frames.Length;

            counter *= 2;

            sb.Append("dw $0000");

            if(FlipX)
            {
                sb.Append(",$" + counter.ToString("X4"));
            }
            else if(FlipY)
            {
                sb.Append(",$0000");
            }

            if(FlipY)
            {
                sb.Append(",$" + (2 * counter).ToString("X4"));
            }

            if(FlipX && FlipY)
            {
                sb.Append(",$" + (3 * counter).ToString("X4"));
            }
            if (sb.Length > 0 && (sb[sb.Length - 1] == ',' || sb[sb.Length - 1] == '\n'))
                sb.Remove(sb.Length - 1, 1);

            return sb.ToString();
        }

        public static string GetFramesStartsFromFrameList(Frame[] frames, bool FlipX, bool FlipY)
        {
            StringBuilder sb = new StringBuilder();
            int cur = 0;
            StringBuilder sb1;

            sb1 = new StringBuilder();
            for (int i = 0; i < frames.Length; i++)
            {
                cur += frames[i].Tiles.Count;

                if (i % 16 == 0) 
                {
                    if (sb1.Length > 0)
                    {
                        sb1.Remove(sb1.Length - 1, 1);
                        sb1.Append("\n\t");
                    }
                    
                    sb1.Append("dw ");
                }
                if (frames[i].Tiles.Count > 0)
                    sb1.Append("$" + (cur - 1).ToString("X4") + ",");
                else sb1.Append("$FFFF,");
            }
            if (sb1.Length > 0 && sb1[sb1.Length - 1] == ',') 
                sb1.Remove(sb1.Length - 1, 1);

            sb.Append(sb1 + "\n");

            if(FlipX)
            {
                sb.Append("\t");
                sb1 = new StringBuilder();
                for (int i = 0; i < frames.Length; i++)
                {
                    cur += frames[i].Tiles.Count;

                    if (i % 16 == 0)
                    {
                        if (sb1.Length > 0)
                        {
                            sb1.Remove(sb1.Length - 1, 1);
                            sb1.Append("\n\t");
                        }

                        sb1.Append("dw ");
                    }
                    if (frames[i].Tiles.Count > 0)
                        sb1.Append("$" + (cur - 1).ToString("X4") + ",");
                    else sb1.Append("$FFFF,");
                }
                if (sb1.Length > 0 && sb1[sb1.Length - 1] == ',')
                    sb1.Remove(sb1.Length - 1, 1);

                sb.Append(sb1 + "\n");
            }

            if(FlipY)
            {
                sb.Append("\t");
                sb1 = new StringBuilder();
                for (int i = 0; i < frames.Length; i++)
                {
                    cur += frames[i].Tiles.Count;

                    if (i % 16 == 0)
                    {
                        if (sb1.Length > 0)
                        {
                            sb1.Remove(sb1.Length - 1, 1);
                            sb1.Append("\n\t");
                        }

                        sb1.Append("dw ");
                    }
                    if (frames[i].Tiles.Count > 0)
                        sb1.Append("$" + (cur - 1).ToString("X4") + ",");
                    else sb1.Append("$FFFF,");
                }
                if (sb1.Length > 0 && sb1[sb1.Length - 1] == ',')
                    sb1.Remove(sb1.Length - 1, 1);

                sb.Append(sb1 + "\n");
            }

            if(FlipX && FlipY)
            {
                sb.Append("\t");
                sb1 = new StringBuilder();
                for (int i = 0; i < frames.Length; i++)
                {
                    cur += frames[i].Tiles.Count;

                    if (i % 16 == 0)
                    {
                        if (sb1.Length > 0)
                        {
                            sb1.Remove(sb1.Length - 1, 1);
                            sb1.Append("\n\t");
                        }

                        sb1.Append("dw ");
                    }
                    if (frames[i].Tiles.Count > 0)
                        sb1.Append("$" + (cur - 1).ToString("X4") + ",");
                    else sb1.Append("$FFFF,");
                }
                if (sb1.Length > 0 && sb1[sb1.Length - 1] == ',')
                    sb1.Remove(sb1.Length - 1, 1);

                sb.Append(sb1 + "\n");
            }
            if (sb.Length > 0 && (sb[sb.Length - 1] == ',' || sb[sb.Length - 1] == '\n'))
                sb.Remove(sb.Length - 1, 1);

            return sb.ToString();
        }

        public static string GetFramesEndsFromFrameList(Frame[] frames, bool FlipX, bool FlipY)
        {
            StringBuilder sb = new StringBuilder();

            StringBuilder sb1;
            int cur;

            cur = 0;
            sb1 = new StringBuilder();
            for (int i = 0; i < frames.Length; i++)
            {
                if (i % 16 == 0)
                {
                    if (sb1.Length > 0)
                    {
                        sb1.Remove(sb1.Length - 1, 1);
                        sb1.Append("\n\t");
                    }

                    sb1.Append("dw ");
                }
                if (frames[i].Tiles.Count > 0)
                    sb1.Append("$" + cur.ToString("X4") + ",");
                else sb1.Append("$FFFF,");

                cur += frames[i].Tiles.Count;
            }
            if (sb1.Length > 0 && sb1[sb1.Length - 1] == ',')
                sb1.Remove(sb1.Length - 1, 1);

            sb.Append(sb1 + "\n");
            
            if(FlipX)
            {
                sb.Append("\t");
                sb1 = new StringBuilder();
                for (int i = 0; i < frames.Length; i++)
                {
                    if (i % 16 == 0)
                    {
                        if (sb1.Length > 0)
                        {
                            sb1.Remove(sb1.Length - 1, 1);
                            sb1.Append("\n\t");
                        }

                        sb1.Append("dw ");
                    }
                    if (frames[i].Tiles.Count > 0)
                        sb1.Append("$" + cur.ToString("X4") + ",");
                    else sb1.Append("$FFFF,");

                    cur += frames[i].Tiles.Count;
                }
                if (sb1.Length > 0 && sb1[sb1.Length - 1] == ',')
                    sb1.Remove(sb1.Length - 1, 1);

                sb.Append(sb1 + "\n");
            }

            if(FlipY)
            {
                sb.Append("\t");
                sb1 = new StringBuilder();
                for (int i = 0; i < frames.Length; i++)
                {
                    if (i % 16 == 0)
                    {
                        if (sb1.Length > 0)
                        {
                            sb1.Remove(sb1.Length - 1, 1);
                            sb1.Append("\n\t");
                        }

                        sb1.Append("dw ");
                    }
                    if (frames[i].Tiles.Count > 0)
                        sb1.Append("$" + cur.ToString("X4") + ",");
                    else sb1.Append("$FFFF,");

                    cur += frames[i].Tiles.Count;
                }
                if (sb1.Length > 0 && sb1[sb1.Length - 1] == ',')
                    sb1.Remove(sb1.Length - 1, 1);

                sb.Append(sb1 + "\n");
            }

            if(FlipX && FlipY)
            {
                sb.Append("\t");
                sb1 = new StringBuilder();
                for (int i = 0; i < frames.Length; i++)
                {
                    if (i % 16 == 0)
                    {
                        if (sb1.Length > 0)
                        {
                            sb1.Remove(sb1.Length - 1, 1);
                            sb1.Append("\n\t");
                        }

                        sb1.Append("dw ");
                    }
                    if (frames[i].Tiles.Count > 0)
                        sb1.Append("$" + cur.ToString("X4") + ",");
                    else sb1.Append("$FFFF,");

                    cur += frames[i].Tiles.Count;
                }
                if (sb1.Length > 0 && sb1[sb1.Length - 1] == ',')
                    sb1.Remove(sb1.Length - 1, 1);

                sb.Append(sb1 + "\n");
            }
            if (sb.Length > 0 && (sb[sb.Length - 1] == ',' || sb[sb.Length - 1] == '\n'))
                sb.Remove(sb.Length - 1, 1);

            return sb.ToString();
        }

        public static string GetTilesCodesFromFrameList(Frame[] frames, bool FlipX, bool FlipY)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("\n");
            StringBuilder sb1;
            int counter;

            for (int i = 0; i < frames.Length; i++)
            {
                counter = 0;
                sb1 = new StringBuilder();
                sb1.Append("Frame" + i + "_" + frames[i] + "_Tiles:\n");
                foreach (TileMask tm in frames[i].Tiles)
                {
                    if (counter % 16 == 0)
                    {
                        if (sb1.Length > 0)
                        {
                            sb1.Remove(sb1.Length - 1, 1);
                            sb1.Append("\n\t");
                        }

                        sb1.Append("db ");
                    }
                    sb1.Append(tm.Tile + ",");
                    counter++;
                }
                if (sb1.Length > 0 && sb1[sb1.Length - 1] == ',')
                    sb1.Remove(sb1.Length - 1, 1);
                sb.Append(sb1 + "\n");
            }

            if (FlipX)
            {
                for (int i = 0; i < frames.Length; i++)
                {
                    counter = 0;
                    sb1 = new StringBuilder();
                    sb1.Append("Frame" + i + "_" + frames[i] + "_TilesFlipX:\n");
                    foreach (TileMask tm in frames[i].Tiles)
                    {
                        if (counter % 16 == 0)
                        {
                            if (sb1.Length > 0)
                            {
                                sb1.Remove(sb1.Length - 1, 1);
                                sb1.Append("\n\t");
                            }

                            sb1.Append("db ");
                        }
                        sb1.Append(tm.Tile + ",");
                        counter++;
                    }
                    if (sb1.Length > 0 && sb1[sb1.Length - 1] == ',')
                        sb1.Remove(sb1.Length - 1, 1);
                    sb.Append(sb1 + "\n");
                }
            }

            if (FlipY)
            {
                for (int i = 0; i < frames.Length; i++)
                {
                    counter = 0;
                    sb1 = new StringBuilder();
                    sb1.Append("Frame" + i + "_" + frames[i] + "_TilesFlipY:\n");
                    foreach (TileMask tm in frames[i].Tiles)
                    {
                        if (counter % 16 == 0)
                        {
                            if (sb1.Length > 0)
                            {
                                sb1.Remove(sb1.Length - 1, 1);
                                sb1.Append("\n\t");
                            }

                            sb1.Append("db ");
                        }
                        sb1.Append(tm.Tile + ",");
                        counter++;
                    }
                    if (sb1.Length > 0 && sb1[sb1.Length - 1] == ',')
                        sb1.Remove(sb1.Length - 1, 1);
                    sb.Append(sb1 + "\n");
                }
            }

            if (FlipX && FlipY)
            {
                for (int i = 0; i < frames.Length; i++)
                {
                    counter = 0;
                    sb1 = new StringBuilder();
                    sb1.Append("Frame" + i + "_" + frames[i] + "_TilesFlipXY:\n");
                    foreach (TileMask tm in frames[i].Tiles)
                    {
                        if (counter % 16 == 0)
                        {
                            if (sb1.Length > 0)
                            {
                                sb1.Remove(sb1.Length - 1, 1);
                                sb1.Append("\n\t");
                            }

                            sb1.Append("db ");
                        }
                        sb1.Append(tm.Tile + ",");
                        counter++;
                    }
                    if (sb1.Length > 0 && sb1[sb1.Length - 1] == ',')
                        sb1.Remove(sb1.Length - 1, 1);
                    sb.Append(sb1 + "\n");
                }
            }
            if (sb.Length > 0 && (sb[sb.Length - 1] == ',' || sb[sb.Length - 1] == '\n'))
                sb.Remove(sb.Length - 1, 1);

            return sb.ToString();
        }

        public static string GetTilesYDispFromFrameList(Frame[] frames, bool FlipX, bool FlipY)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("\n");
            StringBuilder sb1;
            int counter;
            int y, dy;

            for (int i = 0; i < frames.Length; i++)
            {
                counter = 0;
                sb1 = new StringBuilder();
                sb1.Append("Frame" + i + "_" + frames[i] + "_YDisp:\n");
                foreach (TileMask tm in frames[i].Tiles)
                {
                    if (counter % 16 == 0)
                    {
                        if (sb1.Length > 0)
                        {
                            sb1.Remove(sb1.Length - 1, 1);
                            sb1.Append("\n\t");
                        }

                        sb1.Append("db ");
                    }
                    sb1.Append(tm.YDispString + ",");
                    counter++;
                }
                if (sb1.Length > 0 && sb1[sb1.Length - 1] == ',')
                    sb1.Remove(sb1.Length - 1, 1);
                sb.Append(sb1 + "\n");
            }
            if (FlipX)
            {
                for (int i = 0; i < frames.Length; i++)
                {
                    counter = 0;
                    sb1 = new StringBuilder();
                    sb1.Append("Frame" + i + "_" + frames[i] + "_YDispFlipX:\n");
                    foreach (TileMask tm in frames[i].Tiles)
                    {
                        if (counter % 16 == 0)
                        {
                            if (sb1.Length > 0)
                            {
                                sb1.Remove(sb1.Length - 1, 1);
                                sb1.Append("\n\t");
                            }

                            sb1.Append("db ");
                        }
                        sb1.Append(tm.YDispString + ",");
                        counter++;
                    }
                    if (sb1.Length > 0 && sb1[sb1.Length - 1] == ',')
                        sb1.Remove(sb1.Length - 1, 1);
                    sb.Append(sb1 + "\n");
                }
            }

            if (FlipY)
            {
                for (int i = 0; i < frames.Length; i++)
                {
                    counter = 0;
                    sb1 = new StringBuilder();
                    sb1.Append("Frame" + i + "_" + frames[i] + "_YDispFlipY:\n");
                    foreach (TileMask tm in frames[i].Tiles)
                    {
                        if (counter % 16 == 0)
                        {
                            if (sb1.Length > 0)
                            {
                                sb1.Remove(sb1.Length - 1, 1);
                                sb1.Append("\n\t");
                            }

                            sb1.Append("db ");
                        }
                        y = tm.YDisp;
                        dy = frames[i].MidY - ((y / tm.Zoom) - 112);
                        tm.YDisp = frames[i].MidY + 112 + dy;
                        if (tm.Size == 8) tm.YDisp += 8;
                        tm.YDisp *= tm.Zoom;
                        sb1.Append(tm.YDispString + ",");
                        tm.YDisp = y;
                        counter++;
                    }
                    if (sb1.Length > 0 && sb1[sb1.Length - 1] == ',')
                        sb1.Remove(sb1.Length - 1, 1);
                    sb.Append(sb1 + "\n");
                }
            }

            if (FlipX && FlipY)
            {
                for (int i = 0; i < frames.Length; i++)
                {
                    counter = 0;
                    sb1 = new StringBuilder();
                    sb1.Append("Frame" + i + "_" + frames[i] + "_YDispFlipXY:\n");
                    foreach (TileMask tm in frames[i].Tiles)
                    {
                        if (counter % 16 == 0)
                        {
                            if (sb1.Length > 0)
                            {
                                sb1.Remove(sb1.Length - 1, 1);
                                sb1.Append("\n\t");
                            }

                            sb1.Append("db ");
                        }
                        y = tm.YDisp;
                        dy = frames[i].MidY - ((y / tm.Zoom) - 112);
                        tm.YDisp = frames[i].MidY + 112 + dy;
                        if (tm.Size == 8) tm.YDisp += 8;
                        tm.YDisp *= tm.Zoom;
                        sb1.Append(tm.YDispString + ",");
                        tm.YDisp = y;
                        counter++;
                    }
                    if (sb1.Length > 0 && sb1[sb1.Length - 1] == ',')
                        sb1.Remove(sb1.Length - 1, 1);
                    sb.Append(sb1 + "\n");
                }
            }
            if (sb.Length > 0 && (sb[sb.Length - 1] == ',' || sb[sb.Length - 1] == '\n'))
                sb.Remove(sb.Length - 1, 1);

            return sb.ToString();
        }

        public static string GetTilesXDispFromFrameList(Frame[] frames, bool FlipX, bool FlipY)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("\n");
            StringBuilder sb1;
            int counter;
            int x, dx;

            for (int i = 0; i < frames.Length; i++)
            {
                counter = 0;
                sb1 = new StringBuilder();
                sb1.Append("Frame" + i + "_" + frames[i] + "_XDisp:\n");
                foreach (TileMask tm in frames[i].Tiles)
                {
                    if (counter % 16 == 0)
                    {
                        if (sb1.Length > 0)
                        {
                            sb1.Remove(sb1.Length - 1, 1);
                            sb1.Append("\n\t");
                        }

                        sb1.Append("db ");
                    }
                    sb1.Append(tm.XDispString + ",");
                    counter++;
                }
                if (sb1.Length > 0 && sb1[sb1.Length - 1] == ',')
                    sb1.Remove(sb1.Length - 1, 1);
                sb.Append(sb1 + "\n");
            }

            if (FlipX)
            {
                for (int i = 0; i < frames.Length; i++)
                {
                    counter = 0;
                    sb1 = new StringBuilder();
                    sb1.Append("Frame" + i + "_" + frames[i] + "_XDispFlipX:\n");
                    foreach (TileMask tm in frames[i].Tiles)
                    {
                        if (counter % 16 == 0)
                        {
                            if (sb1.Length > 0)
                            {
                                sb1.Remove(sb1.Length - 1, 1);
                                sb1.Append("\n\t");
                            }

                            sb1.Append("db ");
                        }
                        x = tm.XDisp;
                        dx = frames[i].MidX - ((x / tm.Zoom) - 128);
                        tm.XDisp = frames[i].MidX + 128 + dx;
                        if (tm.Size == 8) tm.XDisp += 8;
                        tm.XDisp *= tm.Zoom;
                        sb1.Append(tm.XDispString + ",");
                        tm.XDisp = x;
                        counter++;
                    }
                    if (sb1.Length > 0 && sb1[sb1.Length - 1] == ',')
                        sb1.Remove(sb1.Length - 1, 1);
                    sb.Append(sb1 + "\n");
                }
            }

            if (FlipY)
            {
                for (int i = 0; i < frames.Length; i++)
                {
                    counter = 0;
                    sb1 = new StringBuilder();
                    sb1.Append("Frame" + i + "_" + frames[i] + "_XDispFlipY:\n");
                    foreach (TileMask tm in frames[i].Tiles)
                    {
                        if (counter % 16 == 0)
                        {
                            if (sb1.Length > 0)
                            {
                                sb1.Remove(sb1.Length - 1, 1);
                                sb1.Append("\n\t");
                            }

                            sb1.Append("db ");
                        }
                        sb1.Append(tm.XDispString + ",");
                        counter++;
                    }
                    if (sb1.Length > 0 && sb1[sb1.Length - 1] == ',')
                        sb1.Remove(sb1.Length - 1, 1);
                    sb.Append(sb1 + "\n");
                }
            }

            if (FlipX && FlipY)
            {
                for (int i = 0; i < frames.Length; i++)
                {
                    counter = 0;
                    sb1 = new StringBuilder();
                    sb1.Append("Frame" + i + "_" + frames[i] + "_XDispFlipXY:\n");
                    foreach (TileMask tm in frames[i].Tiles)
                    {
                        if (counter % 16 == 0)
                        {
                            if (sb1.Length > 0)
                            {
                                sb1.Remove(sb1.Length - 1, 1);
                                sb1.Append("\n\t");
                            }

                            sb1.Append("db ");
                        }
                        x = tm.XDisp;
                        dx = frames[i].MidX - ((x / tm.Zoom) - 128);
                        tm.XDisp = frames[i].MidX + 128 + dx;
                        if (tm.Size == 8) tm.XDisp += 8;
                        tm.XDisp *= tm.Zoom;
                        sb1.Append(tm.XDispString + ",");
                        tm.XDisp = x;
                        counter++;
                    }
                    if (sb1.Length > 0 && sb1[sb1.Length - 1] == ',')
                        sb1.Remove(sb1.Length - 1, 1);
                    sb.Append(sb1 + "\n");
                }
            }
            if (sb.Length > 0 && (sb[sb.Length - 1] == ',' || sb[sb.Length - 1] == '\n'))
                sb.Remove(sb.Length - 1, 1);

            return sb.ToString();
        }

        public static string GetTilesPropertiesFromFrameList(Frame[] frames, bool FlipX, bool FlipY, bool SameProp)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("\n");
            StringBuilder sb1;
            int counter;
            bool fx, fy;

            for (int i = 0; i < frames.Length; i++)
            {
                counter = 0;
                sb1 = new StringBuilder();
                sb1.Append("Frame" + i + "_" + frames[i] + "_Properties:\n");
                foreach (TileMask tm in frames[i].Tiles)
                {
                    if (counter % 16 == 0)
                    {
                        if (sb1.Length > 0)
                        {
                            sb1.Remove(sb1.Length - 1, 1);
                            sb1.Append("\n\t");
                        }

                        sb1.Append("db ");
                    }
                    sb1.Append(tm.Properties + ",");
                    counter++;
                }
                if (sb1.Length > 0 && sb1[sb1.Length - 1] == ',')
                    sb1.Remove(sb1.Length - 1, 1);
                sb.Append(sb1 + "\n");
            }

            if (FlipX)
            {
                for (int i = 0; i < frames.Length; i++)
                {
                    counter = 0;
                    sb1 = new StringBuilder();
                    sb1.Append("Frame" + i + "_" + frames[i] + "_PropertiesFlipX:\n");
                    foreach (TileMask tm in frames[i].Tiles)
                    {
                        if (counter % 16 == 0)
                        {
                            if (sb1.Length > 0)
                            {
                                sb1.Remove(sb1.Length - 1, 1);
                                sb1.Append("\n\t");
                            }

                            sb1.Append("db ");
                        }
                        fx = tm.FlipX;
                        if(!SameProp) tm.FlipX = !tm.FlipX;
                        sb1.Append(tm.Properties + ",");
                        tm.FlipX = fx;
                        counter++;
                    }
                    if (sb1.Length > 0 && sb1[sb1.Length - 1] == ',')
                        sb1.Remove(sb1.Length - 1, 1);
                    sb.Append(sb1 + "\n");
                }
            }

            if (FlipY)
            {
                for (int i = 0; i < frames.Length; i++)
                {
                    counter = 0;
                    sb1 = new StringBuilder();
                    sb1.Append("Frame" + i + "_" + frames[i] + "_PropertiesFlipY:\n");
                    foreach (TileMask tm in frames[i].Tiles)
                    {
                        if (counter % 16 == 0)
                        {
                            if (sb1.Length > 0)
                            {
                                sb1.Remove(sb1.Length - 1, 1);
                                sb1.Append("\n\t");
                            }

                            sb1.Append("db ");
                        }
                        fy = tm.FlipY;
                        if (!SameProp) tm.FlipY = !tm.FlipY;
                        sb1.Append(tm.Properties + ",");
                        tm.FlipY = fy;
                        counter++;
                    }
                    if (sb1.Length > 0 && sb1[sb1.Length - 1] == ',')
                        sb1.Remove(sb1.Length - 1, 1);
                    sb.Append(sb1 + "\n");
                }
            }

            if (FlipX && FlipY)
            {
                for (int i = 0; i < frames.Length; i++)
                {
                    counter = 0;
                    sb1 = new StringBuilder();
                    sb1.Append("Frame" + i + "_" + frames[i] + "_PropertiesFlipXY:\n");
                    foreach (TileMask tm in frames[i].Tiles)
                    {
                        if (counter % 16 == 0)
                        {
                            if (sb1.Length > 0)
                            {
                                sb1.Remove(sb1.Length - 1, 1);
                                sb1.Append("\n\t");
                            }

                            sb1.Append("db ");
                        }
                        fx = tm.FlipX;
                        tm.FlipX = !tm.FlipX;
                        fy = tm.FlipY;
                        if (!SameProp) tm.FlipY = !tm.FlipY;
                        sb1.Append(tm.Properties + ",");
                        tm.FlipX = fx;
                        tm.FlipY = fy;
                        counter++;
                    }
                    if (sb1.Length > 0 && sb1[sb1.Length - 1] == ',')
                        sb1.Remove(sb1.Length - 1, 1);
                    sb.Append(sb1 + "\n");
                }
            }
            if (sb.Length > 0 && (sb[sb.Length - 1] == ',' || sb[sb.Length - 1] == '\n'))
                sb.Remove(sb.Length - 1, 1);

            return sb.ToString();
        }

        public static string GetTilesSizesFromFrameList(Frame[] frames, bool FlipX, bool FlipY)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("\n");
            StringBuilder sb1;
            int counter;

            for (int i = 0; i < frames.Length; i++)
            {
                counter = 0;
                sb1 = new StringBuilder();
                sb1.Append("Frame" + i + "_" + frames[i] + "_Sizes:\n");
                foreach (TileMask tm in frames[i].Tiles)
                {
                    if (counter % 16 == 0)
                    {
                        if (sb1.Length > 0)
                        {
                            sb1.Remove(sb1.Length - 1, 1);
                            sb1.Append("\n\t");
                        }

                        sb1.Append("db ");
                    }
                    sb1.Append(tm.SizeString + ",");
                    counter++;
                }
                if (sb1.Length > 0 && sb1[sb1.Length - 1] == ',')
                    sb1.Remove(sb1.Length - 1, 1);
                sb.Append(sb1 + "\n");
            }

            if (FlipX)
            {
                for (int i = 0; i < frames.Length; i++)
                {
                    counter = 0;
                    sb1 = new StringBuilder();
                    sb1.Append("Frame" + i + "_" + frames[i] + "_SizesFlipX:\n");
                    foreach (TileMask tm in frames[i].Tiles)
                    {
                        if (counter % 16 == 0)
                        {
                            if (sb1.Length > 0)
                            {
                                sb1.Remove(sb1.Length - 1, 1);
                                sb1.Append("\n\t");
                            }

                            sb1.Append("db ");
                        }
                        sb1.Append(tm.SizeString + ",");
                        counter++;
                    }
                    if (sb1.Length > 0 && sb1[sb1.Length - 1] == ',')
                        sb1.Remove(sb1.Length - 1, 1);
                    sb.Append(sb1 + "\n");
                }
            }

            if (FlipY)
            {
                for (int i = 0; i < frames.Length; i++)
                {
                    counter = 0;
                    sb1 = new StringBuilder();
                    sb1.Append("Frame" + i + "_" + frames[i] + "_SizesFlipY:\n");
                    foreach (TileMask tm in frames[i].Tiles)
                    {
                        if (counter % 16 == 0)
                        {
                            if (sb1.Length > 0)
                            {
                                sb1.Remove(sb1.Length - 1, 1);
                                sb1.Append("\n\t");
                            }

                            sb1.Append("db ");
                        }
                        sb1.Append(tm.SizeString + ",");
                        counter++;
                    }
                    if (sb1.Length > 0 && sb1[sb1.Length - 1] == ',')
                        sb1.Remove(sb1.Length - 1, 1);
                    sb.Append(sb1 + "\n");
                }
            }

            if (FlipX && FlipY)
            {
                for (int i = 0; i < frames.Length; i++)
                {
                    counter = 0;
                    sb1 = new StringBuilder();
                    sb1.Append("Frame" + i + "_" + frames[i] + "_SizesFlipXY:\n");
                    foreach (TileMask tm in frames[i].Tiles)
                    {
                        if (counter % 16 == 0)
                        {
                            if (sb1.Length > 0)
                            {
                                sb1.Remove(sb1.Length - 1, 1);
                                sb1.Append("\n\t");
                            }

                            sb1.Append("db ");
                        }
                        sb1.Append(tm.SizeString + ",");
                        counter++;
                    }
                    if (sb1.Length > 0 && sb1[sb1.Length - 1] == ',')
                        sb1.Remove(sb1.Length - 1, 1);
                    sb.Append(sb1 + "\n");
                }
            }
            if (sb.Length > 0 && (sb[sb.Length - 1] == ',' || sb[sb.Length - 1] == '\n'))
                sb.Remove(sb.Length - 1, 1);

            return sb.ToString();
        }
        public Bitmap GetBitmap()
        {
            if (!dirty && bitmap != null) return bitmap;
            dirty = false;
            MinX = int.MaxValue;
            MinY = int.MaxValue;
            int maxX = int.MinValue, maxY = int.MinValue;
            int mx, my;
            int oz;
            foreach(TileMask tm in Tiles)
            {
                oz = tm.Zoom;
                mx = tm.XDisp / oz;
                my = tm.YDisp / oz;
                if (mx < MinX) MinX = mx;
                if (my < MinY) MinY = my;

                mx += tm.Size;
                my += tm.Size;

                if (mx > maxX) maxX = mx;
                if (my > maxY) maxY = my;
            }

            bitmap = new Bitmap(maxX - MinX, maxY - MinY);
            using (System.Drawing.Graphics g =
                System.Drawing.Graphics.FromImage(bitmap))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                foreach(TileMask tm in Tiles)
                {
                    g.DrawImage(tm.GetBitmap(), tm.XDisp / tm.Zoom - MinX, tm.YDisp / tm.Zoom - MinY, tm.Size, tm.Size);
                }
            }
            return bitmap;
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
