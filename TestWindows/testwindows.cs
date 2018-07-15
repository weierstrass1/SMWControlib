using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using controls;
using backend;

namespace TestWindows
{
    public partial class testwindows : Form
    {
        public testwindows()
        {
            InitializeComponent();
            ColorPalette.GeneratePalette("Doom3.pal", 16);
            gfxBox1.GetTiles("Doom.bin", TileSize.Size16x16, BaseTile.Top);
            gfxBox1.GetTiles("Doom3.bin", TileSize.Size16x16, BaseTile.Botton);


        }
    }
}
