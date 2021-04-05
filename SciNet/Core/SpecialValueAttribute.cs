using System;

namespace SciNet.Core
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class SpecialValueAttribute : Attribute
    {
        public Type ValueType { get; }

        public string Description { get; }

        public SpecialValueAttribute(Type valueType, string description)
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