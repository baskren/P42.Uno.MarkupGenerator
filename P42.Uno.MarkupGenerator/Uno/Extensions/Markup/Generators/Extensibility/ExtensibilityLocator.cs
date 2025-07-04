// Decompiled with JetBrains decompiler
// Type: Uno.Extensions.Markup.Generators.Extensibility.ExtensibilityLocator
// Assembly: Uno.Extensions.Markup.Generators, Version=255.255.255.255, Culture=neutral, PublicKeyToken=null
// MVID: E6210F11-717B-4BCF-8EB9-4DC107A6F4FB
// Assembly location: C:\Users\ben\AppData\Local\Temp\Zehykin\f1db9ce6ec\analyzers\dotnet\cs\Uno.Extensions.Markup.Generators.dll

using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace Uno.Extensions.Markup.Generators.Extensibility;

internal static class ExtensibilityLocator
{
    public static ITypeExtension[] Extensions { get; } =
        [.. typeof(ExtensibilityLocator)
                .Assembly
                .GetTypes()
                .Where
                (x => 
                    typeof(P42.Uno.MarkupGenerator.Extensibility.ITypeExtension).IsAssignableFrom(x)
                    && !x.IsInterface 
                    && !x.IsAbstract
                )
                .Select(x => (ITypeExtension)Activator.CreateInstance(x))
        ];
}
