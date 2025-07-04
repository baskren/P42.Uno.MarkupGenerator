using System;
using System.Collections.Generic;
using System.Text;
using CodeGenHelpers;
using Uno.Extensions.Markup.Generators;

namespace P42.Uno.MarkupGenerator.Extensibility;

internal class PointTypeExtensions : ITypeExtension
{
    const string TypeName = "global::Windows.Foundation.Point";

    public bool CanExtend(string qualifiedTypeName)
    {
        return qualifiedTypeName == TypeName;
    }

    public void WriteAttachedPropertyBuilderExtensions(AttachedPropertyInfo prop, Func<MethodBuilder> createBuilder)
    {
        createBuilder()
            .AddParameter("double", "x")
            .AddParameter("double", "y")
            .WithBody(w => w.AppendLine($"return {prop.Name}(new {TypeName}(x, y));"));
    }

    public void WriteDependencyPropertyExtensions(ClassBuilder builder, DependencyPropertyExtensionInfo info, Func<MethodBuilder> createBuilder)
    {
        createBuilder()
            .AddParameter("double", "x")
            .AddParameter("double", "y").WithBody(w =>
            {
                w.AppendLine($"element.{info.PropertyName} = new {TypeName}(x, y);");
                w.AppendLine("return element;");
            });
    }

    public void WriteStyleBuilderExtensions(ClassBuilder builder, StyleBuilderInfo info, Func<MethodBuilder> createBuilder)
    {
        createBuilder()
            .AddParameter("double", "x")
            .AddParameter("double", "y")
            .WithBody(w =>
            {
                w.AppendLine($"builder.{info.PropertyName}(new {TypeName}(x, y));");
                w.AppendLine("return builder;");
            });
    }
}
