// Decompiled with JetBrains decompiler
// Type: Uno.Extensions.Markup.Generators.ImmutableArrayComparer
// Assembly: Uno.Extensions.Markup.Generators, Version=255.255.255.255, Culture=neutral, PublicKeyToken=null
// MVID: E6210F11-717B-4BCF-8EB9-4DC107A6F4FB
// Assembly location: C:\Users\ben\AppData\Local\Temp\Zehykin\f1db9ce6ec\analyzers\dotnet\cs\Uno.Extensions.Markup.Generators.dll

using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;

#nullable enable
namespace Uno.Extensions.Markup.Generators;

internal sealed class ImmutableArrayComparer : IEqualityComparer<ImmutableArray<IAssemblySymbol>>
{
    public static IEqualityComparer<ImmutableArray<IAssemblySymbol>> Instance { get; } = (IEqualityComparer<ImmutableArray<IAssemblySymbol>>)new ImmutableArrayComparer();

    private ImmutableArrayComparer()
    {
    }

    public bool Equals(ImmutableArray<IAssemblySymbol> x, ImmutableArray<IAssemblySymbol> y)
    {
        if (x.Length != y.Length)
            return false;
        for (int index = 0; index < x.Length; ++index)
        {
            if (!x[index].Equals(y[index], SymbolEqualityComparer.Default))
                return false;
        }
        return true;
    }

    public int GetHashCode(ImmutableArray<IAssemblySymbol> obj)
    {
        HashCode hashCode = new();
        foreach (IAssemblySymbol iAssemblySymbol in obj)
            hashCode.Add(iAssemblySymbol);
        return hashCode.ToHashCode();
    }
}
