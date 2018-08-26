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

        public Animation()
        {
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
                bp = first.GetBitmap();
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
