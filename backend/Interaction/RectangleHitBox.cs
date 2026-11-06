using SMWControlibBackend.Graphics;
using System.Drawing;

namespace SMWControlibBackend.Interaction
{
    public class RectangleHitBox : HitBox
    {
        public int Width, Height;

        public RectangleHitBox()
        {
            Type = HitBoxType.Rectangle;
            Size = 6;
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(RectangleHitBox)) return false;

            RectangleHitBox rhb = (RectangleHitBox)obj;
            if (XOffset != rhb.XOffset) return false;
            if (YOffset != rhb.YOffset) return false;
            if (ActionName != rhb.ActionName) return false;
            if (Width != rhb.Width) return false;
            if (Height != rhb.Height) return false;

            return true;
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

        public override HitBox GetFlippedBox(bool FlipX, bool FlipY, int midX, int midY)
        {
            int deltaX = XOffset + Width - midX;
            int deltaY = YOffset + Height - midY;

            RectangleHitBox rhb = new RectangleHitBox();
            if(FlipX)
            {
                rhb.XOffset = midX - deltaX;
            }
            else
            {
                rhb.XOffset = XOffset;
            }
            if(FlipY)
            {
                rhb.YOffset = midY - deltaY;
            }
            else
            {
                rhb.YOffset = YOffset;
            }
            rhb.Width = Width;
            rhb.Height = Height;
            rhb.ActionName = ActionName;
            return rhb;
        }

        public override string[] GetHitBoxString(string[] actionNames)
        {
            int i;
            for (i = 0; i < actionNames.Length; i++)
            {
                if (actionNames[i] == ActionName)
                {
                    break;
                }
            }
            i *= 2;
            string xOff = XOffset.ToString("X4");
            string yOff = YOffset.ToString("X4");
            string w = Width.ToString("X4");
            string h = Height.ToString("X4");
            string act = i.ToString("X4");
            return new string[]
            {
                "$0001",
                $"${xOff.Substring(xOff.Length-4,4)}",
                $"${yOff.Substring(yOff.Length-4,4)}",
                $"${w.Substring(w.Length-4,4)}",
                $"${h.Substring(h.Length-4,4)}",
                $"${act.Substring(act.Length-4,4)}",
            };
            
        }

        public override HitBox Clone()
        {
            RectangleHitBox rhb = new RectangleHitBox
            {
                Width = Width,
                Height = Height,
                Name = Name,
                XOffset = XOffset,
                YOffset = YOffset,
                BorderColor = BorderColor,
                FrontColor = FrontColor,
                Type = Type,
                ActionName = ActionName,
                Size = Size
            };
            return rhb;
        }

        public override int GetHashCode()
        {
            var hashCode = 1290862079;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + Width.GetHashCode();
            hashCode = hashCode * -1521134295 + Height.GetHashCode();
            return hashCode;
        }
    }
}
