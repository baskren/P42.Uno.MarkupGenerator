// Decompiled with JetBrains decompiler
// Type: Uno.Extensions.Markup.Generators.Extensibility.ResourceDictionaryTypeExtension
// Assembly: Uno.Extensions.Markup.Generators, Version=255.255.255.255, Culture=neutral, PublicKeyToken=null
// MVID: E6210F11-717B-4BCF-8EB9-4DC107A6F4FB
// Assembly location: C:\Users\ben\AppData\Local\Temp\Zehykin\f1db9ce6ec\analyzers\dotnet\cs\Uno.Extensions.Markup.Generators.dll

using CodeGenHelpers;
using System;

#nullable enable
namespace Uno.Extensions.Markup.Generators.Extensibility;

internal class ResourceDictionaryTypeExtension : ITypeExtension
{
    public bool CanExtend(string qualifiedTypeName)
    {
        return false;
        //return qualifiedTypeName == "global::Microsoft.UI.Xaml.ResourceDictionary";
    }

    public void WriteAttachedPropertyBuilderExtensions(
      AttachedPropertyInfo prop,
      Func<MethodBuilder> createBuilder)
    {
    }

    public void WriteDependencyPropertyExtensions(
      ClassBuilder builder,
      DependencyPropertyExtensionInfo info,
      Func<MethodBuilder> createBuilder)
    {
        createBuilder().AddParameter<MethodBuilder>("Action<ResourceDictionaryBuilder>", "configureResources").WithBody((Action<ICodeWriter>)(w =>
        {
            w.AppendLine($"element.{info.PropertyName} ??= new global::Microsoft.UI.Xaml.ResourceDictionary();");
            w.AppendLine($"configureResources(new ResourceDictionaryBuilder(element.{info.PropertyName}));");
            w.AppendLine("return element;");
        }));
    }

    public void WriteStyleBuilderExtensions(
      ClassBuilder builder,
      StyleBuilderInfo info,
      Func<MethodBuilder> createBuilder)
    {
    }
}
