using System;
using System.Windows.Forms;

namespace SMWControlibControls.InteractionControls
{
    public partial class NewHitboxInteractionActionDialog : Form
    {
        string[] names;
        public static bool Autoselect = true;
        public static bool GotoAct = false;
        public static string NewName = "";
        public NewHitboxInteractionActionDialog()
        {
            InitializeComponent();
            accept.Click += acceptClick;
        }

        private void acceptClick(object sender, EventArgs e)
        {
            if (name.Text == "" || name.Text == null || name.Text.Length <= 0)
            {
                name.Text= "HitboxAction" + names.Length;
            }

            bool found = true;
            int j = 0;
            string newStr = name.Text;
            while (found)
            {
                found = false;
                for (int i = 0; i < names.Length; i++)
                {
                    if (names[i] == name.Text)
                    {
                        found = true;
                        break;
                    }
                }
                if (found)
                {
                    j++;
                    name.Text = newStr + j;
                }
            }

            DialogResult = DialogResult.OK;
            Dispose();
        }

        public static DialogResult Show(IWin32Window Owner,
            string[] hbact)
        {
            NewHitboxInteractionActionDialog dial = new NewHitboxInteractionActionDialog
            {
                names = hbact
            };
            dial.name.Text = "HitboxAction" + hbact.Length;
            dial.autosel.Checked = Autoselect;
            dial.gotoact.Checked = GotoAct;
            dial.autosel.CheckedChanged += dial.autoselCheckedChanged;
            dial.gotoact.CheckedChanged += dial.gotoactCheckedChanged;
            bool found = true;
            int j = hbact.Length;
            while (found) 
            {
                found = false;
                for (int i = 0; i < hbact.Length; i++)
                {
                    if (hbact[i] == dial.name.Text)
                    {
                        found = true;
                        break;
                    }
                }
                if (found)
                {
                    j++;
                    dial.name.Text = "HitboxAction" + j;
                }
            }
            NewName = dial.name.Text;

            return dial.ShowDialog(Owner);
        }

        private void gotoactCheckedChanged(object sender, EventArgs e)
        {
            GotoAct = gotoact.Checked;
        }

        private void autoselCheckedChanged(object sender, EventArgs e)
        {
            Autoselect = autosel.Checked;
        }
    }
}
