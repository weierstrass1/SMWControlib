using System;

namespace SMWControlibBackend.Graphics
{
    public class TileMaskContainer
    {
        public string XDisp;
        public string YDisp;
        public int SP;
        public int PalId;
        public int Size;
        public int Priority;
        public bool FlipX;
        public bool FlipY;
        public string Tile;

        public TileMask ToTileMask(Tile[,] t16SP12, Tile[,] t16SP34, Tile[,] t8SP12, Tile[,] t8SP34)
        {
            Tile[,] t;

            if(SP == 0)
            {
                if (Size == 16) t = t16SP12;
                else t = t8SP12;
            }
            else
            {
                if (Size == 16) t = t16SP34;
                else t = t8SP34;
            }

            int i = 0;

            if(Tile[1] >= 'A')
            {
                i = 10 + Tile[1] - 'A';
            }
            else
            {
                i = Tile[1] - '0';
            }

            int j = 0;

            if (Tile[2] >= 'A')
            {
                j = 10 + Tile[2] - 'A';
            }
            else
            {
                j = Tile[2] - '0';
            }

            TileSP sp = TileSP.SP01;
            if (SP > 0) sp = TileSP.SP23;

            TileMask tm = new TileMask(sp, t[j, i], 2, FlipX, FlipY);

            sbyte b = Convert.ToSByte(XDisp.Substring(1), 16);
            tm.XDisp = b + 128;
            tm.XDisp *= 2;
            b = Convert.ToSByte(YDisp.Substring(1), 16);
            tm.YDisp = b + 112;
            tm.YDisp *= 2;
            tm.Palette = (PaletteId)PalId;
            tm.Priority = (TilePriority)Priority;
            return tm;
        }

        public void ToTileMaskContainer(TileMask tm)
        {
            XDisp = tm.XDispString;
            YDisp = tm.YDispString;
            SP = (int)tm.SP;
            PalId = (int)tm.Palette;
            Size = tm.Size;
            Priority = (int)tm.Priority;
            FlipX = tm.FlipX;
            FlipY = tm.FlipY;
            Tile = tm.Tile;
        }
    }
}
