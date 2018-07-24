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
    public partial class SpriteGridSettings : Form
    {
        public static Color GridColor { get; private set; }
        public static Color CenterSquareColor { get; private set; }
        public static Color SelectionRectangleColor { get; private set; }
        public static Color SelectedTilesColor { get; private set; }
        public static bool EnableCenterSquare { get; private set; }

        private SpriteGridSettings()
        {
            InitializeComponent();
            accept.Click += click;
            pictureBox1.DoubleClick += doubleClick;
            pictureBox2.DoubleClick += doubleClick;
            pictureBox3.DoubleClick += doubleClick;
            pictureBox4.DoubleClick += doubleClick;
            checkBox1.CheckedChanged += checkedChanged;
        }

        private void checkedChanged(object sender, EventArgs e)
        {
            setEnableCenterSquare(checkBox1.Checked);
        }

        private void doubleClick(object sender, EventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                if (pb == pictureBox1)
                    setGridColor(colorDialog1.Color);
                else if(pb == pictureBox2)
                    setCenterSquareColor(colorDialog1.Color);
                else if(pb == pictureBox3)
                    setSelectionRectangleColor(colorDialog1.Color);
                else
                    setSelectedTilesColor(colorDialog1.Color);
            }
        }

        private void click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Dispose();
        }

        public static DialogResult Show(IWin32Window Owner,
            Color GridColor,
            Color CenterSquareColor, Color SelectionRectangleColor,
            Color SelectedTilesColor, bool EnableCenterSquare)
        {
            SpriteGridSettings settings = new SpriteGridSettings();

            settings.setGridColor(GridColor);
            settings.setCenterSquareColor(CenterSquareColor);
            settings.setSelectionRectangleColor(SelectionRectangleColor);
            settings.setSelectedTilesColor(SelectedTilesColor);
            settings.setEnableCenterSquare(EnableCenterSquare);
            settings.checkBox1.Checked = EnableCenterSquare;

            return settings.ShowDialog(Owner);
        }

        private void setGridColor(Color C)
        {
            GridColor = C;
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            using (Graphics g = Graphics.FromImage(pictureBox1.Image))
            {
                Brush brush = new SolidBrush(C);
                g.FillRectangle(brush, 0, 0, pictureBox1.Width, pictureBox1.Height);
            }
            pictureBox1.Refresh();
        }
        private void setCenterSquareColor(Color C)
        {
            CenterSquareColor = C;
            pictureBox2.Image = new Bitmap(pictureBox2.Width, pictureBox2.Height);
            using (Graphics g = Graphics.FromImage(pictureBox2.Image))
            {
                Brush brush = new SolidBrush(C);
                g.FillRectangle(brush, 0, 0, pictureBox2.Width, pictureBox2.Height);
            }
            pictureBox2.Refresh();
        }
        private void setSelectionRectangleColor(Color C)
        {
            SelectionRectangleColor = C;
            pictureBox3.Image = new Bitmap(pictureBox3.Width, pictureBox3.Height);
            using (Graphics g = Graphics.FromImage(pictureBox3.Image))
            {
                Brush brush = new SolidBrush(C);
                g.FillRectangle(brush, 0, 0, pictureBox3.Width, pictureBox3.Height);
            }
            pictureBox3.Refresh();
        }
        private void setSelectedTilesColor(Color C)
        {
            SelectedTilesColor = C;
            pictureBox4.Image = new Bitmap(pictureBox4.Width, pictureBox4.Height);
            using (Graphics g = Graphics.FromImage(pictureBox4.Image))
            {
                Brush brush = new SolidBrush(C);
                g.FillRectangle(brush, 0, 0, pictureBox4.Width, pictureBox4.Height);
            }
            pictureBox4.Refresh();
        }
        private void setEnableCenterSquare(bool B)
        {
            EnableCenterSquare = B;
        }
    }
}
