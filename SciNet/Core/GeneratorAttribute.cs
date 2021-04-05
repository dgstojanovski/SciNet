using System;

namespace SciNet.Core
{
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class GeneratorAttribute : Attribute
    {
        public GeneratorAttribute(params Type[] targetTypes)
        {
            TargetTypes = targetTypes;
        }

        public Type[] TargetTypes { get; }
    }
}