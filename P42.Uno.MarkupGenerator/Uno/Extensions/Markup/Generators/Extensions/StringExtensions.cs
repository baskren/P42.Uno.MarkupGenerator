// Decompiled with JetBrains decompiler
// Type: Uno.Extensions.Markup.Generators.Extensions.StringExtensions
// Assembly: Uno.Extensions.Markup.Generators, Version=255.255.255.255, Culture=neutral, PublicKeyToken=null
// MVID: E6210F11-717B-4BCF-8EB9-4DC107A6F4FB
// Assembly location: C:\Users\ben\AppData\Local\Temp\Zehykin\f1db9ce6ec\analyzers\dotnet\cs\Uno.Extensions.Markup.Generators.dll

using Microsoft.CodeAnalysis.CSharp;
using System.Globalization;

#nullable enable
namespace Uno.Extensions.Markup.Generators.Extensions;

public static class StringExtensions
{
    public static string Camelcase(this string str)
        => str.Length <= 1 
            ? str.ToLowerInvariant() 
            : char.ToLower(str[0], CultureInfo.InvariantCulture).ToString() + str.Substring(1);
    

    public static string EscapeIdentifier(this string identifier)
        => SyntaxFacts.GetKeywordKind(identifier) != 0 
            ? "@" + identifier 
            : identifier;
    
}
