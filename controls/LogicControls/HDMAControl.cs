using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMWControlibControls.LogicControls
{
    public partial class HDMAControl : UserControl
    {
        public HDMAControl()
        {
            InitializeComponent();
            Resize += resize;
        }

        private void resize(object sender, EventArgs e)
        {
            int w = leftSection.Width - (hdmaWindow1.Width + 8);
            if (w < 0) w = 0;
            w /= 2;
            int h = leftSection.Height - (hdmaWindow1.Height + 8);
            if (h < 0) h = 0;
            h /= 2;
            left.Width = w;
            right.Width = w;
            top.Height = h;
            bottom.Height = h;
        }
    }
}
