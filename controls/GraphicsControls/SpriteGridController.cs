using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SMWControlibBackend.Graphics;

namespace SMWControlibControls.GraphicsControls
{
    public partial class SpriteGridController : UserControl
    {
        public List<TileMask> Tiles
        {
            set
            {
                spriteGrid1.Tiles = value;
            }
        }
        public TileMask[] NewTiles
        {
            set
            {
                spriteGrid1.NewTiles = value;
            }
        }
        public Size MaxS { get; private set; }
        public event Action ZoomChanged, MidChanged;

        public int MidX
        {
            get
            {
                return spriteGrid1.MidX;
            }
            set
            {
                trackBar1.Value = value;
            }
        }
        public int MidY
        {
            get
            {
                return spriteGrid1.MidY;
            }
            set
            {
                trackBar2.Value = value;
            }
        }

        public SpriteGridController()
        {
            InitializeComponent();
            spriteGrid1.SizeChanged += sizeChanged;
            SizeChanged += sizeChanged;
            grid.Checked = spriteGrid1.ActivateGrid;
            grid.CheckedChanged += checkedChanged;
            settings.Click += click;
            grid.Checked = true;
            trackBar1.ValueChanged += tb1ValueChanged;
            trackBar2.ValueChanged += tb2ValueChanged;
            zoom.SelectedIndexChanged += selectedIndexChanged;
            zoom.SelectedIndex = 1;
            cellSize.SelectedIndexChanged += cellSizeSelectedIndexChanged;
            cellSize.SelectedIndex = 3;
            moveLeft.Click += moveLeftClick;
            moveUp.Click += moveUpClick;
            moveDown.Click += moveDownClick;
            moveRight.Click += moveRightClick;
            mirrorH.Click += mirrorHClick;
            mirrorV.Click += mirrorVClick;
            spriteGrid1.MidChanged += midChange;
            toolTip1.SetToolTip(mirrorH,
                "Flip selected tiles horizontally.\nHotkey: 'H'");
            toolTip1.SetToolTip(mirrorV,
                "Flip selected tiles vertically.\nHotkey: 'V'");
            toolTip1.SetToolTip(moveLeft, 
                "Move the selected tiles to Left.\nHotkey: 'A'");
            toolTip1.SetToolTip(moveUp,
                "Move the selected tiles to Up.\nHotkey: 'W'");
            toolTip1.SetToolTip(moveDown,
                "Move the selected tiles to Down.\nHotkey: 'S'");
            toolTip1.SetToolTip(moveRight,
                "Move the selected tiles to Right.\nHotkey: 'D'");
            toolTip1.SetToolTip(spriteGrid1.ToolTipControl, "Here you can build the frame.\n" +
                                  "Select tiles and then you can add them\n" +
                                  "to the grid with 'Right Click'.\n" +
                                  "You can select tiles and move them with\n" +
                                  "the mouse, also you can delete them pressing\n" +
                                  "'Delete' key.");
            toolTip1.SetToolTip(grid, "Show or Hide the grid.");
            toolTip1.SetToolTip(settings, "Change some properties of the\n" +
                                         "grid, for example, the color.");
        }

        private void midChange()
        {
            MidChanged?.Invoke();
        }

        public void MoveLeft()
        {
            spriteGrid1.MoveSelection(-spriteGrid1.GridAccuracy, 0);
        }

        public void MoveUp()
        {
            spriteGrid1.MoveSelection(0, -spriteGrid1.GridAccuracy);
        }

        public void MoveDown()
        {
            spriteGrid1.MoveSelection(0, spriteGrid1.GridAccuracy);
        }

        public void MoveRight()
        {
            spriteGrid1.MoveSelection(spriteGrid1.GridAccuracy, 0);
        }

        private void mirrorHClick(object sender, EventArgs e)
        {
            spriteGrid1.Mirror(true, false);
        }

        private void mirrorVClick(object sender, EventArgs e)
        {
            spriteGrid1.Mirror(false, true);
        }

        private void moveLeftClick(object sender, EventArgs e)
        {
            MoveLeft();
        }
        private void moveUpClick(object sender, EventArgs e)
        {
            MoveUp();
        }
        private void moveDownClick(object sender, EventArgs e)
        {
            MoveDown();
        }
        private void moveRightClick(object sender, EventArgs e)
        {
            MoveRight();
        }

        Zoom[] cellSizes = { 1, 2, 4, 8, 16 };
        private void cellSizeSelectedIndexChanged(object sender, EventArgs e)
        {
            spriteGrid1.GridAccuracy = cellSizes[cellSize.SelectedIndex];
        }

        public void AdaptSize()
        {
            MaximumSize = new Size(spriteGrid1.MaximumSize.Width + trackBar2.Width + 8,
                spriteGrid1.MaximumSize.Height + panel2.Height
                + trackBar1.Height + panel3.Height + 8);
            MaxS = MaximumSize;
        }

        private void selectedIndexChanged(object sender, EventArgs e)
        {
            spriteGrid1.Zoom = zoom.SelectedIndex + 1;
            spriteGrid1.Size = new Size(spriteGrid1.MaximumSize.Width,
                spriteGrid1.MaximumSize.Height);
            AdaptSize();
            ZoomChanged?.Invoke();
        }

        private void tb1ValueChanged(object sender, EventArgs e)
        {
            spriteGrid1.MidX = (trackBar1.Value - 136);
        }
        private void tb2ValueChanged(object sender, EventArgs e)
        {
            spriteGrid1.MidY = (120 - trackBar2.Value);
        }

        public void Init()
        {
            try
            {
                SpriteGridSettingsContainer set =
                    SpriteGridSettingsContainer.Deserialize(
                        "Settings/GridSettings.set");
                spriteGrid1.GridColor =
                    Color.FromArgb(set.GridColorR,
                    set.GridColorG, set.GridColorB);
                spriteGrid1.CenterSquareColor =
                    Color.FromArgb(set.CenterSquareColorR,
                    set.CenterSquareColorG, set.CenterSquareColorB);
                spriteGrid1.SelectionFillColor =
                    Color.FromArgb(set.SelectionRectangleColorR,
                    set.SelectionRectangleColorG,
                    set.SelectionRectangleColorB);
                spriteGrid1.SelectionBorderColor =
                    Color.FromArgb(set.SelectedTilesColorR,
                    set.SelectedTilesColorG, set.SelectedTilesColorB);
                spriteGrid1.BackgroundColor = Color.FromArgb(set.BackgroundColorR,
                    set.BackgroundColorG, set.BackgroundColorB);
                spriteGrid1.ActivateCenterSquare =
                    set.EnableCenterSquare;
                spriteGrid1.GridTypeUsed = set.GridType;
            }
            catch { }
            selectedIndexChanged(null, null);
        }
        private void click(object sender, EventArgs e)
        {
            if (SpriteGridSettings.Show(ParentForm,
                spriteGrid1.GridColor,
                spriteGrid1.CenterSquareColor, 
                spriteGrid1.SelectionFillColor,
                spriteGrid1.SelectionBorderColor, 
                spriteGrid1.BackgroundColor,
                spriteGrid1.ActivateCenterSquare,
                spriteGrid1.GridTypeUsed) == DialogResult.OK)
            {
                spriteGrid1.GridColor = 
                    SpriteGridSettings.GridColor;
                spriteGrid1.CenterSquareColor =
                    SpriteGridSettings.CenterSquareColor;
                spriteGrid1.SelectionFillColor =
                    SpriteGridSettings.SelectionRectangleColor;
                spriteGrid1.SelectionBorderColor =
                    SpriteGridSettings.SelectedTilesColor;
                spriteGrid1.BackgroundColor = SpriteGridSettings.BackgroundColor;
                spriteGrid1.ActivateCenterSquare =
                    SpriteGridSettings.EnableCenterSquare;
                spriteGrid1.GridTypeUsed = (int)SpriteGridSettings.Type;

                SpriteGridSettingsContainer ser =
                    new SpriteGridSettingsContainer
                    {
                        GridColorR =
                        SpriteGridSettings.GridColor.R,
                        GridColorG =
                        SpriteGridSettings.GridColor.G,
                        GridColorB =
                        SpriteGridSettings.GridColor.B,
                        CenterSquareColorR =
                        SpriteGridSettings.CenterSquareColor.R,
                        CenterSquareColorG =
                        SpriteGridSettings.CenterSquareColor.G,
                        CenterSquareColorB =
                        SpriteGridSettings.CenterSquareColor.B,
                        SelectionRectangleColorR =
                        SpriteGridSettings.SelectionRectangleColor.R,
                        SelectionRectangleColorG =
                        SpriteGridSettings.SelectionRectangleColor.G,
                        SelectionRectangleColorB =
                        SpriteGridSettings.SelectionRectangleColor.B,
                        SelectedTilesColorR =
                        SpriteGridSettings.SelectedTilesColor.R,
                        SelectedTilesColorG =
                        SpriteGridSettings.SelectedTilesColor.G,
                        SelectedTilesColorB =
                        SpriteGridSettings.SelectedTilesColor.B,
                        BackgroundColorR =
                        SpriteGridSettings.BackgroundColor.R,
                        BackgroundColorG =
                        SpriteGridSettings.BackgroundColor.G,
                        BackgroundColorB =
                        SpriteGridSettings.BackgroundColor.B,
                        EnableCenterSquare =
                        SpriteGridSettings.EnableCenterSquare,
                        GridType = (int)SpriteGridSettings.Type
                    };
                ser.Serialize("Settings/GridSettings.set");
            }
        }

        private void checkedChanged(object sender, EventArgs e)
        {
            spriteGrid1.ActivateGrid = grid.Checked;
        }
        public void DeleteSelection()
        {
            spriteGrid1.DeleteSelection();
        }
        private void sizeChanged(object sender, EventArgs e)
        {
            int mw = Math.Max(spriteGrid1.Width + trackBar2.Width + 9,
                Math.Max(panel3.Width, panel2.Width));
            int mh = spriteGrid1.Height + trackBar1.Height + panel2.Height +
                panel3.Height + 9;

            MaximumSize = new Size(mw, mh);
            trackBar1.Width = spriteGrid1.Width + 12;
            trackBar2.Height = spriteGrid1.Height + 12;
            int w = Width - panel5.Width;
            w /= 2;
            panel4.Width = w + 18;
            panel6.Width = w - 18;
        }

        public void ReDraw()
        {
            spriteGrid1.ReDraw();
        }
    }
}
