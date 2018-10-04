using SMWControlibBackend.Logic.HDMA;
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
    public partial class NewHDMADialog : Form
    {
        public NewHDMADialog()
        {
            InitializeComponent();
            for (int i = 0; i < EffectType.EffectTypes.Length; i++)
            {
                type.Items.Add(EffectType.EffectTypes[i]);
            }
            type.SelectedIndexChanged += typeSelectedIndexChanged;
            EffectOptions eo;
            for (int i = 0; i < 6; i++)
            {
                eo = i;
                option.Items.Add(eo);
            }
            option.SelectedIndexChanged += optionSelectedIndexChanged;
        }

        private void optionSelectedIndexChanged(object sender, EventArgs e)
        {
            optionDesc.Text = ((EffectOptions)option.SelectedItem).Description;
        }

        private void typeSelectedIndexChanged(object sender, EventArgs e)
        {
            typeDesc.Text = ((EffectType)type.SelectedItem).Description;
        }

        public static new DialogResult Show(IWin32Window owner)
        {
            NewHDMADialog win = new NewHDMADialog();
            return win.ShowDialog(owner);
        }
    }
}
