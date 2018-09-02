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
    public partial class InteractionGrid : PictureBox
    {

        Frame selectedFrame;
        HitBox selectedHitbox;
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
            grid = new PictureBox
            {
                Margin = new Padding(0, 0, 0, 0),
                Padding = new Padding(0, 0, 0, 0),
                Parent = this,
                Location = new Point(0, 0),
                BackColor = Color.Transparent
            };
            boxes = new PictureBox
            {
                Margin = new Padding(0, 0, 0, 0),
                Padding = new Padding(0, 0, 0, 0),
                Parent = grid,
                Location = new Point(0, 0),
                BackColor = Color.Transparent
            };
            currentBox = new PictureBox
            {
                Margin = new Padding(0, 0, 0, 0),
                Padding = new Padding(0, 0, 0, 0),
                Parent = boxes,
                Location = new Point(0, 0),
                BackColor = Color.Transparent
            };
        }

        void buildBack()
        {
            Image = new Bitmap(Width, Height);

            Bitmap b = null;
            if (selectedFrame != null)
                selectedFrame.GetBitmap(128, 112);

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
                    g.DrawImage(b, 0, 0, b.Width * zoom, Height * zoom);
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
                selectedHitbox.Draw(g, 128, 112, zoom, 3);
            }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
    }
}
