using SMWControlibBackend.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMWControlibControls.GraphicsControls
{
    public partial class PaletteButton : Button
    {
        private OpenFileDialog open;
        public PaletteBox target;

        public string StartFolder { get; set; } = "";

        public PaletteButton()
        {
            InitializeComponent();
            open = new OpenFileDialog();
            open.Multiselect = false;
            open.Filter = "Palette Files (*.pal)|*.pal";
            open.CheckFileExists = true;
            open.CheckPathExists = true;
            Click += PaletteButton_Click;
        }

        private void PaletteButton_Click(object sender, EventArgs e)
        {
            if (target != null)
            {
                open.InitialDirectory = StartFolder;
                if (open.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(open.FileName))
                    {
                        target.LoadPalette(open.FileName);
                    }
                }
            }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
    }
}
