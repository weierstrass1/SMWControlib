﻿using System.Windows.Forms;
using SMWControlibBackend.Graphics;
using SMWControlibControls.GraphicsControls;

namespace TestWindows
{
    public partial class testwindows : Form
    {
        public testwindows()
        {
            InitializeComponent();
            gfxButton1.Target = spriteGFXBox1;
            gfxButton2.Target = spriteGFXBox1;
            gfxButton3.Target = spriteGFXBox2;
            gfxButton4.Target = spriteGFXBox2;
            spriteGFXBox1.SelectionChanged += selectionChanged;
            spriteGFXBox2.SelectionChanged += selectionChanged;
            paletteButton1.target = paletteBox1;
            frameCreator1.SelectionChanged += frameCreator1_SelectionChanged;
            spriteGFXBox1.GraphicsLoaded += graphicsLoaded;
            spriteGFXBox2.GraphicsLoaded += graphicsLoaded;
        }

        private void graphicsLoaded()
        {
            resizeableSpriteGridController1.ReDraw();
        }

        private void frameCreator1_SelectionChanged()
        {
            if (frameCreator1.SelectedFrame == null)
                resizeableSpriteGridController1.Tiles = null;
            else
                resizeableSpriteGridController1.Tiles = frameCreator1.SelectedFrame.Tiles;
        }

        private void selectionChanged(object sender)
        {
            SpriteGFXBox gb = (SpriteGFXBox)sender;
            resizeableSpriteGridController1.NewTiles =
                gb.GetBitmapsFromSelectedTiles(false, false, TilePriority.AboveAllLayersP0);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if(keyData==Keys.Delete)
            {
                resizeableSpriteGridController1.DeleteSelection();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
