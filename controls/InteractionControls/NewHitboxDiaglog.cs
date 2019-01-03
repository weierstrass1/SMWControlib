using SMWControlibBackend.Graphics.Frames;
using SMWControlibBackend.Interaction;
using System;
using System.Text;
using System.Windows.Forms;

namespace SMWControlibControls.InteractionControls
{
    public partial class NewHitboxDiaglog : Form
    {
        public static bool AutoSelect = true;
        Frame frame;

        public NewHitboxDiaglog()
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
            HitBox NewHitbox = null;

            if (name.Text == null || name.Text.Length == 0 || name.Text == "" ||
                (!(name.Text[0] >= 'a' && name.Text[0] <= 'z') &&
                !(name.Text[0] >= 'A' && name.Text[0] <= 'Z')))
                name.Text = "h" + name.Text;

            validName();

            NewHitbox = new RectangleHitBox()
            {
                Name = name.Text,
                XOffset = 0,
                YOffset = 0,
                Width = 16,
                Height = 16
            };

            frame.HitBoxes.Add(NewHitbox);
            DialogResult = DialogResult.OK;
            Dispose();
        }

        private void validName()
        {
            bool notValid = true;
            while (notValid)
            {
                notValid = false;
                foreach (HitBox f in frame.HitBoxes)
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
            NewHitboxDiaglog nfd = new NewHitboxDiaglog()
            {
                frame = Frame
            };

            nfd.name.Text = "Hitbox" + Frame.HitBoxes.Count;

            nfd.autosel.Checked = AutoSelect;

            return nfd.ShowDialog(Owner);
        }
    }
}
