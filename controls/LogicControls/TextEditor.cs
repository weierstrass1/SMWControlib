using AutocompleteMenuNS;
using ScintillaNET;
using SMWControlibBackend.Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SMWControlibControls.LogicControls
{
    public partial class TextEditor : Scintilla
    {
        private static ArgsTypes[] argTypes;
        private static Dictionary<string, SMWControlibBackend.Logic.Command[]> commands;
        private static SMWControlibBackend.Logic.Label[] labels;
        private static SMWControlibBackend.Logic.Group[] groups;
        private static SMWControlibBackend.Logic.Group errorGroup, commentGroup,
            defineGroup, defineArgsGroup;
        private Dictionary<string, Define> defines;
        private Dictionary<int, List<Error>> errors;
        private UndoRedoDynamicArray<UndoRedoStruct> undoRedoList;
        public bool CanUndoRedo = false;
        public event Action<Dictionary<int, List<Error>>> ErrorsAdded;

        private const int bookmarkMargin = 1; // Conventionally the symbol margin
        private const int bookmarkMarker = 3;

        const string font = "Consolas";
        const int size = 12;
        Color backColor = Color.FromArgb(232, 232, 248);
        AutocompleteMenu autocom;
        public TextEditor()
        {
            InitializeComponent();
            undoRedoList = new UndoRedoDynamicArray<UndoRedoStruct>(20, 20);
            errors = new Dictionary<int, List<Error>>();
            defines = new Dictionary<string, Define>();
            CaretForeColor = Color.White;
            Styles[Style.Default].Font = font;
            Styles[Style.Default].ForeColor = Color.FromArgb(224, 224, 248);
            Styles[Style.Default].Bold = true;
            Styles[Style.Default].BackColor = backColor;
            Styles[Style.Default].Size = size;
            StyleClearAll();
            try
            {
                autocom = new AutocompleteMenu
                {
                    SearchPattern = @"\!",
                    TargetControlWrapper = new ScintillaWrapper(this),
                    AutoPopup = true,
                    LeftPadding = 0,
                };               
                
                groups = SMWControlibBackend.Logic.Group.GetGroups(@".\CSVs\Syntax\groups.csv");
                argTypes = ArgsTypes.GetArgsTypes(@".\CSVs\Syntax\args.csv", groups);
                commands = SMWControlibBackend.Logic.Command.GetCommands(@".\CSVs\Syntax\commands.csv", argTypes, groups);
                foreach (SMWControlibBackend.Logic.Command[] val in commands.Values)
                {
                    foreach(SMWControlibBackend.Logic.Command v in val)
                    {
                        Styles[v.Group.Style].ForeColor = Color.FromArgb(16, 32, 176);
                        Styles[v.Group.Style].BackColor = backColor;
                    }
                }
                foreach(ArgsTypes v in argTypes)
                {
                    Styles[v.Group.Style].ForeColor = Color.FromArgb(128,128,144);//(224, 200, 48);
                }
                labels = new SMWControlibBackend.Logic.Label[4];
                labels[0] = new NormalLabel(groups, "Label");
                Styles[labels[0].Group.Style].ForeColor = Color.FromArgb(35, 25, 75);
                labels[1] = new SubLabel(groups, "Special Label");
                Styles[labels[1].Group.Style].ForeColor = Color.FromArgb(70, 50, 150);
                labels[2] = new PlusLabel(groups, "Special Label");
                labels[3] = new MinusLabel(groups, "Special Label");
                errorGroup = SMWControlibBackend.Logic.Group.FindGroup(groups, "Error");
                Styles[errorGroup.Style].ForeColor = Color.FromArgb(176,16, 32);
                commentGroup = SMWControlibBackend.Logic.Group.FindGroup(groups, "Comment");
                Styles[commentGroup.Style].ForeColor = Color.FromArgb(16, 102, 64);
                Styles[commentGroup.Style].BackColor = backColor;
                defineGroup = SMWControlibBackend.Logic.Group.FindGroup(groups, "Define");
                Styles[defineGroup.Style].ForeColor = Color.FromArgb(70, 50, 150);
                Styles[defineGroup.Style].BackColor = backColor;
                defineArgsGroup = SMWControlibBackend.Logic.Group.FindGroup(groups, "Define Arg");
                Styles[defineArgsGroup.Style].ForeColor = Color.FromArgb(224, 200, 48);
                Styles[defineArgsGroup.Style].BackColor = backColor;

                importDefines();
                CanUndoRedo = true;
            }
            catch
            {
            }

            BeforeInsert += beforeInsert;
            Insert += insert;

            BeforeDelete += beforeDelete;
            Delete += delete;

            CharAdded += charAdded;
            autocom.Selected += selected;
            ClearCmdKey(Keys.Control | Keys.Z);
            ClearCmdKey(Keys.Control | Keys.Y);
            ClearCmdKey(Keys.Control | Keys.Q);
            ClearCmdKey(Keys.Control | Keys.W);
            ClearCmdKey(Keys.Control | Keys.E);
            ClearCmdKey(Keys.Control | Keys.R);
            ClearCmdKey(Keys.Control | Keys.T);
            ClearCmdKey(Keys.Control | Keys.Y);
            ClearCmdKey(Keys.Control | Keys.U);
            ClearCmdKey(Keys.Control | Keys.I);
            ClearCmdKey(Keys.Control | Keys.O);
            ClearCmdKey(Keys.Control | Keys.P);
            ClearCmdKey(Keys.Control | Keys.A);
            ClearCmdKey(Keys.Control | Keys.S);
            ClearCmdKey(Keys.Control | Keys.D);
            ClearCmdKey(Keys.Control | Keys.F);
            ClearCmdKey(Keys.Control | Keys.G);
            ClearCmdKey(Keys.Control | Keys.H);
            ClearCmdKey(Keys.Control | Keys.J);
            ClearCmdKey(Keys.Control | Keys.K);
            ClearCmdKey(Keys.Control | Keys.B);
            ClearCmdKey(Keys.Control | Keys.N);
            Margin margin = Margins[bookmarkMargin];
            margin.Width = 16;
            margin.Sensitive = true;
            margin.Type = MarginType.Symbol;
            margin.Mask = Marker.MaskAll;
            margin.Cursor = MarginCursor.Arrow;
            Margins[0].Type = MarginType.RightText;
            Margins[0].Width = 35;

            Styles[255].ForeColor = Color.FromArgb(224, 224, 248);
            Styles[255].BackColor = Color.FromArgb(80, 80, 128);

            Marker marker = Markers[bookmarkMarker];
            marker.Symbol = MarkerSymbol.SmallRect;
            marker.SetBackColor(Color.FromArgb(105, 75, 224));
            marker.SetForeColor(Color.FromArgb(70, 50, 150));
            TextChanged += textChanged;
        }

        private int maxLineNumberCharLength = 1;
        private void textChanged(object sender, EventArgs e)
        {
            // Did the number of characters in the line number display change?
            // i.e. nnn VS nn, or nnnn VS nn, etc...
            var maxline = Lines.Count.ToString().Length;
            if (maxline == maxLineNumberCharLength)
                return;

            // Calculate the width required to display the last line number
            // and include some padding for good measure.
            const int padding = 2;
            Margins[0].Width = TextWidth(Style.LineNumber, new string('9', maxline + 1)) + padding;
            maxLineNumberCharLength = maxline;
        }

        private void updateLineNumbers(int startingAtLine)
        {
            // Starting at the specified line index, update each
            // subsequent line margin text with a hex line number.
            for (int i = startingAtLine; i < Lines.Count; i++)
            {
                Lines[i].MarginStyle = 255;
                Lines[i].MarginText = "" + i;
            }
        }

        private void delete(object sender, ModificationEventArgs e)
        {
            if (e.LinesAdded != 0)
                updateLineNumbers(LineFromPosition(e.Position));
            int deltaLines = curLinesLen - Lines.Count;
            Line curL = Lines[CurrentLine];
            string line = curL.Text;
            int lineInd = curL.Position;
            int linel = line.Length;
            int lineN = LineFromPosition(lineInd);

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

            newError = false;
            definesDetected = false;
            highlightLine(line, lineInd, linel);

            definePossibleFix();
            if (newError || newErrors)
            {
                ErrorsAdded?.Invoke(errors);
            }
        }

        private void definePossibleFix()
        {
            if (definesDetected)
            {
                List<int> testList = new List<int>();
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
                Line curL;
                string line;
                int lineInd;
                int linel;
                foreach (int tl in testList)
                {
                    curL = Lines[tl];
                    line = curL.Text;
                    lineInd = curL.Position;
                    linel = line.Length;
                    highlightLine(line, lineInd, linel);
                }
            }
        }

        private void deleteBookMark(int Line)
        {
            Line line = Lines[Line];
            line.MarkerDelete(bookmarkMarker);
        }

        private void addBookMark(int Line)
        {
            Line line = Lines[Line];
            line.MarkerAdd(bookmarkMarker);
        }

        public void SuperSnescriptUndo()
        {
            UndoRedoStruct e = undoRedoList.Undo();
            if (e == default(UndoRedoStruct)) return;
            CanUndoRedo = false;
            if (e.UndoRedoAction == UndoRedoAction.Delete)
            {
                InsertText(e.Position, e.Text);
                CurrentPosition = e.Position + e.Text.Length;
                SelectionStart = CurrentPosition;
                SelectionEnd = SelectionStart;
            }
            else if(e.UndoRedoAction == UndoRedoAction.Insert)
            {
                DeleteRange(e.Position, e.Text.Length);
                CurrentPosition = e.Position;
                SelectionStart = CurrentPosition;
                SelectionEnd = SelectionStart;
            }
            CanUndoRedo = true;
        }

        private void selected(object sender, SelectedEventArgs e)
        {
            DeleteRange(adpos, curpos - adpos);
            adding = false;
        }

        public void OpenTab()
        {
            List<AutocompleteItem> items = new List<AutocompleteItem>();

            foreach (string def in defines.Keys)
            {
                items.Add(new SnippetAutocompleteItem(def));
            }
            autocom.SetAutocompleteItems(items);
            autocom.CaptureFocus = true;
        }

        StringBuilder sb = new StringBuilder();
        bool adding = false;
        int adpos = 0;
        int curpos = 0;

        private void charAdded(object sender, CharAddedEventArgs e)
        {
            if(e.Char=='!')
            {
                sb.Clear();
                sb.Append(@"^\!");
                adding = true;
                adpos = CurrentPosition - 1;
            }
            else if(adding)
            {
                if (e.Char == '\n')
                {
                    adding = false;
                }
                sb.Append("" + (char)e.Char);
            }
            curpos = CurrentPosition;
            autocom.SearchPattern = sb.ToString();
            List<AutocompleteItem> items = new List<AutocompleteItem>();

            foreach (string def in defines.Keys)
            {
                if (Regex.Match(def, autocom.SearchPattern).Success)
                    items.Add(new SnippetAutocompleteItem(def));
            }
            autocom.SetAutocompleteItems(items);
            autocom.CaptureFocus = false;
            autocom.TargetControlWrapper.TargetControl.Location = new Point(0, 0);
            autocom.Show(this, false);
        }

        int curLinesLen = 0;
        private void beforeDelete(object sender, BeforeModificationEventArgs e)
        {
            if(CanUndoRedo)
            {
                UndoRedoStruct ne = new UndoRedoStruct(e, UndoRedoAction.Delete);
                undoRedoList.Add(ne);
            }
            if (adding && e.Text.Length == 1)
            {
                if (sb.Length > 3)
                {
                    sb.Remove(sb.Length - 1, 1);
                    autocom.SearchPattern = sb.ToString();
                    List<AutocompleteItem> items = new List<AutocompleteItem>();

                    foreach (string def in defines.Keys)
                    {
                        if (Regex.Match(def, autocom.SearchPattern).Success)
                            items.Add(new SnippetAutocompleteItem(def));
                    }
                    curpos = CurrentPosition;
                    autocom.SetAutocompleteItems(items);
                    autocom.CaptureFocus = false;
                    autocom.Show(this, true);
                }
                else
                {
                    autocom.Close();
                }
            }
            curLinesLen = Lines.Count;
        }

        private void insert(object sender, ModificationEventArgs e)
        {
            if (e.LinesAdded != 0)
                updateLineNumbers(LineFromPosition(e.Position));
            string[] newLines = e.Text.Replace("\r", "").Split('\n');
            int linePos = LineFromPosition(e.Position);
            int pos = Lines[linePos].Position;


            int endL = Lines.Count - 1;
            if (endL < 0) return;

            Line curL;
            string line;
            int lineInd;
            int linel;
            newError = false;
            definesDetected = false;
            for (int i = linePos; i < linePos + newLines.Length; i++)
            {
                curL = Lines[i];
                line = curL.Text;
                lineInd = curL.Position;
                linel = line.Length;
                highlightLine(line, lineInd, linel);
            }
            definePossibleFix();

            if (newError || newErrors)
            {
                ErrorsAdded?.Invoke(errors);
            }
        }

        bool newErrors = false;
        private void beforeInsert(object sender, BeforeModificationEventArgs e)
        {
            if (CanUndoRedo)
            {
                UndoRedoStruct ne = new UndoRedoStruct(e, UndoRedoAction.Insert);
                undoRedoList.Add(ne);
            }
            string[] newLines = e.Text.Replace("\r\n", "\n").Split('\n');
            int linelen = newLines.Length;
            if (e.Text == "\r\n" || e.Text == "\n")
            {
                linelen = 1;
            }
            int linePos = LineFromPosition(e.Position);
            int pos = Lines[linePos].Position;

            Match m = Regex.Match(e.Text, @"\n");

            List<Tuple<int, int, string>> removeList;
            List<Tuple<int, int, string>> news;
            List<int> removeList2 = new List<int>();
            List<KeyValuePair<int, List<Error>>> news2 =
                new List<KeyValuePair<int, List<Error>>>();
            bool mustRemove = false;
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
                        else if (t.Item1 == linePos && t.Item2 >= e.Position - pos)
                        {
                            removeList.Add(t);
                            news.Add(new Tuple<int, int, string>(
                                t.Item1 + linelen,
                                t.Item2 - (e.Position - pos),
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
                        foreach(Error err in kvp.Value)
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
                            if(err.Start >= e.Position - pos)
                            {
                                mustRemove = true;
                                err.Line += linelen;
                                err.Start -= (e.Position - pos);
                            }
                        }
                        if(mustRemove)
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
                        if (t.Item1 == linePos && t.Item2 >= e.Position - pos)
                        {
                            removeList.Add(t);
                            news.Add(new Tuple<int, int, string>(
                                t.Item1,
                                t.Item2 + e.Text.Length,
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
                            if (err.Start >= e.Position - pos)
                            {
                                err.Start += e.Text.Length;
                                newErrors = true;
                            }
                        }
                    }
                }
            }
        }

        const string startSpacesPattern = @"^(\ |\t)*";
        const string endSpacesPattern = @"(\ |\t)*$";
        const string separatorPattern = @"(\ |\t)+:(\ |\t)+";

        private void importDefines()
        {
            string s = File.ReadAllText(@".\ASM\Defines.asm");
            AppendTextWithHighlight(s);
            errors.Clear();
            List<Tuple<int, int, string>> newsPos = new List<Tuple<int, int, string>>();
            foreach (Define d in defines.Values)
            {
                newsPos = new List<Tuple<int, int, string>>();
                foreach (Tuple<int, int, string> t in d.OthersPositions)
                {
                    newsPos.Add(new Tuple<int, int, string>(t.Item1 - Lines.Count - 1,
                        t.Item2, t.Item3));
                }
                d.OthersPositions.Clear();
                foreach (Tuple<int, int, string> t in newsPos)
                {
                    d.OthersPositions.Add(t);
                }
            }
            Text = "";
        }

        public void AppendTextWithHighlight(string text)
        {
            int lastLine = Lines.Count - 1;
            if (lastLine < 0) lastLine = 0;
            AppendText(text);

            string[] lines = text.Split('\n');

            int endL = Lines.Count - 1;
            if (endL < 0) return;

            Line curL;
            string line;
            int lineInd;
            int linel;

            for (int i = lastLine; i <= endL; i++)
            {
                curL = Lines[i];
                line = curL.Text;
                lineInd = curL.Position;
                linel = line.Length;
                highlightLine(line, lineInd, linel);
            }
        }

        private CodePointer[] separateLine(string line)
        {
            return CodePointer.Split(line, separatorPattern);
        }

        private List<CodePointer> labelHighlight(string line, CodePointer label)
        {
            List<CodePointer> pointers = new List<CodePointer>();
            if (labels == null) return pointers;
            for (int j = 0; j < labels.Length; j++)
            {
                if (label.End + 1 < line.Length &&
                    line[label.End + 1] == ':' &&
                    labels[j].SyntaxIsCorrect(label.Code.TrimStart(' ')
                    .TrimStart('\t') + line[label.End + 1]))
                {
                    label.Append("" + line[label.End + 1]);
                    label.Group = labels[j].Group;
                    pointers.Add(label);
                    break;
                }
                if (labels[j].SyntaxIsCorrect(label.Code.Trim(' ').Trim('\t')))
                {
                    label.Group = labels[j].Group;
                    pointers.Add(label);
                    break;
                }
            }
            return pointers;
        }
        Error posError = null;
        int errPos = 0;
        private List<CodePointer> commandHighlight(CodePointer cmd, int lineInd)
        {
            List<CodePointer> pointers = new List<CodePointer>();
            if (commands == null) return pointers;
            string tryname;
            SMWControlibBackend.Logic.Command[] names;
            CodePointer[] cpauxs;
            int minBytes = int.MaxValue;
            tryname = cmd.Code.Replace('\t', ' ').Split(' ')[0].ToLower();
            posError = new Error(lineInd, errPos, cmd.Code,
                ErrorCode.InvalidCommand, tryname);
            if (commands.ContainsKey(tryname))
            {
                posError = new Error(lineInd, errPos, cmd.Code,
                    ErrorCode.InvalidCommandArgument, tryname);
                names = commands[tryname];
                minBytes = int.MaxValue;
                for (int j = 0; j < names.Length; j++)
                {
                    if (names[j].IsCorrect(defines, cmd.Code, lineInd, cmd.Start))
                    {
                        minBytes = j;
                    }
                    else
                    {
                        posError = names[j].PossibleError;
                    }
                }
                if (minBytes != int.MaxValue)
                {
                    cpauxs =
                        names[minBytes].GetPointers(defines, cmd.Start,
                        cmd.Code, lineInd, cmd.Start);
                    foreach (CodePointer cpx in cpauxs)
                    {
                        pointers.Add(cpx);
                    }
                }
            }
            return pointers;
        }

        bool newError = false;
        private void highlightLine(string line, int lineInd, int linel)
        {
            string[] comments = line.Split(';');
            if (comments == null || comments.Length <= 0) return;

            int lineN = LineFromPosition(lineInd);

            try
            {
                StartStyling(lineInd);
                SetStyling(linel, commentGroup.Style);
            }
            catch
            {

            }

            List<CodePointer> pointers = new List<CodePointer>();
            List<CodePointer> ps;

            Match m = Regex.Match(comments[0], startPattern);
            if (m.Success)
            {
                CodePointer cp = new CodePointer
                {
                    Start = m.Index,
                    End = m.Index + m.Length - 1,
                    Code = m.ToString(),
                    Group = errorGroup
                };
                pointers.Add(cp);
                paintPointers(lineInd, pointers);
                pointers.Clear();
            }

            string shortercmds = comments[0].Substring(m.Length).Replace('\r', '\n').Replace("\n", "");

            CodePointer[] cmds = CodePointer.Split(shortercmds, separatorPattern);

            if (cmds == null)
            {
                return;
            }
            removeDefinesAtPosition(lineN);
            removeErrorsAtPosition(lineN);
            for (int j = 0; j < cmds.Length; j++)
            {
                ps = highlightMiniLine(cmds[j].Code, lineN, +m.Length
                    + cmds[j].Start);
                foreach (CodePointer cp in ps)
                {
                    pointers.Add(cp);
                }
                paintPointers(lineInd + m.Length
                    + cmds[j].Start, pointers);
            }

            m = Regex.Match(comments[0], endPattern);
            if (m.Success)
            {
                pointers.Clear();
                CodePointer cp = new CodePointer
                {
                    Start = m.Index,
                    End = m.Index + m.Length - 1,
                    Code = m.ToString(),
                    Group = errorGroup
                };
                pointers.Add(cp);
                paintPointers(lineInd, pointers);
            }
        }

        private void removeErrorsAtPosition(int lineInd)
        {
            if (errors.ContainsKey(lineInd))
            {
                errors.Remove(lineInd);
                newErrors = true;
            }
        }

        private void removeDefinesAtPosition(int lineInd)
        {
            List<Tuple<int, int, string>> removeList;
            List<string> removeList2 = new List<string>();
            foreach (KeyValuePair<string, Define> kvp in defines)
            {
                removeList = new List<Tuple<int, int, string>>();
                foreach (Tuple<int, int, string> t in kvp.Value.OthersPositions)
                {
                    if (t.Item1 == lineInd)
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

        const string startPattern = @"^(\ |\t)*(:(\ |\t)*)*";
        const string endPattern = @"(\ |\t)+(:(\ |\t)*)*$";
        bool definesDetected = false;
        private List<CodePointer> highlightMiniLine(string line, int lineInd, int startIndex)
        {
            CodePointer[] labs = CodePointer.Split(line, @":(\ |\t)*");
            List<CodePointer> pointers = new List<CodePointer>();
            if (commands == null) return pointers;
            List<CodePointer> tmp;
            Define def;

            for (int i = 0; i < labs.Length; i++)
            {
                tmp = labelHighlight(line, labs[i]);

                errPos = labs[i].Start + startIndex;
                if (tmp.Count <= 0) tmp = commandHighlight(labs[i], lineInd);

                if (tmp.Count <= 0)
                {
                    def = Define.GetDefine(defines, labs[i].Code, lineInd, labs[i].Start + startIndex);
                    
                    if(def != Define.Default && def != null)
                    {
                        defines.Add(def.Name, def);
                    }
                    if (def != null)
                    {
                        addBookMark(lineInd);
                        labs[i].Group = defineGroup;
                        tmp = new List<CodePointer>
                        {
                            labs[i]
                        };
                    }
                    definesDetected = true;
                }

                if (tmp.Count <= 0)
                {
                    if (posError == null) posError = new Error(lineInd, startIndex, "", ErrorCode.Unknown);
                    if(Define.PossibleError!=null)
                    {
                        if (posError.Code == ErrorCode.InvalidCommand
                            && Define.PossibleError.Code == ErrorCode.DefineNotFound)
                        {
                            posError = Define.PossibleError;
                        }
                        else if (posError.Code == ErrorCode.InvalidCommand
                            && Define.PossibleError.Code == ErrorCode.InvalidDefineSignature
                            && Regex.IsMatch(labs[i].Code, Define.MidPattern))
                        {
                            posError = Define.PossibleError;
                        }
                    }
                    if (!errors.ContainsKey(posError.Line))
                    {
                        errors.Add(posError.Line, new List<Error>());
                    }
                    newError = true;
                    errors[posError.Line].Add(posError);
                    labs[i].Group = errorGroup;
                    pointers.Add(labs[i]);
                }
                else
                {
                    foreach(CodePointer cp in tmp)
                    {
                        pointers.Add(cp);
                    }
                }
            }

            return pointers;
        }

        private void paintPointers(int ind, List<CodePointer> pointers)
        {
            try
            {
                int offset = 0;
                foreach (CodePointer pointer in pointers)
                {
                    StartStyling(ind + pointer.Start);
                    SetStyling(pointer.Code.Length, pointer.Group.Style);
                    offset += pointer.Code.Length;
                }
            }
            catch
            {

            }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
    }
}
