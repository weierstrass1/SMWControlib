using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SMWControlibBackend.Graphics.Frames;

namespace SMWControlibControls.GraphicsControls
{
    public partial class AnimationCreator : UserControl
    {
        List<Animation> anims;

        public Animation[] Animations
        {
            get
            {
                return anims.ToArray();
            }
        }

        Animation selectedAnimation;

        public Animation SelectedAnimation
        {
            get
            {
                return selectedAnimation;
            }
            private set
            {
                selectedAnimation = value;
                if(selectedAnimation != null)
                {
                    if (selectedAnimation.PlayType == PlayType.Continuous)
                    {
                        continuous.Checked = true;
                    }
                    else
                    {
                        onlyOnce.Checked = true;
                    }
                }
                SelectionChanged?.Invoke();
            }
        }

        public event Action SelectionChanged;

        public AnimationCreator()
        {
            InitializeComponent();
            anims = new List<Animation>();
            create.Click += createClick;
            delete.Click += deleteClick;
            animationSelector.SelectedIndexChanged += selectedIndexChanged;
            onlyOnce.CheckedChanged += onlyOnceCheckedChanged;
            continuous.CheckedChanged += continuousCheckedChanged;
            rename.Click += renameClick;
            info.Click += infoClick;
        }

        public void LoadProject(Animation[] animations)
        {
            anims.Clear();
            foreach(Animation an in animations)
            {
                anims.Add(an);
            }
            SelectedAnimation = null;
            refreshAnimations();
            if (anims.Count > 0) animationSelector.SelectedIndex = 0;
        }
        private void infoClick(object sender, EventArgs e)
        {
            MessageBox.Show("Not implemented yet.", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void renameClick(object sender, EventArgs e)
        {
            MessageBox.Show("Not implemented yet.", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void continuousCheckedChanged(object sender, EventArgs e)
        {
            if (SelectedAnimation == null || anims == null || anims.Count <= 0)
                return;
            if (continuous.Checked)
                SelectedAnimation.PlayType = PlayType.Continuous;
        }

        private void onlyOnceCheckedChanged(object sender, EventArgs e)
        {
            if (SelectedAnimation == null || anims == null || anims.Count <= 0)
                return;
            if (onlyOnce.Checked)
                SelectedAnimation.PlayType = PlayType.OnlyOnce;
        }

        private void deleteClick(object sender, EventArgs e)
        {
            if (SelectedAnimation != null)
            {
                int i = anims.IndexOf(SelectedAnimation);
                anims.Remove(SelectedAnimation);
                if (anims.Count == 0)
                {
                    SelectedAnimation = null;
                }
                else
                {
                    if (i >= anims.Count)
                    {
                        i = anims.Count - 1;
                    }
                    SelectedAnimation = anims[i];
                }
                refreshAnimations();
            }
        }

        private void selectedIndexChanged(object sender, EventArgs e)
        {
            if (animationSelector.SelectedItem.GetType() == typeof(Animation))
                SelectedAnimation = (Animation)animationSelector.SelectedItem;
            else
                SelectedAnimation = null;
        }

        private void createClick(object sender, EventArgs e)
        {
            if(NewAnimationDialog.Show(ParentForm, anims) == DialogResult.OK)
            {
                refreshAnimations();
                if (NewAnimationDialog.AutoSelect)
                {
                    animationSelector.SelectedIndex = anims.Count - 1;
                }
            }
        }

        private void refreshAnimations()
        {
            animationSelector.Items.Clear();
            foreach (Animation f in anims)
            {
                animationSelector.Items.Add(f);
            }
            if (SelectedAnimation != null)
                animationSelector.SelectedItem = SelectedAnimation;
           
        }
    }
}
