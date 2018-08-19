﻿using System;
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
    public partial class AnimationEditor : UserControl
    {
        Animation animation;
        public Frame[] Selection;
        public event Action<AnimationEditor> AddClick;
        public AnimationEditor()
        {
            InitializeComponent();
            animation = new Animation();
            buildTable();
        }

        private void buildTable()
        {
            if (animation == null || animation.Length == 0)
            {
                tableLayoutPanel1.ColumnCount = 1;

                tableLayoutPanel1.Controls.Clear();

                tableLayoutPanel1.Width = 208 * tableLayoutPanel1.ColumnCount;
                AnimationFrameEditor afex = new AnimationFrameEditor();
                afex.AddClick += addClick;

                tableLayoutPanel1.Controls.Add(afex, 0, 0);
                return;
            }
            int scroll = panel1.HorizontalScroll.Value;
            tableLayoutPanel1.ColumnCount = animation.Length;

            tableLayoutPanel1.Controls.Clear();

            tableLayoutPanel1.Width = 208 * tableLayoutPanel1.ColumnCount;

            FrameMask fm = animation[0];
            int i = 0;
            AnimationFrameEditor afe; 
            while (fm != null)
            {
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 208));

                afe = new AnimationFrameEditor
                {
                    FrameMask = fm
                };
                afe.AddClick += addClick;
                afe.RemoveClick += removeClick;
                afe.ExchangeClick += exchangeClick;
                tableLayoutPanel1.Controls.Add(afe, i, 0);
                fm = fm.Next;
                i++;
            }
            panel1.AutoScrollPosition = new Point(scroll, 0);
        }

        private void addClick(AnimationFrameEditor obj)
        {
            AddClick?.Invoke(this);

            if (Selection == null || Selection.Length <= 0) return;

            if (obj.FrameMask == null)
            {
                animation.Add(Selection, 0);
            }
            else
            {
                animation.Add(Selection, obj.FrameMask.Index);
            }
            buildTable();
        }
        private void removeClick(AnimationFrameEditor obj)
        {
            if (obj.FrameMask == null) return;

            animation.Remove(obj.FrameMask.Index);
            buildTable();
        }

        private void exchangeClick(AnimationFrameEditor obj)
        {
            if (obj.FrameMask == null) return;

            animation.Exchange(obj.FrameMask.Index);
            buildTable();
        }
    }
}
