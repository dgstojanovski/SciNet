# SciNet

## Quickstart
To execute the unit tests, simply run `dotnet test` in the solution directory:
```
PS C:\Repositories\SciNet> dotnet test
  Determining projects to restore...
  All projects are up-to-date for restore.
  SciNet -> D:\Repositories\SciNet\SciNet\bin\Debug\net5.0\SciNet.dll
  SciNet.Tests -> D:\Repositories\SciNet\SciNet.Tests\bin\Debug\net5.0\SciNet.Tests.dll
Test run for D:\Repositories\SciNet\SciNet.Tests\bin\Debug\net5.0\SciNet.Tests.dll (.NETCoreApp,Version=v5.0)
Microsoft (R) Test Execution Command Line Tool Version 16.9.1
Copyright (c) Microsoft Corporation.  All rights reserved.

Starting test execution, please wait...
A total of 1 test files matched the specified pattern.

Passed!  - Failed:     0, Passed:   114, Skipped:     0, Total:   114, Duration: 403 ms - SciNet.Tests.dll (net5.0)
```

To generate the documentation, run the project `SciNet.Generator` with a single argument specifying the output directory:
```
PS C:\Repositories\SciNet> dotnet run --project SciNet.Generator C:\Working\SciNet
Executed DefinitionMarkdownGenerator for SciNet.Mathematics.Complex: C:\Working\SciNet\Documentation\Definitions\Complex.md
Executed DefinitionMarkdownGenerator for SciNet.Mathematics.Matrix: C:\Working\SciNet\Documentation\Definitions\Matrix.md
Executed DefinitionMarkdownGenerator for SciNet.Mathematics.Real: C:\Working\SciNet\Documentation\Definitions\Real.md
Executed DefinitionMarkdownGenerator for SciNet.Mathematics.Vector: C:\Working\SciNet\Documentation\Definitions\Vector.md
Executed ValueMarkdownGenerator for SciNet.Mathematics.ComplexValue: C:\Working\SciNet\Documentation\Values\ComplexValue.md
Executed ValueMarkdownGenerator for SciNet.Mathematics.MatrixValue: C:\Working\SciNet\Documentation\Values\MatrixValue.md
Executed ValueMarkdownGenerator for SciNet.Mathematics.RealValue: C:\Working\SciNet\Documentation\Values\RealValue.md
Executed ValueMarkdownGenerator for SciNet.Mathematics.VectorValue: C:\Working\SciNet\Documentation\Values\VectorValue.md
```
