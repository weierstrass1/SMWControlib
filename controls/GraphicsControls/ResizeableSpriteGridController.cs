using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SMWControlibBackend.Graphics;

namespace SMWControlibControls.GraphicsControls
{
    public partial class ResizeableSpriteGridController : UserControl
    {
        public List<TileMask>Tiles
        {
            set
            {
                spriteGridController1.Tiles = value;
            }
        }
        public TileMask[] NewTiles
        {
            set
            {
                spriteGridController1.NewTiles = value;
            }
        }
        public ResizeableSpriteGridController()
        {
            InitializeComponent();
            SizeChanged += sizeChanged;
            spriteGridController1.Init();
        }

        public void DeleteSelection()
        {
            spriteGridController1.DeleteSelection();
        }
        private void sizeChanged(object sender, EventArgs e)
        {
            section1.MinimumSize = new Size(spriteGridController1.MinimumSize.Width + 4,
                spriteGridController1.MinimumSize.Height + 4);
            if (section1.Width <= spriteGridController1.MaximumSize.Width + 4)
            {
                left.Width = 0;
                right.Width = 0;
            }
            else
            {
                int adder = section1.Width - spriteGridController1.MaximumSize.Width - 4;
                adder /= 2;
                left.Width = adder;
                right.Width = adder;
            }
            if (section1.Height <= spriteGridController1.MaximumSize.Height + 4)
            {
                up.Height = 0;
                down.Height = 0;
            }
            else
            {
                int adder = section1.Height - spriteGridController1.MaximumSize.Height - 4;
                adder /= 2;
                up.Height = adder;
                down.Height = adder;
            }
        }
    }
}
