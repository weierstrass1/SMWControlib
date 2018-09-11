using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMWControlibControls.GraphicsControls
{
    public abstract class Theme
    {
        private static Color normalButtonBorderColor;
        public static Color NormalButtonBorderColor
        {
            get
            {
                return normalButtonBorderColor;
            }
            set
            {
                normalButtonBorderColor = value;
                NormalButtonBorderColorChanged?.Invoke(normalButtonBorderColor);
            }
        }
        private static Color normalButtonBackColor;
        public static Color NormalButtonBackColor
        {
            get
            {
                return normalButtonBackColor;
            }
            set
            {
                normalButtonBackColor = value;
                NormalButtonBackColorChanged?.Invoke(normalButtonBackColor);
            }
        }
        private static Color normalButtonHoverBackColor;
        public static Color NormalButtonHoverBackColor
        {
            get
            {
                return normalButtonHoverBackColor;
            }
            set
            {
                normalButtonHoverBackColor = value;
                NormalButtonHoverBackColorChanged?.Invoke(normalButtonHoverBackColor);
            }
        }
        public static event Action<Color> NormalButtonBorderColorChanged,
            NormalButtonBackColorChanged, NormalButtonHoverBackColorChanged;
        private static Color[] buttonBorderColor, 
            buttonBackColor, buttonHoverBackColor;
        public static event Action<int, Color> ButtonBorderColorChanged,
            ButtonBackColorChanged, ButtonHoverBackColorChanged;

        public Color GetButtonBorderColor(int i)
        {
            if (buttonBorderColor == null || buttonBorderColor.Length <= i || i < 0)
                return default(Color);

            return buttonBorderColor[i];
        }

        public Color GetButtonBackColor(int i)
        {
            if (buttonBackColor == null || buttonBackColor.Length <= i || i < 0)
                return default(Color);

            return buttonBackColor[i];
        }

        public Color GetButtonHoverBackColor(int i)
        {
            if (buttonHoverBackColor == null || buttonHoverBackColor.Length <= i || i < 0)
                return default(Color);

            return buttonHoverBackColor[i];
        }

        public void SetButtonBorderColor(int i, Color c)
        {
            if (buttonBorderColor == null || buttonBorderColor.Length <= i || i < 0)
                return;

            buttonBorderColor[i] = c;
            ButtonBorderColorChanged?.Invoke(i, c);
        }

        public void SetButtonBackColor(int i, Color c)
        {
            if (buttonBackColor == null || buttonBackColor.Length <= i || i < 0)
                return;

            buttonBackColor[i] = c;
            ButtonBackColorChanged?.Invoke(i, c);
        }

        public void SetButtonHoverBackColor(int i, Color c)
        {
            if (buttonHoverBackColor == null || buttonHoverBackColor.Length <= i || i < 0)
                return;

            buttonHoverBackColor[i] = c;
            ButtonHoverBackColorChanged?.Invoke(i, c);
        }
    }
}
