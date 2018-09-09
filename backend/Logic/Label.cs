using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SMWControlibBackend.Logic
{
    public abstract class Label
    {
        protected string reGex = "";

        public abstract bool IsValid(int Line, string[] Lines, NormalLabel Label);

        public bool SyntaxIsCorrect(string arg)
        {
            Match m = Regex.Match(arg, reGex);
            return m.Success;
        }
    }
}
