using System;
using System.Windows.Forms;
using SMWControlibBackend.Logic.HDMA;

namespace SMWControlibControls.LogicControls
{
    public partial class HDMAControl : UserControl
    {
        HDMA hdma;
        public HDMAControl()
        {
            hdma = new HDMA(new Effect(EffectType.Brightness, EffectOptions.Option0Static));
            InitializeComponent();
            Resize += resize;
            add.Click += addClick;
        }
        public HDMAControl(HDMA HDMA)
        {
            hdma = HDMA;
            InitializeComponent();
            Resize += resize;
            add.Click += addClick;
        }

        private void addClick(object sender, EventArgs e)
        {
            if (LineOptionsDialog.Show(ParentForm, hdma) == DialogResult.OK) 
            {
                hdmaWindow1.UpdateLayers();
            }
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
