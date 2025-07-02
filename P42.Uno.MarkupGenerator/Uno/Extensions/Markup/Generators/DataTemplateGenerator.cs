// Decompiled with JetBrains decompiler
// Type: Uno.Extensions.Markup.Generators.DataTemplateGenerator
// Assembly: Uno.Extensions.Markup.Generators, Version=255.255.255.255, Culture=neutral, PublicKeyToken=null
// MVID: E6210F11-717B-4BCF-8EB9-4DC107A6F4FB
// Assembly location: C:\Users\ben\AppData\Local\Temp\Zehykin\f1db9ce6ec\analyzers\dotnet\cs\Uno.Extensions.Markup.Generators.dll

using CodeGenHelpers;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using Uno.Extensions.Markup.Generators.Extensions;

#nullable enable
namespace Uno.Extensions.Markup.Generators;

[Generator("C#", [])]
internal sealed class DataTemplateGenerator : IncrementalExtensionsGeneratorBase<DataTemplateInfo>
{
    private protected override EquatableArray<DataTemplateInfo>? GetInfoForType(
      INamedTypeSymbol namedType)
    {
        if (namedType.IsGenericType)
            return new EquatableArray<DataTemplateInfo>?();

        var builder = ImmutableArray.CreateBuilder<DataTemplateInfo>();
        var GenerationTypeInfo =Generators.GenerationTypeInfo.From(namedType);
        foreach (var iPropertySymbol in namedType.GetMembers().OfType<IPropertySymbol>())
        {
            if (!iPropertySymbol.IsStatic && !iPropertySymbol.IsReadOnly)
            {
                string typeExcludingGlobal = iPropertySymbol.Type.GetFullyQualifiedTypeExcludingGlobal();
                string PropertyTypeFullyQualifiedName = "global::" + typeExcludingGlobal;
                if ((!(iPropertySymbol.Type.Name == "DataTemplateSelector") ? 0 : (typeExcludingGlobal == "Microsoft.UI.Xaml.Controls.DataTemplateSelector" ? 1 : 0)) != 0)
                {
                    builder.Add(new DataTemplateInfo(PropertyTypeFullyQualifiedName, false, false, true, false, iPropertySymbol.Name, iPropertySymbol.Type.Name, GenerationTypeInfo));
                }
                else
                {
                    if (iPropertySymbol.SetMethod != null && (iPropertySymbol.Type as INamedTypeSymbol).IsFrameworkTemplate(out bool isControlTemplate))
                    {
                        bool PropertyTypeIsDataTemplate = iPropertySymbol.Type.Name == "DataTemplate" && typeExcludingGlobal == "Microsoft.UI.Xaml.DataTemplate";
                        builder.Add(new DataTemplateInfo(PropertyTypeFullyQualifiedName, isControlTemplate, PropertyTypeIsDataTemplate, false, false, iPropertySymbol.Name, iPropertySymbol.Type.Name, GenerationTypeInfo));
                    }
                }
            }
        }
        var baseType = namedType.BaseType;
        while (baseType != null)
        {
            foreach (var iPropertySymbol in baseType.GetMembers().OfType<IPropertySymbol>())
            {
                var property = iPropertySymbol;
                if (!builder.Any(x => x.PropertyName == property.Name))
                {
                    string typeExcludingGlobal = property.Type.GetFullyQualifiedTypeExcludingGlobal();
                    if ((property.Type as INamedTypeSymbol).IsFrameworkTemplate(out bool isControlTemplate))
                    {
                        bool flag = property.Type.Name == "DataTemplate" && typeExcludingGlobal == "Microsoft.UI.Xaml.DataTemplate";
                        string PropertyTypeFullyQualifiedName = "global::" + typeExcludingGlobal;
                        builder.Add(new DataTemplateInfo(PropertyTypeFullyQualifiedName, isControlTemplate, flag, false, flag, property.Name, property.Type.Name, GenerationTypeInfo));
                    }
                }
            }
            baseType = baseType.BaseType;
            if (baseType == null || baseType.GetFullyQualifiedTypeExcludingGlobal() == "Microsoft.UI.Xaml.UIElement")
                break;
        }
        return builder.Count == 0 
            ? new EquatableArray<DataTemplateInfo>?() 
            : new EquatableArray<DataTemplateInfo>?(builder.ToImmutableArray().AsEquatableArray());
    }

    private protected override string GetClassName(string typeName) => typeName + "Markup";

    private protected override void GenerateCodeFromInfosCore(
      ClassBuilder builder,
      EquatableArray<DataTemplateInfo> infos,
      SourceProductionContext context,
      CancellationToken cancellationToken)
    {
        foreach (var info1 in infos)
        {
            var info = info1;
            cancellationToken.ThrowIfCancellationRequested();
            GenerationTypeInfo generationTypeInfo;
            if (info.PropertyTypeIsDataTemplateSelector)
            {
                var methodBuilder = builder
                    .AddMethod(info.PropertyName)
                    .MakePublicMethod()
                    .MakeStaticMethod()
                    .AddAttribute("global::Uno.Extensions.Markup.Internals.MarkupExtensionAttribute");
                generationTypeInfo = info.GenerationTypeInfo;
                string fullyQualifiedName = generationTypeInfo.TypeFullyQualifiedName;
                var parameterized = methodBuilder
                    .WithReturnType(fullyQualifiedName)
                    .AddGeneric("TItem");
                generationTypeInfo = info.GenerationTypeInfo;
                string typeName = "this " + generationTypeInfo.TypeFullyQualifiedName;
                parameterized
                    .AddParameter(typeName, "element")
                    .AddParameter("Action<TItem, MarkupDataTemplateSelectorBuilder<TItem>>", "configureDataTemplateSelector")
                    .WithBody(w =>
                    {
                        w.AppendLine("var builder = new MarkupDataTemplateSelectorBuilder<TItem>();");
                        w.AppendUnindentedLine("#nullable disable");
                        w.AppendLine("configureDataTemplateSelector(default, builder);");
                        w.AppendUnindentedLine("#nullable enable");
                        w.AppendLine($"element.{info.PropertyName} = builder;");
                        w.AppendLine("return element;");
                    });
            }
            else
            {
                string delegateName = info.PropertyTypeName.Camelcase() + "Delegate";
                if (info.PropertyTypeIsControlTemplate)
                {
                    var parameterized1 = builder
                        .AddMethod(info.PropertyName)
                        .MakePublicMethod()
                        .MakeStaticMethod()
                        .AddAttribute("global::Uno.Extensions.Markup.Internals.MarkupExtensionAttribute");
                    var controlType = "TControl";
                    generationTypeInfo = info.GenerationTypeInfo;
                    if (!generationTypeInfo.IsSealed)
                    {
                        parameterized1
                            .AddGeneric("TControl", b => b.AddConstraint(info.GenerationTypeInfo.TypeFullyQualifiedName))
                            .WithReturnType("TControl")
                            .AddParameter("this TControl", "element");
                    }
                    else
                    {
                        generationTypeInfo = info.GenerationTypeInfo;
                        controlType = generationTypeInfo.TypeFullyQualifiedName;
                        var methodBuilder = parameterized1;
                        generationTypeInfo = info.GenerationTypeInfo;
                        var fullyQualifiedName = generationTypeInfo.TypeFullyQualifiedName;
                        var parameterized2 = methodBuilder.WithReturnType(fullyQualifiedName);
                        generationTypeInfo = info.GenerationTypeInfo;
                        var typeName = "this " + generationTypeInfo.TypeFullyQualifiedName;
                        parameterized2.AddParameter(typeName, "element");
                    }
                    if (info.PropertyTypeName == "ControlTemplate")
                        parameterized1
                            .AddParameter("Func<UIElement>", delegateName)
                            .WithBody(w =>
                            {
                                w.AppendLine($"element.{info.PropertyName} = DataTemplateHelpers.ControlTemplate(() => {delegateName}(), typeof({controlType}));");
                                w.AppendLine("return element;");
                            });
                    else
                        parameterized1
                            .AddGeneric("TRootControl", b => b.AddConstraint("UIElement"))
                            .AddParameter($"Func<{controlType}, TRootControl>", delegateName)
                            .WithBody(w =>
                            {
                                w.AppendUnindentedLine("#nullable disable");
                                w.AppendLine($"element.{info.PropertyName} = DataTemplateHelpers.FrameworkTemplate<{info.PropertyTypeFullyQualifiedName}>(() => (UIElement){delegateName}(element), typeof({controlType}));");
                                w.AppendUnindentedLine("#nullable enable");
                                w.AppendLine("return element;");
                            });
                }
                else if (!info.PropertyTypeIsDataTemplate)
                {
                    var methodBuilder = builder
                        .AddMethod(info.PropertyName)
                        .MakePublicMethod()
                        .MakeStaticMethod();
                    generationTypeInfo = info.GenerationTypeInfo;
                    var fullyQualifiedName = generationTypeInfo.TypeFullyQualifiedName;
                    var parameterized3 = methodBuilder
                        .WithReturnType(fullyQualifiedName)
                        .AddAttribute("global::Uno.Extensions.Markup.Internals.MarkupExtensionAttribute");
                    generationTypeInfo = info.GenerationTypeInfo;
                    var typeName = "this " + generationTypeInfo.TypeFullyQualifiedName;
                    var parameterized4 = parameterized3
                        .AddParameter(typeName, "element");
                    if (info.PropertyTypeName == "ItemsPanelTemplate")
                        parameterized4
                            .AddGeneric("TItem", b => b.AddConstraint("UIElement, new()"))
                            .AddParameterWithNullValue("Action<TItem>?", "configureItemsPanel")
                            .WithBody(w =>
                            {
                                w.AppendLine($"element.{info.PropertyName} = DataTemplateHelpers.ItemsPanelTemplate<TItem>(configureItemsPanel);");
                                w.AppendLine("return element;");
                            });
                    else
                        parameterized4
                            .AddParameter("Func<UIElement>", delegateName)
                            .WithBody(w =>
                            {
                                if (info.PropertyTypeName == "FrameworkTemplate")
                                    w.AppendLine($"element.{info.PropertyName} = DataTemplateHelpers.FrameworkTemplate(() => {delegateName}());");
                                else
                                    w.AppendLine($"element.{info.PropertyName} = DataTemplateHelpers.FrameworkTemplate<{info.PropertyTypeFullyQualifiedName}>({delegateName});");
                                w.AppendLine("return element;");
                            });
                }
                else
                {
                    var methodBuilder1 = builder
                        .AddMethod(info.PropertyName)
                        .MakePublicMethod()
                        .MakeStaticMethod()
                        .AddAttribute("global::Uno.Extensions.Markup.Internals.MarkupExtensionAttribute");
                    generationTypeInfo = info.GenerationTypeInfo;
                    var fullyQualifiedName1 = generationTypeInfo.TypeFullyQualifiedName;
                    var parameterized5 = methodBuilder1
                        .WithReturnType(fullyQualifiedName1)
                        .AddGeneric("TItem");
                    generationTypeInfo = info.GenerationTypeInfo;
                    var typeName1 = "this " + generationTypeInfo.TypeFullyQualifiedName;
                    parameterized5
                        .AddParameter(typeName1, "element")
                        .AddParameter("Func<TItem, UIElement>", delegateName)
                        .WithBody(w =>
                        {
                            w.AppendUnindentedLine("#nullable disable");
                            if (info.PropertyTypeIsDataTemplate)
                                w.AppendLine($"element.{info.PropertyName} = DataTemplateHelpers.DataTemplate(() => {delegateName}(default));");
                            else
                                w.AppendLine($"element.{info.PropertyName} = DataTemplateHelpers.FrameworkTemplate<{info.PropertyTypeFullyQualifiedName}>(() => {delegateName}(default));");
                            w.AppendUnindentedLine("#nullable enable");
                            w.AppendLine("return element;");
                        });
                    var methodBuilder2 = builder
                        .AddMethod(info.PropertyName)
                        .MakePublicMethod()
                        .MakeStaticMethod()
                        .AddAttribute("global::Uno.Extensions.Markup.Internals.MarkupExtensionAttribute");
                    generationTypeInfo = info.GenerationTypeInfo;
                    var fullyQualifiedName2 = generationTypeInfo.TypeFullyQualifiedName;
                    var parameterized6 = methodBuilder2.WithReturnType(fullyQualifiedName2);
                    generationTypeInfo = info.GenerationTypeInfo;
                    var typeName2 = "this " + generationTypeInfo.TypeFullyQualifiedName;
                    parameterized6
                        .AddParameter(typeName2, "element")
                        .AddParameter("Func<UIElement>", delegateName)
                        .WithBody(w =>
                        {
                            w.AppendLine($"element.{info.PropertyName} = DataTemplateHelpers.DataTemplate(() => {delegateName}());");
                            w.AppendLine("return element;");
                        });
                    var methodBuilder3 = builder
                        .AddMethod(info.PropertyName).
                        MakePublicMethod()
                        .MakeStaticMethod()
                        .AddAttribute("global::Uno.Extensions.Markup.Internals.MarkupExtensionAttribute");
                    generationTypeInfo = info.GenerationTypeInfo;
                    var fullyQualifiedName3 = generationTypeInfo.TypeFullyQualifiedName;
                    var parameterized7 = methodBuilder3
                        .WithReturnType(fullyQualifiedName3)
                        .AddGeneric("TItem")
                        .AddGeneric("TRoot", b => b.AddConstraint("UIElement, new()"));
                    generationTypeInfo = info.GenerationTypeInfo;
                    var typeName3 = "this " + generationTypeInfo.TypeFullyQualifiedName;
                    parameterized7
                        .AddParameter(typeName3, "element")
                        .AddParameter("Action<TItem, TRoot>", "configureRoot")
                        .WithBody(w =>
                        {
                            w.AppendUnindentedLine("#nullable disable");
                            w.AppendLine($"element.{info.PropertyName} = DataTemplateHelpers.DataTemplate<TRoot>(root => configureRoot(default, root));");
                            w.AppendUnindentedLine("#nullable enable");
                            w.AppendLine("return element;");
                        });
                }
            }
        }
    }
}
