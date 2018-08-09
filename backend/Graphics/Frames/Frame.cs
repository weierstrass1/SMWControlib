using System.Collections.Generic;

namespace SMWControlibBackend.Graphics.Frames
{
    public class Frame
    {
        public string Name;
        public List<TileMask> Tiles { get; private set; }

        public Frame()
        {
            Tiles = new List<TileMask>();
        }

        public Frame Duplicate()
        {
            Frame frame = new Frame()
            {
                Name = ""
            };

            foreach (TileMask tm in Tiles)
            {
                frame.Tiles.Add(tm.Clone());
            }
            return frame;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
