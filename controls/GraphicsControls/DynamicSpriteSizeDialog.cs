using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SMWControlibBackend.Graphics.Frames;

namespace SMWControlibControls.GraphicsControls
{
    public partial class DynamicSpriteSizeDialog : Form
    {
        public static DynamicSize DynSize = DynamicSize.DynamicSprite96x96;
        static string desc16 = "Dynamic sprite that use a space of 16x16 or less on the VRAM per frame.\n\nIt doesn't require 50% More Mode.\n\nIt can be of 60fps or 30fps.";
        static string desc1632 = "Dynamic sprite that use a space of 32x16 or less on the VRAM per frame.\n\nIt doesn't require 50% More Mode.\n\nIt can be of 60fps or 30fps.";
        static string desc32 = "Dynamic sprite that use a space of 64x16 or less on the VRAM per frame.\n\nIt doesn't require 50% More Mode.\n\nIt can be of 60fps or 30fps.";
        static string desc48 = "Dynamic sprite that use a space of 128x16 or less on the VRAM per frame.\n\nIt doesn't require 50% More Mode.\n\nIt can be of 60fps or 30fps.";
        static string desc64 = "Dynamic sprite that use a space of 128x32 or less on the VRAM per frame.\n\nIt doesn't require 50% More Mode.\n\nIt can be of 60fps or 30fps.";
        static string desc80 = "Dynamic sprite that use a space of 128x48 or less on the VRAM per frame.\n\nIt requires 50% More Mode.\n\nIt can be of 60fps or 30fps.";
        static string desc96 = "Dynamic sprite that use a space of 128x64 or less on the VRAM per frame.\n\nIt doesn't require 50% More Mode.\n\nIt can be only of 30fps.";
        static string desc112 = "Dynamic sprite that use a space of 128x96 or less on the VRAM per frame.\n\nIt requires 50% More Mode.\n\nIt can be only of 30fps.";
        public DynamicSpriteSizeDialog()
        {
            InitializeComponent();
            accept.Click += Accept_Click;
            s16.CheckedChanged += S16_CheckedChanged;
            s1632.CheckedChanged += S1632_CheckedChanged;
            s32.CheckedChanged += S32_CheckedChanged;
            s48.CheckedChanged += S48_CheckedChanged;
            s64.CheckedChanged += S64_CheckedChanged;
            s80.CheckedChanged += S80_CheckedChanged;
            s96.CheckedChanged += S96_CheckedChanged;
            s112.CheckedChanged += S112_CheckedChanged;
        }

        private void S1632_CheckedChanged(object sender, EventArgs e)
        {
            if (s1632.Checked)
            {
                desc.Text = desc1632;
                DynSize = DynamicSize.DynamicSprite32x16;
            }
        }

        private void S16_CheckedChanged(object sender, EventArgs e)
        {
            if (s16.Checked)
            {
                desc.Text = desc16;
                DynSize = DynamicSize.DynamicSprite16x16;
            }
        }

        private void S112_CheckedChanged(object sender, EventArgs e)
        {
            if (s112.Checked)
            {
                desc.Text = desc112;
                DynSize = DynamicSize.DynamicSprite112x112;
            }
        }

        private void S96_CheckedChanged(object sender, EventArgs e)
        {
            if (s96.Checked)
            {
                desc.Text = desc96;
                DynSize = DynamicSize.DynamicSprite96x96;
            }
        }

        private void S80_CheckedChanged(object sender, EventArgs e)
        {
            if (s80.Checked)
            {
                desc.Text = desc80;
                DynSize = DynamicSize.DynamicSprite80x80;
            }
        }

        private void S64_CheckedChanged(object sender, EventArgs e)
        {
            if (s64.Checked)
            {
                desc.Text = desc64;
                DynSize = DynamicSize.DynamicSprite64x64;
            }
        }

        private void S48_CheckedChanged(object sender, EventArgs e)
        {
            if (s48.Checked)
            {
                desc.Text = desc48;
                DynSize = DynamicSize.DynamicSprite48x48;
            }
        }

        private void S32_CheckedChanged(object sender, EventArgs e)
        {
            if(s32.Checked)
            {
                desc.Text = desc32;
                DynSize = DynamicSize.DynamicSprite32x32;
            }
        }

        private void Accept_Click(object sender, EventArgs e)
        {
            
            DialogResult = DialogResult.OK;
            Dispose();
        }

        public static new DialogResult Show(IWin32Window Owner)
        {
            DynamicSpriteSizeDialog dssd = new DynamicSpriteSizeDialog();
            return dssd.ShowDialog(Owner);
        }
    }
}
