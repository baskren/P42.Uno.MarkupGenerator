// Decompiled with JetBrains decompiler
// Type: Uno.Extensions.Markup.Generators.GenerationTypeInfo
// Assembly: Uno.Extensions.Markup.Generators, Version=255.255.255.255, Culture=neutral, PublicKeyToken=null
// MVID: E6210F11-717B-4BCF-8EB9-4DC107A6F4FB
// Assembly location: C:\Users\ben\AppData\Local\Temp\Zehykin\f1db9ce6ec\analyzers\dotnet\cs\Uno.Extensions.Markup.Generators.dll

using Microsoft.CodeAnalysis;
using System.Runtime.CompilerServices;
using Uno.Extensions.Markup.Generators.Extensions;

#nullable enable
namespace Uno.Extensions.Markup.Generators;

internal record struct GenerationTypeInfo(
  string TypeFullyQualifiedName,
  string TypeName,
  string TypeContainingNamespace,
  Accessibility DeclaredAccessibility,
  bool IsSealed)
{
    public static GenerationTypeInfo From(INamedTypeSymbol typeSymbol)
        => new(
            typeSymbol.GetFullyQualifiedTypeIncludingGlobal(), 
            typeSymbol.Name, 
            typeSymbol.ContainingNamespace.ToDisplayString(null), 
            typeSymbol.DeclaredAccessibility, 
            typeSymbol.IsSealed);
    
}
