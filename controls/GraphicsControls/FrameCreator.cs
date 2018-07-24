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

namespace SMWControlibControls.GraphicsControls
{
    public partial class FrameCreator : UserControl
    {
        List<Frame> frames;

        private Frame selectedFrame;
        public Frame SelectedFrame
        {
            get
            {
                return selectedFrame;
            }
            private set
            {
                selectedFrame = value;
                SelectionChanged?.Invoke();
            }
        }
        public event Action SelectionChanged;

        public FrameCreator()
        {
            InitializeComponent();
            frames = new List<Frame>();
            create.Click += create_Click;
            delete.Click += delete_Click;
            settings.Click += settings_Click;
            SelectedFrame = null;
            frameSelector.SelectedIndexChanged += selectedIndexChanged;
        }

        private void selectedIndexChanged(object sender, EventArgs e)
        {
            if (frameSelector.SelectedItem.GetType() == typeof(Frame))
                SelectedFrame = (Frame)frameSelector.SelectedItem;
            else
                SelectedFrame = null;
        }

        private void create_Click(object sender, EventArgs e)
        {
            if (NewFrameDialog.Show(ParentForm, frames)
                == DialogResult.OK)
            {
                refreshFrames();
            }
        }
        private void delete_Click(object sender, EventArgs e)
        {
            if (SelectedFrame != null)
            {
                frames.Remove(SelectedFrame);
                refreshFrames();
                if (frames.Count == 0) 
                        SelectedFrame = null;
            }
        }

        private void settings_Click(object sender, EventArgs e)
        {

        }
        private void refreshFrames()
        {
            frameSelector.Items.Clear();
            foreach (Frame f in frames)
            {
                frameSelector.Items.Add(f);
            }
            if (SelectedFrame != null)
                frameSelector.SelectedItem = SelectedFrame;
            else
                frameSelector.SelectedIndex = 0;
        }

    }
}
