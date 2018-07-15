using System;
using System.Drawing;
using System.IO;

namespace backend
{
    public enum PaletteId
    {
        p0 = 0, p1 = 1, p2 = 2, p3 = 3, p4 = 4, p5 = 5, p6 = 6, p7 = 7, p8 = 8,
        p9 = 9, pA = 10, pB = 11, pC = 12, pD = 13, pE = 14, pF = 15
    }
    public class ColorPalette
    {
        private static ColorPalette[] palettes;
        public static int PalettesLength
        {
            get
            {
                if (palettes == null) return 0;
                return palettes.Length;
            }
        }
        private Color[] colors;
        private static PaletteId selectedPalette;
        public static PaletteId SelectedPalette
        {
            get
            {
                return selectedPalette;
            }
            set
            {
                selectedPalette = value;
                SelectedPaletteChange?.Invoke();
            }
        }
        public static event Action SelectedPaletteChange, AllPalettesChange;
        public static event Action<int> PalettesChange;
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

        public static ColorPalette GetPalette(PaletteId i)
        {
            if (palettes == null) return null;
            SelectedPalette = i;
            return palettes[(int)SelectedPalette];
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
            AllPalettesChange?.Invoke();
            for (int i = 0; i < palettes.Length; i++)
            {
                PalettesChange?.Invoke(i);
            }
        }
    }
}
