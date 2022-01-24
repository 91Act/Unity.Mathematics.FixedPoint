# Unity FixedPoint Mathematics

## 前言

这是一个基于 [Unity.Mathematics](https://github.com/Unity-Technologies/Unity.Mathematics) 和 [FixedMath.Net](https://github.com/asik/FixedMath.Net) 的 unity 定点数数学库，用于解决不同平台浮点运算不一致的问题，满足确定性计算需要。直到 unity 的 burst/DOTS 支持确定性计算。

API尽量保持与Unity.Mathematics一致，方便在两边库做切换。

代码生成的工程 : /Plugins/Unity.Mathematics.FixedPoint.CodeGen/FixedPoint.CodeGen.csproj

## 使用

通过在 `PROJECT_ROOT/Packages/manifest.json` 中添加以下内容，将包导入到你的 unity 工程中：
```json
{
    "dependencies": {
        "com.91act.noah.fixed.point.math": "https://github.com/91Act/Unity.Mathematics.FixedPoint.git"
    }
}
```

## 测试和开发

把这个仓库和[测试仓库](https://github.com/91Act/Unity.Mathematics.FixedPoint.Tests)一起拷贝到某个 Unity Project 的 Assets 文件夹下。

## 缺失功能

以下功能是缺失的：

- MathFp.tanh
- MathFp.cosh
- MathFp.sinh
- MathFp.log10

虽然有这些函数的API，但内容被标记为过时的。

## 相对 Unity.Mathematics 的修改


Unity.Mathematics 使用代码生成来创建它的 vector 和 matrix 结构，本仓库大部分修改内容集中在`VectorGenerator.cs`、`MathFp.cs`。

原本打算尽可能保持与 Unity.Mathematics 一致，但是 `VectorGenerator.cs` 的代码本身拓展性不强，所以如果之后 Unity.Mathematics 有新版本，还是需要手动合并这些修改。 


`MathFp.cs` 包含常用的定点数计算操作，它的实现是基于 Unity.Mathematics 的浮点版本的 `math.cs`。

## 精度

需要增加更多的测试来覆盖当前定点数计算过程的精度。目前，有些计算过程的实现可能是不适合定点数的。

## 开源许可

当前仓库基于 MIT License ([LICENSE.md](LICENSE.md))

Unity.Mathematics ([Unity Companion License](https://github.com/Unity-Technologies/Unity.Mathematics/blob/master/LICENSE.md))

FixedMath.Net ([Apache License, Version 2.0](LICENSE_Fp.txt))
