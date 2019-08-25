using System;
using System.Drawing;
using System.Linq;

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
            if (Frame.Tiles == null || Frame.TilesLenght <= 0) return null;
            Bitmap bp = Frame.GetBitmap();
            Bitmap bp2 = null;
            int leftDist = 0;
            int rightDist = 0;
            int topDist = 0;
            int bottomDist = 0;
            int fMidX = (Frame.MidX);
            int fMidY = (Frame.MidY);
            int zoom = 1;
            int fmidXZ = fMidX + 8;
            int fmidYZ = fMidY + 8;
            int xd = 0, yd = 0;
            int minX = Frame.MinX;
            int minY = Frame.MinY;
            int maxX = minX + bp.Width;
            int maxY = minY + bp.Height;

            minX -= 128;
            maxX -= 128;
            minY -= 112;
            maxY -= 112;

            leftDist = minX - fmidXZ;
            rightDist = maxX - fmidXZ;
            topDist = minY - fmidYZ;
            bottomDist = maxY - fmidYZ;

            int w = Math.Max(Math.Abs(leftDist), Math.Abs(rightDist)) * 2;
            int h = Math.Max(Math.Abs(bottomDist), Math.Abs(topDist)) * 2;

            if (w < 1) w = 1;
            if (h < 1) h = 1;

            bp2 = new Bitmap(w, h);

            int dx = (fmidXZ - minX - 8);
            xd = w / 2;
            xd -= 8;
            xd -= dx;
            int dy = (fmidYZ - minY - 8);
            yd = h / 2;
            yd -= 8;
            yd -= dy;

            using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bp2))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

                g.DrawImage(bp, xd, yd,
                    bp.Width / zoom, bp.Height / zoom);
            }

            if (FlipX && FlipY)
                bp2.RotateFlip(RotateFlipType.RotateNoneFlipXY);
            else if (FlipX)
                bp2.RotateFlip(RotateFlipType.RotateNoneFlipX);
            else if (FlipY)
                bp2.RotateFlip(RotateFlipType.RotateNoneFlipY);

            return bp2;
        }
    }
}
