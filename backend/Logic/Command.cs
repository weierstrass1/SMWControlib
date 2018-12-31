using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SMWControlibBackend.Logic
{
    public class Command
    {
        public string Name { get; private set; }
        public int ArgsCount
        {
            get
            {
                if (Args!=null)
                    return Args.Length;
                return 0;
            }
        }
        public ArgsTypes[] Args;
        public Group Group { get; private set; }

        public string Prefix { get; private set; }
        public string Sufix { get; private set; }

        private Command(string name, ArgsTypes[] args, Group group)
        {
            Name = name;
            Args = args;
            Group = group;
        }

        string whiteSpaces = @"^(\ |\t)+";
        public CodePointer[] GetPointers(int offset, string cmd)
        {
            string name = cmd.Replace('\t', ' ').Split(' ')[0];

            List<CodePointer> pointers = new List<CodePointer>();

            CodePointer cp;

            cp = new CodePointer
            {
                Start = offset,
                End = offset + name.Length - 1,
                Code = name,
                Group = Group
            };
            pointers.Add(cp);

            string arg = cmd.Substring(name.Length);
            Match m;
            CodePointer[] newcps;
            int offs = cp.End + 1;

            if (Args != null)
            {
                string newAr;
                for (int i = 0; i < Args.Length; i++)
                {
                    newAr = Args[i].RegEXPattern.
                        Remove(Args[i].RegEXPattern.Length - 1);

                    m = Regex.Match(arg, whiteSpaces);
                    if (m.Success)
                    {
                        offs += m.Length;
                        arg = arg.Substring(m.Length);
                    }

                    m = Regex.Match(arg, newAr);
                    if(m.Success)
                    {
                        newcps = Args[i].GetPointers(offs, m.ToString());

                        if (newcps != null)
                        {
                            foreach(CodePointer codp in newcps)
                            {
                                pointers.Add(codp);
                            }
                        }
                        arg = arg.Substring(m.ToString().Length);
                        offs += m.ToString().Length;
                    }
                    if (i < Args.Length - 1)
                    {
                        arg = arg.Substring(1);
                        offs++;
                    }
                }
            }

            return pointers.ToArray();
        }

        public bool IsCorrect(string cmd)
        {
            
            if (Args == null || Args.Length <= 0)
            {
                if (cmd == null || cmd == "" || cmd.Length <= 0)
                    return true;
                else
                    return false;
            }

            if (Args.Length == 1)
            {
                if (Args[0].IsCorrectWithoutPrefixSufix(cmd))
                    return true;
                else
                    return false;
            }

            string[] args = cmd.Split(',');
            if (Args.Length != args.Length) return false;

            for (int i = 0; i < args.Length; i++)
            {
                if (i == 0)
                {
                    if (!Args[i].IsCorrectWithoutPrefix(args[i]))
                        return false;
                }
                else if (i == args.Length - 1)
                {
                    if (!Args[i].IsCorrectWithoutSufix(args[i]))
                        return false;
                }
                else
                {
                    if (!Args[i].IsCorrect(args[i]))
                        return false;
                }
            }
            return true;
        }

        static string numPostMod = @"((\*\*|\~|\%|\<\<|\>\>|\+|\-|\/|\&|\||\*|\^)(\%[0-1]+|\$[\da-fA-F]+|\d+))*";
        public static Dictionary<string, Dictionary<string,
            Dictionary<string, Command[]>>> GetCommands(string path,
            ArgsTypes[] argsTypes, Group[] groups)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException("File not found.");

            string[] commandsSTR = File.ReadAllLines(path);

            if (commandsSTR.Length <= 1)
                throw new Exception("File doesn't have any command.");

            Command[] commands = new Command[commandsSTR.Length - 1];
            string[] cmd;
            int nums = 0;
            ArgsTypes[] newargs;

            for (int i = 1; i < commandsSTR.Length; i++)
            {
                cmd = commandsSTR[i].Split(';');
                if (cmd.Length <= 1)
                    throw new Exception("Invalid Command at line: " + i);

                if (!int.TryParse(cmd[1].Replace(" ", "").Replace("   ", ""),
                    out nums)) 
                {
                    throw new Exception("Invalid Command at line: " + i);
                }
                
                if(nums > cmd.Length-3)
                {
                    throw new Exception("Invalid Command at line: " + i);
                }
                newargs = null;
                if (nums > 0)
                {
                    newargs = new ArgsTypes[nums];
                    for (int j = 0; j < nums; j++)
                    {
                        newargs[j] = ArgsTypes.Exists(cmd[j + 3], argsTypes);
                        if (newargs[j] == null)
                        {
                            throw new Exception("Invalid Command at line: " + i);
                        }
                    }
                }

                commands[i - 1] = new Command(cmd[0].ToLower(), newargs,
                    Group.FindGroup(groups, cmd[2]));
                if (commands[i - 1].Args != null && commands[i - 1].Args.Length > 0)
                {
                    commands[i - 1].Prefix = commands[i - 1].Args[0].Prefix;
                    commands[i - 1].Sufix = commands[i - 1].Args[commands[i - 1].Args.Length - 1].Postfix;

                    string pref;
                    pref = commands[i - 1].Prefix.Replace(",", ", *").Replace("[", @"\[").Replace("]", @"\]");
                    string postf;
                    postf = commands[i - 1].Sufix.Replace(",", ", *").Replace("[", @"\[").Replace("]", @"\]");
                    pref = pref.Replace(".", @"\.").Replace("+", @"\+");
                    postf = postf.Replace(".", @"\.").Replace("+", @"\+");
                    pref = pref.Replace("(", @"\(").Replace(")", @"\)" + numPostMod);
                    postf = postf.Replace("(", @"\(").Replace(")", @"\)" + numPostMod);
                    commands[i - 1].Prefix = "^" + pref;
                    commands[i - 1].Sufix = postf + "$";
                }
                if (commands[i - 1].Prefix == null || commands[i - 1].Prefix == "^")
                    commands[i - 1].Prefix = "NULL";
                if (commands[i - 1].Sufix == null || commands[i - 1].Sufix == "$") 
                    commands[i - 1].Sufix = "NULL";
            }

            Dictionary<string, Dictionary<string, Dictionary<string, List<Command>>>> dic =
                new Dictionary<string, Dictionary<string, Dictionary<string, List<Command>>>>();

            foreach (Command c in commands)
            {
                if (!dic.ContainsKey(c.Name))
                {
                    dic.Add(c.Name, new Dictionary<string, Dictionary<string, List<Command>>>());
                }
                if(!dic[c.Name].ContainsKey(c.Prefix))
                {
                    dic[c.Name].Add(c.Prefix, new Dictionary<string, List<Command>>());
                }
                if(!dic[c.Name][c.Prefix].ContainsKey(c.Sufix))
                {
                    dic[c.Name][c.Prefix].Add(c.Sufix, new List<Command>());
                }
                dic[c.Name][c.Prefix][c.Sufix].Add(c);
            }

            Dictionary<string, Dictionary<string,
            Dictionary<string, Command[]>>> arrDic = new Dictionary<string, Dictionary<string,
            Dictionary<string, Command[]>>>();
            foreach (string s in dic.Keys)
            {
                arrDic.Add(s, new Dictionary<string, Dictionary<string, Command[]>>());
                foreach (string s2 in dic[s].Keys)
                {
                    arrDic[s].Add(s2, new Dictionary<string, Command[]>());
                    foreach (string s3 in dic[s][s2].Keys)
                    {
                        arrDic[s][s2].Add(s3, dic[s][s2][s3].ToArray());
                    }
                }
            }

            return arrDic;
        }

        public override string ToString()
        {
            string s = Name;
            if(Args!=null)
            {
                s += " ";
                foreach (ArgsTypes at in Args)
                {
                    s += at.Name + ",";
                }
                s = s.Remove(s.Length - 1, 1);
            }
            
            return s;
        }
    }
}
