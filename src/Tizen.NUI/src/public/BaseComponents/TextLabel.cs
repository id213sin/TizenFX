/*
 * Copyright(c) 2020 Samsung Electronics Co., Ltd.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 */

extern alias TizenSystemSettings;
using TizenSystemSettings.Tizen.System;
using System;
using System.Globalization;
using System.ComponentModel;
using Tizen.NUI.Binding;

namespace Tizen.NUI.BaseComponents
{
    /// <summary>
    /// A control which renders a short text string.<br />
    /// Text labels are lightweight, non-editable, and do not respond to the user input.<br />
    /// </summary>
    /// <since_tizen> 3 </since_tizen>
    public partial class TextLabel : View
    {
        private class TextLayout : LayoutItem
        {
            protected override void OnMeasure(MeasureSpecification widthMeasureSpec, MeasureSpecification heightMeasureSpec)
            {
                // Padding will be automatically applied by DALi TextLabel.
                float totalWidth = widthMeasureSpec.Size.AsDecimal();
                float totalHeight = heightMeasureSpec.Size.AsDecimal();

                if (widthMeasureSpec.Mode == MeasureSpecification.ModeType.Exactly)
                {
                    if (heightMeasureSpec.Mode != MeasureSpecification.ModeType.Exactly)
                    {
                        totalHeight = Owner.GetHeightForWidth(totalWidth);
                        heightMeasureSpec = new MeasureSpecification(new LayoutLength(totalHeight), MeasureSpecification.ModeType.Exactly);
                    }
                }
                else
                {
                    if (heightMeasureSpec.Mode == MeasureSpecification.ModeType.Exactly)
                    {
                        // GetWidthForHeight is not implemented.
                        totalWidth = Owner.GetNaturalSize().Width;
                        widthMeasureSpec = new MeasureSpecification(new LayoutLength(totalWidth), MeasureSpecification.ModeType.Exactly);
                    }
                    else
                    {
                        Vector3 naturalSize = Owner.GetNaturalSize();
                        totalWidth = naturalSize.Width;
                        totalHeight = naturalSize.Height;

                        heightMeasureSpec = new MeasureSpecification(new LayoutLength(totalHeight), MeasureSpecification.ModeType.Exactly);
                        widthMeasureSpec = new MeasureSpecification(new LayoutLength(totalWidth), MeasureSpecification.ModeType.Exactly);
                    }
                }

                MeasuredSize.StateType childWidthState = MeasuredSize.StateType.MeasuredSizeOK;
                MeasuredSize.StateType childHeightState = MeasuredSize.StateType.MeasuredSizeOK;

                SetMeasuredDimensions(ResolveSizeAndState(new LayoutLength(totalWidth), widthMeasureSpec, childWidthState),
                                       ResolveSizeAndState(new LayoutLength(totalHeight), heightMeasureSpec, childHeightState));
            }
        }

        static TextLabel() { }

        private string textLabelSid = null;
        private bool systemlangTextFlag = false;
        private TextLabelSelectorData selectorData;
        private Font font = null;
        private Font currentFont = null;
        private bool fontDirtyFlag = true;

        private string getStyleFamilyName()
        {
            return Style?.FontFamily.Normal;
        }

        private float? getStylePointSize()
        {
            return Style?.PointSize?.Normal;
        }

        private float? getStylePixelSize()
        {
            // FIXME: Why pixelSize isn't a Selector type variable?
            return Style?.PixelSize;
        }

        private FontWeightType getStyleFontWeight()
        {
            return FontWeightType.Normal;
        }

        private FontWidthType getStyleFontWidth()
        {
            return FontWidthType.Normal;
        }

        private FontSlantType getStyleFontSlant()
        {
            return FontSlantType.Normal;
        }

        /// <summary>
        /// Return a copied Style instance of the TextLabel.
        /// </summary>
        /// <remarks>
        /// It returns copied style instance so that changing it does not effect to the view.
        /// Style setting is possible by using constructor or the function of <see cref="View.ApplyStyle"/>.
        /// </remarks>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public TextLabelStyle Style => new TextLabelStyle(this);

        /// <summary>
        /// Creates the TextLabel control.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public TextLabel() : this(Interop.TextLabel.TextLabel_New__SWIG_0(), true)
        {
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            Layout = new TextLayout();
        }

        /// This will be public opened in next release of tizen after ACR done. Before ACR, it is used as HiddenAPI (InhouseAPI).
        [EditorBrowsable(EditorBrowsableState.Never)]
        public TextLabel(TextLabelStyle viewStyle) : this(Interop.TextLabel.TextLabel_New__SWIG_0(), true, viewStyle)
        {
            Layout = new TextLayout();
        }

        /// <summary>
        /// Creates the TextLabel with setting the status of shown or hidden.
        /// </summary>
        /// <param name="shown">false : Not displayed (hidden), true : displayed (shown)</param>
        /// This will be public opened in next release of tizen after ACR done. Before ACR, it is used as HiddenAPI (InhouseAPI).
        [EditorBrowsable(EditorBrowsableState.Never)]
        public TextLabel(bool shown) : this(Interop.TextLabel.TextLabel_New__SWIG_0(), true)
        {
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            Layout = new TextLayout();
            SetVisible(shown);
        }

        /// <summary>
        /// Creates the TextLabel control.
        /// </summary>
        /// <param name="text">The text to display</param>
        /// <since_tizen> 3 </since_tizen>
        public TextLabel(string text) : this(Interop.TextLabel.TextLabel_New__SWIG_1(text), true)
        {
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            Layout = new TextLayout();
        }

        /// <summary>
        /// Creates the TextLabel with setting the status of shown or hidden.
        /// </summary>
        /// <param name="text">The text to display</param>
        /// <param name="shown">false : Not displayed (hidden), true : displayed (shown)</param>
        /// This will be public opened in next release of tizen after ACR done. Before ACR, it is used as HiddenAPI (InhouseAPI).
        [EditorBrowsable(EditorBrowsableState.Never)]
        public TextLabel(string text, bool shown) : this(Interop.TextLabel.TextLabel_New__SWIG_1(text), true)
        {
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            Layout = new TextLayout();
            SetVisible(shown);
        }

        internal TextLabel(TextLabel handle, bool shown = true) : this(Interop.TextLabel.new_TextLabel__SWIG_1(TextLabel.getCPtr(handle)), true)
        {
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();

            if (!shown)
            {
                SetVisible(false);
            }
        }

        internal TextLabel(global::System.IntPtr cPtr, bool cMemoryOwn, ViewStyle viewStyle, bool shown = true) : base(Interop.TextLabel.TextLabel_SWIGUpcast(cPtr), cMemoryOwn, viewStyle)
        {
            if (!shown)
            {
                SetVisible(false);
            }
        }

        internal TextLabel(global::System.IntPtr cPtr, bool cMemoryOwn, bool shown = true) : base(Interop.TextLabel.TextLabel_SWIGUpcast(cPtr), cMemoryOwn)
        {
            if (!shown)
            {
                SetVisible(false);
            }
        }

        /// <summary>
        /// The TranslatableText property.<br />
        /// The text can set the SID value.<br />
        /// </summary>
        /// <exception cref='ArgumentNullException'>
        /// ResourceManager about multilingual is null.
        /// </exception>
        /// <since_tizen> 4 </since_tizen>
        public string TranslatableText
        {
            get
            {
                return (string)GetValue(TranslatableTextProperty);
            }
            set
            {
                SetValue(TranslatableTextProperty, value);
                selectorData?.TranslatableText.UpdateIfNeeds(this, value);
            }
        }
        private string translatableText
        {
            get
            {
                return textLabelSid;
            }
            set
            {
                if (NUIApplication.MultilingualResourceManager == null)
                {
                    throw new ArgumentNullException("ResourceManager about multilingual is null");
                }
                string translatableText = null;
                textLabelSid = value;
                translatableText = NUIApplication.MultilingualResourceManager?.GetString(textLabelSid, new CultureInfo(SystemSettings.LocaleLanguage.Replace("_", "-")));
                if (translatableText != null)
                {
                    Text = translatableText;
                    if (systemlangTextFlag == false)
                    {
                        SystemSettings.LocaleLanguageChanged += new WeakEventHandler<LocaleLanguageChangedEventArgs>(SystemSettings_LocaleLanguageChanged).Handler;
                        systemlangTextFlag = true;
                    }
                }
                else
                {
                    Text = "";
                }
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// The Text property.<br />
        /// The text to display in the UTF-8 format.<br />
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public string Text
        {
            get
            {
                return (string)GetValue(TextProperty);
            }
            set
            {
                SetValue(TextProperty, value);
                selectorData?.Text.UpdateIfNeeds(this, value);
                NotifyPropertyChangedAndRequestLayout();
            }
        }

        /// <summary>
        /// The FontFamily property.<br />
        /// The requested font family to use.<br />
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public string FontFamily
        {
            get
            {
                return (string)GetValue(FontFamilyProperty);
            }
            set
            {
                fontDirtyFlag = true;
                SetValue(FontFamilyProperty, value);
                selectorData?.FontFamily.UpdateIfNeeds(this, value);
                NotifyPropertyChangedAndRequestLayout();
            }
        }

        /// <summary>
        /// The FontStyle property.<br />
        /// The requested font style to use.<br />
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public PropertyMap FontStyle
        {
            get
            {
                return (PropertyMap)GetValue(FontStyleProperty);
            }
            set
            {
                fontDirtyFlag = true;
                SetValue(FontStyleProperty, value);
                NotifyPropertyChangedAndRequestLayout();
            }
        }

        /// <summary>
        /// The PointSize property.<br />
        /// The size of font in points.<br />
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public float PointSize
        {
            get
            {
                return (float)GetValue(PointSizeProperty);
            }
            set
            {
                fontDirtyFlag = true;
                SetValue(PointSizeProperty, value);
                selectorData?.PointSize.UpdateIfNeeds(this, value);
                NotifyPropertyChangedAndRequestLayout();
            }
        }

        /// <summary>
        /// The Font property.<br />
        /// The requested font to use.<br />
        /// Any null or None property will be retrieved from Normal state of TextLabelStyle.<br />
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        public Font Font
        {
            set
            {
                if (this.font == value)
                {
                    return;
                }

                this.font = value;
                if (this.font == null)
                {
                    // TODO: Need to restore proper font properties from a TextLabelStyle.
                    return;
                }

                string familyName = ((this.font.FamilyName == null) ? getStyleFamilyName() : this.font.FamilyName);
                float? size = null;
                FontSizeUnit sizeUnit;
                if (this.font.Size == null)
                {
                    size = getStylePointSize();
                    if (size != null)
                    {
                        sizeUnit = FontSizeUnit.Point;
                    }
                    else
                    {
                        size = getStylePixelSize();
                        sizeUnit = FontSizeUnit.Pixel;
                    }
                }
                else
                {
                    size = (float)this.font.Size;
                    sizeUnit = this.font.SizeUnit;
                }
                FontWeightType weightType = ((this.font.Weight == FontWeightType.None) ? getStyleFontWeight() : this.font.Weight);
                FontWidthType widthType = ((this.font.Width == FontWidthType.None) ? getStyleFontWidth() : this.font.Width);
                FontSlantType slantType = ((this.font.Slant == FontSlantType.None) ? getStyleFontSlant() : this.font.Slant);

                PropertyMap styleMap = new PropertyMap();
                styleMap.Add("weight", new PropertyValue(weightType.ToString()));
                styleMap.Add("width", new PropertyValue(widthType.ToString()));
                styleMap.Add("slant", new PropertyValue(slantType.ToString()));

                SetValue(FontFamilyProperty, familyName);
                selectorData?.FontFamily.UpdateIfNeeds(this, familyName);
                if (sizeUnit == FontSizeUnit.Point)
                {
                    SetValue(PointSizeProperty, size);
                    selectorData?.PointSize.UpdateIfNeeds(this, size);
                }
                else
                {
                    SetValue(PixelSizeProperty, size);
                }

                SetValue(FontStyleProperty, styleMap);
                fontDirtyFlag = true;

                NotifyPropertyChangedAndRequestLayout();
            }
            get
            {
                return this.font;
            }
        }

        /// <summary>
        /// The CurrentFont property.<br />
        /// The effective font which is being used.<br />
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        public Font CurrentFont
        {
            get
            {
                if ((this.currentFont == null) || (this.fontDirtyFlag == true))
                {
                    FontSizeUnit sizeUnit = ((this.font == null) ? FontSizeUnit.Point : this.font.SizeUnit);
                    float? size = ((sizeUnit == FontSizeUnit.Point) ? (float?)GetValue(PointSizeProperty) : (float?)GetValue(PixelSizeProperty));
                    this.currentFont = new Font((string)GetValue(FontFamilyProperty), size, sizeUnit, (PropertyMap)GetValue(FontStyleProperty));
                    this.fontDirtyFlag = false;
                }
                return this.currentFont;
            }
        }

        /// <summary>
        /// The MultiLine property.<br />
        /// The single-line or multi-line layout option.<br />
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public bool MultiLine
        {
            get
            {
                return (bool)GetValue(MultiLineProperty);
            }
            set
            {
                SetValue(MultiLineProperty, value);
                NotifyPropertyChangedAndRequestLayout();
            }
        }

        /// <summary>
        /// The HorizontalAlignment property.<br />
        /// The line horizontal alignment.<br />
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public HorizontalAlignment HorizontalAlignment
        {
            get
            {
                return (HorizontalAlignment)GetValue(HorizontalAlignmentProperty);
            }
            set
            {
                SetValue(HorizontalAlignmentProperty, value);
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// The VerticalAlignment property.<br />
        /// The line vertical alignment.<br />
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public VerticalAlignment VerticalAlignment
        {
            get
            {
                return (VerticalAlignment)GetValue(VerticalAlignmentProperty);
            }
            set
            {
                SetValue(VerticalAlignmentProperty, value);
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// The TextColor property.<br />
        /// The color of the text.<br />
        /// Animation framework can be used to change the color of the text when not using mark up.<br />
        /// Cannot animate the color when text is auto scrolling.<br />
        /// </summary>
        /// <remarks>
        /// The property cascade chaining set is possible. For example, this (textLabel.TextColor.X = 0.1f;) is possible.
        /// </remarks>
        /// <since_tizen> 3 </since_tizen>
        public Color TextColor
        {
            get
            {
                Color temp = (Color)GetValue(TextColorProperty);
                return new Color(OnTextColorChanged, temp.R, temp.G, temp.B, temp.A);
            }
            set
            {
                SetValue(TextColorProperty, value);
                selectorData?.TextColor.UpdateIfNeeds(this, value);
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// The ShadowOffset property.<br />
        /// The drop shadow offset 0 indicates no shadow.<br />
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        /// <remarks>
        /// Deprecated.(API Level 6) Use Shadow instead.
        /// The property cascade chaining set is possible. For example, this (textLabel.ShadowOffset.X = 0.1f;) is possible.
        /// </remarks>
        [Obsolete("Please do not use this ShadowOffset(Deprecated). Please use Shadow instead.")]
        public Vector2 ShadowOffset
        {
            get
            {
                Vector2 shadowOffset = new Vector2();
                Shadow.Find(TextLabel.Property.SHADOW, "offset")?.Get(shadowOffset);
                return new Vector2(OnShadowOffsetChanged, shadowOffset.X, shadowOffset.Y);
            }
            set
            {
                PropertyMap temp = new PropertyMap();
                temp.Insert("offset", new PropertyValue(value));

                PropertyMap shadowMap = Shadow;
                shadowMap.Merge(temp);

                SetValue(ShadowProperty, shadowMap);
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// The ShadowColor property.<br />
        /// The color of a drop shadow.<br />
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        /// <remarks>
        /// Deprecated.(API Level 6) Use Shadow instead.
        /// The property cascade chaining set is possible. For example, this (textLabel.ShadowColor.X = 0.1f;) is possible.
        /// </remarks>
        [Obsolete("Please do not use this ShadowColor(Deprecated). Please use Shadow instead.")]
        public Vector4 ShadowColor
        {
            get
            {
                Vector4 shadowColor = new Vector4();
                Shadow.Find(TextLabel.Property.SHADOW, "color")?.Get(shadowColor);
                return new Vector4(OnShadowColorChanged, shadowColor.X, shadowColor.Y, shadowColor.Z, shadowColor.W);
            }
            set
            {
                PropertyMap temp = new PropertyMap();
                temp.Insert("color", new PropertyValue(value));

                PropertyMap shadowMap = Shadow;
                shadowMap.Merge(temp);

                SetValue(ShadowProperty, shadowMap);
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// The UnderlineEnabled property.<br />
        /// The underline enabled flag.<br />
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        /// <remarks>
        /// Deprecated.(API Level 6) Use Underline instead.
        /// </remarks>
        [Obsolete("Please do not use this UnderlineEnabled(Deprecated). Please use Underline instead.")]
        public bool UnderlineEnabled
        {
            get
            {
                bool underlineEnabled = false;
                Underline.Find(TextLabel.Property.UNDERLINE, "enable")?.Get(out underlineEnabled);
                return underlineEnabled;
            }
            set
            {
                PropertyMap temp = new PropertyMap();
                temp.Add("enable", new PropertyValue(value));

                PropertyMap undelineMap = Underline;
                undelineMap.Merge(temp);

                SetValue(UnderlineProperty, undelineMap);
                NotifyPropertyChanged();

            }
        }

        /// <summary>
        /// The UnderlineColor property.<br />
        /// Overrides the underline height from font metrics.<br />
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        /// <remarks>
        /// Deprecated.(API Level 6) Use Underline instead.
        /// The property cascade chaining set is possible. For example, this (textLabel.UnderlineColor.X = 0.1f;) is possible.
        /// </remarks>
        [Obsolete("Please do not use this UnderlineColor(Deprecated). Please use Underline instead.")]
        public Vector4 UnderlineColor
        {
            get
            {
                Vector4 underlineColor = new Vector4();
                Underline.Find(TextLabel.Property.UNDERLINE, "color")?.Get(underlineColor);
                return new Vector4(OnUnderlineColorChanged, underlineColor.X, underlineColor.Y, underlineColor.Z, underlineColor.W);
            }
            set
            {
                PropertyMap temp = new PropertyMap();
                temp.Insert("color", new PropertyValue(value));

                PropertyMap undelineMap = Underline;
                undelineMap.Merge(temp);

                SetValue(UnderlineProperty, undelineMap);
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// The UnderlineHeight property.<br />
        /// Overrides the underline height from font metrics.<br />
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        /// <remarks>
        /// Deprecated.(API Level 6) Use Underline instead.
        /// </remarks>
        [Obsolete("Please do not use this UnderlineHeight(Deprecated). Please use Underline instead.")]
        public float UnderlineHeight
        {
            get
            {
                float underlineHeight = 0.0f;
                Underline.Find(TextLabel.Property.UNDERLINE, "height")?.Get(out underlineHeight);
                return underlineHeight;
            }
            set
            {
                PropertyMap temp = new PropertyMap();
                temp.Insert("height", new PropertyValue(value));

                PropertyMap undelineMap = Underline;
                undelineMap.Merge(temp);

                SetValue(UnderlineProperty, undelineMap);
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// The EnableMarkup property.<br />
        /// Whether the mark-up processing is enabled.<br />
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public bool EnableMarkup
        {
            get
            {
                return (bool)GetValue(EnableMarkupProperty);
            }
            set
            {
                SetValue(EnableMarkupProperty, value);
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// The EnableAutoScroll property.<br />
        /// Starts or stops auto scrolling.<br />
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public bool EnableAutoScroll
        {
            get
            {
                return (bool)GetValue(EnableAutoScrollProperty);
            }
            set
            {
                SetValue(EnableAutoScrollProperty, value);
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// The AutoScrollSpeed property.<br />
        /// Sets the speed of scrolling in pixels per second.<br />
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public int AutoScrollSpeed
        {
            get
            {
                return (int)GetValue(AutoScrollSpeedProperty);
            }
            set
            {
                SetValue(AutoScrollSpeedProperty, value);
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// The AutoScrollLoopCount property.<br />
        /// Number of complete loops when scrolling enabled.<br />
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public int AutoScrollLoopCount
        {
            get
            {
                return (int)GetValue(AutoScrollLoopCountProperty);
            }
            set
            {
                SetValue(AutoScrollLoopCountProperty, value);
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// The AutoScrollGap property.<br />
        /// Gap before scrolling wraps.<br />
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public float AutoScrollGap
        {
            get
            {
                return (float)GetValue(AutoScrollGapProperty);
            }
            set
            {
                SetValue(AutoScrollGapProperty, value);
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// The LineSpacing property.<br />
        /// The default extra space between lines in points.<br />
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public float LineSpacing
        {
            get
            {
                return (float)GetValue(LineSpacingProperty);
            }
            set
            {
                SetValue(LineSpacingProperty, value);
                NotifyPropertyChangedAndRequestLayout();
            }
        }

        /// <summary>
        /// The Underline property.<br />
        /// The default underline parameters.<br />
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public PropertyMap Underline
        {
            get
            {
                return (PropertyMap)GetValue(UnderlineProperty);
            }
            set
            {
                SetValue(UnderlineProperty, value);
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// The Shadow property.<br />
        /// The default shadow parameters.<br />
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public PropertyMap Shadow
        {
            get
            {
                return (PropertyMap)GetValue(ShadowProperty);
            }
            set
            {
                SetValue(ShadowProperty, value);
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Describes a text shadow for a TextLabel.
        /// It is null by default.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public TextShadow TextShadow
        {
            get
            {
                return (TextShadow)GetValue(TextShadowProperty);
            }
            set
            {
                SetValue(TextShadowProperty, value);
                selectorData?.TextShadow.UpdateIfNeeds(this, value);
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// The Emboss property.<br />
        /// The default emboss parameters.<br />
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public string Emboss
        {
            get
            {
                return (string)GetValue(EmbossProperty);
            }
            set
            {
                SetValue(EmbossProperty, value);
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// The Outline property.<br />
        /// The default outline parameters.<br />
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public PropertyMap Outline
        {
            get
            {
                return (PropertyMap)GetValue(OutlineProperty);
            }
            set
            {
                SetValue(OutlineProperty, value);
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// The PixelSize property.<br />
        /// The size of font in pixels.<br />
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public float PixelSize
        {
            get
            {
                return (float)GetValue(PixelSizeProperty);
            }
            set
            {
                fontDirtyFlag = true;
                SetValue(PixelSizeProperty, value);
                NotifyPropertyChangedAndRequestLayout();
            }
        }

        /// <summary>
        /// The Ellipsis property.<br />
        /// Enable or disable the ellipsis.<br />
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public bool Ellipsis
        {
            get
            {
                return (bool)GetValue(EllipsisProperty);
            }
            set
            {
                SetValue(EllipsisProperty, value);
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// The AutoScrollLoopDelay property.<br />
        /// Do something.<br />
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public float AutoScrollLoopDelay
        {
            get
            {
                return (float)GetValue(AutoScrollLoopDelayProperty);
            }
            set
            {
                SetValue(AutoScrollLoopDelayProperty, value);
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// The AutoScrollStopMode property.<br />
        /// Do something.<br />
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public AutoScrollStopMode AutoScrollStopMode
        {
            get
            {
                return (AutoScrollStopMode)GetValue(AutoScrollStopModeProperty);
            }
            set
            {
                SetValue(AutoScrollStopModeProperty, value);
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// The line count of the text.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public int LineCount
        {
            get
            {
                int temp = 0;
                GetProperty(TextLabel.Property.LINE_COUNT).Get(out temp);
                return temp;
            }
        }

        /// <summary>
        /// The LineWrapMode property.<br />
        /// line wrap mode when the text lines over layout width.<br />
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public LineWrapMode LineWrapMode
        {
            get
            {
                return (LineWrapMode)GetValue(LineWrapModeProperty);
            }
            set
            {
                SetValue(LineWrapModeProperty, value);
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// The direction of the text such as left to right or right to left.
        /// </summary>
        /// <since_tizen> 5 </since_tizen>
        /// This will be released at Tizen.NET API Level 5, so currently this would be used as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public TextDirection TextDirection
        {
            get
            {
                int temp = 0;
                GetProperty(TextLabel.Property.TEXT_DIRECTION).Get(out temp);
                return (TextDirection)temp;
            }
        }

        /// <summary>
        /// The vertical line alignment of the text.
        /// </summary>
        /// <since_tizen> 5 </since_tizen>
        /// This will be released at Tizen.NET API Level 5, so currently this would be used as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public VerticalLineAlignment VerticalLineAlignment
        {
            get
            {
                return (VerticalLineAlignment)GetValue(VerticalLineAlignmentProperty);
            }
            set
            {
                SetValue(VerticalLineAlignmentProperty, value);
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// The text alignment to match the direction of the system language.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        public bool MatchSystemLanguageDirection
        {
            get
            {
                return (bool)GetValue(MatchSystemLanguageDirectionProperty);
            }
            set
            {
                SetValue(MatchSystemLanguageDirectionProperty, value);
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// The text fit parameters.<br />
        /// The textFit map contains the following keys :<br />
        /// - enable (bool type) : True to enable the text fit or false to disable(the default value is false)<br />
        /// - minSize (float type) : Minimum Size for text fit(the default value is 10.f)<br />
        /// - maxSize (float type) : Maximum Size for text fit(the default value is 100.f)<br />
        /// - stepSize (float type) : Step Size for font increase(the default value is 1.f)<br />
        /// - fontSize (string type) : The size type of font, You can choose between "pointSize" or "pixelSize". (the default value is "pointSize")<br />
        /// </summary>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public PropertyMap TextFit
        {
            get
            {
                return (PropertyMap)GetValue(TextFitProperty);
            }
            set
            {
                fontDirtyFlag = true;
                SetValue(TextFitProperty, value);
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// The MinLineSize property.<br />
        /// </summary>
        /// <since_tizen> 8 </since_tizen>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public float MinLineSize
        {
            get
            {
                return (float)GetValue(MinLineSizeProperty);
            }
            set
            {
                SetValue(MinLineSizeProperty, value);
                NotifyPropertyChangedAndRequestLayout();
            }
        }

        private TextLabelSelectorData SelectorData
        {
            get
            {
                if (selectorData == null)
                {
                    selectorData = new TextLabelSelectorData();
                }
                return selectorData;
            }
        }

        /// <summary>
        /// Downcasts a handle to textLabel handle
        /// </summary>
        /// <param name="handle"></param>
        /// <returns></returns>
        /// <since_tizen> 3 </since_tizen>
        /// Please do not use! this will be deprecated!
        /// Instead please use as keyword.
        [Obsolete("Please do not use! This will be deprecated! Please use as keyword instead! " +
            "Like: " +
            "BaseHandle handle = new TextLabel(\"Hello World!\"); " +
            "TextLabel label = handle as TextLabel")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static TextLabel DownCast(BaseHandle handle)
        {
            TextLabel ret = Registry.GetManagedBaseHandleFromNativePtr(handle) as TextLabel;

            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            return ret;
        }

        /// <inheritdoc/>
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected override void Dispose(DisposeTypes type)
        {
            if (disposed)
            {
                return;
            }

            if (type == DisposeTypes.Explicit)
            {
                //Called by User
                //Release your own managed resources here.
                //You should release all of your own disposable objects here.
                selectorData?.Reset(this);
            }

            base.Dispose(type);
        }

        internal static global::System.Runtime.InteropServices.HandleRef getCPtr(TextLabel obj)
        {
            return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
        }

        /// This will not be public opened.
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected override void ReleaseSwigCPtr(System.Runtime.InteropServices.HandleRef swigCPtr)
        {
            Interop.TextLabel.delete_TextLabel(swigCPtr);
        }

        /// <summary>
        /// Get attribues, it is abstract function and must be override.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        /// This will be public opened in tizen_6.0 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected override ViewStyle GetViewStyle()
        {
            return new TextLabelStyle();
        }

        /// <summary>
        /// Invoked whenever the binding context of the textlabel changes. Implement this method to add class handling for this event.
        /// </summary>
        protected override void OnBindingContextChanged()
        {
            fontDirtyFlag = true;
            base.OnBindingContextChanged();
        }

        private void SystemSettings_LocaleLanguageChanged(object sender, LocaleLanguageChangedEventArgs e)
        {
            Text = NUIApplication.MultilingualResourceManager?.GetString(textLabelSid, new CultureInfo(e.Value.Replace("_", "-")));
        }

        private void  NotifyPropertyChangedAndRequestLayout()
        {
            NotifyPropertyChanged();
            Layout?.RequestLayout();
        }

        internal new class Property
        {
            internal static readonly int TEXT = Interop.TextLabel.TextLabel_Property_TEXT_get();
            internal static readonly int FONT_FAMILY = Interop.TextLabel.TextLabel_Property_FONT_FAMILY_get();
            internal static readonly int FONT_STYLE = Interop.TextLabel.TextLabel_Property_FONT_STYLE_get();
            internal static readonly int POINT_SIZE = Interop.TextLabel.TextLabel_Property_POINT_SIZE_get();
            internal static readonly int MULTI_LINE = Interop.TextLabel.TextLabel_Property_MULTI_LINE_get();
            internal static readonly int HORIZONTAL_ALIGNMENT = Interop.TextLabel.TextLabel_Property_HORIZONTAL_ALIGNMENT_get();
            internal static readonly int VERTICAL_ALIGNMENT = Interop.TextLabel.TextLabel_Property_VERTICAL_ALIGNMENT_get();
            internal static readonly int TEXT_COLOR = Interop.TextLabel.TextLabel_Property_TEXT_COLOR_get();
            internal static readonly int ENABLE_MARKUP = Interop.TextLabel.TextLabel_Property_ENABLE_MARKUP_get();
            internal static readonly int ENABLE_AUTO_SCROLL = Interop.TextLabel.TextLabel_Property_ENABLE_AUTO_SCROLL_get();
            internal static readonly int AUTO_SCROLL_SPEED = Interop.TextLabel.TextLabel_Property_AUTO_SCROLL_SPEED_get();
            internal static readonly int AUTO_SCROLL_LOOP_COUNT = Interop.TextLabel.TextLabel_Property_AUTO_SCROLL_LOOP_COUNT_get();
            internal static readonly int AUTO_SCROLL_GAP = Interop.TextLabel.TextLabel_Property_AUTO_SCROLL_GAP_get();
            internal static readonly int LINE_SPACING = Interop.TextLabel.TextLabel_Property_LINE_SPACING_get();
            internal static readonly int UNDERLINE = Interop.TextLabel.TextLabel_Property_UNDERLINE_get();
            internal static readonly int SHADOW = Interop.TextLabel.TextLabel_Property_SHADOW_get();
            internal static readonly int EMBOSS = Interop.TextLabel.TextLabel_Property_EMBOSS_get();
            internal static readonly int OUTLINE = Interop.TextLabel.TextLabel_Property_OUTLINE_get();
            internal static readonly int PIXEL_SIZE = Interop.TextLabel.TextLabel_Property_PIXEL_SIZE_get();
            internal static readonly int ELLIPSIS = Interop.TextLabel.TextLabel_Property_ELLIPSIS_get();
            internal static readonly int AUTO_SCROLL_STOP_MODE = Interop.TextLabel.TextLabel_Property_AUTO_SCROLL_STOP_MODE_get();
            internal static readonly int AUTO_SCROLL_LOOP_DELAY = Interop.TextLabel.TextLabel_Property_AUTO_SCROLL_LOOP_DELAY_get();
            internal static readonly int LINE_COUNT = Interop.TextLabel.TextLabel_Property_LINE_COUNT_get();
            internal static readonly int LINE_WRAP_MODE = Interop.TextLabel.TextLabel_Property_LINE_WRAP_MODE_get();
            internal static readonly int TEXT_DIRECTION = Interop.TextLabel.TextLabel_Property_TEXT_DIRECTION_get();
            internal static readonly int VERTICAL_LINE_ALIGNMENT = Interop.TextLabel.TextLabel_Property_VERTICAL_LINE_ALIGNMENT_get();
            internal static readonly int MATCH_SYSTEM_LANGUAGE_DIRECTION = Interop.TextLabel.TextLabel_Property_MATCH_SYSTEM_LANGUAGE_DIRECTION_get();
            internal static readonly int TEXT_FIT = Interop.TextLabel.TextLabel_Property_TEXT_FIT_get();
            internal static readonly int MIN_LINE_SIZE = Interop.TextLabel.TextLabel_Property_MIN_LINE_SIZE_get();
        }

        private void OnShadowColorChanged(float x, float y, float z, float w)
        {
            ShadowColor = new Vector4(x, y, z, w);
        }
        private void OnShadowOffsetChanged(float x, float y)
        {
            ShadowOffset = new Vector2(x, y);
        }
        private void OnTextColorChanged(float r, float g, float b, float a)
        {
            TextColor = new Color(r, g, b, a);
        }
        private void OnUnderlineColorChanged(float x, float y, float z, float w)
        {
            UnderlineColor = new Vector4(x, y, z, w);
        }
    }
}
