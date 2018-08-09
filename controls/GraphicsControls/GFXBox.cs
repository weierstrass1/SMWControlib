using SMWControlibBackend.Graphics;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SMWControlibControls.GraphicsControls
{
    public partial class GFXBox : PictureBox
    {
        #region Properties
        private Bitmap behindBitmap;
        private Rectangle selection;
        private Pen selectionColor = Pens.White;
        private int selectionMinSize = 1;
        private int selectionAccuracy = 1;
        private int zoom = 1;
        private int tileSize = 16;
        private Zoom tilezoom = SMWControlibBackend.Graphics.Zoom.x1;

        private TileSP sp;
        public int SP
        {
            get
            {
                return (int)sp;
            }
            set
            {
                if (value == 0) sp = TileSP.SP01;
                else sp = TileSP.SP23;
            }
        }

        [
            Category("Int"),
            Description("Zoom of the bitmaps generated from tiles.")
        ]
        public int TileZoom { get { return tilezoom; }
            set
            {
                tilezoom = value;
                if (selectedTiles != null)
                {
                    getTilesFromSelection();
                }
            }
        }

        private Tile[,] tiles16;
        private Tile[,] tiles8;
        private Tile[,] selectedTiles;

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
                setBehindBitmap(value);
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
                setSelection(value);
            }
        }

        [
            Category("Pen"),
            Description("Color of the selection.")
        ]
        public Color SelectionColor
        {
            get
            {
                return selectionColor.Color;
            }
            set
            {
                setSeletionColor(value);
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
                setSelectionMinSize(value);
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
                setSelectionAccuracy(value);
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
                setZoom(value);
            }
        }

        private int selectionStartX, selectionStartY;
        private int selectionEndX, selectionEndY;
        private bool selecting = false;
        public event Action SelectionChanged;
        bool tilesNotFiled16 = true;
        bool tilesNotFiled8 = true;
        #endregion

        public GFXBox()
        {
            InitializeComponent();
            BehindBitmap = new Bitmap(Size.Width, Size.Height);
            MouseDown += mouseDown;
            MouseMove += mouseMove;
            MouseUp += mouseUp;
            SizeChanged += sizeChanged;
        }

        private void oneGlobalPaletteChange(PaletteId obj)
        {
            if(ColorPalette.SelectedPalette == obj)
                reDraw();
        }

        private void globalPaletteChange()
        {
            reDraw();
        }

        public TileMask[] GetBitmapsFromSelectedTiles(bool flipX, bool flipY, TilePriority priority)
        {
            if (selectedTiles == null) return null;
            TileMask[] tms = 
                new TileMask[selectedTiles.GetLength(0) * selectedTiles.GetLength(1)];

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
            int zsiz = tileSize * TileZoom;
            for (int i = 0; i < selectedTiles.GetLength(0); i++, x += zsiz)
            {
                if (oddX && i == w)
                    x -= (zsiz / 2);

                y = 0;
                for (int j = 0; j < selectedTiles.GetLength(1); j++, y += zsiz)
                {
                    if (oddY && j == h)
                        y -= (zsiz / 2);
                    tms[k] = new TileMask(sp, selectedTiles[i, j], TileZoom, flipX, flipY)
                    {
                        Priority = priority,
                        XDisp = x,
                        YDisp = y
                    };
                    k++;
                }
            }
            return tms;
        }

        private void sizeChanged(object sender, EventArgs e)
        {
            BehindBitmap = new Bitmap(Size.Width, Size.Height);
        }

        #region Mouse
        private void mouseDown(object sender, MouseEventArgs e)
        {
            selectionStartX = e.X;
            selectionStartY = e.Y;

            if (selectionStartX < 0) selectionStartX = 0;
            if (selectionStartY < 0) selectionStartY = 0;

            if (selectionStartX > Width) selectionStartX = Width;
            if (selectionStartY > Height) selectionStartY = Height;

            selecting = true;
        }

        private void mouseMove(object sender, MouseEventArgs e)
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
            if (maxx == selectionStartX) maxx += (zacc - (maxx % zacc));
            if (maxy == selectionStartY) maxy += (zacc - (maxy % zacc));

            int w = maxx - minx;
            int h = maxy - miny;
            

            Selection = new Rectangle(minx, miny, w, h);
        }

        private void mouseUp(object sender, MouseEventArgs e)
        {
            mouseMove(sender, e);
            selecting = false;
            getTilesFromSelection();
            SelectionChanged?.Invoke();
        }
        #endregion

        public void GetTiles(string path,TileSize tileSize, BaseTile baseTile)
        {
            try
            {
                int l = File.ReadAllBytes(path).Length;
                if (l > 8192)
                {
                    MessageBox.Show("The GFX must be of 8192 or less.",
                        "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }
                byte[,] colors = SnesGraphics.GenerateGFX(path);
                int y = 0;
                if (baseTile == BaseTile.Botton) y = Height / 2;
                Bitmap bp = SnesGraphics.GenerateBitmapFromColorMatrix(colors, zoom);
                using (Graphics g = Graphics.FromImage(behindBitmap))
                {
                    g.DrawImage(bp, 0, y, bp.Width, bp.Height);
                }
                BehindBitmap = behindBitmap;

                this.tileSize = (int)tileSize;
                Tile[,] tils = Tile.GenerateTilesFromColorMatrix
                    (colors, tileSize, baseTile, out baseTile);
                Tile[,] tiles = null;

                switch (baseTile)
                {
                    case BaseTile.None:
                        bool go = false;
                        if (this.tileSize == 8)
                        {
                            if (tiles8 == null)
                            {
                                tiles8 = tils;
                                ColorPalette.SelectedGlobalPaletteChange += globalPaletteChange;
                                ColorPalette.GlobalPalletesChange += globalPaletteChange;
                                ColorPalette.OneGlobalPaletteChange += oneGlobalPaletteChange;
                            }
                            else
                            {
                                tiles = tiles8;
                                go = true;
                            }
                            tilesNotFiled8 = false;
                        }
                        else if (this.tileSize == 16)
                        {
                            if (tiles16 == null)
                            {
                                tiles16 = tils;
                                ColorPalette.SelectedGlobalPaletteChange += globalPaletteChange;
                                ColorPalette.GlobalPalletesChange += globalPaletteChange;
                                ColorPalette.OneGlobalPaletteChange += oneGlobalPaletteChange;
                            }
                            else
                            {
                                tiles = tiles16;
                                go = true;
                            }
                            tilesNotFiled16 = false;
                        }
                        if (go)
                        {
                            for (int i = 0; i < tiles.GetLength(0); i++)
                            {
                                for (int j = 0; j < tiles.GetLength(1); j++)
                                {
                                    if (tiles[i, j] != null)
                                        Tile.fusionTiles(tiles[i, j], tils[i, j], BaseTile.None);
                                    else tiles[i, j] = tils[i, j];
                                }
                            }
                        }
                        break;
                    default:
                        tiles = null;
                        int adder = 0;
                        int jFusion = 7;
                        int xlim = 16;
                        int ylim = 16;
                        if (this.tileSize == 8)
                        {
                            if (tiles8 == null) tiles8 = new Tile[16, 16];
                            tiles = tiles8;
                            if (baseTile == BaseTile.Botton)
                            {
                                adder = 8;
                            }
                        }
                        else if (this.tileSize == 16)
                        {
                            if (tiles16 == null) tiles16 = new Tile[15, 15];
                            tiles = tiles16;
                            xlim = 15;
                            ylim = 15;
                            if (baseTile == BaseTile.Botton)
                            {
                                adder = 7;
                                jFusion = 0;
                            }
                        }

                        int canFusion;
                        for (int i = 0; i < xlim; i++)
                        {
                            canFusion = 0;
                            for (int j = 0; j + canFusion < tils.GetLength(1) && j + adder + canFusion < ylim; j++)
                            {
                                if (canFusion == 0 && j == jFusion && this.tileSize == 16)
                                {
                                    tiles[i, j + adder] =
                                        Tile.fusionTiles(tiles[i, j + adder], tils[i, j], baseTile);
                                    canFusion = 1;
                                    j--;
                                }
                                else
                                {                                        
                                    if (tiles[i, j + adder + canFusion] == null)
                                        tiles[i, j + adder + canFusion] = tils[i, j];
                                    else
                                        Tile.fusionTiles(tiles[i, j + adder + canFusion], tils[i, j], BaseTile.None);

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
            if (tilesNotFiled16 && tiles16 != null)
            {
                Tile.FillTileMatrix(tiles16, TileSize.Size16x16, BaseTile.Top);
                tilesNotFiled16 = false;
                ColorPalette.SelectedGlobalPaletteChange += globalPaletteChange;
                ColorPalette.GlobalPalletesChange += globalPaletteChange;
                ColorPalette.OneGlobalPaletteChange += oneGlobalPaletteChange;
            }
            if (tilesNotFiled8 && tiles8 != null)
            {
                Tile.FillTileMatrix(tiles8, TileSize.Size8x8, BaseTile.Top);
                tilesNotFiled8 = false;
                ColorPalette.SelectedGlobalPaletteChange += globalPaletteChange;
                ColorPalette.GlobalPalletesChange += globalPaletteChange;
                ColorPalette.OneGlobalPaletteChange += oneGlobalPaletteChange;
            }
            reDraw();
        }

        private void getTilesFromSelection()
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

            if (tileSize == 16) selectedTiles = new Tile[(xlim / 2) + xlim % 2, (ylim / 2) + ylim % 2];
            else selectedTiles = new Tile[xlim, ylim];

            int p = 0, q = 0;
            for (int i = 0; i < xlim; p++)
            {
                q = 0;
                for (int j = 0; j < ylim; q++)
                {
                    selectedTiles[p, q] = tiles[xarr + i, yarr + j];

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

        private void setBehindBitmap(Bitmap newBitmap)
        {
            if (newBitmap == null) newBitmap = new Bitmap(Width, Height);
            behindBitmap = newBitmap;
            Image = behindBitmap;
            reDraw();
        }

        private void setSelection(Rectangle rect)
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
            reDraw();
        }

        private void setSeletionColor(Color color)
        {
            selectionColor = new Pen(color);
            reDraw();
        }

        private void setSelectionMinSize(int size)
        {
            if (size <= 0) return;
            selectionMinSize = size;
            Selection = selection;
        }

        private void setSelectionAccuracy(int accuracy)
        {
            if (accuracy <= 0) return;
            selectionAccuracy = accuracy;
            Selection = selection;
        }

        private void setZoom(int newZoom)
        {
            if (newZoom <= 0) return;

            zoom = newZoom;
            SelectionAccuracy = selectionAccuracy;
            SelectionMinSize = selectionMinSize;
        }

        private void reDraw()
        {
            Image = new Bitmap(Image.Width, Image.Height);
            try
            {
                using (Graphics g = Graphics.FromImage(Image))
                {
                    Brush br = new SolidBrush(ColorPalette.GetGlobalColor(0));
                    g.FillRectangle(br, 0, 0, Width, Height);
                }
            }
            catch { }
            

                Tile[,] tiles = tiles16;
            int up = 2;
            if (tileSize == 8)
            {
                tiles = tiles8;
                up = 1;
            }
            if(tiles!=null)
            {
                behindBitmap = new Bitmap(tiles.GetLength(0) * tileSize * zoom,
                    tiles.GetLength(1) * tileSize * zoom);
                Bitmap bp;
                int zsiz;
                for (int i = 0; i < tiles.GetLength(0); i += up)
                {
                    for (int j = 0; j <= tiles.GetLength(1); j += up)
                    {
                        using (Graphics g = Graphics.FromImage(behindBitmap))
                        {
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                            bp = tiles[i, j].GetImage
                                (ColorPalette.SelectedPalette, zoom);
                            zsiz = tiles[i, j].Size * zoom;
                            g.DrawImage
                                (bp, (i / up) * zsiz, (j / up) * zsiz, 
                                zsiz, zsiz);
                        }
                    }
                }
            }
            using (Graphics g = Graphics.FromImage(Image))
            {
                if (behindBitmap != null)
                    g.DrawImage(behindBitmap, 0, 0);
                if (selection != null)
                    g.DrawRectangle(selectionColor, selection);
            }
            Refresh();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
    }
}
