using System;
using System.Drawing;
using System.Windows.Forms;
using SMWControlibBackend.Graphics;
using SMWControlibBackend.Interaction;
using SMWControlibBackend.Graphics.Frames;

namespace SMWControlibControls.InteractionControls
{
    public partial class ResizeableInteractionGrid : UserControl
    {
        public Zoom Zoom
        {
            set
            {
                interactionGrid1.Zoom = value;
            }
        }

        public Zoom CellSize
        {
            set
            {
                interactionGrid1.GridAccuracy = value;
            }
        }

        public Color SelectedInteractionPointColor
        {
            set
            {
                interactionGrid1.SelectedInteractionPointColor = value;
            }
        }

        public Color SelectedHitboxFillColor
        {
            set
            {
                interactionGrid1.SelectedHitboxFillColor = value;
            }
        }

        public Color SelectedHitboxBorderColor
        {
            set
            {
                interactionGrid1.SelectedHitboxBorderColor = value;
            }
        }

        public bool SelectingHitbox
        {
            set
            {
                interactionGrid1.SelectingHitbox = value;
            }
        }

        public InteractionPoint SelectedInteractionPoint
        {
            set
            {
                interactionGrid1.SelectedInteractionPoint = value;
            }
        }

        public HitBox SelectedHitbox
        {
            set
            {
                interactionGrid1.SelectedHitbox = value;
            }
        }

        public Frame SelectedFrame
        {
            set
            {
                interactionGrid1.SelectedFrame = value;
            }
        }

        public ResizeableInteractionGrid()
        {
            InitializeComponent();
            interactionGrid1.SizeChanged += interactionGridSizeChanged;
            SizeChanged += interactionGridSizeChanged;
            Zoom = 3;
        }

        private void interactionGridSizeChanged(object sender, EventArgs e)
        {
            int w = Width - interactionGrid1.Width;
            w /= 2;
            w -= 10;
            int h = Height - interactionGrid1.Height;
            h /= 2;
            if (w < 0) w = 0;
            if (h < 0) h = 0;

            left.Width = w;
            right.Width = w;
            top.Height = h;
            bottom.Height = h;
        }
    }
}
