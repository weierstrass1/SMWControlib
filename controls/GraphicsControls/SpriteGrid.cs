using SMWControlibBackend.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SMWControlibControls.GraphicsControls
{
    public enum GridType { Dotted = 0, Lined = 1, Dashed = 2};
    public partial class SpriteGrid : PictureBox
    {
        private bool[,,] nonDirties;
        private Bitmap[,,] grids;
        private List<TileMask> selection;

        private List<TileMask> tiles;
        public List<TileMask> Tiles
        {
            private get
            {
                return tiles;
            }
            set
            {
                tiles = value;
                reDraw();
            }
        }

        public TileMask[] NewTiles { private get; set; }

        private Zoom gridAccuracy = 8;
        public int GridAccuracy
        {
            get
            {
                return gridAccuracy;
            }
            set
            {
                gridAccuracy = value;
                buildGrid();
                reDraw();
            }
        }

        private Color selectionFillColor = SystemColors.WindowFrame;
        public Color SelectionFillColor
        {
            get
            {
                return selectionFillColor;
            }
            set
            {
                selectionFillColor = value;
                reDraw();
            }
        }

        private Color centerSquareColor = SystemColors.WindowFrame;
        public Color CenterSquareColor
        {
            get
            {
                return centerSquareColor;
            }
            set
            {
                centerSquareColor = value;
                reDraw();
            }
        }

        private Color selectionBorderColor = SystemColors.AppWorkspace;
        public Color SelectionBorderColor
        {
            get
            {
                return selectionBorderColor;
            }
            set
            {
                selectionBorderColor = value;
                reDraw();
            }
        }

        private Color gridColor = SystemColors.ControlLightLight;
        public Color GridColor
        {
            get
            {
                return gridColor;
            }
            set
            {
                gridColor = value;
                nonDirties = new bool[20, 20, 3];
                buildGrid();
                reDraw();
            }
        }
        private GridType gridTypeUsed;
        public int GridTypeUsed
        {   get
            {
                return (int)gridTypeUsed;
            }
            set
            {
                switch(value)
                {
                    case 0:
                        gridTypeUsed = GridType.Dotted;
                        break;
                    case 1:
                        gridTypeUsed = GridType.Lined;
                        break;
                    default:
                        gridTypeUsed = GridType.Dashed;
                        break;
                }
                buildGrid();
                reDraw();
            }
        }

        private Zoom zoom = 1;
        public int Zoom
        {
            get
            {
                return zoom;
            }
            set
            {
                if (zoom != value)
                {
                    zoom = value;
                    MaximumSize = new Size(256 * zoom, 240 * zoom);
                    if (Tiles != null)
                    {
                        foreach (TileMask tm in Tiles)
                        {
                            tm.Zoom = zoom;
                        }
                    }
                    buildGrid();
                    reDraw();
                }
            }
        }

        private bool activateCenterSquare = true;
        public bool ActivateCenterSquare
        {
            get
            {
                return activateCenterSquare;
            }
            set
            {
                activateCenterSquare = value;
                reDraw();
            }
        }

        private bool activateGrid = true;
        public bool ActivateGrid
        {
            get
            {
                return activateGrid;
            }
            set
            {
                activateGrid = value;
                if(activateGrid)
                {
                    buildGrid();
                }
                reDraw();
            }
        }

        private bool canSelect = true, selecting = false, moving = false;
        private int selStartX = 0, selStartY = 0;
        private int selEndX = 0, selEndY = 0;
        private int counter = 0;
        private int dMinx = 0, dMiny = 0;
        private int dMaxx = 0, dMaxy = 0;
        public SpriteGrid()
        {
            InitializeComponent();
            SizeChanged += spriteGrid_SizeChanged;
            ColorPalette.SelectedGlobalPaletteChange += 
                colorPalette_SelectedGlobalPaletteChange;
            ColorPalette.GlobalPalletesChange += 
                colorPalette_GlobalPalletesChange;
            ColorPalette.OneGlobalPaletteChange += 
                colorPalette_OneGlobalPaletteChange;
            Click += spriteGrid_Click;
            MouseDown += spriteGrid_MouseDown;
            MouseMove += spriteGrid_MouseMove;
            MouseUp += spriteGrid_MouseUp;
            cursorPosition.Parent = this;
            cursorPosition.Location = new Point(0, 0);
        }
        private void spriteGrid_MouseDown(object sender, MouseEventArgs e)
        {
            if (canSelect && e.Button == MouseButtons.Left)
            {
                selecting = true;
                selStartX = e.X;
                selStartY = e.Y;
                selEndX = e.X;
                selEndY = e.Y;
                select();
            }
            else if (e.Button == MouseButtons.Left)
            {
                selStartX = e.X;
                selStartY = e.Y;
                selEndX = e.X;
                selEndY = e.Y;
                if (intoSelection())
                {
                    getDiffPos();
                    move();
                    moving = true;
                }
                else
                {
                    deselect();
                    canSelect = true;
                    selecting = true;
                    selStartX = e.X;
                    selStartY = e.Y;
                    selEndX = e.X;
                    selEndY = e.Y;
                    select();
                }
            }
            else
            {
                deselect();
                canSelect = true;
            }
            reDraw();
        }
        private void spriteGrid_MouseMove(object sender, MouseEventArgs e)
        {
            int zacc = gridAccuracy * zoom;
            int accX = (e.X / zacc) * zacc;
            int accY = (e.Y / zacc) * zacc;
            int posx = (accX / zoom) - 128;
            int posy = 112 - (accY / zoom);
            string sx = Convert.ToString(posx, 16);
            if (sx.Length <= 1) sx = "0" + sx;
            sx = sx.Substring(sx.Length - 2, 2);
            sx = sx.ToUpper();
            sx = "$" + sx;
            string sy = Convert.ToString(posy, 16);
            if (sy.Length <= 1) sy = "0" + sy;
            sy = sy.Substring(sy.Length - 2, 2);
            sy = sy.ToUpper();
            sy = "$" + sy;

            cursorPosition.Text = "X: " + sx + " - Y: " + sy;
            int newX = 0, newY = 0;
            if (posy >= 0) newY = Height - cursorPosition.Height;
            if (posx < 0) newX = Width - cursorPosition.Width;
            if (BorderStyle != BorderStyle.None)
            {
                if (newX > 0) newX -= 4;
                if (newY != 0) newY -= 4;
            }
            cursorPosition.Location = new Point(newX, newY);
            if (canSelect && selecting)
            {
                selEndX = e.X;
                selEndY = e.Y;
                select();
                reDraw();
            }
            else if(moving)
            {
                selEndX = e.X;
                selEndY = e.Y;
                move();
                reDraw();
            }
        }
        private void spriteGrid_MouseUp(object sender, MouseEventArgs e)
        {
            if (canSelect && selecting)
            {
                selecting = false;
                canSelect = false;
                selEndX = e.X;
                selEndY = e.Y;
                select();
                reDraw();
                if (selection == null || selection.Count == 0)
                    canSelect = true;
            }
            else if (moving)
            {
                selEndX = e.X;
                selEndY = e.Y;
                move();
                reDraw();
                moving = false;
            }
        }
        private void move()
        {
            int minX = int.MaxValue;
            int minY = int.MaxValue;
            int maxX = int.MinValue;
            int maxY = int.MinValue;
            foreach (TileMask tm in selection)
            {
                if (tm.xDisp < minX)
                {
                    minX = tm.xDisp;
                }
                if (tm.yDisp < minY)
                {
                    minY = tm.yDisp;
                }
                if (tm.xDisp + tm.Size * zoom > maxX)
                {
                    maxX = tm.xDisp + tm.Size * zoom;
                }
                if (tm.yDisp + tm.Size * zoom > maxY)
                {
                    maxY = tm.yDisp + tm.Size * zoom;
                }
            }

            int dfx = selEndX + dMaxx;
            if (dfx > Width) dfx -= Width;
            else dfx = 0;

            int dfx2 = selEndX - dMinx;
            if (dfx2 < 0) dfx2 *= -1;
            else dfx2 = 0;

            int dfy = selEndY + dMaxy;
            if (dfy > Height) dfy -= Height;
            else dfy = 0;

            int dfy2 = selEndY - dMiny;
            if (dfy2 < 0) dfy2 *= -1;
            else dfy2 = 0;

            int accZoom = gridAccuracy * zoom;

            foreach (TileMask tm in selection)
            {
                tm.xDisp -= minX;
                tm.yDisp -= minY;
                tm.xDisp -= dMinx;
                tm.yDisp -= dMiny;
                tm.xDisp += selEndX + dfx2 - dfx;
                tm.yDisp += selEndY + dfy2 - dfy;
                tm.xDisp = (tm.xDisp / accZoom) * accZoom;
                tm.yDisp = (tm.yDisp / accZoom) * accZoom;
            }
        }
        private void getDiffPos()
        {
            int minX = int.MaxValue;
            int minY = int.MaxValue;
            int maxX = int.MinValue;
            int maxY = int.MinValue;
            foreach (TileMask tm in selection)
            {
                if (tm.xDisp < minX)
                {
                    minX = tm.xDisp;
                }
                if (tm.yDisp < minY)
                {
                    minY = tm.yDisp;
                }
                if (tm.xDisp + tm.Size * zoom > maxX)
                {
                    maxX = tm.xDisp + tm.Size * zoom;
                }
                if (tm.yDisp + tm.Size * zoom > maxY)
                {
                    maxY = tm.yDisp + tm.Size * zoom;
                }
            }
            dMinx = selStartX - minX;
            dMiny = selStartY - minY;
            dMaxx = maxX - selStartX;
            dMaxy = maxY - selStartY;
        }
        private void deselect()
        {
            if (selection != null)
            {
                foreach (TileMask tm in selection)
                {
                    tm.IsDirty -= isDirty;
                    tm.IsSelected = false;
                }
            }
            selection = new List<TileMask>();
        }
        private void select()
        {
            if (Tiles == null) return;
            deselect();

            int minX = Math.Min(selStartX, selEndX);
            int minY = Math.Min(selStartY, selEndY);
            int maxX = Math.Max(selStartX, selEndX);
            int maxY = Math.Max(selStartY, selEndY);

            minX = Math.Max(0, minX);
            minY = Math.Max(0, minY);
            maxX = Math.Min(maxX, Width - 1);
            maxY = Math.Min(maxY, Height - 1);

            foreach (TileMask tm in Tiles)
            {
                if (tm.xDisp + tm.Size * zoom >= minX && tm.xDisp <= maxX &&
                    tm.yDisp + tm.Size * zoom >= minY && tm.yDisp <= maxY) 
                {
                    tm.IsDirty += isDirty;
                    tm.IsSelected = true;
                    selection.Add(tm);
                }
            }
        }
        public void DeleteSelection()
        {
            if (selection != null)
            {
                foreach (TileMask tm in selection)
                {
                    tm.IsDirty -= isDirty;
                    tm.IsSelected = false;
                    Tiles.Remove(tm);
                }
            }
            selection = new List<TileMask>();
            reDraw();
        }
        private bool intoSelection()
        {
            if (selection == null || selection.Count == 0) return false;
            if (selStartX < 0) selStartX = 0;
            else if (selStartX >= Width) selStartX = Width - 1;
            if (selStartY < 0) selStartY = 0;
            else if (selStartY >= Height) selStartY = Height - 1;

            foreach(TileMask tm in selection)
            {
                if (selStartX >= tm.xDisp && selStartX <= tm.xDisp + tm.Size * zoom &&
                    selStartY >= tm.yDisp && selStartY <= tm.yDisp + tm.Size * zoom)
                {
                    return true;
                }
            }
            return false;
        }
        private void isDirty(TileMask obj)
        {
            obj.GetBitmap();

            counter++;
            if (counter == selection.Count)
            {
                counter = 0;
                reDraw();
                Refresh();
            }
        }

        private void spriteGrid_Click(object sender, EventArgs e)
        {
            MouseEventArgs mea = (MouseEventArgs)e;
            if (mea.Button == MouseButtons.Right)
            {
                addTiles(mea.X, mea.Y);
                reDraw();
            }
        }

        private void addTiles(int x, int y)
        {
            if (Tiles == null) 
            {
                MessageBox.Show("You must create a new frame before add tiles to the grid.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (NewTiles == null || NewTiles.Length == 0)
                return;
            if (x >= Width) x = Width - 1;
            else if (x < 0) x = 0;
            if (y >= Height) y = Height - 1;
            else if (y < 0) y = 0;

            int gzoom = gridAccuracy * zoom;
            int xAcc = (x / gzoom) * gzoom;
            int yAcc = (y / gzoom) * gzoom;
            TileMask tmaux;
            bool cant = false;
            for (int i = 0; i < NewTiles.Length; i++)
            {
                tmaux = NewTiles[i].Clone();
                tmaux.xDisp /= tmaux.Zoom;
                tmaux.yDisp /= tmaux.Zoom;
                tmaux.xDisp *= zoom;
                tmaux.yDisp *= zoom;
                tmaux.xDisp += xAcc;
                tmaux.yDisp += yAcc;
                tmaux.Zoom = zoom;
                tmaux.IsSelected = false;
                if (tmaux.xDisp <= Width - tmaux.Size * zoom &&
                    tmaux.yDisp <= Height - tmaux.Size * zoom)
                {
                    Tiles.Add(tmaux);
                }
                else cant = true;
            }
            if (cant)
            {
                MessageBox.Show("Some tiles can't be added to the grid at this position, because They are out of the limits.",
                    "Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }

        #region BuildGrid
        private void buildGrid()
        {
            switch(gridTypeUsed)
            {
                case GridType.Dotted:
                    buildPointGrid();
                    break;
                case GridType.Lined:
                    buildLineGrid();
                    break;
                default:
                    buildLittleLinesGrid();
                    break;
            }
        }

        private void buildLittleLinesGrid()
        {
            if (grids == null)
            {
                grids = new Bitmap[20, 20, 3];
                nonDirties = new bool[20, 20, 3];
            }

            if (nonDirties[gridAccuracy - 1, zoom - 1, 2]) return;

            Bitmap bp = new Bitmap(Width, Height);
            List<RectangleF> recs = new List<RectangleF>();

            int adder = gridAccuracy * zoom;

            for (int i = 0; i < Width; i += adder)
            {
                for (int j = 0; j < Height; j += 8)
                {
                    recs.Add(new RectangleF(i, j, 1, 4));
                }
            }

            for (int i = 0; i < Width; i += 8)
            {
                for (int j = 0; j < Height; j += adder)
                {
                    recs.Add(new RectangleF(i, j, 4, 1));
                }
            }

            using (Graphics g = Graphics.FromImage(bp))
            {
                g.FillRectangles(
                    new SolidBrush(gridColor), recs.ToArray());
            }

            grids[gridAccuracy - 1, zoom - 1, 2] = bp;
            nonDirties[gridAccuracy - 1, zoom - 1, 2] = true;
        }

        private void buildLineGrid()
        {
            if (grids == null)
            {
                grids = new Bitmap[20, 20, 3];
                nonDirties = new bool[20, 20, 3];
            }

            if (nonDirties[gridAccuracy - 1, zoom - 1, 1]) return;

            Bitmap bp = new Bitmap(Width, Height);
            List<RectangleF> recs = new List<RectangleF>();

            int adder = gridAccuracy * zoom;

            int limit = Math.Max(Width, Height);

            for (int i = 0; i < limit; i += adder)
            {
                recs.Add(new RectangleF(i, 0, 1, Height));
                recs.Add(new RectangleF(0, i, Width, 1));
            }

            using (Graphics g = Graphics.FromImage(bp))
            {
                g.FillRectangles(
                    new SolidBrush(gridColor), recs.ToArray());
            }

            grids[gridAccuracy - 1, zoom - 1, 1] = bp;
            nonDirties[gridAccuracy - 1, zoom - 1, 1] = true;
        }

        private void buildPointGrid()
        {
            if (grids == null)
            {
                grids = new Bitmap[20, 20, 3];
                nonDirties = new bool[20, 20, 3];
            }

            if (nonDirties[gridAccuracy - 1, zoom - 1, 0]) return;

            Bitmap bp = new Bitmap(Width, Height);
            List<RectangleF> recs = new List<RectangleF>();

            int adder = gridAccuracy * zoom;

            for (int i = 0; i < Width; i += adder)
            {
                for (int j = 0; j < Height; j += 4)
                {
                    recs.Add(new RectangleF(i, j, 1, 1));
                }
            }

            for (int i = 0; i < Width; i += 4)
            {
                for (int j = 0; j < Height; j += adder)
                {
                    recs.Add(new RectangleF(i, j, 1, 1));
                }
            }

            using (Graphics g = Graphics.FromImage(bp))
            {
                g.FillRectangles(
                    new SolidBrush(gridColor), recs.ToArray());
            }

            grids[gridAccuracy - 1, zoom - 1, 0] = bp;
            nonDirties[gridAccuracy - 1, zoom - 1, 0] = true;
        }
        #endregion

        private void reDraw()
        {
            if (grids == null) buildGrid();
            Image = new Bitmap(Width, Height);
            try
            {
                using (Graphics g = Graphics.FromImage(Image))
                {
                    Brush br = new SolidBrush(ColorPalette.GetGlobalColor(0));
                    g.FillRectangle(br, 0, 0, Width, Height);
                }
            }
            catch { }

            if (Tiles != null)
            {
                foreach (TileMask tm in Tiles)
                {
                    using (Graphics g = Graphics.FromImage(Image))
                    {
                        g.DrawImage(
                            tm.GetBitmap(),
                            tm.xDisp, tm.yDisp);
                    }
                }
            }

            using (Graphics g = Graphics.FromImage(Image))
            {
                if(activateGrid)
                {
                    g.DrawImage(
                    grids[gridAccuracy - 1, zoom - 1, GridTypeUsed],
                    0, 0);
                }
                
                if(selecting)
                {
                    selectionFillColor = Color.FromArgb(96,
                        SelectionFillColor.R, SelectionFillColor.G,
                        SelectionFillColor.B);
                    Brush br = new SolidBrush(SelectionFillColor);

                    int minX = Math.Min(selStartX, selEndX);
                    int minY = Math.Min(selStartY, selEndY);
                    int maxX = Math.Max(selStartX, selEndX);
                    int maxY = Math.Max(selStartY, selEndY);

                    minX = Math.Max(0, minX);
                    minY = Math.Max(0, minY);
                    maxX = Math.Min(maxX, Width - 1);
                    maxY = Math.Min(maxY, Height - 1);

                    g.FillRectangle(br, minX, minY, maxX - minX, maxY - minY);
                }
                if (selection != null && selection.Count != 0)
                {
                    Pen p = new Pen(SelectionBorderColor);
                    foreach(TileMask tm in selection)
                    {
                        g.DrawRectangle(p, tm.xDisp, tm.yDisp,
                            tm.Size * zoom, tm.Size * zoom);
                    }
                }

                if (activateGrid && ActivateCenterSquare)
                {
                    Pen p1 = new Pen(centerSquareColor);
                    g.DrawRectangle(p1, 128 * zoom, 112 * zoom, 16 * zoom, 16 * zoom);
                }
            }
            
        }

        private void spriteGrid_SizeChanged(object sender, EventArgs e)
        {
            nonDirties = new bool[20, 20, 3];
            buildGrid();
            reDraw();
        }

        private void colorPalette_OneGlobalPaletteChange(PaletteId obj)
        {
            reDraw();
        }

        private void colorPalette_GlobalPalletesChange()
        {
            reDraw();
        }

        private void colorPalette_SelectedGlobalPaletteChange()
        {
            reDraw();
        }
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
    }
}
