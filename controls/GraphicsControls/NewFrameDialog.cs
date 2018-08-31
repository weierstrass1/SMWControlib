using SMWControlibBackend.Graphics.Frames;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SMWControlibControls.GraphicsControls
{
    public partial class NewFrameDialog : Form
    {
        public static bool Duplicate = false, AutoSelect = true;
        List<Frame> frames;
        private NewFrameDialog()
        {
            InitializeComponent();
            accept.Click += click;
            isDuplicate.CheckedChanged += checkedChanged;
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
            foreach(char c in name.Text)
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

        private void checkedChanged(object sender, EventArgs e)
        {
            frameSelector.Enabled = isDuplicate.Checked;
            Duplicate = isDuplicate.Checked;
        }

        private void click(object sender, EventArgs e)
        {
            if(isDuplicate.Checked && 
                frameSelector.SelectedItem.GetType() != typeof(Frame))
            {
                MessageBox.Show("If you want to duplicate a frame, then You must select a frame.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Frame NewFrame = null;

            if (name.Text == null || name.Text.Length == 0 || name.Text == "" ||
                (!(name.Text[0] >= 'a' && name.Text[0] <= 'z') &&
                !(name.Text[0] >= 'A' && name.Text[0] <= 'Z')))
                name.Text = "f" + name.Text;

            validName();
            if (!isDuplicate.Checked)
            {
                NewFrame = new Frame()
                {
                    Name = name.Text
                };
            }
            else
            {
                NewFrame = ((Frame)frameSelector.SelectedItem).
                    Duplicate();
                NewFrame.Name = name.Text;
            }
            frames.Add(NewFrame);
            DialogResult = DialogResult.OK;
            Dispose();
        }

        private void validName()
        {
            bool notValid = true;
            while (notValid)
            {
                notValid = false;
                foreach (Frame f in frames)
                {
                    if(f.Name==name.Text)
                    {
                        notValid = true;
                        name.Text += "0";
                        break;
                    }
                }
            }
            
        }

        public static DialogResult Show(IWin32Window Owner,
            List<Frame> Frames)
        {
            NewFrameDialog nfd = new NewFrameDialog()
            {
                frames = Frames
            };
            foreach(Frame f in Frames)
            {
                nfd.frameSelector.Items.Add(f);
            }
            nfd.name.Text = "Frame" + Frames.Count;
            if (nfd.frameSelector.Items == null ||
                nfd.frameSelector.Items.Count == 0)
                nfd.isDuplicate.Enabled = false;
            if (Frames != null && Frames.Count != 0)
                nfd.frameSelector.SelectedIndex = 0;

            nfd.isDuplicate.Checked = Duplicate;
            nfd.autosel.Checked = AutoSelect;

            return nfd.ShowDialog(Owner);
        }
    }
}
