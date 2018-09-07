using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

        private Command(string name, ArgsTypes[] args)
        {
            Name = name;
            Args = args;
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

        public static Dictionary<string, Command[]> GetCommands(string path, ArgsTypes[] argsTypes)
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
                
                if(nums > cmd.Length-2)
                {
                    throw new Exception("Invalid Command at line: " + i);
                }
                newargs = null;
                if (nums > 0)
                {
                    newargs = new ArgsTypes[nums];
                    for (int j = 0; j < nums; j++)
                    {
                        newargs[j] = ArgsTypes.Exists(cmd[j + 2], argsTypes);
                        if (newargs[j] == null)
                        {
                            throw new Exception("Invalid Command at line: " + i);
                        }
                    }
                }

                commands[i - 1] = new Command(cmd[0].ToLower(), newargs);
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
