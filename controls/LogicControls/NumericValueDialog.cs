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

        public override int GetValue(int valueId, int regId)
        {
            if (ValueID != valueId) return 0;
            if (RegID != regId) return 0;

            int v = (int)value.Value;

            int bas = 7;

            if (MaxBit >= 8) bas = 15;

            int disp = bas - MaxBit;

            return v << disp;
        }
    }
}
