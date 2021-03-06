﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace SMWControlibBackend.Logic
{
    public class ArgsTypes
    {
        public string Name { get; private set; }
        public string Prefix { get; private set; }
        public string Postfix { get; private set; }
        public int Bytes { get; private set; }
        public string Type { get; private set; }
        public string RegEXPattern { get; private set; }
        public string RegEXPatternWithoutPrefix { get; private set; }
        public string RegEXPatternWithoutSufix { get; private set; }
        public string RegEXPatternWithoutPrefixSufix { get; private set; }
        public Group Group { get; private set; }

        private ArgsTypes()
        {
        }

        //string spliter = @"((\*\*|\~|\%|\<\<|\>\>|\+|\-|\/|\&|\||\*|\^)(\%[0-1]+|\$[\da-fA-F]+|\d+))+";
        public CodePointer[] GetPointers(int offset, string arg)
        {
            if (arg == null || arg == "" || arg.Length <= 0) return null;
            List<CodePointer> pointers = new List<CodePointer>();

            CodePointer cp;
            cp = new CodePointer
            {
                Start = offset,
                End = offset + arg.Length - 1,
                Code = arg,
                Group = Group
            };
            pointers.Add(cp);

            return pointers.ToArray();
        }

        public bool IsCorrect(string arg)
        {
            if (arg == null || arg == "" || arg.Length <= 0)
                return false;
            MatchCollection m = Regex.Matches(arg, RegEXPattern);
            if (m.Count == 0) return false;
            return true;
        }

        public bool IsCorrectWithoutPrefix(string arg)
        {
            if (arg == null) return false;
            MatchCollection m = Regex.Matches(arg, RegEXPatternWithoutPrefix);
            if (m.Count == 0) return false;
            return true;
        }

        public bool IsCorrectWithoutSufix(string arg)
        {
            if (arg == null) return false;
            MatchCollection m = Regex.Matches(arg, RegEXPatternWithoutSufix);
            if (m.Count == 0) return false;
            return true;
        }

        public bool IsCorrectWithoutPrefixSufix(string arg)
        {
            if (arg == null)
                return false;
            MatchCollection m = Regex.Matches(arg, RegEXPatternWithoutPrefixSufix);
            if (m.Count == 0) return false;
            return true;
        }
        public static ArgsTypes[] GetArgsTypes(string path, Group[] groups)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException("File not found.");

            string[] argsSTR = File.ReadAllLines(path);

            if (argsSTR.Length <= 1)
                throw new Exception("File doesn't have any arg type.");

            ArgsTypes[] argsTypes = new ArgsTypes[argsSTR.Length - 1];
            string[] args;
            int b = 0;

            for (int i = 1; i < argsSTR.Length; i++)
            {
                args = argsSTR[i].Split(';');

                if (args.Length != 6) 
                    throw new Exception("Invalid Command at line: " + i);

                if (args[3] == "var")
                {
                    b = -1;
                }
                else if (!int.TryParse(args[3], out b))
                    throw new Exception("Invalid Command at line: " + i);

                argsTypes[i - 1] = new ArgsTypes
                {
                    Name = args[0],
                    Prefix = args[1].ToLower(),
                    Postfix = args[2].ToLower(),
                    Bytes = b,
                    Type = args[4],
                    Group = Group.FindGroup(groups, args[5])
                };
                argsTypes[i - 1].buildRegEX();
            }

            return argsTypes;
        }

        string[] dnums = { "([0-9]|[1-8][0-9]|9[0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])",
                            "([0-9]|[1-8][0-9]|9[0-9]|[1-8][0-9]{2}|9[0-8][0-9]|99[0-9]|[1-8][0-9]{3}|9[0-8][0-9]{2}|99[0-8][0-9]|999[0-9]|[1-5][0-9]{4}|6[0-4][0-9]{3}|65[0-4][0-9]{2}|655[0-2][0-9]|6553[0-5])",
                            "([0-9]|[1-8][0-9]|9[0-9]|[1-8][0-9]{2}|9[0-8][0-9]|99[0-9]|[1-8][0-9]{3}|9[0-8][0-9]{2}|99[0-8][0-9]|999[0-9]|[1-8][0-9]{4}|9[0-8][0-9]{3}|99[0-8][0-9]{2}|999[0-8][0-9]|9999[0-9]|[1-8][0-9]{5}|9[0-8][0-9]{4}|99[0-8][0-9]{3}|999[0-8][0-9]{2}|9999[0-8][0-9]|99999[0-9]|[1-8][0-9]{6}|9[0-8][0-9]{5}|99[0-8][0-9]{4}|999[0-8][0-9]{3}|9999[0-8][0-9]{2}|99999[0-8][0-9]|999999[0-9]|1[0-5][0-9]{6}|16[0-6][0-9]{5}|167[0-6][0-9]{4}|1677[0-6][0-9]{3}|16777[01][0-9]{2}|1677720[0-9]|1677721[0-5])"};

        string numPostMod = @"((\*\*|\~|\%|\<\<|\>\>|\+|\-|\/|\&|\||\*|\^)(\%[0-1]+|\$[\da-fA-F]+|\d+))*";
        private void buildRegEX()
        {
            RegEXPattern = "^";
            RegEXPatternWithoutPrefix = "^";
            RegEXPatternWithoutSufix = "^";
            RegEXPatternWithoutPrefixSufix = "^";
            string pref;
            pref = Prefix.Replace(",", ", *").Replace("[", @"\[").Replace("]", @"\]");
            string postf;
            postf = Postfix.Replace(",", ", *").Replace("[", @"\[").Replace("]", @"\]");
            pref = pref.Replace(".", @"\.").Replace("+", @"\+");
            postf = postf.Replace(".", @"\.").Replace("+", @"\+");
            pref = pref.Replace("(", @"\(").Replace(")", @"\)" + numPostMod);
            postf = postf.Replace("(", @"\(").Replace(")", @"\)" + numPostMod);

            RegEXPattern += pref;
            RegEXPatternWithoutSufix += pref;

            if (Bytes == 0)
            {
                RegEXPattern += postf + "$";
                return;
            }

            if(Type == "string with spaces")
            {
                RegEXPattern += @"[^\" + '"' + "]+" + postf + "$";
                RegEXPatternWithoutPrefix += @"[^\" + '"' + "]+" + postf + "$";
                RegEXPatternWithoutSufix += @"[^\" + '"' + "]+" + "$";
                RegEXPatternWithoutPrefixSufix += @"[^\" + '"' + "]+" + "$";
                return;
            }
            if (Type == "string")
            {
                string adder = "";
                if (Prefix.Length > 0) adder = @"\d";
                RegEXPattern += @"([" + adder + @"a-zA-Z]|_)+(\.*_*[\da-zA-Z]*)*" + numPostMod + postf + "$";
                RegEXPatternWithoutPrefix += @"([" + adder + @"a-zA-Z]|_)+(\.*_*[\da-zA-Z]*)*" + numPostMod + postf + "$";
                RegEXPatternWithoutSufix += @"([" + adder + @"a-zA-Z]|_)+(\.*_*[\da-zA-Z]*)*" + numPostMod + "$";
                RegEXPatternWithoutPrefixSufix += @"([" + adder + @"a-zA-Z]|_)+(\.*_*[\da-zA-Z]*)*" + numPostMod + "$";
                return;
            }
            if (Type == "fixed" && Bytes < 0)
            {
                RegEXPattern += pref + "*" + numPostMod + postf + "$";
                RegEXPatternWithoutPrefix += pref + "*" + numPostMod + postf + "$";
                RegEXPatternWithoutSufix += pref + "*" + numPostMod + "$";
                RegEXPatternWithoutPrefixSufix += pref + "*" + numPostMod + "$";
                return;
            }

            int bn = Bytes * 8;
            string bnum = "%[0,1]{1," + bn + "}";
            int hn = Bytes * 2;
            string hnum = @"\$[\dA-Fa-f]{1," + hn + "}";
            string dnum = "0*" + dnums[Bytes - 1];
            //RegEXPattern += "[" + bnum + "-" + hnum + "-" + dnum + "]" + postf;
            RegEXPattern += "(" + bnum + "|" + hnum + "|" + dnum + ")" + numPostMod + postf + "$";
            RegEXPatternWithoutPrefix += "(" + bnum + "|" + hnum + "|" + dnum + ")" + numPostMod + postf + "$";
            RegEXPatternWithoutSufix += "(" + bnum + "|" + hnum + "|" + dnum + ")" + numPostMod + "$";
            RegEXPatternWithoutPrefixSufix += "(" + bnum + "|" + hnum + "|" + dnum + ")" + numPostMod + "$";
        }
        public static ArgsTypes Exists(string arg, ArgsTypes[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                if (arg == args[i].Name) return args[i];
            }
            return null;
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
