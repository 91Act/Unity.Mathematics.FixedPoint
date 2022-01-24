using System;
using System.Globalization;
using System.IO;
using System.Threading;
using Unity.Mathematics.FixedPoint;

namespace Unity.Mathematics.Mathematics.CodeGen
{
    class MainClass
    {
        public static void GenerateASinLUT(string directory)
        {
            using (var writer = new StreamWriter(Path.Combine(directory, "FpLUTASine.cs")))
            {
                writer.Write(
                    @"namespace Unity.Mathematics.FixedPoint
{
    partial struct Fp
    {
        public static readonly long[] ASinLUT = new long[]
        {");
                int lineCounter = 0;
                for (int i = 0; i < Fp.LUTSize; ++i)
                {
                    // [-1, 1]
                    var value = 2.0f * i / (Fp.LUTSize - 1) - 1.0f;
                    if (lineCounter++ % 8 == 0)
                    {
                        writer.WriteLine();
                        writer.Write("            ");
                    }

                    var asin = Math.Asin(value);
                    var rawValue = ((Fp)asin).value;
                    if (rawValue > 0)
                    {
                        writer.Write("0x{0:X}L, ", rawValue);
                    }
                    else
                    {
                        writer.Write("-0x{0:X}L, ", -rawValue);
                    }
                }

                writer.Write(
                    @"
        };
    }
}");
            }
        }

        public static void GenerateSinLUT(string directory)
        {
            using (var writer = new StreamWriter(Path.Combine(directory, "FpLUTSine.cs")))
            {
                writer.Write(
                    @"namespace Unity.Mathematics.FixedPoint
{
    partial struct Fp
    {
        public static readonly long[] SinLUT = new[]
        {");
                int lineCounter = 0;
                for (int i = 0; i < Fp.LUTSize; ++i)
                {
                    // [0, Pi/2]
                    var angle = i * Math.PI * 0.5 / (Fp.LUTSize - 1);
                    if (lineCounter++ % 8 == 0)
                    {
                        writer.WriteLine();
                        writer.Write("            ");
                    }

                    var sin = Math.Sin(angle);
                    var rawValue = ((Fp)sin).value;
                    writer.Write("0x{0:X}L, ", rawValue);
                }

                writer.Write(
                    @"
        };
    }
}");
            }
        }

        public static void GenerateTanLUT(string directory)
        {
            using (var writer = new StreamWriter(Path.Combine(directory, "FpLUTTangent.cs")))
            {
                writer.Write(
                    @"namespace Unity.Mathematics.FixedPoint
{
    partial struct Fp
    {
        public static readonly long[] TanLUT = new[]
        {");
                int lineCounter = 0;
                for (int i = 0; i < Fp.LUTSize; ++i)
                {
                    // [0, Pi/2]
                    var angle = i * Math.PI * 0.5 / (Fp.LUTSize - 1);
                    if (lineCounter++ % 8 == 0)
                    {
                        writer.WriteLine();
                        writer.Write("            ");
                    }

                    var tan = Math.Tan(angle);
                    var rawValue = (((decimal)tan > (decimal)Fp.MaxValue.value / (1L << 32) || tan < 0.0) ? Fp.MaxValue : (Fp)tan).value;
                    writer.Write("0x{0:X}L, ", rawValue);
                }

                writer.Write(
                    @"
        };
    }
}");
            }
        }

        public static void GenerateExpLUT(string directory)
        {
            using (var writer = new StreamWriter(Path.Combine(directory, "FpLUTExponent.cs")))
            {
                writer.Write(
                    @"namespace Unity.Mathematics.FixedPoint
{
    partial struct Fp
    {
        public static readonly long[] ExpLUT = new[]
        {");
                int lineCounter = 0;
                for (int i = 0; i < Fp.LUTSize; ++i)
                {
                    if (lineCounter++ % 8 == 0)
                    {
                        writer.WriteLine();
                        writer.Write("            ");
                    }
                    // [-1, 3]
                    var exp = Math.Exp(4.0f * i / (double)(Fp.LUTSize - 1) - 1.0f);
                    var rawValue = (((decimal)exp > (decimal)Fp.MaxValue.value / (1L << 32) || exp < 0.0) ? Fp.MaxValue : (Fp)exp).value;
                    writer.Write("0x{0:X}L, ", rawValue);
                }

                writer.Write(
                    @"
        };
    }
}");
            }
        }

        public static void Main(string[] args)
        {
            var root = AppDomain.CurrentDomain.BaseDirectory;
            var index = root.IndexOf("FixedPointMath\\Plugins", StringComparison.Ordinal);
            var dir = root.Substring(0, index + "FixedPointMath".Length);
            if (!Directory.Exists(dir))
            {
                Console.Error.WriteLine(
                    $"ERROR : Root path {dir} don't exist, Please change it to a proper root path.");
                Console.ReadKey();
                return;
            }

            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");


            var implementationDirectory =
                new DirectoryInfo(Path.Combine(dir, "Assets/FixedPointMath/FixedPoint"));
            if (!implementationDirectory.Exists)
            {
                throw new InvalidOperationException($"The directory `{implementationDirectory.FullName}` must exist");
            }

            var testDirectory = new DirectoryInfo(Path.Combine(dir, "Assets/Tests/"));
            if (!implementationDirectory.Exists)
            {
                throw new InvalidOperationException($"The directory `{testDirectory.FullName}` must exist");
            }

            Console.WriteLine("Generating LUT!");
            string FpGeneratedPath = Path.Combine(implementationDirectory.FullName, "Generated");
            if (!Directory.Exists(FpGeneratedPath))
            {
                Directory.CreateDirectory(FpGeneratedPath);
            }

            GenerateASinLUT(FpGeneratedPath);
            GenerateSinLUT(FpGeneratedPath);
            GenerateTanLUT(FpGeneratedPath);
            GenerateExpLUT(FpGeneratedPath);
            Console.WriteLine("Done!!\n\n\n");
            Console.WriteLine("Generating swizzle and operators!");
            string testGeneratedPath = Path.Combine(testDirectory.FullName, "Generated");
            if (!Directory.Exists(testGeneratedPath))
            {
                Directory.CreateDirectory(testGeneratedPath);
            }

            VectorGenerator.Write(FpGeneratedPath, testGeneratedPath);
            Console.WriteLine("Done!!");
            // Console.ReadKey();
        }
    }
}