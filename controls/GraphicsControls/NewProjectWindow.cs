using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMWControlibControls.GraphicsControls
{
    public partial class NewProjectWindow : Form
    {
        public static string ProjectName = "";
        public static ProjectType ProjectType = 0;
        static string stardarddesc = "This is a regular sprite that uses graphics preloaded at the start of the level.\n They can use any tile on SP1, SP2, SP3 and SP4.";
        static string dynamicdesc = "This is a sprite that loads its graphics to the vram when it change frame during animation.\n They can use tiles on SP1, SP2 and first half of SP3, also they can use tiles on their dynamic space. Dynamic sprites of 96x96 only can use tiles on SP1, SP2 and their dynamic space, dynamic sprites of 112x112 only can use tiles on SP1 and their dynamic space.";
        static string semidynamicdesc = "This is a sprite that when it is loaded on the level, it loads all their graphics to the vram, then all copies of the sprite use the same space on vram. When all copies dies or dissappear, the space reserved is free for another semidynamic sprite.\n They can use SP1 and their semi-dynamic space.";
        static string playerdesc = "This is for custom players.";
        public NewProjectWindow()
        {
            InitializeComponent();
            standard.CheckedChanged += Standard_CheckedChanged;
            dynamic.CheckedChanged += Dynamic_CheckedChanged;
            semidynamic.CheckedChanged += Semidynamic_CheckedChanged;
            player.CheckedChanged += Player_CheckedChanged;
            standard.Checked = true;
            accept.Click += Accept_Click;
        }

        private void Accept_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Dispose();
        }

        private void Player_CheckedChanged(object sender, EventArgs e)
        {
            if (player.Checked)
            {
                desc.Text = playerdesc;
                ProjectType = ProjectType.Player;
            }
        }

        private void Semidynamic_CheckedChanged(object sender, EventArgs e)
        {
            if (semidynamic.Checked)
            {
                desc.Text = semidynamicdesc;
                ProjectType = ProjectType.SemiDynamic;
            }
        }

        private void Dynamic_CheckedChanged(object sender, EventArgs e)
        {
            if (dynamic.Checked)
            {
                desc.Text = dynamicdesc;
                ProjectType = ProjectType.Dynamic;
            }
        }

        private void Standard_CheckedChanged(object sender, EventArgs e)
        {
            if (standard.Checked)
            {
                desc.Text = stardarddesc;
                ProjectType = ProjectType.Standard;
            }
        }

        public static new DialogResult Show(IWin32Window Owner)
        {
            NewProjectWindow npw = new NewProjectWindow();
            
            return npw.ShowDialog(Owner);
        }
    }
    public class ProjectType
    {
        int value;
        public static ProjectType Standard = new ProjectType(0);
        public static ProjectType Dynamic = new ProjectType(1);
        public static ProjectType SemiDynamic = new ProjectType(2);
        public static ProjectType Player = new ProjectType(3);

        ProjectType(int v)
        {
            value = v;
        }

        public static implicit operator ProjectType(int i)
        {
            switch (i)
            {
                case 0:
                    return Standard;
                case 1:
                    return Dynamic;
                case 2:
                    return SemiDynamic;
                default:
                    return Player;
            }
        }

        public static implicit operator int(ProjectType pt)
        {
            return pt.value;
        }
    }
}
