using System;

namespace SciNet
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class FactoryAttribute : Attribute
    {
        internal Type ValueType { get; }

        internal string Description { get; }

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
    public sealed class FactoryMethodAttribute : Attribute
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

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method)]
    public sealed class ValuePropertyAttribute : Attribute
    {
        public Type ValueType { get; }

        public string Description { get; }

        public ValuePropertyAttribute(Type valueType, string description)
        {
            ValueType = valueType.IsValueType
                ? valueType
                : throw new ArgumentException("Specified type must be a value type", nameof(valueType));

            Description = !string.IsNullOrWhiteSpace(description)
                ? description
                : throw new ArgumentException("A non-empty description must be provided", nameof(description));
        }
    }
    
    [AttributeUsage(AttributeTargets.Enum)]
    public sealed class ValuePropertyOptionAttribute : Attribute
    {
        public Type ValueType { get; }

        public string Description { get; }

        public ValuePropertyOptionAttribute(Type valueType, string description)
        {
            ValueType = valueType.IsValueType
                ? valueType
                : throw new ArgumentException("Specified type must be a value type", nameof(valueType));

            Description = !string.IsNullOrWhiteSpace(description)
                ? description
                : throw new ArgumentException("A non-empty description must be provided", nameof(description));
        }
    }
    
    [AttributeUsage(AttributeTargets.Enum)]
    public sealed class ValuePrototypeAttribute : Attribute
    {
        public Type ValueType { get; }

        public string Description { get; }

        public ValuePrototypeAttribute(Type valueType, string description)
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
