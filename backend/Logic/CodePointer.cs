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
        public int Start { get; private set; }
        public int End { get; private set; }
        public string Group { get; internal set; }
        public string Code { get; internal set; }

        private CodePointer()
        {
        }

        public static CodePointer[] Split(string line, string separatorPattern)
        {
            MatchCollection ms = Regex.Matches(line, separatorPattern);
            string[] strings = Regex.Split(line, separatorPattern);
            List<string> s = new List<string>();
            foreach(string ss in strings)
            {
                if (ss != "\t" && ss != " " && ss != "" && ss != null && ss.Length > 0)
                {
                    s.Add(ss);
                }
            }
            strings = s.ToArray();
            if (strings == null || strings.Length <= 0) return null;

            int st = 0;
            IEnumerator enumerator = ms.GetEnumerator();
            enumerator.Reset();
            CodePointer[] pointers = new CodePointer[strings.Length];
            bool moved = false;

            for (int i = 0, j = -1; i < strings.Length; i++, j++)
            {
                if (moved)
                {
                    st += ((Match)enumerator.Current).Length;
                }
                pointers[i] = new CodePointer
                {
                    Start = st,
                    End = st + strings[i].Length,
                    Group = "Unknown",
                    Code = strings[i]
                };

                moved = enumerator.MoveNext();
                st += strings[i].Length;
            }

            return pointers;
        }
    }
}
