namespace SMWControlibBackend.Logic.HDMA
{
    public class HDMAValue
    { 
        public int ValueID { get; private set; }
        public int RegisterID { get; private set; }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public ValueType Type { get; private set; }

        public string AffectedBits { get; private set; }

        public int MinValue { get; private set; }
        public int MaxValue { get; private set; }

        public HDMAValue(int ID, int Reg, string name, string description,
            ValueType type, string affectedBits, int minVal, int maxVal)
        {
            ValueID = ID;
            RegisterID = Reg;
            Name = name;
            Description = description;
            Type = type;
            AffectedBits = affectedBits;
            MinValue = minVal;
            MaxValue = maxVal;
        }
    }
}
