using System.Drawing;

namespace SMWControlibBackend.Graphics
{
    public class GlobalColorPaletteContainer
    {
        public ColorPaletteContainer[] Palettes;

        public void ToGlobalColorPalette()
        {
            ColorPalette.globalPalettes = new ColorPalette[Palettes.Length];

            for (int i = 0; i < Palettes.Length; i++)
            {
                ColorPalette.globalPalettes[i] = new ColorPalette();
                ColorPalette.globalPalettes[i].Length = (byte)Palettes[i].Red.Length;
                ColorPalette.globalPalettes[i].colors = new Color[ColorPalette.globalPalettes[i].Length];
                for (int j = 0; j < ColorPalette.globalPalettes[i].Length; j++)
                {
                    ColorPalette.globalPalettes[i].colors[j] =
                        Color.FromArgb(Palettes[i].Red[j], 
                        Palettes[i].Green[j],
                        Palettes[i].Blue[j]);
                }
            }
            ColorPalette.eventTrigger();
        }

        public void ToGlobalColorPaletteContainer()
        {
            Palettes =
                new ColorPaletteContainer[ColorPalette.globalPalettes.Length];

            Color c;

            for (int i = 0; i < Palettes.Length; i++)
            {
                Palettes[i] = new ColorPaletteContainer();
                Palettes[i].Red = new byte[ColorPalette.globalPalettes[i].Length];
                Palettes[i].Green = new byte[ColorPalette.globalPalettes[i].Length];
                Palettes[i].Blue = new byte[ColorPalette.globalPalettes[i].Length];

                for (int j = 0; j < Palettes[i].Red.Length; j++)
                {
                    c = ColorPalette.globalPalettes[i].GetColor((byte)j);
                    Palettes[i].Red[j] = c.R;
                    Palettes[i].Green[j] = c.G;
                    Palettes[i].Blue[j] = c.B;
                }
            }
        }
    }
}
