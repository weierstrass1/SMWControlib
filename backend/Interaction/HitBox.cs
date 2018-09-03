using SMWControlibBackend.Graphics;
using SMWControlibBackend.Logic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMWControlibBackend.Interaction
{
    public struct HitBoxType
    {
        public static readonly HitBoxType Rectangle = new HitBoxType(0);
        public static readonly HitBoxType InteractionPoint = new HitBoxType(1);

        public int Value { get; private set; }

        private HitBoxType(int value)
        {
            Value = value;
        }

        public static implicit operator int(HitBoxType d)
        {
            return d.Value;
        }
    }

    public abstract class HitBox
    {
        public string Name;
        public int XOffset, YOffset;
        public Color BorderColor = Color.FromArgb(255, 0, 0),
            FrontColor = Color.FromArgb(120, 255, 0, 0);
        public HitBoxType Type { get; protected set; }
        public Routine Action;

        public abstract void Draw(System.Drawing.Graphics g, 
            int centerX, int centerY, Zoom Zoom);

        public abstract void Draw(System.Drawing.Graphics g,
            int centerX, int centerY, Zoom Zoom, int borderSize);

        public override string ToString()
        {
            return Name;
        }
    }
}
