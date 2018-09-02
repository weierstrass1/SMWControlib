using SMWControlibBackend.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMWControlibBackend.Interaction
{
    public class RectangleHitBox : HitBox
    {
        public int Width, Height;

        public RectangleHitBox()
        {
            Type = HitBoxType.Rectangle;
        }

        public override void Draw(System.Drawing.Graphics g,
            int centerX, int centerY, Zoom Zoom)
        {
            int x = centerX + XOffset;
            int y = centerY + YOffset;
            x *= Zoom;
            y *= Zoom;
            int w = Width * Zoom;
            int h = Height * Zoom;
            g.FillRectangle(new SolidBrush(FrontColor), x, y, w, h);
            g.DrawRectangle(new Pen(BorderColor), x, y, w, h);
        }

        public override void Draw(System.Drawing.Graphics g,
            int centerX, int centerY, Zoom Zoom, int borderSize)
        {
            int x = centerX + XOffset;
            int y = centerY + YOffset;
            x *= Zoom;
            y *= Zoom;
            int w = Width * Zoom;
            int h = Height * Zoom;
            g.FillRectangle(new SolidBrush(FrontColor), x, y, w, h);
            Brush b = new SolidBrush(BorderColor);
            g.FillRectangle(b, 0, 0, w, borderSize);
            g.FillRectangle(b, 0, 0, borderSize, h);
            g.FillRectangle(b, 0, h - borderSize, w, borderSize);
            g.FillRectangle(b, w - borderSize, 0, borderSize, h);
        }
    }
}
