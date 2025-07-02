// Decompiled with JetBrains decompiler
// Type: Uno.Extensions.Markup.Generators.DataContextGenerator
// Assembly: Uno.Extensions.Markup.Generators, Version=255.255.255.255, Culture=neutral, PublicKeyToken=null
// MVID: E6210F11-717B-4BCF-8EB9-4DC107A6F4FB
// Assembly location: C:\Users\ben\AppData\Local\Temp\Zehykin\f1db9ce6ec\analyzers\dotnet\cs\Uno.Extensions.Markup.Generators.dll

using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using CodeGenHelpers;
using Microsoft.CodeAnalysis;

#nullable enable
namespace Uno.Extensions.Markup.Generators;

[Generator("C#", [])]
internal sealed class DataContextGenerator :
  IncrementalExtensionsGeneratorBase<DataContextExtensionInfo>
{
    private protected override void GenerateCodeFromInfosCore(
      ClassBuilder builder,
      EquatableArray<DataContextExtensionInfo> infos,
      SourceProductionContext context,
      CancellationToken cancellationToken)
    {
        string fullyQualifiedName = infos.First().GenerationTypeInfo.TypeFullyQualifiedName;

        builder
            .AddMethod("DataContext")
            .AddGeneric("T")
            .MakePublicMethod()
            .MakeStaticMethod()
            .WithReturnType(fullyQualifiedName)
            .AddParameter("this " + fullyQualifiedName, "element")
            .AddParameter($"Action<{fullyQualifiedName}, T>", "configureElement")
            .WithBody(w =>
                {
                    w.AppendUnindentedLine("#nullable disable");
                    w.AppendLine("configureElement(element, default);");
                    w.AppendUnindentedLine("#nullable enable");
                    w.AppendLine("return element;");
                });
    }

    private protected override string GetClassName(string typeName) => typeName + "Markup";

    private protected override EquatableArray<DataContextExtensionInfo>? GetInfoForType(
      INamedTypeSymbol namedType)
    {
        if (namedType.IsGenericType)
            return new EquatableArray<DataContextExtensionInfo>?();
        bool flag = false;
        for (var type = namedType; type != null; type = type.BaseType)
        {
            if (type.Name == "UserControl" && type.GetFullyQualifiedTypeExcludingGlobal() == "Microsoft.UI.Xaml.Controls.UserControl")
            {
                flag = true;
                break;
            }
        }
        return flag 
            ? new EquatableArray<DataContextExtensionInfo>?(ImmutableArray.Create(new DataContextExtensionInfo(GenerationTypeInfo.From(namedType))).AsEquatableArray()) 
            : new EquatableArray<DataContextExtensionInfo>?();
    }
}
