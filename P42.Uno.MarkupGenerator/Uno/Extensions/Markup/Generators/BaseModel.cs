

// Uno.Extensions.Markup.Generators.BaseModel
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Uno.Extensions.Markup.Generators;

//[GeneratedEquality]
internal abstract partial record BaseModel(GenerationTypeInfo GenerationTypeInfo)
{
    /*
    [CompilerGenerated]
    protected virtual Type EqualityContract
    {
        [CompilerGenerated]
        get
        {
            return typeof(BaseModel);
        }
    }

    [CompilerGenerated]
    public override string ToString()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("BaseModel");
        stringBuilder.Append(" { ");
        if (PrintMembers(stringBuilder))
        {
            stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
    }

    [CompilerGenerated]
    protected virtual bool PrintMembers(StringBuilder builder)
    {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("GenerationTypeInfo = ");
        builder.Append(GenerationTypeInfo.ToString());
        return true;
    }

    [CompilerGenerated]
    public override int GetHashCode()
    {
        return EqualityComparer<Type>.Default.GetHashCode(EqualityContract) * -1521134295 + EqualityComparer<GenerationTypeInfo>.Default.GetHashCode(GenerationTypeInfo);
    }

    [CompilerGenerated]
    public virtual bool Equals(BaseModel? other)
    {
        if ((object)this != other)
        {
            if ((object)other != null && EqualityContract == other!.EqualityContract)
            {
                return EqualityComparer<GenerationTypeInfo>.Default.Equals(GenerationTypeInfo, other!.GenerationTypeInfo);
            }
            return false;
        }
        return true;
    }

    [CompilerGenerated]
    protected BaseModel(BaseModel original)
    {
        GenerationTypeInfo = original.GenerationTypeInfo;
    }
    */
}
