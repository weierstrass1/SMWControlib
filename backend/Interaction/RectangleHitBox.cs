using SMWControlibBackend.Graphics;
using System.Drawing;
using System.Text;

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
            return rhb;
        }

        public override string GetHitBoxString(string[] actionNames)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("db ");
            string s = (XOffset).ToString("X2");
            s = s.Substring(s.Length - 2);
            sb.Append("$01,$" + s + ",$");
            s = (YOffset).ToString("X2");
            s = s.Substring(s.Length - 2);
            sb.Append(s + ",$");
            s = (Width).ToString("X2");
            s = s.Substring(s.Length - 2);
            sb.Append(s + ",$");
            s = (Height).ToString("X2");
            s = s.Substring(s.Length - 2);
            int i = 0;
            for (i = 0; i < actionNames.Length; i++)
            {
                if (actionNames[i] == ActionName)
                {
                    break;
                }
            }
            sb.Append(s + ",$" + (i).ToString("X2") + "\n");
            return sb.ToString();
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
    }
}
