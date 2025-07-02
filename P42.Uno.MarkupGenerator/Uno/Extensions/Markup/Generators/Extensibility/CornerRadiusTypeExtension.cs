// Decompiled with JetBrains decompiler
// Type: Uno.Extensions.Markup.Generators.Extensibility.CornerRadiusTypeExtension
// Assembly: Uno.Extensions.Markup.Generators, Version=255.255.255.255, Culture=neutral, PublicKeyToken=null
// MVID: E6210F11-717B-4BCF-8EB9-4DC107A6F4FB
// Assembly location: C:\Users\ben\AppData\Local\Temp\Zehykin\f1db9ce6ec\analyzers\dotnet\cs\Uno.Extensions.Markup.Generators.dll

using CodeGenHelpers;
using System;

#nullable enable
namespace Uno.Extensions.Markup.Generators.Extensibility;

internal class CornerRadiusTypeExtension : ITypeExtension
{
    public bool CanExtend(string qualifiedTypeName)
    {
        return qualifiedTypeName == "global::Microsoft.UI.Xaml.CornerRadius";
    }

    public void WriteAttachedPropertyBuilderExtensions(
      AttachedPropertyInfo prop,
      Func<MethodBuilder> createBuilder)
    {
        createBuilder().AddParameter<MethodBuilder>("double", "topLeft").AddParameter<MethodBuilder>("double", "topRight").AddParameter<MethodBuilder>("double", "bottomRight").AddParameter<MethodBuilder>("double", "bottomLeft").WithBody((Action<ICodeWriter>)(w => w.AppendLine($"return {prop.Name}(new global::Microsoft.UI.Xaml.CornerRadius(topLeft, topRight, bottomRight, bottomLeft));")));
        createBuilder().AddParameter<MethodBuilder>("double", "uniformRadius").WithBody((Action<ICodeWriter>)(w => w.AppendLine($"return {prop.Name}(new global::Microsoft.UI.Xaml.CornerRadius(uniformRadius));")));
    }

    public void WriteDependencyPropertyExtensions(
      ClassBuilder builder,
      DependencyPropertyExtensionInfo info,
      Func<MethodBuilder> createBuilder)
    {
        createBuilder().AddParameter<MethodBuilder>("double", "topLeft").AddParameter<MethodBuilder>("double", "topRight").AddParameter<MethodBuilder>("double", "bottomRight").AddParameter<MethodBuilder>("double", "bottomLeft").WithBody((Action<ICodeWriter>)(w =>
        {
            w.AppendLine($"element.{info.PropertyName} = new global::Microsoft.UI.Xaml.CornerRadius(topLeft, topRight, bottomRight, bottomLeft);");
            w.AppendLine("return element;");
        }));
        createBuilder().AddParameter<MethodBuilder>("double", "uniformRadius").WithBody((Action<ICodeWriter>)(w =>
        {
            w.AppendLine($"element.{info.PropertyName} = new global::Microsoft.UI.Xaml.CornerRadius(uniformRadius);");
            w.AppendLine("return element;");
        }));
    }

    public void WriteStyleBuilderExtensions(
      ClassBuilder builder,
      StyleBuilderInfo info,
      Func<MethodBuilder> createBuilder)
    {
        createBuilder().AddParameter<MethodBuilder>("double", "topLeft").AddParameter<MethodBuilder>("double", "topRight").AddParameter<MethodBuilder>("double", "bottomRight").AddParameter<MethodBuilder>("double", "bottomLeft").WithBody((Action<ICodeWriter>)(w =>
        {
            w.AppendLine($"builder.{info.PropertyName}(new global::Microsoft.UI.Xaml.CornerRadius(topLeft, topRight, bottomRight, bottomLeft));");
            w.AppendLine("return builder;");
        }));
        createBuilder().AddParameter<MethodBuilder>("double", "uniformRadius").WithBody((Action<ICodeWriter>)(w =>
        {
            w.AppendLine($"builder.{info.PropertyName}(new global::Microsoft.UI.Xaml.CornerRadius(uniformRadius));");
            w.AppendLine("return builder;");
        }));
    }
}
