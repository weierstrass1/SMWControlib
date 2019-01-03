namespace SMWControlibBackend.Graphics.Frames
{
    public class FrameMaskContainer
    {
        public string FrameName;
        public int Time;
        public int Index;
        public bool FlipX;
        public bool FlipY;

        public FrameMask ToFrameMask(Frame[] frames)
        {
            if (frames == null || frames.Length <= 0) return null;
            Frame f = frames[0];

            for (int i = 0; i < frames.Length; i++)
            {
                if (frames[i].Name == FrameName)
                {
                    f = frames[i];
                    break;
                }
            }

            FrameMask fm = new FrameMask()
            {
                Frame = f,
                Time = Time,
                FlipX = FlipX,
                FlipY = FlipY,
                Index = Index
            };
            
            return fm;
        }
        public void ToFrameMaskContainer(FrameMask fm)
        {
            FrameName = fm.Frame.Name;
            Time = fm.Time;
            Index = fm.Index;
            FlipX = fm.FlipX;
            FlipY = fm.FlipY;
        }
    }
}
