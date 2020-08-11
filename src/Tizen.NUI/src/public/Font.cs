using System;
using System.ComponentModel;

namespace Tizen.NUI
{
    /// <summary>
    /// Enumeration type for the unit of font size.
    /// </summary>
    /// <since_tizen> 6 </since_tizen>
    public enum FontSizeUnit
    {
        /// <summary>
        /// Point(pt).
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        Point,
        /// <summary>
        /// Pixel(px).
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        Pixel
    }

    /// <summary>
    /// Font provides properties for font information.
    /// It's immutable.
    /// </summary>
    /// <since_tizen> 6 </since_tizen>
    public class Font : Disposable
    {
        private string familyName;
        private float? size;
        private FontSizeUnit sizeUnit;
        private FontWeightType weight;
        private FontWidthType width;
        private FontSlantType slant;

        /// <summary>
        /// Creates the Font instance with Family Name.
        /// </summary>
        /// <param name="FamilyName">A font family name</param>
        /// <since_tizen> 6 </since_tizen>
        public Font(string FamilyName)
        {
            familyName = FamilyName;
        }

        /// <summary>
        /// Creates the Font instance with Family Name and Size.
        /// </summary>
        /// <param name="FamilyName">A font family name</param>
        /// <param name="Size">A font size</param>
        /// <param name="SizeUnit">A unit of the font size</param>
        /// <since_tizen> 6 </since_tizen>
        public Font(string FamilyName, float? Size, FontSizeUnit SizeUnit) : this(FamilyName)
        {
            size = Size;
            sizeUnit = SizeUnit;
        }

        /// <summary>
        /// Creates the Font instance with Family Name, Size and Style.
        /// </summary>
        /// <param name="FamilyName">A font family name</param>
        /// <param name="Size">A font size</param>
        /// <param name="SizeUnit">A unit of the font size</param>
        /// <param name="Weight">A font weight</param>
        /// <param name="Width">A font width</param>
        /// <param name="Slant">A font slant</param>
        /// <since_tizen> 6 </since_tizen>
        public Font(string FamilyName, float? Size, FontSizeUnit SizeUnit, FontWeightType Weight, FontWidthType Width, FontSlantType Slant) : this(FamilyName, Size, SizeUnit)
        {
            weight = Weight;
            width = Width;
            slant = Slant;
        }

        private T getFontStyleType<T>(PropertyMap styleMap, string key)
        {
            PropertyValue value = styleMap?.Find(0, key);
            string valueString;

            if (value != null && value.Get(out valueString) == true)
            {
                return (T)Enum.Parse(typeof(T), valueString, true);
            }
            else
            {
                return (T)Enum.Parse(typeof(T), "None");
            }
        }

        /// <summary>
        /// Creates the Font instance with Family Name, Size and Style.
        /// For internal usage.
        /// </summary>
        /// <param name="FamilyName">A font family name</param>
        /// <param name="Size">A font size</param>
        /// <param name="SizeUnit">A unit of the font size</param>
        /// <param name="StyleMap">A property map which has weight, width and slant properties</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Font(string FamilyName, float? Size, FontSizeUnit SizeUnit, PropertyMap StyleMap) : this(FamilyName, Size, SizeUnit)
        {
            weight = getFontStyleType<FontWeightType>(StyleMap, "weight");
            width = getFontStyleType<FontWidthType>(StyleMap, "width");
            slant = getFontStyleType<FontSlantType>(StyleMap, "slant");
        }

        /// <summary>
        /// The Size property.<br />
        /// The size of font. It allows null.<br />
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        public float? Size
        {
            get
            {
                return this.size;
            }
        }

        /// <summary>
        /// The SizeUnit property.<br />
        /// It defines a unit of Font.Size property.<br />
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        public FontSizeUnit SizeUnit
        {
            get
            {
                return this.sizeUnit;
            }
        }

        /// <summary>
        /// The FamilyName property.<br />
        /// The requested font family to use.<br />
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        public string FamilyName
        {
            get
            {
                return this.familyName;
            }
        }

        /// <summary>
        /// The Weight property.<br />
        /// The requested font weight to use.<br />
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        public FontWeightType Weight
        {
            get
            {
                return this.weight;
            }
        }

        /// <summary>
        /// The Width property.<br />
        /// The requested font width to use.<br />
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        public FontWidthType Width
        {
            get
            {
                return this.width;
            }
        }

        /// <summary>
        /// The Slant property.<br />
        /// The requested font slant to use.<br />
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        public FontSlantType Slant
        {
            get
            {
                return this.slant;
            }
        }
    }
}