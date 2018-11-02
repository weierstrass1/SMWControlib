using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMWControlibBackend.Graphics.Frames
{
    public class AnimationContainer
    {
        public FrameMaskContainer[] FrameMasks;
        public int PlayType;
        public string Name;

        public Animation ToAnimation(Frame[] frames)
        {
            Animation an = new Animation()
            {
                PlayType = (PlayType)PlayType,
                Name = Name
            };

            FrameMask[] fms = new FrameMask[FrameMasks.Length];

            for (int i = 0; i < FrameMasks.Length; i++)
            {
                fms[i] = FrameMasks[i].ToFrameMask(frames);
            }

            an.LoadFrameMasks(fms);

            return an;
        }
        public void ToAnimationContainer(Animation an)
        {
            Name = an.Name;
            PlayType = (int)an.PlayType;
            FrameMasks = new FrameMaskContainer[an.Length];
            FrameMask[] fms = an.GetFrameMasks();

            for (int i = 0; i < FrameMasks.Length; i++)
            {
                FrameMasks[i] = new FrameMaskContainer();
                FrameMasks[i].ToFrameMaskContainer(fms[i]);
            }
        }
    }
}
