using SMWControlibBackend.Graphics;
using SMWControlibBackend.Graphics.Frames;
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
    public partial class FrameInfoDialog : Form
    {
        private Frame frame;
        private FrameInfoDialog()
        {
            InitializeComponent();
            accept.Click += click;
            name.TextChanged += textChanged;
        }

        private void textChanged(object sender, EventArgs e)
        {
            int st = name.SelectionStart;
            bool invalidChar = false;
            StringBuilder sb = new StringBuilder();
            foreach (char c in name.Text)
            {
                if ((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z')
                    || (c >= '0' && c <= '9') || c == '-' || c == '_')
                {
                    sb.Append(c);
                }
                else
                {
                    invalidChar = true;
                }
            }

            if (invalidChar)
            {
                name.Text = sb.ToString();
                if (st - 1 < 0) st = 1;
                name.SelectionStart = st - 1;
            }
            else name.SelectionStart = st;


        }

        private void click(object sender, EventArgs e)
        {
            if (name.Text == null || name.Text.Length == 0 || name.Text == "" ||
                (!(name.Text[0] >= 'a' && name.Text[0] <= 'z') &&
                !(name.Text[0] >= 'A' && name.Text[0] <= 'Z')))
                name.Text = "f" + name.Text;
            frame.Name = name.Text;

            DialogResult = DialogResult.OK;
            Dispose();
        }

        public static DialogResult Show(IWin32Window Owner, Frame f)
        {
            FrameInfoDialog fid = new FrameInfoDialog
            {
                frame = f
            };

            int i = 0;

            fid.xdisp.Text = "db ";
            fid.ydisp.Text = "db ";
            fid.props.Text = "db ";
            fid.tiles.Text = "db ";
            fid.sizes.Text = "db ";

            foreach (TileMask t in f.Tiles)
            {
                if (i % 12 == 0 && i != 0)
                {
                    fid.xdisp.Text = fid.xdisp.Text.Remove(fid.xdisp.Text.Length - 1, 1);
                    fid.ydisp.Text = fid.ydisp.Text.Remove(fid.ydisp.Text.Length - 1, 1);
                    fid.props.Text = fid.props.Text.Remove(fid.props.Text.Length - 1, 1);
                    fid.tiles.Text = fid.tiles.Text.Remove(fid.tiles.Text.Length - 1, 1);
                    fid.sizes.Text = fid.sizes.Text.Remove(fid.sizes.Text.Length - 1, 1);

                    fid.xdisp.Text += Environment.NewLine + "db ";
                    fid.ydisp.Text += Environment.NewLine + "db ";
                    fid.props.Text += Environment.NewLine + "db ";
                    fid.tiles.Text += Environment.NewLine + "db ";
                    fid.sizes.Text += Environment.NewLine + "db ";
                }
                fid.xdisp.Text += t.XDispString + ",";
                fid.ydisp.Text += t.YDispString + ",";
                fid.props.Text += t.Properties + ",";
                fid.tiles.Text += t.Tile + ",";
                fid.sizes.Text += t.SizeString + ",";
                i++;
            }
            if (fid.xdisp.Text[fid.xdisp.Text.Length - 1] == ',')
            {
                fid.xdisp.Text = fid.xdisp.Text.Remove(fid.xdisp.Text.Length - 1, 1);
                fid.ydisp.Text = fid.ydisp.Text.Remove(fid.ydisp.Text.Length - 1, 1);
                fid.props.Text = fid.props.Text.Remove(fid.props.Text.Length - 1, 1);
                fid.tiles.Text = fid.tiles.Text.Remove(fid.tiles.Text.Length - 1, 1);
                fid.sizes.Text = fid.sizes.Text.Remove(fid.sizes.Text.Length - 1, 1);
            }

            fid.name.Text = f.Name;

            return fid.ShowDialog(Owner);
        }
    }
}
