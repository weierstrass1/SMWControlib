using SMWControlibBackend.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SMWControlibControls.GraphicsControls
{
    public enum GridType { Dotted = 0, Lined = 1, Dashed = 2};
    public partial class SpriteGrid : Panel
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
                deselect();
                applyZoom(zoom);
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
                buildSelectionBox();
                ReDraw();
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
                buildCenterSquare();
                ReDraw();
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
                buildSelectionBox();
                ReDraw();
            }
        }

        private Color backgroundColor = Color.FromArgb(232, 232, 255);
        public Color BackgroundColor
        {
            get
            {
                return backgroundColor;
            }
            set
            {
                backgroundColor = value;
                ReDraw();
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
                ReDraw();
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
                ReDraw();
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
                    applyZoom(value);
                }
            }
        }
        private void applyZoom(int value)
        {
            zoom = value;
            MaximumSize = new Size((256 * zoom) + 6,
                (240 * zoom) + 6);
            if (Tiles != null)
            {
                foreach (TileMask tm in Tiles)
                {
                    tm.XDisp /= tm.Zoom;
                    tm.YDisp /= tm.Zoom;
                    tm.Zoom = zoom;
                    tm.XDisp *= tm.Zoom;
                    tm.YDisp *= tm.Zoom;
                }
            }
            buildGrid();
            ReDraw();
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
                if (ActivateCenterSquare)
                {
                    centerSquare.Image = centerSquareImg;
                }
                else
                {
                    centerSquare.Image = new Bitmap(Width, Height);
                }
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
                else
                {
                    gridBox.Image = new Bitmap(Width, Height);
                }
            }
        }

        private int midX = 0, midY = 0;
        public event Action MidChanged;
        public int MidX
        {
            get
            {
                return midX;
            }
            set
            {
                midX = value;
                buildCenterSquare();
                MidChanged?.Invoke();
            }
        }

        public int MidY
        {
            get
            {
                return midY;
            }
            set
            {
                midY = value;
                buildCenterSquare();
                MidChanged?.Invoke();
            }
        }

        private bool canSelect = true, selecting = false, moving = false;
        private int selStartX = 0, selStartY = 0;
        private int selEndX = 0, selEndY = 0;
        private int counter = 0;
        private int dMinx = 0, dMiny = 0;
        private int dMaxx = 0, dMaxy = 0;
        private PictureBox baseImage, gridBox, centerSquare, 
            selectionBox;
        public Control ToolTipControl { get { return selectionBox; } }
        private Bitmap gridBoxImg, centerSquareImg;
        public Action<object, MouseEventArgs> MovingMouse;
        public Action Moved;
        public SpriteGrid()
        {
            InitializeComponent();
            SizeChanged += spriteGrid_SizeChanged;
            ColorPalette.SelectedGlobalPaletteChange += 
                colorPalette_SelectedGlobalPaletteChange;
            ColorPalette.GlobalPalletesChangeExcecuted += 
                colorPalette_GlobalPalletesChange;
            ColorPalette.OneGlobalPaletteChange += 
                colorPalette_OneGlobalPaletteChange;
            cursorPosition.Parent = this;

            cursorPosition.Location = new Point(0, 0);
            baseImage = new PictureBox
            {
                Margin = new Padding(0, 0, 0, 0),
                Padding = new Padding(0, 0, 0, 0),
                Parent = this,
                Location = new Point(0, 0),
                BackColor = Color.Transparent
            };
            gridBox = new PictureBox
            {
                Margin = new Padding(0, 0, 0, 0),
                Padding = new Padding(0, 0, 0, 0),
                Parent = this,
                Location = new Point(0, 0),
                BackColor = Color.Transparent
            };
            gridBox.BringToFront();
            centerSquare = new PictureBox
            {
                Margin = new Padding(0, 0, 0, 0),
                Padding = new Padding(0, 0, 0, 0),
                Parent = gridBox,
                Location = new Point(0, 0),
                BackColor = Color.Transparent
            };
            selectionBox = new PictureBox
            {
                Margin = new Padding(0, 0, 0, 0),
                Padding = new Padding(0, 0, 0, 0),
                Parent = centerSquare,
                Location = new Point(0, 0),
                BackColor = Color.Transparent
            };
            selectionBox.Click += spriteGrid_Click;
            selectionBox.MouseDown += spriteGrid_MouseDown;
            selectionBox.MouseMove += spriteGrid_MouseMove;
            selectionBox.MouseUp += spriteGrid_MouseUp;
            SizeChanged += sizeChanged;
        }

        private void sizeChanged(object sender, EventArgs e)
        {
            buildGrid();
            ReDraw();
        }

        private void spriteGrid_MouseDown(object sender, MouseEventArgs e)
        {
            int X = e.X;
            int Y = e.Y;

            if (canSelect && e.Button == MouseButtons.Left)
            {
                selecting = true;
                selStartX = X;
                selStartY = Y;
                selEndX = X;
                selEndY = Y;
                select();
            }
            else if (e.Button == MouseButtons.Left)
            {
                selStartX = X;
                selStartY = Y;
                selEndX = X;
                selEndY = Y;
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
                    selStartX = X;
                    selStartY = Y;
                    selEndX = X;
                    selEndY = Y;
                    select();
                }
            }
            else
            {
                deselect();
                canSelect = true;
            }
            ReDraw();
        }
        private void spriteGrid_MouseMove(object sender, MouseEventArgs e)
        {
            MovingMouse?.Invoke(sender, e);
            int X = e.X;
            int Y = e.Y;

            int zacc = gridAccuracy * zoom;
            int accX = (X / zacc) * zacc;
            int accY = (Y / zacc) * zacc;
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
                selEndX = X;
                selEndY = Y;
                select();
                ReDraw();
            }
            else if(moving)
            {
                selEndX = X;
                selEndY = Y;
                move();
            }
        }
        private void spriteGrid_MouseUp(object sender, MouseEventArgs e)
        {
            int X = e.X;
            int Y = e.Y;

            if (canSelect && selecting)
            {
                selecting = false;
                canSelect = false;
                selEndX = X;
                selEndY = Y;
                select();
                ReDraw();
                if (selection == null || selection.Count == 0)
                    canSelect = true;
            }
            else if (moving)
            {
                selEndX = X;
                selEndY = Y;
                move();
                moving = false;
            }
        }

        public void Mirror(bool FlipX, bool FlipY)
        {
            if (selection == null || selection.Count <= 0) return;
            int minX = int.MaxValue;
            int minY = int.MaxValue;
            int maxX = int.MinValue;
            int maxY = int.MinValue;
            foreach (TileMask tm in selection)
            {
                if (tm.XDisp < minX)
                {
                    minX = tm.XDisp;
                }
                if (tm.YDisp < minY)
                {
                    minY = tm.YDisp;
                }
                if (tm.XDisp > maxX)
                {
                    maxX = tm.XDisp;
                }
                if (tm.YDisp > maxY)
                {
                    maxY = tm.YDisp;
                }
            }

            foreach (TileMask tm in selection)
            {
                if(FlipX)
                {
                    tm.FlipX = !tm.FlipX;
                    tm.XDisp = minX + maxX - tm.XDisp;
                }
                if (FlipY)
                {
                    tm.FlipY = !tm.FlipY;
                    tm.YDisp = minY + maxY - tm.YDisp;
                }
            }
            buildSelectionBox();
        }

        public void MoveSelection(int x, int y)
        {
            if (selection == null || selection.Count <= 0) return;
            int minX = int.MaxValue;
            int minY = int.MaxValue;
            int maxX = int.MinValue;
            int maxY = int.MinValue;
            foreach (TileMask tm in selection)
            {
                if (tm.XDisp < minX)
                {
                    minX = tm.XDisp;
                }
                if (tm.YDisp < minY)
                {
                    minY = tm.YDisp;
                }
                if (tm.XDisp + tm.Size * zoom > maxX)
                {
                    maxX = tm.XDisp + tm.Size * zoom;
                }
                if (tm.YDisp + tm.Size * zoom > maxY)
                {
                    maxY = tm.YDisp + tm.Size * zoom;
                }
            }

            int xZoom = x * Zoom;
            int yZoom = y * Zoom;
            int w = maxX - minX;
            int h = maxY - minY;

            int dfx = minX + xZoom + w;
            if (dfx > Width) dfx -= Width;
            else dfx = 0;

            int dfx2 = minX + xZoom;
            if (dfx2 < 0) dfx2 *= -1;
            else dfx2 = 0;

            int dfy = minY + yZoom + h;
            if (dfy > Height) dfy -= Height;
            else dfy = 0;

            int dfy2 = minY + yZoom;
            if (dfy2 < 0) dfy2 *= -1;
            else dfy2 = 0;

            int accZoom = gridAccuracy * zoom;
            int xmin = minX + xZoom + dfx2 - dfx;
            xmin = (xmin / accZoom) * accZoom;
            int ymin = minY + yZoom + dfy2 - dfy;
            ymin = (ymin / accZoom) * accZoom;

            int dif;

            foreach (TileMask tm in selection)
            {
                dif = tm.XDisp - minX;
                tm.XDisp = xmin + dif;
                dif = tm.YDisp - minY;
                tm.YDisp = ymin + dif;
            }
            buildSelectionBox();
        }
        private void move()
        {
            if (selection == null || selection.Count <= 0) return;
            int minX = int.MaxValue;
            int minY = int.MaxValue;
            int maxX = int.MinValue;
            int maxY = int.MinValue;
            foreach (TileMask tm in selection)
            {
                if (tm.XDisp < minX)
                {
                    minX = tm.XDisp;
                }
                if (tm.YDisp < minY)
                {
                    minY = tm.YDisp;
                }
                if (tm.XDisp + tm.Size * zoom > maxX)
                {
                    maxX = tm.XDisp + tm.Size * zoom;
                }
                if (tm.YDisp + tm.Size * zoom > maxY)
                {
                    maxY = tm.YDisp + tm.Size * zoom;
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
            int xmin = -dMinx + selEndX + dfx2 - dfx;
            int ymin = -dMiny + selEndY + dfy2 - dfy;
            xmin = (xmin / accZoom) * accZoom;
            ymin = (ymin / accZoom) * accZoom;

            int dif;

            foreach (TileMask tm in selection)
            {
                dif = tm.XDisp - minX;
                tm.XDisp = xmin + dif;
                dif = tm.YDisp - minY;
                tm.YDisp = ymin + dif;
            }
            Moved?.Invoke();
            buildSelectionBox();
        }
        private void getDiffPos()
        {
            int minX = int.MaxValue;
            int minY = int.MaxValue;
            int maxX = int.MinValue;
            int maxY = int.MinValue;
            foreach (TileMask tm in selection)
            {
                if (tm.XDisp < minX)
                {
                    minX = tm.XDisp;
                }
                if (tm.YDisp < minY)
                {
                    minY = tm.YDisp;
                }
                if (tm.XDisp + tm.Size * zoom > maxX)
                {
                    maxX = tm.XDisp + tm.Size * zoom;
                }
                if (tm.YDisp + tm.Size * zoom > maxY)
                {
                    maxY = tm.YDisp + tm.Size * zoom;
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
            buildSelectionBox();
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
                if (tm.XDisp + tm.Size * zoom >= minX && tm.XDisp <= maxX &&
                    tm.YDisp + tm.Size * zoom >= minY && tm.YDisp <= maxY) 
                {
                    tm.IsDirty += isDirty;
                    tm.IsSelected = true;
                    selection.Add(tm);
                }
            }
            buildSelectionBox();
        }
        public void DeleteSelection()
        {
            if (selection != null)
            {
                foreach (TileMask tm in selection)
                {
                    tm.IsDirty -= isDirty;
                    tm.IsSelected = false;
                    tm.RemoveDirtyEvent();
                    Tiles.Remove(tm);
                }
            }
            selection = new List<TileMask>();
            buildSelectionBox();
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
                if (selStartX >= tm.XDisp && selStartX <= tm.XDisp + tm.Size * zoom &&
                    selStartY >= tm.YDisp && selStartY <= tm.YDisp + tm.Size * zoom)
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
                buildSelectionBox();
                Refresh();
            }
        }

        private void spriteGrid_Click(object sender, EventArgs e)
        {
            MouseEventArgs mea = (MouseEventArgs)e;
            if (mea.Button == MouseButtons.Right)
            {
                addTiles(mea.X, mea.Y);
                ReDraw();
            }
        }

        private void addTiles(int x, int y)
        {
            if (Tiles == null) 
            {
                MessageBox.Show("You must create a new frame before adding tiles to the grid.",
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
                tmaux.XDisp /= tmaux.Zoom;
                tmaux.YDisp /= tmaux.Zoom;
                tmaux.XDisp *= zoom;
                tmaux.YDisp *= zoom;
                tmaux.XDisp += xAcc;
                tmaux.YDisp += yAcc;
                tmaux.Zoom = zoom;
                tmaux.Palette = ColorPalette.SelectedPalette;
                tmaux.IsSelected = false;
                if (tmaux.XDisp <= Width - tmaux.Size * zoom &&
                    tmaux.YDisp <= Height - tmaux.Size * zoom)
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

            if (nonDirties[gridAccuracy - 1, zoom - 1, 2])
            {
                if (ActivateGrid)
                    gridBox.Image = grids[gridAccuracy - 1, zoom - 1, 2];
                gridBox.Size = new Size(gridBox.Image.Width, gridBox.Image.Height);
                gridBoxImg = grids[gridAccuracy - 1, zoom - 1, 2];
                buildCenterSquare();
                buildSelectionBox();
                return;
            }

            Bitmap bp = new Bitmap(Width, Height);
            List<RectangleF> recs = new List<RectangleF>();

            int adder = gridAccuracy * zoom;

            for (int i = 0; i < Width; i += adder)
            {
                for (int j = 0; j < Height; j += 8)
                {
                    recs.Add(new RectangleF(i, j - 1, 1, 3));
                }
            }

            for (int i = 0; i < Width; i += 8)
            {
                for (int j = 0; j < Height; j += adder)
                {
                    recs.Add(new RectangleF(i - 1, j, 3, 1));
                }
            }

            using (Graphics g = Graphics.FromImage(bp))
            {
                g.FillRectangles(
                    new SolidBrush(gridColor), recs.ToArray());
            }

            grids[gridAccuracy - 1, zoom - 1, 2] = bp;
            nonDirties[gridAccuracy - 1, zoom - 1, 2] = true;
            if (ActivateGrid)
                gridBox.Image = grids[gridAccuracy - 1, zoom - 1, 2];
            gridBox.Size = new Size(gridBox.Image.Width, gridBox.Image.Height);
            gridBoxImg = grids[gridAccuracy - 1, zoom - 1, 2];
            buildCenterSquare();
            buildSelectionBox();
        }

        private void buildLineGrid()
        {
            if (grids == null)
            {
                grids = new Bitmap[20, 20, 3];
                nonDirties = new bool[20, 20, 3];
            }

            if (nonDirties[gridAccuracy - 1, zoom - 1, 1])
            {
                if (ActivateGrid)
                    gridBox.Image = grids[gridAccuracy - 1, zoom - 1, 1];
                gridBox.Size = new Size(gridBox.Image.Width, gridBox.Image.Height);
                gridBoxImg = grids[gridAccuracy - 1, zoom - 1, 1];
                buildCenterSquare();
                buildSelectionBox();
                return;
            }

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
            if (ActivateGrid)
                gridBox.Image = grids[gridAccuracy - 1, zoom - 1, 1];
            gridBox.Size = new Size(gridBox.Image.Width, gridBox.Image.Height);
            gridBoxImg = grids[gridAccuracy - 1, zoom - 1, 1];
            buildCenterSquare();
            buildSelectionBox();
        }

        private void buildPointGrid()
        {
            if (grids == null)
            {
                grids = new Bitmap[20, 20, 3];
                nonDirties = new bool[20, 20, 3];
            }

            if (nonDirties[gridAccuracy - 1, zoom - 1, 0])
            {
                if (ActivateGrid)
                    gridBox.Image = grids[gridAccuracy - 1, zoom - 1, 0];
                gridBox.Size = new Size(gridBox.Image.Width, gridBox.Image.Height);
                gridBoxImg = grids[gridAccuracy - 1, zoom - 1, 0];
                buildCenterSquare();
                buildSelectionBox();
                return;
            }

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
            if (ActivateGrid)
                gridBox.Image = grids[gridAccuracy - 1, zoom - 1, 0];
            gridBox.Size = new Size(gridBox.Image.Width, gridBox.Image.Height);
            gridBoxImg = grids[gridAccuracy - 1, zoom - 1, 0];
            buildCenterSquare();
            buildSelectionBox();
        }
        #endregion

        private void buildCenterSquare()
        {
            Pen p1 = new Pen(centerSquareColor);
            Bitmap newImage = new Bitmap(Width, Height);
            using (Graphics g = Graphics.FromImage(newImage))
            {
                g.DrawRectangle(p1, 128 * zoom, 112 * zoom, 16 * zoom, 16 * zoom);
                g.DrawLine(p1, (midX + 136) * zoom, 0, 
                    (midX + 136) * zoom, newImage.Height);
                g.DrawLine(p1, 0, (midY + 120) * zoom,
                    newImage.Width, (midY + 120) * zoom);
            }
            centerSquare.Size = new Size(Width, Height);
            centerSquareImg = newImage;

            if (ActivateCenterSquare)
                centerSquare.Image = newImage;
        }

        private void buildSelectionBox()
        {
            int minX = int.MaxValue;
            int minY = int.MaxValue;
            int maxX = int.MinValue;
            int maxY = int.MinValue;
            bool needClear = false;
            GC.Collect();
            GC.WaitForPendingFinalizers();
            try
            {
                selectionBox.Image = new Bitmap(Width, Height);
                selectionBox.Size = new Size(selectionBox.Image.Width,
                    selectionBox.Image.Height);
            }
            catch
            {
                needClear = true;
            }

            if (selection != null && selection.Count > 0)
            {
                using (Graphics g = Graphics.FromImage(selectionBox.Image))
                {
                    if (needClear)
                        g.Clear(selectionBox.BackColor);
                    if (selection != null && selection.Count != 0)
                    {
                        Pen p = new Pen(SelectionBorderColor);
                        foreach (TileMask tm in selection)
                        {
                            g.DrawImage(tm.GetBitmap(),
                                tm.XDisp, tm.YDisp);
                            g.DrawRectangle(p, tm.XDisp, tm.YDisp,
                                tm.Size * zoom, tm.Size * zoom);
                        }
                    }
                    needClear = false;
                }
            }
           
            using (Graphics g = Graphics.FromImage(selectionBox.Image))
            {
                if (needClear)
                    g.Clear(selectionBox.BackColor);
                if (selecting)
                {
                    selectionFillColor = Color.FromArgb(96,
                        SelectionFillColor.R, SelectionFillColor.G,
                        SelectionFillColor.B);
                    Brush br = new SolidBrush(SelectionFillColor);

                    minX = Math.Min(selStartX, selEndX);
                    minY = Math.Min(selStartY, selEndY);
                    maxX = Math.Max(selStartX, selEndX);
                    maxY = Math.Max(selStartY, selEndY);

                    minX = Math.Max(0, minX);
                    minY = Math.Max(0, minY);
                    maxX = Math.Min(maxX, Width - 1);
                    maxY = Math.Min(maxY, Height - 1);

                    g.FillRectangle(br, minX, minY, maxX - minX, maxY - minY);
                }
            }
            if (needClear) selectionBox.Refresh();
        }
        public void ReDraw()
        {
            if (grids == null) buildGrid();

            bool mustClear = false;
            try
            {
                baseImage.Image = new Bitmap(Width, Height);
            }
            catch
            {
                mustClear = true;
            }
            using (Graphics g = Graphics.FromImage(baseImage.Image))
            {
                if (mustClear) g.Clear(baseImage.BackColor);
                Brush br = new SolidBrush(BackgroundColor);
                g.FillRectangle(br, 0, 0, Width, Height);
            }

            if (Tiles != null)
            {
                foreach (TileMask tm in Tiles)
                {
                    if (!selection.Contains(tm))
                    {
                        using (Graphics g = Graphics.FromImage(baseImage.Image))
                        {
                            g.DrawImage(
                                tm.GetBitmap(),
                                tm.XDisp, tm.YDisp);
                        }
                    }
                }
            }
            baseImage.Size = new Size(Width, Height);
            BackgroundImage = baseImage.Image;
        }

        private void spriteGrid_SizeChanged(object sender, EventArgs e)
        {
            nonDirties = new bool[20, 20, 3];
            buildGrid();
            ReDraw();
        }

        private void colorPalette_OneGlobalPaletteChange(PaletteId obj)
        {
            buildSelectionBox();
        }

        private void colorPalette_GlobalPalletesChange()
        {
            buildSelectionBox();
            ReDraw();
        }

        private void colorPalette_SelectedGlobalPaletteChange()
        {
            buildSelectionBox();
        }
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
    }
}
