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

        public InteractionMenu()
        {
            InitializeComponent();
            frameSelector.SelectedIndexChanged += frameSelectorSelectedIndexChanged;
            hbSelector.SelectedIndexChanged += hbSelectorSelectedIndexChanged;
            createHB.Click += createHBClick;
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
        }
    }
}
