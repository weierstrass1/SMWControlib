using SMWControlibBackend.Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMWControlibControls.LogicControls
{
    public partial class TextEditor : RichTextBox
    {
        private static ArgsTypes[] argTypes;
        private static Dictionary<string, Command[]> commands;
        private static SMWControlibBackend.Logic.Label[] labels;
        private static SMWControlibBackend.Logic.Group[] groups;
        private static SMWControlibBackend.Logic.Group errorGroup;


        public TextEditor()
        {
            InitializeComponent();
            
            try
            {
                groups = SMWControlibBackend.Logic.Group.GetGroups(@".\CSVs\Syntax\groups.csv");
                argTypes = ArgsTypes.GetArgsTypes(@".\CSVs\Syntax\args.csv", groups);
                commands = Command.GetCommands(@".\CSVs\Syntax\commands.csv", argTypes, groups);
                labels = new SMWControlibBackend.Logic.Label[4];
                labels[0] = new NormalLabel(groups, "Label");
                labels[0].Group.Color = Color.FromArgb(64, 248, 248);
                labels[1] = new SubLabel(groups, "Special Label");
                labels[1].Group.Color = Color.FromArgb(160, 248, 248);
                labels[2] = new PlusLabel(groups, "Special Label");
                labels[3] = new MinusLabel(groups, "Special Label");
                errorGroup = SMWControlibBackend.Logic.Group.FindGroup(groups, "Error");
                errorGroup.Color = Color.FromArgb(255, 0, 0);
            }
            catch { }
            TextChanged += textChanged;
        }

        const string startSpacesPattern = @"^(\ |\t)*";
        const string endSpacesPattern = @"(\ |\t)*$";
        const string separatorPattern = @"(\ |\t)+:(\ |\t)+";
        private void textChanged(object sender, EventArgs e)
        {
            if (commands == null || argTypes == null) return;
            if (Lines == null || Lines.Length <= 0) return;
            
            int l = GetLineFromCharIndex(SelectionStart - 1);

            int lastInd = SelectionStart;
            int lastLen = SelectionLength;

            string line = Lines[l];
            int lineInd = GetFirstCharIndexFromLine(l);
            int linel = line.Length;

            string[] comments = line.Split(';');
            if (comments == null || comments.Length <= 0) return;

            SuspendPainting();

            Select(lineInd, linel);
            SelectionColor = Color.FromArgb(224, 224, 224);
            SelectionFont = new Font(Font, FontStyle.Bold);
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

            string shortercmds = comments[0].Substring(m.Length);

            CodePointer[] cmds = CodePointer.Split(shortercmds, separatorPattern);

            if (cmds == null)
            {
                Select(lastInd, 0);
                ResumePainting();
                return;
            }

            for (int j = 0; j < cmds.Length; j++)
            {
                ps = highLightMiniLine(cmds[j].Code);
                foreach(CodePointer cp in ps)
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

            Select(lastInd, 0);
            ResumePainting();
        }

        private CodePointer[] separateLine(string line)
        {
            return CodePointer.Split(line, separatorPattern);
        }

        private List<CodePointer> labelHighlight(string line, CodePointer label)
        {
            List<CodePointer> pointers = new List<CodePointer>();
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

        private List<CodePointer> commandHighlight(CodePointer cmd)
        {
            List<CodePointer> pointers = new List<CodePointer>();
            string tryname;
            Command[] names;
            CodePointer[] cpauxs;
            int minBytes = int.MaxValue;
            tryname = cmd.Code.Replace('\t', ' ').Split(' ')[0].ToLower();
            if (commands.ContainsKey(tryname))
            {
                names = commands[tryname];
                minBytes = int.MaxValue;
                for (int j = 0; j < names.Length; j++)
                {
                    if (names[j].IsCorrect(cmd.Code))
                    {
                        minBytes = j;
                    }
                }
                if (minBytes != int.MaxValue)
                {
                    cpauxs =
                        names[minBytes].GetPointers(cmd.Start, cmd.Code);
                    foreach (CodePointer cpx in cpauxs)
                    {
                        pointers.Add(cpx);
                    }
                }
            }
            return pointers;
        }

        const string startPattern = @"^(\ |\t)*(:(\ |\t)*)*";
        const string endPattern = @"(\ |\t)+(:(\ |\t)*)*$";
        private List<CodePointer> highLightMiniLine(string line)
        {
            CodePointer[] labs = CodePointer.Split(line, @":(\ |\t)*");
            List<CodePointer> pointers = new List<CodePointer>();
            List<CodePointer> tmp;

            for (int i = 0; i < labs.Length; i++)
            {
                tmp = labelHighlight(line, labs[i]);

                if (tmp.Count <= 0) tmp = commandHighlight(labs[i]);

                if (tmp.Count <= 0)
                {
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
            int offset = 0;
            foreach(CodePointer pointer in pointers)
            {
                Select(ind + pointer.Start, pointer.Code.Length);
                SelectionColor = pointer.Group.Color;
                SelectionFont = new Font(Font, FontStyle.Bold);
                offset += pointer.Code.Length;
            }
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, ref IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, IntPtr lParam);

        const int wm_USER = 0x400;
        const int wm_SETREDRAW = 0xB;
        const int wm_GETEVENTMASK = wm_USER + 59;
        const int em_SETEVENTMASK = wm_USER + 69;
        const int em_GETSCROLLPOS = wm_USER + 221;
        const int em_SETSCROLLPOS = wm_USER + 222;

        private IntPtr _ScrollPoint;
        private bool _Painting = true;
        private IntPtr _EventMask;
        private int _SuspendIndex = 0;
        private int _SuspendLength = 0;

        public void SuspendPainting()
        {
            if (_Painting)
            {
                _SuspendIndex = SelectionStart;
                _SuspendLength = SelectionLength;
                SendMessage(Handle, em_GETSCROLLPOS, 0, ref _ScrollPoint);
                SendMessage(Handle, wm_SETREDRAW, 0, IntPtr.Zero);
                _EventMask = SendMessage(Handle, wm_GETEVENTMASK, 0, IntPtr.Zero);
                _Painting = false;
            }
        }

        public void ResumePainting()
        {
            if (!_Painting)
            {
                Select(_SuspendIndex, _SuspendLength);
                SendMessage(Handle, em_SETSCROLLPOS, 0, ref _ScrollPoint);
                SendMessage(Handle, em_SETEVENTMASK, 0, _EventMask);
                SendMessage(Handle, wm_SETREDRAW, 1, IntPtr.Zero);
                _Painting = true;
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
    }
}
