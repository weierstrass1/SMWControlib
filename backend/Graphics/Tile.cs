﻿using System;
using System.Drawing;

namespace SMWControlibBackend.Graphics
{
    public enum TileSize { Size8x8 = 8, Size16x16 = 16 };
    public enum BaseTile { Top = 0, Botton = 8 , None = -1};

    public struct Zoom
    {
        public static readonly Zoom X1 = 1;
        public static readonly Zoom X2 = 2;
        public static readonly Zoom X4 = 4;
        public static readonly Zoom X6 = 6;
        public static readonly Zoom X8 = 8;
        public static readonly Zoom X10 = 10;
        public static readonly Zoom X12 = 12;
        public static readonly Zoom X14 = 14;
        public static readonly Zoom X16 = 16;
        public static readonly Zoom X18 = 18;
        public static readonly Zoom X20 = 20;
        private int value;

        private Zoom(int Value)
        {
            value = Value;
            if (value < 1) value = 1;
            if (value > 20) value = 20;
        }

        public static implicit operator int(Zoom z)
        {
            return z.value;
        }

        public static implicit operator Zoom(int i)
        {
            return new Zoom(i);
        }
    }

    public class Tile
    {
        public string Code { get; private set; }
        public int Size { get; private set; }
        public bool FullyDirty { get; private set; }
        private bool[] _Dirty;
        private byte[,] colors;
        private Bitmap[,] images;
        public event Action<Tile> IsFullyDirty;
        public event Action<Tile, int> IsDirty;


        private Tile(bool UseGlobalPalette)
        {
            setDirty();
            if(UseGlobalPalette)
            {
                ColorPalette.GlobalPalletesChange += globalPalletesChange;
                ColorPalette.OneGlobalPaletteChange += oneGlobalPaletteChange;
            }
        }

        private void globalPalletesChange()
        {
            setDirty();
        }

        private void oneGlobalPaletteChange(PaletteId obj)
        {
            setDirty((int)obj, true);
        }

        private void setDirty()
        {
            FullyDirty = true;
            _Dirty = new bool[ColorPalette.GlobalPalettesLength];
            for (int i = 0; i < _Dirty.Length; i++)
            {
                _Dirty[i] = true;
            }
            IsFullyDirty?.Invoke(this);
        }

        private void setDirty(int i,bool val)
        {
            if (_Dirty == null) return;
            if (i < 0) i = 0;
            else if (i >= _Dirty.Length) i = _Dirty.Length - 1;

            if (_Dirty[i] != val)
            {
                _Dirty[i] = val;
                if (_Dirty[i]) IsDirty?.Invoke(this, i);
            }
        }

        public static Tile FusionTiles(Tile target, Tile fusion, BaseTile baseTile)
        {
            if (target == null)
            {
                target = fusion;
                return target;
            }
            if(fusion == null)
            {
                return target;
            }
            if (target.Size != fusion.Size)
                throw new Exception("Tiles aren't of the same size.");

            switch(baseTile)
            {
                case BaseTile.None:
                    if (fusion.colors != null) target.colors = fusion.colors;
                    break;
                default:
                    int numbbaseTile = (int)baseTile;
                    if (target.Size == (int)TileSize.Size8x8) numbbaseTile /= 2;

                    int ylim = target.Size/2;
                    int adder = 0;
                    if (baseTile == BaseTile.Botton) adder = ylim;

                    for (int i = 0; i < target.colors.GetLength(0); i++)
                    {
                        for (int j = 0; j < ylim; j++)
                        {
                            target.colors[i, j + adder] = fusion.colors[i, j];
                        }
                    }
                    break;
            }
            target.setDirty();
            return target;
        }

        public Bitmap GetImage(PaletteId i, Zoom zoom)
        {
            if (images == null || images[(int)i, zoom - 1] == null || FullyDirty || _Dirty[(int)i])
                GenerateBitmap(i, zoom);

            return images[(int)i, zoom - 1];
        }

        public void GenerateBitmap(PaletteId paletteId, Zoom zoom)
        {
            if (ColorPalette.GlobalPaletteSize == 0) return;
            int npid = (int)paletteId;

            if (images == null || images.GetLength(0) != ColorPalette.GlobalPaletteSize)
                images = new Bitmap[ColorPalette.GlobalPaletteSize, 20];

            images[npid, zoom - 1] = new Bitmap(Size * zoom, Size * zoom);

            Bitmap bp = images[npid, zoom - 1];
            Brush br;
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (colors[i, j] != 0)
                    {
                        using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bp))
                        {
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                            br = new SolidBrush(ColorPalette.GetGlobalColor(colors[i, j], paletteId));
                            g.FillRectangle(br, new RectangleF(i * zoom, j * zoom, zoom, zoom));
                        }
                    }
                }
            }
            FullyDirty = false;
            setDirty(npid, false);
        }

        private static char[] intToHex = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };

        public static void FillTileMatrix(Tile[,] tiles, TileSize size, BaseTile baseTile)
        {
            if (tiles == null) return;
            int maxs = 16;
            if (size == TileSize.Size16x16) maxs = 15;
            int numBase = (int)baseTile;
            int maxh = maxs - numBase;

            if (tiles.GetLength(0) > maxs)
                throw new Exception("The Width of Tile Matrix must be of " + maxs + " or less.");
            if (tiles.GetLength(1) > maxh)
                throw new Exception("The Height of Tile Matrix must be of " + maxh + " or less.");

            int numbSize = (int)size;

            for (int i = 0; i < tiles.GetLength(0); i++)
            {
                for (int j = 0; j < tiles.GetLength(1); j++)
                {
                    if (tiles[i, j] == null)
                    {
                        tiles[i, j] = new Tile(true)
                        {
                            colors = new byte[numbSize, numbSize],
                            Code = "$" + intToHex[numBase + j] + intToHex[i],
                            Size = numbSize
                        };
                    }
                }
            }
        }
        public static Tile[,] GenerateTilesFromColorMatrix
            (byte[,] colors, TileSize size, BaseTile baseTile, out BaseTile baseTileout)
        {
            return GenerateTilesFromColorMatrix
                (colors, size, baseTile,out baseTileout, null, null);
        }
        public static Tile[,] GenerateTilesFromColorMatrix
            (byte[,] colors, TileSize size, BaseTile baseTile, out BaseTile baseTileout,
            Action<Tile> IsFullyDirty, Action<Tile, int> IsDirty)
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
            
            if(size == TileSize.Size16x16)
            {
                w--;
                if (h == 16) h--;
            }

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
                    tiles[i, j] = new Tile(true)
                    {
                        colors = new byte[numSize, numSize],
                        Code = "$" + intToHex[numBase + j] + intToHex[i],
                        Size = numSize
                    };
                    for (int p = 0; p < numSize && x + p < colors.GetLength(0); p++)
                    {
                        for (int q = 0; y + q < colors.GetLength(1) && (j < h && q < numSize); q++) 
                        {
                            tiles[i, j].colors[p, q] = colors[x + p, y + q];
                        }
                    }
                    if (IsFullyDirty != null)
                        tiles[i, j].IsFullyDirty += IsFullyDirty;
                    if (IsDirty != null)
                        tiles[i, j].IsDirty += IsDirty;
                }
            }

            return tiles;
        }
    }
}
