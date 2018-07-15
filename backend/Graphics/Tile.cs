using System;
using System.Drawing;

namespace backend
{
    public enum TileSize { Size8x8 = 8, Size16x16 = 16 };
    public enum BaseTile { Top = 0, Botton = 8 , None = -1};

    public static class Zoom
    {
        public static readonly zoom x1 = 1;
        public static readonly zoom x2 = 2;
        public static readonly zoom x4 = 4;
        public static readonly zoom x6 = 6;
        public static readonly zoom x8 = 8;
        public static readonly zoom x10 = 10;
        public static readonly zoom x12 = 12;
        public static readonly zoom x14 = 14;
        public static readonly zoom x16 = 16;
        public static readonly zoom x18 = 18;
        public static readonly zoom x20 = 20;
    }

    public struct zoom
    {
        int value;

        private zoom(int Value)
        {
            value = 1;
        }

        public static implicit operator int(zoom z)
        {
            return z.value;
        }

        public static implicit operator zoom(int i)
        {
            zoom z = new zoom(i);
            if (z.value < 1) z.value = 1;
            if (z.value > 20) z.value = 20;
            return z;
        }
    }

    public class Tile
    {
        public string Code { get; private set; }
        public int Size { get; private set; }
        byte[,] colors;
        Bitmap[,] images;

        private Tile()
        {
        }

        public static Tile fusionTiles(Tile target, Tile fusion, BaseTile baseTile)
        {
            if (target == null)
            {
                target = fusion;
                return target;
            }
            if (target.Size != fusion.Size)
                throw new Exception("Tiles aren't of the same size.");

            switch(baseTile)
            {
                case BaseTile.None:
                    target.colors = fusion.colors;
                    break;
                default:
                    int numbbaseTile = (int)baseTile;
                    if (target.Size == (int)TileSize.Size8x8) numbbaseTile /= 2;

                    int ylim = target.Size/2;
                    if (baseTile == BaseTile.Botton) ylim = target.Size;

                    for (int i = 0; i < target.colors.GetLength(0); i++)
                    {
                        for (int j = numbbaseTile; j < ylim; i++)
                        {
                            target.colors[i, j] = fusion.colors[i, j];
                        }
                    }
                    break;
            }
            return target;
        }

        public Bitmap GetImage(uint i, zoom zoom)
        {
            if (images == null) GenerateBitmap(i, zoom);
            if (i > (uint)images.GetLength(0)) i = (uint)images.GetLength(0) - 1;

            int numbZoom = (int)zoom;

            return images[i, numbZoom / 2];
        }

        public void GenerateBitmap(uint paletteId, zoom zoom)
        {
            ColorPalette cp = ColorPalette.GetPalette(paletteId);

            if (cp == null) return;

            if (paletteId >= cp.Length)
            {
                paletteId = cp.Length - 1;
            }

            if (images == null || images.GetLength(0) != cp.Length) images = new Bitmap[cp.Length, 7];

            int numbZoom = zoom;

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
        public static Tile[,] GenerateTilesFromColorMatrix(byte[,] colors, TileSize size, BaseTile baseTile,out BaseTile baseTileout)
        {
            baseTileout = baseTile;
            if (colors.GetLength(0) != 128 || 
                (colors.GetLength(1) != 128 && colors.GetLength(1) != 64))
                throw new Exception("Not Valid Matrix.");

            int h = 8;
            int w = 16;
            if (colors.GetLength(1) == 128)
            {
                baseTile = BaseTile.None;
                baseTileout = baseTile;
            }
            if (baseTile == BaseTile.None) h = 16;

            Tile[,] tiles = new Tile[w, h];
            int numSize = (int)size;
            int numBase = (int)baseTile;
            if (numBase < 0) numBase = 0;
            int x, y;
            int ylim = numSize;
            if (size == TileSize.Size16x16) ylim /= 2;

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
                    for (int p = 0; p < numSize && x + p < colors.GetLength(0); p++)
                    {
                        for (int q = 0; y + q < colors.GetLength(1) && ((j < h - 1 && q < numSize) || (j == h - 1 && q < ylim)); q++)
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
