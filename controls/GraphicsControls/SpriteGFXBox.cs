using SMWControlibBackend.Graphics;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SMWControlibControls.GraphicsControls
{
    public partial class SpriteGFXBox : GFXBox
    {
        private Rectangle selection;
        private Pen selectionColor = Pens.White;
        private int selectionMinSize = 8;
        private int selectionAccuracy = 8;
        private Zoom tilezoom = SMWControlibBackend.Graphics.Zoom.X1;
        private TileSP sp;
        public Tile[,] Tiles16 { get; private set; }
        public Tile[,] Tiles8 { get; private set; }
        private Tile[,] selectedTiles;
        private int selectionStartX, selectionStartY;
        private int selectionEndX, selectionEndY;
        private bool selecting = false;
        public event Action<object> SelectionChanged;
        public event Action GraphicsLoaded;

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
            Category("Int"),
            Description("Zoom of the bitmaps generated from tiles.")
        ]
        public int TileZoom
        {
            get { return tilezoom; }
            set
            {
                tilezoom = value;
                if (selectedTiles != null)
                {
                    getTilesFromSelection();
                }
            }
        }

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

        public SpriteGFXBox()
        {
            InitializeComponent();
            MouseDown += mouseDown;
            MouseMove += mouseMove;
            MouseUp += mouseUp;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
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
            int tileSize = 16;
            if (selectedTiles[0, 0].Size == 8) tileSize = 8;
            int zsiz = tileSize * TileZoom;
            for (int i = 0; i < selectedTiles.GetLength(0); i++, x += zsiz)
            {
                if (tileSize == 16 && oddX && i == w)
                    x -= (zsiz / 2);

                y = 0;
                for (int j = 0; j < selectedTiles.GetLength(1); j++, y += zsiz)
                {
                    if (tileSize == 16 && oddY && j == h)
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

        protected override void setZoom(int newZoom)
        {
            base.setZoom(newZoom);
            SelectionAccuracy = selectionAccuracy;
            SelectionMinSize = selectionMinSize;
        }

        protected override void reDraw()
        {
            base.reDraw();

            using (Graphics g = Graphics.FromImage(Image))
            {
                if (BehindBitmap != null)
                    g.DrawImage(BehindBitmap, 0, 0);
                if (selection != null)
                    g.DrawRectangle(selectionColor, selection);
            }
            Refresh();
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

            int xlim = Width - (selectionMinSize * Zoom);
            int ylim = Height - (selectionMinSize * Zoom);

            if (minx > xlim)
                minx = xlim;
            if (miny > ylim)
                miny = ylim;

            int zacc = selectionAccuracy * Zoom;
            if (maxx == selectionStartX) maxx += (zacc - (maxx % zacc));
            if (maxy == selectionStartY) maxy += (zacc - (maxy % zacc));

            int glim = 128 * Zoom;

            if (maxx >= glim) maxx = glim - 1;
            if (maxy >= glim) maxy = glim - 1;

            int w = maxx - minx;
            int h = maxy - miny;


            Selection = new Rectangle(minx, miny, w, h);
        }

        private void mouseUp(object sender, MouseEventArgs e)
        {
            mouseMove(sender, e);
            selecting = false;
            getTilesFromSelection();
            SelectionChanged?.Invoke(this);
        }
        #endregion

        public override void LoadGFX(string path, int position)
        {
            base.LoadGFX(path, position);
            BaseTile bt = BaseTile.None;
            if (Tiles16 == null)
            {
                Tiles16 = Tile.GenerateTilesFromColorMatrix(colorMatrix,
                TileSize.Size16x16, bt, out bt);
            }
            else
            {
                Tile[,] t = Tile.GenerateTilesFromColorMatrix(colorMatrix,
                TileSize.Size16x16, bt, out bt);
                for (int i = 0; i < Tiles16.GetLength(0); i++)
                {
                    for (int j = 0; j < Tiles16.GetLength(1); j++)
                    {
                        Tile.FusionTiles(Tiles16[i, j], t[i, j], BaseTile.None);
                    }
                }
            }
            if (Tiles8 == null)
            {
                Tiles8 = Tile.GenerateTilesFromColorMatrix(colorMatrix,
                TileSize.Size8x8, bt, out bt);
            }
            else
            {
                Tile[,] t = Tile.GenerateTilesFromColorMatrix(colorMatrix,
                TileSize.Size8x8, bt, out bt);
                for (int i = 0; i < Tiles8.GetLength(0); i++)
                {
                    for (int j = 0; j < Tiles8.GetLength(1); j++)
                    {
                        Tile.FusionTiles(Tiles8[i, j], t[i, j], BaseTile.None);
                    }
                }
            }
            GraphicsLoaded?.Invoke();
        }
        private void getTilesFromSelection()
        {
            int tileSize = 16;
            Tile[,] tiles = Tiles16;
            if (selection.Width / Zoom == 8 || 
                selection.Height / Zoom == 8)
            {
                tiles = Tiles8;
                tileSize = 8;
            }
            if (tiles == null) return;

            int xarr = selection.X / (8 * Zoom);
            int yarr = selection.Y / (8 * Zoom);
            if (xarr >= tiles.GetLength(0)) xarr = tiles.GetLength(0) - 1;
            if (yarr >= tiles.GetLength(1)) yarr = tiles.GetLength(1) - 1;

            int xlim = selection.Width / (8 * Zoom);
            int ylim = selection.Height / (8 * Zoom);

            if (tileSize == 16)
                selectedTiles =
                    new Tile[(xlim / 2) + xlim % 2, (ylim / 2) + ylim % 2];
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

        private void setSelection(Rectangle rect)
        {
            if (rect == null) return;

            int zacc = selectionAccuracy * Zoom;
            int zmsz = selectionMinSize * Zoom;

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
    }
}
