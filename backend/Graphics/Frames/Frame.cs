using SMWControlibBackend.Interaction;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SMWControlibBackend.Graphics.Frames
{
    public class Frame
    {
        public string Name;
        public List<HitBox> HitBoxes { get; set; }
        public List<InteractionPoint> InteractionPoints { get; private set; }
        public List<TileMask> Tiles { get; private set; }
        public int MidX;
        public int MidY;
        public int Index { get; private set; }
        public Frame ShareWith = null;

        public Frame()
        {
            Tiles = new List<TileMask>();
            HitBoxes = new List<HitBox>();
            InteractionPoints = new List<InteractionPoint>();
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

        public Bitmap GetBitmap(int BitmapWidth, int BitmapHeight, Zoom zoom)
        {
            Bitmap bp = new Bitmap(BitmapWidth * zoom, BitmapHeight * zoom);
            using (System.Drawing.Graphics g =
                System.Drawing.Graphics.FromImage(bp))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                int aux = 0;
                foreach (TileMask tm in Tiles)
                {
                    aux = tm.Zoom;
                    tm.Zoom = zoom;

                    g.DrawImage(tm.GetBitmap(),
                        (tm.XDisp / aux) * zoom, (tm.YDisp / aux) * zoom);
                    tm.Zoom = aux;
                }
            }
            return bp;
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

        public static string GetTilesPropertiesFromFrameList(Frame[] frames, bool FlipX, bool FlipY)
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
                        tm.FlipX = !tm.FlipX;
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
                        tm.FlipY = !tm.FlipY;
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
                        tm.FlipY = !tm.FlipY;
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
            int minX = int.MaxValue, minY = int.MaxValue;
            int maxX = int.MinValue, maxY = int.MinValue;
            int mx, my;
            foreach(TileMask tm in Tiles)
            {
                if (tm.XDisp < minX) minX = tm.XDisp;
                if (tm.YDisp < minY) minY = tm.YDisp;

                mx = tm.XDisp + tm.Size * tm.Zoom;
                if (mx > maxX) maxX = mx;
                my = tm.YDisp + tm.Size * tm.Zoom;
                if (my > maxY) maxY = my;
            }

            Bitmap bp = new Bitmap(maxX - minX, maxY - minY);
            using (System.Drawing.Graphics g =
                System.Drawing.Graphics.FromImage(bp))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                foreach(TileMask tm in Tiles)
                {
                    g.DrawImage(tm.GetBitmap(), tm.XDisp - minX, tm.YDisp - minY);
                }
            }
            return bp;
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
