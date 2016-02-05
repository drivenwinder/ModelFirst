using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Drawing2D;
using System.Drawing;
using ComponentFactory.Krypton.Toolkit;

namespace EAP.Win.UI.Utils
{
    public class PaletteHelper
    {

        private static Blend _activeBackColorGradientBlend;
        public static Blend ActiveBackColorGradientBlend
        {
            get
            {
                if (_activeBackColorGradientBlend == null)
                {
                    Blend blend = new Blend(2);

                    blend.Factors = new float[] { 0.5F, 1.0F };
                    blend.Positions = new float[] { 0.0F, 1.0F };
                    _activeBackColorGradientBlend = blend;
                }

                return _activeBackColorGradientBlend;
            }
        }

        public static Color ActiveBackColor1
        {
            get
            {
                return KryptonManager.CurrentGlobalPalette
                    .GetBackColor1(PaletteBackStyle.HeaderDockActive, PaletteState.Normal);
            }
        }

        public static Color ActiveBackColor2
        {
            get
            {
                return KryptonManager.CurrentGlobalPalette
                    .GetBackColor2(PaletteBackStyle.HeaderDockActive, PaletteState.Normal);
            }
        }

        public static Color InactiveBackColor1
        {
            get
            {
                return KryptonManager.CurrentGlobalPalette
                       .GetBackColor1(PaletteBackStyle.HeaderDockInactive, PaletteState.Normal);
            }
        }

        public static Color InactiveBackColor2
        {
            get
            {
                return KryptonManager.CurrentGlobalPalette
                       .GetBackColor2(PaletteBackStyle.HeaderDockInactive, PaletteState.Normal);
            }
        }

        public static Color ActiveTextColor
        {
            get
            {
                return KryptonManager.CurrentGlobalPalette
                    .GetContentShortTextColor1(PaletteContentStyle.HeaderDockActive, PaletteState.Normal);
            }
        }

        public static Color InactiveTextColor
        {
            get
            {
                return KryptonManager.CurrentGlobalPalette
                    .GetContentShortTextColor1(PaletteContentStyle.HeaderDockInactive, PaletteState.Normal);
            }
        }

        public static Color GetBackColor
        {
            get
            {
                return KryptonManager.CurrentGlobalPalette
                        .GetBackColor1(PaletteBackStyle.PanelAlternate, PaletteState.Normal);
            }
        }

        public static Pen PenToolWindowTabBorder
        {
            get
            {
                return new Pen(KryptonManager.CurrentGlobalPalette
                    .GetBorderColor1(PaletteBorderStyle.HeaderDockInactive, PaletteState.Normal));
            }
        }

        public static Pen PenDocumentTabActiveBorder
        {
            get
            {
                return new Pen(KryptonManager.CurrentGlobalPalette
                    .GetBorderColor1(PaletteBorderStyle.HeaderDockActive, PaletteState.Normal));
            }
        }

        public static Pen PenDocumentTabInactiveBorder
        {
            get
            {
                return new Pen(KryptonManager.CurrentGlobalPalette
                    .GetBorderColor1(PaletteBorderStyle.HeaderDockInactive, PaletteState.Normal));
            }
        }

        public static Brush BrushDocumentInactiveBackground
        {
            get
            {
                return new System.Drawing.SolidBrush(KryptonManager.CurrentGlobalPalette
                    .GetBackColor1(PaletteBackStyle.HeaderDockInactive, PaletteState.Normal));
            }
        }

        public static Color ColorToolWindowActiveText
        {
            get
            {
                return KryptonManager.CurrentGlobalPalette
                  .GetContentShortTextColor1(PaletteContentStyle.HeaderDockActive, PaletteState.Normal);
            }
        }

        public static Color ColorDocumentActiveText
        {
            get
            {
                return KryptonManager.CurrentGlobalPalette
                  .GetContentShortTextColor1(PaletteContentStyle.HeaderDockActive, PaletteState.Normal);
            }
        }

        public static Color ColorToolWindowInactiveText
        {
            get
            {
                return KryptonManager.CurrentGlobalPalette
                  .GetContentShortTextColor1(PaletteContentStyle.LabelTitleControl, PaletteState.Normal);
            }
        }

        public static Color ColorDocumentInactiveText
        {
            get
            {
                return KryptonManager.CurrentGlobalPalette
                  .GetContentShortTextColor1(PaletteContentStyle.HeaderDockInactive, PaletteState.Normal);
            }
        }
    }
}
