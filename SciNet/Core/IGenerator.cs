using System;
using System.Collections.Generic;
using System.IO;

namespace SciNet.Core
{
    public interface IGenerator<T> : IGenerator where T : Attribute
    {
    }

    public interface IGenerator
    {
        IEnumerable<FileInfo> Generate(Type type);
    }
}