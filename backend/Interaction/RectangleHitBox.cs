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
            g.FillRectangle(b, x, y, w, borderSize);
            g.FillRectangle(b, x, y, borderSize, h);
            g.FillRectangle(b, x, y + h - borderSize, w, borderSize);
            g.FillRectangle(b, x + w - borderSize, y, borderSize, h);
            int borderSize2 = 2 * borderSize;
            int dborderSize = borderSize;
            g.FillRectangle(b, x - dborderSize, y - dborderSize,
                borderSize2, borderSize2);
            g.FillRectangle(b, x - dborderSize, y - borderSize + h,
                borderSize2, borderSize2);
            g.FillRectangle(b, x - borderSize + w, y - dborderSize,
                borderSize2, borderSize2);
            g.FillRectangle(b, x - borderSize + w, y - borderSize + h,
                borderSize2, borderSize2);
        }
    }
}
