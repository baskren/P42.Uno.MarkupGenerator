using System;
using Windows.UI;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;

namespace P42.Uno.Markup;

public static class ColorExtensions
{
    public static Color HighContrastCompanionColor(this Color color)
    {
        var yiq = (color.R * 299 + color.G * 587 + color.B * 114) / 1000;
        return yiq < 128 ? Colors.White : Colors.Black;
    }


    #region Tranlators
    public static Color AsWinUiColor(this System.Drawing.Color color)
        => new()
        {
            A = color.A,
            R = color.R,
            G = color.G,
            B = color.B
        };

    public static System.Drawing.Color ToSystemDrawingColor(this Color color)
        => System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);

    public static SolidColorBrush ToBrush(this System.Drawing.Color color) => new(color.AsWinUiColor());

    public static SolidColorBrush ToBrush(this Color color) => new(color);

    public static Brush ToBrush(this Brush brush)
        => brush;
    #endregion


    #region Modifiers
    /// <summary>
    /// Interpolates between two colors - keeping the Alpha of the first (unless it's transparent ... then its white with alpha 0);
    /// </summary>
    /// <returns>The blend.</returns>
    /// <param name="c">C.</param>
    /// <param name="c2">C2.</param>
    /// <param name="percent">Percent.</param>
    public static Color RgbHybridBlend(this Color c, Color c2, double percent)
    {
        var c1 = new Color { R = c.R, G = c.G, B = c.B, A = c.A };
        if (c1 == Colors.Transparent)
            c1 = new Color { R = 255, G = 255, B = 255, A = 0 };
        var A = c1.A;
        var R = (byte)(c1.R + (c2.R - c1.R) * percent).Clamp(0, 255);
        var G = (byte)(c1.G + (c2.G - c1.G) * percent).Clamp(0, 255);
        var B = (byte)(c1.B + (c2.B - c1.B) * percent).Clamp(0, 255);
        return new Color { R = R, G = G, B = B, A = A };
    }

    /// <summary>
    /// Interpolates between two colors
    /// </summary>
    /// <param name="c1"></param>
    /// <param name="c2"></param>
    /// <param name="percent"></param>
    /// <returns></returns>
    public static Color RgbaBlend(this Color c1, Color c2, double percent)
    {
        var A = (byte)(c1.A + (c2.A - c1.A) * percent).Clamp(0, 255);
        var R = (byte)(c1.R + (c2.R - c1.R) * percent).Clamp(0, 255);
        var G = (byte)(c1.G + (c2.G - c1.G) * percent).Clamp(0, 255);
        var B = (byte)(c1.B + (c2.B - c1.B) * percent).Clamp(0, 255);
        return new Color { R = R, G = G, B = B, A = A };
    }

    /// <summary>
    /// Withs the alpha.
    /// </summary>
    /// <returns>The alpha.</returns>
    /// <param name="c">C.</param>
    /// <param name="alpha">Alpha.</param>
    public static Color WithAlpha(this Color c, double alpha)
        => c.WithAlpha((byte)(alpha * 255));

    public static Color WithAlpha(this Color c, byte alpha) => new() { R = c.R, G = c.G, B = c.B, A = byte.Clamp(alpha, 0, 255) };


    public static Color AssureGesturable(this Color c) => new() { R = c.R, G = c.G, B = c.B, A = Math.Max((byte)0x1, c.A) };

    public static Brush AssureGesturable(this Brush b)
    {
        if (b is SolidColorBrush scb)
        {
            var color = scb.Color;
            return new SolidColorBrush(color.AssureGesturable());
        }

        if (b is null)
            return new SolidColorBrush(Colors.Gray.WithAlpha(0x1));

        return b;
    }

    public static Color AsGesterableEnabled(this Color c, bool isEnabled)
        => new() { R = c.R, G = c.G, B = c.B, A = Math.Max((byte)0x1, isEnabled ? c.A : (byte)(c.A>>1) ) };

    public static Brush AsGesterableEnabled(this Brush b, bool isEnabled)
    {
        if (b is SolidColorBrush scb)
        {
            var color = scb.Color;
            return new SolidColorBrush(color.AsGesterableEnabled(isEnabled));
        }

        if (b is null && isEnabled)
            return new SolidColorBrush(Colors.Gray.WithAlpha(0x1));

        return b;
    }
    #endregion


    #region Factory Methods

    #region RGB
    public static Color ColorFromRgb(byte r, byte g, byte b)
        => ColorFromRgba(r, g, b);

    public static Color ColorFromRgb(int r, int g, int b)
        => ColorFromRgba(r, g, b);

    public static Color ColorFromRgb(double r, double g, double b)
        => ColorFromRgba(r, g, b);
    #endregion

    #region RGBA
    public static Color ColorFromRgba(byte r, byte g, byte b, byte a = 0xFF) => new() { R = r, G = g, B = b, A = a };

    public static Color ColorFromRgba(int r, int g, int b, int a = 255)
        => new() { R = (byte)r.Clamp(0, 255), G = (byte)g.Clamp(0, 255), B = (byte)b.Clamp(0, 255), A = (byte)a.Clamp(0, 255) };

    public static Color ColorFromRgba(double r, double g, double b, double a = 1.0)
        => ColorFromArgb(a, r, g, b);
    #endregion

    #region ARGB
    public static Color ColorFromArgb(byte a, byte r, byte g, byte b) => new() { R = r, G = g, B = b, A = a };

    public static Color ColorFromArgb(int a, int r, int g, int b)
        => new() { R = (byte)r.Clamp(0, 255), G = (byte)g.Clamp(0, 255), B = (byte)b.Clamp(0, 255), A = (byte)a.Clamp(0, 255) };

    public static Color ColorFromArgb(double a, double r, double g, double b)
    {
        var A = (byte)(a * 255).Clamp(0, 255);
        var R = (byte)(r * 255).Clamp(0, 255);
        var G = (byte)(g * 255).Clamp(0, 255);
        var B = (byte)(b * 255).Clamp(0, 255);
        return new Color { R = R, G = G, B = B, A = A };
    }
    #endregion

    #region Hue Saturation 
    public static Color ColorFromHsva(double h, double s, double v, double a)
    {
        h = h.Clamp(0, 1);
        s = s.Clamp(0, 1);
        v = v.Clamp(0, 1);
        var range = (int)Math.Floor(h * 6) % 6;
        var f = h * 6 - Math.Floor(h * 6);
        var p = v * (1 - s);
        var q = v * (1 - f * s);
        var t = v * (1 - (1 - f) * s);

        switch (range)
        {
            case 0:
                return ColorFromRgba(v, t, p, a);
            case 1:
                return ColorFromRgba(q, v, p, a);
            case 2:
                return ColorFromRgba(p, v, t, a);
            case 3:
                return ColorFromRgba(p, q, v, a);
            case 4:
                return ColorFromRgba(t, p, v, a);
        }
        return ColorFromRgba(v, p, q, a);
    }

    public static Color ColorFromHsla(double hue, double saturation, double luminosity, double a = 1.0)
    {

        hue = Math.Min(Math.Max(0, hue), 1.0);
        saturation = Math.Min(Math.Max(0, saturation), 1.0);
        luminosity = Math.Min(Math.Max(0, luminosity), 1.0);

        if (luminosity == 0 || saturation == 0)
            return default;

        var temp2 = luminosity <= 0.5f ? luminosity * (1.0f + saturation) : luminosity + saturation - luminosity * saturation;
        var temp1 = 2.0 * luminosity - temp2;

        var t3 = new[] { hue + 1.0f / 3.0f, hue, hue - 1.0f / 3.0f };
        var clr = new double[] { 0, 0, 0 };
        for (var i = 0; i < 3; i++)
        {
            if (t3[i] < 0)
                t3[i] += 1.0f;
            if (t3[i] > 1)
                t3[i] -= 1.0f;
            if (6.0 * t3[i] < 1.0)
                clr[i] = temp1 + (temp2 - temp1) * t3[i] * 6.0f;
            else if (2.0 * t3[i] < 1.0)
                clr[i] = temp2;
            else if (3.0 * t3[i] < 2.0)
                clr[i] = temp1 + (temp2 - temp1) * (2.0f / 3.0f - t3[i]) * 6.0f;
            else
                clr[i] = temp1;
        }
        return ColorFromArgb(a, clr[0], clr[1], clr[2]);
    }
    #endregion

    #region UINT
    public static Color ColorFromUint(uint argb)
        => global::Microsoft.UI.ColorHelper.FromArgb((byte)(argb >> 24), (byte)(argb >> 16), (byte)(argb >> 8), (byte)argb);
    #endregion

    #region string
    public static Color ColorFromString(string colorString)
        => (global::Windows.UI.Color)global::Microsoft.UI.Xaml.Markup.XamlBindingHelper.ConvertValue(typeof(global::Windows.UI.Color), colorString);
    /*
{
    // Undefined
    if (hex.Length < 3)
        return default(Color);
    var idx = hex[0] == '#' ? 1 : 0;

    switch (hex.Length - idx)
    {
        case 3: //#rgb => ffrrggbb
            var t1 = ToHexD(hex[idx++]);
            var t2 = ToHexD(hex[idx++]);
            var t3 = ToHexD(hex[idx]);

            return ColorFromRgb((int)t1, (int)t2, (int)t3);

        case 4: //#argb => aarrggbb
            var f1 = ToHexD(hex[idx++]);
            var f2 = ToHexD(hex[idx++]);
            var f3 = ToHexD(hex[idx++]);
            var f4 = ToHexD(hex[idx]);
            return ColorFromRgba((int)f2, (int)f3, (int)f4, (int)f1);

        case 6: //#rrggbb => ffrrggbb
            return ColorFromRgb((int)(ToHex(hex[idx++]) << 4 | ToHex(hex[idx++])),
                (int)(ToHex(hex[idx++]) << 4 | ToHex(hex[idx++])),
                (int)(ToHex(hex[idx++]) << 4 | ToHex(hex[idx])));

        case 8: //#aarrggbb
            var a1 = ToHex(hex[idx++]) << 4 | ToHex(hex[idx++]);
            return ColorFromRgba((int)(ToHex(hex[idx++]) << 4 | ToHex(hex[idx++])),
                (int)(ToHex(hex[idx++]) << 4 | ToHex(hex[idx++])),
                (int)(ToHex(hex[idx++]) << 4 | ToHex(hex[idx])),
                (int)a1);

        default: //everything else will result in unexpected results
            return default(Color);
    }
}
    /// <summary>
    /// Takes a color string, typical of an HTML tag's color attribute, and coverts it to a Windows.UI color.
    /// </summary>
    /// <returns>The color.</returns>
    /// <param name="s">the color string</param>
    public static Color ColorFromString(this string s)        
    {
        if (s.ToLower().StartsWith("rgb(", StringComparison.OrdinalIgnoreCase))
        {
            //var values = s.Substring(4, s.Length - 5).Split(',').Select(int.Parse).ToArray();
            var components = s[4..^1].Split(',');
            if (components.Length != 3)
                throw new FormatException($"Could not parse [{s}] into RGB integer components");
            var r = byte.Parse(components[0]);
            var g = byte.Parse(components[1]);
            var b = byte.Parse(components[2]);
            return Color.FromArgb(1, r, g, b);
        }
        if (s.ToLower().StartsWith("rgba(", StringComparison.OrdinalIgnoreCase))
        {
            //var values = s.Substring(5, s.Length - 6).Split(',').Select(int.Parse).ToArray();
            var components = s[5..^1].Split(',');
            if (components.Length != 4)
                throw new FormatException($"Could not parse [{s}] into RGBA integer components");
            var r = byte.Parse(components[0]);
            var g = byte.Parse(components[1]);
            var b = byte.Parse(components[2]);
            var a = byte.Parse(components[3]);
            return Color.FromArgb(a, r, g, b);
        }
        if (s.StartsWith("#", StringComparison.OrdinalIgnoreCase))
        {
            var color = ColorFromHex(s);
            return color;
        }
        var colorName = s.ToLower();
        return colorName switch
        {
            "aliceblue" => ColorFromHex("F0F8FF"),
            "antiquewhite" => ColorFromHex("FAEBD7"),
            "aqua" => ColorFromHex("00FFFF"),
            "aquamarine" => ColorFromHex("7FFFD4"),
            "azure" => ColorFromHex("F0FFFF"),
            "beige" => ColorFromHex("F5F5DC"),
            "bisque" => ColorFromHex("FFE4C4"),
            "black" => ColorFromHex("000000"),
            "blanchedalmond" => ColorFromHex("FFEBCD"),
            "blue" => ColorFromHex("0000FF"),
            "blueviolet" => ColorFromHex("8A2BE2"),
            "brown" => ColorFromHex("A52A2A"),
            "burlywood" => ColorFromHex("DEB887"),
            "cadetblue" => ColorFromHex("5F9EA0"),
            "chartreuse" => ColorFromHex("7FFF00"),
            "chocolate" => ColorFromHex("D2691E"),
            "coral" => ColorFromHex("FF7F50"),
            "cornflowerblue" => ColorFromHex("6495ED"),
            "cornsilk" => ColorFromHex("FFF8DC"),
            "crimson" => ColorFromHex("DC143C"),
            "cyan" => ColorFromHex("00FFFF"),
            "darkblue" => ColorFromHex("00008B"),
            "darkcyan" => ColorFromHex("008B8B"),
            "darkgoldenrod" => ColorFromHex("B8860B"),
            "darkgray" => ColorFromHex("A9A9A9"),
            "darkgrey" => ColorFromHex("A9A9A9"),
            "darkgreen" => ColorFromHex("006400"),
            "darkkhaki" => ColorFromHex("BDB76B"),
            "darkmagenta" => ColorFromHex("8B008B"),
            "darkolivegreen" => ColorFromHex("556B2F"),
            "darkorange" => ColorFromHex("FF8C00"),
            "darkorchid" => ColorFromHex("9932CC"),
            "darkred" => ColorFromHex("8B0000"),
            "darksalmon" => ColorFromHex("E9967A"),
            "darkseagreen" => ColorFromHex("8FBC8F"),
            "darkslateblue" => ColorFromHex("483D8B"),
            "darkslategray" => ColorFromHex("2F4F4F"),
            "darkslategrey" => ColorFromHex("2F4F4F"),
            "darkturquoise" => ColorFromHex("00CED1"),
            "darkviolet" => ColorFromHex("9400D3"),
            "deeppink" => ColorFromHex("FF1493"),
            "deepskyblue" => ColorFromHex("00BFFF"),
            "dimgray" => ColorFromHex("696969"),
            "dimgrey" => ColorFromHex("696969"),
            "dodgerblue" => ColorFromHex("1E90FF"),
            "firebrick" => ColorFromHex("B22222"),
            "floralwhite" => ColorFromHex("FFFAF0"),
            "forestgreen" => ColorFromHex("228B22"),
            "fuchsia" => ColorFromHex("FF00FF"),
            "gainsboro" => ColorFromHex("DCDCDC"),
            "ghostwhite" => ColorFromHex("F8F8FF"),
            "gold" => ColorFromHex("FFD700"),
            "goldenrod" => ColorFromHex("DAA520"),
            "gray" => ColorFromHex("808080"),
            "grey" => ColorFromHex("808080"),
            "green" => ColorFromHex("008000"),
            "greenyellow" => ColorFromHex("ADFF2F"),
            "honeydew" => ColorFromHex("F0FFF0"),
            "hotpink" => ColorFromHex("FF69B4"),
            "indianred " => ColorFromHex("CD5C5C"),
            "indigo " => ColorFromHex("4B0082"),
            "ivory" => ColorFromHex("FFFFF0"),
            "khaki" => ColorFromHex("F0E68C"),
            "lavender" => ColorFromHex("E6E6FA"),
            "lavenderblush" => ColorFromHex("FFF0F5"),
            "lawngreen" => ColorFromHex("7CFC00"),
            "lemonchiffon" => ColorFromHex("FFFACD"),
            "lightblue" => ColorFromHex("ADD8E6"),
            "lightcoral" => ColorFromHex("F08080"),
            "lightcyan" => ColorFromHex("E0FFFF"),
            "lightgoldenrodyellow" => ColorFromHex("FAFAD2"),
            "lightgray" => ColorFromHex("D3D3D3"),
            "lightgrey" => ColorFromHex("D3D3D3"),
            "lightgreen" => ColorFromHex("90EE90"),
            "lightpink" => ColorFromHex("FFB6C1"),
            "lightsalmon" => ColorFromHex("FFA07A"),
            "lightseagreen" => ColorFromHex("20B2AA"),
            "lightskyblue" => ColorFromHex("87CEFA"),
            "lightslategray" => ColorFromHex("778899"),
            "lightslategrey" => ColorFromHex("778899"),
            "lightsteelblue" => ColorFromHex("B0C4DE"),
            "lightyellow" => ColorFromHex("FFFFE0"),
            "lime" => ColorFromHex("00FF00"),
            "limegreen" => ColorFromHex("32CD32"),
            "linen" => ColorFromHex("FAF0E6"),
            "magenta" => ColorFromHex("FF00FF"),
            "maroon" => ColorFromHex("800000"),
            "mediumaquamarine" => ColorFromHex("66CDAA"),
            "mediumblue" => ColorFromHex("0000CD"),
            "mediumorchid" => ColorFromHex("BA55D3"),
            "mediumpurple" => ColorFromHex("9370DB"),
            "mediumseagreen" => ColorFromHex("3CB371"),
            "mediumslateblue" => ColorFromHex("7B68EE"),
            "mediumspringgreen" => ColorFromHex("00FA9A"),
            "mediumturquoise" => ColorFromHex("48D1CC"),
            "mediumvioletred" => ColorFromHex("C71585"),
            "midnightblue" => ColorFromHex("191970"),
            "mintcream" => ColorFromHex("F5FFFA"),
            "mistyrose" => ColorFromHex("FFE4E1"),
            "moccasin" => ColorFromHex("FFE4B5"),
            "navajowhite" => ColorFromHex("FFDEAD"),
            "navy" => ColorFromHex("000080"),
            "oldlace" => ColorFromHex("FDF5E6"),
            "olive" => ColorFromHex("808000"),
            "olivedrab" => ColorFromHex("6B8E23"),
            "orange" => ColorFromHex("FFA500"),
            "orangered" => ColorFromHex("FF4500"),
            "orchid" => ColorFromHex("DA70D6"),
            "palegoldenrod" => ColorFromHex("EEE8AA"),
            "palegreen" => ColorFromHex("98FB98"),
            "paleturquoise" => ColorFromHex("AFEEEE"),
            "palevioletred" => ColorFromHex("DB7093"),
            "papayawhip" => ColorFromHex("FFEFD5"),
            "peachpuff" => ColorFromHex("FFDAB9"),
            "peru" => ColorFromHex("CD853F"),
            "pink" => ColorFromHex("FFC0CB"),
            "plum" => ColorFromHex("DDA0DD"),
            "powderblue" => ColorFromHex("B0E0E6"),
            "purple" => ColorFromHex("800080"),
            "rebeccapurple" => ColorFromHex("663399"),
            "red" => ColorFromHex("FF0000"),
            "rosybrown" => ColorFromHex("BC8F8F"),
            "royalblue" => ColorFromHex("4169E1"),
            "saddlebrown" => ColorFromHex("8B4513"),
            "salmon" => ColorFromHex("FA8072"),
            "sandybrown" => ColorFromHex("F4A460"),
            "seagreen" => ColorFromHex("2E8B57"),
            "seashell" => ColorFromHex("FFF5EE"),
            "sienna" => ColorFromHex("A0522D"),
            "silver" => ColorFromHex("C0C0C0"),
            "skyblue" => ColorFromHex("87CEEB"),
            "slateblue" => ColorFromHex("6A5ACD"),
            "slategray" => ColorFromHex("708090"),
            "slategrey" => ColorFromHex("708090"),
            "snow" => ColorFromHex("FFFAFA"),
            "springgreen" => ColorFromHex("00FF7F"),
            "steelblue" => ColorFromHex("4682B4"),
            "tan" => ColorFromHex("D2B48C"),
            "teal" => ColorFromHex("008080"),
            "thistle" => ColorFromHex("D8BFD8"),
            "tomato" => ColorFromHex("FF6347"),
            "turquoise" => ColorFromHex("40E0D0"),
            "violet" => ColorFromHex("EE82EE"),
            "wheat" => ColorFromHex("F5DEB3"),
            "white" => ColorFromHex("FFFFFF"),
            "whitesmoke" => ColorFromHex("F5F5F5"),
            "yellow" => ColorFromHex("FFFF00"),
            "yellowgreen" => ColorFromHex("9ACD32"),
            _ => default(Color),
        };
    }
    */

    #endregion

    #endregion


    #region HSL 
    public static (float Hue, float Saturation, float Luminosity) ToHsl(this Color color)
    {
        ToHsl(color, out var h, out var s, out var l);
        return (h, s, l);
    }

    public static void ToHsl(this Color color, out float hue, out float saturation, out float luminosity)
    {
        var r = color.R / 255;
        var g = color.G / 255;
        var b = color.B / 255;

        var v = Math.Max(r, g);
        v = Math.Max(v, b);

        var m = Math.Min(r, g);
        m = Math.Min(m, b);

        luminosity = (m + v) / 2.0f;
        if (luminosity <= 0.0)
        {
            hue = saturation = luminosity = 0;
            return;
        }
        var vm = v - m;
        saturation = vm;

        if (saturation > 0.0)
        {
            saturation /= luminosity <= 0.5f ? v + m : 2.0f - v - m;
        }
        else
        {
            hue = 0;
            saturation = 0;
            return;
        }

        var r2 = (v - r) / vm;
        var g2 = (v - g) / vm;
        var b2 = (v - b) / vm;

        if (r == v)
        {
            hue = g == m ? 5.0f + b2 : 1.0f - g2;
        }
        else if (g == v)
        {
            hue = b == m ? 1.0f + r2 : 3.0f - b2;
        }
        else
        {
            hue = r == m ? 3.0f + g2 : 5.0f - r2;
        }
        hue /= 6.0f;
    }
    #endregion


    #region Tests
    /// <summary>
    /// Tests if the color is one of the default values
    /// </summary>
    /// <param name="c"></param>
    /// <returns></returns>
    public static bool IsDefault(this Color c)
        => c == default || (c.R == 0 && c.G == 0 && c.B == 0 && c.A == 0);


    /// <summary>
    /// Tests if the color is a default or is transparent
    /// </summary>
    /// <param name="c"></param>
    /// <returns></returns>
    public static bool IsDefaultOrTransparent(this Color c)
        => IsDefault(c) || c.A == 0;
    #endregion


    #region ToString
    /// <summary>
    /// Returns a string with comma separated, 0-255, integer values for color's RGB
    /// </summary>
    /// <returns>The int rgb color string.</returns>
    /// <param name="color">Color.</param>
    public static string ToIntRgbColorString(this Color color)
        => $"{color.R},{color.G},{color.B}";

    /// <summary>
    /// Returns a sring with comma separated, 0-255, integer values for color's RGBA
    /// </summary>
    /// <param name="color"></param>
    /// <returns></returns>
    public static string ToIntRgbaColorString(this Color color)
        => $"{color.R},{color.G},{color.B},{color.A}";

    /// <summary>
    /// Returns a string with comma separated, 0-255, integer values for color's RGBA
    /// </summary>
    /// <returns>The int rgb color string.</returns>
    /// <param name="color">Color.</param>
    public static string ToRgbaColorString(this Color color)
        => $"{color.ToIntRgbColorString()},{color.A}";
        

    /// <summary>
    /// Returns a 3 character hexadecimal string of a color's RGB value
    /// </summary>
    /// <returns>The hex rgb color string.</returns>
    /// <param name="color">Color.</param>
    public static string ToHexRgbColorString(this Color color)
    {
        var r = color.R >> 4;
        var g = color.G >> 4;
        var b = color.B >> 4;
        var R = r.ToString("x1");
        var G = g.ToString("x1");
        var B = b.ToString("x1");
        return R + G + B;
    }

    /// <summary>
    /// Returns a 4 character hexadecimal string of a color's ARGB value
    /// </summary>
    /// <returns>The hex rgb color string.</returns>
    /// <param name="color">Color.</param>
    public static string ToHexArgbColorString(this Color color)
    {
        var a = color.A >> 4;
        return a.ToString("x1") + color.ToHexRgbColorString();
    }

    /// <summary>
    /// Returns a 6 character hexadecimal string of a color's RRGGBB value
    /// </summary>
    /// <returns>The hex rgb color string.</returns>
    /// <param name="color">Color.</param>
    public static string ToHexRrggbbColorString(this Color color)
    {
        return color.R.ToString("x2") + color.G.ToString("x2") + color.B.ToString("x2");
    }

    /// <summary>
    /// Returns a 8 character hexadecimal string of a color's AARRGGBB value
    /// </summary>
    /// <returns>The hex rgb color string.</returns>
    /// <param name="color">Color.</param>
    public static string ToHexAarrggbbColorString(this Color color)
    {
        return color.A.ToString("x2") + color.ToHexRrggbbColorString();
    }

    public static int ToInt(this Color color)
    {
        var value = 0;
        value += color.A << 0x18;
        value += color.R << 0x10;
        value += color.G << 0x08;
        value += color.B;

        return value;
    }
    #endregion


    #region Internal

    private static uint ToHex(char c)
    {
        var x = (ushort)c;
        if (x >= '0' && x <= '9')
            return (uint)(x - '0');

        x |= 0x20;
        if (x >= 'a' && x <= 'f')
            return (uint)(x - 'a' + 10);
        return 0;
    }

    private static uint ToHexD(char c)
    {
        var j = ToHex(c);
        return (j << 4) | j;
    }
    #endregion


    public static Color AppColor(string key)
    {
        if (Application.Current.Resources[key] is Color color)
            return color;
        throw new Exception($"color not found in Application.Current.Resources for key [{key}]. ");
    }

    public static Brush AppBrush(string key)
    {
        try
        {
            var obj = Application.Current.Resources[key];
            if (obj is Brush brush)
                return brush;
            if (obj is Color color)
                return color.ToBrush();
            throw new Exception($"Brush not found in Application.Current.Resources for key [{key}]. ");
        }
        catch 
        { 
            return null;
        }
    }

    public static Color AsColor(this Brush brush)
    {
        if (brush is SolidColorBrush colorBrush)
            return colorBrush.Color;
        return default(Color);
    }

}
