using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SMWControlibBackend.Graphics;
using SMWControlibBackend.Graphics.Frames;
using SMWControlibBackend.Interaction;
using SMWControlibControls.GraphicsControls;

namespace SMWControlibControls.InteractionControls
{
    public struct GrabType
    {
        public static readonly GrabType Top = new GrabType(0);
        public static readonly GrabType Bottom = new GrabType(1);
        public static readonly GrabType Left = new GrabType(2);
        public static readonly GrabType Right = new GrabType(3);
        public static readonly GrabType TopLeft = new GrabType(4);
        public static readonly GrabType TopRight = new GrabType(5);
        public static readonly GrabType BottomLeft = new GrabType(6);
        public static readonly GrabType BottomRight = new GrabType(7);
        public static readonly GrabType Center = new GrabType(8);
        public static readonly GrabType None = new GrabType(9);

        public int Value { get; private set; }

        private GrabType(int value)
        {
            Value = value;
        }

        public static implicit operator int(GrabType d)
        {
            return d.Value;
        }
    }
    public partial class InteractionGrid : PictureBox
    {

        Frame selectedFrame;
        public Frame SelectedFrame
        {
            get
            {
                return selectedFrame;
            }
            set
            {
                selectedFrame = value;
                buildBack();
                buildBoxes();
                buildCurrentBox();
            }
        }
        HitBox selectedHitbox;
        public HitBox SelectedHitbox
        {
            get
            {
                return selectedHitbox;
            }
            set
            {
                selectedHitbox = value;
                buildBoxes();
                buildCurrentBox();
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

        bool editing = false;
        GrabType grabType = GrabType.None;
        private void applyZoom(int value)
        {
            zoom = value;
            MaximumSize = new Size((256 * zoom) + 6,
                (240 * zoom) + 6);

            buildBack();
            buildGrid();
            buildBoxes();
            buildCurrentBox();
        }

        private bool[,,] nonDirties;
        private Bitmap[,,] grids;
        private GridType gridTypeUsed;
        public int GridTypeUsed
        {
            get
            {
                return (int)gridTypeUsed;
            }
            set
            {
                switch (value)
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
            }
        }

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
                if (activateGrid)
                {
                    buildGrid();
                }
                else
                {
                    grid.Image = new Bitmap(Width, Height);
                }
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
            }
        }

        PictureBox grid, boxes, currentBox;
        private Bitmap gridBoxImg;

        public InteractionGrid()
        {
            InitializeComponent();
            Color t = Color.FromArgb(0, 0, 0, 0);
            grid = new PictureBox
            {
                Margin = new Padding(0, 0, 0, 0),
                Padding = new Padding(0, 0, 0, 0),
                Parent = this,
                Location = new Point(0, 0),
                BackColor = t
            };
            boxes = new PictureBox
            {
                Margin = new Padding(0, 0, 0, 0),
                Padding = new Padding(0, 0, 0, 0),
                Parent = grid,
                Location = new Point(0, 0),
                BackColor = t
            };
            currentBox = new PictureBox
            {
                Margin = new Padding(0, 0, 0, 0),
                Padding = new Padding(0, 0, 0, 0),
                Parent = boxes,
                Location = new Point(0, 0),
                BackColor = t
            };

            currentBox.MouseDown += mouseDown;
            currentBox.MouseMove += mouseMove;
            currentBox.MouseUp += mouseUp;
        }

        private void mouseUp(object sender, MouseEventArgs e)
        {
            editing = false;
        }

        private void mouseDown(object sender, MouseEventArgs e)
        {
            editing = true;
        }

        private void mouseMove(object sender, MouseEventArgs e)
        {
            if (SelectedHitbox == null) return;

            int x = e.X;
            int y = e.Y;
            int xoff = SelectedHitbox.XOffset + 128;
            xoff *= zoom;
            int yoff = SelectedHitbox.YOffset + 112;
            yoff *= zoom;

            if (SelectedHitbox.Type == HitBoxType.Rectangle)
            {
                RectangleHitBox r = (RectangleHitBox)SelectedHitbox;
                int w = r.Width * zoom;
                int h = r.Height * zoom;

                if (editing)
                {
                    int zacc = GridAccuracy * zoom;
                    int xAcc = (x / zacc) * zacc;
                    int yAcc = (y / zacc) * zacc;
                    int right = xoff + w;
                    int bottom = yoff + h;

                    if (grabType == GrabType.Top)
                    {
                        yAcc += zacc;
                        if (y % zacc != 0) yAcc -= zacc;
                        if (yAcc >= bottom - zacc) yAcc = bottom - zacc;
                        if (yAcc < 0) yAcc = 0;

                        r.YOffset = (yAcc / zoom) - 112;
                        r.Height = (bottom - yAcc) / Zoom;
                    }
                    else if (grabType == GrabType.Bottom)
                    {
                        if (y % zacc != 0) yAcc += zacc;
                        if (yAcc - yoff <= zacc) yAcc = yoff + zacc;
                        if (yAcc >= 240 * zoom) yAcc = 240 * zoom;

                        r.Height = (yAcc - yoff) / Zoom;
                    }
                    else if (grabType == GrabType.Left)
                    {
                        xAcc += zacc;
                        if (x % zacc != 0) xAcc -= zacc;
                        if (xAcc >= right - zacc) xAcc = right - zacc;
                        if (xAcc < 0) xAcc = 0;

                        r.XOffset = (xAcc / zoom) - 128;
                        r.Width = (right - xAcc) / Zoom;
                    }
                    else if (grabType == GrabType.Right)
                    {
                        if (x % zacc != 0) xAcc += zacc;
                        if (xAcc - xoff <= zacc) xAcc = xoff + zacc;
                        if (xAcc >= 256 * zoom) xAcc = 256 * zoom;

                        r.Width = (xAcc - xoff) / Zoom;
                    }
                    else if (grabType == GrabType.TopLeft)
                    {
                        yAcc += zacc;
                        if (y % zacc != 0) yAcc -= zacc;
                        if (yAcc >= bottom - zacc) yAcc = bottom - zacc;
                        if (yAcc < 0) yAcc = 0;

                        r.YOffset = (yAcc / zoom) - 112;
                        r.Height = (bottom - yAcc) / Zoom;

                        xAcc += zacc;
                        if (x % zacc != 0) xAcc -= zacc;
                        if (xAcc >= right - zacc) xAcc = right - zacc;
                        if (xAcc < 0) xAcc = 0;

                        r.XOffset = (xAcc / zoom) - 128;
                        r.Width = (right - xAcc) / Zoom;
                    }
                    else if (grabType == GrabType.TopRight)
                    {
                        yAcc += zacc;
                        if (y % zacc != 0) yAcc -= zacc;
                        if (yAcc >= bottom - zacc) yAcc = bottom - zacc;
                        if (yAcc < 0) yAcc = 0;

                        r.YOffset = (yAcc / zoom) - 112;
                        r.Height = (bottom - yAcc) / Zoom;

                        if (x % zacc != 0) xAcc += zacc;
                        if (xAcc - xoff <= zacc) xAcc = xoff + zacc;
                        if (xAcc >= 256 * zoom) xAcc = 256 * zoom;

                        r.Width = (xAcc - xoff) / Zoom;
                    }
                    else if (grabType == GrabType.BottomLeft) 
                    {
                        if (y % zacc != 0) yAcc += zacc;
                        if (yAcc - yoff <= zacc) yAcc = yoff + zacc;
                        if (yAcc >= 240 * zoom) yAcc = 240 * zoom;

                        r.Height = (yAcc - yoff) / Zoom;

                        xAcc += zacc;
                        if (x % zacc != 0) xAcc -= zacc;
                        if (xAcc >= right - zacc) xAcc = right - zacc;
                        if (xAcc < 0) xAcc = 0;

                        r.XOffset = (xAcc / zoom) - 128;
                        r.Width = (right - xAcc) / Zoom;
                    }
                    else if (grabType == GrabType.BottomRight)
                    {
                        if (y % zacc != 0) yAcc += zacc;
                        if (yAcc - yoff <= zacc) yAcc = yoff + zacc;
                        if (yAcc >= 240 * zoom) yAcc = 240 * zoom;

                        r.Height = (yAcc - yoff) / Zoom;

                        if (x % zacc != 0) xAcc += zacc;
                        if (xAcc - xoff <= zacc) xAcc = xoff + zacc;
                        if (xAcc >= 256 * zoom) xAcc = 256 * zoom;

                        r.Width = (xAcc - xoff) / Zoom;
                    }

                    buildCurrentBox();
                    return;
                }

                if (x >= xoff - 2 &&
                    x <= xoff + 2 &&
                    y >= yoff - 2 &&
                    y <= yoff + 2)
                {
                    Cursor = Cursors.SizeNWSE;
                    grabType = GrabType.TopLeft;
                }
                else if (x >= xoff + w - 2 &&
                    x <= xoff + w + 2 &&
                    y >= yoff - 2 &&
                    y <= yoff + 2)
                {
                    Cursor = Cursors.SizeNESW;
                    grabType = GrabType.TopRight;
                }
                else if (x >= xoff - 2 &&
                    x <= xoff + 2 &&
                    y >= yoff + h - 2 &&
                    y <= yoff + h + 2)
                {
                    Cursor = Cursors.SizeNESW;
                    grabType = GrabType.BottomLeft;
                }
                else if (x >= xoff + w - 2 &&
                    x <= xoff + w + 2 &&
                    y >= yoff + h - 2 &&
                    y <= yoff + h + 2)
                {
                    Cursor = Cursors.SizeNWSE;
                    grabType = GrabType.BottomRight;
                }
                else if (x >= xoff && x <= xoff + w &&
                    y >= yoff && y <= yoff + 2)
                {
                    Cursor = Cursors.SizeNS;
                    grabType = GrabType.Top;
                }
                else if (x >= xoff && x <= xoff + w &&
                    y >= yoff + h && y <= yoff + h + 2)
                {
                    Cursor = Cursors.SizeNS;
                    grabType = GrabType.Bottom;
                }
                else if (x >= xoff && x <= xoff + 2 &&
                    y >= yoff && y <= yoff + h)
                {
                    Cursor = Cursors.SizeWE;
                    grabType = GrabType.Left;
                }
                else if (x >= xoff + w && x <= xoff + w + 2 &&
                    y >= yoff && y <= yoff + h)
                {
                    Cursor = Cursors.SizeWE;
                    grabType = GrabType.Right;
                }
                else if (x >= xoff && x <= xoff + w &&
                    y >= yoff && y <= yoff + h)
                {
                    Cursor = Cursors.SizeAll;
                    grabType = GrabType.Center;
                }
                else
                {
                    Cursor = Cursors.Default;
                    grabType = GrabType.None;
                }
            }
            
        }

        void buildBack()
        {
            Image = new Bitmap(Width, Height);

            Bitmap b = null;
            if (selectedFrame != null)
                b = selectedFrame.GetBitmap(256, 240, zoom);

            using (Graphics g = Graphics.FromImage(Image))
            {
                g.InterpolationMode = InterpolationMode.NearestNeighbor;
                try
                {
                    g.FillRectangle(new SolidBrush(ColorPalette.GetGlobalColor(0)),
                        0, 0, Width, Height);
                }
                catch { }
                if (b != null)
                    g.DrawImage(b, 0, 0);
            }
        }
        #region BuildGrid
        private void buildGrid()
        {
            switch (gridTypeUsed)
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
                    grid.Image = grids[gridAccuracy - 1, zoom - 1, 2];
                grid.Size = new Size(grid.Image.Width, grid.Image.Height);
                gridBoxImg = grids[gridAccuracy - 1, zoom - 1, 2];
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
                grid.Image = grids[gridAccuracy - 1, zoom - 1, 2];
            grid.Size = new Size(grid.Image.Width, grid.Image.Height);
            gridBoxImg = grids[gridAccuracy - 1, zoom - 1, 2];
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
                    grid.Image = grids[gridAccuracy - 1, zoom - 1, 1];
                grid.Size = new Size(grid.Image.Width, grid.Image.Height);
                gridBoxImg = grids[gridAccuracy - 1, zoom - 1, 1];
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
                grid.Image = grids[gridAccuracy - 1, zoom - 1, 1];
            grid.Size = new Size(grid.Image.Width, grid.Image.Height);
            gridBoxImg = grids[gridAccuracy - 1, zoom - 1, 1];
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
                    grid.Image = grids[gridAccuracy - 1, zoom - 1, 0];
                grid.Size = new Size(grid.Image.Width, grid.Image.Height);
                gridBoxImg = grids[gridAccuracy - 1, zoom - 1, 0];
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
                grid.Image = grids[gridAccuracy - 1, zoom - 1, 0];
            grid.Size = new Size(grid.Image.Width, grid.Image.Height);
            gridBoxImg = grids[gridAccuracy - 1, zoom - 1, 0];
        }
        #endregion

        void buildBoxes()
        {
            if (selectedFrame == null || selectedFrame.HitBoxes.Count <= 0)
                return;
            boxes.Size = new Size(Width, Height);
            boxes.Image = new Bitmap(Width, Height);
            using (Graphics g = Graphics.FromImage(boxes.Image))
            {
                foreach(HitBox h in selectedFrame.HitBoxes)
                {
                    if (selectedHitbox != h)
                        h.Draw(g, 128, 112, zoom);
                }
            }
        }
        void buildCurrentBox()
        {
            if (selectedHitbox == null) return;
            currentBox.Size = new Size(Width, Height);
            currentBox.Image = new Bitmap(Width, Height);

            using (Graphics g = Graphics.FromImage(currentBox.Image))
            {
                selectedHitbox.Draw(g, 128, 112, zoom, 2);
            }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
    }
}
