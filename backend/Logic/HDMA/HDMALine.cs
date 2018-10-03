using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMWControlibBackend.Logic.HDMA
{
    public class HDMALine
    {
        public byte Height;
        public byte[,] Values { get; private set; }

        public HDMALine(int valueSize, int regsSize)
        {
            Values = new byte[regsSize,valueSize];
        }
    }
}
