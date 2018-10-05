using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMWControlibControls.LogicControls
{
    public partial class NumericValueDialog : ValueControl
    {
        public int MaxValue
        {
            get
            {
                return (int)value.Maximum;
            }
            set
            {
                this.value.Maximum = value;
            }
        }
        public int MinValue
        {
            get
            {
                return (int)value.Minimum;
            }
            set
            {
                this.value.Minimum = value;
            }
        }
        public NumericValueDialog()
        {
            InitializeComponent();
            Type = SMWControlibBackend.Logic.HDMA.ValueType.Numeric;
        }
    }
}
