namespace SMWControlibBackend.Logic
{
    public class NormalLabel: Label
    {
        public NormalLabel(Group[] groups, string group) : base(groups, group)
        {
            reGex = @"^[a-zA-Z_]+[a-zA-Z_\d.]*\:$";
        }

        public override bool IsValid(int Line, string[] Lines, NormalLabel Label)
        {
            if (Lines == null || Lines.Length <= 0) return false;
            if (Line < 0 || Line >= Lines.Length) return false;



            return SyntaxIsCorrect(Lines[Line]);
        }
    }
}
