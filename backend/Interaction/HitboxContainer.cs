using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SMWControlibBackend.Interaction
{
    [XmlInclude(typeof(RectangleHitboxContainer))]
    [XmlInclude(typeof(InteractionPointContainer))]
    public class HitboxContainer
    {
        public int Type;
        public string Name;
        public int XOffset, YOffset;
        public int BorderColorA, BorderColorR, BorderColorG, BorderColorB;
        public int FrontColorA, FrontColorR, FrontColorG, FrontColorB;
        public int ActionID;

        public virtual HitBox ToHitBox()
        {
            HitBox hb;

            switch(Type)
            {
                case 0:
                    hb = new RectangleHitBox();
                    break;
                default:
                    hb = new InteractionPoint();
                    break;
            }

            hb.Name = Name;
            hb.XOffset = XOffset;
            hb.YOffset = YOffset;
            hb.BorderColor = Color.FromArgb(BorderColorA, BorderColorR, BorderColorG, BorderColorB);
            hb.FrontColor = Color.FromArgb(FrontColorA, FrontColorR, FrontColorG, FrontColorB);

            return hb;
        }

        public virtual void ToHitboxContainer(HitBox hb)
        {
            Type = hb.Type;
            Name = hb.Name;
            XOffset = hb.XOffset;
            YOffset = hb.YOffset;
            BorderColorA = hb.BorderColor.A;
            BorderColorR = hb.BorderColor.R;
            BorderColorG = hb.BorderColor.G;
            BorderColorB = hb.BorderColor.B;
            FrontColorA = hb.FrontColor.A;
            FrontColorR = hb.FrontColor.R;
            FrontColorG = hb.FrontColor.G;
            FrontColorB = hb.FrontColor.B;

            ActionID = hb.ActionID;
        }
    }
}
