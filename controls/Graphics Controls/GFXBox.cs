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
        zoom tilezoom = backend.Zoom.x1;
        public zoom TileZoom { get { return tilezoom; }
            set
            {
                tilezoom = value;
                if (selectedTiles != null)
                {
                    GetTilesFromSelection(value);
                }
            }
        }
        Tile[,] tiles16;
        Tile[,] tiles8;
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
        public event Action SelectionChanged;
        #endregion

        public GFXBox()
        {
            InitializeComponent();
            BehindBitmap = new Bitmap(Size.Width, Size.Height);
            MouseDown += GFXBox_MouseDown;
            MouseMove += GFXBox_MouseMove;
            MouseUp += GFXBox_MouseUp;
            SizeChanged += GFXBox_SizeChanged;
        }

        public Tuple<int,int,Bitmap>[] GetBitmapsFromSelectedTiles()
        {
            if (selectedTiles == null) return null;
            Tuple<int, int, Bitmap>[] tuples = 
                new Tuple<int, int, Bitmap>[selectedTiles.GetLength(0) * selectedTiles.GetLength(1)];

            int w = selectedTiles.GetLength(0) - 1;
            int h = selectedTiles.GetLength(1) - 1;

            int k = 0;
            int x, y;
            char c1, c2;
            c1 = selectedTiles[0, 0].Code[2];
            c2 = selectedTiles[w, 0].Code[2];

            bool even1 = (c1 % 2 == 0 && c1 > '9')
                        || (c1 % 2 == 1 && c1 <= '9');

            bool even2 = (c2 % 2 == 0 && c2 > '9')
                        || (c2 % 2 == 1 && c2 <= '9');

            bool oddX = even1 != even2;

            c1 = selectedTiles[0, 0].Code[1];
            c2 = selectedTiles[0, h].Code[1];

            even1 = (c1 % 2 == 0 && c1 > '9')
            || (c1 % 2 == 1 && c1 <= '9');

            even2 = (c2 % 2 == 0 && c2 > '9')
                        || (c2 % 2 == 1 && c2 <= '9');
            bool oddY = even1 != even2;

            x = 0;
            int zsiz = tileSize * zoom;
            for (int i = 0; i < selectedTiles.GetLength(0); i++, x += zsiz)
            {
                if (oddX && i == w)
                    x -= (zsiz / 2);

                y = 0;
                for (int j = 0; j < selectedTiles.GetLength(1); j++, y += zsiz)
                {
                    if (oddY && j == h)
                        y -= (zsiz / 2);
                    tuples[k] = new Tuple<int, int, Bitmap>(x, y, selectedTiles[i, j].GetImage(5, zoom));
                    k++;
                }
            }
            return tuples;
        }

        private void GFXBox_SizeChanged(object sender, EventArgs e)
        {
            BehindBitmap = new Bitmap(Size.Width, Size.Height);
        }

        #region Mouse
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

            int xlim = Width - (selectionMinSize * zoom);
            int ylim = Height - (selectionMinSize * zoom);

            if (minx > xlim) minx = xlim;
            if (miny > ylim) miny = ylim;

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
            GetTilesFromSelection(TileZoom);
            SelectionChanged?.Invoke();
        }
        #endregion
        public void GetTiles(string path,TileSize tileSize, BaseTile baseTile)
        {
            try
            {
                byte[,] colors = SnesGraphics.generateGFX(path);
                int y = 0;
                if (baseTile == BaseTile.Botton) y = Height / 2;
                Bitmap bp = SnesGraphics.GenerateBitmapFromColorMatrix(colors, zoom, ColorPalette.GetPalette(5));
                using (Graphics g = Graphics.FromImage(behindBitmap))
                {
                    g.DrawImage(bp, 0, y, bp.Width, bp.Height);
                }
                BehindBitmap = behindBitmap;

                this.tileSize = (int)tileSize;
                Tile[,] tils = Tile.GenerateTilesFromColorMatrix(colors, tileSize, baseTile, out baseTile);
                switch(baseTile)
                {
                    case BaseTile.None:
                        if (this.tileSize == 8) tiles8 = tils;
                        else if (this.tileSize == 16) tiles16 = tils;
                        break;
                    default:
                        Tile[,] tiles = null;
                        int adder = 0;
                        int jFusion = 8;
                        int xlim = 16;
                        int ylim = 16;
                        if (this.tileSize == 8)
                        {
                            if (tiles8 == null) tiles8 = new Tile[16, 16];
                            tiles = tiles8;
                            if (baseTile == BaseTile.Botton) adder = 4;
                        }
                        else if (this.tileSize == 16)
                        {
                            if (tiles16 == null) tiles16 = new Tile[15, 15];
                            tiles = tiles16;
                            xlim = 15;
                            ylim = 15;
                            if (baseTile == BaseTile.Botton)
                            {
                                adder = 8;
                                jFusion = 0;
                            }
                        }

                        for (int i = 0; i < xlim; i++)
                        {
                            for (int j = 0; j < tils.GetLength(1) && j + adder < ylim; j++)
                            {
                                if (j == jFusion && this.tileSize == 16)
                                {
                                    tiles[i, j + adder] = 
                                        Tile.fusionTiles(tiles[i, j + adder], tils[i, j], baseTile);
                                }
                                else
                                {
                                    tiles[i, j + adder] = tils[i, j];
                                }
                            }
                        }
                        break;
                }
            }
            catch
            {
                MessageBox.Show("The GFX is invalid. Must be of 4 or 8 Kb (4096 or 8192 bytes).", 
                    "Invalid GFX Error",
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
            }
        }

        private void GetTilesFromSelection(zoom tileZoom)
        {
            Tile[,] tiles = tiles16;
            if (tileSize == 8) tiles = tiles8;
            if (tiles == null) return;

            int xarr = selection.X / (8 * zoom);
            int yarr = selection.Y / (8 * zoom);
            if (xarr >= tiles.GetLength(0)) xarr = tiles.GetLength(0) - 1;
            if (yarr >= tiles.GetLength(1)) yarr = tiles.GetLength(1) - 1;

            int xlim = selection.Width / (8 * zoom);
            int ylim = selection.Height / (8 * zoom);

            selectedTiles = new Tile[(xlim / 2) + xlim % 2, (ylim / 2) + ylim % 2];
            int p = 0, q = 0;
            for (int i = 0; i < xlim; p++)
            {
                q = 0;
                for (int j = 0; j < ylim; q++)
                {
                    selectedTiles[p, q] = tiles[xarr + i, yarr + j];
                    if (selectedTiles[p, q] != null)
                        selectedTiles[p, q].GenerateBitmap(5, tileZoom);

                    if (tileSize == 8 || (ylim % 2 != 0 && j + 3 == ylim))
                    {
                        j++;
                    }
                    else
                        j += 2;
                }
                if (tileSize == 8 || (xlim % 2 != 0 && i + 3 == xlim))
                {
                    i++;
                }
                else
                    i += 2;
            }
        }

        private void SetBehindBitmap(Bitmap newBitmap)
        {
            if (newBitmap == null) newBitmap = new Bitmap(Width, Height);
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
