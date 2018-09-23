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
        public const int MAX_ITERATION_COUNTER = 30;

        public Define()
        {
            OthersPositions = new List<Tuple<int, int, string>>();
        }

        public static Error PossibleError = null;
        public static Define GetDefine(Dictionary<string, Define> Defines,
            string define, int line, int startIndex)
        {
            if (!SignatureIsCorrect(define))
            {
                PossibleError = new Error(line, startIndex, define,
                    ErrorCode.InvalidDefineSignature, define);
                return null;
            }

            Match m = Regex.Match(define, startPattern);

            define = define.Substring(m.ToString().Length);

            string name = m.ToString();

            m = Regex.Match(define, MidPattern);
            define = define.Substring(m.ToString().Length);
            string prev = define;

            define = TryReplace(Defines, define, line, startIndex);
            if (define == "") return null;
            if (define == null) define = prev;

            if (Defines.ContainsKey(name)) 
            {
                Defines[name].OthersPositions.Add(
                    new Tuple<int, int, string>(line, startIndex, define));
                return Default;
            }

            Define def = new Define
            {
                Name = name
            };

            def.OthersPositions.Add(
                new Tuple<int, int, string>(line, startIndex, define));

            return def;
        }

        public static string TryReplace(Dictionary<string, Define> Defines,
            string define, int line, int startIndex)
        {
            if(!Regex.Match(define, @"\!").Success)
            {
                return null;
            }
            int i = 0;
            string newDef;

            while (Regex.Match(define, @"\!").Success &&
                i < MAX_ITERATION_COUNTER)
            {
                newDef = Replace(Defines, define, line, startIndex);
                if (newDef == "")
                {
                    if (PossibleError.Code != ErrorCode.DefineNotFound)
                    {
                        PossibleError = new Error(line, startIndex, define,
                            ErrorCode.InvalidDefine, define);
                    }
                    return "";
                }
                define = newDef;
            }

            if (Regex.Match(define, @"\!").Success)
            {
                return "";
            }

            return define;
        }

        public static string Replace(Dictionary<string, Define> Defines,
            string define, int line, int startIndex)
        {
            PossibleError = null;
            MatchCollection ms = Regex.Matches(define, startPattern1);
            if (ms.Count <= 0) return "";
            List<Define> defines = new List<Define>();
            string s;
            foreach (Match m in ms)
            {
                s = m.ToString();
                if (Defines.ContainsKey(s))
                {
                    defines.Add(Defines[s]);
                }
                else
                {
                    PossibleError = new Error(line, startIndex, define,
                        ErrorCode.DefineNotFound, m.ToString());
                    return "";
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

            Define[] defarr = defines.ToArray();
            int q = 0;
            StringBuilder sb = new StringBuilder();

            foreach (string str in SplitedDef) 
            {
                if (defarr[q].Name == str)
                {
                    sb.Append(defarr[q].NearestPosition(line, startIndex).Item3);
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

        public static List<CodePointer> DefinesPositions(string define)
        {
            MatchCollection ms = Regex.Matches(define, startPattern1);
            if (ms.Count <= 0) return null;

            List<CodePointer> pointers = new List<CodePointer>();
            CodePointer cp;

            foreach(Match m in ms)
            {
                cp = new CodePointer
                {
                    Start = m.Index,
                    End = m.Index + m.Length - 1,
                    Code = m.ToString()
                };
                pointers.Add(cp);
            }

            return pointers;
        }
    }
}
