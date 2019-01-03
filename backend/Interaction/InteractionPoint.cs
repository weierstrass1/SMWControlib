using System;
using System.Drawing;
using SMWControlibBackend.Graphics;

namespace SMWControlibBackend.Interaction
{
    public struct InteractionPointType
    {
        public static readonly InteractionPointType AboveDetector 
            = new InteractionPointType(0);
        public static readonly InteractionPointType BelowDetector
            = new InteractionPointType(1);
        public static readonly InteractionPointType LeftSideDetector
            = new InteractionPointType(2);
        public static readonly InteractionPointType RightSideDetector
            = new InteractionPointType(3);
        public static readonly InteractionPointType SlopeDetector
            = new InteractionPointType(4);

        public int Value { get; private set; }

        private InteractionPointType(int value)
        {
            Value = value;
        }

        public static implicit operator InteractionPointType(int d)
        {
            switch(d)
            {
                case 0:
                    return AboveDetector;
                case 1:
                    return BelowDetector;
                case 2:
                    return LeftSideDetector;
                case 3:
                    return RightSideDetector;
                default:
                    return SlopeDetector;
            }
        }

        public static implicit operator int(InteractionPointType d)
        {
            return d.Value;
        }
    }

    public class InteractionPoint : HitBox
    {
        public InteractionPointType InteractionType;

        public InteractionPoint()
        {
            InteractionType = InteractionPointType.BelowDetector;
            Type = HitBoxType.InteractionPoint;
        }

        public override void Draw(System.Drawing.Graphics g, int centerX, int centerY, Zoom Zoom)
        {
            int x = centerX + XOffset;
            int y = centerY + YOffset;
            x *= Zoom;
            y *= Zoom;
            g.DrawRectangle(new Pen(BorderColor), x, y, Zoom, Zoom);
        }

        public override void Draw(System.Drawing.Graphics g, int centerX, int centerY, Zoom Zoom, int borderSize)
        {
            int x = centerX + XOffset;
            int y = centerY + YOffset;
            x *= Zoom;
            y *= Zoom;
            int bx = x + (Zoom / 2) - (borderSize / 2) - (borderSize % 2);
            int by = y + (Zoom / 2) - (borderSize / 2) - (borderSize % 2);
            g.FillEllipse(new SolidBrush(FrontColor), bx, by, borderSize, borderSize);
            g.DrawRectangle(new Pen(BorderColor), x, y, Zoom, Zoom);
        }

        public override HitBox GetFlippedBox(bool FlipX, bool FlipY, int midX, int midY)
        {
            throw new NotImplementedException();
        }

        public override string GetHitBoxString(string[] actionNames)
        {
            throw new NotImplementedException();
        }

        public override HitBox Clone()
        {
            throw new NotImplementedException();
        }
    }
}
