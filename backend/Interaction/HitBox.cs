using SMWControlibBackend.Graphics;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

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
        public string ActionName = "";
        public int Size;

        public abstract void Draw(System.Drawing.Graphics g, 
            int centerX, int centerY, Zoom Zoom);

        public abstract void Draw(System.Drawing.Graphics g,
            int centerX, int centerY, Zoom Zoom, int borderSize);

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public abstract string GetHitBoxString(string[] actionNames);

        public abstract HitBox GetFlippedBox(bool FlipX, bool FlipY, int midX, int midY);

        public override string ToString()
        {
            return Name;
        }

        public override int GetHashCode()
        {
            var hashCode = 108845050;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + XOffset.GetHashCode();
            hashCode = hashCode * -1521134295 + YOffset.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(BorderColor);
            hashCode = hashCode * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(FrontColor);
            hashCode = hashCode * -1521134295 + EqualityComparer<HitBoxType>.Default.GetHashCode(Type);
            hashCode = hashCode * -1521134295 + ActionName.GetHashCode();
            return hashCode;
        }

        public static string GetHitboxesFlipAdder(HitBox[] hitboxes, bool FlipX, bool FlipY)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("db $00");

            int leng = hitboxes.Length;
            if (FlipX) leng /= 2;
            if (FlipY) leng /= 2;

            int mul = 1;
            if (FlipX)
            {
                sb.Append(",$" + (leng * mul).ToString("X2"));
                mul++;
            }
            if (FlipY)
            {
                sb.Append(",$" + (leng * mul).ToString("X2"));
                mul++;
            }
            if (FlipX && FlipY) 
            {
                sb.Append(",$" + (leng * mul).ToString("X2"));
            }

            return sb.ToString();
        }

        public static string GetHitboxesStartsFromArray(HitBox[] hitboxes)
        {
            StringBuilder sb = new StringBuilder();
            int count = 0;

            for (int i = 0; i < hitboxes.Length; i++)
            {
                if(i%16==0)
                {
                    if (i != 0) 
                    {
                        sb.Remove(sb.Length - 1, 1);
                        sb.Append("\n\t");
                    }
                    sb.Append("dw ");
                }
                sb.Append("$" + count.ToString("X4") + ",");
                count += hitboxes[i].Size;
            }
            sb.Remove(sb.Length - 1, 1);

            return sb.ToString();
        }

        public static string GetHitboxesFromArray(HitBox[] hitboxes, string[] actionNames)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hitboxes.Length; i++)
            {
                sb.Append(hitboxes[i].GetHitBoxString(actionNames) + "\t");
            }
            return sb.ToString();
        }

        public abstract HitBox Clone();
    }
}
