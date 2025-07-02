// Decompiled with JetBrains decompiler
// Type: Uno.Extensions.Markup.Generators.MethodParameterInfo
// Assembly: Uno.Extensions.Markup.Generators, Version=255.255.255.255, Culture=neutral, PublicKeyToken=null
// MVID: E6210F11-717B-4BCF-8EB9-4DC107A6F4FB
// Assembly location: C:\Users\ben\AppData\Local\Temp\Zehykin\f1db9ce6ec\analyzers\dotnet\cs\Uno.Extensions.Markup.Generators.dll

using Microsoft.CodeAnalysis;
using Uno.Extensions.Markup.Generators.Extensions;

#nullable enable
namespace Uno.Extensions.Markup.Generators;

internal record struct MethodParameterInfo(
  string ParameterTypeFullyQualified,
  bool IsReferenceTypeOrNullableValueType,
  bool IsTextElement,
  bool IsDependencyObject)
{
    public static MethodParameterInfo From(ITypeSymbol type)
    {
        bool IsTextElement = false;
        for (var type1 = type; type1 != null; type1 = type1.BaseType)
        {
            if (type1.Name == "TextElement" 
                && type1.GetFullyQualifiedTypeExcludingGlobal() == "Microsoft.UI.Xaml.Documents.TextElement")
            {
                IsTextElement = true;
                break;
            }
        }
        return new MethodParameterInfo(
            type.GetFullyQualifiedTypeIncludingGlobal(), 
            type.IsReferenceTypeOrNullableValueType(), 
            IsTextElement, 
            type.IsDependencyObject());
    }
}
