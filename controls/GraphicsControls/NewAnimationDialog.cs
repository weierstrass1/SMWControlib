﻿using SMWControlibBackend.Graphics.Frames;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMWControlibControls.GraphicsControls
{
    public partial class NewAnimationDialog : Form
    {
        public static bool AutoSelect = true;
        List<Animation> animations;

        public NewAnimationDialog()
        {
            InitializeComponent();
            accept.Click += click;
            autosel.CheckedChanged += autoselCheckedChanged;
            name.TextChanged += textChanged;
        }

        private void autoselCheckedChanged(object sender, EventArgs e)
        {
            AutoSelect = autosel.Checked;
        }

        private void textChanged(object sender, EventArgs e)
        {
            int st = name.SelectionStart;
            bool invalidChar = false;
            StringBuilder sb = new StringBuilder();
            foreach (char c in name.Text)
            {
                if ((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z')
                    || (c >= '0' && c <= '9') || c == '-' || c == '_')
                {
                    sb.Append(c);
                }
                else
                {
                    invalidChar = true;
                }
            }

            if (invalidChar)
            {
                name.Text = sb.ToString();
                if (st - 1 < 0) st = 1;
                name.SelectionStart = st - 1;
            }
            else name.SelectionStart = st;
        }

        private void click(object sender, EventArgs e)
        {
            Animation NewAnimation = null;

            if (name.Text == null || name.Text.Length == 0 || name.Text == "" ||
                (!(name.Text[0] >= 'a' && name.Text[0] <= 'z') &&
                !(name.Text[0] >= 'A' && name.Text[0] <= 'Z')))
                name.Text = "f" + name.Text;

            validName();

            NewAnimation = new Animation()
            {
                Name = name.Text
            };

            animations.Add(NewAnimation);
            DialogResult = DialogResult.OK;
            Dispose();
        }

        private void validName()
        {
            bool notValid = true;
            while (notValid)
            {
                notValid = false;
                foreach (Animation f in animations)
                {
                    if (f.Name == name.Text)
                    {
                        notValid = true;
                        name.Text += "0";
                        break;
                    }
                }
            }

        }

        public static DialogResult Show(IWin32Window Owner,
            List<Animation> Animations)
        {
            NewAnimationDialog nfd = new NewAnimationDialog()
            {
                animations = Animations
            };

            nfd.name.Text = "Animation" + Animations.Count;

            nfd.autosel.Checked = AutoSelect;

            return nfd.ShowDialog(Owner);
        }
    }
}
