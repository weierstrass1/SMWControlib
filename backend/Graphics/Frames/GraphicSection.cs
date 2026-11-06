using System.Collections.Generic;

namespace SMWControlibBackend.Graphics.Frames
{
    public class GraphicSection
    {
        public List<Frame> SectionFrames { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }

        public GraphicSection(string name, string description)
        {
            Name = name;
            Description = description;
            SectionFrames = new List<Frame>();
        }

        
    }
}
