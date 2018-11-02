using System;
using System.Drawing;
using System.IO;

namespace SMWControlibBackend.Graphics
{
    public enum PaletteId
    {
        P0 = 0, P1 = 1, P2 = 2, P3 = 3, P4 = 4, P5 = 5, P6 = 6, P7 = 7, P8 = 8,
        P9 = 9, PA = 10, PB = 11, PC = 12, PD = 13, PE = 14, PF = 15
    }
    public class ColorPalette
    {
        internal static ColorPalette[] _globalPalettes;
        internal static ColorPalette[] globalPalettes
        {
            get
            {
                if (_globalPalettes == null)
                    GenerateGlobalPalettes("./Resources/DefaultPalette.pal", 16);
                return _globalPalettes;
            }
            set
            {
                _globalPalettes = value;
            }
        }
        public static int GlobalPalettesLength
        {
            get
            {
                return globalPalettes.Length;
            }
        }
        internal Color[] colors;
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
                return globalPalettes[0].Length;
            }
        }
        public static event Action SelectedGlobalPaletteChange, GlobalPalletesChange, GlobalPalletesChangeExcecuted;
        public static event Action<PaletteId> OneGlobalPaletteChange;
        public byte Length { get; internal set; }

        internal ColorPalette()
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
            if (globalPalettes == null) return default(Color);

            return globalPalettes[(int)selectedPalette].GetColor(i); ;
        }

        public static Color GetGlobalColor(byte i, PaletteId pid)
        {
            if (globalPalettes == null) return default(Color);

            return globalPalettes[(int)pid].GetColor(i); ;
        }

        public static void SetGlobalColor(byte i, Color c)
        {
            if (globalPalettes == null) return;

            globalPalettes[(int)SelectedPalette].SetColor(i, c);
            OneGlobalPaletteChange?.Invoke(selectedPalette);
        }

        public static void SetGlobalColor(byte i, Color c, PaletteId pid)
        {
            if (globalPalettes == null) return;

            globalPalettes[(int)pid].SetColor(i, c);
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
            globalPalettes = new ColorPalette[(bytes.Length) / (PaletteSize * 3)];
            int baseIndex = 0;
            int index = 0;
            for (int i = 0; i < globalPalettes.Length; i++)
            {
                globalPalettes[i] = new ColorPalette
                {
                    Length = PaletteSize,
                    colors = new Color[PaletteSize]
                };
                baseIndex = (i * 3 * PaletteSize);
                for (int j = 0; j < PaletteSize; j++)
                {
                    index = baseIndex + j * 3;
                    globalPalettes[i].colors[j] = Color.FromArgb(255 ,bytes[index], bytes[index + 1], bytes[index + 2]);
                }
            }
            GlobalPalletesChange?.Invoke();
            GlobalPalletesChangeExcecuted?.Invoke();
        }

        internal static void eventTrigger()
        {
            GlobalPalletesChange?.Invoke();
            GlobalPalletesChangeExcecuted?.Invoke();
        }
    }
}
