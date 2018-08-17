using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SMWControlibBackend.Graphics.Frames;

namespace SMWControlibControls.GraphicsControls
{
    public partial class FramesSettingsDialog : Form
    {
        private Frame[] frames;
        private FramesSettingsDialog()
        {
            InitializeComponent();
            accept.Click += click;
        }

        private void click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Dispose();
        }

        public static DialogResult Show(IWin32Window Owner,Frame[] Frames)
        {
            FramesSettingsDialog fsd = new FramesSettingsDialog
            {
                frames = Frames
            };
            fsd.init();
            return fsd.ShowDialog(Owner);
        }

        private void init()
        {
            buildTable();
        }

        private void validName(Frame fr)
        {
            bool notValid = true;
            while (notValid)
            {
                notValid = false;
                foreach (Frame f in frames)
                {
                    if (f.Name == fr.Name && f != fr)
                    {
                        notValid = true;
                        fr.Name += "0";
                        break;
                    }
                }
            }

        }

        private void buildTable()
        {
            tableLayoutPanel1.RowCount = frames.Length;
            tableLayoutPanel1.Height = frames.Length * 38;
            tableLayoutPanel1.RowStyles.Clear();
            ImageButton ibtn;
            Label lb;
            for (int i = 0; i < frames.Length; i++)
            {
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 38));
                lb = new Label
                {
                    Text = frames[i].Name,
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font(Font.FontFamily, 12, FontStyle.Bold)
                };
                
                tableLayoutPanel1.Controls.Add(lb, 0, i);
                ibtn = new ImageButton();
                if (i == 0) ibtn.Enabled = false;
                initImageButton(ibtn, Properties.Resources.upArrow);
                ibtn.Click += clickUp;
                tableLayoutPanel1.Controls.Add(ibtn, 1, i);
                ibtn = new ImageButton();
                if (i == frames.Length - 1) ibtn.Enabled = false;
                initImageButton(ibtn, Properties.Resources.downArrow);
                ibtn.Click += clickDown;
                tableLayoutPanel1.Controls.Add(ibtn, 2, i);
                ibtn = new ImageButton();
                initImageButton(ibtn, Properties.Resources.questionBlock);
                ibtn.Click += questionClick;
                tableLayoutPanel1.Controls.Add(ibtn, 3, i);
            }
        }

        private void questionClick(object sender, System.EventArgs e)
        {
            ImageButton ibtn = (ImageButton)sender;
            int row = tableLayoutPanel1.GetRow(ibtn);
            if (FrameInfoDialog.Show(ParentForm, frames[row])
                == DialogResult.OK)
            {
                validName(frames[row]);
                tableLayoutPanel1.GetControlFromPosition(0, row)
                    .Text = frames[row].Name;
            }
        }

        private void clickUp(object sender, System.EventArgs e)
        {
            ImageButton ibtn = (ImageButton)sender;
            int row = tableLayoutPanel1.GetRow(ibtn);
            if (row == 0) return;
            Frame aux = frames[row];
            frames[row] = frames[row - 1];
            frames[row - 1] = aux;

            Control c1 = tableLayoutPanel1.
                GetControlFromPosition(0, row);
            Control c2 = tableLayoutPanel1.
                GetControlFromPosition(0, row-1);
            tableLayoutPanel1.Controls.Remove(c1);
            tableLayoutPanel1.Controls.Remove(c2);
            tableLayoutPanel1.Controls.Add(c1, 0, row - 1);
            tableLayoutPanel1.Controls.Add(c2, 0, row);
        }

        private void clickDown(object sender, System.EventArgs e)
        {
            ImageButton ibtn = (ImageButton)sender;
            int row = tableLayoutPanel1.GetRow(ibtn);
            if (row == frames.Length - 1) return;
            Frame aux = frames[row];
            frames[row] = frames[row + 1];
            frames[row + 1] = aux;

            Control c1 = tableLayoutPanel1.
                GetControlFromPosition(0, row);
            Control c2 = tableLayoutPanel1.
                GetControlFromPosition(0, row + 1);
            tableLayoutPanel1.Controls.Remove(c1);
            tableLayoutPanel1.Controls.Remove(c2);
            tableLayoutPanel1.Controls.Add(c1, 0, row + 1);
            tableLayoutPanel1.Controls.Add(c2, 0, row);
        }

        private void initImageButton(ImageButton b,Image img)
        {
            b.BackgroundImageLayout = ImageLayout.Center;
            b.FlatAppearance.BorderColor = SystemColors.Control;
            b.FlatAppearance.BorderSize = 0;
            b.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            b.FlatAppearance.MouseOverBackColor = SystemColors.Control;
            b.FlatStyle = FlatStyle.Flat;
            b.OffSetX1 = 1;
            b.OffSetX2 = 4;
            b.OffSetY1 = 1;
            b.OffSetY2 = 4;
            b.Source = img;
            b.TabIndex = 1;
            b.Text = "";
            b.UseVisualStyleBackColor = true;
            b.Zoom = 2;
            b.Init();
        }
    }
}
