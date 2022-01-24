using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Unity.Mathematics.Mathematics.CodeGen
{
    class VectorGenerator
    {
        private string _mImplementationDirectory;
        private string _mTestDirectory;
        private string _mBaseType;
        private string _mTypeName;
        private int _mRows;
        private int _mColumns;
        private Features _mFeatures;

        private uint[] m_primes =
        {
            0x6E624EB7u, 0x7383ED49u, 0xDD49C23Bu, 0xEBD0D005u, 0x91475DF7u, 0x55E84827u, 0x90A285BBu, 0x5D19E1D5u,
            0xFAAF07DDu, 0x625C45BDu, 0xC9F27FCBu, 0x6D2523B1u, 0x6E2BF6A9u, 0xCC74B3B7u, 0x83B58237u, 0x833E3E29u,
            0xA9D919BFu, 0xC3EC1D97u, 0xB8B208C7u, 0x5D3ED947u, 0x4473BBB1u, 0xCBA11D5Fu, 0x685835CFu, 0xC3D32AE1u,
            0xB966942Fu, 0xFE9856B3u, 0xFA3A3285u, 0xAD55999Du, 0xDCDD5341u, 0x94DDD769u, 0xA1E92D39u, 0x4583C801u,
            0x9536A0F5u, 0xAF816615u, 0x9AF8D62Du, 0xE3600729u, 0x5F17300Du, 0x670D6809u, 0x7AF32C49u, 0xAE131389u,
            0x5D1B165Bu, 0x87096CD7u, 0x4C7F6DD1u, 0x4822A3E9u, 0xAAC3C25Du, 0xD21D0945u, 0x88FCAB2Du, 0x614DA60Du,
            0x5BA2C50Bu, 0x8C455ACBu, 0xCD266C89u, 0xF1852A33u, 0x77E35E77u, 0x863E3729u, 0xE191B035u, 0x68586FAFu,
            0xD4DFF6D3u, 0xCB634F4Du, 0x9B13B92Du, 0x4ABF0813u, 0x86068063u, 0xD75513F9u, 0x5AB3E8CDu, 0x676E8407u,
            0xB36DE767u, 0x6FCA387Du, 0xAF0F3103u, 0xE4A056C7u, 0x841D8225u, 0xC9393C7Du, 0xD42EAFA3u, 0xD9AFD06Du,
            0x97A65421u, 0x7809205Fu, 0x9C9F0823u, 0x5A9CA13Bu, 0xAFCDD5EFu, 0xA88D187Du, 0xCF6EBA1Du, 0x9D88E5A1u,
            0xEADF0775u, 0x747A9D7Bu, 0x4111F799u, 0xB5F05AF1u, 0xFD80290Bu, 0x8B65ADB7u, 0xDFF4F563u, 0x7069770Du,
            0xD1224537u, 0xE99ED6F3u, 0x48125549u, 0xEEE2123Bu, 0xE3AD9FE5u, 0xCE1CF8BFu, 0x7BE39F3Bu, 0xFAB9913Fu,
            0xB4501269u, 0xE04B89FDu, 0xDB3DE101u, 0x7B6D1B4Bu, 0x58399E77u, 0x5EAC29C9u, 0xFC6014F9u, 0x6BF6693Fu,
            0x9D1B1D9Bu, 0xF842F5C1u, 0xA47EC335u, 0xA477DF57u, 0xC4B1493Fu, 0xBA0966D3u, 0xAFBEE253u, 0x5B419C01u,
            0x515D90F5u, 0xEC9F68F3u, 0xF9EA92D5u, 0xC2FAFCB9u, 0x616E9CA1u, 0xC5C5394Bu, 0xCAE78587u, 0x7A1541C9u,
            0xF83BD927u, 0x6A243BCBu, 0x509B84C9u, 0x91D13847u, 0x52F7230Fu, 0xCF286E83u, 0xE121E6ADu, 0xC9CA1249u,
            0x69B60C81u, 0xE0EB6C25u, 0xF648BEABu, 0x6BDB2B07u, 0xEF63C699u, 0x9001903Fu, 0xA895B9CDu, 0x9D23B201u,
            0x4B01D3E1u, 0x7461CA0Du, 0x79725379u, 0xD6258E5Bu, 0xEE390C97u, 0x9C8A2F05u, 0x4DDC6509u, 0x7CF083CBu,
            0x5C4D6CEDu, 0xF9137117u, 0xE857DCE1u, 0xF62213C5u, 0x9CDAA959u, 0xAA269ABFu, 0xD54BA36Fu, 0xFD0847B9u,
            0x8189A683u, 0xB139D651u, 0xE7579997u, 0xEF7D56C7u, 0x66F38F0Bu, 0x624256A3u, 0x5292ADE1u, 0xD2E590E5u,
            0xF25BE857u, 0x9BC17CE7u, 0xC8B86851u, 0x64095221u, 0xADF428FFu, 0xA3977109u, 0x745ED837u, 0x9CDC88F5u,
            0xFA62D721u, 0x7E4DB1CFu, 0x68EEE0F5u, 0xBC3B0A59u, 0x816EFB5Du, 0xA24E82B7u, 0x45A22087u, 0xFC104C3Bu,
            0x5FFF6B19u, 0x5E6CBF3Bu, 0xB546F2A5u, 0xBBCF63E7u, 0xC53F4755u, 0x6985C229u, 0xE133B0B3u, 0xC3E0A3B9u,
            0xFE31134Fu, 0x712A34D7u, 0x9D77A59Bu, 0x4942CA39u, 0xB40EC62Du, 0x565ED63Fu, 0x93C30C2Bu, 0xDCAF0351u,
            0x6E050B01u, 0x750FDBF5u, 0x7F3DD499u, 0x52EAAEBBu, 0x4599C793u, 0x83B5E729u, 0xC267163Fu, 0x67BC9149u,
            0xAD7C5EC1u, 0x822A7D6Du, 0xB492BF15u, 0xD37220E3u, 0x7AA2C2BDu, 0xE16BC89Du, 0x7AA07CD3u, 0xAF642BA9u,
            0xA8F2213Bu, 0x9F3FDC37u, 0xAC60D0C3u, 0x9263662Fu, 0xE69626FFu, 0xBD010EEBu, 0x9CEDE1D1u, 0x43BE0B51u,
            0xAF836EE1u, 0xB130C137u, 0x54834775u, 0x7C022221u, 0xA2D00EDFu, 0xA8977779u, 0x9F1C739Bu, 0x4B1BD187u,
            0x9DF50593u, 0xF18EEB85u, 0x9E19BFC3u, 0x8196B06Fu, 0xD24EFA19u, 0x7D8048BBu, 0x713BD06Fu, 0x753AD6ADu,
            0xD19764C7u, 0xB5D0BF63u, 0xF9102C5Fu, 0x9881FB9Fu, 0x56A1530Du, 0x804B722Du, 0x738E50E5u, 0x4FC93C25u,
            0xCD0445A5u, 0xD2B90D9Bu, 0xD35C9B2Du, 0xA10D9E27u, 0x568DAAA9u, 0x7530254Fu, 0x9F090439u, 0x5E9F85C9u,
            0x8C4CA03Fu, 0xB8D969EDu, 0xAC5DB57Bu, 0xA91A02EDu, 0xB3C49313u, 0xF43A9ABBu, 0x84E7E01Bu, 0x8E055BE5u
        };

        private uint _mNextPrime;

        public static void Write(string implementationDirectory, string testDirectory)
        {
            VectorGenerator vectorGenerator = new VectorGenerator();
            vectorGenerator._mImplementationDirectory = implementationDirectory;
            vectorGenerator._mTestDirectory = testDirectory;

            for (int rows = 1; rows <= 4; rows++)
            {
                for (int columns = 1; columns <= 4; columns++)
                {
                    if (rows == 1 && columns == 1) // don't generate type1x1
                        continue;


                    if (rows == 1) // ignore row vectors for now
                        continue;

                    vectorGenerator.WriteType("Fp", rows, columns, Features.Arithmetic | Features.UnaryNegation);
                    vectorGenerator.WriteType("int", rows, columns,
                        Features.Arithmetic | Features.UnaryNegation | Features.BitwiseLogic |
                        Features.BitwiseComplement | Features.Shifts);
                    vectorGenerator.WriteType("uint", rows, columns,
                        Features.Arithmetic | Features.UnaryNegation | Features.BitwiseLogic |
                        Features.BitwiseComplement | Features.Shifts);
                    // vectorGenerator.WriteType("ulong", rows, columns,
                    //     Features.Arithmetic | Features.BitwiseLogic | Features.BitwiseComplement | Features.Shifts);
                    vectorGenerator.WriteType("bool", rows, columns, Features.BitwiseLogic);
                }
            }

            vectorGenerator.WriteMatrix();
            vectorGenerator.WriteMath();
        }

        private uint NextPrime()
        {
            return m_primes[_mNextPrime++ & 255]; //TODO: fix
        }

        [Flags]
        private enum Features
        {
            None = 0,
            Arithmetic = 1 << 0,
            Shifts = 1 << 1,
            BitwiseLogic = 1 << 2,
            BitwiseComplement = 1 << 3,
            UnaryNegation = 1 << 4,
            // All = Arithmetic | Shifts | BitwiseLogic | BitwiseComplement | UnaryNegation
        }

        static readonly string[] Components = { "x", "y", "z", "w" };
        static readonly string[] VectorFields = { "x", "y", "z", "w" };
        static readonly string[] MatrixFields = { "c0", "c1", "c2", "c3" };

        static readonly string[] ShuffleComponents =
            { "LeftX", "LeftY", "LeftZ", "LeftW", "RightX", "RightY", "RightZ", "RightW" };

        [StructLayout(LayoutKind.Explicit)]
        internal struct UIntFloatUnion
        {
            [FieldOffset(0)] public uint uintValue;
            [FieldOffset(0)] public float floatValue;
        }

        [StructLayout(LayoutKind.Explicit)]
        internal struct ULongDoubleUnion
        {
            [FieldOffset(0)] public ulong ulongValue;
            [FieldOffset(0)] public double doubleValue;
        }

        public static uint AsUInt(float x)
        {
            UIntFloatUnion u;
            u.uintValue = 0;
            u.floatValue = x;
            return u.uintValue;
        }

        public static ulong AsULong(double x)
        {
            ULongDoubleUnion u;
            u.ulongValue = 0;
            u.doubleValue = x;
            return u.ulongValue;
        }

        public static string ToTypeName(string baseType, int rows, int columns)
        {
            string name = baseType;
            if (rows == 1 && columns > 1)
                return name + columns; // row vector

            if (rows > 1)
                name += rows;
            if (columns > 1)
                name += "x" + columns;
            return name;
        }


        public enum VectorType
        {
            Row,
            Column,
            DontCare
        }

        public static string ToValueDescription(string baseType, int rows, int columns, int n,
            bool addRowColumnVectorPrefix = false)
        {
            string name = ToTypeName(baseType, rows, columns);

            string numStr = "";
            switch (n)
            {
                case 1:
                    numStr = baseType == "int" ? "an " : "a ";
                    break;
                case 2:
                    numStr = "two ";
                    break;
                case 3:
                    numStr = "three ";
                    break;
                case 4:
                    numStr = "four ";
                    break;
            }

            string vectorPrefix =
                addRowColumnVectorPrefix ? ((rows == 1) ? " row" : (columns == 1) ? " column" : "") : "";

            if (n > 1)
                return numStr + name + ((rows == 1 && columns == 1) ? " values" :
                    (rows > 1 && columns > 1) ? " matrices" : vectorPrefix + " vectors");
            return numStr + name + ((rows == 1 && columns == 1) ? " value" :
                (rows > 1 && columns > 1) ? " matrix" : vectorPrefix + " vector");
        }

        public static string ToTypedLiteral(string baseType, int value)
        {
            switch (baseType)
            {
                case "int":
                    return "" + value;
                case "uint":
                    return "" + value + "u";
                case "half":
                case "float":
                    return "" + value + ".0f";
                case "double":
                    return "" + value + ".0";
                case "Fp":
                    return "(Fp)" + value;
                default:
                    return "";
            }
        }

        private static string UpperCaseFirstLetter(string s)
        {
            return char.ToUpper(s[0]) + s.Substring(1);
        }

        private static string s_AutoGenHeader =
            "//------------------------------------------------------------------------------\n" +
            "// <auto-generated>\n" +
            "//     This code was generated by a tool.\n" +
            "//\n" +
            "//     Changes to this file may cause incorrect behavior and will be lost if\n" +
            "//     the code is regenerated.\n" +
            "// </auto-generated>\n" +
            "//------------------------------------------------------------------------------\n";

        private static void WriteFile(string filename, string text)
        {
            // Convert all tabs to spaces
            text = text.Replace("\t", "    ");

            // Generate auto generated comment
            text = s_AutoGenHeader + text;
            // Normalize line endings, convert all EOL to platform EOL (and let git handle it)
            text = text.Replace("\r\n", "\n");
            text = text.Replace("\n", Environment.NewLine);

            File.WriteAllText(filename, text);
        }

        private void WriteMatrix()
        {
            StringBuilder str = new StringBuilder();
            GenerateMatrixImplementation(str);
            WriteFile(_mImplementationDirectory + "/Matrix.gen.cs", str.ToString());
        }

        private void WriteType(string baseType, int rows, int columns, Features features)
        {
            _mBaseType = baseType;
            _mTypeName = ToTypeName(baseType, rows, columns);
            _mRows = rows;
            _mColumns = columns;
            _mFeatures = features;

            // implementation
            StringBuilder str = new StringBuilder();
            GenerateTypeImplementation(str);
            WriteFile(_mImplementationDirectory + "/" + _mTypeName + ".gen.cs", str.ToString());


            if (_mBaseType != "half" && _mBaseType != "ulong")
            {
                str = new StringBuilder();
                GenerateTypeTests(str);
                WriteFile(_mTestDirectory + "/Test" + UpperCaseFirstLetter(_mTypeName) + ".gen.cs", str.ToString());
            }
        }

        private void WriteMath()
        {
            StringBuilder str = new StringBuilder();
            GenerateMathTests(str);
            WriteFile(_mTestDirectory + "/TestMath.gen.cs", str.ToString());
        }


        private void GenerateMemberVariables(StringBuilder str)
        {
            if (_mColumns > 1)
            {
                string columnType = ToTypeName(_mBaseType, _mRows, 1);
                for (int i = 0; i < _mColumns; i++)
                    str.AppendFormat("\t\tpublic {0} {1};\n", columnType, MatrixFields[i]);
            }
            else
            {
                for (int i = 0; i < _mRows; i++)
                {
                    if (_mColumns == 1 && _mBaseType == "bool")
                        str.Append("\t\t[MarshalAs(UnmanagedType.U1)]\n");
                    str.AppendFormat("\t\tpublic {0} {1};\n", _mBaseType, VectorFields[i]);
                }
            }

            str.Append("\n");
        }

        private void GenerateStaticFields(StringBuilder str)
        {
            if (_mBaseType == "int" || _mBaseType == "uint" || _mBaseType == "half" || _mBaseType == "float" ||
                _mBaseType == "double" || _mBaseType == "Fp")
            {
                string zeroStr = ToTypedLiteral(_mBaseType, 0);

                // identity
                if (_mRows == _mColumns)
                {
                    string oneStr = ToTypedLiteral(_mBaseType, 1);
                    str.AppendFormat("\t\t/// <summary>{0} identity transform.</summary>\n", _mTypeName);
                    str.AppendFormat("\t\tpublic static readonly {0} identity = new {0}(", _mTypeName);
                    for (int row = 0; row < _mRows; row++)
                    {
                        for (int column = 0; column < _mColumns; column++)
                        {
                            if (row != 0 || column != 0)
                                str.Append(", ");
                            if (row != 0 && column == 0)
                                str.Append("  ");
                            str.Append(row == column ? oneStr : zeroStr);
                        }
                    }

                    str.Append(");\n\n");
                }

                // zero
                str.AppendFormat("\t\t/// <summary>{0} zero value.</summary>\n", _mTypeName);
                str.AppendFormat("\t\tpublic static readonly {0} zero;\n", _mTypeName);
            }

            str.Append("\n");
        }

        private void GenerateDebuggerTypeProxy(StringBuilder str)
        {
            if (_mColumns > 1)
                return;

            str.Append("\t\tinternal sealed class DebuggerProxy\n");
            str.Append("\t\t{\n");
            for (int i = 0; i < _mRows; i++)
            {
                str.AppendFormat("\t\t\tpublic {0} {1};\n", _mBaseType, VectorFields[i]);
            }

            str.AppendFormat("\t\t\tpublic DebuggerProxy({0} v)\n", _mTypeName);
            str.Append("\t\t\t{\n");
            for (int i = 0; i < _mRows; i++)
            {
                str.AppendFormat("\t\t\t\t{0} = v.{0};\n", VectorFields[i]);
            }

            str.Append("\t\t\t}\n");
            str.Append("\t\t}\n\n");
        }

        private void GenerateConversion(StringBuilder str, StringBuilder opStr, StringBuilder mathStr,
            string sourceBaseType, bool isExplicit, bool isScalar)
        {
            string sourceType = isScalar ? sourceBaseType : ToTypeName(sourceBaseType, _mRows, _mColumns);

            int fieldCount = (_mColumns > 1) ? _mColumns : _mRows;
            string[] fields = (_mColumns > 1) ? MatrixFields : VectorFields;
            string dstFieldType = (_mColumns > 1) ? ToTypeName(_mBaseType, _mRows, 1) : _mBaseType;
            string dstTypeCategory = (_mColumns > 1) ? "matrix" : "vector";
            string explicitlyString = isExplicit ? "Explicitly" : "Implicitly";

            if (isScalar)
            {
                if (sourceBaseType != _mBaseType)
                {
                    str.AppendFormat(
                        "\t\t/// <summary>Constructs a {0} {1} from a single {2} value by converting it to {3} and assigning it to every component.</summary>\n",
                        _mTypeName, dstTypeCategory, sourceType, _mBaseType);
                    mathStr.AppendFormat(
                        "\t\t/// <summary>Returns a {0} {1} constructed from a single {2} value by converting it to {3} and assigning it to every component.</summary>\n",
                        _mTypeName, dstTypeCategory, sourceType, _mBaseType);
                    opStr.AppendFormat(
                        "\t\t/// <summary>{0} converts a single {1} value to a {2} {3} by converting it to {4} and assigning it to every component.</summary>\n",
                        explicitlyString, sourceType, _mTypeName, dstTypeCategory, _mBaseType);
                }
                else
                {
                    str.AppendFormat(
                        "\t\t/// <summary>Constructs a {0} {1} from a single {2} value by assigning it to every component.</summary>\n",
                        _mTypeName, dstTypeCategory, sourceType);
                    mathStr.AppendFormat(
                        "\t\t/// <summary>Returns a {0} {1} constructed from a single {2} value by assigning it to every component.</summary>\n",
                        _mTypeName, dstTypeCategory, sourceType);
                    opStr.AppendFormat(
                        "\t\t/// <summary>{0} converts a single {1} value to a {2} {3} by assigning it to every component.</summary>\n",
                        explicitlyString, sourceType, _mTypeName, dstTypeCategory);
                }
            }
            else
            {
                if (sourceBaseType != _mBaseType)
                {
                    str.AppendFormat(
                        "\t\t/// <summary>Constructs a {0} {1} from a {2} {1} by componentwise conversion.</summary>\n",
                        _mTypeName, dstTypeCategory, sourceType);
                    mathStr.AppendFormat(
                        "\t\t/// <summary>Return a {0} {1} constructed from a {2} {1} by componentwise conversion.</summary>\n",
                        _mTypeName, dstTypeCategory, sourceType);
                    opStr.AppendFormat(
                        "\t\t/// <summary>{0} converts a {1} {2} to a {3} {2} by componentwise conversion.</summary>\n",
                        explicitlyString, sourceType, dstTypeCategory, _mTypeName);
                }
            }

            str.Append("\t\t[MethodImpl(MethodImplOptions.AggressiveInlining)]\n");
            str.AppendFormat("\t\tpublic {0}({1} v)\n", _mTypeName, sourceType);
            str.Append("\t\t{\n");
            for (int i = 0; i < fieldCount; i++)
            {
                string rhs = "v";
                if (!isScalar)
                    rhs = rhs + "." + fields[i];
                if (isExplicit)
                {
                    if (sourceBaseType == "bool")
                    {
                        if (_mColumns > 1)
                            rhs = string.Format("select(new {0}({1}), new {0}({2}), {3})", dstFieldType,
                                ToTypedLiteral(_mBaseType, 0), ToTypedLiteral(_mBaseType, 1), rhs);
                        else
                            rhs = rhs + " ? " + ToTypedLiteral(_mBaseType, 1) + " : " + ToTypedLiteral(_mBaseType, 0);
                    }
                    else if (sourceBaseType == "ulong" && _mBaseType == "Fp")
                    {
                        // ** ulong 转 Fp 特殊处理
                        var typecasting = (_mColumns == 1 ? "(long)" : "");
                        rhs = $"new {dstFieldType}({typecasting}{rhs})";
                    }
                    else
                    {
                        rhs = "(" + dstFieldType + ")" + rhs;
                    }
                }


                str.AppendFormat("\t\t\tthis.{0} = {1};\n", fields[i], rhs);
            }

            str.Append("\t\t}\n\n");

            mathStr.Append("\t\t[MethodImpl(MethodImplOptions.AggressiveInlining)]\n");
            mathStr.AppendFormat("\t\tpublic static {0} {0}({1} v) {{ return new {0}(v); }}\n\n", _mTypeName,
                sourceType);

            opStr.Append("\t\t[MethodImpl(MethodImplOptions.AggressiveInlining)]\n");
            opStr.AppendFormat("\t\tpublic static {0} operator {1}({2} v) {{ return new {1}(v); }}\n\n",
                isExplicit ? "explicit" : "implicit", _mTypeName, sourceType);
        }

        private void GenerateConversionConstructorsAndOperators(StringBuilder str, StringBuilder mathStr)
        {
            StringBuilder opStr = new StringBuilder();

            GenerateConversion(str, opStr, mathStr, _mBaseType, false, true);

            if (_mBaseType == "int")
            {
                GenerateConversion(str, opStr, mathStr, "bool", true, true);
                GenerateConversion(str, opStr, mathStr, "bool", true, false);
                GenerateConversion(str, opStr, mathStr, "uint", true, true);
                GenerateConversion(str, opStr, mathStr, "uint", true, false);
                // GenerateConversion(str, opStr, mathStr, "float", true, true);
                // GenerateConversion(str, opStr, mathStr, "float", true, false);
                // GenerateConversion(str, opStr, mathStr, "double", true, true);
                // GenerateConversion(str, opStr, mathStr, "double", true, false);
            }
            else if (_mBaseType == "uint")
            {
                GenerateConversion(str, opStr, mathStr, "bool", true, true);
                GenerateConversion(str, opStr, mathStr, "bool", true, false);
                GenerateConversion(str, opStr, mathStr, "int", true, true);
                GenerateConversion(str, opStr, mathStr, "int", true, false);
                // GenerateConversion(str, opStr, mathStr, "float", true, true);
                // GenerateConversion(str, opStr, mathStr, "float", true, false);
                // GenerateConversion(str, opStr, mathStr, "double", true, true);
                // GenerateConversion(str, opStr, mathStr, "double", true, false);
            }
            else if (_mBaseType == "half")
            {
                GenerateConversion(str, opStr, mathStr, "float", true, true);
                GenerateConversion(str, opStr, mathStr, "float", true, false);
                GenerateConversion(str, opStr, mathStr, "double", true, true);
                GenerateConversion(str, opStr, mathStr, "double", true, false);
            }
            else if (_mBaseType == "Fp")
            {
                GenerateConversion(str, opStr, mathStr, "int", true, true);
                GenerateConversion(str, opStr, mathStr, "int", true, false);
                GenerateConversion(str, opStr, mathStr, "uint", true, true);
                GenerateConversion(str, opStr, mathStr, "uint", true, false);
                // GenerateConversion(str, opStr, mathStr, "ulong", true, true);
                // GenerateConversion(str, opStr, mathStr, "ulong", true, false);
            }
            else if (_mBaseType == "float")
            {
                GenerateConversion(str, opStr, mathStr, "bool", true, true);
                GenerateConversion(str, opStr, mathStr, "bool", true, false);
                GenerateConversion(str, opStr, mathStr, "int", false, true);
                GenerateConversion(str, opStr, mathStr, "int", false, false);
                GenerateConversion(str, opStr, mathStr, "uint", false, true);
                GenerateConversion(str, opStr, mathStr, "uint", false, false);
                if (_mColumns == 1)
                {
                    GenerateConversion(str, opStr, mathStr, "half", false, true);
                    GenerateConversion(str, opStr, mathStr, "half", false, false);
                }

                GenerateConversion(str, opStr, mathStr, "double", true, true);
                GenerateConversion(str, opStr, mathStr, "double", true, false);
            }
            else if (_mBaseType == "double")
            {
                GenerateConversion(str, opStr, mathStr, "bool", true, true);
                GenerateConversion(str, opStr, mathStr, "bool", true, false);
                GenerateConversion(str, opStr, mathStr, "int", false, true);
                GenerateConversion(str, opStr, mathStr, "int", false, false);
                GenerateConversion(str, opStr, mathStr, "uint", false, true);
                GenerateConversion(str, opStr, mathStr, "uint", false, false);
                if (_mColumns == 1)
                {
                    GenerateConversion(str, opStr, mathStr, "half", false, true);
                    GenerateConversion(str, opStr, mathStr, "half", false, false);
                }

                GenerateConversion(str, opStr, mathStr, "float", false, true);
                GenerateConversion(str, opStr, mathStr, "float", false, false);
            }


            str.Append("\n");
            str.Append(opStr);
            str.Append("\n");
        }

        private void GenerateTypeImplementation(StringBuilder str)
        {
            StringBuilder mathStr = new StringBuilder();

            str.Append("using System;\n");
            str.Append("using System.Runtime.CompilerServices;\n");
            if (_mColumns == 1 && _mBaseType == "bool")
                str.Append("using System.Runtime.InteropServices;\n"); // for MarshalAs
            if (_mColumns == 1)
                str.Append("using System.Diagnostics;\n"); // for DebuggerTypeProxy
            str.Append("using static Unity.Mathematics.FixedPoint.MathFp;\n");
            str.Append("using Unity.IL2CPP.CompilerServices;\n");
            str.Append("\n");
            str.Append("#pragma warning disable 0660, 0661\n\n");
            str.Append("namespace Unity.Mathematics.FixedPoint\n");
            str.Append("{\n");

            if (_mColumns == 1)
                str.AppendFormat("\t[DebuggerTypeProxy(typeof({0}.DebuggerProxy))]\n", _mTypeName);
            if (_mBaseType != "half")
                str.Append("\t[System.Serializable]\n");
            str.Append("\t[Il2CppEagerStaticClassConstruction]\n");
            str.AppendFormat("\tpublic partial struct {0} : System.IEquatable<{0}>", _mTypeName);
            if (_mBaseType != "bool")
                str.Append(", IFormattable");
            str.Append("\n\t{\n");

            GenerateMemberVariables(str);

            GenerateStaticFields(str);

            GenerateConstructors(str, mathStr);

            GenerateConversionConstructorsAndOperators(str, mathStr);

            GenerateOperators(str);

            var isMatrix = _mRows > 1 && _mColumns > 1;

            if (_mRows == 4 && _mColumns == 4 &&
                (_mBaseType == "float" || _mBaseType == "double" || _mBaseType == "Fp"))
            {
                GenerateMulImplementation("rotate", _mBaseType, mathStr, 4, 4, 3, 1, false,
                    string.Format("Return the result of rotating a {0} vector by a {1} matrix",
                        ToTypeName(_mBaseType, 3, 1), ToTypeName(_mBaseType, 4, 4)));
                GenerateMulImplementation("transform", _mBaseType, mathStr, 4, 4, 3, 1, true,
                    string.Format("Return the result of transforming a {0} point by a {1} matrix",
                        ToTypeName(_mBaseType, 3, 1), ToTypeName(_mBaseType, 4, 4)));
            }

            GenerateTransposeFunction(mathStr);
            GenerateInverseFunction(mathStr);
            GenerateFastInverseFunction(mathStr);
            GenerateDeterminantFunction(mathStr);
            GenerateHashFunction(mathStr, false);
            GenerateHashFunction(mathStr, true);

            if (_mColumns == 1)
            {
                str.Append("\n\n");
                GenerateSwizzles(str);
                GenerateShuffleImplementation(mathStr);
            }


            str.Append("\n");
            GenerateIndexOperator(str, isMatrix ? IndexerMode.ByRef : IndexerMode.ByValue);

            str.Append("\n");
            GenerateEquals(str);

            str.Append("\n");
            GenerateGetHashCode(str);

            str.Append("\n");
            GenerateToStringFunction(str, false);
            if (_mBaseType != "bool")
                GenerateToStringFunction(str, true);

            GenerateDebuggerTypeProxy(str);


            // Internal members last
            if (_mColumns == 1 && _mBaseType != "half")
            {
                GenerateSelectShuffleComponentImplementation(mathStr);
            }

            str.Append("\t}\n\n");

            str.Append("\tpublic static partial class MathFp\n");
            str.Append("\t{\n");
            str.Append(mathStr);
            str.Append("\t}\n}\n");
        }

        private void GenerateSelectShuffleComponentImplementation(StringBuilder str)
        {
            if (_mColumns > 1)
                return;

            str.Append("\t\t[MethodImpl(MethodImplOptions.AggressiveInlining)]\n");
            str.AppendFormat(
                "\t\tinternal static {0} select_shuffle_component({1} a, {1} b, ShuffleComponent component)\n",
                _mBaseType, _mTypeName);
            str.Append("\t\t{\n");
            str.Append("\t\t\tswitch(component)\n");
            str.Append("\t\t\t{\n");
            for (int i = 0; i < _mRows; i++)
            {
                str.AppendFormat("\t\t\t\tcase ShuffleComponent.{0}:\n", ShuffleComponents[i]);
                str.AppendFormat("\t\t\t\t\treturn a{0};\n", _mRows > 1 ? "." + VectorFields[i] : "");
            }

            for (int i = 0; i < _mRows; i++)
            {
                str.AppendFormat("\t\t\t\tcase ShuffleComponent.{0}:\n", ShuffleComponents[i + 4]);
                str.AppendFormat("\t\t\t\t\treturn b{0};\n", _mRows > 1 ? "." + VectorFields[i] : "");
            }

            str.Append("\t\t\t\tdefault:\n");
            str.Append(
                "\t\t\t\t\tthrow new System.ArgumentException(\"Invalid shuffle component: \" + (int)component);\n");
            str.Append("\t\t\t}\n");
            str.Append("\t\t}\n\n");
        }

        private void GenerateShuffleImplementation(StringBuilder str)
        {
            if (_mColumns > 1 || _mBaseType == "half")
                return;

            for (int resultComponents = 1; resultComponents <= 4; resultComponents++)
            {
                string resultType = ToTypeName(_mBaseType, resultComponents, 1);

                str.AppendFormat(
                    "\t\t/// <summary>Returns the result of specified shuffling of the components from {0} into {1}.</summary>\n",
                    ToValueDescription(_mBaseType, _mRows, _mColumns, 2),
                    ToValueDescription(_mBaseType, resultComponents, _mColumns, 1));
                str.Append("\t\t[MethodImpl(MethodImplOptions.AggressiveInlining)]\n");
                str.AppendFormat("\t\tpublic static {0} shuffle({1} a, {2} b", resultType, _mTypeName, _mTypeName);
                for (int i = 0; i < resultComponents; i++)
                {
                    str.AppendFormat(", ShuffleComponent {0}", VectorFields[i]);
                }

                str.Append(")\n");
                str.Append("\t\t{\n");

                if (resultComponents != 1)
                    str.AppendFormat("\t\t\treturn {0}(\n", resultType);
                else
                    str.AppendFormat("\t\t\treturn ");

                for (int i = 0; i < resultComponents; i++)
                {
                    if (resultComponents != 1)
                        str.Append("\t\t\t\t");

                    str.AppendFormat("select_shuffle_component(a, b, {0})", VectorFields[i]);
                    if (i != resultComponents - 1)
                        str.Append(",\n");
                }

                if (resultComponents != 1)
                    str.Append(");\n");
                else
                    str.Append(";\n");

                str.Append("\t\t}\n\n");
            }
        }

        private void GenerateMulImplementation(string name, string baseType, StringBuilder str, int lhsRows,
            int lhsColumns, int rhsRows, int rhsColumns, bool doTranslation, string desc)
        {
            // mul(a,b): if a is vector it is treated as a row vector. if b is a vector it is treated as a column vector.
            // bool isResultRowVector = (lhsRows == 1 && lhsColumns > 1);
            int resultRows = (lhsColumns != rhsRows) ? rhsRows : lhsRows;
            string resultType = ToTypeName(baseType, resultRows, rhsColumns);
            string lhsType = ToTypeName(baseType, lhsRows, lhsColumns);
            string rhsType = ToTypeName(baseType, rhsRows, rhsColumns);

            // bool isScalarResult = (resultRows == 1 && rhsColumns == 1);
            bool needsSwizzle = (resultRows != lhsRows);
            if (desc == "")
            {
                str.AppendFormat(
                    "\t\t/// <summary>Returns the {0} result of a matrix multiplication between {1} and {2}.</summary>\n",
                    ToValueDescription(baseType, resultRows, rhsColumns, 0, true),
                    ToValueDescription(baseType, lhsRows, lhsColumns, 1, true),
                    ToValueDescription(baseType, rhsRows, rhsColumns, 1, true));
            }
            else
            {
                str.AppendFormat("\t\t/// <summary>{0}</summary>\n", desc);
            }

            str.Append("\t\t[MethodImpl(MethodImplOptions.AggressiveInlining)]\n");
            str.AppendFormat("\t\tpublic static {1} {0}({2} a, {3} b)\n", name, resultType, lhsType, rhsType);
            str.Append("\t\t{\n");

            str.Append("\t\t\treturn ");

            bool needsParen = rhsColumns > 1 || needsSwizzle;

            if (rhsColumns > 1)
                str.Append(resultType);
            if (needsParen)
                str.Append("(");

            for (int i = 0; i < rhsColumns; i++)
            {
                if (i > 0 || rhsColumns > 1)
                    str.Append("\n\t\t\t\t");
                for (int j = 0; j < lhsColumns; j++)
                {
                    if (j < rhsRows || doTranslation)
                    {
                        if (j != 0)
                            str.Append(" + ");

                        // ** NOTE: mul 需要在计算过程中忽略计算产生的极小值
                        str.Append("IgnoreTooSmallNumber(");
                        if (lhsRows == 1 && lhsColumns == 1)
                            str.Append("a");
                        else if (lhsRows == 1 || lhsColumns == 1)
                            str.Append("a." + VectorFields[j]);
                        else
                            str.Append("a." + MatrixFields[j]);

                        if (j < rhsRows)
                        {
                            str.Append(" * ");

                            if (rhsRows == 1 && rhsColumns == 1)
                                str.Append("b");
                            else if (rhsRows == 1 || rhsColumns == 1)
                                str.Append("b." + VectorFields[j]);
                            else
                                str.Append("b." + MatrixFields[i] + "." + VectorFields[j]);
                        }
                        str.Append(")");
                    }
                }

                if (i != rhsColumns - 1)
                    str.Append(",");
            }

            if (needsParen)
                str.Append(")");
            if (needsSwizzle)
                str.Append("." + GenerateComponentRangeString(0, resultRows));

            str.Append(";\n");
            str.Append("\t\t}\n\n");
        }

        private void GenerateMulImplementations(string baseType, StringBuilder str)
        {
            // type n x k = mul(type n x m, type m x k)
            for (int n = 1; n <= 4; n++)
            {
                for (int m = 1; m <= 4; m++)
                {
                    for (int k = 1; k <= 4; k++)
                    {
                        // mul(a,b): if a is vector it is treated as a row vector. if b is a vector it is treated as a column vector.

                        if (n > 1 && m == 1)
                            continue; // lhs cannot be column vector
                        if (m == 1 && k > 1)
                            continue; // rhs cannot be row vector

                        GenerateMulImplementation("mul", baseType, str, n, m, m, k, false, "");
                    }
                }
            }
        }

        private void GenerateMatrixImplementation(StringBuilder str)
        {
            str.Append("using System;\n");
            str.Append("using System.Runtime.CompilerServices;\n");
            str.Append("\n");
            str.Append("namespace Unity.Mathematics.FixedPoint\n");
            str.Append("{\n");
            str.Append("\tpartial class MathFp\n");
            str.Append("\t{\n");

            GenerateMulImplementations("Fp", str);

            str.Append("\t}\n");
            str.Append("}\n");
        }

        private string GenerateComponentRangeString(int componentIndex, int numComponents)
        {
            string result = "";
            for (int i = 0; i < numComponents; i++)
            {
                result += Components[componentIndex + i];
            }

            return result;
        }

        // Generate constructor and static constructor with a given component partitioning of input parameters
        private void GenerateVectorConstructor(int numParameters, int[] parameterComponents,
            StringBuilder constructorStr, StringBuilder mathStr)
        {
            // Generate signatures
            string dstTypeCategory = (_mColumns > 1) ? "matrix" : "vector";

            StringBuilder descriptionStr = new StringBuilder();
            {
                int idx = 0;
                bool first = true;
                while (true)
                {
                    int paramComponents = parameterComponents[idx];
                    int n = 1;
                    while (idx + 1 < numParameters && parameterComponents[idx + 1] == paramComponents)
                    {
                        n++;
                        idx++;
                    }

                    idx++;

                    bool last = (idx == numParameters);
                    if (!first)
                        descriptionStr.Append(last ? " and " : ", ");

                    descriptionStr.Append(ToValueDescription(_mBaseType, paramComponents, 1, n));
                    if (last)
                        break;
                    first = false;
                }
            }


            constructorStr.AppendFormat("\t\t/// <summary>Constructs a {0} {1} from {2}.</summary>\n", _mTypeName,
                dstTypeCategory, descriptionStr);
            constructorStr.Append("\t\t[MethodImpl(MethodImplOptions.AggressiveInlining)]\n");
            constructorStr.Append("\t\tpublic ");
            constructorStr.Append(_mTypeName);
            constructorStr.Append("(");

            mathStr.AppendFormat("\t\t/// <summary>Returns a {0} {1} constructed from {2}.</summary>\n", _mTypeName,
                dstTypeCategory, descriptionStr);
            mathStr.Append("\t\t[MethodImpl(MethodImplOptions.AggressiveInlining)]\n");
            mathStr.Append("\t\tpublic static ");
            mathStr.Append(_mTypeName);
            mathStr.Append(" ");
            mathStr.Append(_mTypeName);
            mathStr.Append("(");

            int componentIndex = 0;
            for (int i = 0; i < numParameters; i++)
            {
                if (i != 0)
                {
                    constructorStr.Append(", ");
                    mathStr.Append(", ");
                }

                int paramComponents = parameterComponents[i];
                string paramType = ToTypeName(_mBaseType, paramComponents, 1);
                string componentString = GenerateComponentRangeString(componentIndex, paramComponents);

                constructorStr.Append(paramType);
                constructorStr.Append(" ");
                constructorStr.Append(componentString);
                mathStr.Append(paramType);
                mathStr.Append(" ");
                mathStr.Append(componentString);
                componentIndex += paramComponents;
            }

            constructorStr.Append(")\n");
            mathStr.Append(")");

            // Generate function bodies
            constructorStr.Append("\t\t{ ");
            mathStr.Append(" { return new ");
            mathStr.Append(_mTypeName);
            mathStr.Append("(");

            componentIndex = 0;
            for (int i = 0; i < numParameters; i++)
            {
                int paramComponents = parameterComponents[i];
                string componentString = GenerateComponentRangeString(componentIndex, paramComponents);
                for (int j = 0; j < paramComponents; j++)
                {
                    constructorStr.Append("\n\t\t\tthis.");
                    constructorStr.Append(Components[componentIndex]);
                    constructorStr.Append(" = ");
                    constructorStr.Append(componentString);
                    if (paramComponents > 1)
                    {
                        constructorStr.Append(".");
                        constructorStr.Append(Components[j]);
                    }

                    constructorStr.Append(";");
                    componentIndex++;
                }

                if (i != 0)
                {
                    mathStr.Append(", ");
                }

                mathStr.Append(componentString);
            }

            constructorStr.Append("\n\t\t}\n\n");
            mathStr.Append("); }\n\n");
        }

        // Recursively generate all constructor variants with every possibly partition or the components
        private void GenerateVectorConstructors(int numRemainingComponents, int numParameters,
            int[] parameterComponents, StringBuilder constructorStr, StringBuilder mathStr)
        {
            if (numRemainingComponents == 0)
            {
                GenerateVectorConstructor(numParameters, parameterComponents, constructorStr, mathStr);
            }

            for (int i = 1; i <= numRemainingComponents; i++)
            {
                parameterComponents[numParameters] = i;
                GenerateVectorConstructors(numRemainingComponents - i, numParameters + 1,
                    parameterComponents, constructorStr, mathStr);
            }
        }

        public void GenerateMatrixColumnConstructor(StringBuilder constructorStr, StringBuilder mathStr)
        {
            // Generate signatures
            string columnType = ToTypeName(_mBaseType, _mRows, 1);
            string columnDescription = ToValueDescription(_mBaseType, _mRows, 1, _mColumns);
            constructorStr.AppendFormat("\t\t/// <summary>Constructs a {0} matrix from {1}.</summary>\n", _mTypeName,
                columnDescription);
            constructorStr.Append("\t\t[MethodImpl(MethodImplOptions.AggressiveInlining)]\n");
            constructorStr.Append("\t\tpublic ");
            constructorStr.Append(_mTypeName);
            constructorStr.Append("(");

            mathStr.AppendFormat("\t\t/// <summary>Returns a {0} matrix constructed from {1}.</summary>\n", _mTypeName,
                columnDescription);
            mathStr.Append("\t\t[MethodImpl(MethodImplOptions.AggressiveInlining)]\n");
            mathStr.Append("\t\tpublic static ");
            mathStr.Append(_mTypeName);
            mathStr.Append(" ");
            mathStr.Append(_mTypeName);
            mathStr.Append("(");

            for (int column = 0; column < _mColumns; column++)
            {
                if (column != 0)
                {
                    constructorStr.Append(", ");
                    mathStr.Append(", ");
                }

                constructorStr.Append(columnType);
                constructorStr.Append(" ");
                constructorStr.Append(MatrixFields[column]);
                mathStr.Append(columnType);
                mathStr.Append(" ");
                mathStr.Append(MatrixFields[column]);
            }

            constructorStr.Append(")\n");
            mathStr.Append(")");

            // Generate function bodies
            constructorStr.Append("\t\t{");
            mathStr.Append(" { return new ");
            mathStr.Append(_mTypeName);
            mathStr.Append("(");

            for (int column = 0; column < _mColumns; column++)
            {
                constructorStr.Append("\n\t\t\tthis.");
                constructorStr.Append(MatrixFields[column]);
                constructorStr.Append(" = ");
                constructorStr.Append(MatrixFields[column]);
                constructorStr.Append(";");

                if (column != 0)
                {
                    mathStr.Append(", ");
                }

                mathStr.Append(MatrixFields[column]);
            }

            constructorStr.Append("\n\t\t}\n\n");
            mathStr.Append("); }\n\n");
        }

        public void GenerateMatrixRowConstructor(StringBuilder constructorStr, StringBuilder mathStr)
        {
            // Generate signatures
            constructorStr.AppendFormat(
                "\t\t/// <summary>Constructs a {0} matrix from {1} {2} values given in row-major order.</summary>\n",
                _mTypeName, _mRows * _mColumns, _mBaseType);
            constructorStr.Append("\t\t[MethodImpl(MethodImplOptions.AggressiveInlining)]\n");
            constructorStr.Append("\t\tpublic ");
            constructorStr.Append(_mTypeName);
            constructorStr.Append("(");
            mathStr.AppendFormat(
                "\t\t/// <summary>Returns a {0} matrix constructed from from {1} {2} values given in row-major order.</summary>\n",
                _mTypeName, _mRows * _mColumns, _mBaseType);
            mathStr.Append("\t\t[MethodImpl(MethodImplOptions.AggressiveInlining)]\n");
            mathStr.Append("\t\tpublic static ");
            mathStr.Append(_mTypeName);
            mathStr.Append(" ");
            mathStr.Append(_mTypeName);
            mathStr.Append("(");
            string indent0 = new string(' ', _mTypeName.Length + 16);
            string indent1 = new string(' ', _mTypeName.Length * 2 + 24);
            string indent2 = new string(' ', _mTypeName.Length + 24);

            for (int row = 0; row < _mRows; row++)
            {
                if (row != 0)
                {
                    constructorStr.Append(indent0);
                    mathStr.Append(indent1);
                }

                for (int column = 0; column < _mColumns; column++)
                {
                    string paramName = "m" + row + column;
                    constructorStr.Append(_mBaseType);
                    constructorStr.Append(" ");
                    constructorStr.Append(paramName);
                    mathStr.Append(_mBaseType);
                    mathStr.Append(" ");
                    mathStr.Append(paramName);

                    //string separator = (row == m_Rows - 1) ? (column == m_Columns - 1) ? "," : ", " : ", ";
                    string separator = (column == _mColumns - 1) ? (row == _mRows - 1) ? ")\n" : ",\n" : ", ";
                    constructorStr.Append(separator);
                    mathStr.Append(separator);
                }
            }

            string columnType = ToTypeName(_mBaseType, _mRows, 1);

            // constructor body
            constructorStr.Append("\t\t{");
            for (int column = 0; column < _mColumns; column++)
            {
                constructorStr.AppendFormat("\n\t\t\tthis.{0} = new {1}(", MatrixFields[column], columnType);

                for (int row = 0; row < _mRows; row++)
                {
                    constructorStr.Append("m" + row + column);

                    if (row != _mRows - 1)
                    {
                        constructorStr.Append(", ");
                    }
                }

                constructorStr.Append(");");
            }

            constructorStr.Append("\n\t\t}\n\n");

            // static constructor body
            mathStr.AppendFormat("\t\t{{\n\t\t\treturn new {0}(", _mTypeName);
            for (int row = 0; row < _mRows; row++)
            {
                if (row != 0)
                {
                    mathStr.Append(indent2);
                }

                for (int column = 0; column < _mColumns; column++)
                {
                    string paramName = "m" + row + column;
                    mathStr.Append(paramName);

                    //string separator = (row == m_Rows - 1) ? (column == m_Columns - 1) ? "," : ", " : ", ";
                    string separator = (column == _mColumns - 1) ? (row == _mRows - 1) ? ");" : ",\n" : ", ";
                    mathStr.Append(separator);
                }
            }

            mathStr.Append("\n\t\t}\n\n");
        }

        public void GenerateMatrixConstructors(StringBuilder constructorStr, StringBuilder mathStr)
        {
            GenerateMatrixColumnConstructor(constructorStr, mathStr);
            GenerateMatrixRowConstructor(constructorStr, mathStr);
        }

        public void GenerateConstructors(StringBuilder constructorStr, StringBuilder mathStr)
        {
            if (_mColumns == 1)
            {
                int[] parameterComponents = new int[4];
                GenerateVectorConstructors(_mRows, 0, parameterComponents, constructorStr, mathStr);
            }
            else
            {
                GenerateMatrixConstructors(constructorStr, mathStr);
            }
        }

        public void GenerateOperators(StringBuilder str)
        {
            string resultType = _mBaseType;
            string resultBoolType = "bool";

            if (0 != (_mFeatures & Features.Arithmetic))
            {
                GenerateBinaryOperator(_mRows, _mColumns, "*", resultType, str, "multiplication");

                GenerateBinaryOperator(_mRows, _mColumns, "+", resultType, str, "addition");

                GenerateBinaryOperator(_mRows, _mColumns, "-", resultType, str, "subtraction");

                GenerateBinaryOperator(_mRows, _mColumns, "/", resultType, str, "division");

                GenerateBinaryOperator(_mRows, _mColumns, "%", resultType, str, "modulus");

                GenerateUnaryOperator("++", str, "increment");

                GenerateUnaryOperator("--", str, "decrement");

                GenerateBinaryOperator(_mRows, _mColumns, "<", resultBoolType, str, "less than");
                GenerateBinaryOperator(_mRows, _mColumns, "<=", resultBoolType, str, "less or equal");

                GenerateBinaryOperator(_mRows, _mColumns, ">", resultBoolType, str, "greater than");
                GenerateBinaryOperator(_mRows, _mColumns, ">=", resultBoolType, str, "greater or equal");
            }

            if (0 != (_mFeatures & Features.UnaryNegation))
            {
                GenerateUnaryOperator("-", str, "unary minus");
                GenerateUnaryOperator("+", str, "unary plus");
            }

            if (0 != (_mFeatures & Features.Shifts))
            {
                GenerateShiftOperator(_mRows, "<<", str, "left shift");
                GenerateShiftOperator(_mRows, ">>", str, "right shift");
            }

            GenerateBinaryOperator(_mRows, _mColumns, "==", resultBoolType, str, "equality");
            GenerateBinaryOperator(_mRows, _mColumns, "!=", resultBoolType, str, "not equal");

            if (0 != (_mFeatures & Features.BitwiseComplement))
            {
                GenerateUnaryOperator("~", str, "bitwise not");
            }

            if (_mBaseType == "bool")
            {
                GenerateUnaryOperator("!", str, "not");
            }

            if (0 != (_mFeatures & Features.BitwiseLogic))
            {
                GenerateBinaryOperator(_mRows, _mColumns, "&", resultType, str, "bitwise and");
                GenerateBinaryOperator(_mRows, _mColumns, "|", resultType, str, "bitwise or");
                GenerateBinaryOperator(_mRows, _mColumns, "^", resultType, str, "bitwise exclusive or");
            }
        }

        private void GeneratePrimeUIntVector(StringBuilder str, int n)
        {
            str.AppendFormat("uint{0}(", n);
            for (int row = 0; row < n; row++)
            {
                if (row != 0)
                    str.Append(", ");
                str.AppendFormat("0x{0:X}u", NextPrime());
            }

            str.Append(")");
        }

        public void GenerateTransposeFunction(StringBuilder str)
        {
            if (_mColumns == 1 || _mRows == 1)
                return;

            string resultType = ToTypeName(_mBaseType, _mColumns, _mRows);
            str.AppendFormat("\t\t/// <summary>Return the {0} transpose of a {1} matrix.</summary>\n", resultType,
                _mTypeName);
            str.Append("\t\t[MethodImpl(MethodImplOptions.AggressiveInlining)]\n");
            str.AppendFormat("\t\tpublic static {0} transpose({1} v)\n", resultType, _mTypeName);
            str.Append("\t\t{\n");

            str.AppendFormat("\t\t\treturn {0}(\n", resultType);
            for (int i = 0; i < _mColumns; i++)
            {
                str.Append("\t\t\t\t");
                for (int j = 0; j < _mRows; j++)
                {
                    if (j != 0)
                        str.Append(", ");
                    str.AppendFormat("v.c{0}.{1}", i, VectorFields[j]);
                }

                if (i != _mColumns - 1)
                    str.Append(",\n");
            }

            str.Append(");\n");
            str.Append("\t\t}\n\n");
        }

        public void GenerateInverseFunction(StringBuilder str)
        {
            if (_mRows != _mColumns || _mRows == 1)
                return;

            if (_mBaseType != "float" && _mBaseType != "double" && _mBaseType != "Fp")
                return;

            string oneStr = ToTypedLiteral(_mBaseType, 1);

            if (_mRows == 2)
            {
                str.AppendFormat(
                    @"        /// <summary>Returns the {0}2x2 full inverse of a {0}2x2 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static {0}2x2 inverse({0}2x2 m)
        {{
            {0} a = m.c0.x;
            {0} b = m.c1.x;
            {0} c = m.c0.y;
            {0} d = m.c1.y;

            // ** NOTE:忽略计算过程中产生的极小值
            {0} det = IgnoreTooSmallNumber(a * d) - IgnoreTooSmallNumber(b * c);

            return IgnoreTooSmallNumber({0}2x2(d, -b, -c, a) * ({1} / det));
        }}

",
                    _mBaseType, oneStr);
            }
            else if (_mRows == 3)
            {
                str.AppendFormat(
                    @"        /// <summary>Returns the {0}3x3 full inverse of a {0}3x3 matrix.</summary>
        public static {0}3x3 inverse({0}3x3 m)
        {{
            {0}3 c0 = m.c0;
            {0}3 c1 = m.c1;
            {0}3 c2 = m.c2;

            {0}3 t0 = {0}3(c1.x, c2.x, c0.x);
            {0}3 t1 = {0}3(c1.y, c2.y, c0.y);
            {0}3 t2 = {0}3(c1.z, c2.z, c0.z);

            // ** NOTE:忽略计算过程中产生的极小值
            {0}3 m0 = IgnoreTooSmallNumber(t1 * t2.yzx) - IgnoreTooSmallNumber(t1.yzx * t2);
            {0}3 m1 = t0.yzx * t2 - t0 * t2.yzx;
            {0}3 m2 = t0 * t1.yzx - t0.yzx * t1;

            {0} rcpDet = {1} / csum(IgnoreTooSmallNumber(t0.zxy * m0));
            return IgnoreTooSmallNumber({0}3x3(m0, m1, m2) * rcpDet);
        }}

",
                    _mBaseType, oneStr);
            }
            else if (_mRows == 4)
            {
                str.AppendFormat(
                    @"        /// <summary>Returns the {0}4x4 full inverse of a {0}4x4 matrix.</summary>
        public static {0}4x4 inverse({0}4x4 m)
        {{
            {0}4 c0 = m.c0;
            {0}4 c1 = m.c1;
            {0}4 c2 = m.c2;
            {0}4 c3 = m.c3;

            {0}4 r0y_r1y_r0x_r1x = movelh(c1, c0);
            {0}4 r0z_r1z_r0w_r1w = movelh(c2, c3);
            {0}4 r2y_r3y_r2x_r3x = movehl(c0, c1);
            {0}4 r2z_r3z_r2w_r3w = movehl(c3, c2);

            {0}4 r1y_r2y_r1x_r2x = shuffle(c1, c0, ShuffleComponent.LeftY, ShuffleComponent.LeftZ, ShuffleComponent.RightY, ShuffleComponent.RightZ);
            {0}4 r1z_r2z_r1w_r2w = shuffle(c2, c3, ShuffleComponent.LeftY, ShuffleComponent.LeftZ, ShuffleComponent.RightY, ShuffleComponent.RightZ);
            {0}4 r3y_r0y_r3x_r0x = shuffle(c1, c0, ShuffleComponent.LeftW, ShuffleComponent.LeftX, ShuffleComponent.RightW, ShuffleComponent.RightX);
            {0}4 r3z_r0z_r3w_r0w = shuffle(c2, c3, ShuffleComponent.LeftW, ShuffleComponent.LeftX, ShuffleComponent.RightW, ShuffleComponent.RightX);

            {0}4 r0_wzyx = shuffle(r0z_r1z_r0w_r1w, r0y_r1y_r0x_r1x, ShuffleComponent.LeftZ, ShuffleComponent.LeftX, ShuffleComponent.RightX, ShuffleComponent.RightZ);
            {0}4 r1_wzyx = shuffle(r0z_r1z_r0w_r1w, r0y_r1y_r0x_r1x, ShuffleComponent.LeftW, ShuffleComponent.LeftY, ShuffleComponent.RightY, ShuffleComponent.RightW);
            {0}4 r2_wzyx = shuffle(r2z_r3z_r2w_r3w, r2y_r3y_r2x_r3x, ShuffleComponent.LeftZ, ShuffleComponent.LeftX, ShuffleComponent.RightX, ShuffleComponent.RightZ);
            {0}4 r3_wzyx = shuffle(r2z_r3z_r2w_r3w, r2y_r3y_r2x_r3x, ShuffleComponent.LeftW, ShuffleComponent.LeftY, ShuffleComponent.RightY, ShuffleComponent.RightW);
            {0}4 r0_xyzw = shuffle(r0y_r1y_r0x_r1x, r0z_r1z_r0w_r1w, ShuffleComponent.LeftZ, ShuffleComponent.LeftX, ShuffleComponent.RightX, ShuffleComponent.RightZ);

            // ** NOTE:忽略计算过程中产生的极小值
            // Calculate remaining inner term pairs. inner terms have zw=-xy, so we only have to calculate xy and can pack two pairs per vector.
            {0}4 inner12_23 = IgnoreTooSmallNumber(r1y_r2y_r1x_r2x * r2z_r3z_r2w_r3w) - IgnoreTooSmallNumber(r1z_r2z_r1w_r2w * r2y_r3y_r2x_r3x);
            {0}4 inner02_13 = IgnoreTooSmallNumber(r0y_r1y_r0x_r1x * r2z_r3z_r2w_r3w) - IgnoreTooSmallNumber(r0z_r1z_r0w_r1w * r2y_r3y_r2x_r3x);
            {0}4 inner30_01 = IgnoreTooSmallNumber(r3z_r0z_r3w_r0w * r0y_r1y_r0x_r1x) - IgnoreTooSmallNumber(r3y_r0y_r3x_r0x * r0z_r1z_r0w_r1w);

            // Expand inner terms back to 4 components. zw signs still need to be flipped
            {0}4 inner12 = shuffle(inner12_23, inner12_23, ShuffleComponent.LeftX, ShuffleComponent.LeftZ, ShuffleComponent.RightZ, ShuffleComponent.RightX);
            {0}4 inner23 = shuffle(inner12_23, inner12_23, ShuffleComponent.LeftY, ShuffleComponent.LeftW, ShuffleComponent.RightW, ShuffleComponent.RightY);

            {0}4 inner02 = shuffle(inner02_13, inner02_13, ShuffleComponent.LeftX, ShuffleComponent.LeftZ, ShuffleComponent.RightZ, ShuffleComponent.RightX);
            {0}4 inner13 = shuffle(inner02_13, inner02_13, ShuffleComponent.LeftY, ShuffleComponent.LeftW, ShuffleComponent.RightW, ShuffleComponent.RightY);

            // Calculate minors
            {0}4 minors0 = IgnoreTooSmallNumber(r3_wzyx * inner12) - IgnoreTooSmallNumber(r2_wzyx * inner13) + IgnoreTooSmallNumber(r1_wzyx * inner23);

            {0}4 denominator = IgnoreTooSmallNumber(r0_xyzw * minors0);

            // Horizontal sum of denominator. Free sign flip of z and w compensates for missing flip in inner terms.
            denominator = denominator + shuffle(denominator, denominator, ShuffleComponent.LeftY, ShuffleComponent.LeftX, ShuffleComponent.RightW, ShuffleComponent.RightZ);   // x+y		x+y			z+w			z+w
            denominator = denominator - shuffle(denominator, denominator, ShuffleComponent.LeftZ, ShuffleComponent.LeftZ, ShuffleComponent.RightX, ShuffleComponent.RightX);   // x+y-z-w  x+y-z-w		z+w-x-y		z+w-x-y

            {0}4 rcp_denominator_ppnn = {0}4({1}) / denominator;
            {0}4x4 res;
            res.c0 = IgnoreTooSmallNumber(minors0 * rcp_denominator_ppnn);

            {0}4 inner30 = shuffle(inner30_01, inner30_01, ShuffleComponent.LeftX, ShuffleComponent.LeftZ, ShuffleComponent.RightZ, ShuffleComponent.RightX);
            {0}4 inner01 = shuffle(inner30_01, inner30_01, ShuffleComponent.LeftY, ShuffleComponent.LeftW, ShuffleComponent.RightW, ShuffleComponent.RightY);

            {0}4 minors1 = r2_wzyx * inner30 - r0_wzyx * inner23 - r3_wzyx * inner02;
            res.c1 = IgnoreTooSmallNumber(minors1 * rcp_denominator_ppnn);

            {0}4 minors2 = r0_wzyx * inner13 - r1_wzyx * inner30 - r3_wzyx * inner01;
            res.c2 = IgnoreTooSmallNumber(minors2 * rcp_denominator_ppnn);

            {0}4 minors3 = r1_wzyx * inner02 - r0_wzyx * inner12 + r2_wzyx * inner01;
            res.c3 = IgnoreTooSmallNumber(minors3 * rcp_denominator_ppnn);
            return res;
        }}

",
                    _mBaseType, oneStr);
            }
        }

        public void GenerateFastInverseFunction(StringBuilder str)
        {
            if (_mBaseType != "float" && _mBaseType != "double" && _mBaseType != "Fp")
                return;

            if (_mColumns == 4 && _mRows == 3)
            {
                str.AppendFormat(
                    @"        // Fast matrix inverse for rigid transforms (Orthonormal basis and translation)
        public static {0}3x4 fastinverse({0}3x4 m)
        {{
            {0}3 c0 = m.c0;
            {0}3 c1 = m.c1;
            {0}3 c2 = m.c2;
            {0}3 pos = m.c3;

            {0}3 r0 = {0}3(c0.x, c1.x, c2.x);
            {0}3 r1 = {0}3(c0.y, c1.y, c2.y);
            {0}3 r2 = {0}3(c0.z, c1.z, c2.z);

            pos = -(r0 * pos.x + r1 * pos.y + r2 * pos.z);

            return {0}3x4(r0, r1, r2, pos);
        }}

",
                    _mBaseType);
            }
            else if (_mColumns == 4 && _mRows == 4)
            {
                str.AppendFormat(
                    @"        // Fast matrix inverse for rigid transforms (Orthonormal basis and translation)
        public static {0}4x4 fastinverse({0}4x4 m)
        {{
            {0}4 c0 = m.c0;
            {0}4 c1 = m.c1;
            {0}4 c2 = m.c2;
            {0}4 pos = m.c3;

            {0}4 zero = {0}4(0);

            {0}4 t0 = unpacklo(c0, c2);
            {0}4 t1 = unpacklo(c1, zero);
            {0}4 t2 = unpackhi(c0, c2);
            {0}4 t3 = unpackhi(c1, zero);

            {0}4 r0 = unpacklo(t0, t1);
            {0}4 r1 = unpackhi(t0, t1);
            {0}4 r2 = unpacklo(t2, t3);

            pos = -(r0 * pos.x + r1 * pos.y + r2 * pos.z);
            pos.w = 1;

            return {0}4x4(r0, r1, r2, pos);
        }}

", _mBaseType);
            }
        }


        public void GenerateDeterminantFunction(StringBuilder str)
        {
            if (_mRows != _mColumns || _mRows == 1)
                return;

            if (_mBaseType != "float" && _mBaseType != "double" && _mBaseType != "int" && _mBaseType != "Fp")
                return;

            if (_mRows == 2)
            {
                str.AppendFormat(
                    @"        /// <summary>Returns the determinant of a {0}2x2 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static {0} determinant({0}2x2 m)
        {{
            {0} a = m.c0.x;
            {0} b = m.c1.x;
            {0} c = m.c0.y;
            {0} d = m.c1.y;

            return a * d - b * c;
        }}

",
                    _mBaseType);
            }
            else if (_mRows == 3)
            {
                str.AppendFormat(
                    @"        /// <summary>Returns the determinant of a {0}3x3 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static {0} determinant({0}3x3 m)
        {{
            {0}3 c0 = m.c0;
            {0}3 c1 = m.c1;
            {0}3 c2 = m.c2;

            {0} m00 = c1.y * c2.z - c1.z * c2.y;
            {0} m01 = c0.y * c2.z - c0.z * c2.y;
            {0} m02 = c0.y * c1.z - c0.z * c1.y;

            return c0.x * m00 - c1.x * m01 + c2.x * m02;
        }}

",
                    _mBaseType);
            }
            else if (_mRows == 4)
            {
                str.AppendFormat(
                    @"        /// <summary>Returns the determinant of a {0}4x4 matrix.</summary>
        public static {0} determinant({0}4x4 m)
        {{
            {0}4 c0 = m.c0;
            {0}4 c1 = m.c1;
            {0}4 c2 = m.c2;
            {0}4 c3 = m.c3;

            {0} m00 = c1.y * (c2.z * c3.w - c2.w * c3.z) - c2.y * (c1.z * c3.w - c1.w * c3.z) + c3.y * (c1.z * c2.w - c1.w * c2.z);
            {0} m01 = c0.y * (c2.z * c3.w - c2.w * c3.z) - c2.y * (c0.z * c3.w - c0.w * c3.z) + c3.y * (c0.z * c2.w - c0.w * c2.z);
            {0} m02 = c0.y * (c1.z * c3.w - c1.w * c3.z) - c1.y * (c0.z * c3.w - c0.w * c3.z) + c3.y * (c0.z * c1.w - c0.w * c1.z);
            {0} m03 = c0.y * (c1.z * c2.w - c1.w * c2.z) - c1.y * (c0.z * c2.w - c0.w * c2.z) + c2.y * (c0.z * c1.w - c0.w * c1.z);

            return c0.x * m00 - c1.x * m01 + c2.x * m02 - c3.x * m03;
        }}

",
                    _mBaseType);
            }
        }


        public void GenerateHashFunction(StringBuilder str, bool wide)
        {
            if (_mBaseType == "ulong")
                return;
            string returnType = wide ? ToTypeName("uint", _mRows, 1) : "uint";
            string functionName = wide ? "hashwide" : "hash";

            if (wide)
            {
                str.AppendFormat("\t\t/// <summary>\n" +
                                 "\t\t/// Returns a {0} vector hash code of a {1} vector.\n" +
                                 "\t\t/// When multiple elements are to be hashes together, it can more efficient to calculate and combine wide hash\n" +
                                 "\t\t/// that are only reduced to a narrow uint hash at the very end instead of at every step.\n" +
                                 "\t\t/// </summary>\n", returnType, _mTypeName);
            }
            else
            {
                str.AppendFormat("\t\t/// <summary>Returns a uint hash code of a {0} vector.</summary>\n", _mTypeName);
            }


            str.Append("\t\t[MethodImpl(MethodImplOptions.AggressiveInlining)]\n");
            str.AppendFormat("\t\tpublic static {0} {1}({2} v)\n", returnType, functionName, _mTypeName);
            str.Append("\t\t{\n");

            str.AppendFormat("\t\t\treturn ");
            str.Append(wide ? "(" : "csum(");

            if (_mBaseType == "bool")
            {
                for (int column = 0; column < _mColumns; column++)
                {
                    if (column > 0)
                        str.Append(wide ? " + \n\t\t\t\t\t" : " + \n\t\t\t\t\t\t");

                    str.Append("select(");
                    GeneratePrimeUIntVector(str, _mRows);
                    str.Append(", ");
                    GeneratePrimeUIntVector(str, _mRows);
                    str.Append(", ");
                    str.Append(_mColumns > 1 ? "v.c" + column : "v");
                    str.Append(")");
                }

                str.AppendFormat(");\n");
            }
            else
            {
                for (int column = 0; column < _mColumns; column++)
                {
                    if (column > 0)
                        str.Append(wide ? " +\n\t\t\t\t\t" : " +\n\t\t\t\t\t\t");
                    string columnName = _mColumns > 1 ? "v.c" + column : "v";
                    if (_mBaseType != "uint")
                    {
                        if (_mBaseType == "double")
                            columnName = "fold_to_uint(" + columnName + ")";
                        else if (_mBaseType == "half")
                        {
                            if (_mRows == 1)
                                columnName = "v.value";
                            else if (_mRows == 2)
                                columnName = "MathFp.uint2(v.x.value, v.y.value)";
                            else if (_mRows == 3)
                                columnName = "MathFp.uint3(v.x.value, v.y.value, v.z.value)";
                            else if (_mRows == 4)
                                columnName = "MathFp.uint4(v.x.value, v.y.value, v.z.value, v.w.value)";
                        }
                        else
                            columnName = "asuint(" + columnName + ")";
                    }

                    str.Append(columnName);
                    str.Append(" * ");
                    GeneratePrimeUIntVector(str, _mRows);
                }

                str.AppendFormat(") + 0x{0:X}u;\n", NextPrime());
            }

            str.Append("\t\t}\n\n");
        }

        public void GenerateToStringFunction(StringBuilder str, bool useFormat)
        {
            // ** 这三个类型的 tostring 在外部自定义，不使用生成的

            if (_mTypeName == "Fp4" || _mTypeName == "Fp3" || _mTypeName == "Fp2")
            {
                return;
            }

            if (useFormat)
                str.AppendFormat(
                    "\t\t/// <summary>Returns a string representation of the {0} using a specified format and culture-specific format information.</summary>\n",
                    _mTypeName);
            else
                str.AppendFormat("\t\t/// <summary>Returns a string representation of the {0}.</summary>\n",
                    _mTypeName);

            str.Append("\t\t[MethodImpl(MethodImplOptions.AggressiveInlining)]\n");
            if (useFormat)
                str.Append("\t\tpublic string ToString(string format, IFormatProvider formatProvider)\n\t\t{\n");
            else
                str.Append("\t\tpublic override string ToString()\n\t\t{\n");

            str.AppendFormat("\t\t\treturn string.Format(\"{0}(", _mTypeName);


            for (int row = 0; row < _mRows; row++)
            {
                for (int column = 0; column < _mColumns; column++)
                {
                    int idx = row * _mColumns + column;
                    if (idx > 0)
                    {
                        str.Append(", ");
                        if (_mColumns > 1 && column == 0)
                            str.Append(" ");
                    }

                    str.AppendFormat("{{{0}}}", idx);
                    if (_mBaseType == "float")
                        str.Append("f");
                }
            }

            str.Append(")\", ");

            for (int row = 0; row < _mRows; row++)
            {
                for (int column = 0; column < _mColumns; column++)
                {
                    int idx = row * _mColumns + column;
                    if (idx > 0)
                        str.Append(", ");
                    if (_mColumns > 1)
                        str.Append(MatrixFields[column] + "." + VectorFields[row]);
                    else
                        str.Append(VectorFields[row]);
                    if (useFormat)
                        str.Append(".ToString(format, formatProvider)");
                }
            }

            str.Append(");\n");
            str.Append("\t\t}\n\n");
        }

        void GenerateBinaryOperator(int rows, int columns, string op, string resultType, StringBuilder str,
            string opDesc)
        {
            GenerateBinaryOperator(rows, columns, rows, columns, op, resultType, rows, columns, str, opDesc);
            GenerateBinaryOperator(rows, columns, 1, 1, op, resultType, rows, columns, str, opDesc);
            GenerateBinaryOperator(1, 1, rows, columns, op, resultType, rows, columns, str, opDesc);
            str.Append("\n");
        }

        void GenerateBinaryOperator(int lhsRows, int lhsColumns, int rhsRows, int rhsColumns, string op,
            string resultType, int resultRows, int resultColumns, StringBuilder str, string opDesc)
        {
            if (lhsRows == rhsRows && lhsColumns == rhsColumns)
                str.AppendFormat(
                    "\t\t/// <summary>Returns the result of a componentwise {0} operation on {1}.</summary>\n", opDesc,
                    ToValueDescription(_mBaseType, lhsRows, lhsColumns, 2));
            else
                str.AppendFormat(
                    "\t\t/// <summary>Returns the result of a componentwise {0} operation on {1} and {2}.</summary>\n",
                    opDesc, ToValueDescription(_mBaseType, lhsRows, lhsColumns, 1),
                    ToValueDescription(_mBaseType, rhsRows, rhsColumns, 1));

            str.Append("\t\t[MethodImpl(MethodImplOptions.AggressiveInlining)]\n");
            str.AppendFormat("\t\tpublic static {0} operator {1} ({2} lhs, {3} rhs)",
                ToTypeName(resultType, resultRows, resultColumns), op, ToTypeName(_mBaseType, lhsRows, lhsColumns),
                ToTypeName(_mBaseType, rhsRows, rhsColumns));
            str.Append(" { ");
            str.AppendFormat("return new {0} (", ToTypeName(resultType, resultRows, resultColumns));

            string[] fields = (resultColumns > 1) ? MatrixFields : VectorFields;
            int resultCount = (resultColumns > 1) ? resultColumns : resultRows;

            for (int i = 0; i < resultCount; i++)
            {
                if (lhsRows == 1)
                {
                    int rhsIndex = i;
                    str.AppendFormat("lhs {1} rhs.{0}", fields[rhsIndex], op);
                    if (i != resultCount - 1)
                        str.Append(", ");
                }
                else if (rhsRows == 1)
                {
                    int lhsIndex = i;
                    str.AppendFormat("lhs.{0} {1} rhs", fields[lhsIndex], op);
                    if (i != resultCount - 1)
                        str.Append(", ");
                }
                else
                {
                    int lhsIndex = i;
                    int rhsIndex = i;

                    str.AppendFormat("lhs.{0} {2} rhs.{1}", fields[lhsIndex], fields[rhsIndex], op);
                    if (i != resultCount - 1)
                        str.Append(", ");
                }
            }


            str.Append("); }\n\n");
        }

        enum IndexerMode
        {
            ByValue,
            ByRef
        }

        void GenerateIndexOperator(StringBuilder str, IndexerMode mode)
        {
            int count = _mColumns > 1 ? _mColumns : _mRows;
            string returnType = ToTypeName(_mBaseType, _mColumns > 1 ? _mRows : 1, 1);

            var refPrefix = mode == IndexerMode.ByRef ? "ref " : "";

            str.AppendFormat("\t\t/// <summary>Returns the {0} element at a specified index.</summary>\n", returnType);
            str.AppendFormat("\t\tunsafe public {1}{0} this[int index]\n", returnType, refPrefix);
            str.AppendLine("\t\t{");
            str.AppendLine("\t\t\tget");
            str.AppendLine("\t\t\t{");
            str.AppendLine("#if ENABLE_UNITY_COLLECTIONS_CHECKS");
            str.AppendFormat("\t\t\t\tif ((uint)index >= {0})\n", count);
            str.AppendFormat("\t\t\t\t\tthrow new System.ArgumentException(\"index must be between[0...{0}]\");\n",
                count - 1);
            str.AppendLine("#endif");
            // To workaround an undefined behavior with taking the fixed address of a struct field (that could be allocated on the stack)
            // we are fixing this instead of a field
            // See issue https://github.com/dotnet/coreclr/issues/16210
            str.Append("\t\t\t\tfixed (");
            str.Append(_mTypeName);
            str.AppendFormat("* array = &this) {{ return {0}((", refPrefix);
            str.Append(returnType);
            str.Append("*)array)[index]; }\n");
            str.AppendLine("\t\t\t}");

            if (mode == IndexerMode.ByValue)
            {
                str.AppendLine("\t\t\tset");
                str.AppendLine("\t\t\t{");
                str.AppendLine("#if ENABLE_UNITY_COLLECTIONS_CHECKS");
                str.AppendFormat("\t\t\t\tif ((uint)index >= {0})\n", count);
                str.AppendFormat("\t\t\t\t\tthrow new System.ArgumentException(\"index must be between[0...{0}]\");\n",
                    count - 1);
                str.AppendLine("#endif");
                str.Append("\t\t\t\tfixed (");
                str.Append(returnType);
                str.Append("* array = &");
                str.Append(_mColumns > 1 ? "c0" : "x");
                str.Append(") { array[index] = value; }\n");
                str.AppendLine("\t\t\t}");
            }

            str.AppendLine("\t\t}");
        }

        void GenerateEquals(StringBuilder str)
        {
            string[] fields = (_mColumns > 1) ? MatrixFields : VectorFields;
            int resultCount = (_mColumns > 1) ? _mColumns : _mRows;

            str.AppendFormat(
                "\t\t/// <summary>Returns true if the {0} is equal to a given {0}, false otherwise.</summary>\n",
                _mTypeName);
            str.Append("\t\t[MethodImpl(MethodImplOptions.AggressiveInlining)]\n");
            str.AppendFormat("\t\tpublic bool Equals({0} rhs) {{ return ", _mTypeName);

            for (int i = 0; i < resultCount; i++)
            {
                if (_mColumns == 1)
                    str.AppendFormat("{0} == rhs.{0}", fields[i]);
                else
                    str.AppendFormat("{0}.Equals(rhs.{0})", fields[i]);
                if (i != resultCount - 1)
                    str.Append(" && ");
            }

            str.Append("; }\n\n");

            str.AppendFormat(
                "\t\t/// <summary>Returns true if the {0} is equal to a given {0}, false otherwise.</summary>\n",
                _mTypeName);
            str.AppendFormat("\t\tpublic override bool Equals(object o) {{ return Equals(({0})o); }}\n\n", _mTypeName);
        }

        void GenerateGetHashCode(StringBuilder str)
        {
            if (_mBaseType == "ulong")
                return;
            str.AppendFormat("\t\t/// <summary>Returns a hash code for the {0}.</summary>\n", _mTypeName);
            str.Append("\t\t[MethodImpl(MethodImplOptions.AggressiveInlining)]\n");
            str.Append("\t\tpublic override int GetHashCode() { return (int)MathFp.hash(this); }\n\n");
        }


        void GenerateShiftOperator(int lhsRows, string op, StringBuilder str, string opDesc)
        {
            string[] fields = (_mColumns > 1) ? MatrixFields : VectorFields;
            int resultCount = (_mColumns > 1) ? _mColumns : _mRows;

            str.AppendFormat(
                "\t\t/// <summary>Returns the result of a componentwise {0} operation on {1} by a number of bits specified by a single int.</summary>\n",
                opDesc, ToValueDescription(_mBaseType, _mRows, _mColumns, 1));
            str.Append("\t\t[MethodImpl(MethodImplOptions.AggressiveInlining)]\n");
            str.AppendFormat("\t\tpublic static {0} operator {1} ({0} x, int n)", _mTypeName, op);
            str.Append(" { ");
            str.AppendFormat("return new {0} (", _mTypeName);

            for (int i = 0; i < resultCount; i++)
            {
                if (lhsRows == 1)
                {
                    str.AppendFormat("x {0} n", op);
                    if (i != resultCount - 1)
                        str.Append(", ");
                }
                else
                {
                    int lhsIndex = i;
                    str.AppendFormat("x.{0} {1} n", fields[lhsIndex], op);
                    if (i != resultCount - 1)
                        str.Append(", ");
                }
            }

            str.Append("); }\n\n");
        }

        void GenerateUnaryOperator(string op, StringBuilder str, string opDesc)
        {
            str.AppendFormat("\t\t/// <summary>Returns the result of a componentwise {0} operation on {1}.</summary>\n",
                opDesc, ToValueDescription(_mBaseType, _mRows, _mColumns, 1));
            str.Append("\t\t[MethodImpl(MethodImplOptions.AggressiveInlining)]\n");
            str.AppendFormat("\t\tpublic static {0} operator {1} ({0} val)", _mTypeName, op);
            str.Append(" { ");
            str.AppendFormat("return new {0} (", _mTypeName);

            string[] fields = (_mColumns > 1) ? MatrixFields : VectorFields;
            int resultCount = (_mColumns > 1) ? _mColumns : _mRows;

            for (int i = 0; i < resultCount; i++)
            {
                if (op == "-" && _mBaseType == "uint" && _mColumns == 1)
                    str.AppendFormat("(uint){0}val.{1}", op, fields[i]);
                else
                    str.AppendFormat("{0}val.{1}", op, fields[i]);
                if (i != resultCount - 1)
                    str.Append(", ");
            }

            str.Append("); }\n\n\n");
        }

        void GenerateSwizzles(StringBuilder str)
        {
            int count = _mRows;
            // float4 swizzles
            {
                int[] swizzles = new int[4];
                for (int x = 0; x < count; x++)
                {
                    for (int y = 0; y < count; y++)
                    {
                        for (int z = 0; z < count; z++)
                        {
                            for (int w = 0; w < count; w++)
                            {
                                swizzles[0] = x;
                                swizzles[1] = y;
                                swizzles[2] = z;
                                swizzles[3] = w;

                                GenerateSwizzles(swizzles, str);
                            }
                        }
                    }
                }
            }

            // float3 swizzles
            {
                var swizzles = new int[3];
                for (int x = 0; x < count; x++)
                {
                    for (int y = 0; y < count; y++)
                    {
                        for (int z = 0; z < count; z++)
                        {
                            swizzles[0] = x;
                            swizzles[1] = y;
                            swizzles[2] = z;

                            GenerateSwizzles(swizzles, str);
                        }
                    }
                }
            }

            // float2 swizzles
            {
                var swizzles = new int[2];
                for (int x = 0; x < count; x++)
                {
                    for (int y = 0; y < count; y++)
                    {
                        swizzles[0] = x;
                        swizzles[1] = y;

                        GenerateSwizzles(swizzles, str);
                    }
                }
            }
        }

        void GenerateSwizzles(int[] swizzle, StringBuilder str)
        {
            int bits = 0;
            bool allowSetter = true;
            for (int i = 0; i < swizzle.Length; i++)
            {
                int bit = 1 << swizzle[i];
                if ((bits & bit) != 0)
                    allowSetter = false;

                bits |= 1 << swizzle[i];
            }

            str.Append(
                "\t\t[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]\n");
            str.Append("\t\tpublic ");
            str.Append(ToTypeName(_mBaseType, swizzle.Length, 1));
            str.Append(' ');

            for (int i = 0; i < swizzle.Length; i++)
                str.Append(Components[swizzle[i]]);

            // Getter

            str.Append("\n\t\t{");
            if (swizzle.Length != 1)
            {
                str.AppendFormat("\n\t\t\t[MethodImpl(MethodImplOptions.AggressiveInlining)]");
                str.Append("\n\t\t\tget { return new ");
                str.Append(ToTypeName(_mBaseType, swizzle.Length, 1));
                str.Append('(');
            }
            else
                str.Append("\n\t\t\tget { return ");

            for (int i = 0; i < swizzle.Length; i++)
            {
                str.Append(Components[swizzle[i]]);

                if (i != swizzle.Length - 1)
                    str.Append(", ");
            }

            if (swizzle.Length != 1)
                str.Append("); }");
            else
                str.Append("; }");

            //Setter
            if (allowSetter)
            {
                str.AppendFormat("\n\t\t\t[MethodImpl(MethodImplOptions.AggressiveInlining)]");
                str.Append("\n\t\t\tset { ");
                for (int i = 0; i < swizzle.Length; i++)
                {
                    str.Append(Components[swizzle[i]]);
                    if (swizzle.Length != 1)
                    {
                        str.Append(" = value.");
                        str.Append(Components[i]);
                    }
                    else
                    {
                        str.Append(" = value");
                    }

                    str.Append("; ");
                }

                str.Append("}");
            }

            str.Append("\n\t\t}\n\n");
            str.Append("\n");
        }

        // Test Generation

        private void BeginTest(StringBuilder str, string name)
        {
            str.Append("\t\t[Test]\n");
            str.AppendFormat("\t\tpublic static void {0}()\n", name);
            str.Append("\t\t{\n");
        }

        private void EndTest(StringBuilder str)
        {
            str.Append("\t\t}\n\n");
        }

        private void AddParenthesized(StringBuilder str, string[] strings)
        {
            str.Append("(");
            str.Append(string.Join(", ", strings));
            str.Append(")");
        }

        private void TestStaticFields(StringBuilder str)
        {
            if (_mBaseType == "bool")
                return;

            if (_mColumns == 1)
            {
                BeginTest(str, _mTypeName + "_zero");
                for (int row = 0; row < _mRows; row++)
                    str.AppendFormat("\t\t\tTestUtils.AreEqual({0}.zero.{1}, {2});\n", _mTypeName, Components[row],
                        ToTypedLiteral(_mBaseType, 0));
                EndTest(str);
            }
            else
            {
                BeginTest(str, _mTypeName + "_zero");
                for (int column = 0; column < _mColumns; column++)
                for (int row = 0; row < _mRows; row++)
                    str.AppendFormat("\t\t\tTestUtils.AreEqual({0}.zero.c{1}.{2}, {3});\n", _mTypeName, column,
                        Components[row], ToTypedLiteral(_mBaseType, 0));
                EndTest(str);

                if (_mColumns == _mRows)
                {
                    BeginTest(str, _mTypeName + "_identity");
                    for (int column = 0; column < _mColumns; column++)
                    for (int row = 0; row < _mRows; row++)
                        str.AppendFormat("\t\t\tTestUtils.AreEqual({0}.identity.c{1}.{2}, {3});\n", _mTypeName, column,
                            Components[row], ToTypedLiteral(_mBaseType, column == row ? 1 : 0));
                    EndTest(str);
                }
            }
        }

        private void TestConstructor(StringBuilder str, bool isStatic, bool isScalar)
        {
            if (_mColumns == 1)
            {
                string name = "constructor";
                if (isScalar)
                    name = "scalar_" + name;
                if (isStatic)
                    name = "static_" + name;

                BeginTest(str, _mTypeName + "_" + name);
                str.AppendFormat("\t\t\t{0} a = ", _mTypeName);
                if (!isStatic)
                    str.Append("new ");
                str.Append(_mTypeName);

                if (isScalar)
                {
                    string value = _mBaseType == "bool" ? "true" :
                        _mBaseType == "uint" ? "17u" :
                        _mBaseType == "int" ? "17" :
                        _mBaseType == "float" ? "17.0f" :
                        _mBaseType == "double" ? "17.0" :
                        _mBaseType == "Fp" ? "17.0m" : "UNSUPPORTED_TYPE_IN_TEST_CONSTRUCTOR";
                    str.Append("(" + value + ");\n");

                    for (int row = 0; row < _mRows; row++)
                    {
                        str.AppendFormat("\t\t\tTestUtils.AreEqual(a.{0}, {1});\n", Components[row], value);
                    }
                }
                else
                {
                    string[] values = (from row in Enumerable.Range(0, _mRows)
                        select _mBaseType == "bool" ? ((row & 1) != 0 ? "true" : "false") : "" + (row + 1)).ToArray();
                    AddParenthesized(str, values);
                    str.Append(";\n");

                    for (int row = 0; row < _mRows; row++)
                    {
                        str.AppendFormat("\t\t\tTestUtils.AreEqual(a.{0}, {1});\n", Components[row], values[row]);
                    }
                }


                EndTest(str);
            }
        }

        private void TestConstructors(StringBuilder str)
        {
            TestConstructor(str, false, false);
            TestConstructor(str, false, true);
            TestConstructor(str, true, false);
            TestConstructor(str, true, true);
        }

        private void TestOperator(StringBuilder str, bool lhsWide, bool rhsWide, string lhsType, string rhsType,
            string returnType, string op, string opName, bool isBinary, bool isPrefix)
        {
            var rnd = new Random(opName.GetHashCode());

            BeginTest(str, _mTypeName + "_operator_" + opName);

            int numValues = _mRows * _mColumns;
            int numPasses = 4;

            string[] lhsValues = new string[lhsWide ? numValues : 1];
            string[] rhsValues = new string[rhsWide ? numValues : 1];
            string[] resultValues = new string[numValues];

            for (int pass = 0; pass < numPasses; pass++)
            {
                bool boolA = false;
                bool boolB = false;
                int intA = 0;
                int intB = 0;
                uint uintA = 0;
                uint uintB = 0;
                float floatA = 0.0f;
                float floatB = 0.0f;
                double doubleA = 0.0;
                double doubleB = 0.0;

                for (int i = 0; i < numValues; i++)
                {
                    string lhsValue = "";
                    string rhsValue = "";
                    string resultValue = "";

                    if (_mBaseType == "bool")
                    {
                        if (i == 0 || lhsWide) boolA = (rnd.Next(2) == 1);
                        if (i == 0 || rhsWide) boolB = (rnd.Next(2) == 1);

                        lhsValue = boolA ? "true" : "false";
                        rhsValue = boolB ? "true" : "false";
                        switch (op)
                        {
                            case "==":
                                resultValue = (boolA == boolB) ? "true" : "false";
                                break;
                            case "!=":
                                resultValue = (boolA != boolB) ? "true" : "false";
                                break;

                            case "&":
                                resultValue = (boolA & boolB) ? "true" : "false";
                                break;
                            case "|":
                                resultValue = (boolA | boolB) ? "true" : "false";
                                break;
                            case "^":
                                resultValue = (boolA ^ boolB) ? "true" : "false";
                                break;
                            case "!":
                                resultValue = (!boolA) ? "true" : "false";
                                break;
                        }
                    }
                    else if (_mBaseType == "int")
                    {
                        if (i == 0 || lhsWide) intA = rnd.Next();
                        if (i == 0 || rhsWide) intB = rnd.Next();


                        lhsValue = "" + intA;
                        rhsValue = "" + intB;
                        switch (op)
                        {
                            case "+":
                                resultValue = "" + (isBinary ? (intA + intB) : +intA);
                                break;
                            case "-":
                                resultValue = "" + (isBinary ? (intA - intB) : -intA);
                                break;
                            case "*":
                                resultValue = "" + (intA * intB);
                                break;
                            case "/":
                                resultValue = "" + (intA / intB);
                                break;
                            case "%":
                                resultValue = "" + (intA % intB);
                                break;

                            case "<":
                                resultValue = (intA < intB) ? "true" : "false";
                                break;
                            case ">":
                                resultValue = (intA > intB) ? "true" : "false";
                                break;
                            case "==":
                                resultValue = (intA == intB) ? "true" : "false";
                                break;
                            case "!=":
                                resultValue = (intA != intB) ? "true" : "false";
                                break;
                            case "<=":
                                resultValue = (intA <= intB) ? "true" : "false";
                                break;
                            case ">=":
                                resultValue = (intA >= intB) ? "true" : "false";
                                break;

                            case "&":
                                resultValue = "" + (intA & intB);
                                break;
                            case "|":
                                resultValue = "" + (intA | intB);
                                break;
                            case "^":
                                resultValue = "" + (intA ^ intB);
                                break;
                            case "<<":
                                resultValue = "" + (intA << intB);
                                break;
                            case ">>":
                                resultValue = "" + (intA >> intB);
                                break;

                            case "~":
                                resultValue = "" + (~intA);
                                break;
                            case "++":
                                resultValue = "" + (isPrefix ? ++intA : intA++);
                                break;
                            case "--":
                                resultValue = "" + (isPrefix ? --intA : intA--);
                                break;
                        }
                    }
                    else if (_mBaseType == "uint")
                    {
                        if (i == 0 || lhsWide) uintA = (uint)rnd.Next();
                        if (i == 0 || rhsWide) uintB = (uint)rnd.Next();

                        lhsValue = "" + uintA;
                        rhsValue = "" + uintB;
                        switch (op)
                        {
                            case "+":
                                resultValue = "" + (isBinary ? (uintA + uintB) : +uintA);
                                break;
                            case "-":
                                resultValue = "" + (isBinary ? (uintA - uintB) : (uint)-uintA);
                                break;
                            case "*":
                                resultValue = "" + (uintA * uintB);
                                break;
                            case "/":
                                resultValue = "" + (uintA / uintB);
                                break;
                            case "%":
                                resultValue = "" + (uintA % uintB);
                                break;

                            case "<":
                                resultValue = (uintA < uintB) ? "true" : "false";
                                break;
                            case ">":
                                resultValue = (uintA > uintB) ? "true" : "false";
                                break;
                            case "==":
                                resultValue = (uintA == uintB) ? "true" : "false";
                                break;
                            case "!=":
                                resultValue = (uintA != uintB) ? "true" : "false";
                                break;
                            case "<=":
                                resultValue = (uintA <= uintB) ? "true" : "false";
                                break;
                            case ">=":
                                resultValue = (uintA >= uintB) ? "true" : "false";
                                break;

                            case "&":
                                resultValue = "" + (uintA & uintB);
                                break;
                            case "|":
                                resultValue = "" + (uintA | uintB);
                                break;
                            case "^":
                                resultValue = "" + (uintA ^ uintB);
                                break;
                            case "<<":
                                resultValue = "" + (uintA << (int)uintB);
                                break;
                            case ">>":
                                resultValue = "" + (uintA >> (int)uintB);
                                break;

                            case "~":
                                resultValue = "" + (~uintA);
                                break;
                            case "++":
                                resultValue = "" + (isPrefix ? ++uintA : uintA++);
                                break;
                            case "--":
                                resultValue = "" + (isPrefix ? --uintA : uintA--);
                                break;
                        }
                    }
                    else if (_mBaseType == "float")
                    {
                        if (i == 0 || lhsWide) floatA = (float)rnd.NextDouble() * 1024.0f - 512.0f;
                        if (i == 0 || rhsWide) floatB = (float)rnd.NextDouble() * 1024.0f - 512.0f;

                        lhsValue = "" + floatA.ToString("R") + "f";
                        rhsValue = "" + floatB.ToString("R") + "f";
                        switch (op)
                        {
                            case "+":
                                resultValue = "" + (isBinary ? (floatA + floatB) : +floatA).ToString("R") + "f";
                                break;
                            case "-":
                                resultValue = "" + (isBinary ? (floatA - floatB) : -floatA).ToString("R") + "f";
                                break;
                            case "*":
                                resultValue = "" + (floatA * floatB).ToString("R") + "f";
                                break;
                            case "/":
                                resultValue = "" + (floatA / floatB).ToString("R") + "f";
                                break;
                            case "%":
                                resultValue = "" + (floatA % floatB).ToString("R") + "f";
                                break;

                            case "<":
                                resultValue = (floatA < floatB) ? "true" : "false";
                                break;
                            case ">":
                                resultValue = (floatA > floatB) ? "true" : "false";
                                break;
                            case "==":
                                resultValue = (floatA == floatB) ? "true" : "false";
                                break;
                            case "!=":
                                resultValue = (floatA != floatB) ? "true" : "false";
                                break;
                            case "<=":
                                resultValue = (floatA <= floatB) ? "true" : "false";
                                break;
                            case ">=":
                                resultValue = (floatA >= floatB) ? "true" : "false";
                                break;

                            case "++":
                                resultValue = "" + (isPrefix ? ++floatA : floatA++).ToString("R") + "f";
                                break;
                            case "--":
                                resultValue = "" + (isPrefix ? --floatA : floatA--).ToString("R") + "f";
                                break;
                        }
                    }
                    else if (_mBaseType == "Fp")
                    {
                        if (i == 0 || lhsWide) floatA = (float)rnd.NextDouble() * 1024.0f - 512.0f;
                        if (i == 0 || rhsWide) floatB = (float)rnd.NextDouble() * 1024.0f - 512.0f;

                        lhsValue = "" + floatA.ToString("R") + "m";
                        rhsValue = "" + floatB.ToString("R") + "m";
                        switch (op)
                        {
                            case "+":
                                resultValue = "" + (isBinary ? (floatA + floatB) : +floatA).ToString("R") + "m";
                                break;
                            case "-":
                                resultValue = "" + (isBinary ? (floatA - floatB) : -floatA).ToString("R") + "m";
                                break;
                            case "*":
                                resultValue = "" + (floatA * floatB).ToString("R") + "m";
                                break;
                            case "/":
                                resultValue = "" + (floatA / floatB).ToString("R") + "m";
                                break;
                            case "%":
                                resultValue = "" + (floatA % floatB).ToString("R") + "m";
                                break;

                            case "<":
                                resultValue = (floatA < floatB) ? "true" : "false";
                                break;
                            case ">":
                                resultValue = (floatA > floatB) ? "true" : "false";
                                break;
                            case "==":
                                resultValue = (floatA == floatB) ? "true" : "false";
                                break;
                            case "!=":
                                resultValue = (floatA != floatB) ? "true" : "false";
                                break;
                            case "<=":
                                resultValue = (floatA <= floatB) ? "true" : "false";
                                break;
                            case ">=":
                                resultValue = (floatA >= floatB) ? "true" : "false";
                                break;

                            case "++":
                                resultValue = "" + (isPrefix ? ++floatA : floatA++).ToString("R") + "m";
                                break;
                            case "--":
                                resultValue = "" + (isPrefix ? --floatA : floatA--).ToString("R") + "m";
                                break;
                        }
                    }
                    else if (_mBaseType == "double")
                    {
                        if (i == 0 || lhsWide) doubleA = rnd.NextDouble() * 1024.0 - 512.0;
                        if (i == 0 || rhsWide) doubleB = rnd.NextDouble() * 1024.0 - 512.0;

                        lhsValue = "" + doubleA.ToString("R");
                        rhsValue = "" + doubleB.ToString("R");
                        switch (op)
                        {
                            case "+":
                                resultValue = "" + (isBinary ? (doubleA + doubleB) : +doubleA).ToString("R");
                                break;
                            case "-":
                                resultValue = "" + (isBinary ? (doubleA - doubleB) : -doubleA).ToString("R");
                                break;
                            case "*":
                                resultValue = "" + (doubleA * doubleB).ToString("R");
                                break;
                            case "/":
                                resultValue = "" + (doubleA / doubleB).ToString("R");
                                break;
                            case "%":
                                resultValue = "" + (doubleA % doubleB).ToString("R");
                                break;

                            case "<":
                                resultValue = (doubleA < doubleB) ? "true" : "false";
                                break;
                            case ">":
                                resultValue = (doubleA > doubleB) ? "true" : "false";
                                break;
                            case "==":
                                resultValue = (doubleA == doubleB) ? "true" : "false";
                                break;
                            case "!=":
                                resultValue = (doubleA != doubleB) ? "true" : "false";
                                break;
                            case "<=":
                                resultValue = (doubleA <= doubleB) ? "true" : "false";
                                break;
                            case ">=":
                                resultValue = (doubleA >= doubleB) ? "true" : "false";
                                break;

                            case "++":
                                resultValue = "" + (isPrefix ? ++doubleA : doubleA++).ToString("R");
                                break;
                            case "--":
                                resultValue = "" + (isPrefix ? --doubleA : doubleA--).ToString("R");
                                break;
                        }
                    }

                    if (i == 0 || lhsWide) lhsValues[i] = lhsValue;
                    if (i == 0 || rhsWide) rhsValues[i] = rhsValue;
                    resultValues[i] = resultValue;
                }

                str.AppendFormat("\t\t\t{0} a{1} = ", lhsType, pass);
                if (lhsWide) str.Append(lhsType);
                AddParenthesized(str, lhsValues);
                str.Append(";\n");

                // ** 锁定 wide 测试的精度到 0.1
                var precision = "";
                if (_mBaseType == "Fp" && opName.Contains("wide") && opName.Contains("mul"))
                {
                    precision = ", Fp.Point1";
                }

                if (isBinary)
                {
                    str.AppendFormat("\t\t\t{0} b{1} = ", rhsType, pass);
                    if (rhsWide) str.Append(rhsType);
                    AddParenthesized(str, rhsValues);
                    str.Append(";\n");

                    str.AppendFormat("\t\t\t{0} r{1} = {0}", returnType, pass);
                    AddParenthesized(str, resultValues);
                    str.Append(";\n");

                    str.AppendFormat("\t\t\tTestUtils.AreEqual(a{1} {0} b{1}, r{1}{2});\n", op, pass, precision);
                }
                else
                {
                    str.AppendFormat("\t\t\t{0} r{1} = {0}", returnType, pass);
                    AddParenthesized(str, resultValues);
                    str.Append(";\n");

                    if (isPrefix)
                    {
                        str.AppendFormat("\t\t\tTestUtils.AreEqual({0}a{1}, r{1}{2});\n", op, pass, precision);
                    }
                    else
                    {
                        str.AppendFormat("\t\t\tTestUtils.AreEqual(a{1}{0}, r{1}{2});\n", op, pass, precision);
                    }
                }

                if (pass != numPasses - 1)
                    str.Append("\n");
            }

            EndTest(str);
        }

        private void TestShuffle(StringBuilder str)
        {
            if (_mBaseType == "bool")
                return;

            var rnd = new Random(0);

            for (int resultComponents = 1; resultComponents <= 4; resultComponents++)
            {
                BeginTest(str, _mTypeName + "_shuffle_result_" + resultComponents);

                string resultType = ToTypeName(_mBaseType, resultComponents, 1);
                str.AppendFormat("\t\t\t{0} a = {0}", _mTypeName);
                var dataArrA = (from row in Enumerable.Range(0, _mRows)
                    select _mBaseType == "bool" ? ((row & 1) != 0 ? "true" : "false") : "" + (row)).ToArray();
                var dataArrB = (from row in Enumerable.Range(0, _mRows)
                    select _mBaseType == "bool" ? ((row & 1) != 1 ? "true" : "false") : "" + (row + _mRows)).ToArray();

                AddParenthesized(str, dataArrA);
                str.Append(";\n");
                str.AppendFormat("\t\t\t{0} b = {0}", _mTypeName);
                AddParenthesized(str, dataArrB);
                str.Append(";\n\n");


                int totalTests = (int)Math.Pow(_mRows * 2, resultComponents);
                int targetTests = 16;
                int numTests = Math.Min(totalTests, targetTests);

                int[] shuffleIndices = new int[resultComponents];
                string[] shuffleValues = new string[resultComponents];

                for (int testIndex = 0; testIndex < numTests; testIndex++)
                {
                    if (numTests == totalTests)
                    {
                        // just enumerate all of them
                        int t = testIndex;
                        for (int i = 0; i < resultComponents; i++)
                        {
                            shuffleIndices[i] = t % (_mRows * 2);
                            t /= _mRows * 2;
                        }
                    }
                    else
                    {
                        // sample randomly
                        for (int i = 0; i < resultComponents; i++)
                            shuffleIndices[i] = rnd.Next(_mRows * 2);
                    }

                    str.Append("\t\t\tTestUtils.AreEqual(shuffle(a, b");
                    for (int i = 0; i < resultComponents; i++)
                    {
                        int t = shuffleIndices[i];
                        shuffleValues[i] = t >= _mRows ? dataArrB[t - _mRows] : dataArrA[t];
                        str.AppendFormat(", ShuffleComponent.{0}",
                            ShuffleComponents[t >= _mRows ? (t - _mRows + 4) : t]);
                    }

                    str.Append("), ");
                    if (resultComponents > 1)
                        str.Append(resultType);

                    AddParenthesized(str, shuffleValues);
                    str.Append(");\n");
                }


                EndTest(str);
            }
        }

        private void TestUnaryOperator(StringBuilder str, string returnType, string op, string opName, bool isPrefix)
        {
            TestOperator(str, true, false, _mTypeName, _mBaseType, returnType, op, opName, false, isPrefix);
        }

        private void TestBinaryOperator(StringBuilder str, string returnType, string op, string opName)
        {
            TestOperator(str, true, true, _mTypeName, _mTypeName, returnType, op, opName + "_wide_wide", true, false);
            TestOperator(str, true, false, _mTypeName, _mBaseType, returnType, op, opName + "_wide_scalar", true,
                false);
            TestOperator(str, false, true, _mBaseType, _mTypeName, returnType, op, opName + "_scalar_wide", true,
                false);
        }

        private void TestOperators(StringBuilder str)
        {
            string boolResultType = ToTypeName("bool", _mRows, _mColumns);

            TestBinaryOperator(str, boolResultType, "==", "equal");
            TestBinaryOperator(str, boolResultType, "!=", "not_equal");

            if (_mBaseType == "int" || _mBaseType == "uint" || _mBaseType == "float" || _mBaseType == "double" ||
                _mBaseType == "Fp")
            {
                TestBinaryOperator(str, boolResultType, "<", "less");
                TestBinaryOperator(str, boolResultType, ">", "greater");
                TestBinaryOperator(str, boolResultType, "<=", "less_equal");
                TestBinaryOperator(str, boolResultType, ">=", "greater_equal");

                TestBinaryOperator(str, _mTypeName, "+", "add");
                TestBinaryOperator(str, _mTypeName, "-", "sub");
                TestBinaryOperator(str, _mTypeName, "*", "mul");
                TestBinaryOperator(str, _mTypeName, "/", "div");
                TestBinaryOperator(str, _mTypeName, "%", "mod");

                TestUnaryOperator(str, _mTypeName, "+", "plus", true);
                TestUnaryOperator(str, _mTypeName, "-", "neg", true);

                TestUnaryOperator(str, _mTypeName, "++", "prefix_inc", true);
                TestUnaryOperator(str, _mTypeName, "++", "postfix_inc", false);
                TestUnaryOperator(str, _mTypeName, "--", "prefix_dec", true);
                TestUnaryOperator(str, _mTypeName, "--", "postfix_dec", false);
            }

            if (_mBaseType == "bool" || _mBaseType == "int" || _mBaseType == "uint")
            {
                TestBinaryOperator(str, _mTypeName, "&", "bitwise_and");
                TestBinaryOperator(str, _mTypeName, "|", "bitwise_or");
                TestBinaryOperator(str, _mTypeName, "^", "bitwise_xor");
            }

            if (_mBaseType == "bool")
            {
                TestUnaryOperator(str, _mTypeName, "!", "logical_not", true);
            }

            if (_mBaseType == "int" || _mBaseType == "uint")
            {
                TestOperator(str, true, false, _mTypeName, "int", _mTypeName, "<<", "left_shift", true, false);
                TestOperator(str, true, false, _mTypeName, "int", _mTypeName, ">>", "right_shift", true, false);

                TestUnaryOperator(str, _mTypeName, "~", "bitwise_not", true);
            }

            if (_mColumns == 1)
            {
                TestShuffle(str);
            }
        }

        private T[] GetColumn<T>(T[,] matrix, int columnNumber)
        {
            return Enumerable.Range(0, matrix.GetLength(0)).Select(x => matrix[x, columnNumber]).ToArray();
        }

        private string ToLiteral<T>(T value)
        {
            if (typeof(T) == typeof(float))
            {
                float f = (float)(object)value;
                uint uf = AsUInt(f);
                if (float.IsPositiveInfinity(f))
                    return "float.PositiveInfinity";
                if (float.IsNegativeInfinity(f))
                    return "float.NegativeInfinity";
                if (f == float.MaxValue)
                    return "float.MaxValue";
                if (f == float.MinValue)
                    return "float.MinValue";
                if (float.IsNaN(f))
                    return uf >= 0x80000000u ? "float.NaN" : "-float.NaN";
                if (uf == 0x80000000u)
                    return "-0.0f";
                return ((float)(object)value).ToString("R") + "f";
            }

            if (typeof(T) == typeof(double))
            {
                double d = (double)(object)value;
                ulong ud = AsULong(d);
                if (double.IsPositiveInfinity(d))
                    return "double.PositiveInfinity";
                if (double.IsNegativeInfinity(d))
                    return "double.NegativeInfinity";
                if (d == double.MaxValue)
                    return "double.MaxValue";
                if (d == double.MinValue)
                    return "double.MinValue";
                if (double.IsNaN(d))
                    return ud >= 0x8000000000000000ul ? "double.NaN" : "-double.NaN";
                if (ud == 0x8000000000000000ul)
                    return "-0.0";
                string s = ((double)(object)value).ToString("R");
                if (s.IndexOfAny("eE.".ToCharArray()) == -1)
                    s += ".0";
                return s;
            }

            if (typeof(T) == typeof(decimal))
            {
                decimal d = (decimal)(object)value;

                if (d == decimal.MaxValue)
                    return "double.MaxValue";
                if (d == decimal.MinValue)
                    return "double.MinValue";
                return ((decimal)(object)value).ToString(CultureInfo.CurrentCulture) + "m";
            }

            if (typeof(T) == typeof(uint))
            {
                return "" + value + "u";
            }

            if (typeof(T) == typeof(long))
            {
                return "" + value + "L";
            }

            if (typeof(T) == typeof(ulong))
            {
                return "" + value + "UL";
            }

            if (typeof(T) == typeof(bool))
            {
                return (bool)(object)value ? "true" : "false";
            }

            return "" + value;
        }

        private void GenerateComponentWiseParam<T>(StringBuilder str, string typeName, int numComponents, T[] input,
            int test)
        {
            int numTests = input.GetLength(0);
            if (numComponents > 1)
                str.Append(typeName + numComponents + "(");
            for (int i = 0; i < numComponents; i++)
            {
                int idx = Math.Min(test + i, numTests - 1);
                T v = input[idx];
                if (i > 0)
                    str.Append(", ");
                str.Append(ToLiteral(v));
            }

            if (numComponents > 1)
                str.Append(")");
        }

        private string GetTypeName(Type t)
        {
            if (t == typeof(float))
                return "float";
            if (t == typeof(double))
                return "double";
            if (t == typeof(bool))
                return "bool";
            if (t == typeof(int))
                return "int";
            if (t == typeof(uint))
                return "uint";
            if (t == typeof(long))
                return "long";
            if (t == typeof(ulong))
                return "ulong";
            if (t == typeof(decimal))
                return "Fp";
            return "";
        }

        private void GenerateComponentWiseTest<TIn, TOut>(StringBuilder str, string functionName, TIn[,] input,
            TOut[] output,
            int maxComponents, long maxUps = 0, bool signedZeroEqual = false)
        {
            int numTests = input.GetLength(0);
            int numParams = input.GetLength(1);
            string inputTypeName = GetTypeName(typeof(TIn));
            string outputTypeName = GetTypeName(typeof(TOut));
            for (int numComponents = 1; numComponents <= maxComponents; numComponents++)
            {
                BeginTest(str, functionName + "_" + inputTypeName + (numComponents > 1 ? ("" + numComponents) : ""));
                for (int test = 0; test < numTests; test += numComponents)
                {
                    str.AppendFormat("\t\t\tTestUtils.AreEqual({0}(", functionName);
                    for (int param = 0; param < numParams; param++)
                    {
                        if (param > 0)
                            str.Append(", ");
                        GenerateComponentWiseParam(str, inputTypeName, numComponents, GetColumn(input, param), test);
                    }

                    str.Append("), ");
                    GenerateComponentWiseParam(str, outputTypeName, numComponents, output, test);

                    if ((typeof(TOut) == typeof(decimal) || typeof(TOut) == typeof(float) ||
                         typeof(TOut) == typeof(double)) &&
                        maxUps > 0)
                    {
                        str.Append(", " + maxUps + (maxUps >= int.MaxValue ? "L" : "") + ", " +
                                   (signedZeroEqual ? "true" : "false"));
                    }

                    str.Append(");\n");
                }

                EndTest(str);
            }
        }

        // private void GenerateComponentWiseTestFloatAndDouble(StringBuilder str, string functionName, double[,] input,
        //     double[] output, int floatMaxEps = 0, int doubleMaxEps = 0, bool signedZeroEqual = false)
        // {
        //     float[,] inputFloat = new float[input.GetLength(0), input.GetLength(1)];
        //
        //     for (int i = 0; i < input.GetLength(0); i++)
        //     for (int j = 0; j < input.GetLength(1); j++)
        //         inputFloat[i, j] = (float)input[i, j];
        //     float[] outputFloat = new float[output.GetLength(0)];
        //     for (int i = 0; i < output.GetLength(0); i++)
        //         outputFloat[i] = (float)output[i];
        //     GenerateComponentWiseTest(str, functionName, inputFloat, outputFloat, 4, floatMaxEps, signedZeroEqual);
        //     GenerateComponentWiseTest(str, functionName, input, output, 4, doubleMaxEps, signedZeroEqual);
        // }

        [SuppressMessage("ReSharper", "CommentTypo")]
        private void GenerateMathTests(StringBuilder str)
        {
            str.Append("using NUnit.Framework;\n");
            str.Append("using static Unity.Mathematics.FixedPoint.MathFp;\n");
            // str.Append("using static Unity.Mathematics.math;\n\n");
            str.Append("namespace Unity.Mathematics.FixedPoint.Tests\n");
            str.Append("{\n");
            str.Append("\t[TestFixture]\n");
            str.Append("\tpublic partial class TestMath\n");
            str.Append("\t{\n");
            str.Append("#if ENABLE_DECIMAL\n");
            /*
            GenerateComponentWiseTest(str, "abs", new int[,] { { 0 }, { -7 }, { 11 }, { -2147483647 }, { -2147483648 } }, new int[] { 0, 7, 11, 2147483647, -2147483648 }, 4);

            GenerateComponentWiseTestFloatAndDouble(str, "abs", new double[,] { { 0.0 }, { -1.1 }, { 2.2 }, { double.NegativeInfinity }, { double.PositiveInfinity } }, new double[] { 0.0, 1.1, 2.2, double.PositiveInfinity, double.PositiveInfinity }, 0, 0, false);

            GenerateComponentWiseTest(str, "isfinite", new float[,] { { -float.NaN }, { float.NegativeInfinity }, { float.MinValue }, { -1.0f }, { 0.0f }, { 1.0f }, { float.MaxValue }, { float.PositiveInfinity }, { float.NaN } },
                                                                   new bool[] { false, false, true, true, true, true, true, false, false }, 4);
            GenerateComponentWiseTest(str, "isfinite", new double[,] { { -float.NaN }, { double.NegativeInfinity }, { double.MinValue }, { -1.0 }, { 0.0 }, { 1.0 }, { double.MaxValue }, { double.PositiveInfinity }, { double.NaN } },
                                                                   new bool[] { false, false, true, true, true, true, true, false, false }, 4);
            GenerateComponentWiseTest(str, "isinf", new float[,] { { -float.NaN }, { float.NegativeInfinity }, { float.MinValue }, { -1.0f }, { 0.0f }, { 1.0f }, { float.MaxValue }, { float.PositiveInfinity }, { float.NaN } },
                                                                   new bool[] { false, true, false, false, false, false, false, true, false }, 4);
            GenerateComponentWiseTest(str, "isinf", new double[,] { { -double.NaN }, { double.NegativeInfinity }, { double.MinValue }, { -1.0 }, { 0.0 }, { 1.0 }, { double.MaxValue }, { double.PositiveInfinity }, { double.NaN } },
                                                                   new bool[] { false, true, false, false, false, false, false, true, false }, 4);
            GenerateComponentWiseTest(str, "isnan", new float[,] { { -float.NaN }, { float.NegativeInfinity }, { float.MinValue }, { -1.0f }, { 0.0f }, { 1.0f }, { float.MaxValue }, { float.PositiveInfinity }, { float.NaN } },
                                                                   new bool[] { true, false, false, false, false, false, false, false, true }, 4);
            GenerateComponentWiseTest(str, "isnan", new double[,] { { -double.NaN }, { double.NegativeInfinity }, { double.MinValue }, { -1.0 }, { 0.0 }, { 1.0 }, { double.MaxValue }, { double.PositiveInfinity }, { double.NaN } },
                                                                   new bool[] { true, false, false, false, false, false, false, false, true }, 4);

            GenerateComponentWiseTestFloatAndDouble(str, "sin", new double[,] { { -1000000.0 }, { -1.2 }, { 0.0 }, { 1.2 }, { 1000000.0 }, { double.NegativeInfinity }, { double.NaN }, { double.PositiveInfinity } },
                                                                new double[] { 0.34999350217129295, -0.93203908596722635, 0.0, 0.93203908596722635, -0.34999350217129295, double.NaN, double.NaN, double.NaN }, 1, 32);

            GenerateComponentWiseTestFloatAndDouble(str, "cos", new double[,] { { -1000000.0 }, { -1.2 }, { 0.0 }, { 1.2 }, { 1000000.0 },  { double.NegativeInfinity }, { double.NaN }, { double.PositiveInfinity } },
                                                                new double[] { 0.93675212753314479,  0.36235775447667358, 1.0, 0.36235775447667358,  0.93675212753314479, double.NaN, double.NaN, double.NaN }, 8, 32);

            GenerateComponentWiseTestFloatAndDouble(str, "tan", new double[,] { { -1000000.0 }, { -1.2 }, { 0.0 }, { 1.2 }, { 1000000.0 }, { double.NegativeInfinity }, { double.NaN }, { double.PositiveInfinity } },
                                                                new double[] { 0.373624453987599, -2.57215162212632, 0.0, 2.57215162212632, -0.373624453987599, double.NaN, double.NaN, double.NaN }, 1, 32);

            GenerateComponentWiseTestFloatAndDouble(str, "atan",new double[,] { { -1000000.0 }, { -1.2 }, { 0.0 }, { 1.2 }, { 1000000.0 }, { double.NegativeInfinity }, { double.NaN }, { double.PositiveInfinity } },
                                                                new double[] { -1.570795326794897, -0.8760580505981934, 0.0, 0.8760580505981934, 1.570795326794897, -1.570796326794897, double.NaN, 1.570796326794897 }, 1, 32);

            GenerateComponentWiseTestFloatAndDouble(str, "atan2", new double[,] { { 3.1, 2.4 }, { 3.1, -2.4 }, { -3.1, 2.4 }, { -3.1, -2.4 }, { 0.0, 0.0 },
                                                                { 1.0, double.NegativeInfinity },   { 1.0, double.PositiveInfinity }, { double.NegativeInfinity, 1.0 },   { double.PositiveInfinity, 1.0 },
                                                                // { double.NegativeInfinity, double.PositiveInfinity }, // TODO: fails with burst
                                                                { 1.0, double.NaN }, { double.NaN, 1.0 }, { double.NaN, double.NaN },
                                                                },
                                                                new double[] { 0.91199029067742038, 2.2296023629123729, -0.91199029067742038, -2.2296023629123729, 0.0f,
                                                                Math.PI, 0.0, -Math.PI*0.5, Math.PI*0.5,
                                                                // double.NaN, // TODO: fails with burst
                                                                double.NaN, double.NaN, double.NaN,
                                                                }, 1, 32);


            GenerateComponentWiseTestFloatAndDouble(str, "sinh",new double[,] { { -2.0 }, { -1.2 }, { 0.0 }, { 1.2 }, { 2.0 }, { double.NegativeInfinity }, { double.NaN }, { double.PositiveInfinity } },
                                                                new double[] { -3.626860407847018, -1.509461355412173, 0.0, 1.509461355412173, 3.626860407847018, double.NegativeInfinity, double.NaN, double.PositiveInfinity }, 1, 32);

            GenerateComponentWiseTestFloatAndDouble(str, "cosh",new double[,] { { -2.0 }, { -1.2 }, { 0.0 }, { 1.2 }, { 2.0 }, { double.NegativeInfinity }, { double.NaN }, { double.PositiveInfinity } },
                                                                new double[] { 3.7621956910836314, 1.81065556732437, 1.0, 1.81065556732437, 3.7621956910836314, double.PositiveInfinity, double.NaN, double.PositiveInfinity }, 1, 32);

            GenerateComponentWiseTestFloatAndDouble(str, "tanh",new double[,] { { -2.0 }, { -1.2 }, { 0.0 }, { 1.2 }, { 2.0 }, { double.NegativeInfinity }, { double.NaN }, { double.PositiveInfinity } },
                                                                new double[] { -0.96402758007581688, -0.83365460701215526, 0.0, 0.83365460701215526, 0.96402758007581688, -1.0, double.NaN, 1.0 }, 1, 32);


            GenerateComponentWiseTestFloatAndDouble(str, "exp", new double[,] { { -10.0 }, { -1.2 }, { 0.0 }, { 1.2 }, { double.NegativeInfinity }, { double.NaN }, { double.PositiveInfinity } },
                                                                new double[] { 0.00004539992976248485, 0.3011942119122021, 1.0, 3.3201169227365475, 0.0, double.NaN, double.PositiveInfinity }, 1, 32);

            GenerateComponentWiseTestFloatAndDouble(str, "exp2",new double[,] { { -10.0 }, { -1.2 }, { 0.0 }, { 1.2 }, { double.NegativeInfinity }, { double.NaN }, { double.PositiveInfinity } },
                                                                new double[] { 0.0009765625, 0.435275281648062, 1.0, 2.297396709994070, 0.0, double.NaN, double.PositiveInfinity }, 1, 32);

            GenerateComponentWiseTestFloatAndDouble(str, "exp10",new double[,] { { -10.0 }, { -1.2 }, { 0.0 }, { 1.2 }, { double.NegativeInfinity }, { double.NaN }, { double.PositiveInfinity } },
                                                                new double[] { 1e-10, 0.063095734448019325, 1.0, 15.8489319246111348520210, 0.0, double.NaN, double.PositiveInfinity }, 32, 32);


            GenerateComponentWiseTestFloatAndDouble(str, "log", new double[,] { { 1.2e-9 }, { 1.0 }, { 1.2e10 }, { -1.0 }, { double.NegativeInfinity }, { double.NaN }, { double.PositiveInfinity } },
                                                                new double[] { -20.540944280152457, 0.0, 23.20817248673441, double.NaN, double.NaN, double.NaN, double.PositiveInfinity }, 1, 32);

            GenerateComponentWiseTestFloatAndDouble(str, "log2", new double[,] { { 1.2e-9 }, { 1.0 }, { 1.2e10 }, { -1.0 }, { double.NegativeInfinity }, { double.NaN }, { double.PositiveInfinity } },
                                                                new double[] { -29.634318448152467, 0.0, 33.48231535470742, double.NaN, double.NaN, double.NaN, double.PositiveInfinity }, 1, 32);

            GenerateComponentWiseTestFloatAndDouble(str, "log10", new double[,] { { 1.2e-9 }, { 1.0 }, { 1.2e10 }, { -1.0 }, { double.NegativeInfinity }, { double.NaN }, { double.PositiveInfinity } },
                                                                new double[] { -8.92081875395237517, 0.0, 10.079181246047624, double.NaN, double.NaN, double.NaN, double.PositiveInfinity }, 1, 32);

            GenerateComponentWiseTestFloatAndDouble(str, "radians", new double[,] { { -123.45 }, { 0.0 }, { 123.45 }, { double.NegativeInfinity }, { double.NaN }, { double.PositiveInfinity } },
                                                                new double[] { -2.15460896158699986, 0.0, 2.15460896158699986, double.NegativeInfinity, double.NaN, double.PositiveInfinity }, 1, 1);

            GenerateComponentWiseTestFloatAndDouble(str, "degrees", new double[,] { { -123.45 }, { 0.0 }, { 123.45 }, { double.NegativeInfinity }, { double.NaN }, { double.PositiveInfinity } },
                                                                new double[] { -7073.1639808900125122, 0.0, 7073.1639808900125122, double.NegativeInfinity, double.NaN, double.PositiveInfinity}, 1, 1);

            GenerateComponentWiseTestFloatAndDouble(str, "sign",new double[,] { { -123.45 }, { -1e-20 }, { 0.0 }, { 1e-10 }, { 123.45 }, { double.NegativeInfinity }, { double.NaN }, { double.PositiveInfinity } },
                                                                new double[] { -1.0, -1.0, 0.0, 1.0, 1.0, -1.0, 0.0, 1.0 });

            GenerateComponentWiseTestFloatAndDouble(str, "sqrt", new double[,] { { -1.0}, { 0.0 }, { 1e-10 }, { 123.45 }, { double.NegativeInfinity }, { double.NaN }, { double.PositiveInfinity } },
                                                                new double[] { double.NaN, 0.0, 1e-5, 11.11080555135405, double.NaN, double.NaN, double.PositiveInfinity, }, 1, 1);

            GenerateComponentWiseTestFloatAndDouble(str, "rsqrt", new double[,] { { -1.0 }, { 0.0 }, { 1e10 }, { 123.45 }, { double.NegativeInfinity }, { double.NaN }, { double.PositiveInfinity } },
                                                                new double[] { double.NaN, double.PositiveInfinity, 1e-5, 0.0900024751020984295, double.NaN, double.NaN, 0.0, }, 1, 1);

            GenerateComponentWiseTestFloatAndDouble(str, "rcp", new double[,] { { -123.45 }, { 0.0 }, { 123.45 }, { double.NaN }, { double.PositiveInfinity } },
                                                                new double[] { -0.0081004455245038477, double.PositiveInfinity, 0.0081004455245038477, double.NaN, 0.0, }, 1, 1);

            GenerateComponentWiseTestFloatAndDouble(str, "floor", new double[,] { { double.NegativeInfinity },  { -100.51 }, { -100.5 }, { -100.49 }, { 0.0 }, { 100.49 }, { 100.50 }, { 100.51 }, { double.PositiveInfinity }, { double.NaN } },
                                                                new double[] { double.NegativeInfinity, -101.0, -101.0, -101.0, 0.0, 100.0, 100.0, 100.0, double.PositiveInfinity, double.NaN } );

            GenerateComponentWiseTestFloatAndDouble(str, "ceil", new double[,] { { double.NegativeInfinity }, { -100.51 }, { -100.5 }, { -100.49 }, { 0.0 }, { 100.49 }, { 100.50 }, { 100.51 }, { double.PositiveInfinity }, { double.NaN } },
                                                                new double[] { double.NegativeInfinity, -100.0, -100.0, -100.0, 0.0, 101.0, 101.0, 101.0, double.PositiveInfinity, double.NaN });

            GenerateComponentWiseTestFloatAndDouble(str, "round", new double[,] { { double.NegativeInfinity }, { -100.51 }, { -100.5 }, { -100.49 }, { 0.0 }, { 100.49 }, { 100.50 }, { 100.51 }, { 101.50 }, { double.PositiveInfinity }, { double.NaN } },
                                                                new double[] { double.NegativeInfinity, -101.0, -100.0, -100.0, 0.0, 100.0, 100.0, 101.0, 102.0, double.PositiveInfinity, double.NaN });

            GenerateComponentWiseTestFloatAndDouble(str, "trunc", new double[,] { { double.NegativeInfinity }, { -100.51 }, { -100.5 }, { -100.49 }, { 0.0 }, { 100.49 }, { 100.50 }, { 100.51 }, { 101.50 }, { double.PositiveInfinity }, { double.NaN } },
                                                                new double[] { double.NegativeInfinity, -100.0, -100.0, -100.0, 0.0, 100.0, 100.0, 100.0, 101.0, double.PositiveInfinity, double.NaN });

            GenerateComponentWiseTestFloatAndDouble(str, "frac", new double[,] { { double.NegativeInfinity }, { -1e20 }, { -100.3 }, { 0.0 }, { 100.8 }, { double.PositiveInfinity }, { double.NaN } },
                                                                new double[] { double.NaN, 0.0, 0.7, 0.0, 0.8, double.NaN, double.NaN }, 64, 64);

            GenerateComponentWiseTestFloatAndDouble(str, "lerp", new double[,] { { -123.45, 439.43, -1.5 }, { -123.45, 439.43, 0.5 }, { -123.45, 439.43, 5.5 }, { -123.45, 439.43, double.NaN } },
                                                                new double[] { -967.77, 157.99, 2972.39, double.NaN }, 1, 1);

            GenerateComponentWiseTestFloatAndDouble(str, "unlerp", new double[,] {  { -123.45, 439.43, -254.3}, { -123.45, 439.43, 0.0 }, { -123.45, 439.43, 632.1 },
                                                                                    { 123.4, 123.4, -430.0 }, { 123.4, 123.4, 430.0 },
                                                                                    { 439.43, -123.45, -254.3}, { 439.43, -123.45, 0.0 }, { 439.43, -123.45, 632.1 } },
                                                                new double[] { -0.23246517907902217, 0.21931850483229107, 1.3422932063672541,
                                                                                double.NegativeInfinity, double.PositiveInfinity,
                                                                                1.2324651790790221, 0.78068149516770893, -0.34229320636725412}, 4, 4);

            GenerateComponentWiseTestFloatAndDouble(str, "remap", new double[,] { { -123.45, 439.43, 541.3, 631.5, -200.0 }, { -123.45, 439.43, 541.3, 631.5, -100.0 }, { -123.45, 439.43, 541.3, 631.5, 500.0 },
                                                                                  { 439.43, -123.45, 541.3, 631.5, -200.0 }, { 439.43, -123.45, 541.3, 631.5, -100.0 }, { 439.43, -123.45, 541.3, 631.5, 500.0 },
                                                                                  { -123.45, 439.43, 631.5, 541.3, -200.0 }, { -123.45, 439.43, 631.5, 541.3,-100.0 }, { -123.45,  439.43, 631.5, 541.3, 500.0 },
                                                                                  { -123.45,-123.45, 541.3, 631.5, -200.0 }, { -123.45,-123.45, 541.3, 631.5, -100.0 }, },
                                                                new double[] {  529.03306921546333, 545.05779917566799, 641.20617893689596,
                                                                                643.76693078453667, 627.74220082433201, 531.59382106310404,
                                                                                643.76693078453667, 627.74220082433201, 531.59382106310404,
                                                                                double.NegativeInfinity, double.PositiveInfinity,
                                                                }, 4, 4);


            GenerateComponentWiseTest(str, "clamp", new int[,] {    { int.MinValue, -123, 439},
                                                                    { -254, -123, 439}, { 246, -123, 439 }, { 632, -123, 439 },
                                                                    { -254,  439,-123}, { 246,  439,-123 }, { 632,  439,-123 },
                                                                    { int.MaxValue, -123, 439},
                                                                },
                                                    new int[] { -123, -123, 246, 439, 439, 439, 439, 439 }, 4);

            GenerateComponentWiseTest(str, "clamp", new uint[,] {   { 0, 123, 439},
                                                                    { 54, 123, 439}, { 246, 123, 439 }, { 632, 123, 439 },
                                                                    { 54, 439, 123}, { 246, 439, 123 }, { 632, 439, 123 },
                                                                    { uint.MaxValue, 123, 439},
                                                                },
                                                    new uint[] { 123, 123, 246, 439, 439, 439, 439, 439 }, 4);

            GenerateComponentWiseTest(str, "clamp", new long[,] {   { long.MinValue, -123, 439},
                                                                    { -254, -123, 439}, { 246, -123, 439 }, { 632, -123, 439 },
                                                                    { -254,  439,-123}, { 246,  439,-123 }, { 632,  439,-123 },
                                                                    { long.MaxValue, -123, 439},
                                                                },
                                                    new long[] { -123, -123, 246, 439, 439, 439, 439, 439 }, 1);

            GenerateComponentWiseTest(str, "clamp", new ulong[,] {  { 0, 123, 439},
                                                                    { 54, 123, 439}, { 246, 123, 439 }, { 632, 123, 439 },
                                                                    { 54, 439, 123}, { 246, 439, 123 }, { 632, 439, 123 },
                                                                    { ulong.MaxValue, 123, 439},
                                                                },
                                                    new ulong[] { 123, 123, 246, 439, 439, 439, 439, 439 }, 1);

            GenerateComponentWiseTestFloatAndDouble(str, "clamp", new double[,] {   { double.NegativeInfinity, -123.45, 439.43},
                                                                                    { -254.3, -123.45, 439.43}, { 246.3, -123.45, 439.43 }, { 632.1, -123.45, 439.43 },
                                                                                    { -254.3,  439.43,-123.45}, { 246.3,  439.43,-123.45 }, { 632.1,  439.43,-123.45 },
                                                                                    { double.PositiveInfinity, -123.45, 439.43},
                                                                                    { double.NaN, -123.45, 439.43},
                                                                                    },
                                                    new double[] { -123.45, -123.45, 246.3, 439.43, 439.43, 439.43, 439.43, 439.43, 439.43 });

            GenerateComponentWiseTestFloatAndDouble(str, "saturate", new double[,] { { double.NegativeInfinity }, { -123.45 }, { 0.0 }, { 0.5 }, { 1.0 }, { 123.45 }, { double.PositiveInfinity }, { double.NaN } },
                                                    new double[] { 0.0, 0.0, 0.0, 0.5, 1.0, 1.0, 1.0, 1.0 });

            GenerateComponentWiseTestFloatAndDouble(str, "step", new double[,] { { -123.45, double.NegativeInfinity }, { -123.45, -200.0 }, { -123.45, 200.0 }, { -123.45, double.PositiveInfinity }, { -123.45, double.NaN },
                                                                                 { 123.45, double.NegativeInfinity }, { 123.45, -200.0 }, { 123.45, 200.0 }, { 123.45, double.PositiveInfinity }, { 123.45, double.NaN },
                                                                                 { double.NegativeInfinity, double.NegativeInfinity }, { double.NegativeInfinity, -200.0 }, { double.NegativeInfinity, 200.0 }, { double.NegativeInfinity, double.PositiveInfinity }, { double.NegativeInfinity, double.NaN },
                                                                                 { double.PositiveInfinity, double.NegativeInfinity }, { double.PositiveInfinity, -200.0 }, { double.PositiveInfinity, 200.0 }, { double.PositiveInfinity, double.PositiveInfinity }, { double.PositiveInfinity, double.NaN },
                                                                                 { double.NaN, double.NegativeInfinity }, { double.NaN, -200.0 }, { double.NaN, 200.0 }, { double.NaN, double.PositiveInfinity }, { double.NaN, double.NaN }},
                                                                 new double[] {  0.0, 0.0, 1.0, 1.0, 0.0,
                                                                                 0.0, 0.0, 1.0, 1.0, 0.0,
                                                                                 1.0, 1.0, 1.0, 1.0, 0.0,
                                                                                 0.0, 0.0, 0.0, 1.0, 0.0,
                                                                                 0.0, 0.0, 0.0, 0.0, 0.0});


            GenerateComponentWiseTest(str, "min", new int[,] { { int.MinValue, int.MinValue }, { int.MinValue, -1 }, { -1, int.MinValue },
                                                                                { -1234, -3456 }, { -3456, -1234 }, { -1234, 3456 }, { 3456, -1234 }, { 1234, 3456 }, { 3456, 1234 },
                                                                                { 1, int.MaxValue}, { int.MaxValue, 1 }, { int.MaxValue, int.MinValue }, { int.MaxValue, int.MaxValue} },
                                                  new int[] { int.MinValue, int.MinValue, int.MinValue,
                                                                -3456, -3456, -1234, -1234, 1234, 1234,
                                                                1, 1, int.MinValue, int.MaxValue}, 4);

            GenerateComponentWiseTest(str, "min", new uint[,] { { 1234u, 3456u }, { 3456u, 1234u },
                                                                { 0xFFFFFF00u, 7u}, { 7u, 0xFFFFFF00u},
                                                                { 1u, uint.MaxValue}, { uint.MaxValue, 1u }, { uint.MaxValue, uint.MaxValue} },
                                                  new uint[] { 1234u, 1234u, 7u, 7u, 1u, 1u, uint.MaxValue}, 4);

            GenerateComponentWiseTest(str, "min", new long[,] { { long.MinValue, long.MinValue }, { long.MinValue, -1 }, { -1, long.MinValue },
                                                                                { -1234, -3456 }, { -3456, -1234 }, { -1234, 3456 }, { 3456, -1234 }, { 1234, 3456 }, { 3456, 1234 },
                                                                                { 1, long.MaxValue}, { long.MaxValue, 1 }, { long.MaxValue, long.MinValue }, { long.MaxValue, long.MaxValue} },
                                                  new long[] { long.MinValue, long.MinValue, long.MinValue,
                                                                -3456, -3456, -1234, -1234, 1234, 1234,
                                                                1, 1, long.MinValue, long.MaxValue}, 1);

            GenerateComponentWiseTest(str, "min", new ulong[,] { { 1234u, 3456u }, { 3456u, 1234u },
                                                                { 0xFFFFFFFFFFFFFF00ul, 7u}, { 7u, 0xFFFFFFFFFFFFFF00ul},
                                                                { 1u, ulong.MaxValue}, { ulong.MaxValue, 1u }, { ulong.MaxValue, ulong.MaxValue} },
                                                  new ulong[] { 1234u, 1234u, 7u, 7u, 1u, 1u, ulong.MaxValue }, 1);


            GenerateComponentWiseTestFloatAndDouble(str, "min", new double[,] { { double.NegativeInfinity, double.NegativeInfinity }, { double.NegativeInfinity, -1.0 }, { -1.0, double.NegativeInfinity },
                                                                                { -1234.56, -3456.7 }, { -3456.7, -1234.56 }, { -1234.56, 3456.7 }, { 3456.7, -1234.56 }, { 1234.56, 3456.7 }, { 3456.7, 1234.56 },
                                                                                { 1.0, double.PositiveInfinity}, { double.PositiveInfinity, 1.0 }, { double.PositiveInfinity, double.NegativeInfinity }, { double.PositiveInfinity, double.PositiveInfinity },
                                                                                { double.NaN, 2.3 },    { 2.3, double.NaN }, { double.NaN, double.NaN } },
                                                                new double[] { double.NegativeInfinity, double.NegativeInfinity, double.NegativeInfinity,
                                                                                -3456.7, -3456.7, -1234.56, -1234.56, 1234.56, 1234.56,
                                                                                1.0, 1.0, double.NegativeInfinity, double.PositiveInfinity,
                                                                                2.3, 2.3, double.NaN});



            GenerateComponentWiseTest(str, "max", new int[,] { { int.MinValue, int.MinValue }, { int.MinValue, -1 }, { -1, int.MinValue },
                                                                                { -1234, -3456 }, { -3456, -1234 }, { -1234, 3456 }, { 3456, -1234 }, { 1234, 3456 }, { 3456, 1234 },
                                                                                { 1, int.MaxValue}, { int.MaxValue, 1 }, { int.MaxValue, int.MinValue }, { int.MaxValue, int.MaxValue} },
                                                  new int[] { int.MinValue, -1, -1,
                                                                -1234, -1234, 3456, 3456, 3456, 3456,
                                                                int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue}, 4);

            GenerateComponentWiseTest(str, "max", new uint[,] { { 1234u, 3456u }, { 3456u, 1234u },
                                                                { 0xFFFFFF00u, 7u}, { 7u, 0xFFFFFF00u},
                                                                { 1u, uint.MaxValue}, { uint.MaxValue, 1u }, { uint.MaxValue, uint.MaxValue} },
                                                  new uint[] { 3456u, 3456u, 0xFFFFFF00u, 0xFFFFFF00u, uint.MaxValue, uint.MaxValue, uint.MaxValue }, 4);

            GenerateComponentWiseTest(str, "max", new long[,] { { long.MinValue, long.MinValue }, { long.MinValue, -1 }, { -1, long.MinValue },
                                                                                { -1234, -3456 }, { -3456, -1234 }, { -1234, 3456 }, { 3456, -1234 }, { 1234, 3456 }, { 3456, 1234 },
                                                                                { 1, long.MaxValue}, { long.MaxValue, 1 }, { long.MaxValue, long.MinValue }, { long.MaxValue, long.MaxValue} },
                                                  new long[] { long.MinValue, -1, -1,
                                                                -1234, -1234, 3456, 3456, 3456, 3456,
                                                                long.MaxValue, long.MaxValue, long.MaxValue, long.MaxValue}, 1);

            GenerateComponentWiseTest(str, "max", new ulong[,] { { 1234u, 3456u }, { 3456u, 1234u },
                                                                { 0xFFFFFFFFFFFFFF00ul, 7u}, { 7u, 0xFFFFFFFFFFFFFF00ul},
                                                                { 1u, ulong.MaxValue}, { ulong.MaxValue, 1u }, { ulong.MaxValue, ulong.MaxValue} },
                                                  new ulong[] { 3456u, 3456u, 0xFFFFFFFFFFFFFF00ul, 0xFFFFFFFFFFFFFF00ul, ulong.MaxValue, ulong.MaxValue, ulong.MaxValue }, 1);

            GenerateComponentWiseTestFloatAndDouble(str, "max", new double[,] { { double.NegativeInfinity, double.NegativeInfinity }, { double.NegativeInfinity, -1.0 }, { -1.0, double.NegativeInfinity },
                                                                                { -1234.56, -3456.7 }, { -3456.7, -1234.56 }, { -1234.56, 3456.7 }, { 3456.7, -1234.56 }, { 1234.56, 3456.7 }, { 3456.7, 1234.56 },
                                                                                { 1.0, double.PositiveInfinity}, { double.PositiveInfinity, 1.0 }, { double.PositiveInfinity, double.NegativeInfinity }, { double.PositiveInfinity, double.PositiveInfinity },
                                                                                { double.NaN, 2.3 },    { 2.3, double.NaN }, { double.NaN, double.NaN } },
                                                                new double[] { double.NegativeInfinity, -1.0, -1.0,
                                                                                -1234.56, -1234.56, 3456.7, 3456.7, 3456.7, 3456.7,
                                                                                double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity,
                                                                                2.3, 2.3, double.NaN});


            GenerateComponentWiseTestFloatAndDouble(str, "smoothstep", new double[,] { { -123.45, 345.6, double.NegativeInfinity}, { -123.45, 345.6, -200.0}, { -123.45, 345.6, -100.0}, { -123.45, 345.6, 400.0}, { -123.45, 345.6, double.PositiveInfinity}, { -123.45, 345.6, double.NaN },
                                                                                        { 345.6, -123.45, double.NegativeInfinity}, { 345.6, -123.45, -200.0}, { 345.6, -123.45, -100.0}, { 345.6, -123.45, 400.0}, { 345.6, -123.45, double.PositiveInfinity}, { 345.6, -123.45, double.NaN }},
                                                                       new double[] { 0.0, 0.0, 0.0072484810488798993, 1.0, 1.0, 1.0,
                                                                                      1.0, 1.0, 0.9927515189511201007, 0.0, 0.0, 1.0}, 8, 8);

            GenerateComponentWiseTest(str, "mad", new int[,] { { 1234, 5678, 91011 }, { 1234, 5678, -91011 }, { 1234, -5678, 91011 }, { 1234, -5678, -91011 },
                                                               {-1234, 5678, 91011 }, {-1234, 5678, -91011 }, {-1234, -5678, 91011 }, {-1234, -5678, -91011 },
                                                               { 98765, 56789, 91011 }, { 98765, 56789,-91011 }, { 98765,-56789, 91011 }, { 98765,-56789,-91011 },
                                                               {-98765, 56789, 91011 }, {-98765, 56789,-91011 }, {-98765,-56789, 91011 }, {-98765,-56789,-91011 }},
                                                  new int[] { 7097663,  6915641, -6915641, -7097663,
                                                             -6915641, -7097663,  7097663,  6915641,
                                                              1313889300, 1313707278, -1313707278, -1313889300,
                                                             -1313707278, -1313889300, 1313889300, 1313707278}, 4);


            GenerateComponentWiseTest(str, "mad", new uint[,] { { 1234u, 5678u, 91011u }, { 98765u, 56789u, 91011u } },
                                                  new uint[] { 7097663u, 1313889300u }, 4);


            GenerateComponentWiseTest(str, "mad", new long[,] { { 1234, 5678, 91011 }, { 1234, 5678, -91011 }, { 1234, -5678, 91011 }, { 1234, -5678, -91011 },
                                                               {-1234, 5678, 91011 }, {-1234, 5678, -91011 }, {-1234, -5678, 91011 }, {-1234, -5678, -91011 },
                                                               { 9876543210, 5678901234, 9101112134 }, { 9876543210, 5678901234,-9101112134 }, { 9876543210,-5678901234, 9101112134 }, { 9876543210,-5678901234,-9101112134 },
                                                               {-9876543210, 5678901234, 9101112134 }, {-9876543210, 5678901234,-9101112134 }, {-9876543210,-5678901234, 9101112134 }, {-9876543210,-5678901234,-9101112134 }},
                                                  new long[] { 7097663,  6915641, -6915641, -7097663,
                                                             -6915641, -7097663,  7097663,  6915641,
                                                              747681210895778426, 747681192693554158, -747681192693554158, -747681210895778426,
                                                              -747681192693554158, -747681210895778426, 747681210895778426, 747681192693554158}, 1);

            GenerateComponentWiseTest(str, "mad", new ulong[,] { { 1234, 5678, 91011 }, { 9876543210, 5678901234, 9101112134 }},
                                                  new long[] { 7097663, 747681210895778426 }, 1);


            GenerateComponentWiseTestFloatAndDouble(str, "mad",         new double[,] { { -123.45, 345.6, 4.321 }, { double.NaN, 345.6, 4.321 }, { -123.45, double.NaN, 4.321 }, { -123.45, 345.6, double.NaN } },
                                                                        new double[] { -42659.999, double.NaN, double.NaN, double.NaN }, 1, 1);


            GenerateComponentWiseTestFloatAndDouble(str, "fmod", new double[,] {    { double.NegativeInfinity, double.NegativeInfinity }, { -323.4, double.NegativeInfinity }, { -0.0, double.NegativeInfinity}, { 0.0, double.NegativeInfinity}, { 323.4, double.NegativeInfinity}, { double.PositiveInfinity, double.NegativeInfinity}, { double.NaN, double.NegativeInfinity},
                                                                                    { double.NegativeInfinity, -123.6}, { -323.4, -123.6}, { -0.0, -123.6}, { 0.0, -123.6}, { 323.4, -123.6}, { double.PositiveInfinity, -123.6}, { double.NaN, -123.6},
                                                                                    { double.NegativeInfinity, -0.0}, { -323.4, -0.0}, { -0.0, -0.0}, { 0.0, -0.0}, { 323.4, -0.0}, { double.PositiveInfinity, -0.0}, { double.NaN, -0.0},
                                                                                    { double.NegativeInfinity, 0.0}, { -323.4, 0.0}, { -0.0, 0.0}, { 0.0, 0.0}, { 323.4, 0.0}, { double.PositiveInfinity, 0.0}, { double.NaN, 0.0},
                                                                                    { double.NegativeInfinity, 123.6}, { -323.4, 123.6}, { -0.0, 123.6}, { 0.0, 123.6}, { 323.4, 123.6}, { double.PositiveInfinity, 123.6}, { double.NaN, 123.6},
                                                                                    { double.NegativeInfinity, double.PositiveInfinity}, { -323.4, double.PositiveInfinity}, { -0.0, double.PositiveInfinity}, { 0.0, double.PositiveInfinity}, { 323.4, double.PositiveInfinity}, { double.PositiveInfinity, double.PositiveInfinity}, { double.NaN, double.PositiveInfinity},
                                                                                    { double.NegativeInfinity, double.NaN}, { -323.4, double.NaN}, { -0.0, double.NaN}, { 0.0, double.NaN}, { 323.4, double.NaN}, { double.PositiveInfinity, double.NaN}, { double.NaN, double.NaN},
                                                                                    },
                                                                 new double[] {     double.NaN, -323.4, -0.0, 0.0, 323.4, double.NaN, double.NaN,
                                                                                    double.NaN, -76.2, -0.0, 0.0, 76.2, double.NaN, double.NaN,
                                                                                    double.NaN, double.NaN,double.NaN,double.NaN,double.NaN,double.NaN,double.NaN,
                                                                                    double.NaN, double.NaN,double.NaN,double.NaN,double.NaN,double.NaN,double.NaN,
                                                                                    double.NaN, -76.2, -0.0, 0.0, 76.2, double.NaN, double.NaN,
                                                                                    double.NaN, -323.4, -0.0, 0.0, 323.4, double.NaN, double.NaN,
                                                                                    double.NaN, double.NaN,double.NaN,double.NaN,double.NaN,double.NaN,double.NaN}, 1, 1);

            GenerateComponentWiseTestFloatAndDouble(str, "pow", new double[,] {     { double.NegativeInfinity, double.NegativeInfinity }, { -3.4, double.NegativeInfinity }, { -0.0, double.NegativeInfinity}, { 0.0, double.NegativeInfinity}, { 3.4, double.NegativeInfinity}, { double.PositiveInfinity, double.NegativeInfinity}, { double.NaN, double.NegativeInfinity},

                                                                                    { double.NegativeInfinity, -2.6}, { -3.4, -2.6}, { -0.0, -2.6}, { 0.0, -2.6}, { 3.4, -2.6}, { double.PositiveInfinity, -2.6}, { double.NaN, -2.6},

                                                                                    { double.NegativeInfinity, -0.0}, { -3.4, -0.0}, { -0.0, -0.0}, { 0.0, -0.0}, { 3.4, -0.0}, { double.PositiveInfinity, -0.0}, // { double.NaN, -0.0}, // TODO: fails with burst
                                                                                    { double.NegativeInfinity, 0.0}, { -3.4, 0.0}, { -0.0, 0.0}, { 0.0, 0.0}, { 3.4, 0.0}, { double.PositiveInfinity, 0.0}, // { double.NaN, 0.0}, // TODO: fails with burst

                                                                                    { double.NegativeInfinity, 2.6}, { -3.4, 2.6}, { -0.0, 2.6}, { 0.0, 2.6}, { 3.4, 2.6}, { double.PositiveInfinity, 2.6}, { double.NaN, 2.6},
                                                                                    { double.NegativeInfinity, double.PositiveInfinity}, { -3.4, double.PositiveInfinity}, { -0.0, double.PositiveInfinity}, { 0.0, double.PositiveInfinity}, { 3.4, double.PositiveInfinity}, { double.PositiveInfinity, double.PositiveInfinity}, { double.NaN, double.PositiveInfinity},
                                                                                    { double.NegativeInfinity, double.NaN}, { -3.4, double.NaN}, { -0.0, double.NaN}, { 0.0, double.NaN}, { 3.4, double.NaN}, { double.PositiveInfinity, double.NaN}, { double.NaN, double.NaN},

                                                                                    },
                                                                 new double[]       {
                                                                                      0.0, 0.0, double.PositiveInfinity, double.PositiveInfinity, 0.0, 0.0, double.NaN,
                                                                                      0.0, double.NaN, double.PositiveInfinity, double.PositiveInfinity, 0.041510199028461224, 0.0, double.NaN,
                                                                                      1.0, 1.0, 1.0, 1.0, 1.0, 1.0, // double.NaN, // TODO: fails with burst
                                                                                      1.0, 1.0, 1.0, 1.0, 1.0, 1.0, // double.NaN, // TODO: fails with burst

                                                                                      double.PositiveInfinity, double.NaN, 0.0, 0.0, 24.090465076169736, double.PositiveInfinity, double.NaN,

                                                                                      double.PositiveInfinity, double.PositiveInfinity, 0.0, 0.0, double.PositiveInfinity, double.PositiveInfinity, double.NaN,
                                                                                      double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN,
                                                                                    }, 1, 1);


            GenerateComponentWiseTest(str, "ceilpow2", new int[,] { { 0 }, { 1 }, { 2 }, { 3 }, { 1019642234 }, { 1823423423 }, { -2147483647 } },
                                                       new int[] { 0, 1, 2, 4, 1073741824, -2147483648, 0 }, 4);

            GenerateComponentWiseTest(str, "ceilpow2", new uint[,] { { 0u }, { 1u }, { 2u }, { 3u }, { 1019642234u }, { 1823423423u }, { 4294967295u } },
                                                       new uint[] { 0u, 1u, 2u, 4u, 1073741824u, 2147483648u, 0u }, 4);


            GenerateComponentWiseTest(str, "ceilpow2", new long[,] { { 0L }, { 1L }, { 2L }, { 3L }, { 1019642234L }, { 1823423423L }, { 2147483648L }, { 4294967295L }, { 4294967296L }, { 7227372236554874814L }, { -100L } },
                                                       new long[] { 0L, 1L, 2L, 4L, 1073741824L, 2147483648L, 2147483648L, 4294967296L, 4294967296L, -9223372036854775808L, 0L }, 1);


            GenerateComponentWiseTest(str, "ceilpow2", new ulong[,] { { 0UL }, { 1UL }, { 2UL }, { 3UL }, { 1019642234UL }, { 1823423423UL }, { 2147483648UL }, { 4294967295UL }, { 4294967296UL }, { 7227372236554874814UL }, { 10223372036854775808UL } },
                                                       new ulong[] { 0UL, 1UL, 2UL, 4UL, 1073741824UL, 2147483648UL, 2147483648UL, 4294967296UL, 4294967296UL, 9223372036854775808UL, 0L }, 1);
            */
            GenerateFpComponentWiseTests(str);
            // GenerateFpNotImplementedCases(str);
            str.Append("#endif\n");
            str.Append("\n\t}");
            str.Append("\n}\n");
        }

        private void GenerateTypeTests(StringBuilder str)
        {
            str.Append("using NUnit.Framework;\n");
            str.Append("using static Unity.Mathematics.FixedPoint.MathFp;\n");
            // str.Append("using static Unity.Mathematics.math;\n\n");
            str.Append("namespace Unity.Mathematics.FixedPoint.Tests\n");
            str.Append("{\n");
            str.Append("\t[TestFixture]\n");
            str.AppendFormat("\tpublic class Test{0}\n", UpperCaseFirstLetter(_mTypeName));
            str.Append("\t{\n");
            // ** 屏蔽所有 decimal 相关的测试，因为 burst 不支持这个类型
            if (_mBaseType == "Fp")
            {
                str.Append($"#if ENABLE_DECIMAL\n");
            }

            TestStaticFields(str);
            TestConstructors(str);
            TestOperators(str);

            if (_mBaseType == "Fp")
            {
                str.Append($"#endif\n");
            }
            str.Append("\n\t}");
            str.Append("\n}\n");
        }


        private void GenerateComponentWiseTestFp(StringBuilder str, string functionName, decimal[,] input,
            decimal[] output, long fpMaxEps = 0, bool signedZeroEqual = false)
        {
            if (input.GetLength(0) != output.GetLength(0))
                throw new Exception("Bad length for GenerateComponentWiseTestFp: " + functionName);

            GenerateComponentWiseTest(str, functionName, input, output, 4, fpMaxEps, signedZeroEqual);
        }

        private void GenerateFpComponentWiseTests(StringBuilder str)
        {
            int highPrecision = 1024;
            int mediumPrecision = 32000;
            int mediumLowPrecision = 128000;
            int lowPrecision = 200000000;
            long lowLowPrecision = 35359738368L;
            long veryLowPrecision = 37200899080192L;

            GenerateComponentWiseTestFp(str, "abs", new[,] { { 0.0m }, { -1.1m }, { 2.2m } },
                new[] { 0.0m, 1.1m, 2.2m });

            GenerateComponentWiseTestFp(str, "sin",
                new[,] { { -1000000.0m }, { -1.2m }, { 0.0m }, { 1.2m }, { 1000000.0m } },
                new[]
                    { 0.34999350217129295m, -0.93203908596722635m, 0.0m, 0.93203908596722635m, -0.34999350217129295m },
                lowPrecision);

            GenerateComponentWiseTestFp(str, "cos",
                new[,] { { -1000000.0m }, { -1.2m }, { 0.0m }, { 1.2m }, { 1000000.0m } },
                new[]
                    { 0.93675212753314479m, 0.36235775447667358m, 1.0m, 0.36235775447667358m, 0.93675212753314479m },
                lowPrecision);

            GenerateComponentWiseTestFp(str, "tan",
                new[,] { { -1000000.0m }, { -1.2m }, { 0.0m }, { 1.2m }, { 1000000.0m } },
                new[] { 0.373624453987599m, -2.57215162212632m, 0.0m, 2.57215162212632m, -0.373624453987599m },
                veryLowPrecision);

            GenerateComponentWiseTestFp(str, "atan",
                new[,] { { -1000000.0m }, { -1.2m }, { 0.0m }, { 1.2m }, { 1000000.0m } },
                new[]
                    { -1.570795326794897m, -0.8760580505981934m, 0.0m, 0.8760580505981934m, 1.570795326794897m },
                veryLowPrecision);

            GenerateComponentWiseTestFp(str, "atan2",
                new[,] { { 3.1m, 2.4m }, { 3.1m, -2.4m }, { -3.1m, 2.4m }, { -3.1m, -2.4m }, { 0.0m, 0.0m } },
                new[]
                {
                    0.91199029067742038m, 2.2296023629123729m, -0.91199029067742038m, -2.2296023629123729m, 0.0m
                }, veryLowPrecision);


            GenerateComponentWiseTestFp(str, "log", new[,] { { 1.2e-9m }, { 1.0m }, { 1.2e9m } },
                new[] { (decimal)Math.Log(1.2e-9), (decimal)Math.Log(1.0), (decimal)Math.Log(1.2e9), },
                veryLowPrecision);

            GenerateComponentWiseTestFp(str, "log2", new[,] { { 1.2e-9m }, { 1.0m }, { 1.2e9m } },
                new[] { (decimal)Math.Log(1.2e-9, 2), (decimal)Math.Log(1.0, 2), (decimal)Math.Log(1.2e9, 2), },
                veryLowPrecision);

            GenerateComponentWiseTestFp(str, "radians", new[,] { { -123.45m }, { 0.0m }, { 123.45m } },
                new[] { -2.15460896158699986m, 0.0m, 2.15460896158699986m }, lowPrecision);

            GenerateComponentWiseTestFp(str, "degrees", new[,] { { -123.45m }, { 0.0m }, { 123.45m } },
                new[] { -7073.1639808900125122m, 0.0m, 7073.1639808900125122m }, mediumPrecision);

            GenerateComponentWiseTestFp(str, "sign",
                new[,] { { -123.45m }, { -1e-9m }, { 0.0m }, { 1e-9m }, { 123.45m } },
                new[] { -1.0m, -1.0m, 0.0m, 1.0m, 1.0m });

            GenerateComponentWiseTestFp(str, "sqrt", new[,] { { 0.0m }, { 1e-3m }, { 123.45m } },
                new[] { 0.0m, (decimal)(Math.Sqrt(1e-3)), 11.11080555135405m }, lowPrecision);

            GenerateComponentWiseTestFp(str, "rsqrt", new[,] { { 1e9m }, { 123.45m } },
                new[] { (decimal)(1.0 / Math.Sqrt(1e9)), 0.0900024751020984295m }, lowLowPrecision);

            GenerateComponentWiseTestFp(str, "rcp", new[,] { { -123.45m }, { 123.45m } },
                new[] { -0.0081004455245038477m, 0.0081004455245038477m }, lowPrecision);

            GenerateComponentWiseTestFp(str, "floor",
                new[,]
                    { { -100.51m }, { -100.5m }, { -100.49m }, { 0.0m }, { 100.49m }, { 100.50m }, { 100.51m } },
                new[] { -101.0m, -101.0m, -101.0m, 0.0m, 100.0m, 100.0m, 100.0m });

            GenerateComponentWiseTestFp(str, "ceil",
                new[,]
                    { { -100.51m }, { -100.5m }, { -100.49m }, { 0.0m }, { 100.49m }, { 100.50m }, { 100.51m } },
                new[] { -100.0m, -100.0m, -100.0m, 0.0m, 101.0m, 101.0m, 101.0m });

            GenerateComponentWiseTestFp(str, "round",
                new[,]
                {
                    { -100.51m }, { -100.5m }, { -100.49m }, { 0.0m }, { 100.49m }, { 100.50m }, { 100.51m },
                    { 101.50m }
                },
                new[] { -101.0m, -100.0m, -100.0m, 0.0m, 100.0m, 100.0m, 101.0m, 102.0m });

            GenerateComponentWiseTestFp(str, "trunc",
                new[,]
                {
                    { -100.51m }, { -100.5m }, { -100.49m }, { 0.0m }, { 100.49m }, { 100.50m }, { 100.51m },
                    { 101.50m }
                },
                new[] { -100.0m, -100.0m, -100.0m, 0.0m, 100.0m, 100.0m, 100.0m, 101.0m });

            GenerateComponentWiseTestFp(str, "frac", new[,] { { -1e9m }, { -100.3m }, { 0.0m }, { 100.8m } },
                new[] { 0.0m, 0.7m, 0.0m, 0.8m }, lowPrecision);

            GenerateComponentWiseTestFp(str, "lerp",
                new[,]
                    { { -123.45m, 439.43m, -1.5m }, { -123.45m, 439.43m, 0.5m }, { -123.45m, 439.43m, 5.5m } },
                new[] { -967.77m, 157.99m, 2972.39m }, highPrecision);

            GenerateComponentWiseTestFp(str, "unlerp", new[,]
                {
                    { -123.45m, 439.43m, -254.3m }, { -123.45m, 439.43m, 0.0m }, { -123.45m, 439.43m, 632.1m },
                    { 439.43m, -123.45m, -254.3m }, { 439.43m, -123.45m, 0.0m }, { 439.43m, -123.45m, 632.1m }
                },
                new[]
                {
                    -0.23246517907902217m, 0.21931850483229107m, 1.3422932063672541m,
                    1.2324651790790221m, 0.78068149516770893m, -0.34229320636725412m
                }, lowPrecision);

            GenerateComponentWiseTestFp(str, "remap", new[,]
                {
                    { -123.45m, 439.43m, 541.3m, 631.5m, -200.0m }, { -123.45m, 439.43m, 541.3m, 631.5m, -100.0m },
                    { -123.45m, 439.43m, 541.3m, 631.5m, 500.0m },
                    { 439.43m, -123.45m, 541.3m, 631.5m, -200.0m }, { 439.43m, -123.45m, 541.3m, 631.5m, -100.0m },
                    { 439.43m, -123.45m, 541.3m, 631.5m, 500.0m },
                    { -123.45m, 439.43m, 631.5m, 541.3m, -200.0m }, { -123.45m, 439.43m, 631.5m, 541.3m, -100.0m },
                    { -123.45m, 439.43m, 631.5m, 541.3m, 500.0m },
                },
                new[]
                {
                    529.03306921546333m, 545.05779917566799m, 641.20617893689596m,
                    643.76693078453667m, 627.74220082433201m, 531.59382106310404m,
                    643.76693078453667m, 627.74220082433201m, 531.59382106310404m,
                }, mediumLowPrecision);

            GenerateComponentWiseTestFp(str, "clamp", new[,]
                {
                    { -254.3m, -123.45m, 439.43m }, { 246.3m, -123.45m, 439.43m }, { 632.1m, -123.45m, 439.43m },
                    { -254.3m, 439.43m, -123.45m }, { 246.3m, 439.43m, -123.45m }, { 632.1m, 439.43m, -123.45m }
                },
                new[] { -123.45m, 246.3m, 439.43m, 439.43m, 439.43m, 439.43m });

            GenerateComponentWiseTestFp(str, "saturate", new[,] { { -123.45m }, { 0.0m }, { 0.5m }, { 1.0m } },
                new[] { 0.0m, 0.0m, 0.5m, 1.0m });

            GenerateComponentWiseTestFp(str, "step", new[,]
                {
                    { -123.45m, -200.0m }, { -123.45m, 200.0m },
                    { 123.45m, -200.0m }, { 123.45m, 200.0m }
                },
                new[]
                {
                    0.0m, 1.0m,
                    0.0m, 1.0m,
                });


            GenerateComponentWiseTestFp(str, "min", new[,]
                {
                    { -1234.56m, -3456.7m }, { -3456.7m, -1234.56m }, { -1234.56m, 3456.7m }, { 3456.7m, -1234.56m },
                    { 1234.56m, 3456.7m }, { 3456.7m, 1234.56m },
                },
                new[]
                {
                    -3456.7m, -3456.7m, -1234.56m, -1234.56m, 1234.56m, 1234.56m,
                });

            GenerateComponentWiseTestFp(str, "max", new[,]
                {
                    { -1234.56m, -3456.7m }, { -3456.7m, -1234.56m }, { -1234.56m, 3456.7m }, { 3456.7m, -1234.56m },
                    { 1234.56m, 3456.7m }, { 3456.7m, 1234.56m }
                },
                new[]
                {
                    -1234.56m, -1234.56m, 3456.7m, 3456.7m, 3456.7m, 3456.7m,
                });


            GenerateComponentWiseTestFp(str, "smoothStep", new[,]
                {
                    { -123.45m, 345.6m, -200.0m }, { -123.45m, 345.6m, -100.0m }, { -123.45m, 345.6m, 400.0m },
                    { 345.6m, -123.45m, -200.0m }, { 345.6m, -123.45m, -100.0m }, { 345.6m, -123.45m, 400.0m }
                },
                new[]
                {
                    0.0m, 0.0072484810488798993m, 1.0m,
                    1.0m, 0.9927515189511201007m, 0.0m
                }, lowLowPrecision);

            GenerateComponentWiseTestFp(str, "mad", new[,] { { -123.45m, 345.6m, 4.321m } },
                new[] { -42659.999m }, mediumPrecision);


            GenerateComponentWiseTestFp(str, "fmod", new[,]
                {
                    { -323.4m, -123.6m }, { -0.0m, -123.6m }, { 0.0m, -123.6m }, { 323.4m, -123.6m },
                    { -323.4m, 123.6m }, { -0.0m, 123.6m }, { 0.0m, 123.6m }, { 323.4m, 123.6m }
                },
                new[]
                {
                    -76.2m, -0.0m, 0.0m, 76.2m,
                    -76.2m, -0.0m, 0.0m, 76.2m
                }, mediumPrecision);

            GenerateComponentWiseTestFp(str, "pow", new[,]
                {
                    { 3.4m, -2.6m },
                    { -3.4m, -0.0m }, { -0.0m, -0.0m }, { 0.0m, -0.0m }, { 3.4m, -0.0m },
                    { -3.4m, 0.0m }, { -0.0m, 0.0m }, { 0.0m, 0.0m }, { 3.4m, 0.0m },
                    { -0.0m, 2.6m }, { 0.0m, 2.6m }, { 3.4m, 2.6m }
                },
                new[]
                {
                    0.041510199028461224m,
                    1.0m, 1.0m, 1.0m, 1.0m,
                    1.0m, 1.0m, 1.0m, 1.0m,
                    0.0m, 0.0m, 24.090465076169736m
                }, lowPrecision);
        }

        void GenerateFpNotImplementedCases(StringBuilder str)
        {
            int lowPrecision = 200000000;

            GenerateComponentWiseTestFp(str, "sinh",
                new[,] { { -2.0m }, { -1.2m }, { 0.0m }, { 1.2m }, { 2.0m } },
                new[]
                    { -3.626860407847018m, -1.509461355412173m, 0.0m, 1.509461355412173m, 3.626860407847018m },
                lowPrecision);

            GenerateComponentWiseTestFp(str, "cosh",
                new[,] { { -2.0m }, { -1.2m }, { 0.0m }, { 1.2m }, { 2.0m } },
                new[] { 3.7621956910836314m, 1.81065556732437m, 1.0m, 1.81065556732437m, 3.7621956910836314m },
                lowPrecision);

            GenerateComponentWiseTestFp(str, "tanh",
                new[,] { { -2.0m }, { -1.2m }, { 0.0m }, { 1.2m }, { 2.0m } },
                new[]
                    { -0.96402758007581688m, -0.83365460701215526m, 0.0m, 0.83365460701215526m, 0.96402758007581688m },
                lowPrecision);


            GenerateComponentWiseTestFp(str, "exp", new[,] { { -10.0m }, { -1.2m }, { 0.0m }, { 1.2m } },
                new[]
                    { 0.00004539992976248485m, 0.3011942119122021m, 1.0m, 3.3201169227365475m }, lowPrecision);

            GenerateComponentWiseTestFp(str, "exp2", new[,] { { -10.0m }, { -1.2m }, { 0.0m }, { 1.2m } },
                new[] { 0.0009765625m, 0.435275281648062m, 1.0m, 2.297396709994070m }, lowPrecision);

            GenerateComponentWiseTestFp(str, "exp10", new[,] { { -10.0m }, { -1.2m }, { 0.0m }, { 1.2m } },
                new[] { 1e-10m, 0.063095734448019325m, 1.0m, 15.8489319246111348520210m }, lowPrecision);

            GenerateComponentWiseTestFp(str, "log10", new[,] { { 1.2e-9m }, { 1.0m }, { 1.2e9m } },
                new[] { (decimal)Math.Log10(1.2e-9), (decimal)Math.Log10(1.0), (decimal)Math.Log10(1.2e9), },
                lowPrecision);
        }
    }
}