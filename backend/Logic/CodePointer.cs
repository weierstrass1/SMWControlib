using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace SMWControlibBackend.Logic
{
    public class CodePointer
    {
        public int Start { get; set; }
        public int End { get; set; }
        public Group Group { get; set; }
        public string Code { get; set; }

        public CodePointer()
        {
        }

        public void Move(int pos)
        {
            Start += pos;
            End += pos;
        }

        public void Append(string s)
        {
            Code = Code + s;
            End = Start + Code.Length;
        }
        public static CodePointer[] Split(string line, string separatorPattern)
        {
            if (line == "" || line == null || line.Length <= 0) return null;
            MatchCollection ms = Regex.Matches(line, separatorPattern);
            if (ms.Count == 0)
            {
                CodePointer[] pa = new CodePointer[1];
                pa[0] = new CodePointer
                {
                    Start = 0,
                    End = line.Length - 1,
                    Code = line
                };
                return pa;
            }

            List<CodePointer> pointers = new List<CodePointer>();
            CodePointer p;
            int s = 0;
            int e = 0;

            foreach (Match m in ms)
            {
                e = m.Index - 1;
                if (e >= s)
                {
                    p = new CodePointer
                    {
                        Start = s,
                        End = e,
                        Code = line.Substring(s, 1 + e - s)
                    };
                    pointers.Add(p);
                }
                s = m.Index + m.Length;
            }

            e = line.Length - 1;
            if (e >= s)
            {
                p = new CodePointer
                {
                    Start = s,
                    End = e,
                    Code = line.Substring(s, 1 + e - s)
                };
                pointers.Add(p);
            }

            return pointers.ToArray();
        }

        public override string ToString()
        {
            return Code;
        }
    }
}
