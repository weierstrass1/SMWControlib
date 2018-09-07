using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SMWControlibBackend.Graphics.Frames;
using SMWControlibBackend.Interaction;
using SMWControlibBackend.Graphics;

namespace SMWControlibControls.InteractionControls
{
    public partial class InteractionMenu : UserControl
    {
        List<Frame> frames;
        Frame selectedFrame;
        public Frame SelectedFrame
        {
            get
            {
                return selectedFrame;
            }
            private set
            {
                selectedFrame = value;
                FrameSelectionChanged?.Invoke();
            }
        }
        HitBox selectedHitBox;
        public HitBox SelectedHitBox
        {
            get
            {
                return selectedHitBox;
            }
            private set
            {
                selectedHitBox = value;
                HitboxSelectionChanged?.Invoke();
            }
        }
        InteractionPoint selectedInteractionPoint;
        public InteractionPoint SelectedInteractionPoint
        {
            get
            {
                return selectedInteractionPoint;
            }
            private set
            {
                selectedInteractionPoint = value;
                InteractionPointSelectionChanged?.Invoke();
            }
        }

        public event Action FrameSelectionChanged,
            HitboxSelectionChanged, InteractionPointSelectionChanged;
        public event Action<bool> OptionChanged;
        public event Action<Color> BorderColorChanged, FillColorChanged,
            IPColorChanged;
        public event Action<int> ZoomChanged, CellSizeChanged;

        public InteractionMenu()
        {
            InitializeComponent();
            frameSelector.SelectedIndexChanged += frameSelectorSelectedIndexChanged;
            hbSelector.SelectedIndexChanged += hbSelectorSelectedIndexChanged;
            ipSelector.SelectedIndexChanged += ipSelectorSelectedIndexChanged;
            shareWithRadioButton.CheckedChanged += shareWithRadioButtonCheckedChanged;
            createHB.Click += createHBClick;
            createIP.Click += createIPClick;
            tabControl1.SelectedIndexChanged += tabSelectedIndexChanged;
            borderC.DoubleClick += borderCDoubleClick;
            fillC.DoubleClick += fillCDoubleClick;
            ipColor.DoubleClick += ipColorDoubleClick;
            zoom.SelectedIndexChanged += zoomSelectedIndexChanged;
            cellSize.SelectedIndexChanged += cellSizeSelectedIndexChanged;
            ipType.SelectedIndex = 1;
            zoom.SelectedIndex = 2;
            cellSize.SelectedIndex = 3;
        }

        Zoom[] cellSizes = { 1, 2, 4, 8, 16 };
        private void cellSizeSelectedIndexChanged(object sender, EventArgs e)
        {
            CellSizeChanged?.Invoke(cellSizes[cellSize.SelectedIndex]);
        }

        private void zoomSelectedIndexChanged(object sender, EventArgs e)
        {
            ZoomChanged?.Invoke(zoom.SelectedIndex + 1);
        }

        private void shareWithRadioButtonCheckedChanged(object sender, EventArgs e)
        {
            shareSelector.Enabled = shareWithRadioButton.Checked;
        }

        private void ipColorDoubleClick(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog(ParentForm) == DialogResult.OK)
            {
                if (colorDialog1.Color == Color.FromArgb(1, 1, 1))
                    colorDialog1.Color = Color.FromArgb(0, 0, 0);

                ipColor.BackColor = colorDialog1.Color;
                IPColorChanged?.Invoke(ipColor.BackColor);
            }
        }

        private void fillCDoubleClick(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog(ParentForm) == DialogResult.OK)
            {
                if (colorDialog1.Color == Color.FromArgb(1, 1, 1))
                    colorDialog1.Color = Color.FromArgb(0, 0, 0);

                fillC.BackColor = colorDialog1.Color;
                FillColorChanged?.Invoke(fillC.BackColor);
            }
        }

        private void borderCDoubleClick(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog(ParentForm) == DialogResult.OK)
            {
                if (colorDialog1.Color == Color.FromArgb(1, 1, 1))
                    colorDialog1.Color = Color.FromArgb(0, 0, 0);

                borderC.BackColor = colorDialog1.Color;
                BorderColorChanged?.Invoke(borderC.BackColor);
            }
        }

        private void tabSelectedIndexChanged(object sender, EventArgs e)
        {
            OptionChanged?.Invoke(tabControl1.SelectedIndex == 0);
        }

        private void ipSelectorSelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectedFrame == null) return;
            if (ipSelector.SelectedItem.GetType() == typeof(InteractionPoint))
                SelectedInteractionPoint = (InteractionPoint)ipSelector.SelectedItem;
            else
                SelectedInteractionPoint = null;
        }

        private void createIPClick(object sender, EventArgs e)
        {
            if (SelectedFrame == null)
            {
                MessageBox.Show("You must create a frame first.",
                       "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (NewInteractionPointDialog.Show(ParentForm, SelectedFrame)
                == DialogResult.OK)
            {
                refreshIP();
                HitBox h = selectedFrame.InteractionPoints[SelectedFrame.InteractionPoints.Count - 1];
                h.FrontColor = Color.FromArgb(120, ipColor.BackColor);
                if (NewInteractionPointDialog.AutoSelect)
                {
                    ipSelector.SelectedIndex = SelectedFrame.InteractionPoints.Count - 1;
                }
            }
        }

        private void frameSelectorSelectedIndexChanged(object sender, EventArgs e)
        {
            if (frameSelector.SelectedItem.GetType() == typeof(Frame))
                SelectedFrame = (Frame)frameSelector.SelectedItem;
            else
                SelectedFrame = null;
            refreshHB();
            if (SelectedFrame.HitBoxes.Count > 0)
                hbSelector.SelectedIndex = 0;
            refreshIP();
            if (SelectedFrame.InteractionPoints.Count > 0)
                ipSelector.SelectedIndex = 0;
        }

        public void UpdateFrameList(Frame[] Frames)
        {
            if (Frames == null || Frames.Length <= 0) 
            {
                frames = null;
                SelectedFrame = null;
                hbSelector.Items.Clear();
                ipSelector.Items.Clear();
                return;
            }

            frames = Frames.ToList();
            Frame fr = SelectedFrame;
            frameSelector.Items.Clear();

            foreach(Frame f in frames)
            {
                frameSelector.Items.Add(f);
            }

            if (frameSelector.Items != null
                && frameSelector.Items.Count > 0
                && SelectedFrame != null
                && frameSelector.Items.Contains(SelectedFrame))
                frameSelector.SelectedItem = SelectedFrame;
            else
                frameSelector.SelectedIndex = 0;
        }

        private void hbSelectorSelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectedFrame == null) return;
            if (hbSelector.SelectedItem.GetType().IsSubclassOf(typeof(HitBox)))
                SelectedHitBox = (HitBox)hbSelector.SelectedItem;
            else
                SelectedHitBox = null;
        }

        private void createHBClick(object sender, EventArgs e)
        {
            if (SelectedFrame == null)
            {
                MessageBox.Show("You must create a frame first.",
                       "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (NewHitboxDiaglog.Show(ParentForm, SelectedFrame)
                == DialogResult.OK)
            {
                refreshHB();
                HitBox h = selectedFrame.HitBoxes[SelectedFrame.HitBoxes.Count - 1];
                h.BorderColor = borderC.BackColor;
                h.FrontColor = Color.FromArgb(120, fillC.BackColor);
                if (NewHitboxDiaglog.AutoSelect)
                {
                    hbSelector.SelectedIndex = SelectedFrame.HitBoxes.Count - 1;
                }
            }
        }

        private void refreshHB()
        {
            hbSelector.Items.Clear();
            if (SelectedFrame == null || SelectedFrame.HitBoxes.Count <= 0)
                return;
            foreach (HitBox f in SelectedFrame.HitBoxes)
            {
                hbSelector.Items.Add(f);
            }
            if (SelectedHitBox != null)
                hbSelector.SelectedItem = SelectedHitBox;

            hbSelector.Refresh();
        }

        private void refreshIP()
        {
            ipSelector.Items.Clear();
            if (SelectedFrame == null || SelectedFrame.InteractionPoints.Count <= 0)
                return;
            foreach (InteractionPoint f in SelectedFrame.InteractionPoints)
            {
                ipSelector.Items.Add(f);
            }
            if (SelectedInteractionPoint != null)
                ipSelector.SelectedItem = SelectedInteractionPoint;

            ipSelector.Refresh();
        }
    }
}
