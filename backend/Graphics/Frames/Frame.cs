﻿using System.Collections.Generic;
using System.Drawing;

namespace SMWControlibBackend.Graphics.Frames
{
    public class Frame
    {
        public string Name;
        public List<TileMask> Tiles { get; private set; }
        public int MidX;
        public int MidY;

        public Frame()
        {
            Tiles = new List<TileMask>();
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
