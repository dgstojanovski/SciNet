using System;

namespace SciNet
{
    [AttributeUsage(AttributeTargets.Class)]
    internal sealed class FactoryAttribute : Attribute
    {
        public Type ValueType { get; }

        public string Description { get; }

        public FactoryAttribute(Type valueType, string description)
        {
            ValueType = valueType.IsValueType 
                ? valueType
                : throw new ArgumentException("Specified type must be a value type", nameof(valueType));

            Description = !string.IsNullOrWhiteSpace(description)
                ? description
                : throw new ArgumentException("A non-empty description must be provided", nameof(description));
        }
    }

    [AttributeUsage(AttributeTargets.Method)]
    internal sealed class FactoryMethodAttribute : Attribute
    {
        public Type ValueType { get; }

        public string Description { get; }

        public FactoryMethodAttribute(Type valueType, string description)
        {
            ValueType = valueType.IsValueType
                ? valueType
                : throw new ArgumentException("Specified type must be a value type", nameof(valueType));

            Description = !string.IsNullOrWhiteSpace(description)
                ? description
                : throw new ArgumentException("A non-empty description must be provided", nameof(description));
        }
    }

    [AttributeUsage(AttributeTargets.Struct)]
    internal sealed class ValueTypeAttribute : Attribute
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

    [AttributeUsage(AttributeTargets.Property)]
    internal sealed class ValueTypePropertyAttribute : Attribute
    {
        public Type ValueType { get; }

        public string Description { get; }

        public ValueTypePropertyAttribute(Type valueType, string description)
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
