using System.Collections.Generic;
using System.Linq;

namespace SMWControlibBackend.Logic.HDMA
{
    public class DMATransferMode
    {
        public int DMATransferModeID { get; private set; }
        public int AffectedRegisters { get; private set; }
        public string Description { get; private set; }
        public static readonly DMATransferMode Mode0 = 
            new DMATransferMode(0, 1, "1 register write once (1 byte: p)");
        public static readonly DMATransferMode Mode1 =
            new DMATransferMode(1, 2, "2 registers write once (2 bytes: p, p+1)");
        public static readonly DMATransferMode Mode2 =
            new DMATransferMode(2, 1, "1 register write twice (2 bytes: p, p)");
        public static readonly DMATransferMode Mode3 =
            new DMATransferMode(3, 2, "2 registers write twice each (4 bytes: p, p, p+1, p+1)");
        public static readonly DMATransferMode Mode4 =
            new DMATransferMode(4, 4, "4 registers write once (4 bytes: p, p+1, p+2, p+3)");
        public static readonly DMATransferMode Mode5 =
            new DMATransferMode(5, 2, "2 registers write twice alternate (4 bytes: p, p+1, p, p+1)");
        public static readonly DMATransferMode Mode6 =
            new DMATransferMode(6, 1, "1 register write twice (2 bytes: p, p)");
        public static readonly DMATransferMode Mode7 =
            new DMATransferMode(7, 2, "2 registers write twice each (4 bytes: p, p, p+1, p+1");

        private DMATransferMode(int ID, int regs, string description)
        {
            DMATransferModeID = ID;
            AffectedRegisters = regs;
            Description = description;
        }

        public static implicit operator DMATransferMode(int i)
        {
            switch (i)
            {
                case 0:
                    return Mode0;
                case 1:
                    return Mode1;
                case 2:
                    return Mode2;
                case 3:
                    return Mode3;
                case 4:
                    return Mode4;
                case 5:
                    return Mode5;
                case 6:
                    return Mode6;
                default:
                    return Mode7;
            }
        }

        public static implicit operator int(DMATransferMode c)
        {
            return c.DMATransferModeID;
        }
    }
    public class HDMAChannel
    {
        public int ChannelID { get; private set; }
        public bool UsedByOriginalGame { get; private set; }
        public static readonly HDMAChannel Channel0 = new HDMAChannel(0, true);
        public static readonly HDMAChannel Channel1 = new HDMAChannel(1, true);
        public static readonly HDMAChannel Channel2 = new HDMAChannel(2, true);
        public static readonly HDMAChannel Channel3 = new HDMAChannel(3, false);
        public static readonly HDMAChannel Channel4 = new HDMAChannel(4, false);
        public static readonly HDMAChannel Channel5 = new HDMAChannel(5, false);
        public static readonly HDMAChannel Channel6 = new HDMAChannel(6, false);
        public static readonly HDMAChannel Channel7 = new HDMAChannel(7, true);

        private HDMAChannel(int ID, bool used)
        {
            ChannelID = ID;
            UsedByOriginalGame = used;
        }

        public static void SetUsedByTheOriginalGame(int ID, bool value)
        {
            HDMAChannel c = ID;
            c.UsedByOriginalGame = value;
        }

        public static implicit operator HDMAChannel(int i)
        {
            switch(i)
            {
                case 0:
                    return Channel0;
                case 1:
                    return Channel1;
                case 2:
                    return Channel2;
                case 3:
                    return Channel3;
                case 4:
                    return Channel4;
                case 5:
                    return Channel5;
                case 6:
                    return Channel6;
                default:
                    return Channel7;
            }
        }

        public static implicit operator int(HDMAChannel c)
        {
            return c.ChannelID;
        }
    }
    public class HDMA
    {
        private List<HDMALine> lines { get; set; }
        public List<HDMALine> Lines
        {
            get
            {
                return lines.ToArray().ToList();
            }
        }
        public Effect Effect { get; private set; }
        public int Height = 0;

        public HDMA(Effect effect)
        {
            Effect = effect;
            lines = new List<HDMALine>();
        }

        public void RemoveLine(HDMALine line)
        {
            lines.Remove(line);
        }

        public HDMALine AddLine()
        {
            int max = 0;
            int cur;
            for (uint i = 0; i < Effect.Type.ChannelsLength; i++)
            {
                cur = Effect.Type.GetTransferMode(i).AffectedRegisters;
                if (cur > max) max = cur;
            }
            HDMALine l = new HDMALine(max, Effect.Type.ChannelsLength)
            {
                Height = 1
            };

            lines.Add(l);

            return l;
        }

        public HDMALine this[int key]
        {
            get
            {
                int counter = 0;
                foreach(HDMALine hl in lines)
                {
                    if (key <= hl.Height + counter) return hl;
                    counter += hl.Height;
                }
                return null;
            }
        }
    }
}
