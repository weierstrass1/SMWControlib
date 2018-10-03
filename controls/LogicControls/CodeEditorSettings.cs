using SMWControlibBackend.Logic;
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
    public partial class CodeEditorSettings : Form
    {
        private GroupControl[] groupControls;
        private StylesContainer stylesContainer;
        private CodeEditorSettings()
        {
            InitializeComponent();
            accept.Click += click;
        }

        private void click(object sender, EventArgs e)
        {
            for (int i = 0; i < groupControls.Length; i++)
            {
                stylesContainer.ForeColorRed[groupControls[i].Group.Style] =
                    groupControls[i].Color.R;
                stylesContainer.ForeColorGreen[groupControls[i].Group.Style] =
                    groupControls[i].Color.G;
                stylesContainer.ForeColorBlue[groupControls[i].Group.Style] =
                    groupControls[i].Color.B;
            }

            DialogResult = DialogResult.OK;

            Dispose();
        }

        public static DialogResult ShowDialog(IWin32Window owner,
            StylesContainer sc, Group[] groups)
        {
            CodeEditorSettings ces = new CodeEditorSettings
            {
                stylesContainer = sc,
                groupControls = new GroupControl[groups.Length]
            };
            Color c;
            int s;

            for (int i = 0; i < groups.Length; i++)
            {
                s = groups[i].Style;
                c = Color.FromArgb(sc.ForeColorRed[s], sc.ForeColorGreen[s],
                    sc.ForeColorBlue[s]);
                ces.groupControls[i] = new GroupControl(groups[i], c)
                {
                    Dock = DockStyle.Top,
                    Parent = ces.panelGroup
                };
                ces.groupControls[i].BringToFront();
            }

            return ces.ShowDialog(owner);
        }
    }
}
