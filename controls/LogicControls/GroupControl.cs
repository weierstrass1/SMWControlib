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
    public partial class GroupControl : UserControl
    {
        public Group Group { get; private set; }
        public Color Color
        {
            get
            {
                return color.BackColor;
            }
        }
        public GroupControl(Group group)
        {
            InitializeComponent();
            Group = group;
            name.Text = Group.Name;
            description.Text = Group.Description;
            color.DoubleClick += doubleClick;
            red.ValueChanged += valueChanged;
            green.ValueChanged += valueChanged;
            blue.ValueChanged += valueChanged;
        }

        private void valueChanged(object sender, EventArgs e)
        {
            color.BackColor = Color.FromArgb((int)red.Value,
                (int)green.Value, (int)blue.Value);
        }

        private void doubleClick(object sender, EventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            colorDialog1.Color = pb.BackColor;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                pb.BackColor = colorDialog1.Color;
                red.Value = pb.BackColor.R;
                green.Value = pb.BackColor.G;
                blue.Value = pb.BackColor.B;
            }
        }
    }
}
