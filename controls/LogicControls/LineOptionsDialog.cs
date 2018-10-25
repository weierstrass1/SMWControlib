using SMWControlibBackend.Logic.HDMA;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SMWControlibControls.LogicControls
{
    public partial class LineOptionsDialog : Form
    {
        private List<ValueControl> values;

        HDMA hdmas;
        private LineOptionsDialog()
        {
            InitializeComponent();
            values = new List<ValueControl>();
            accept.Click += click;
        }

        private void click(object sender, EventArgs e)
        {
            HDMALine line = hdmas.AddLine();

            IEnumerator<ValueControl> en = values.GetEnumerator();

            en.Reset();

            en.MoveNext();

            line.Height = (byte)en.Current.GetValue(-1, -1);

            while (en.MoveNext())
            {
                line.Values[en.Current.RegID, en.Current.ValueID] =
                    (byte)(line.Values[en.Current.RegID, en.Current.ValueID] | 
                    en.Current.GetValue(en.Current.ValueID, en.Current.RegID));
            }

            DialogResult = DialogResult.OK;
            Dispose();
        }

        public static DialogResult Show(IWin32Window owner, HDMA hdma)
        {
            LineOptionsDialog lod = new LineOptionsDialog();

            lod.hdmas = hdma;
            lod.build(hdma);

            return lod.ShowDialog(owner);
        }

        private void build(HDMA hdma)
        {
            ValueControl v = new NumericValueDialog()
            {
                MaxValue = 224,
                MinValue = 0,
                Parent = panel1,
                Dock = DockStyle.Top,
                Name = "Height",
                Description = "Number of pixels used by the line.",
                ValueID = -1,
                RegID = -1,
                AffectedBits = new int[8]
            };
            v.AffectedBits[0] = 0;
            v.AffectedBits[1] = 1;
            v.AffectedBits[2] = 2;
            v.AffectedBits[3] = 3;
            v.AffectedBits[4] = 4;
            v.AffectedBits[5] = 5;
            v.AffectedBits[6] = 6;
            v.AffectedBits[7] = 7;
            v.BringToFront();
            values.Add(v);

            EffectType et = hdma.Effect.Type;
            
            foreach(int k in et.Values.Keys)
            {
                foreach(HDMAValue[] t 
                    in et.Values.Values)
                {
                    for (int i = 0; i < t.Length; i++)
                    {
                        switch(t[i].Type)
                        {
                            case SMWControlibBackend.Logic.HDMA.ValueType.Boolean:
                                v = new BooleanValueControl();
                                break;
                            case SMWControlibBackend.Logic.HDMA.ValueType.Bright:
                                v = new BrightValueControl
                                {
                                    MinValue = t[i].MinValue,
                                    MaxValue = t[i].MaxValue
                                };
                                break;
                            default:
                                v = new ValueControl();
                                break;
                        }
                        v.ValueID = k;
                        v.RegID = t[i].RegisterID;
                        v.Name = t[i].Name;
                        v.Description = t[i].Description;
                        v.Parent = panel1;
                        v.Dock = DockStyle.Top;
                        v.AffectedBits = new int[t[i].AffectedBits.Length];
                        for (int j = 0; j < v.AffectedBits.Length; j++)
                        {
                            v.AffectedBits[j] = 
                                (int)Char.GetNumericValue(t[i].AffectedBits[j]);
                        }
                        v.BringToFront();
                        values.Add(v);
                    }
                }
            }
        }
    }
}
