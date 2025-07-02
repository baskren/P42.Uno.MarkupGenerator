// Decompiled with JetBrains decompiler
// Type: Uno.Extensions.Markup.Generators.Extensibility.ColorTypeExtension
// Assembly: Uno.Extensions.Markup.Generators, Version=255.255.255.255, Culture=neutral, PublicKeyToken=null
// MVID: E6210F11-717B-4BCF-8EB9-4DC107A6F4FB
// Assembly location: C:\Users\ben\AppData\Local\Temp\Zehykin\f1db9ce6ec\analyzers\dotnet\cs\Uno.Extensions.Markup.Generators.dll

using CodeGenHelpers;
using System;

#nullable enable
namespace Uno.Extensions.Markup.Generators.Extensibility;

internal class ColorTypeExtension : ITypeExtension
{
    public bool CanExtend(string qualifiedTypeName)
    {
        return qualifiedTypeName == "global::Windows.UI.Color";
    }

    public void WriteAttachedPropertyBuilderExtensions(
      AttachedPropertyInfo prop,
      Func<MethodBuilder> createBuilder)
    {
        createBuilder().AddParameter<MethodBuilder>("string", "hexString").WithBody((Action<ICodeWriter>)(w => w.AppendLine($"return {prop.Name}((global::Windows.UI.Color)global::Microsoft.UI.Xaml.Markup.XamlBindingHelper.ConvertValue(typeof(global::Windows.UI.Color), hexString));")));
    }

    public void WriteDependencyPropertyExtensions(
      ClassBuilder builder,
      DependencyPropertyExtensionInfo info,
      Func<MethodBuilder> createBuilder)
    {
        createBuilder().AddParameter<MethodBuilder>("string", "hexString").WithBody((Action<ICodeWriter>)(w => w.AppendLine($"return element.{info.PropertyName}((global::Windows.UI.Color)global::Microsoft.UI.Xaml.Markup.XamlBindingHelper.ConvertValue(typeof(global::Windows.UI.Color), hexString));")));
    }

    public void WriteStyleBuilderExtensions(
      ClassBuilder builder,
      StyleBuilderInfo info,
      Func<MethodBuilder> createBuilder)
    {
        createBuilder().AddParameter<MethodBuilder>("string", "hexString").WithBody((Action<ICodeWriter>)(w =>
        {
            w.AppendLine("var color = (global::Windows.UI.Color)global::Microsoft.UI.Xaml.Markup.XamlBindingHelper.ConvertValue(typeof(global::Windows.UI.Color), hexString);");
            w.AppendLine($"return builder.{info.PropertyName}(color);");
        }));
    }
}
