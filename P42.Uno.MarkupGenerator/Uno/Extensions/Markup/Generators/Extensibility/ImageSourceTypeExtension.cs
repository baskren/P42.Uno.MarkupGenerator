// Decompiled with JetBrains decompiler
// Type: Uno.Extensions.Markup.Generators.Extensibility.ImageSourceTypeExtension
// Assembly: Uno.Extensions.Markup.Generators, Version=255.255.255.255, Culture=neutral, PublicKeyToken=null
// MVID: E6210F11-717B-4BCF-8EB9-4DC107A6F4FB
// Assembly location: C:\Users\ben\AppData\Local\Temp\Zehykin\f1db9ce6ec\analyzers\dotnet\cs\Uno.Extensions.Markup.Generators.dll

using CodeGenHelpers;
using System;

#nullable enable
namespace Uno.Extensions.Markup.Generators.Extensibility;

internal class ImageSourceTypeExtension : ITypeExtension
{
    public bool CanExtend(string qualifiedTypeName)
    {
        return false;
        //return qualifiedTypeName == "global::Microsoft.UI.Xaml.Media.ImageSource";
    }

    public void WriteAttachedPropertyBuilderExtensions(
      AttachedPropertyInfo prop,
      Func<MethodBuilder> createBuilder)
    {
        createBuilder()
            .AddParameter("string", "source")
            .WithBody(w =>
            {
                w.AppendLine("var assemblyName = global::System.Reflection.Assembly.GetCallingAssembly().GetName().Name ?? throw new global::System.NullReferenceException(\"Unable to determine the calling assembly. Please use a fully qualified uri.\");");
                w.AppendLine("var imageSource = MarkupImageSource.Load(source, assemblyName);");
                w.If("imageSource is not null").WithBody(x => x.AppendLine($"return {prop.Name}(imageSource);")).EndIf();
                w.AppendLine("return builder;");
            });
        createBuilder()
            .AddParameter("global::System.Uri", "uri")
            .WithBody(w =>
            {
                w.AppendLine("var assemblyName = global::System.Reflection.Assembly.GetCallingAssembly().GetName().Name ?? throw new global::System.NullReferenceException(\"Unable to determine the calling assembly. Please use a fully qualified uri.\");");
                w.AppendLine("var imageSource = MarkupImageSource.Load(uri, assemblyName);");
                w.If("imageSource is not null").WithBody(x => x.AppendLine($"return {prop.Name}(imageSource);")).EndIf();
                w.AppendLine("return builder;");
            });
    }

    public void WriteDependencyPropertyExtensions(
      ClassBuilder builder,
      DependencyPropertyExtensionInfo info,
      Func<MethodBuilder> createBuilder)
    {
        createBuilder()
            .AddParameter("string", "source")
            .WithBody(w =>
            {
                w.AppendLine("var assemblyName = global::System.Reflection.Assembly.GetCallingAssembly().GetName().Name ?? throw new global::System.NullReferenceException(\"Unable to determine the calling assembly. Please use a fully qualified uri.\");");
                w.AppendLine("var imageSource = MarkupImageSource.Load(source, assemblyName);");
                w.If("imageSource is not null").WithBody(x => x.AppendLine($"return element.{info.PropertyName}(imageSource);")).EndIf();
                w.AppendLine("return element;");
            });
        createBuilder()
            .AddParameter("global::System.Uri", "uri")
            .WithBody(w =>
            {
                w.AppendLine("var assemblyName = global::System.Reflection.Assembly.GetCallingAssembly().GetName().Name ?? throw new global::System.NullReferenceException(\"Unable to determine the calling assembly. Please use a fully qualified uri.\");");
                w.AppendLine("var imageSource = MarkupImageSource.Load(uri, assemblyName);");
                w.If("imageSource is not null").WithBody(x => x.AppendLine($"return element.{info.PropertyName}(imageSource);")).EndIf();
                w.AppendLine("return element;");
            });
    }

    public void WriteStyleBuilderExtensions(
      ClassBuilder builder,
      StyleBuilderInfo info,
      Func<MethodBuilder> createBuilder)
    {
        createBuilder()
            .AddParameter("string", "source")
            .WithBody(w =>
            {
                w.AppendLine("var assemblyName = global::System.Reflection.Assembly.GetCallingAssembly().GetName().Name ?? throw new global::System.NullReferenceException(\"Unable to determine the calling assembly. Please use a fully qualified uri.\");");
                w.AppendLine("var imageSource = MarkupImageSource.Load(source, assemblyName);");
                w.If("imageSource is not null").WithBody(x => x.AppendLine($"return builder.{info.PropertyName}(imageSource);")).EndIf();
                w.AppendLine("return builder;");
            });
        createBuilder()
            .AddParameter("global::System.Uri", "uri")
            .WithBody(w =>
            {
                w.AppendLine("var assemblyName = global::System.Reflection.Assembly.GetCallingAssembly().GetName().Name ?? throw new global::System.NullReferenceException(\"Unable to determine the calling assembly. Please use a fully qualified uri.\");");
                w.AppendLine("var imageSource = MarkupImageSource.Load(uri, assemblyName);");
                w.If("imageSource is not null").WithBody(x => x.AppendLine($"return builder.{info.PropertyName}(imageSource);")).EndIf();
                w.AppendLine("return builder;");
            });
    }
}
