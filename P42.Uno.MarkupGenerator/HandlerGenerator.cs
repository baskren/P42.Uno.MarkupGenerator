using System;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace P42.Uno.MarkupGenerator;

[Generator]
public class HandlerGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var provider = context.SyntaxProvider.CreateSyntaxProvider
            (
                predicate: (c,_) => c is ClassDeclarationSyntax,
                transform: (n, _) => (ClassDeclarationSyntax)n.Node
            ).Where(m => m is not null);

        var compilation = context.CompilationProvider.Combine(provider.Collect());

        context.RegisterSourceOutput(compilation,
            (spc, source) => Execute(spc, source.Left, source.Right));
        
    }

    private void Execute(SourceProductionContext context, 
        Compilation compilation, 
        ImmutableArray<ClassDeclarationSyntax> typeList)
    {
        var code = """
            namespace SampleSourceGenerator;

            public static class ClassNames
            {
                public static string Names = "Hello from Roslyn";
            }
            """;

        context.AddSource("ClassName.g.cs", code);
    }
}
