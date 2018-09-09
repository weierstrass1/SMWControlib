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

        public TextEditor()
        {
            InitializeComponent();
            
            try
            {
                groups = SMWControlibBackend.Logic.Group.GetGroups(@".\CSVs\Syntax\groups.csv");
                argTypes = ArgsTypes.GetArgsTypes(@".\CSVs\Syntax\args.csv");
                commands = Command.GetCommands(@".\CSVs\Syntax\commands.csv", argTypes);
                labels = new SMWControlibBackend.Logic.Label[4];
                labels[0] = new NormalLabel();
                labels[1] = new SubLabel();
                labels[2] = new PlusLabel();
                labels[3] = new MinusLabel();
            }
            catch { }
            TextChanged += textChanged;
        }

        const string startSpacesPattern = @"^(\ |\t)*";
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
            bool correct = false;

            string[] comments = line.Split(';');
            if (comments == null || comments.Length <= 0) return;

            CodePointer[] cmds = CodePointer.Split(comments[0], separatorPattern);

            if (cmds == null) return;
            SuspendPainting();


            string name;
            Command[] names;
            int st = 0;
            Match ma;
            string cmdTrim, cmdTrim2;

            Select(lineInd, linel);
            SelectionColor = Color.FromArgb(224, 224, 224);
            SelectionFont = new Font(Font, FontStyle.Bold);
            bool isLabel = true;
            CodePointer[] cps;
            int adder = 0;
            string name2 = "";
            for (int j = 0; j < cmds.Length; j++)
            {
                cmdTrim = cmds[j].Code.Trim(' ').Trim('\t');
                name = cmdTrim.Split(' ', '\t')[0].ToLower();
                ma = Regex.Match(cmds[j].Code, startSpacesPattern);
                st = 0;
                if (ma.Success)
                    st = ma.Length;
                correct = false;
                isLabel = false;
                adder = 0;
                cps = CodePointer.Split(cmds[j].Code, ":");
                if (cps.Length > 1) adder++;
                for (int i = 0; i < labels.Length; i++)
                {
                    if(labels[i].SyntaxIsCorrect(cps[0].Code))
                    {
                        Select(lineInd + cmds[j].Start + st, cps[0].Code.Length + adder);
                        SelectionColor = Color.FromArgb(80, 224, 224);
                        SelectionFont = new Font(Font, FontStyle.Bold);
                        isLabel = true;
                        correct = true;
                        break;
                    }
                }
                if (adder > 0)
                {
                    cmdTrim2 = cps[1].Code.Trim(' ').Trim('\t');
                    name2 = cmdTrim2.Split(' ', '\t')[0].ToLower();
                    names = commands[name.ToLower()];
                    for (int i = 0; i < names.Length; i++)
                    {
                        if (names[i].IsCorrect(cmdTrim))
                        {
                            Select(lineInd + cmds[j].Start + st, names[i].Name.Length);
                            SelectionColor = Color.FromArgb(80, 80, 224);
                            SelectionFont = new Font(Font, FontStyle.Bold);
                            Select(lineInd + cmds[j].Start + st + SelectionLength, cmds[j].Code.Length - SelectionLength);
                            SelectionColor = Color.FromArgb(224, 224, 224);
                            SelectionFont = new Font(Font, FontStyle.Bold);
                            correct = true;
                            break;
                        }
                    }
                }
                if (!isLabel && commands.ContainsKey(name))
                {
                    names = commands[name.ToLower()];
                    for (int i = 0; i < names.Length; i++)
                    {
                        if (names[i].IsCorrect(cmdTrim))
                        {
                            Select(lineInd + cmds[j].Start + st, names[i].Name.Length);
                            SelectionColor = Color.FromArgb(80, 80, 224);
                            SelectionFont = new Font(Font, FontStyle.Bold);
                            Select(lineInd + cmds[j].Start + st + SelectionLength, cmds[j].Code.Length - SelectionLength);
                            SelectionColor = Color.FromArgb(224, 224, 224);
                            SelectionFont = new Font(Font, FontStyle.Bold);
                            correct = true;
                            break;
                        }
                    }
                }
                if (!correct)
                {
                    Select(lineInd + cmds[j].Start + st, cmds[j].Code.Length);
                    SelectionBackColor = BackColor;
                    SelectionColor = Color.FromArgb(224, 0, 0);
                    SelectionFont = new Font(Font, FontStyle.Regular);
                }
            }
            
            Select(lastInd, 0);
            ResumePainting();
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
