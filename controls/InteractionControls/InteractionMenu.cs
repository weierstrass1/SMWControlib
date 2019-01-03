using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SMWControlibBackend.Graphics.Frames;
using SMWControlibBackend.Interaction;
using SMWControlibBackend.Graphics;
using System.Text.RegularExpressions;
using SMWControlibBackend.Logic;

namespace SMWControlibControls.InteractionControls
{
    public partial class InteractionMenu : UserControl
    {
        List<Frame> frames;
        Frame selectedFrame;
        List<HitBox> shareWithAllList = new List<HitBox>();
        public Frame SelectedFrame
        {
            get
            {
                return selectedFrame;
            }
            private set
            {
                selectedFrame = value;
                if (selectedFrame != null && selectedFrame.HitBoxes != null &&
                    selectedFrame.HitBoxes.Count > 0)
                {
                    SelectedHitBox = selectedFrame.HitBoxes[0];
                }
                else
                {
                    SelectedHitBox = null;
                }
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
        public event Action<string> AddText, GotoText, DelText;

        public InteractionMenu()
        {
            InitializeComponent();
            actionSelectorHB.SelectedIndex = 0;
            frameSelector.SelectedIndexChanged += frameSelectorSelectedIndexChanged;
            hbSelector.SelectedIndexChanged += hbSelectorSelectedIndexChanged;
            ipSelector.SelectedIndexChanged += ipSelectorSelectedIndexChanged;
            createHB.Click += createHBClick;
            createIP.Click += createIPClick;
            tabControl1.SelectedIndexChanged += tabSelectedIndexChanged;
            borderC.DoubleClick += borderCDoubleClick;
            fillC.DoubleClick += fillCDoubleClick;
            ipColor.DoubleClick += ipColorDoubleClick;
            newAct.Click += newActClick;
            gotoAct.Click += gotoActClick;
            delAct.Click += delActClick;
            shareAllRadioButton.CheckedChanged += shareAllRadioButtonCheckedChanged;
            shareWithRadioButton.CheckedChanged += shareWithRadioButtonCheckedChanged;
            shareSelector.SelectedIndexChanged += shareSelectorSelectedIndexChanged;
            deleteHB.Click += deleteHBClick;

            zoom.SelectedIndexChanged += zoomSelectedIndexChanged;
            cellSize.SelectedIndexChanged += cellSizeSelectedIndexChanged;
            ipType.SelectedIndex = 1;
            zoom.SelectedIndex = 2;
            cellSize.SelectedIndex = 3;
            tabControl1.TabPages.Remove(tabPage2);
        }

        private void deleteHBClick(object sender, EventArgs e)
        {
            if (selectedFrame != null && SelectedHitBox != null) 
            {
                int ind = hbSelector.SelectedIndex;
                if (selectedFrame.HitBoxes.Contains(SelectedHitBox))
                {
                    selectedFrame.HitBoxes.Remove(SelectedHitBox);
                }

                hbSelector.Items.Clear();
                foreach (HitBox hb in selectedFrame.HitBoxes)
                {
                    hbSelector.Items.Add(hb);
                }
                if (hbSelector.Items.Count > 0)
                {
                    if (hbSelector.Items.Count <= ind)
                    {
                        ind = hbSelector.Items.Count - 1;
                    }
                    hbSelector.SelectedIndex = ind;
                }
                else
                {
                    SelectedHitBox = null;
                }
            }
        }

        private void shareSelectorSelectedIndexChanged(object sender, EventArgs e)
        {
            if (shareSelector.Enabled && selectedFrame != null && 
                shareSelector.SelectedItem != null)  
            {
                selectedFrame.ShareWith = (Frame)shareSelector.SelectedItem;
            }
        }

        private void shareAllRadioButtonCheckedChanged(object sender, EventArgs e)
        {
            if (shareAllRadioButton.Checked)
            {
                if (selectedFrame != null && frames != null && frames.Count > 0)
                {
                    foreach (Frame f in frames)
                    {
                        f.HitBoxes = selectedFrame.HitBoxes;
                    }
                    shareWithAllList = selectedFrame.HitBoxes;
                }
            }
            else
            {
                if (selectedFrame != null && frames != null && frames.Count > 0)
                {
                    foreach (Frame f in frames)
                    {
                        if (f != selectedFrame)
                        {
                            f.HitBoxes = new List<HitBox>();
                            foreach (HitBox h in selectedFrame.HitBoxes)
                            {
                                f.HitBoxes.Add(h.Clone());
                            }
                        }
                    }
                }
            }
        }

        private void delActClick(object sender, EventArgs e)
        {
            string str = "";
            if (actionSelectorHB.SelectedItem.GetType() == typeof(string))
                str = (string)actionSelectorHB.SelectedItem;
            else
                str = ((HitBoxAction)actionSelectorHB.SelectedItem).Name;

            DelText?.Invoke(str);
        }

        private void gotoActClick(object sender, EventArgs e)
        {
            string str = "";
            if (actionSelectorHB.SelectedItem.GetType() == typeof(string))
                str = (string)actionSelectorHB.SelectedItem;
            else
                str = ((HitBoxAction)actionSelectorHB.SelectedItem).Name;

            GotoText?.Invoke(str);
        }

        public string[] GetActionNames()
        {
            string[] names = new string[actionSelectorHB.Items.Count];
            int i = 0;
            foreach (var ob in actionSelectorHB.Items)
            {
                if (ob.GetType() == typeof(string)) names[i] = (string)ob;
                else names[i] = ((HitBoxAction)ob).Name;
                i++;
            }
            return names;
        }

        string addstart = "\n\n;>Action\n;### Action rep1 ###\nrep1:\n\tLDX !SpriteIndex\n"+
            ";Here you can write your action code\nRTS\n;>End Action\n";

        private void newActClick(object sender, EventArgs e)
        {
            string[] names = GetActionNames();
            if (NewHitboxInteractionActionDialog.Show(ParentForm, names) == DialogResult.OK)
            {
                AddText?.Invoke(addstart.Replace("rep1", NewHitboxInteractionActionDialog.NewName));
                if (NewHitboxInteractionActionDialog.Autoselect) 
                {
                    foreach(var ob in actionSelectorHB.Items)
                    {
                        if (ob.GetType() == typeof(string)) 
                        {
                            if ((string)ob == NewHitboxInteractionActionDialog.NewName)
                            {
                                actionSelectorHB.SelectedIndex = actionSelectorHB.Items.IndexOf(ob);
                                break;
                            }
                        }
                        else if (((HitBoxAction)ob).Name == NewHitboxInteractionActionDialog.NewName)
                        {
                            actionSelectorHB.SelectedIndex = actionSelectorHB.Items.IndexOf(ob);
                            break;
                        }
                    }
                }
                if (NewHitboxInteractionActionDialog.GotoAct)
                {
                    GotoText?.Invoke(NewHitboxInteractionActionDialog.NewName);
                }
            }
        }

        string actionDetector = @";>Action( |\t)*(\r\n|\n)(|.*(\r\n|\n))*[a-zA-Z_]+[a-zA-Z_\d.]*\:( |\t)*(\r\n|\n)(|.*(\r\n|\n))*;>End Action";
        public void GetActions(string code)
        {
            int ind = actionSelectorHB.SelectedIndex;
            MatchCollection ms = Regex.Matches(code, actionDetector);
            actionSelectorHB.Items.Clear();
            actionSelectorHB.Items.Add("DefaultAction");
            foreach (Match m in ms)
            {
                actionSelectorHB.Items.Add(new HitBoxAction(m));
            }

            string[] names = new string[actionSelectorHB.Items.Count];
            int i = 0;
            foreach (var ob in actionSelectorHB.Items)
            {
                if (ob.GetType() == typeof(string)) names[i] = (string)ob;
                else names[i] = ((HitBoxAction)ob).Name;
                i++;
            }

            if (ind >= actionSelectorHB.Items.Count) ind = actionSelectorHB.Items.Count - 1;

            actionSelectorHB.SelectedIndex = ind;

            if (frames == null || frames.Count <= 0) return;
            bool found = false;
            foreach (Frame f in frames)
            {
                foreach (HitBox hb in f.HitBoxes)
                {
                    found = false;
                    for (i = 0; i < names.Length; i++)
                    {
                        if (hb.ActionName == names[i])
                        {
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                    {
                        hb.ActionName = (string)actionSelectorHB.Items[0];
                    }
                }
            }
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
            if (shareWithRadioButton.Checked && selectedFrame != null
                && shareSelector.SelectedItem != null)
            {
                if (selectedFrame.ShareWith == null)
                    selectedFrame.ShareWith = (Frame)shareSelector.SelectedItem;

                selectedFrame.HitBoxes = ((Frame)shareSelector.SelectedItem).HitBoxes;
            }
            else if (!shareWithRadioButton.Checked && selectedFrame != null
                && shareSelector.SelectedItem != null)
            {
                selectedFrame.HitBoxes = new List<HitBox>();
                foreach (HitBox hb in ((Frame)shareSelector.SelectedItem).HitBoxes)
                {
                    selectedFrame.HitBoxes.Add(hb.Clone());
                }
            }
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

            shareSelector.Items.Clear();
            if (frameSelector.Items == null || frameSelector.Items.Count <= 0)
            {
                shareWithRadioButton.Enabled = false;
                label22.Enabled = false;
                shareSelector.Enabled = false;
                if (shareWithRadioButton.Checked) 
                    dontShareRadioButton.Checked = true;
                selectedFrame.ShareWith = null;
                return;
            }
            foreach(Frame f in frameSelector.Items)
            {
                if (f != SelectedFrame)
                {
                    shareSelector.Items.Add(f);
                }
            }
            if (shareSelector.Items == null || shareSelector.Items.Count <= 0)
            {
                shareWithRadioButton.Enabled = false;
                label22.Enabled = false;
                shareSelector.Enabled = false;
                if (shareWithRadioButton.Checked)
                    dontShareRadioButton.Checked = true;
                selectedFrame.ShareWith = null;
            }
            else
            {
                shareWithRadioButton.Enabled = true;
                label22.Enabled = true;
                if (selectedFrame.ShareWith == null)
                {
                    shareSelector.SelectedIndex = 0;
                    if (shareWithRadioButton.Checked)
                        dontShareRadioButton.Checked = true;
                }
                else
                {
                    bool found = false;
                    foreach (Frame f in shareSelector.Items)
                    {
                        if(f == selectedFrame.ShareWith)
                        {
                            shareSelector.SelectedItem = f;
                            shareWithRadioButton.Checked = true;
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                    {
                        shareSelector.SelectedIndex = 0;
                    }
                }
                shareSelector.Enabled = shareWithRadioButton.Checked;
            }

        }

        public void UpdateFrameList(Frame[] Frames)
        {
            if (Frames == null || Frames.Length <= 0) 
            {
                frames = null;
                SelectedFrame = null;
                hbSelector.Items.Clear();
                ipSelector.Items.Clear();
                shareWithRadioButton.Enabled = false;
                label22.Enabled = false;
                shareSelector.Enabled = false;
                if (shareWithRadioButton.Checked)
                    dontShareRadioButton.Checked = true;
                if (selectedFrame != null) selectedFrame.ShareWith = null;
                return;
            }

            frames = Frames.ToList();
            Frame fr = SelectedFrame;
            frameSelector.Items.Clear();

            foreach(Frame f in frames)
            {
                frameSelector.Items.Add(f);
                if (shareAllRadioButton.Checked && shareWithAllList != null) 
                {
                    f.HitBoxes = shareWithAllList;
                }
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
            {
                SelectedHitBox = (HitBox)hbSelector.SelectedItem;
                borderC.BackColor = selectedHitBox.BorderColor;
                fillC.BackColor = Color.FromArgb(255, selectedHitBox.FrontColor);
            }
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
