using SMWControlibBackend.Graphics.Frames;
using SMWControlibBackend.Interaction;
using System;
using System.Text;
using System.Windows.Forms;

namespace SMWControlibControls.InteractionControls
{
    public partial class NewInteractionPointDialog : Form
    {
        public static bool AutoSelect = true;
        Frame frame;

        public NewInteractionPointDialog()
        {
            InitializeComponent();
            accept.Click += click;
            autosel.CheckedChanged += autoselCheckedChanged;
            name.TextChanged += textChanged;
        }

        private void autoselCheckedChanged(object sender, EventArgs e)
        {
            AutoSelect = autosel.Checked;
        }

        private void textChanged(object sender, EventArgs e)
        {
            int st = name.SelectionStart;
            bool invalidChar = false;
            StringBuilder sb = new StringBuilder();
            foreach (char c in name.Text)
            {
                if ((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z')
                    || (c >= '0' && c <= '9') || c == '-' || c == '_')
                {
                    sb.Append(c);
                }
                else
                {
                    invalidChar = true;
                }
            }

            if (invalidChar)
            {
                name.Text = sb.ToString();
                if (st - 1 < 0) st = 1;
                name.SelectionStart = st - 1;
            }
            else name.SelectionStart = st;
        }

        private void click(object sender, EventArgs e)
        {
            InteractionPoint NewHitbox = null;

            if (name.Text == null || name.Text.Length == 0 || name.Text == "" ||
                (!(name.Text[0] >= 'a' && name.Text[0] <= 'z') &&
                !(name.Text[0] >= 'A' && name.Text[0] <= 'Z')))
                name.Text = "p" + name.Text;

            validName();

            NewHitbox = new InteractionPoint()
            {
                Name = name.Text,
                XOffset = 0,
                YOffset = 0,
            };

            frame.InteractionPoints.Add(NewHitbox);
            DialogResult = DialogResult.OK;
            Dispose();
        }

        private void validName()
        {
            bool notValid = true;
            while (notValid)
            {
                notValid = false;
                foreach (InteractionPoint f in frame.InteractionPoints)
                {
                    if (f.Name == name.Text)
                    {
                        notValid = true;
                        name.Text += "0";
                        break;
                    }
                }
            }

        }

        public static DialogResult Show(IWin32Window Owner,
            Frame Frame)
        {
            NewInteractionPointDialog nfd = new NewInteractionPointDialog()
            {
                frame = Frame
            };

            nfd.name.Text = "Point" + Frame.InteractionPoints.Count;

            nfd.autosel.Checked = AutoSelect;

            return nfd.ShowDialog(Owner);
        }
    }
}
