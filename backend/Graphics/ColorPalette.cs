using System;
using System.Drawing;
using System.IO;

namespace SMWControlibBackend.Graphics
{
    public enum PaletteId
    {
        p0 = 0, p1 = 1, p2 = 2, p3 = 3, p4 = 4, p5 = 5, p6 = 6, p7 = 7, p8 = 8,
        p9 = 9, pA = 10, pB = 11, pC = 12, pD = 13, pE = 14, pF = 15
    }
    public class ColorPalette
    {
        private static ColorPalette[] globalPalettes;
        private static ColorPalette[] GlobalPalettes
        {
            get
            {
                if (globalPalettes == null)
                    GenerateGlobalPalettes("./Resources/DefaultPalette.pal", 16);
                return globalPalettes;
            }
            set
            {
                globalPalettes = value;
            }
        }
        public static int GlobalPalettesLength
        {
            get
            {
                return GlobalPalettes.Length;
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
                SelectedGlobalPaletteChange?.Invoke();
            }
        }
        public static byte GlobalPaletteSize
        {
            get
            {
                return GlobalPalettes[0].Length;
            }
        }
        public static event Action SelectedGlobalPaletteChange, GlobalPalletesChange;
        public static event Action<PaletteId> OneGlobalPaletteChange;
        public byte Length { get; private set; }


        private ColorPalette()
        {
        }

        public Color GetColor(byte i)
        {
            if (Length == 0) return default(Color);
            if (i >= Length) i = (byte)(Length - 1);
            Color c = colors[i];
            return Color.FromArgb(c.A, c.R, c.G, c.B);
        }

        public void SetColor(byte i, Color c)
        {
            if (Length == 0) return;

            if (i >= Length) i = (byte)(Length - 1);
            colors[i] = c;
        }

        public static Color GetGlobalColor(byte i)
        {
            if (GlobalPalettes == null) return default(Color);

            return GlobalPalettes[(int)selectedPalette].GetColor(i); ;
        }

        public static Color GetGlobalColor(byte i, PaletteId pid)
        {
            if (GlobalPalettes == null) return default(Color);

            return GlobalPalettes[(int)pid].GetColor(i); ;
        }

        public static void SetGlobalColor(byte i, Color c)
        {
            if (GlobalPalettes == null) return;

            GlobalPalettes[(int)SelectedPalette].SetColor(i, c);
            OneGlobalPaletteChange?.Invoke(selectedPalette);
        }

        public static void SetGlobalColor(byte i, Color c, PaletteId pid)
        {
            if (GlobalPalettes == null) return;

            GlobalPalettes[(int)pid].SetColor(i, c);
            OneGlobalPaletteChange?.Invoke(pid);
        }

        public static void GenerateGlobalPalettes(string path, byte PaletteSize)
        {
            byte[] bytes = File.ReadAllBytes(path);
            GenerateGlobalPalettes(bytes, PaletteSize);
        }

        public static void GenerateGlobalPalettes(byte[] bytes, byte PaletteSize)
        {
            if (bytes.Length != 768) 
            {
                throw new Exception("The Palette is not valid.");
            }
            GlobalPalettes = new ColorPalette[(bytes.Length) / (PaletteSize * 3)];
            int baseIndex = 0;
            int index = 0;
            for (int i = 0; i < GlobalPalettes.Length; i++)
            {
                GlobalPalettes[i] = new ColorPalette();
                GlobalPalettes[i].Length = PaletteSize;
                GlobalPalettes[i].colors = new Color[PaletteSize];
                baseIndex = (i * 3 * PaletteSize);
                for (int j = 0; j < PaletteSize; j++)
                {
                    index = baseIndex + j * 3;
                    GlobalPalettes[i].colors[j] = Color.FromArgb(255 ,bytes[index], bytes[index + 1], bytes[index + 2]);
                }
            }
            GlobalPalletesChange?.Invoke();
        }
    }
}
