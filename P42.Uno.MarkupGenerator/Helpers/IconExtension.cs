using System;
using System.Collections.Generic;
using System.Text;
using CodeGenHelpers;

namespace P42.Uno.MarkupGenerator.Helpers;
internal static class IconExtension
{
    public static void CreateIconExtensions(Func<MethodBuilder> createBuilder, string propertyName, string elementName, bool isSource = false, string comment = "")
    {
        createBuilder()
            .AddParameter("global::Microsoft.UI.Xaml.Controls.Symbol", "symbol")
            .WithBody(w => w.AppendLine($"return {elementName}.{propertyName}(new SymbolIcon{(isSource?"Source":"")} {{ Symbol = value }}); //{comment}"));

        createBuilder()
            .AddParameter("string", "glyph")
            .AddParameter("global::Microsoft.UI.Xaml.Media.FontFamily", "fontFamily")
            .WithBody(w => w.AppendLine($"return {elementName}.{propertyName}(new FontIcon{(isSource?"Source":"")} {{FontFamily = fontFamily, Glyph = glyph}});"));

        createBuilder()
            .AddParameter("string", "glyph")
            .AddParameter("global::Microsoft.UI.Xaml.Media.FontFamily", "fontFamily")
            .AddParameter("double", "fontSize")
            .WithBody(w => w.AppendLine($"return {elementName}.{propertyName}(new FontIcon{(isSource?"Source":"")} {{FontFamily = fontFamily, FontSize = fontSize, Glyph = glyph}});"));

        createBuilder()
            .AddParameter("string", "glyph")
            .AddParameter("string", "fontFamily")
            .WithBody(w => w.AppendLine($"return {elementName}.{propertyName}(new FontIcon{(isSource?"Source":"")} {{FontFamily = new FontFamily(fontFamily), Glyph = glyph}});"));

        createBuilder()
            .AddParameter("string", "glyph")
            .AddParameter("string", "fontFamily")
            .AddParameter("double", "fontSize")
            .WithBody(w => w.AppendLine($"return {elementName}.{propertyName}(new FontIcon{(isSource?"Source":"")} {{FontFamily = new FontFamily(fontFamily), FontSize = fontSize, Glyph = glyph}});"));

        createBuilder()
            .AddParameter("global::Microsoft.UI.Xaml.Media.Geometry", "path")
            .WithBody(w => w.AppendLine($"return {elementName}.{propertyName}(new PathIcon{(isSource?"Source":"")} {{Data = path}});"));

        createBuilder()
            .AddParameter("global::System.Uri", "bitMapUriSource")
            .AddParameter("bool", "showAsMonoChrome = true")
            .WithBody(w => w.AppendLine($"return {elementName}.{propertyName}(new BitmapIcon{(isSource?"Source":"")} {{UriSource = bitMapUriSource,ShowAsMonochrome = showAsMonoChrome}});"));

    }

}

