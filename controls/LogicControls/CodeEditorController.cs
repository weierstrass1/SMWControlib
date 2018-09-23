using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SMWControlibBackend.Logic;

namespace SMWControlibControls.LogicControls
{
    public partial class CodeEditorController : UserControl
    {
        Color clickedBack = Color.FromArgb(224, 200, 48);
        Color tabBack1 = Color.FromArgb(200,200,224);
        Color tabBack2 = Color.FromArgb(216, 216, 232);
        Color fore = Color.FromArgb(80, 80, 96);
        public TextEditor CodeEditor
        {
            get
            {
                TabPage t = tabControl1.SelectedTab;
                string s = "codeEd" + tabControl1.SelectedIndex;
                if (t.Controls.ContainsKey(s))
                {
                    return (TextEditor)t.Controls[s];
                }
                return null;
            }
        }
        public CodeEditorController()
        {
            InitializeComponent();
            CodeEditor.ErrorsAdded += errorsAdded;
            CodeEditor.MouseEnter += mouseEnter;
            errorMatrix.MouseEnter += mouseEnter;
            errorMatrix.RowCount = 0;
            errorMatrix.RowStyles.Clear();
        }

        private void mouseEnter(object sender, EventArgs e)
        {
            ((Control)sender).Focus();
        }

        private void errorsAdded(Dictionary<int, List<Error>> obj)
        {
            lastClicked = -1;
            List<Error> ers = new List<Error>();

            foreach (List<Error> k in obj.Values)
            {
                foreach (Error e in k)
                {
                    ers.Add(e);
                }
            }

            ers.Sort();

            int rowc = errorMatrix.RowCount;
            errorMatrix.RowCount = Math.Max(errorMatrix.RowCount, ers.Count);
            errorMatrix.Height = obj.Count * 20;

            int i = 0;
            System.Windows.Forms.Label l;

            for (i = rowc; i < ers.Count; i++)
            {
                errorMatrix.RowStyles.Add(new RowStyle(SizeType.Absolute, 20));

                l = new System.Windows.Forms.Label
                {
                    Font = Font,
                    ForeColor = fore,
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Margin = new Padding(0, 0, 1, 0)
                };

                l.MouseEnter += mouseEnter;
                l.Click += click;
                l.DoubleClick += doubleClick;
                errorMatrix.Controls.Add(l, 0, i);

                l = new System.Windows.Forms.Label
                {
                    Font = Font,
                    ForeColor = fore,
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Margin = new Padding(1, 0, 1, 0)
                };

                l.MouseEnter += mouseEnter;
                l.Click += click;
                l.DoubleClick += doubleClick;
                errorMatrix.Controls.Add(l, 1, i);

                l = new System.Windows.Forms.Label
                {
                    Font = Font,
                    ForeColor = fore,
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Margin = new Padding(1, 0, 1, 0)
                };

                l.MouseEnter += mouseEnter;
                l.Click += click;
                l.DoubleClick += doubleClick;
                errorMatrix.Controls.Add(l, 2, i);

                l = new System.Windows.Forms.Label
                {
                    Font = Font,
                    ForeColor = fore,
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Margin = new Padding(1, 0, 1, 0)
                };

                l.MouseEnter += mouseEnter;
                l.Click += click;
                l.DoubleClick += doubleClick;
                errorMatrix.Controls.Add(l, 3, i);

                l = new System.Windows.Forms.Label
                {
                    Font = Font,
                    ForeColor = fore,
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Margin = new Padding(1, 0, 1, 0)
                };

                l.MouseEnter += mouseEnter;
                l.Click += click;
                l.DoubleClick += doubleClick;
                errorMatrix.Controls.Add(l, 4, i);
            }

            Color b;
            i = 0;
            foreach (Error e in ers)
            {
                if (i % 2 == 0) b = tabBack1;
                else b = tabBack2;

                l = (System.Windows.Forms.Label)errorMatrix.
                    GetControlFromPosition(0, i);
                l.Text = e.Code.Code.ToString("X");
                l.BackColor = b;

                l = (System.Windows.Forms.Label)errorMatrix.
                    GetControlFromPosition(1, i);
                l.Text = e.Code.ToString();
                l.BackColor = b;

                l = (System.Windows.Forms.Label)errorMatrix.
                    GetControlFromPosition(2, i);
                l.Text = "" + e.Line;
                l.BackColor = b;

                l = (System.Windows.Forms.Label)errorMatrix.
                    GetControlFromPosition(3, i);
                l.Text = "" + e.Start;
                l.BackColor = b;

                l = (System.Windows.Forms.Label)errorMatrix.
                    GetControlFromPosition(4, i);
                l.Text = e.Message;
                l.BackColor = b;

                i++;
            }
        }

        int lastClicked = -1;
        private void click(object sender, EventArgs e)
        {
            if (sender.GetType() == typeof(System.Windows.Forms.Label))
            {
                System.Windows.Forms.Label l = (System.Windows.Forms.Label)sender;

                int row = errorMatrix.GetRow(l);
                if (row == lastClicked)
                {
                    return;
                }

                if (lastClicked >= 0)
                {
                    Color b;
                    if (lastClicked % 2 == 0) b = tabBack1;
                    else b = tabBack2;

                    l = (System.Windows.Forms.Label)errorMatrix.
                        GetControlFromPosition(0, lastClicked);
                    l.BackColor = b;
                    l = (System.Windows.Forms.Label)errorMatrix.
                        GetControlFromPosition(1, lastClicked);
                    l.BackColor = b;
                    l = (System.Windows.Forms.Label)errorMatrix.
                        GetControlFromPosition(2, lastClicked);
                    l.BackColor = b;
                    l = (System.Windows.Forms.Label)errorMatrix.
                        GetControlFromPosition(3, lastClicked);
                    l.BackColor = b;
                    l = (System.Windows.Forms.Label)errorMatrix.
                        GetControlFromPosition(4, lastClicked);
                    l.BackColor = b;
                }

                l = (System.Windows.Forms.Label)errorMatrix.
                    GetControlFromPosition(0, row);
                l.BackColor = clickedBack;
                l = (System.Windows.Forms.Label)errorMatrix.
                    GetControlFromPosition(1, row);
                l.BackColor = clickedBack;
                l = (System.Windows.Forms.Label)errorMatrix.
                    GetControlFromPosition(2, row);
                l.BackColor = clickedBack;
                l = (System.Windows.Forms.Label)errorMatrix.
                    GetControlFromPosition(3, row);
                l.BackColor = clickedBack;
                l = (System.Windows.Forms.Label)errorMatrix.
                    GetControlFromPosition(4, row);
                l.BackColor = clickedBack;
                lastClicked = row;
            }
        }

        int adder = 9;
        private void doubleClick(object sender, EventArgs e)
        {
            if(sender.GetType() == typeof(System.Windows.Forms.Label))
            {
                System.Windows.Forms.Label l = (System.Windows.Forms.Label)sender;

                int row = errorMatrix.GetRow(l);

                l = (System.Windows.Forms.Label)errorMatrix.GetControlFromPosition(2, row);
                int gopos = int.Parse(l.Text);
                int gp = gopos;
                int lind = CodeEditor.LineFromPosition(CodeEditor.CurrentPosition);
                if (gopos >= lind) gopos += adder;
                else gopos -= adder;
                if (gopos >= CodeEditor.Lines.Count) gopos = CodeEditor.Lines.Count - 1;
                if (gopos < 0) gopos = 0;
                
                CodeEditor.Lines[gopos].Goto();
                l = (System.Windows.Forms.Label)errorMatrix.GetControlFromPosition(3, row);
                gopos = CodeEditor.Lines[gp].Position + int.Parse(l.Text);
                CodeEditor.CurrentPosition = gopos;
                CodeEditor.SelectionStart = gopos;
                CodeEditor.SelectionEnd = gopos;
                CodeEditor.Focus();
            }
        }
    }
}
