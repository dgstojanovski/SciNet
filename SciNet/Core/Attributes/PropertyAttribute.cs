using System;

namespace SciNet.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method)]
    public sealed class PropertyAttribute : Attribute
    {
        public PropertyAttribute(Type valueType, string description)
        {
            ValueType = valueType.IsValueType
                ? valueType
                : throw new ArgumentException("Specified type must be a value type", nameof(valueType));

            Description = !string.IsNullOrWhiteSpace(description)
                ? description
                : throw new ArgumentException("A non-empty description must be provided", nameof(description));
        }

        public Type ValueType { get; }

        public string Description { get; }
    }
}