using System.Windows.Forms;

namespace SMWControlibControls.LogicControls
{
    public partial class ValueControl : UserControl
    {
        public int[] AffectedBits;

        public int MinBit
        {
            get
            {
                return AffectedBits[0];
            }
        }

        public int MaxBit
        {
            get
            {
                return AffectedBits[AffectedBits.Length - 1];
            }
        }

        public int ValueID;
        public int RegID;
        public new string Name
        {
            set
            {
                name.Text = value;
            }
        }
        public string Description
        {
            set
            {
                typeDesc.Text = value;
            }
        }
        public SMWControlibBackend.Logic.HDMA.ValueType Type { get; protected set; }
        public ValueControl()
        {
            InitializeComponent();
        }

        public virtual int GetValue(int valueId, int regId)
        {
            if (ValueID != valueId) return 0;
            if (RegID != regId) return 0;
            return 1;
        }
    }
}
