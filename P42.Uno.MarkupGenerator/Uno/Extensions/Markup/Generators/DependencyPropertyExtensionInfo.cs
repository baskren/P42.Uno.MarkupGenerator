using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.CodeAnalysis;
using Uno.Extensions.Markup.Generators.Extensions;

#nullable enable
namespace Uno.Extensions.Markup.Generators;

//[GeneratedEquality]
internal record DependencyPropertyExtensionInfo
    (
        string PropertyTypeFullyQualified, 
        string PropertyName, 
        bool PropertyHasPublicSetter, 
        bool PropertyIsCollection, 
        bool PropertyIsDependencyObjectButNotFrameworkElement, 
        bool PropertyTypeIsReferenceTypeOrNullableValueType, 
        bool PropertyTypeIsInstantiable, 
        bool GenerationTypeIsFrameworkElement, 
        bool IsTemplateType, 
        Accessibility PropertyDeclaredAccessibility, 
        bool IsDependencyProperty, 
        EquatableArray<MethodParameterInfo> MethodParameterTypes, 
        bool IsNotSealedAndIsShadowing, 
        GenerationTypeInfo GenerationTypeInfo
    ) : BaseModel(GenerationTypeInfo)
{

    public static DependencyPropertyExtensionInfo From(
          IPropertySymbol propertySymbol,
          GenerationTypeInfo typeInfo,
          bool isDependencyProperty,
          bool generationTypeIsFrameworkElement,
          bool isNotSealedAndIsShadowing)
    {
        var type = propertySymbol.Type;
        bool flag1 = false;
        bool flag2 = false;
        for (; type != null; type = type.BaseType)
        {
            var typeExcludingGlobal = type.GetFullyQualifiedTypeExcludingGlobal();
            if (type.Name == "FrameworkElement" && typeExcludingGlobal == "Microsoft.UI.Xaml.FrameworkElement")
            {
                flag2 = true;
                flag1 = true;
                break;
            }
            if (type.Name == "DependencyObject" && typeExcludingGlobal == "Microsoft.UI.Xaml.DependencyObject")
            {
                flag1 = true;
                break;
            }
        }

        if (!flag1)
            flag1 = ImmutableArrayExtensions.Any(propertySymbol.Type.AllInterfaces, x => x.Name == "DependencyObject" && x.GetFullyQualifiedTypeExcludingGlobal() == "Microsoft.UI.Xaml.DependencyObject");

        bool PropertyIsCollection = propertySymbol.IsCollectionWithAddMethod();
        string typeExcludingGlobal1 = propertySymbol.Type.GetFullyQualifiedTypeExcludingGlobal();
        bool IsTemplateType = (propertySymbol.Type as INamedTypeSymbol).IsFrameworkTemplate() || typeExcludingGlobal1 == "Microsoft.UI.Xaml.Controls.DataTemplateSelector";
        return new DependencyPropertyExtensionInfo(
            propertySymbol.Type.GetFullyQualifiedTypeIncludingGlobal(), 
            propertySymbol.Name, propertySymbol.HasPublicSetter(), 
            PropertyIsCollection, 
            flag1 && !flag2, 
            propertySymbol.Type.IsReferenceTypeOrNullableValueType(), 
            propertySymbol.Type.IsInstantiable(), 
            generationTypeIsFrameworkElement, 
            IsTemplateType, 
            propertySymbol.DeclaredAccessibility, 
            isDependencyProperty, 
            PropertyIsCollection 
                ? DependencyPropertyExtensionInfo.GetAddMethodParameterTypes(propertySymbol) 
                : new EquatableArray<MethodParameterInfo>(), isNotSealedAndIsShadowing, typeInfo
            );
    }

    private static EquatableArray<MethodParameterInfo> GetAddMethodParameterTypes(IPropertySymbol property)
    {
        if (property.Name == "DateBase")
            return ImmutableArray<MethodParameterInfo>.Empty.AsEquatableArray();

        var builder = ImmutableHashSet.CreateBuilder<MethodParameterInfo>();
        for (var iTypeSymbol = property.Type; iTypeSymbol != null; iTypeSymbol = iTypeSymbol.BaseType)
        {
            foreach (MethodParameterInfo methodParameterInfo in iTypeSymbol.GetMembers("Add").OfType<IMethodSymbol>().Where(x => x.Parameters.Length == 1).Select(x => MethodParameterInfo.From(x.Parameters[0].Type)))
                builder.Add(methodParameterInfo);
        }

        if (property.Type.TypeKind == TypeKind.Interface)
        {
            foreach (var allInterface in property.Type.AllInterfaces)
            {
                foreach (var iMethodSymbol in allInterface.GetMembers("Add").OfType<IMethodSymbol>().Where(x => x.Parameters.Length == 1))
                    builder.Add(MethodParameterInfo.From(iMethodSymbol.Parameters[0].Type));
            }
        }

        return ImmutableArray.ToImmutableArray(builder).AsEquatableArray();
    }

}
