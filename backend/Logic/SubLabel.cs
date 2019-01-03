namespace SMWControlibBackend.Logic
{
    public class SubLabel : Label
    {
        public SubLabel(Group[] groups, string group) : base(groups, group)
        {
            reGex = @"^\.[a-zA-Z_\d]+[a-zA-Z_\d.]*:?$";
        }

        public override bool IsValid(int Line, string[] Lines, NormalLabel Label)
        {
            if (Label == null) return false;
            if (Lines == null || Lines.Length <= 0) return false;
            if (Line < 0 || Line >= Lines.Length) return false;

            if (!SyntaxIsCorrect(Lines[Line])) return false;

            for (int i = 0; i < Line; i++)
            {
                if (Label.SyntaxIsCorrect(Lines[i]))
                    return true;
            }
            return false;
        }
    }
}
