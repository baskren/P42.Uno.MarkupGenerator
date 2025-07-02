

// Uno.Extensions.Markup.Generators.AttachedPropertyInfo
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Uno.Extensions.Markup.Generators;

//[GeneratedEquality]
internal partial record AttachedPropertyInfo(string Name, string PropertyTypeFullyQualified, bool PropertyTypeIsNullableValueType, string DependencyProperty, GenerationTypeInfo GenerationTypeInfo) : BaseModel(GenerationTypeInfo)
{

}
