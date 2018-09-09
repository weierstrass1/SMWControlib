using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMWControlibBackend.Logic
{
    public class MinusLabel : Label
    {
        public MinusLabel()
        {
            reGex = @"^\-+:?$";
        }

        public override bool IsValid(int Line, string[] Lines, NormalLabel Label)
        {
            if (Lines == null || Lines.Length <= 0) return false;
            if (Line < 0 || Line >= Lines.Length) return false;

            return SyntaxIsCorrect(Lines[Line]);
        }
    }
}
