// Decompiled with JetBrains decompiler
// Type: Uno.Extensions.Markup.Generators.DependencyPropertyExtensionGenerator
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
using Uno.Extensions.Markup.Generators.Extensibility;
using Uno.Extensions.Markup.Generators.Extensions;

#nullable enable
namespace Uno.Extensions.Markup.Generators;

[Generator("C#", [])]
internal sealed class DependencyPropertyExtensionGenerator :
  IncrementalExtensionsGeneratorBase<DependencyPropertyExtensionInfo>
{
    private protected override EquatableArray<DependencyPropertyExtensionInfo>? GetInfoForType(INamedTypeSymbol namedType)
    {
        if (!namedType.IsDependencyObject() || namedType.IsGenericType)
            return new EquatableArray<DependencyPropertyExtensionInfo>?();
        string name = namedType.Name;
        if (name == "Binding" || name == "Setter")
            return new EquatableArray<DependencyPropertyExtensionInfo>?();
        bool generationTypeIsFrameworkElement = namedType.IsFrameworkTemplate();
        for (var type = namedType; type != null; type = type.BaseType)
        {
            if (type.Name == "FrameworkElement" && type.GetFullyQualifiedTypeExcludingGlobal() == "Microsoft.UI.Xaml.FrameworkElement")
            {
                generationTypeIsFrameworkElement = true;
                break;
            }
        }
        var membersForGeneration = namedType.GetMembersForGeneration();
        var iPropertySymbols = membersForGeneration.OfType<IPropertySymbol>().Where(x => x.IsCollectionWithAddMethod() || x.HasPublicSetter());
        ImmutableArray<DependencyPropertyExtensionInfo>.Builder builder = ImmutableArray.CreateBuilder<DependencyPropertyExtensionInfo>();
        GenerationTypeInfo typeInfo = GenerationTypeInfo.From(namedType);
        foreach (IPropertySymbol ipropertySymbol in iPropertySymbols)
        {
            IPropertySymbol property = ipropertySymbol;
            if (
                !property.IsStatic 
                && !property.IsIndexer 
                && !property.Name.Contains(".") 
                && property.DeclaredAccessibility != Accessibility.Private 
                && property.DeclaredAccessibility != Accessibility.Protected 
                && !IsIgnoredProperty(property) 
                && !property.IsNotImplemented() 
                && (property.Name != "DataContext")
               )
            {
                bool isDependencyProperty = ImmutableArrayExtensions.Any(membersForGeneration, m => m.Name == property.Name + "Property");
                builder.Add(DependencyPropertyExtensionInfo.From(property, typeInfo, isDependencyProperty, generationTypeIsFrameworkElement, !namedType.IsSealed && DependencyPropertyExtensionGenerator.IsShadowing(property, namedType)));
            }
        }
        return new EquatableArray<DependencyPropertyExtensionInfo>?(builder.ToImmutable().AsEquatableArray());
    }

    private static string AdjustParameterName(string parameterName)
    {
        return parameterName == "element" ? "value" : parameterName.EscapeIdentifier();
    }

    private static bool IsIgnoredProperty(IPropertySymbol property)
    {
        if (property.Name == "TemplatedParent" || property.Name == "XamlRoot")
            return true;
        string name1 = property.Name;
        if (name1 == "Count" || name1 == "IsReadOnly")
            return property.ContainingType.Name == "ColorKeyFrameCollection";
        if (property.Name == "RoutedEvent")
            return property.ContainingType.Name == "EventTrigger";
        string name2 = property.Name;
        if (name2 == "AreDimensionsConstrained" || name2 == "RenderPhase")
            return property.ContainingType.Name == "FrameworkElement";
        if (property.Name == "IsParsing")
        {
            string name3 = property.ContainingType.Name;
            return name3 == "FrameworkElement" || name3 == "ResourceDictionary";
        }
        if (property.Name == "BasedOn")
            return property.ContainingType.Name == "Style";
        if (property.Name == "TargetType")
        {
            string name4 = property.ContainingType.Name;
            return name4 == "Style" || name4 == "ControlTemplate";
        }
        if (property.Name == "Name")
        {
            string name5 = property.ContainingType.Name;
            return name5 == "VisualState" || name5 == "VisualStateGroup";
        }
        return property.Name == "ItemsPanelRoot" && property.ContainingType.Name == "ItemsControl";
    }

    private static bool IsShadowing(IPropertySymbol property, INamedTypeSymbol namedType)
    {
        var inamedTypeSymbol = namedType;
        while (inamedTypeSymbol != null && !(inamedTypeSymbol.Name == "UIElement"))
        {
            inamedTypeSymbol = inamedTypeSymbol.BaseType;
            if (inamedTypeSymbol != null && inamedTypeSymbol.GetMembers(property.Name).Length > 0)
                return true;
        }
        return false;
    }

    private protected override string GetClassName(string typeName) => typeName + "Markup";

    private protected override void GenerateCodeFromInfosCore(
      ClassBuilder builder,
      EquatableArray<DependencyPropertyExtensionInfo> infos,
      SourceProductionContext context,
      CancellationToken cancellationToken)
    {
        foreach (DependencyPropertyExtensionInfo info in infos)
        {
            cancellationToken.ThrowIfCancellationRequested();
            GeneratePropertyExtensions(builder, info);
        }
        infos.FirstOrDefault();
    }

    private void GeneratePropertyExtensions(
      ClassBuilder builder,
      DependencyPropertyExtensionInfo info)
    {
        string type = info.PropertyTypeFullyQualified;
        if (info.PropertyHasPublicSetter)
        {
            string parameterName = DependencyPropertyExtensionGenerator.AdjustParameterName(info.PropertyName.Camelcase());
            MethodBuilder methodBuilder = DependencyPropertyExtensionGenerator.CreatePropertyBuilder(ref builder, info.GenerationTypeInfo, info.PropertyName, info.IsNotSealedAndIsShadowing).AddParameter(type, parameterName);
            if (type == "global::Microsoft.UI.Xaml.ResourceDictionary")
                methodBuilder.WithBody(w =>
                {
                    if (info.PropertyName == "Resources")
                    {
                        w.AppendLine("element.Resources ??= new ResourceDictionary();");
                        w.AppendLine($"element.Resources.MergedDictionaries.Add({parameterName});");
                    }
                    else
                        w.AppendLine($"element.{info.PropertyName} = {parameterName};");
                    w.AppendLine("return element;");
                });
            else
                methodBuilder.WithBody((Action<ICodeWriter>)(w =>
                {
                    if (info.PropertyIsDependencyObjectButNotFrameworkElement)
                        w.AppendLine($"ResourceObserver.SetResourceParent({parameterName}, element);");
                    else if (info.PropertyIsCollection)
                    {
                        foreach (MethodParameterInfo methodParameterType in info.MethodParameterTypes)
                        {
                            if (methodParameterType.IsDependencyObject)
                                w.ForEach("var item", parameterName).WithBody(_ => _.If("item is Microsoft.UI.Xaml.DependencyObject dObj").WithBody(ifWrite => ifWrite.AppendLine("ResourceObserver.SetResourceParent(dObj, element);")).EndIf());
                        }
                    }
                    string str = $"element.{info.PropertyName} = {parameterName}";
                    if (type == "global::Microsoft.UI.Xaml.Style")
                        w.AppendLine($"global::Uno.Extensions.Markup.LoadingObserver.AddLoadingCallback(element, _ => {str});");
                    else
                        w.AppendLine(str + ";");
                    w.AppendLine("return element;");
                }));
            ((IEnumerable<ITypeExtension>)ExtensibilityLocator.Extensions).ForEach(x =>
            {
                if (!x.CanExtend(info.PropertyTypeFullyQualified))
                    return;
                x.WriteDependencyPropertyExtensions(builder, info, () => DependencyPropertyExtensionGenerator.CreatePropertyBuilder(ref builder, info.GenerationTypeInfo, info.PropertyName));
            });
            if (info.PropertyIsCollection)
                CreateCollectionExtension(builder, info);
            if (!info.IsDependencyProperty)
                return;
            if (!info.IsTemplateType)
            {
                CreatePropertyBuilder(ref builder, info.GenerationTypeInfo, info.PropertyName)
                    .AddGeneric("TSource")
                    .AddParameter("Func<TSource>", "propertyBinding")
                    .AddParameter("[CallerArgumentExpression(\"propertyBinding\")]string?", "propertyBindingExpression = null")
                    .WithBody(w =>
                    {
                        if (info.GenerationTypeInfo.IsSealed)
                            w.AppendLine($"return {info.PropertyName}(element, _ => _.Binding(propertyBinding, propertyBindingExpression));");
                        else
                            w.AppendLine($"return {info.PropertyName}<T>(element, _ => _.Binding(propertyBinding, propertyBindingExpression));");
                    });
                CreatePropertyBuilder(ref builder, info.GenerationTypeInfo, info.PropertyName)
                    .AddGeneric("TSource")
                    .AddParameter("Func<TSource>", "propertyBinding")
                    .AddParameter($"Func<TSource, {type}>", "convertDelegate")
                    .AddParameter("[CallerArgumentExpression(\"propertyBinding\")]string?", "propertyBindingExpression = null")
                    .WithBody(w =>
                    {
                        if (info.GenerationTypeInfo.IsSealed)
                            w.AppendLine($"return {info.PropertyName}(element, _ => _.Binding(propertyBinding, propertyBindingExpression).Convert(convertDelegate));");
                        else
                            w.AppendLine($"return {info.PropertyName}<T>(element, _ => _.Binding(propertyBinding, propertyBindingExpression).Convert(convertDelegate));");
                    });
            }
            
            CreatePropertyBuilder(ref builder, info.GenerationTypeInfo, info.PropertyName)
                .AddParameter($"Action<IDependencyPropertyBuilder<{type}>>", "configureProperty")
                .WithBody(w =>
                {
                    w.AppendLine($"var builder = DependencyPropertyBuilder<{type}>.Instance;");
                    w.AppendLine("configureProperty(builder);");
                    w.AppendLine($"builder.SetBinding(element, {info.GenerationTypeInfo.TypeFullyQualifiedName}.{info.PropertyName}Property, \"{info.PropertyName}\");");
                    w.AppendLine("return element;");
                });
                
        }
        else
        {
            if (!info.PropertyIsCollection || info.PropertyDeclaredAccessibility != Accessibility.Public)
                return;
            CreateCollectionExtension(builder, info);
        }
    }

    private void CreateCollectionExtension(ClassBuilder builder, DependencyPropertyExtensionInfo info)
    {
        EquatableArray<MethodParameterInfo> methodParameterTypes = info.MethodParameterTypes;
        string parameterName = info.PropertyName.EndsWith("s") || info.PropertyName == "Children" ? info.PropertyName.Camelcase() : info.PropertyName.Camelcase() + "s";
        parameterName = DependencyPropertyExtensionGenerator.AdjustParameterName(parameterName);
        foreach (MethodParameterInfo methodParameterInfo in methodParameterTypes)
        {
            MethodParameterInfo parameterSymbol = methodParameterInfo;
            DependencyPropertyExtensionGenerator.CreatePropertyBuilder(ref builder, info.GenerationTypeInfo, info.PropertyName).AddParameter($"params {parameterSymbol.ParameterTypeFullyQualified}[]", parameterName).WithBody(w =>
            {
                if (info.PropertyHasPublicSetter && info.PropertyTypeIsReferenceTypeOrNullableValueType && info.PropertyTypeIsInstantiable)
                    w.AppendLine($"element.{info.PropertyName} ??= new {info.PropertyTypeFullyQualified}();");
                else if (info.PropertyTypeIsReferenceTypeOrNullableValueType)
                    w.If($"element.{info.PropertyName} == null").WithBody(x => x.AppendLine($"throw new NullReferenceException(\"{info.PropertyName}\");")).EndIf();
                w.ForEach("var item", parameterName).WithBody(_ =>
                {
                    if (parameterSymbol.IsDependencyObject)
                        _.If("item is Microsoft.UI.Xaml.DependencyObject dObj").WithBody(ifWrite => ifWrite.AppendLine("ResourceObserver.SetResourceParent(dObj, element);")).EndIf();
                    if (parameterSymbol.IsReferenceTypeOrNullableValueType)
                        _.If("item is null").WithBody(ifWrite => ifWrite.AppendLine("continue;")).EndIf();
                    if (info.GenerationTypeIsFrameworkElement && parameterSymbol.IsTextElement)
                        _.If("element is Microsoft.UI.Xaml.FrameworkElement fe").WithBody(ifWrite => ifWrite.AppendLine("global::Uno.Extensions.Markup.TextElementMarkup.ApplyDataContext(fe, item);")).EndIf();
                    _.AppendLine($"element.{info.PropertyName}.Add(item);");
                });
                w.AppendLine("return element;");
            });
        }
    }

    private static MethodBuilder CreatePropertyBuilder(
      ref ClassBuilder builder,
      GenerationTypeInfo generationTypeInfo,
      string propertyName,
      bool forceStronglyTyped = false)
    {
        var parameterized = builder
            .AddMethod(propertyName)
            .MakePublicMethod()
            .MakeStaticMethod()
            .AddAttribute("global::Uno.Extensions.Markup.Internals.MarkupExtensionAttribute");

        if (generationTypeInfo.IsSealed | forceStronglyTyped)
            parameterized.
                AddParameter("this " + generationTypeInfo.TypeFullyQualifiedName, "element")
                .WithReturnType(generationTypeInfo.TypeFullyQualifiedName);
        else
            parameterized
                .AddGeneric("T", _ => _.AddConstraint(generationTypeInfo.TypeFullyQualifiedName))
                .AddParameter("this T", "element").WithReturnType("T");

        return parameterized;
    }
}
