// Decompiled with JetBrains decompiler
// Type: Uno.Extensions.Markup.Generators.Extensibility.ThicknessTypeExtension
// Assembly: Uno.Extensions.Markup.Generators, Version=255.255.255.255, Culture=neutral, PublicKeyToken=null
// MVID: E6210F11-717B-4BCF-8EB9-4DC107A6F4FB
// Assembly location: C:\Users\ben\AppData\Local\Temp\Zehykin\f1db9ce6ec\analyzers\dotnet\cs\Uno.Extensions.Markup.Generators.dll

using CodeGenHelpers;
using System;

#nullable enable
namespace Uno.Extensions.Markup.Generators.Extensibility;

internal class ThicknessTypeExtension : ITypeExtension
{
    public bool CanExtend(string qualifiedTypeName)
    {
        return qualifiedTypeName == "global::Microsoft.UI.Xaml.Thickness";
    }

    public void WriteAttachedPropertyBuilderExtensions(
      AttachedPropertyInfo prop,
      Func<MethodBuilder> createBuilder)
    {
        createBuilder().AddParameter<MethodBuilder>("double", "uniformLength").WithBody((Action<ICodeWriter>)(w => w.AppendLine($"return {prop.Name}(new Thickness(uniformLength));")));
        createBuilder().AddParameter<MethodBuilder>("double", "x").AddParameter<MethodBuilder>("double", "y").WithBody((Action<ICodeWriter>)(w => w.AppendLine($"return {prop.Name}(new Thickness(x, y, x, y));")));
        createBuilder().AddParameter<MethodBuilder>("double", "left").AddParameter<MethodBuilder>("double", "top").AddParameter<MethodBuilder>("double", "right").AddParameter<MethodBuilder>("double", "bottom").WithBody((Action<ICodeWriter>)(w => w.AppendLine($"return {prop.Name}(new Thickness(left, top, right, bottom));")));
    }

    public void WriteDependencyPropertyExtensions(
      ClassBuilder builder,
      DependencyPropertyExtensionInfo info,
      Func<MethodBuilder> createBuilder)
    {
        GenerationTypeInfo generationTypeInfo = info.GenerationTypeInfo;
        createBuilder().AddParameter<MethodBuilder>("double", "uniformLength").WithBody((Action<ICodeWriter>)(w =>
        {
            w.AppendLine($"element.{info.PropertyName} = new Thickness(uniformLength);");
            w.AppendLine("return element;");
        }));
        createBuilder().AddParameter<MethodBuilder>("double", "x").AddParameter<MethodBuilder>("double", "y").WithBody((Action<ICodeWriter>)(w =>
        {
            w.AppendLine($"element.{info.PropertyName} = new Thickness(x, y, x, y);");
            w.AppendLine("return element;");
        }));
        createBuilder().AddParameter<MethodBuilder>("double", "left").AddParameter<MethodBuilder>("double", "top").AddParameter<MethodBuilder>("double", "right").AddParameter<MethodBuilder>("double", "bottom").WithBody((Action<ICodeWriter>)(w =>
        {
            w.AppendLine($"element.{info.PropertyName} = new Thickness(left, top, right, bottom);");
            w.AppendLine("return element;");
        }));
    }

    public void WriteStyleBuilderExtensions(
      ClassBuilder builder,
      StyleBuilderInfo info,
      Func<MethodBuilder> createBuilder)
    {
        createBuilder().AddParameter<MethodBuilder>("double", "uniformLength").WithBody((Action<ICodeWriter>)(w =>
        {
            w.AppendLine($"builder.{info.PropertyName}(new Thickness(uniformLength));");
            w.AppendLine("return builder;");
        }));
        createBuilder().AddParameter<MethodBuilder>("double", "x").AddParameter<MethodBuilder>("double", "y").WithBody((Action<ICodeWriter>)(w =>
        {
            w.AppendLine($"builder.{info.PropertyName}(new Thickness(x, y, x, y));");
            w.AppendLine("return builder;");
        }));
        createBuilder().AddParameter<MethodBuilder>("double", "left").AddParameter<MethodBuilder>("double", "top").AddParameter<MethodBuilder>("double", "right").AddParameter<MethodBuilder>("double", "bottom").WithBody((Action<ICodeWriter>)(w =>
        {
            w.AppendLine($"builder.{info.PropertyName}(new Thickness(left, top, right, bottom));");
            w.AppendLine("return builder;");
        }));
    }
}
