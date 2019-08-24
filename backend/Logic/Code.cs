using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace SMWControlibBackend.Logic
{
    public class Code
    {
        private ArgsTypes[] argsTypes;
        public List<int> ArgsStyles
        {
            get
            {
                List<int> styles = new List<int>();

                foreach(ArgsTypes at in argsTypes)
                {
                    if (!styles.Contains(at.Group.Style))
                    {
                        styles.Add(at.Group.Style);
                    }
                }

                return styles;
            }
        }
        private Dictionary<string, Dictionary<string, 
            Dictionary<string, Command[]>>> commands;
        public List<int> CommandsStyles
        {
            get
            {
                List<int> styles = new List<int>();

                foreach(string s in commands.Keys)
                {
                    foreach (string s2 in commands[s].Keys)
                    {
                        foreach(string s3 in commands[s][s2].Keys)
                        {
                            foreach(Command c in commands[s][s2][s3])
                            {
                                if (!styles.Contains(c.Group.Style))
                                {
                                    styles.Add(c.Group.Style);
                                }
                            }
                        }
                    }
                }

                return styles;
            }
        }
        private Label[] labels;
        public List<int> LabelsStyles
        {
            get
            {
                List<int> styles = new List<int>();

                foreach (Label at in labels)
                {
                    if (!styles.Contains(at.Group.Style))
                    {
                        styles.Add(at.Group.Style);
                    }
                }

                return styles;
            }
        }
        private Group[] groups;
        public Group[] Groups
        {
            get
            {
                return groups.ToList().ToArray();
            }
        }
        public Group ErrorGroup, CommentGroup, DefineGroup, DefineArgsGroup, DefaultGroup;
        private Dictionary<string, Define> defines;
        private Dictionary<int, List<Error>> errors;
        public event Action<Dictionary<int, List<Error>>> ErrorsAdded;
        private bool newErrors = false;
        private List<string> autoCompleteTexts;
        private string[] autoCompleteTextsArray;
        private bool definesDetected = false;
        public string[] AutoCompleteTexts
        {
            get
            {
                if (autoCompleteTextsArray == null)
                    autoCompleteTextsArray = autoCompleteTexts.ToArray();
                return autoCompleteTextsArray;
            }
        }

        public Code(string _ArgsTypes,string _Commands, string _Groups)
        {
            autoCompleteTexts = new List<string>();
            groups = Group.GetGroups(_Groups);
            argsTypes = ArgsTypes.GetArgsTypes(_ArgsTypes, groups);
            commands = Command.GetCommands(_Commands, argsTypes, groups);
            labels = new Label[4];
            labels[0] = new NormalLabel(groups, "Label");
            labels[1] = new SubLabel(groups, "Special Label");
            labels[2] = new PlusLabel(groups, "Special Label");
            labels[3] = new MinusLabel(groups, "Special Label");
            defines = new Dictionary<string, Define>();
            errors = new Dictionary<int, List<Error>>();
            DefaultGroup = Group.Default;
            ErrorGroup = Group.FindGroup(groups, "Error");
            CommentGroup = Group.FindGroup(groups, "Comment");
            DefineGroup = Group.FindGroup(groups, "Define");
            DefineArgsGroup = Group.FindGroup(groups, "Define Arg");
        }

        public List<string> GetActiveDefines()
        {
            List<string> actDef = new List<string>();
            foreach (Define d in defines.Values)
            {
                foreach (Tuple<int, int, string> t in d.OthersPositions)
                {

                }
            }
            return actDef;
        }

        public List<string> FilterAutocompleteWords(string word)
        {
            List<string> at = new List<string>();

            foreach (string s  in autoCompleteTexts)
            {
                if (Regex.IsMatch(s, word, RegexOptions.IgnoreCase)) 
                {
                    at.Add(s);
                }
            }
            return at;
        }

        public void ImportDefines()
        {
            string s = File.ReadAllText(@".\ASM\Defines.asm");

            string[] newLines = s.Replace("\r\n", "\n").Split('\n');
            int linelen = newLines.Length;
            if (s == "\r\n" || s == "\n")
            {
                linelen = 1;
            }

            for (int i = 0; i < newLines.Length; i++)
            {
                GetPointersFromLine(i, newLines[i]);
            }

            errors.Clear();
            List<Tuple<int, int, string>> newsPos;
            foreach (Define d in defines.Values)
            {
                newsPos = new List<Tuple<int, int, string>>();
                foreach (Tuple<int, int, string> t in d.OthersPositions)
                {
                    newsPos.Add(new Tuple<int, int, string>(t.Item1 - linelen - 1,
                        t.Item2, t.Item3));
                }
                d.OthersPositions.Clear();
                foreach (Tuple<int, int, string> t in newsPos)
                {
                    d.OthersPositions.Add(t);
                }
            }
        }

        public void DeleteLinesAt(int lineIndex, int deltaLines)
        {
            int lineN = lineIndex;

            if (deltaLines > 0)
            {
                for (int i = lineN; i <= lineN + deltaLines; i++)
                {
                    removeDefinesAtPosition(i);
                    removeErrorsAtPosition(i);
                }
                List<Tuple<int, int, string>> removeList, news;

                foreach (Define d in defines.Values)
                {
                    removeList =
                        new List<Tuple<int, int, string>>();
                    news =
                        new List<Tuple<int, int, string>>();
                    foreach (Tuple<int, int, string> t in d.OthersPositions)
                    {
                        if (t.Item1 > lineN)
                        {
                            removeList.Add(t);
                            news.Add(new Tuple<int, int, string>(
                                t.Item1 - deltaLines,
                                t.Item2,
                                t.Item3));
                        }
                    }
                    foreach (Tuple<int, int, string> t in removeList)
                    {
                        d.OthersPositions.Remove(t);
                    }
                    foreach (Tuple<int, int, string> t in news)
                    {
                        d.OthersPositions.Add(t);
                    }
                }

                List<int> removeList2 = new List<int>();
                List<KeyValuePair<int, List<Error>>> news2 =
                    new List<KeyValuePair<int, List<Error>>>();
                newErrors = false;

                foreach (KeyValuePair<int, List<Error>> kvp in errors)
                {
                    if (kvp.Key > lineN)
                    {
                        foreach (Error err in kvp.Value)
                        {
                            err.Line -= deltaLines;
                        }
                        removeList2.Add(kvp.Key);
                        news2.Add(
                            new KeyValuePair<int, List<Error>>
                            (kvp.Key - deltaLines, kvp.Value));
                        newErrors = true;
                    }
                }
                foreach (int k in removeList2)
                {
                    errors.Remove(k);
                }

                foreach (KeyValuePair<int, List<Error>> kvvp in news2)
                {
                    errors.Add(kvvp.Key, kvvp.Value);
                }
            }
        }

        public void ClearFlags()
        {
            newErrors = false;
            definesDetected = false;
        }

        public List<int> EndAnalysis()
        {
            List<int> ret = definePossibleFix();
            if (newErrors)
            {
                ErrorsAdded?.Invoke(errors);
            }
            return ret;
        }

        private List<int> definePossibleFix()
        {
            List<int> testList = new List<int>();
            if (definesDetected)
            {
                foreach (KeyValuePair<int, List<Error>> kvp1 in errors)
                {
                    foreach (Error errr in kvp1.Value)
                    {
                        if (errr.Code == ErrorCode.DefineNotFound)
                        {
                            newErrors = true;
                            testList.Add(kvp1.Key);
                            break;
                        }
                    }
                }
            }
            return testList;
        }

        public void AddLinesAt(int lineIndex, int linePosition, int startIndex, string adder)
        {
            string[] newLines = adder.Replace("\r\n", "\n").Split('\n');
            int linelen = newLines.Length;
            if (adder == "\r\n" || adder == "\n")
            {
                linelen = 1;
            }
            int linePos = lineIndex;
            int pos = linePosition;

            Match m = Regex.Match(adder, @"\n");

            List<Tuple<int, int, string>> removeList;
            List<Tuple<int, int, string>> news;
            List<int> removeList2 = new List<int>();
            List<KeyValuePair<int, List<Error>>> news2 =
                new List<KeyValuePair<int, List<Error>>>();
            bool mustRemove;
            newErrors = false;
            if (m.Success)
            {

                foreach (Define d in defines.Values)
                {
                    removeList =
                        new List<Tuple<int, int, string>>();
                    news =
                        new List<Tuple<int, int, string>>();
                    foreach (Tuple<int, int, string> t in d.OthersPositions)
                    {
                        if (t.Item1 > linePos)
                        {
                            removeList.Add(t);
                            news.Add(new Tuple<int, int, string>(
                                t.Item1 + linelen,
                                t.Item2,
                                t.Item3));
                        }
                        else if (t.Item1 == linePos && t.Item2 >= startIndex - pos)
                        {
                            removeList.Add(t);
                            news.Add(new Tuple<int, int, string>(
                                t.Item1 + linelen,
                                t.Item2 - (startIndex - pos),
                                t.Item3));
                        }
                    }
                    foreach (Tuple<int, int, string> t in removeList)
                    {
                        d.OthersPositions.Remove(t);
                    }
                    foreach (Tuple<int, int, string> t in news)
                    {
                        d.OthersPositions.Add(t);
                    }
                }

                foreach (KeyValuePair<int, List<Error>> kvp in errors)
                {
                    if (kvp.Key > linePos)
                    {
                        foreach (Error err in kvp.Value)
                        {
                            err.Line += linelen;
                        }
                        removeList2.Add(kvp.Key);
                        news2.Add(
                            new KeyValuePair<int, List<Error>>
                            (kvp.Key + linelen, kvp.Value));
                        newErrors = true;
                    }
                    else if (kvp.Key == linePos)
                    {
                        mustRemove = false;
                        foreach (Error err in kvp.Value)
                        {
                            if (err.Start >= startIndex - pos)
                            {
                                mustRemove = true;
                                err.Line += linelen;
                                err.Start -= (startIndex - pos);
                            }
                        }
                        if (mustRemove)
                        {
                            newErrors = true;
                            removeList2.Add(kvp.Key);
                            news2.Add(
                                new KeyValuePair<int, List<Error>>
                                (kvp.Key + linelen, kvp.Value));
                        }
                    }
                }
                foreach (int k in removeList2)
                {
                    errors.Remove(k);
                }

                foreach (KeyValuePair<int, List<Error>> kvvp in news2)
                {
                    errors.Add(kvvp.Key, kvvp.Value);
                }
            }
            else
            {
                foreach (Define d in defines.Values)
                {
                    removeList =
                        new List<Tuple<int, int, string>>();
                    news =
                        new List<Tuple<int, int, string>>();
                    foreach (Tuple<int, int, string> t in d.OthersPositions)
                    {
                        if (t.Item1 == linePos && t.Item2 >= startIndex - pos)
                        {
                            removeList.Add(t);
                            news.Add(new Tuple<int, int, string>(
                                t.Item1,
                                t.Item2 + adder.Length,
                                t.Item3));
                        }
                    }
                    foreach (Tuple<int, int, string> t in removeList)
                    {
                        d.OthersPositions.Remove(t);
                    }
                    foreach (Tuple<int, int, string> t in news)
                    {
                        d.OthersPositions.Add(t);
                    }
                }

                foreach (KeyValuePair<int, List<Error>> kvp in errors)
                {
                    if (kvp.Key == linePos)
                    {
                        foreach (Error err in kvp.Value)
                        {
                            if (err.Start >= startIndex - pos)
                            {
                                err.Start += adder.Length;
                                newErrors = true;
                            }
                        }
                    }
                }
            }
        }

        const string startPattern = @"^(\ |\t)*(:(\ |\t)*)+";
        const string startSpaces = @"^(\ |\t)*";
        const string separatorPattern = @"(\ |\t)+:(\ |\t)+";
        const string endPattern = @"(\ |\t)+(:(\ |\t)*)*$";
        const string endSpaces = @"(\ |\t)*$";
        public List<CodePointer> GetPointersFromLine(int lineIndex, string line)
        {
            List<CodePointer> pointers = new List<CodePointer>();

            CodePointer cp = new CodePointer
            {
                Start = 0,
                End = line.Length - 1,
                Code = line,
                Group = DefaultGroup
            };
            pointers.Add(cp);

            string[] splittedLine = line.Split(';');
            if (splittedLine == null || splittedLine.Length <= 0) return pointers;

            string lineCode = splittedLine[0];
            string comment = line.Substring(lineCode.Length);

            Match stMatch = Regex.Match(lineCode, startPattern);

            if (stMatch.Success) 
            {
                cp = new CodePointer
                {
                    Start = stMatch.Index,
                    End = stMatch.Index + stMatch.Length - 1,
                    Code = stMatch.ToString(),
                    Group = ErrorGroup
                };
                pointers.Add(cp);
                newErrors = true;
            }
            else
            {
                stMatch = Regex.Match(lineCode, startSpaces);
            }

            string shorterLineCode = lineCode.Substring(stMatch.Length).Replace('\r', '\n').Replace("\n", "");
            Match m = Regex.Match(shorterLineCode, endSpaces);
            if (m.Success) 
            {
                int ll = shorterLineCode.Length - m.Length;
                if (ll < 0) ll = 0;
                shorterLineCode =
                    shorterLineCode.Substring(0, ll);
            }

            CodePointer[] cmds = CodePointer.Split(shorterLineCode, separatorPattern);
            removeDefinesAtPosition(lineIndex);
            removeErrorsAtPosition(lineIndex);

            if (cmds != null && cmds.Length > 0)
            {

                List<CodePointer> tmps;

                for (int j = 0; j < cmds.Length; j++)
                {
                    cmds[j].Move(stMatch.Length);
                    tmps = getPointersFromMiniLine(cmds[j], lineIndex);
                    foreach (CodePointer cp1 in tmps)
                    {
                        pointers.Add(cp1);
                    }
                }
            }

            Match endMatch = Regex.Match(lineCode, endPattern);

            if (endMatch.Success)
            {
                cp = new CodePointer
                {
                    Start = endMatch.Index,
                    End = endMatch.Index + endMatch.Length - 1,
                    Code = endMatch.ToString(),
                    Group = ErrorGroup
                };
                pointers.Add(cp);
                newErrors = true;
            }

            cp = new CodePointer
            {
                Start = lineCode.Length,
                End = line.Length - 1,
                Code = comment,
                Group = CommentGroup
            };
            pointers.Add(cp);

            return pointers;
        }

        private List<CodePointer> getPointersFromMiniLine(CodePointer pointer, int lineIndex)
        {
            List<CodePointer> pointers = new List<CodePointer>();

            CodePointer[] cmds = CodePointer.Split(pointer.Code, @":(\ |\t)*");
            if (cmds == null || cmds.Length <= 0) return pointers;

            List<CodePointer> tmps;
            CodePointer cp;
            string rep;
            string nextChar;

            
            for (int i = 0; i < cmds.Length; i++)
            {
                cmds[i].Move(pointer.Start);

                tmps = getPointersFromDefineSignature(cmds[i], lineIndex);
                if (tmps.Count > 0) 
                {
                    if (Define.possibleError != null)
                    {
                        tmps[1].Group = ErrorGroup;
                        if (!errors.ContainsKey(lineIndex))
                        {
                            errors.Add(lineIndex, new List<Error>());
                        }
                        errors[lineIndex].Add(Define.possibleError);
                        newErrors = true;
                    }
                }
                
                
                if (tmps.Count <= 0)
                {
                    rep = Define.Replace(defines, cmds[i].Code, lineIndex, cmds[i].Start);
                    cp = new CodePointer
                    {
                        Start = cmds[i].Start,
                        End = cmds[i].Start + rep.Length - 1,
                        Code = rep
                    };
                    
                    
                    tmps = getPointersFromCommands(cp);
                    if (tmps.Count <= 0)
                    {
                        nextChar = "";
                        if (pointer.Code.Length + pointer.Start > cmds[i].End + 1 &&
                            pointer.Code[cmds[i].End - cmds[i].Start + 1] == ':')
                        {
                            nextChar = ":";
                        }

                        tmps = getPointersFromLabels(cp, nextChar);
                    }
                }

                
                if (tmps.Count <= 0) 
                {
                    tmps = new List<CodePointer>();
                    cmds[i].Group = ErrorGroup;
                    tmps.Add(cmds[i]);

                    if (!errors.ContainsKey(lineIndex))
                    {
                        errors.Add(lineIndex, new List<Error>());
                    }
                    errors[lineIndex].Add(new Error(lineIndex, cmds[i].Start,
                        cmds[i].Code, ErrorCode.InvalidDefineSignature, cmds[i].Code));

                    newErrors = true;
                }

                foreach(CodePointer cp1 in tmps)
                {
                    pointers.Add(cp1);
                }
            }

            return pointers;
        }
        private List<CodePointer> getPointersFromLabels(CodePointer cmd, string nextChar)
        {
            List<CodePointer> pointers = new List<CodePointer>();
            List<CodePointer> tmps = new List<CodePointer>();
            string lab = cmd.Code + nextChar;
            foreach (Label l in labels)
            {
                if (l.SyntaxIsCorrect(lab))
                {
                    cmd.Group = l.Group;
                    tmps.Add(cmd);
                    break;
                }
            }

            if (tmps != null && tmps.Count > 0) 
            {
                IEnumerator<CodePointer> ienum = Define.definesFounded.GetEnumerator();
                CodePointer cp3;
                string newCMD;
                foreach (CodePointer cp in tmps)
                {
                    ienum.Reset();
                    newCMD = cp.Code;
                    foreach (CodePointer cp2 in Define.replaced)
                    {
                        ienum.MoveNext();
                        if (cp.End >= cp2.End && cp.Start <= cp2.Start)
                        {
                            newCMD = newCMD.Replace(cp2.Code, ienum.Current.Code);
                        }
                    }
                    cp3 = new CodePointer
                    {
                        Start = cp.Start,
                        End = cp.Start + newCMD.Length - 1,
                        Code = newCMD,
                        Group = cp.Group
                    };
                    pointers.Add(cp3);
                }
            }

            return pointers;
        }
        private List<CodePointer> getPointersFromCommands(CodePointer cmd)
        {
            List<CodePointer> pointers = new List<CodePointer>();

            string scmd = cmd.Code.Split(';')[0];
            scmd = scmd.ToLower();
            scmd = scmd.Replace("\t", " ");
            string name = scmd.Split(' ')[0];

            if (!commands.ContainsKey(name)) return pointers;

            scmd = scmd.Replace(" ", "");

            scmd = scmd.Remove(0, name.Length);

            Command foundedC;
            List<CodePointer> tmps = null;

            string prefix = "";
            Match m = null;

            foreach(string s in commands[name].Keys)
            {
                if (s != "NULL")
                {
                    m = Regex.Match(scmd, s);
                    if (m.Success)
                    {
                        prefix = s;
                        break;
                    }
                }
            }

            if (prefix == "") prefix = "NULL";

            string sufix = "";

            if(commands[name].ContainsKey(prefix))
            {
                foreach (string s in commands[name][prefix].Keys)
                {
                    if (s != "NULL")
                    {
                        m = Regex.Match(scmd, s);
                        if (m.Success)
                        {
                            sufix = s;
                            break;
                        }
                    }
                }

                if (sufix == "") sufix = "NULL";
                else
                    scmd = scmd.Remove(m.Index, m.Length);


                if (commands[name][prefix].ContainsKey(sufix))
                {
                    foreach (Command c in commands[name][prefix][sufix])
                    {
                        if (c.IsCorrect(scmd))
                        {
                            tmps = c.GetPointers(cmd.Start, cmd.Code).ToList();
                            foundedC = c;
                            break;
                        }
                    }
                }
                else
                {
                    return pointers;
                }
            }
            else
            {
                return pointers;
            }

            if (tmps != null)
            {
                IEnumerator<CodePointer> ienum = Define.definesFounded.GetEnumerator();
                CodePointer cp3;
                string newCMD;
                foreach (CodePointer cp in tmps) 
                {
                    ienum.Reset();
                    newCMD = cp.Code;
                    foreach (CodePointer cp2 in Define.replaced)
                    {
                        ienum.MoveNext();
                        if (cp.End >= cp2.End && cp.Start <= cp2.Start) 
                        {
                            newCMD = newCMD.Replace(cp2.Code, ienum.Current.Code);
                        }
                    }
                    cp3 = new CodePointer
                    {
                        Start = cp.Start,
                        End = cp.Start + newCMD.Length - 1,
                        Code = newCMD,
                        Group = cp.Group
                    };
                    pointers.Add(cp3);
                }
            }

            return pointers;
        }

        private List<CodePointer> getPointersFromDefineSignature(CodePointer cmd, int lineIndex)
        {
            Define d;
            List<CodePointer> pointers = new List<CodePointer>();
            if (Define.SignatureIsCorrect(cmd.Code))
            {
                d = Define.GetDefine(defines, cmd.Code, lineIndex, cmd.Start);

                if (defines.ContainsKey(d.Name))
                {
                    defines[d.Name].OthersPositions.Add(d.OthersPositions[0]);
                }
                else
                {
                    defines.Add(d.Name, d);
                }
                pointers = Define.GetPointers(cmd.Code, cmd.Start);
                if (pointers != null && pointers.Count >= 2) 
                {
                    pointers[0].Group = DefineGroup;
                    pointers[1].Group = DefineArgsGroup;
                }
            }
            return pointers;
        }
        private void removeErrorsAtPosition(int lineIndex)
        {
            if (errors.ContainsKey(lineIndex))
            {
                errors.Remove(lineIndex);
                newErrors = true;
            }
        }

        private void removeDefinesAtPosition(int lineIndex)
        {
            List<Tuple<int, int, string>> removeList;
            List<string> removeList2 = new List<string>();
            foreach (KeyValuePair<string, Define> kvp in defines)
            {
                removeList = new List<Tuple<int, int, string>>();
                foreach (Tuple<int, int, string> t in kvp.Value.OthersPositions)
                {
                    if (t.Item1 == lineIndex)
                    {
                        removeList.Add(t);
                    }
                }
                foreach (Tuple<int, int, string> t in removeList)
                {
                    kvp.Value.OthersPositions.Remove(t);
                }
                if (kvp.Value.OthersPositions.Count <= 0)
                {
                    removeList2.Add(kvp.Key);
                }
            }
            foreach (string saux in removeList2)
            {
                defines.Remove(saux);
            }
        }
    }
}
