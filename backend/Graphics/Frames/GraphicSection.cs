using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
