using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMWControlibControls.LogicControls
{
    public partial class BrightValueControl : SMWControlibControls.LogicControls.ColorValueControl
    {
        public BrightValueControl()
        {
            InitializeComponent();
            value.ValueChanged += valueChanged;
        }

        private void valueChanged(object sender, EventArgs e)
        {
            float f = ((float)value.Value)/15;

            bcolor.BackColor = Color.FromArgb(
                (int)(color.BackColor.R * f),
                (int)(color.BackColor.G * f),
                (int)(color.BackColor.B * f));
        }
    }
}
