using System;
using System.Drawing;
using System.IO;

namespace backend
{
    public class ColorPalette
    {
        private static ColorPalette[] palettes;
        private Color[] colors;
        public uint Length { get; private set; }


        private ColorPalette()
        {
        }

        public Color GetColor(uint i)
        {
            if (i >= Length)
            {
                i = Length - 1;
            }
            return colors[i];
        }

        public void SetColor(uint i, Color c)
        {
            if (i >= Length)
            {
                i = Length - 1;
            }
            colors[i] = c;
        }

        public static ColorPalette GetPalette(uint i)
        {
            if (palettes == null) return null;
            if (i >= (uint)palettes.Length)
            {
                i = (uint)palettes.Length - 1;
            }
            return palettes[i];
        }

        public static void GeneratePalette(string path, uint PaletteSize)
        {
            byte[] bytes = File.ReadAllBytes(path);
            GeneratePalette(bytes, PaletteSize);
        }

        public static void GeneratePalette(byte[] bytes, uint PaletteSize)
        {
            if (bytes.Length % PaletteSize != 0) 
            {
                throw new Exception("The Palette Size is not valid.");
            }
            palettes = new ColorPalette[(bytes.Length) / (PaletteSize * 3)];
            int baseIndex = 0;
            int index = 0;
            for (int i = 0; i < palettes.Length; i++)
            {
                palettes[i] = new ColorPalette();
                palettes[i].Length = PaletteSize;
                palettes[i].colors = new Color[PaletteSize];
                baseIndex = (int)(i * 3 * PaletteSize);
                for (int j = 0; j < PaletteSize; j++)
                {
                    index = baseIndex + j * 3;
                    palettes[i].colors[j] = Color.FromArgb(255 ,bytes[index], bytes[index + 1], bytes[index + 2]);
                }
            }
        }
    }
}
