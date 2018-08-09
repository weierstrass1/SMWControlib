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
        public SpriteGridController()
        {
            InitializeComponent();
            spriteGrid1.SizeChanged += sizeChanged;
            SizeChanged += sizeChanged;
            grid.Checked = spriteGrid1.ActivateGrid;
            grid.CheckedChanged += checkedChanged;
            settings.Click += click;
            grid.Checked = true;
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
                spriteGrid1.ActivateCenterSquare =
                    set.EnableCenterSquare;
                spriteGrid1.GridTypeUsed = set.GridType;
            }
            catch { }
        }
        private void click(object sender, EventArgs e)
        {
            if (SpriteGridSettings.Show(ParentForm,
                spriteGrid1.GridColor,
                spriteGrid1.CenterSquareColor, 
                spriteGrid1.SelectionFillColor,
                spriteGrid1.SelectionBorderColor, 
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
                spriteGrid1.ActivateCenterSquare =
                    SpriteGridSettings.EnableCenterSquare;
                spriteGrid1.GridTypeUsed = (int)SpriteGridSettings.Type;

                SpriteGridSettingsContainer ser =
                    new SpriteGridSettingsContainer()
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
            MaximumSize = new Size(spriteGrid1.MaximumSize.Width,
                spriteGrid1.MaximumSize.Height + panel2.Height);
        }
    }
}
