using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SMWControlibBackend.Interaction;

namespace SMWControlibBackend.Graphics.Frames
{
    public class FrameContainer
    {
        public string Name;
        public string[] HitboxesNames;
        public string[] InteractionPointsNames;
        public TileMaskContainer[] TileMasks;
        public int MidX;
        public int MidY;

        public void ToFrameContainer(Frame f, List<HitBox> hbs, List<InteractionPoint> ips)
        {
            Name = f.Name;
            MidX = f.MidX;
            MidY = f.MidY;
            HitboxesNames = new string[f.HitBoxes.Count];
            int i = 0;
            foreach (HitBox hb in f.HitBoxes)
            {
                HitboxesNames[i] = "" + hbs.IndexOf(hb) + "_" + hb.Name;
                i++;
            }

            InteractionPointsNames = new string[f.InteractionPoints.Count];
            i = 0;
            foreach (InteractionPoint hb in f.InteractionPoints)
            {
                InteractionPointsNames[i] = "" + ips.IndexOf(hb) + "_" + hb.Name;
                i++;
            }

            TileMasks = new TileMaskContainer[f.Tiles.Count];
            i = 0;

            foreach(TileMask tm in f.Tiles)
            {
                TileMasks[i] = new TileMaskContainer();
                TileMasks[i].ToTileMaskContainer(tm);
                i++;
            }
        }
    }
}
