using System;
using System.IO;

namespace SMWControlibBackend.Logic
{
    public class Group
    {
        public static readonly Group Default = new Group
        {
            Name = "Default",
            Description = "Used for normal text.",
            Style = 0
        };
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int Style;

        public Group()
        {

        }

        public static Group FindGroup(Group[] groups, string s)
        {
            string se = s.Trim(' ').Trim('\t');

            for (int i = 0; i < groups.Length; i++)
            {
                if(groups[i].Name.Trim(' ').Trim('\t') == se)
                {
                    return groups[i];
                }
            }
            return null;
        }

        public static Group[] GetGroups(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException("File not found.");

            string[] groupsSTR = File.ReadAllLines(path);

            if (groupsSTR.Length <= 1)
                throw new Exception("File doesn't have any group.");

            Group[] groups = new Group[groupsSTR.Length - 1];
            string[] group;

            for (int i = 1; i < groupsSTR.Length && i < 256; i++)
            {
                group = groupsSTR[i].Split(';');

                if (group.Length <= 1)
                    throw new Exception("Invalid Group at line: " + i);

                groups[i - 1] = new Group
                {
                    Name = group[0],
                    Description = group[1],
                    Style = i
                };
            }
            return groups;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
