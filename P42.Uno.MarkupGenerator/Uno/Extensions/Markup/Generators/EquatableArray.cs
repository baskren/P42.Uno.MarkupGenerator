// Decompiled with JetBrains decompiler
// Type: Uno.Extensions.Markup.Generators.EquatableArray
// Assembly: Uno.Extensions.Markup.Generators, Version=255.255.255.255, Culture=neutral, PublicKeyToken=null
// MVID: E6210F11-717B-4BCF-8EB9-4DC107A6F4FB
// Assembly location: C:\Users\ben\AppData\Local\Temp\Zehykin\f1db9ce6ec\analyzers\dotnet\cs\Uno.Extensions.Markup.Generators.dll

using System;
using System.Collections.Immutable;

#nullable enable
namespace Uno.Extensions.Markup.Generators;

internal static class EquatableArray
{
    public static EquatableArray<T> AsEquatableArray<T>(this ImmutableArray<T> array) where T : IEquatable<T>
    {
        return new EquatableArray<T>(array);
    }
}
