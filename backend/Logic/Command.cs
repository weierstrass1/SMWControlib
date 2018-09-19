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

        private Command(string name, ArgsTypes[] args, Group group)
        {
            Name = name;
            Args = args;
            Group = group;
        }

        public CodePointer[] GetPointers(Dictionary<string, Define> Defines,
            int offset, string cmd, int line, int startIndex)
        {
            CodePointer[] cps = GetPointers(offset, cmd);

            if (cps.Length > 1 || Args == null || Args.Length <= 0) return cps;

            CodePointer cp2 = new CodePointer
            {
                Start = offset + cps[0].Code.Length,
                End = offset + cps[0].Code.Length + cmd.Length - 1,
                Code = cmd.Substring(cps[0].Code.Length),
                Group = Args[0].Group
            };

            CodePointer[] cps2 = new CodePointer[2];
            cps2[0] = cps[0];
            cps2[1] = cp2;
            return cps2;
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
        
        public bool IsCorrect(Dictionary<string, Define> Defines,
            string cmd, int line, int startIndex)
        {
            if (IsCorrect(cmd)) return true;

            string s = Define.TryReplace(Defines, cmd, line, startIndex);

            if (s == "") return false;

            return IsCorrect(s);
        }

        public bool IsCorrect(string cmd)
        {
            cmd = cmd.Split(';')[0];
            cmd = cmd.ToLower();
            string n = cmd.Replace("\t", " ");
            n = n.Split(' ')[0];

            if (n != Name) return false;

            cmd = cmd.Replace(" ", "").Replace("\t", "");
            if (Args == null || Args.Length <= 0)
            {
                if (cmd == Name)
                    return true;
                else
                    return false;
            }

            cmd = cmd.Remove(0, Name.Length);

            if(Args.Length==1)
            {
                if (Args[0].IsCorrect(cmd))
                    return true;
                else
                    return false;
            }

            string[] args = cmd.Split(',');
            if (Args.Length != args.Length) return false;

            for (int i = 0; i < args.Length; i++)
            {
                if (!Args[i].IsCorrect(args[i])) return false;
            }
            return true;
        }

        public static Dictionary<string, Command[]> GetCommands(string path,
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
            }

            Dictionary<string, List<Command>> dic = new Dictionary<string, List<Command>>();

            foreach (Command c in commands)
            {
                if (!dic.ContainsKey(c.Name))
                {
                    dic.Add(c.Name, new List<Command>());
                }
                dic[c.Name].Add(c);
            }

            Dictionary<string, Command[]> arrDic = new Dictionary<string, Command[]>();
            foreach (KeyValuePair<string, List<Command>> pair in dic)
            {
                arrDic.Add(pair.Key, pair.Value.ToArray());
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
