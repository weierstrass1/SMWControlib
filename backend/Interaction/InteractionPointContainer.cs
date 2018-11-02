using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMWControlibBackend.Interaction
{
    public class InteractionPointContainer : HitboxContainer
    {
        public int InteractionType;

        public override HitBox ToHitBox()
        {
            InteractionPoint hb = (InteractionPoint)base.ToHitBox();
            hb.InteractionType = InteractionType;
            return hb;
        }
        public override void ToHitboxContainer(HitBox hb)
        {
            InteractionPoint ip = ((InteractionPoint)hb);
            base.ToHitboxContainer(ip);
            InteractionType = ip.InteractionType;
        }
    }
}
