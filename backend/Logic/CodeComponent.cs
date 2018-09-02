using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMWControlibBackend.Logic
{
    public abstract class CodeComponent
    {
        public string Name;
        public string Code;

        public abstract Tuple<bool, string> IsCorrect();

        public override string ToString()
        {
            return Name;
        }
    }
}
