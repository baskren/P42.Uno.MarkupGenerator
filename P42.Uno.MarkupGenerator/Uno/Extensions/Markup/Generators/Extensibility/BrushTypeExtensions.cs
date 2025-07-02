// Decompiled with JetBrains decompiler
// Type: Uno.Extensions.Markup.Generators.Extensibility.BrushTypeExtension
// Assembly: Uno.Extensions.Markup.Generators, Version=255.255.255.255, Culture=neutral, PublicKeyToken=null
// MVID: E6210F11-717B-4BCF-8EB9-4DC107A6F4FB
// Assembly location: C:\Users\ben\AppData\Local\Temp\Zehykin\f1db9ce6ec\analyzers\dotnet\cs\Uno.Extensions.Markup.Generators.dll

using CodeGenHelpers;
using System;

#nullable enable
namespace Uno.Extensions.Markup.Generators.Extensibility;

internal class BrushTypeExtension : ITypeExtension
{
    public bool CanExtend(string qualifiedTypeName)
    {
        return qualifiedTypeName == "global::Microsoft.UI.Xaml.Media.Brush" || qualifiedTypeName == "global::Microsoft.UI.Xaml.Media.SolidColorBrush";
    }

    public void WriteAttachedPropertyBuilderExtensions(
      AttachedPropertyInfo prop,
      Func<MethodBuilder> createBuilder)
    {
        createBuilder().AddParameter<MethodBuilder>("global::Windows.UI.Color", "solidColor").WithBody((Action<ICodeWriter>)(w => w.AppendLine($"return {prop.Name}(new global::Microsoft.UI.Xaml.Media.SolidColorBrush(solidColor));")));
        createBuilder().AddParameter<MethodBuilder>("string", "hexString").WithBody((Action<ICodeWriter>)(w =>
        {
            w.AppendLine("var solidColor = (global::Windows.UI.Color)global::Microsoft.UI.Xaml.Markup.XamlBindingHelper.ConvertValue(typeof(global::Windows.UI.Color), hexString);");
            w.AppendLine($"return {prop.Name}(new global::Microsoft.UI.Xaml.Media.SolidColorBrush(solidColor));");
        }));
    }

    public void WriteDependencyPropertyExtensions(
      ClassBuilder builder,
      DependencyPropertyExtensionInfo info,
      Func<MethodBuilder> createBuilder)
    {
        createBuilder().AddParameter<MethodBuilder>("global::Windows.UI.Color", "solidColor").WithBody((Action<ICodeWriter>)(w =>
        {
            w.AppendLine($"element.{info.PropertyName} = new global::Microsoft.UI.Xaml.Media.SolidColorBrush(solidColor);");
            w.AppendLine("return element;");
        }));
        createBuilder().AddParameter<MethodBuilder>("string", "hexString").WithBody((Action<ICodeWriter>)(w =>
        {
            w.AppendLine("var solidColor = (global::Windows.UI.Color)global::Microsoft.UI.Xaml.Markup.XamlBindingHelper.ConvertValue(typeof(global::Windows.UI.Color), hexString);");
            w.AppendLine($"element.{info.PropertyName} = new global::Microsoft.UI.Xaml.Media.SolidColorBrush(solidColor);");
            w.AppendLine("return element;");
        }));
    }

    public void WriteStyleBuilderExtensions(
      ClassBuilder builder,
      StyleBuilderInfo info,
      Func<MethodBuilder> createBuilder)
    {
        createBuilder().AddParameter<MethodBuilder>("global::Windows.UI.Color", "solidColor").WithBody((Action<ICodeWriter>)(w =>
        {
            w.AppendLine("var brush = new global::Microsoft.UI.Xaml.Media.SolidColorBrush(solidColor);");
            w.AppendLine($"return {info.PropertyName}(builder, brush);");
        }));
        createBuilder().AddParameter<MethodBuilder>("string", "hexString").WithBody((Action<ICodeWriter>)(w =>
        {
            w.AppendLine("var solidColor = (global::Windows.UI.Color)global::Microsoft.UI.Xaml.Markup.XamlBindingHelper.ConvertValue(typeof(global::Windows.UI.Color), hexString);");
            w.AppendLine($"return {info.PropertyName}(builder, solidColor);");
        }));
    }
}
