using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMWControlibBackend.Logic
{
    public class Group
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Color Color { get; set; }

        private Group()
        {

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

            for (int i = 1; i < groupsSTR.Length; i++)
            {
                group = groupsSTR[i].Split(';');

                if (group.Length <= 1)
                    throw new Exception("Invalid Group at line: " + i);

                groups[i - 1] = new Group
                {
                    Name = group[0],
                    Description = group[1],
                    Color = SystemColors.Control
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
