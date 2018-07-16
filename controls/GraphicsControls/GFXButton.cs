using SMWControlibBackend.Graphics;
using System;
using System.IO;
using System.Windows.Forms;

namespace SMWControlibControls.GraphicsControls
{
    public partial class GFXButton : Button
    {
        private OpenFileDialog open;
        public GFXBox target;

        private BaseTile baseTile;
        public int BaseTile
        {   get
            {
                return (int)baseTile;
            }
            set
            {
                if (value < 0) baseTile = SMWControlibBackend.Graphics.BaseTile.None;
                else if (value == 0) baseTile = SMWControlibBackend.Graphics.BaseTile.Top;
                else baseTile = SMWControlibBackend.Graphics.BaseTile.Botton;
            }
        }
        public string StartFolder { get; set; } = "";

        private TileSize tilesize = TileSize.Size16x16;
        public int Tilesize
        {
            get
            {
                return (int)tilesize;
            }
            set
            {
                if (value <= 8) tilesize = TileSize.Size8x8;
                else tilesize = TileSize.Size16x16;
            }
        }
        public GFXButton()
        {
            InitializeComponent();
            open = new OpenFileDialog();
            open.Multiselect = false;
            open.Filter = "Binary Files (*.bin)|*.bin";
            open.CheckFileExists = true;
            open.CheckPathExists = true;
            Click += GFXButton_Click;
        }

        private void GFXButton_Click(object sender, EventArgs e)
        {
            if (target != null)
            {
                open.InitialDirectory = StartFolder;
                if (open.ShowDialog() == DialogResult.OK)
                {
                    if(File.Exists(open.FileName))
                    {
                        target.GetTiles(open.FileName, tilesize, baseTile);
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
