using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SMWControlibBackend.Logic
{
    public class Define
    {
        public static readonly Define Default = new Define();
        public string Name;
        public List<Tuple<int, int, string>> OthersPositions;
        public Group Group;

        public Define()
        {
            OthersPositions = new List<Tuple<int, int, string>>();
        }

        internal static List<CodePointer> definesFounded;
        internal static List<CodePointer> replaced;
        public static string Replace(Dictionary<string, Define> Defines,
            string define, int line, int startIndex)
        {
            definesFounded = new List<CodePointer>();
            replaced = new List<CodePointer>();
            MatchCollection ms = Regex.Matches(define, startPattern1);
            if (ms.Count <= 0) return define;
            List<Define> defines = new List<Define>();
            CodePointer cp;
            string s;
            foreach (Match m in ms)
            {
                s = m.ToString();
                if (Defines.ContainsKey(s))
                {
                    defines.Add(Defines[s]);
                    cp = new CodePointer
                    {
                        Start = startIndex + m.Index,
                        End = startIndex + m.Index + m.Length - 1,
                        Code = m.ToString()
                    };
                    definesFounded.Add(cp);
                }
            }

            List<string> SplitedDef = new List<string>();
            int st = 0;
            int len = 0;
            foreach(Match m in ms)
            {
                if(m.Index == 0)
                {
                    len = m.Length;
                    SplitedDef.Add(define.Substring(st, len));
                }
                else
                {
                    len = m.Index - st;
                    SplitedDef.Add(define.Substring(st, len));
                    st = m.Index;
                    len = m.Length;
                    SplitedDef.Add(define.Substring(st, len));
                }
                st += m.Length;
            }
            int lenn = define.Length - st;
            if (lenn > 0)
                SplitedDef.Add(define.Substring(st , lenn));

            Define[] defarr = defines.ToArray();
            if (defarr.Length <= 0) return define;
            int q = 0;
            StringBuilder sb = new StringBuilder();
            string newm;
            Tuple<int, int, string> tup;

            foreach (string str in SplitedDef) 
            {
                if (q < defarr.Length && defarr[q].Name == str)
                {
                    tup = defarr[q].NearestPosition(line, startIndex);
                    if(tup==null)
                    {
                        sb.Append(str);
                    }
                    else
                    {
                        newm = tup.Item3;
                        cp = new CodePointer
                        {
                            Start = startIndex + sb.Length,
                            End = startIndex + sb.Length + newm.Length - 1,
                            Code = newm
                        };
                        replaced.Add(cp);
                        sb.Append(newm);
                    }
                    q++;
                }
                else
                {
                    sb.Append(str);
                }
            }

            return sb.ToString();
        }

        public Tuple<int, int, string> NearestPosition(int line, int startIndex)
        {
            Tuple<int, int, string> vals = null;
            foreach (Tuple<int, int, string> t in OthersPositions)
            {
                if (t.Item1 < line ||
                    (t.Item1 == line && t.Item2 > startIndex) &&
                    (vals == null || t.Item1 > vals.Item1 ||
                    (t.Item1 == vals.Item1 && t.Item2 > vals.Item2)))
                {
                    vals = t;
                }
            }

            return vals;
        }

        const string startPattern1 = @"\![\da-zA-Z_]*";
        const string startPattern = @"^\![\da-zA-Z_]*";
        public const string MidPattern = @"(\t|\ )+(\+|\:|\#|\?)?\=(\t|\ )+";
        static string endPattern = @"(\S+|\" + '"' + @"(\S+(\t|\ )*)*\" +
            '"' + @")(\t|\ )*$";
        static string completePattern = startPattern + MidPattern + endPattern;
        public static bool SignatureIsCorrect(string define)
        {
            Match m = Regex.Match(define, completePattern);

            if (!m.Success) return false;

            string newdef = define.Replace("" + '"', "");

            return (newdef.Length == define.Length - 2) || (newdef.Length == define.Length);
        }

        public static List<CodePointer> GetPointers(string define, int startIndex)
        {
            List<CodePointer> pointers = new List<CodePointer>();
            Match m = Regex.Match(define, startPattern);
            string name = m.ToString();

            CodePointer cp = new CodePointer
            {
                Start = m.Index + startIndex,
                End = m.Index + startIndex + name.Length - 1,
                Code = name
            };

            pointers.Add(cp);

            define = define.Substring(m.Length);

            m = Regex.Match(define, MidPattern);

            define = define.Substring(m.Length);

            int st = cp.End + m.Length;
            int en = define.Length - 1 + startIndex;

            cp = new CodePointer
            {
                Start = st + 1,
                End = en,
                Code = define
            };

            pointers.Add(cp);

            return pointers;
        }

        internal static Error possibleError;
        public static Define GetDefine(Dictionary<string, Define> Defines,
            string define, int line, int startIndex)
        {
            possibleError = null;
            Match m = Regex.Match(define, startPattern);
            string name = m.ToString();

            define = define.Substring(name.Length);

            m = Regex.Match(define, "^" + MidPattern);

            define = define.Substring(m.Length);
            int stind = startIndex + name.Length + m.Length;
            string value = Replace(Defines, define, line, stind);

            m = Regex.Match(value, startPattern1);
            if(m.Success)
            {
                possibleError = new Error(line, stind + m.Index, m.ToString(),
                    ErrorCode.DefineNotFound, m.ToString());
            }

            Define def = new Define
            {
                Name = name
            };
            def.OthersPositions.Add(new Tuple<int, int, string>(line, startIndex, value));

            return def;
        }
    }
}
