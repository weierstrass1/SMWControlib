using System;
using System.Collections.Generic;

namespace SMWControlibBackend.Logic.HDMA
{
    public enum ValueType { Numeric = 0, Boolean = 1, Option = 2, Bright = 3, OneColor = 4, TwoColors = 5, ThreeColors = 6, Red = 7, Green = 8, Blue = 9}
    public class EffectOptions
    {
        public int EffectOptionsID { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public static readonly EffectOptions Option0Static = new EffectOptions(
            0,
            "Static",
            "HDMA that never changes.");
        public static readonly EffectOptions Option1StaticTransition = new EffectOptions(
            1,
            "Static Transition",
            "Transition between 2 HDMA effects that never change.");
        public static readonly EffectOptions Option2StaticAnimation = new EffectOptions(
            2,
            "Static Animation",
            "Animation of HDMA that never change.");
        public static readonly EffectOptions Option3Positional = new EffectOptions(
            3,
            "Positional",
            "HDMA that changes depends on a position.");
        public static readonly EffectOptions Option4PositionalTransition = new EffectOptions(
            4,
            "Positional Transition",
            "Transition between 2 HDMA effects that changes depends on a position.");
        public static readonly EffectOptions Option5PositionalAnimation = new EffectOptions(
            5,
            "Positional Animation",
            "Animation of HDMA that changes depends on a position.");
        private EffectOptions(int ID, string name, string description)
        {
            EffectOptionsID = ID;
            Name = name;
            Description = description;
        }

        public static implicit operator EffectOptions(int i)
        {
            switch (i)
            {
                case 0:
                    return Option0Static;
                case 1:
                    return Option1StaticTransition;
                case 2:
                    return Option2StaticAnimation;
                case 3:
                    return Option3Positional;
                case 4:
                    return Option4PositionalTransition;
                default:
                    return Option5PositionalAnimation;
            }
        }

        public static implicit operator int(EffectOptions c)
        {
            return c.EffectOptionsID;
        }

        public override string ToString()
        {
            return Name;
        }
    }
    public class EffectType
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int ChannelsLength { get; private set; }
        private string[] registers { get; set; }
        private DMATransferMode[] transferModes { get; set; }
        private Dictionary<string, Tuple<string, ValueType, string, int, int>[]> setups;

        public Dictionary<int, HDMAValue[]> Values { get; private set; }

        private static EffectType brightness = new EffectType(
            "Brightness",
            "Can be used for gradients of brightness, disable screen refreshing per line and " +
            "Widescreen effect.", 
            "00#0");
        public static EffectType Brightness
        {
            get
            {
                if (brightness.setups == null) 
                {
                    brightness.setups = 
                        new Dictionary<string, Tuple<string, ValueType, string, int, int>[]>();
                }
                if (brightness.Values == null)
                {
                    brightness.Values =
                        new Dictionary<int, HDMAValue[]>();
                    HDMAValue[] t;

                    t = new HDMAValue[2];

                    t[0] = new HDMAValue(0, 0, "Disable",
                        "When a line is disabled, the game will have more time during NMI, disabled lines are black.",
                        ValueType.Boolean, "0", 0, 1);

                    t[1] = new HDMAValue(0, 0, "Brightness Level",
                        "Controls how bright the colors are.",
                        ValueType.Bright, "4567", 0, 15);

                    brightness.Values.Add(0, t);
                }
                return brightness;
            }
        }
        private static readonly EffectType pixelation = new EffectType(
            "Pixelation",
            "Can be used for pixelate lines.", 
            "06#0");
        public static EffectType Pixelation
        {
            get
            {
                if (pixelation.setups == null)
                {
                    pixelation.setups =
                        new Dictionary<string, Tuple<string, ValueType, string, int, int>[]>();
                }
                if (pixelation.Values == null)
                {
                    pixelation.Values =
                        new Dictionary<int, HDMAValue[]>();
                    HDMAValue[] t;

                    t = new HDMAValue[5];

                    t[0] = new HDMAValue(0, 0, "Layer 1",
                        "Allow pixelation for Layer 1.",
                        ValueType.Boolean, "4", 0, 1);
                    t[1] = new HDMAValue(0, 0, "Layer 2",
                        "Allow pixelation for Layer 2.",
                        ValueType.Boolean, "5", 0, 1);
                    t[2] = new HDMAValue(0, 0, "Layer 3",
                        "Allow pixelation for Layer 3.",
                        ValueType.Boolean, "6", 0, 1);
                    t[3] = new HDMAValue(0, 0, "Layer 4",
                        "Allow pixelation for Layer 4.",
                        ValueType.Boolean, "7", 0, 1);
                    t[4] = new HDMAValue(0,0, "Pixel Size",
                        "controls how pixelated the layer becomes.",
                          ValueType.Numeric, "0123", 0, 7);

                    pixelation.Values.Add(0, t);
                }
                return pixelation;
            }
        }
        private static readonly EffectType layer1HScroll = new EffectType(
            "Layer 1 H. Scroll.",
            "Can be used for different layer 1 horizontal scroll per line. Examples: Parallax Scroll and Wave Effect.",
            "0D#2");
        public static EffectType Layer1HScroll
        {
            get
            {
                if (layer1HScroll.setups == null)
                {
                    layer1HScroll.setups =
                        new Dictionary<string, Tuple<string, ValueType, string, int, int>[]>();
                }
                if (layer1HScroll.Values == null)
                {
                    layer1HScroll.Values =
                        new Dictionary<int, HDMAValue[]>();
                    HDMAValue[] t;

                    t = new HDMAValue[1];

                    t[0] = new HDMAValue(0,0, "Scroll",
                        "Controls the position of the scroll.",
                        ValueType.Numeric, "6789ABCDF", 0, 1023);

                    layer1HScroll.Values.Add(0, t);
                }
                return layer1HScroll;
            }
        }
        private static readonly EffectType layer1VScroll = new EffectType(
            "Layer 1 V. Scroll.",
            "Can be used for different layer 1 vertical scroll per line. Examples: Wave Effect.",
            "0E#2");
        public static EffectType Layer1VScroll
        {
            get
            {
                if (layer1VScroll.setups == null)
                {
                    layer1VScroll.setups =
                        new Dictionary<string, Tuple<string, ValueType, string, int, int>[]>();
                }
                if (layer1VScroll.Values == null)
                {
                    layer1VScroll.Values =
                        new Dictionary<int, HDMAValue[]>();
                    HDMAValue[] t;

                    t = new HDMAValue[1];

                    t[0] = new HDMAValue(0, 0, "Scroll",
                        "Controls the position of the scroll.",
                        ValueType.Numeric, "6789ABCDF", 0, 1023);

                    layer1VScroll.Values.Add(0, t);
                }
                return layer1VScroll;
            }
        }
        private static readonly EffectType layer1HVScroll = new EffectType(
            "Layer 1 H. and V. Scroll.",
            "Can be used for different layer 1 horizontal and vertical scroll per line. Examples: Wave Effect.",
            "0D#3");
        public static EffectType Layer1HVScroll
        {
            get
            {
                if (layer1HVScroll.setups == null)
                {
                    layer1HVScroll.setups =
                        new Dictionary<string, Tuple<string, ValueType, string, int, int>[]>();
                }
                if (layer1HVScroll.Values == null)
                {
                    layer1HVScroll.Values =
                        new Dictionary<int, HDMAValue[]>();
                    HDMAValue[] t;

                    t = new HDMAValue[1];

                    t[0] = new HDMAValue(0, 0, "Horizontal Scroll",
                        "Controls the position of the horizontal scroll.",
                        ValueType.Numeric, "6789ABCDF", 0, 1023);
                    layer1HVScroll.Values.Add(0, t);

                    t = new HDMAValue[1];

                    t[0] = new HDMAValue(1, 0, "Vertical Scroll",
                        "Controls the position of the vertical scroll.",
                        ValueType.Numeric, "6789ABCDF", 0, 1023);
                    layer1HVScroll.Values.Add(1, t);
                }
                return layer1HVScroll;
            }
        }

        private static readonly EffectType layer2HScroll = new EffectType(
    "Layer 2 H. Scroll.",
    "Can be used for different layer 2 horizontal scroll per line. Examples: Parallax Scroll and Wave Effect.",
    "0F#2");
        public static EffectType Layer2HScroll
        {
            get
            {
                if (layer2HScroll.setups == null)
                {
                    layer2HScroll.setups =
                        new Dictionary<string, Tuple<string, ValueType, string, int, int>[]>();
                }
                if (layer2HScroll.Values == null)
                {
                    layer2HScroll.Values =
                        new Dictionary<int, HDMAValue[]>();
                    HDMAValue[] t;

                    t = new HDMAValue[1];

                    t[0] = new HDMAValue(0, 0, "Scroll",
                        "Controls the position of the scroll.",
                        ValueType.Numeric, "6789ABCDF", 0, 1023);

                    layer2HScroll.Values.Add(0, t);
                }
                return layer2HScroll;
            }
        }
        private static readonly EffectType layer2VScroll = new EffectType(
            "Layer 2 V. Scroll.",
            "Can be used for different layer 2 vertical scroll per line. Examples: Wave Effect.",
            "10#2");
        public static EffectType Layer2VScroll
        {
            get
            {
                if (layer2VScroll.setups == null)
                {
                    layer2VScroll.setups =
                        new Dictionary<string, Tuple<string, ValueType, string, int, int>[]>();
                }
                if (layer2VScroll.Values == null)
                {
                    layer2VScroll.Values =
                        new Dictionary<int, HDMAValue[]>();
                    HDMAValue[] t;

                    t = new HDMAValue[1];

                    t[0] = new HDMAValue(0, 0, "Scroll",
                        "Controls the position of the scroll.",
                        ValueType.Numeric, "6789ABCDF", 0, 1023);

                    layer2VScroll.Values.Add(0, t);
                }
                return layer2VScroll;
            }
        }
        private static readonly EffectType layer2HVScroll = new EffectType(
            "Layer 2 H. and V. Scroll.",
            "Can be used for different layer 2 horizontal and vertical scroll per line. Examples: Wave Effect.",
            "0F#3");
        public static EffectType Layer2HVScroll
        {
            get
            {
                if (layer2HVScroll.setups == null)
                {
                    layer2HVScroll.setups =
                        new Dictionary<string, Tuple<string, ValueType, string, int, int>[]>();
                }
                if (layer2HVScroll.Values == null)
                {
                    layer2HVScroll.Values =
                        new Dictionary<int, HDMAValue[]>();
                    HDMAValue[] t;

                    t = new HDMAValue[1];

                    t[0] = new HDMAValue(0, 0, "Horizontal Scroll",
                        "Controls the position of the horizontal scroll.",
                        ValueType.Numeric, "6789ABCDF", 0, 1023);
                    layer2HVScroll.Values.Add(0, t);

                    t = new HDMAValue[1];

                    t[0] = new HDMAValue(1, 0, "Vertical Scroll",
                        "Controls the position of the vertical scroll.",
                        ValueType.Numeric, "6789ABCDF", 0, 1023);
                    layer2HVScroll.Values.Add(1, t);
                }
                return layer2HVScroll;
            }
        }

        private static readonly EffectType layer3HScroll = new EffectType(
    "Layer 3 H. Scroll.",
    "Can be used for different layer 3 horizontal scroll per line. Examples: Parallax Scroll and Wave Effect.",
    "11#2");
        public static EffectType Layer3HScroll
        {
            get
            {
                if (layer3HScroll.setups == null)
                {
                    layer3HScroll.setups =
                        new Dictionary<string, Tuple<string, ValueType, string, int, int>[]>();
                }
                if (layer3HScroll.Values == null)
                {
                    layer3HScroll.Values =
                        new Dictionary<int, HDMAValue[]>();
                    HDMAValue[] t;

                    t = new HDMAValue[1];

                    t[0] = new HDMAValue(0, 0, "Scroll",
                        "Controls the position of the scroll.",
                        ValueType.Numeric, "6789ABCDF", 0, 1023);

                    layer3HScroll.Values.Add(0, t);
                }
                return layer3HScroll;
            }
        }
        private static readonly EffectType layer3VScroll = new EffectType(
            "Layer 3 V. Scroll.",
            "Can be used for different layer 3 vertical scroll per line. Examples: Wave Effect.",
            "12#2");
        public static EffectType Layer3VScroll
        {
            get
            {
                if (layer3VScroll.setups == null)
                {
                    layer3VScroll.setups =
                        new Dictionary<string, Tuple<string, ValueType, string, int, int>[]>();
                }
                if (layer3VScroll.Values == null)
                {
                    layer3VScroll.Values =
                        new Dictionary<int, HDMAValue[]>();
                    HDMAValue[] t;

                    t = new HDMAValue[1];

                    t[0] = new HDMAValue(0, 0, "Scroll",
                        "Controls the position of the scroll.",
                        ValueType.Numeric, "6789ABCDF", 0, 1023);

                    layer3VScroll.Values.Add(0, t);
                }
                return layer3VScroll;
            }
        }
        private static readonly EffectType layer3HVScroll = new EffectType(
            "Layer 3 H. and V. Scroll.",
            "Can be used for different layer 3 horizontal and vertical scroll per line. Examples: Wave Effect.",
            "11#3");
        public static EffectType Layer3HVScroll
        {
            get
            {
                if (layer3HVScroll.setups == null)
                {
                    layer3HVScroll.setups =
                        new Dictionary<string, Tuple<string, ValueType, string, int, int>[]>();
                }
                if (layer3HVScroll.Values == null)
                {
                    layer3HVScroll.Values =
                        new Dictionary<int, HDMAValue[]>();
                    HDMAValue[] t;

                    t = new HDMAValue[1];

                    t[0] = new HDMAValue(0, 0, "Horizontal Scroll",
                        "Controls the position of the horizontal scroll.",
                        ValueType.Numeric, "6789ABCDF", 0, 1023);
                    layer3HVScroll.Values.Add(0, t);

                    t = new HDMAValue[1];

                    t[0] = new HDMAValue(1, 0, "Vertical Scroll",
                        "Controls the position of the vertical scroll.",
                        ValueType.Numeric, "6789ABCDF", 0, 1023);
                    layer3HVScroll.Values.Add(1, t);
                }
                return layer3HVScroll;
            }
        }

        private static readonly EffectType layer4HScroll = new EffectType(
    "Layer 4 H. Scroll.",
    "Can be used for different layer 4 horizontal scroll per line. Examples: Parallax Scroll and Wave Effect.",
    "13#2");
        public static EffectType Layer4HScroll
        {
            get
            {
                if (layer4HScroll.setups == null)
                {
                    layer4HScroll.setups =
                        new Dictionary<string, Tuple<string, ValueType, string, int, int>[]>();
                }
                if (layer4HScroll.Values == null)
                {
                    layer4HScroll.Values =
                        new Dictionary<int, HDMAValue[]>();
                    HDMAValue[] t;

                    t = new HDMAValue[1];

                    t[0] = new HDMAValue(0, 0, "Scroll",
                        "Controls the position of the scroll.",
                        ValueType.Numeric, "6789ABCDF", 0, 1023);

                    layer4HScroll.Values.Add(0, t);
                }
                return layer4HScroll;
            }
        }
        private static readonly EffectType layer4VScroll = new EffectType(
            "Layer 4 V. Scroll.",
            "Can be used for different layer 4 vertical scroll per line. Examples: Wave Effect.",
            "14#2");
        public static EffectType Layer4VScroll
        {
            get
            {
                if (layer4VScroll.setups == null)
                {
                    layer4VScroll.setups =
                        new Dictionary<string, Tuple<string, ValueType, string, int, int>[]>();
                }
                if (layer4VScroll.Values == null)
                {
                    layer4VScroll.Values =
                        new Dictionary<int, HDMAValue[]>();
                    HDMAValue[] t;

                    t = new HDMAValue[1];

                    t[0] = new HDMAValue(0, 0, "Scroll",
                        "Controls the position of the scroll.",
                        ValueType.Numeric, "6789ABCDF", 0, 1023);

                    layer4VScroll.Values.Add(0, t);
                }
                return layer4VScroll;
            }
        }
        private static readonly EffectType layer4HVScroll = new EffectType(
            "Layer 4 H. and V. Scroll.",
            "Can be used for different layer 4 horizontal and vertical scroll per line. Examples: Wave Effect.",
            "13#3");
        public static EffectType Layer4HVScroll
        {
            get
            {
                if (layer4HVScroll.setups == null)
                {
                    layer4HVScroll.setups =
                        new Dictionary<string, Tuple<string, ValueType, string, int, int>[]>();
                }
                if (layer4HVScroll.Values == null)
                {
                    layer4HVScroll.Values =
                        new Dictionary<int, HDMAValue[]>();
                    HDMAValue[] t;

                    t = new HDMAValue[1];

                    t[0] = new HDMAValue(0, 0, "Horizontal Scroll",
                        "Controls the position of the horizontal scroll.",
                        ValueType.Numeric, "6789ABCDF", 0, 1023);
                    layer4HVScroll.Values.Add(0, t);

                    t = new HDMAValue[1];

                    t[0] = new HDMAValue(1, 0, "Vertical Scroll",
                        "Controls the position of the vertical scroll.",
                        ValueType.Numeric, "6789ABCDF", 0, 1023);
                    layer4HVScroll.Values.Add(1, t);
                }
                return layer4HVScroll;
            }
        }

        private static readonly EffectType palette = new EffectType("Palette", 
            "Can be used to change 1 color of the palette per line.",
            "21#3");
        public static EffectType Palette
        {
            get
            {
                if (palette.setups == null)
                {
                    palette.setups =
                        new Dictionary<string, Tuple<string, ValueType, string, int, int>[]>();
                }
                if (palette.Values == null)
                {
                    palette.Values =
                        new Dictionary<int, HDMAValue[]>();
                    HDMAValue[] t;

                    t = new HDMAValue[1];

                    t[0] = new HDMAValue(1,0, "Color Index",
                        "Indicate the index of the color on the palette.",
                        ValueType.Numeric, "01234567", 0, 255);

                    palette.Values.Add(1, t);

                    t = new HDMAValue[1];

                    t[0] = new HDMAValue(2,0, "Color",
                        "Controls the color of the palette.",
                        ValueType.ThreeColors, "123456789ABCDEF", 0, 127);
                    palette.Values.Add(2, t);
                }
                return palette;
            }
        }
        private static readonly EffectType Windowing1Window = new EffectType(
            "Windowing for 1 window",
            "Can be used for layer and color windowing at the same time.",
            "26#1");
        private static readonly EffectType Windowing2Windows = new EffectType(
            "Windowing for 2 windows",
            "Can be used for layer and color windowing at the same time.",
            "26#4");
        private static readonly EffectType LayerWindowing1Window = new EffectType(
            "Layer Windowing for 1 window",
            "Can be used for layer windowing.",
            "26#1");
        private static readonly EffectType LayerWindowing2Windows = new EffectType(
            "Layer Windowing for 2 windows",
            "Can be used for layer windowing.",
            "26#4");
        private static readonly EffectType ColorWindowing1Window = new EffectType(
            "Color Windowing for 1 window",
            "Can be used for color windowing.",
            "26#1");
        private static readonly EffectType ColorWindowing2Windows2 = new EffectType(
            "Color Windowing for 2 windows",
            "Can be used for color windowing.",
            "26#4");
        private static readonly EffectType ColorGradient1Color = new EffectType(
            "Color Gradient of 1 Color",
            "Can be used for Red, Green, Blue, Yellow, Purple, Cyan and White color gradient.",
            "32#0");
        private static readonly EffectType ColorGradient2Colors = new EffectType(
            "Color Gradient of 2 Colors",
            "Can be used for color gradient of combinations of 2 colors (Red, Green, Blue, Yellow, Purple, Cyan and White).",
            "32#2");
        private static readonly EffectType ColorGradient3Colors = new EffectType(
            "Color Gradient of 3 Colors",
            "Can be used for color gradient of combinations of 3 colors (Red, Green, Blue).",
            "32#0", "32#2");

        private static EffectType[] effectTypes;
        public static EffectType[] EffectTypes
        {
            get
            {
                if(effectTypes == null)
                {
                    effectTypes = new EffectType[24];
                    effectTypes[0] = Brightness;
                    effectTypes[1] = ColorGradient1Color;
                    effectTypes[2] = ColorGradient2Colors;
                    effectTypes[3] = ColorGradient3Colors;
                    effectTypes[4] = Palette;
                    effectTypes[5] = Layer1HScroll;
                    effectTypes[6] = Layer1VScroll;
                    effectTypes[7] = Layer1HVScroll;
                    effectTypes[8] = Layer2HScroll;
                    effectTypes[9] = Layer2VScroll;
                    effectTypes[10] = Layer2HVScroll;
                    effectTypes[11] = Layer3HScroll;
                    effectTypes[12] = Layer3VScroll;
                    effectTypes[13] = Layer3HVScroll;
                    effectTypes[14] = Layer4HScroll;
                    effectTypes[15] = Layer4VScroll;
                    effectTypes[16] = Layer4HVScroll;
                    effectTypes[17] = Pixelation;
                    effectTypes[18] = ColorWindowing1Window;
                    effectTypes[19] = ColorWindowing2Windows2;
                    effectTypes[20] = LayerWindowing1Window;
                    effectTypes[21] = LayerWindowing2Windows;
                    effectTypes[22] = Windowing1Window;
                    effectTypes[23] = Windowing2Windows;
                }
                return effectTypes;
            }
        }

        private EffectType(string name, string description, params string[] regs)
        {
            Name = name;
            Description = description;
            ChannelsLength = regs.Length;

            transferModes = new DMATransferMode[ChannelsLength];
            registers = new string[ChannelsLength];

            string[] s;

            for (int i = 0; i < regs.Length; i++)
            {
                s = regs[i].Split('#');
                registers[i] = s[0];
                transferModes[i] = int.Parse(s[1]);
            }
        }

        public DMATransferMode GetTransferMode(uint id)
        {
            if (id >= ChannelsLength) id = (uint)(ChannelsLength - 1);
            return transferModes[id];
        }

        public override string ToString()
        {
            string s = "";
            for (int i = 0; i < registers.Length; i++)
            {
                s += registers[i] + "-" + (+transferModes[i]);
                if (i < registers.Length - 1) s += ",";
            }
            s += ":" + Name;
            return s;
        }
    }
    public class Effect
    {
        public EffectType Type { get; private set; }
        private HDMAChannel[] channels { get; set; }
        public EffectOptions Option { get; private set; }

        public Effect(EffectType type, EffectOptions option)
        {
            Type = type;
            channels = new HDMAChannel[Type.ChannelsLength];
            Option = option;

            int j = 0;
            HDMAChannel c;
            for (int i = 0; i < 8 && j < channels.Length; i++) 
            {
                c = i;
                if (!c.UsedByOriginalGame) 
                {
                    channels[j] = c;
                    j++;
                }
            }

            if (j < channels.Length - 1)
            {
                for (int i = 0; i < 8 && j < channels.Length; i++)
                {
                    c = i;
                    if (c.UsedByOriginalGame)
                    {
                        channels[j] = c;
                        j++;
                    }
                }
            }
        }

        public HDMAChannel GetChannel(uint id)
        {
            if (id >= channels.Length) id = (uint)(channels.Length - 1);
            return channels[id];
        }

        public void SetChannel(uint id, HDMAChannel channel)
        {
            if (id >= channels.Length) id = (uint)(channels.Length - 1);
            channels[id] = channel;
        }
        public void SetChannels(params HDMAChannel[] Channels)
        {
            for (int i = 0; i < channels.Length && i < Channels.Length; i++)
            {
                channels[i] = Channels[i];
            }
        }
    }
}
