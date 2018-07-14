using System;
using System.Drawing;

namespace backend
{
    public enum TileSize { Size8x8 = 8, Size16x16 = 16 };
    public enum BaseTile { Top = 0, Botton = 8 , None = 0};
    public enum Zoom { x1 = 1, x2 = 2, x4 = 4, x6 = 6, x8 = 8, x10 = 10, x12 = 12 };
    public class Tile
    {

        public string Code { get; private set; }
        public int Size { get; private set; }
        byte[,] colors;
        Bitmap[,] images;

        private Tile()
        {
        }

        public Bitmap GetImage(uint i, Zoom zoom)
        {
            if (images == null) return null;
            if (i > (uint)images.GetLength(0)) i = (uint)images.GetLength(0) - 1;

            int numbZoom = (int)zoom;

            return images[i, numbZoom / 2];
        }

        public void GenerateBitmap(uint paletteId, Zoom zoom)
        {
            ColorPalette cp = ColorPalette.GetPalette(paletteId);

            if (cp == null) return;

            if (paletteId >= cp.Length)
            {
                paletteId = cp.Length - 1;
            }

            if (images == null || images.GetLength(0) != cp.Length) images = new Bitmap[cp.Length, 7];

            int numbZoom = (int)zoom;

            images[paletteId, numbZoom / 2] = new Bitmap(Size * numbZoom, Size * numbZoom);

            Bitmap bp = images[paletteId, numbZoom / 2];

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    using (Graphics g = Graphics.FromImage(bp))
                    {
                        g.FillRectangle(new SolidBrush(cp.GetColor(colors[i, j])),
                        new RectangleF(i * numbZoom, j * numbZoom, numbZoom, numbZoom));
                    }
                }
            }
        }

        static char[] intToHex = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
        public static Tile[,] GenerateTilesFromColorMatrix(byte[,] colors, TileSize size, BaseTile baseTile)
        {
            if (colors.GetLength(0) != 128 || (colors.GetLength(1) != 128 && colors.GetLength(1) != 64))
                throw new Exception("Not Valid Matrix.");

            int h = 8;
            int w = 16;
            if (colors.GetLength(1) == 128)
            {
                baseTile = BaseTile.None;
            }
            if (baseTile == BaseTile.None) h = 16;
            if (size == TileSize.Size16x16)
            {
                w = 15;
                h--;
            }
            Tile[,] tiles = new Tile[w, h];
            int numSize = (int)size;
            int numBase = (int)baseTile;
            int x, y;

            for (int i = 0; i < w; i++)
            {
                x = i * 8;
                for (int j = 0; j < h; j++)
                {
                    y = j * 8;
                    tiles[i, j] = new Tile();
                    tiles[i, j].colors = new byte[numSize, numSize];
                    tiles[i, j].Code = "$" + intToHex[numBase + j]+ intToHex[i];
                    tiles[i, j].Size = numSize;
                    for (int p = 0; p < numSize; p++)
                    {
                        for (int q = 0; q < numSize; q++)
                        {
                            tiles[i, j].colors[p, q] = colors[x + p, y + q];
                        }
                    }
                }
            }

            return tiles;
        }
    }
}
