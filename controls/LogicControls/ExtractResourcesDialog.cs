using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMWControlibControls.LogicControls
{
    public partial class ExtractResourcesDialog : Form
    {
        public static bool FlipX = true, FlipY = false;
        public static bool Code = true;
        public static bool SP1 = false, SP2 = false, SP3 = false, SP4 = false;
        public static bool Palette = false;
        public static string ProjectName = "DyzenProject";
        public ExtractResourcesDialog()
        {
            InitializeComponent();
            accept.Click += acceptClick;
            codecheck.CheckedChanged += codecheckCheckedChanged;
        }

        private void codecheckCheckedChanged(object sender, EventArgs e)
        {
            xflipcheck.Enabled = codecheck.Checked;
            label19.Enabled = codecheck.Checked;
            yflipcheck.Enabled = codecheck.Checked;
            label2.Enabled = codecheck.Checked;
        }

        private void acceptClick(object sender, EventArgs e)
        {
            Code = codecheck.Checked;
            FlipX = xflipcheck.Checked;
            FlipY  = yflipcheck.Checked;
            SP1 = sp1check.Checked;
            SP2 = sp2check.Checked;
            SP3 = sp3check.Checked;
            SP4 = sp4check.Checked;
            Palette = palettecheck.Checked;

            string projname = "";
            for (int i = 0; i < name.Text.Length; i++)
            {
                if (name.Text[i] == ' ' || (name.Text[i] >= '0' && name.Text[i] <= '9')
                    || (name.Text[i] >= 'a' && name.Text[i] <= 'z') ||
                    (name.Text[i] >= 'A' && name.Text[i] <= 'Z'))
                {
                    projname += name.Text[i];
                }
            }
            if (projname == "" || projname == null || projname.Length <= 0)
            {
                projname = "DyzenProject";
            }

            ProjectName = projname;

            DialogResult = DialogResult.OK;
            Dispose();
        }

        public static new DialogResult Show(IWin32Window Owner)
        {
            ExtractResourcesDialog erd = new ExtractResourcesDialog();

            erd.codecheck.Checked = Code;
            erd.xflipcheck.Checked = FlipX;
            erd.yflipcheck.Checked = FlipY;
            erd.sp1check.Checked = SP1;
            erd.sp2check.Checked = SP2;
            erd.sp3check.Checked = SP3;
            erd.sp4check.Checked = SP4;
            erd.palettecheck.Checked = Palette;
            erd.name.Text = ProjectName;

            return erd.ShowDialog(Owner);
        }
    }
}
