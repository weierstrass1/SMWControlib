using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMWControlibBackend.Graphics.Frames
{
    public class FrameMask
    {
        public Frame Frame;
        public int Time = 4;
        public int Index { get; internal set; }
        public FrameMask Next;
        public bool FlipX, FlipY;

        public Bitmap GetBitmap()
        {
            Bitmap bp = Frame.GetBitmap();
            Bitmap bp2 = null;
            int leftDist = int.MinValue;
            int rightDist = int.MinValue;
            int topDist = int.MinValue;
            int bottomDist = int.MinValue;
            int fMidX = (Frame.MidX);
            int fMidY = (Frame.MidY);
            foreach (TileMask tm in Frame.Tiles)
            {
                if (fMidX * tm.Zoom - tm.XDisp > leftDist)
                {
                    leftDist = fMidX * tm.Zoom - tm.XDisp;
                }
                if (tm.XDisp + tm.Size * tm.Zoom - fMidX * tm.Zoom > rightDist) 
                {
                    rightDist = tm.XDisp + tm.Size * tm.Zoom - fMidX * tm.Zoom;
                }
                if (fMidY * tm.Zoom - tm.YDisp > topDist)
                {
                    topDist = fMidY * tm.Zoom - tm.YDisp;
                }
                if (tm.YDisp + tm.Size * tm.Zoom - fMidY * tm.Zoom > bottomDist)
                {
                    bottomDist = tm.YDisp + tm.Size * tm.Zoom - fMidY * tm.Zoom;
                }
            }

            if (FlipX && FlipY)
            {
                bp.RotateFlip(RotateFlipType.RotateNoneFlipXY);
            }
            else if (FlipX)
            {
                bp.RotateFlip(RotateFlipType.RotateNoneFlipX);
            }
            else if(FlipY)
            {
                bp.RotateFlip(RotateFlipType.RotateNoneFlipY);
            }

            int w = Math.Abs(Math.Max(leftDist, rightDist) * 2);
            int h = Math.Abs(Math.Max(bottomDist, topDist) * 2);
            bp2 = new Bitmap(w, h);
            using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bp2))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                if (FlipX && FlipY)
                    g.DrawImage(bp, 0, 0);
                else if (FlipX)
                    g.DrawImage(bp, 0, fMidY - topDist);
                else if (FlipY)
                    g.DrawImage(bp, fMidX - leftDist, 0);
                else
                    g.DrawImage(bp, fMidX - leftDist, fMidY - topDist);
            }

            return bp2;
        }
    }
}
