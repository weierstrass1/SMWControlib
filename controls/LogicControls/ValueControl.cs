using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SMWControlibBackend.Logic.HDMA;

namespace SMWControlibControls.LogicControls
{
    public partial class ValueControl : UserControl
    {
        public int[] AffectedBits;
        public int ValueID;
        public string Name;
        public string Description;
        public SMWControlibBackend.Logic.HDMA.ValueType Type { get; protected set; }
        public ValueControl()
        {
            InitializeComponent();
        }
    }
}
