using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMWControlibBackend.Graphics.Frames
{
    public class FrameMask
    {
        public Frame Frame;
        public int Time = 4;
        public int Index { get; internal set; }
        public FrameMask Next;
    }
}
