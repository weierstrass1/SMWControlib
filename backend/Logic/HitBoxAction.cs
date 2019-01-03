using System.Text.RegularExpressions;

namespace SMWControlibBackend.Logic
{
    public class HitBoxAction
    {
        public string Name;
        public Match Pointer;

        string pat = @"\n[a-zA-Z_]+[a-zA-Z_\d.]*\:";
        public HitBoxAction(Match m)
        {
            Pointer = m;
            Match m2 = Regex.Match(m.Value, pat);
            if (m2.Success) 
            {
                Name = m2.Value.Remove(m2.Value.Length - 1, 1);
                Name = Name.Replace("\n", "");
            }
            else
            {
                Name = "";
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
