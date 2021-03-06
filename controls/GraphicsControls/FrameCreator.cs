﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SMWControlibBackend.Graphics;
using SMWControlibBackend.Graphics.Frames;

namespace SMWControlibControls.GraphicsControls
{
    public partial class FrameCreator : UserControl
    {
        public event Action<Frame> FrameAdded, FrameDeleted;
        List<Frame> frames;

        public Frame[] Frames
        {
            get
            {
                return frames.ToArray();
            }
        }

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
            create.Click += createClick;
            delete.Click += deleteClick;
            settings.Click += settingsClick;
            SelectedFrame = null;
            frameSelector.SelectedIndexChanged += selectedIndexChanged;
        }

        public void LoadProjectFrames(Frame[] projFrames)
        {
            frames.Clear();

            foreach(Frame f in projFrames)
            {
                frames.Add(f);
            }
            SelectedFrame = null;
            refreshFrames();
            if (frames.Count > 0) frameSelector.SelectedIndex = 0;
        }

        public void AddFrames(List<Frame> newFrames, int w, int h)
        {
            foreach(Frame f in newFrames)
            {
                f.MidX = 136;
                f.MidY = 120;
                foreach (TileMask tm in f.Tiles)
                {
                    tm.XDisp = (tm.XDisp - w) + f.MidX * 2;
                    tm.YDisp = (tm.YDisp - h) + f.MidY * 2 + 2;
                }
                frames.Add(f);
            }
            refreshFrames();

            foreach(Frame f in frames)
            {
                SelectedFrame = f;
                SelectedFrame.GetBitmap();
            }

            if (SelectedFrame == null)
                SelectedFrame = frames[0];
        }

        public void ChangeMid(int MidX, int MidY)
        {
            foreach(Frame f in frames)
            {
                f.MidX = MidX;
                f.MidY = MidY;
            }
        }

        private void settingsClick(object sender, EventArgs e)
        {
            if (frames == null || frames.Count <= 0)
            {
                MessageBox.Show("You must create a frame first.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Frame[] framesArr = frames.ToArray();
            if (FramesSettingsDialog.Show(ParentForm, framesArr)
                == DialogResult.OK)
            {
                frames.Clear();
                for (int i = 0; i < framesArr.Length; i++)
                {
                    frames.Add(framesArr[i]);
                }
                refreshFrames();
            }
        }

        private void selectedIndexChanged(object sender, EventArgs e)
        {
            if (frameSelector.SelectedItem.GetType() == typeof(Frame))
                SelectedFrame = (Frame)frameSelector.SelectedItem;
            else
                SelectedFrame = null;
        }

        private void createClick(object sender, EventArgs e)
        {
            if (NewFrameDialog.Show(ParentForm, frames)
                == DialogResult.OK)
            {
                FrameAdded?.Invoke(frames[frames.Count - 1]);
                refreshFrames();
                if(NewFrameDialog.AutoSelect)
                {
                    frameSelector.SelectedIndex = frames.Count - 1;
                }
            }
        }
        private void deleteClick(object sender, EventArgs e)
        {
            if (SelectedFrame != null)
            {
                int i = frames.IndexOf(SelectedFrame);
                FrameDeleted?.Invoke(SelectedFrame);
                frames.Remove(SelectedFrame);
                if (frames.Count == 0)
                {
                    SelectedFrame = null;
                }
                else
                {
                    if (i >= frames.Count)
                    {
                        i = frames.Count - 1;
                    }
                    SelectedFrame = frames[i];
                }
                refreshFrames();
            }
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
        }

    }
}
