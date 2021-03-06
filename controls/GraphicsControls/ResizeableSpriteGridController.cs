﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SMWControlibBackend.Graphics;

namespace SMWControlibControls.GraphicsControls
{
    public partial class ResizeableSpriteGridController : UserControl
    {
        public event Action Moved
        {
            add
            {
                spriteGridController1.Moved += value;
            }
            remove
            {
                spriteGridController1.Moved -= value;
            }
        }
        public List<TileMask>Tiles
        {
            set
            {
                spriteGridController1.Tiles = value;
            }
        }
        public TileMask[] NewTiles
        {
            set
            {
                spriteGridController1.NewTiles = value;
            }
        }

        public event Action MidChanged;

        public int MidX
        {
            get
            {
                return spriteGridController1.MidX;
            }
            set
            {
                spriteGridController1.MidX = value;
            }
        }
        public int MidY
        {
            get
            {
                return spriteGridController1.MidY;
            }
            set
            {
                spriteGridController1.MidY = value;
            }
        }
        public ResizeableSpriteGridController()
        {
            InitializeComponent();
            SizeChanged += sizeChanged;
            spriteGridController1.ZoomChanged += zoomChanged;
            spriteGridController1.MidChanged += midChange;
            spriteGridController1.Init();
            spriteGridController1.MidX = MidX;
            spriteGridController1.MidY = MidY;
        }
        private void midChange()
        {
            MidChanged?.Invoke();
        }

        public void MoveLeft()
        {
            spriteGridController1.MoveLeft();
        }

        public void MoveUp()
        {
            spriteGridController1.MoveUp();
        }

        public void MoveDown()
        {
            spriteGridController1.MoveDown();
        }

        public void MoveRight()
        {
            spriteGridController1.MoveRight();
        }

        private void zoomChanged()
        {
            sizeChanged(null, null);
        }

        public void DeleteSelection()
        {
            spriteGridController1.DeleteSelection();
        }
        private void sizeChanged(object sender, EventArgs e)
        {
            spriteGridController1.AdaptSize();

            int minW = Math.Min(Width, spriteGridController1.MaxS.Width);
            int minH = Math.Min(Height, spriteGridController1.MaxS.Height);

            spriteGridController1.MaximumSize = new Size(minW, minH);

            section1.MinimumSize = new Size(spriteGridController1.MinimumSize.Width + 4,
                spriteGridController1.MinimumSize.Height + 4);
            if (Width <= spriteGridController1.MaximumSize.Width)
            {
                left.Width = 0;
                right.Width = 0;
            }
            else
            {
                int adder = Width - spriteGridController1.MaximumSize.Width;
                if (adder % 2 == 1) adder--;
                adder /= 2;
                left.Width = adder;
                right.Width = adder;
            }
            if (Height <= spriteGridController1.MaximumSize.Height)
            {
                up.Height = 0;
                down.Height = 0;
            }
            else
            {
                int adder = Height - spriteGridController1.MaximumSize.Height;
                if (adder % 2 == 1) adder--;
                adder /= 2;
                up.Height = adder;
                down.Height = adder;
            }
        }

        public void ReDraw()
        {
            spriteGridController1.ReDraw();
        }
    }
}
