using System;

namespace SciNet.Core
{
    [AttributeUsage(AttributeTargets.Struct)]
    public sealed class ValueTypeAttribute : Attribute
    {
        public Type ValueType { get; }

        public string Description { get; }

        public ValueTypeAttribute(Type valueType, string description)
        {
            ValueType = valueType.IsValueType
                ? valueType
                : throw new ArgumentException("Specified type must be a value type", nameof(valueType));

            Description = !string.IsNullOrWhiteSpace(description)
                ? description
                : throw new ArgumentException("A non-empty description must be provided", nameof(description));
        }
    }
}