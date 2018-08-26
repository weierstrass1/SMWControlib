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
    public partial class AnimationEditor : UserControl
    {
        private Animation animation;
        public Animation Animation
        {
            get
            {
                return animation;
            }
            set
            {
                animation = value;
                buildTable();
                AnimationChanged?.Invoke();
            }
        }
        private Animation _Animation
        {
            set
            {
                animation = value;
                AnimationChanged?.Invoke();
            }
        }
        public Frame[] Selection;
        public event Action<AnimationEditor> AddClick;
        public event Action AnimationChanged;
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
                afe.TimeChanged += timeChanged;
                afe.FlipXChanged += flipChanged;
                afe.FlipYChanged += flipChanged;
                tableLayoutPanel1.Controls.Add(afe, i, 0);
                fm = fm.Next;
                i++;
            }
            panel1.AutoScrollPosition = new Point(scroll, panel1.AutoScrollPosition.Y);
        }

        private void timeChanged(AnimationFrameEditor obj)
        {
            _Animation = animation;
        }

        private void flipChanged(AnimationFrameEditor obj)
        {
            _Animation = animation;
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
            _Animation = animation;
        }
        private void removeClick(AnimationFrameEditor obj)
        {
            if (obj.FrameMask == null) return;
            Control c;
            AnimationFrameEditor afex;
            int fmInd = obj.FrameMask.Index;
            animation.Remove(fmInd);
            if (animation.Length <= 0)
            {
                buildTable();
                return;
            }

            tableLayoutPanel1.Controls.Remove(obj);
            for (int i = fmInd; i < tableLayoutPanel1.ColumnCount - 1; i++)
            {

                c = tableLayoutPanel1.GetControlFromPosition(i + 1, 0);
                afex = (AnimationFrameEditor)c;
                afex.FrameMask = afex.FrameMask;
                tableLayoutPanel1.Controls.Remove(c);
                tableLayoutPanel1.Controls.Add(c, i, 0);
            }
            if (fmInd >= animation.Length)
            {
                c = tableLayoutPanel1.
                    GetControlFromPosition(animation.Length - 1, 0);
                afex = (AnimationFrameEditor)c;
                afex.FrameMask = afex.FrameMask;
            }
            tableLayoutPanel1.ColumnCount = animation.Length;
            tableLayoutPanel1.Width = 208 * tableLayoutPanel1.ColumnCount;
            _Animation = animation;
        }

        private void exchangeClick(AnimationFrameEditor obj)
        {
            if (obj.FrameMask == null) return;

            animation.Exchange(obj.FrameMask.Index);

            obj.FrameMask = obj.FrameMask;
            int ind = obj.FrameMask.Index + 1;

            AnimationFrameEditor obj2 =
                (AnimationFrameEditor)tableLayoutPanel1.GetControlFromPosition(ind, 0);
            obj2.FrameMask = obj2.FrameMask;
            _Animation = animation;
        }
    }
}
