// Decompiled with JetBrains decompiler
// Type: Uno.Extensions.Markup.Generators.Extensibility.GeometryTypeExtension
// Assembly: Uno.Extensions.Markup.Generators, Version=255.255.255.255, Culture=neutral, PublicKeyToken=null
// MVID: E6210F11-717B-4BCF-8EB9-4DC107A6F4FB
// Assembly location: C:\Users\ben\AppData\Local\Temp\Zehykin\f1db9ce6ec\analyzers\dotnet\cs\Uno.Extensions.Markup.Generators.dll

using CodeGenHelpers;
using System;

#nullable enable
namespace Uno.Extensions.Markup.Generators.Extensibility;

internal sealed class GeometryTypeExtension : ITypeExtension
{
    public bool CanExtend(string qualifiedTypeName)
    {
        return qualifiedTypeName == "global::Microsoft.UI.Xaml.Media.Geometry";
    }

    public void WriteAttachedPropertyBuilderExtensions(
      AttachedPropertyInfo prop,
      Func<MethodBuilder> createBuilder)
    {
        createBuilder().AddParameter<MethodBuilder>("string", "path").WithBody((Action<ICodeWriter>)(w =>
        {
            w.AppendLine("var geometry = (global::Microsoft.UI.Xaml.Media.Geometry)global::Microsoft.UI.Xaml.Markup.XamlBindingHelper.ConvertValue(typeof(global::Microsoft.UI.Xaml.Media.Geometry), path);");
            w.AppendLine($"return {prop.Name}(geometry);");
        }));
    }

    public void WriteDependencyPropertyExtensions(
      ClassBuilder builder,
      DependencyPropertyExtensionInfo info,
      Func<MethodBuilder> createBuilder)
    {
        createBuilder().AddParameter<MethodBuilder>("string", "path").WithBody((Action<ICodeWriter>)(w =>
        {
            w.AppendLine("var geometry = (global::Microsoft.UI.Xaml.Media.Geometry)global::Microsoft.UI.Xaml.Markup.XamlBindingHelper.ConvertValue(typeof(global::Microsoft.UI.Xaml.Media.Geometry), path);");
            w.AppendLine($"element.{info.PropertyName} = geometry;");
            w.AppendLine("return element;");
        }));
    }

    public void WriteStyleBuilderExtensions(
      ClassBuilder builder,
      StyleBuilderInfo info,
      Func<MethodBuilder> createBuilder)
    {
        builder.AddNamespaceImport("Microsoft.UI.Xaml.Media");
        createBuilder().AddParameter<MethodBuilder>("string", "path").WithBody((Action<ICodeWriter>)(w =>
        {
            w.AppendLine("var geometry = (global::Microsoft.UI.Xaml.Media.Geometry)global::Microsoft.UI.Xaml.Markup.XamlBindingHelper.ConvertValue(typeof(global::Microsoft.UI.Xaml.Media.Geometry), path);");
            w.AppendLine($"builder.{info.PropertyName}(geometry);");
            w.AppendLine("return builder;");
        }));
    }
}
