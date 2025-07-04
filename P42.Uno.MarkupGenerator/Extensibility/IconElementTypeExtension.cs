// Decompiled with JetBrains decompiler
// Type: Uno.Extensions.Markup.Generators.Extensibility.ImageSourceTypeExtension
// Assembly: Uno.Extensions.Markup.Generators, Version=255.255.255.255, Culture=neutral, PublicKeyToken=null
// MVID: E6210F11-717B-4BCF-8EB9-4DC107A6F4FB
// Assembly location: C:\Users\ben\AppData\Local\Temp\Zehykin\f1db9ce6ec\analyzers\dotnet\cs\Uno.Extensions.Markup.Generators.dll

using System;
using System.IO;
using CodeGenHelpers;
using Uno.Extensions.Markup.Generators;

#nullable enable
namespace P42.Uno.MarkupGenerator.Extensibility;

internal class IconElementTypeExtension : ITypeExtension
{
    public bool CanExtend(string qualifiedTypeName)
    {
        //return false;
        return qualifiedTypeName == "global::Microsoft.UI.Xaml.Controls.IconElement";
    }

    public void WriteAttachedPropertyBuilderExtensions(
      AttachedPropertyInfo prop,
      Func<MethodBuilder> createBuilder)
    {
        Helpers.IconExtension.CreateIconExtensions(createBuilder, prop.Name, "element");
    }

    public void WriteDependencyPropertyExtensions(
      ClassBuilder builder,
      DependencyPropertyExtensionInfo info,
      Func<MethodBuilder> createBuilder)
    {
        Helpers.IconExtension.CreateIconExtensions(createBuilder, info.PropertyName, "element");
    }

    public void WriteStyleBuilderExtensions(
      ClassBuilder builder,
      StyleBuilderInfo info,
      Func<MethodBuilder> createBuilder)
    {
        Helpers.IconExtension.CreateIconExtensions(createBuilder, info.PropertyName, "builder");
    }

}
