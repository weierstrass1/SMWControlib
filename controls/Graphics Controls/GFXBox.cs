using backend;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace controls
{
    public partial class GFXBox : PictureBox
    {
        #region Properties
        Bitmap behindBitmap;
        Rectangle selection;
        Pen selectionColor = Pens.White;
        int selectionMinSize = 1;
        int selectionAccuracy = 1;
        int zoom = 1;
        int tileSize = 16;
        Tile[,] tiles;
        Tile[,] selectedTiles;

        [
            Category("Image"),
            Description("The main image loaded from gfx.")
        ]
        public Bitmap BehindBitmap
        {
            get
            {
                return behindBitmap;
            }
            set
            {
                SetBehindBitmap(value);
            }
        }

        [
            Category("Rectangle"),
            Description("A rectangle that is used to select zones of the gfx.")
        ]
        public Rectangle Selection
        {
            get
            {
                return selection;
            }
            set
            {
                SetSelection(value);
            }
        }

        [
            Category("Pen"),
            Description("Color of the selection.")
        ]
        public Pen SelectionColor
        {
            get
            {
                return selectionColor;
            }
            set
            {
                SetSeletionColor(value);
            }
        }

        [
            Category("Size"),
            Description("Minimum size of the selection.")
        ]
        public int SelectionMinSize
        {
            get
            {
                return selectionMinSize;
            }
            set
            {
                SetSelectionMinSize(value);
            }
        }

        [
            Category("Accuracy"),
            Description("Used to select zones easily.")
        ]
        public int SelectionAccuracy
        {
            get
            {
                return selectionAccuracy;
            }
            set
            {
                SetSelectionAccuracy(value);
            }
        }

        [
            Category("Zoom"),
            Description("Used to applify gfx.")
        ]
        public int Zoom
        {
            get
            {
                return zoom;
            }
            set
            {
                SetZoom(value);
            }
        }
        int selectionStartX, selectionStartY;
        int selectionEndX, selectionEndY;
        bool selecting = false;
        #endregion

        public GFXBox()
        {
            InitializeComponent();
            behindBitmap = new Bitmap(Width, Height);
            MouseDown += GFXBox_MouseDown;
            MouseMove += GFXBox_MouseMove;
            MouseUp += GFXBox_MouseUp;
        }

        private void GFXBox_MouseDown(object sender, MouseEventArgs e)
        {
            selectionStartX = e.X;
            selectionStartY = e.Y;

            if (selectionStartX < 0) selectionStartX = 0;
            if (selectionStartY < 0) selectionStartY = 0;

            if (selectionStartX > Width) selectionStartX = Width;
            if (selectionStartY > Height) selectionStartY = Height;

            selecting = true;
        }

        private void GFXBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (!selecting) return;

            selectionEndX = e.X;
            selectionEndY = e.Y;

            if (selectionEndX < 0) selectionEndX = 0;
            if (selectionEndY < 0) selectionEndY = 0;

            if (selectionEndX > Width) selectionEndX = Width;
            if (selectionEndY > Height) selectionEndY = Height;

            int minx = Math.Min(selectionStartX, selectionEndX);
            int miny = Math.Min(selectionStartY, selectionEndY);
            int maxx = Math.Max(selectionStartX, selectionEndX);
            int maxy = Math.Max(selectionStartY, selectionEndY);

            int zacc = selectionAccuracy * zoom;
            if (maxx == selectionStartX) maxx -= (maxx % zacc);
            if (maxy == selectionStartY) maxy -= (maxy % zacc);

            int w = maxx - minx;
            int h = maxy - miny;
            

            Selection = new Rectangle(minx, miny, w, h);
        }

        private void GFXBox_MouseUp(object sender, MouseEventArgs e)
        {
            GFXBox_MouseMove(sender, e);
            selecting = false;
            if (tiles != null) GetTilesFromSelection();
        }

        public void GetTiles(string path,TileSize tileSize, BaseTile baseTile)
        {
            try
            {
                byte[,] colors = SnesGraphics.generateGFX("Doom3.bin");
                BehindBitmap = SnesGraphics.GenerateBitmapFromColorMatrix(colors, zoom, ColorPalette.GetPalette(5));
                this.tileSize = (int)tileSize;
                Tile[,] tils = Tile.GenerateTilesFromColorMatrix(colors, tileSize, baseTile);

            }
            catch
            {
                MessageBox.Show("The GFX is invalid. Must be of 4 or 8 Kb (4096 or 8192 bytes).", 
                    "Invalid GFX Error",
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
            }
        }

        private void GetTilesFromSelection()
        {
            int xarr = selection.X / (8 * zoom);
            int yarr = selection.Y / (8 * zoom);
            if (xarr >= tiles.GetLength(0)) xarr = tiles.GetLength(0) - 1;
            if (yarr >= tiles.GetLength(1)) yarr = tiles.GetLength(1) - 1;

            int xlim = selection.Width / (8 * zoom);
            int ylim = selection.Height / (8 * zoom);

            selectedTiles = new Tile[xlim, ylim];

            for (int i = 0; i < xlim;)
            {
                for (int j = 0; j < ylim;)
                {
                    selectedTiles[i, j] = tiles[xarr + i, yarr + j];
                    selectedTiles[i, j].GenerateBitmap(5, backend.Zoom.x4);

                    if (tileSize == 8 || (ylim % 2 != 0 && j + 2 >= ylim)) j++;
                    else j += 2;
                }
                if (tileSize == 8 || (xlim % 2 != 0 && i + 2 >= xlim)) i++;
                else i += 2;
            }
        }

        private void SetBehindBitmap(Bitmap newBitmap)
        {
            behindBitmap = newBitmap;
            Image = behindBitmap;
            ReDraw();
        }

        private void SetSelection(Rectangle rect)
        {
            if (rect == null) return;

            int zacc = selectionAccuracy * zoom;
            int zmsz = selectionMinSize * zoom;

            rect.X = (rect.X / zacc) * zacc;
            rect.Y = (rect.Y / zacc) * zacc;

            int modw = rect.Width % (zacc);
            if (modw != 0) rect.Width += (zacc - modw);
            rect.Width = Math.Max(rect.Width, zmsz);

            int modh = rect.Height % (zacc);
            if (modh != 0) rect.Height += (zacc - modh);
            rect.Height = Math.Max(rect.Height, zmsz);

            selection = rect;
            ReDraw();
        }

        private void SetSeletionColor(Pen color)
        {
            selectionColor = color;
            ReDraw();
        }

        private void SetSelectionMinSize(int size)
        {
            if (size <= 0) return;
            selectionMinSize = size;
            Selection = selection;
        }

        private void SetSelectionAccuracy(int accuracy)
        {
            if (accuracy <= 0) return;
            selectionAccuracy = accuracy;
            Selection = selection;
        }

        private void SetZoom(int newZoom)
        {
            if (newZoom <= 0) return;

            zoom = newZoom;
            SelectionAccuracy = selectionAccuracy;
            SelectionMinSize = selectionMinSize;
        }

        private void ReDraw()
        {
            if (Image == null) return;
            Image = new Bitmap(Image.Width, Image.Height);
            using (Graphics g = Graphics.FromImage(Image))
            {
                g.DrawImage(behindBitmap, 0, 0);
                if (selection != null)
                {
                    g.DrawRectangle(selectionColor, selection);
                }
            }
            Refresh();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
    }
}
