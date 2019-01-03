using System;

namespace SMWControlibBackend.Logic
{
    public abstract class CodeComponent
    {
        public string Name;
        public string Description;
        public string Tag { get; protected set; }
        public string EndTag { get; protected set; }
        public string DescriptionTag { get; protected set; }
        public string DescriptionEndTag { get; protected set; }
        public string Code;


        public abstract Tuple<bool, string> IsCorrect();

        public override string ToString()
        {
            return Name;
        }
    }
}
