using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMWControlibBackend.Graphics.Frames
{
    public enum PlayType { OnlyOnce = 0, Continuous = 1 } 
    public class Animation
    {
        FrameMask first;
        public int Length{ get; private set; }
        public PlayType PlayType = PlayType.Continuous;
        public string Name;

        public Animation()
        {
        }

        public void LoadFrameMasks(FrameMask[] fms)
        {
            first = fms[0];
            Length = fms.Length;

            for (int i = 1; i < fms.Length; i++)
            {
                fms[i - 1].Next = fms[i];
            }
        }

        public FrameMask[] GetFrameMasks()
        {
            if (Length <= 0) return null;
            FrameMask[] list = new FrameMask[Length];
            FrameMask fmaux = first;
            int i = 0;
            while (fmaux != null)
            {
                list[i] = fmaux;
                fmaux = fmaux.Next;
                i++;
            }
            return list;
        }

        public Bitmap[] GetBitmaps()
        {
            if (Length <= 0) return null;
            FrameMask fmaux = first;

            List<Bitmap> list = new List<Bitmap>();
            Bitmap bp;
            int w = 1, h = 1;

            while (fmaux != null)
            {
                bp = fmaux.GetBitmap();
                if(bp!=null)
                {
                    if (bp.Width > w) w = bp.Width;
                    if (bp.Height > h) h = bp.Height;
                }
                list.Add(bp);
                fmaux = fmaux.Next;
            }

            Bitmap[] ret = new Bitmap[list.Count];
            
            int x, y;
            int i = 0;

            foreach (Bitmap b in list)
            {
                bp = new Bitmap(w, h);
                if (b != null)
                {
                    using (System.Drawing.Graphics g = System.Drawing.
                    Graphics.FromImage(bp))
                    {
                        x = w - b.Width;
                        x /= 2;
                        y = h - b.Height;
                        y /= 2;

                        g.DrawImage(b, x, y);
                    }
                }
                ret[i] = bp;
                i++;
            }

            return ret.ToArray();
        }

        public void Exchange(int index)
        {
            if (Length < 2) return;
            if (index >= Length - 1) return;
            FrameMask fmaux = this[index];
            Frame faux = fmaux.Frame;
            int iaux = fmaux.Time;
            fmaux.Frame = fmaux.Next.Frame;
            fmaux.Time = fmaux.Next.Time;
            fmaux.Next.Frame = faux;
            fmaux.Next.Time = iaux;
        }

        public void Add(Frame[] frames, int index)
        {
            if (frames == null || frames.Length <= 0) return;

            FrameMask iter = null, next = null;
            if (Length == 0)
            {
                Length = frames.Length;
                first = new FrameMask()
                {
                    Frame = frames[0],
                    Index = 0
                };
                
                iter = first;
            }
            else
            {
                iter = this[index];
                next = iter.Next;

                Length += frames.Length;

                FrameMask fnew = new FrameMask()
                {
                    Frame = frames[0],
                    Index = iter.Index + 1
                };
                iter.Next = fnew;
                iter = iter.Next;
            }
            FrameMask fmaux;
            for (int i = 1; i < frames.Length; i++)
            {
                fmaux = new FrameMask()
                {
                    Frame = frames[i],
                    Index = iter.Index + 1
                };
                iter.Next = fmaux;
                iter = fmaux;
            }
            iter.Next = next;
            if (next != null) next.Index = iter.Index + 1;
            iter = next;

            while (iter != null)
            {
                if (iter.Next != null) iter.Next.Index = iter.Index + 1;
                iter = iter.Next;
            }
        }

        public void Remove(int index)
        {
            if (Length <= 0) return;
            if (Length == 1)
            {
                first = null;
                Length = 0;
                return;
            }

            if (index == 0)
            {
                first = first.Next;

                FrameMask iterator = first;
                int newInd = 0;
                while (iterator != null)
                {
                    iterator.Index = newInd;
                    iterator = iterator.Next;
                    newInd++;
                }
                Length--;
                return;
            }

            int i = 0;
            FrameMask ret = first;
            while (i < index - 1 && i < Length - 2)
            {
                ret = ret.Next;
                i++;
            }
            ret.Next = ret.Next.Next;

            FrameMask iter = ret.Next;
            if (iter != null)
            {
                iter.Index = ret.Index + 1;
                int newInd = iter.Index;
                while (iter != null)
                {
                    iter = iter.Next;
                    newInd++;
                    if (iter != null) iter.Index = newInd;
                }
            }
            
            Length--;
        }

        public override string ToString()
        {
            return Name;
        }

        public static string GetAnimationChangeRoutine(Animation[] ans)
        {
            StringBuilder sb = new StringBuilder();
            int j = 0;

            for (int i = 0; i < ans.Length; i++)
            {
                if (ans[i].Length > 0)
                {
                    if (j != 0)
                    {
                        sb.Append("\tJMP ChangeAnimationFromStart\n");
                    }
                    sb.Append("ChangeAnimationFromStart_" + ans[i].Name + ":\n");
                    if(j != 0)
                    {
                        sb.Append("\tLDA #$" + (j).ToString("X2") + "\n\t" + "STA !AnimationIndex,x\n");
                    }
                    else
                    {
                        sb.Append("\tSTZ !AnimationIndex,x\n");
                    }
                    j++;
                }
            }
            if (sb.Length > 0 && sb[sb.Length - 1] == ',')
                sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }
        public static string GetAnimationLenghts(Animation[] ans)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < ans.Length; i++)
            {
                if (ans[i].Length > 0)
                {
                    if (i % 16 == 0)
                    {
                        if (sb.Length > 0)
                        {
                            sb.Remove(sb.Length - 1, 1);
                            sb.Append("\n\t");
                        }

                        sb.Append("dw ");
                    }
                    sb.Append("$" + (ans[i].Length).ToString("X4") + ",");
                }
            }
            if (sb.Length > 0 && sb[sb.Length - 1] == ',')
                sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }

        public static string GetAnimationLastTransitions(Animation[] ans)
        {
            StringBuilder sb = new StringBuilder();
            int n = 0;

            for (int i = 0; i < ans.Length; i++)
            {
                if (ans[i].Length > 0)
                {
                    if (i % 16 == 0)
                    {
                        if (sb.Length > 0)
                        {
                            sb.Remove(sb.Length - 1, 1);
                            sb.Append("\n\t");
                        }

                        sb.Append("dw ");
                    }
                    n = ans[i].Length - 1;
                    if (ans[i].PlayType == PlayType.Continuous) n = 0;
                    sb.Append("$" + (n).ToString("X4") + ",");
                }
            }
            if (sb.Length > 0 && sb[sb.Length - 1] == ',')
                sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }

        public static string GetAnimationIndexers(Animation[] ans)
        {
            StringBuilder sb = new StringBuilder();
            int s = 0;

            for (int i = 0; i < ans.Length; i++)
            {
                if (ans[i].Length > 0)
                {
                    if (i % 16 == 0)
                    {
                        if (sb.Length > 0)
                        {
                            sb.Remove(sb.Length - 1, 1);
                            sb.Append("\n\t");
                        }

                        sb.Append("dw ");
                    }
                    sb.Append("$" + (s).ToString("X4") + ",");
                    s += ans[i].Length;
                }
            }
            if (sb.Length > 0 && sb[sb.Length - 1] == ',')
                sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }

        public static string GetAnimationFrames(Animation[] ans)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("\n");
            FrameMask fm;

            for (int i = 0; i < ans.Length; i++)
            {
                if (ans[i].Length > 0)
                {
                    fm = ans[i].first;
                    sb.Append("Animation" + i + "_" + ans[i].Name + "_Frames:\n");
                    for (int j = 0; j < ans[i].Length; j++)
                    {
                        if (j % 16 == 0)
                        {
                            if (sb.Length > 0)
                            {
                                sb.Remove(sb.Length - 1, 1);
                                sb.Append("\n\t");
                            }

                            sb.Append("db ");
                        }
                        sb.Append("$" + (fm.Frame.Index).ToString("X2") + ",");
                        fm = fm.Next;
                    }
                    if (sb.Length > 0 && sb[sb.Length - 1] == ',')
                        sb.Remove(sb.Length - 1, 1);
                    sb.Append("\n");
                }
            }
            if (sb.Length > 0 && (sb[sb.Length - 1] == ',' || sb[sb.Length - 1] == '\n')) 
                sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }

        public static string GetAnimationTimes(Animation[] ans)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("\n");
            FrameMask fm;

            for (int i = 0; i < ans.Length; i++)
            {
                if (ans[i].Length > 0)
                {
                    fm = ans[i].first;
                    sb.Append("Animation" + i + "_" + ans[i].Name + "_Times:\n");
                    for (int j = 0; j < ans[i].Length; j++)
                    {
                        if (j % 16 == 0)
                        {
                            if (sb.Length > 0)
                            {
                                sb.Remove(sb.Length - 1, 1);
                                sb.Append("\n\t");
                            }

                            sb.Append("db ");
                        }
                        sb.Append("$" + (fm.Time).ToString("X2") + ",");
                        fm = fm.Next;
                    }
                    if (sb.Length > 0 && sb[sb.Length - 1] == ',')
                        sb.Remove(sb.Length - 1, 1);
                    sb.Append("\n");
                }
            }
            if (sb.Length > 0 && (sb[sb.Length - 1] == ',' || sb[sb.Length - 1] == '\n'))
                sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }

        public static string GetAnimationFlips(Animation[] ans)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("\n");
            FrameMask fm;
            int n = 0;

            for (int i = 0; i < ans.Length; i++)
            {
                if (ans[i].Length > 0)
                {
                    fm = ans[i].first;
                    sb.Append("Animation" + i + "_" + ans[i].Name + "_Flips:\n");
                    for (int j = 0; j < ans[i].Length; j++)
                    {
                        if (j % 16 == 0)
                        {
                            if (sb.Length > 0)
                            {
                                sb.Remove(sb.Length - 1, 1);
                                sb.Append("\n\t");
                            }

                            sb.Append("db ");
                        }
                        n = 0;
                        if (fm.FlipX) n = 1;
                        if (fm.FlipY) n += 2;
                        sb.Append("$" + (n).ToString("X2") + ",");
                        fm = fm.Next;
                    }
                    if (sb.Length > 0 && sb[sb.Length - 1] == ',')
                        sb.Remove(sb.Length - 1, 1);
                    sb.Append("\n");
                }
            }
            if (sb.Length > 0 && (sb[sb.Length - 1] == ',' || sb[sb.Length - 1] == '\n'))
                sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }

        public FrameMask this[int key]
        {
            get
            {
                if (Length == 0) return null;
                if (key <= 0) return first;

                int i = 0;
                FrameMask ret = first;
                while (i < key && i < Length - 1)
                {
                    ret = ret.Next;
                    i++;
                }
                return ret;
            }
        }
    }
}
