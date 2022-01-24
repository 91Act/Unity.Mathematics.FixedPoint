using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Unity.Mathematics.FixedPoint
{
    public static partial class MathFp
    {
        /// <summary>Extrinsic rotation order. Specifies in which order rotations around the principal axes (x, y and z) are to be applied.</summary>
        public enum RotationOrder : byte
        {
            /// <summary>Extrinsic rotation around the x axis, then around the y axis and finally around the z axis.</summary>
            XYZ,

            /// <summary>Extrinsic rotation around the x axis, then around the z axis and finally around the y axis.</summary>
            XZY,

            /// <summary>Extrinsic rotation around the y axis, then around the x axis and finally around the z axis.</summary>
            YXZ,

            /// <summary>Extrinsic rotation around the y axis, then around the z axis and finally around the x axis.</summary>
            YZX,

            /// <summary>Extrinsic rotation around the z axis, then around the x axis and finally around the y axis.</summary>
            ZXY,

            /// <summary>Extrinsic rotation around the z axis, then around the y axis and finally around the x axis.</summary>
            ZYX,

            /// <summary>Unity default rotation order. Extrinsic Rotation around the z axis, then around the x axis and finally around the y axis.</summary>
            Default = ZXY
        };

        /// <summary>Specifies a shuffle component.</summary>
        public enum ShuffleComponent : byte
        {
            /// <summary>Specified the x component of the left vector.</summary>
            LeftX,

            /// <summary>Specified the y component of the left vector.</summary>
            LeftY,

            /// <summary>Specified the z component of the left vector.</summary>
            LeftZ,

            /// <summary>Specified the w component of the left vector.</summary>
            LeftW,

            /// <summary>Specified the x component of the right vector.</summary>
            RightX,

            /// <summary>Specified the y component of the right vector.</summary>
            RightY,

            /// <summary>Specified the z component of the right vector.</summary>
            RightZ,

            /// <summary>Specified the w component of the right vector.</summary>
            RightW
        };

        const string NotSupportedYet = "Not supported yet.";

        /// <summary>Returns the bit pattern of a uint as an int.</summary>
        /// <param name="x">The uint bits to copy.</param>
        /// <returns>The int with the same bit pattern as the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int asint(uint x)
        {
            return (int)x;
        }

        /// <summary>Returns the bit pattern of a uint2 as an int2.</summary>
        /// <param name="x">The uint2 bits to copy.</param>
        /// <returns>The int2 with the same bit pattern as the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int2 asint(uint2 x)
        {
            return int2((int)x.x, (int)x.y);
        }

        /// <summary>Returns the bit pattern of a uint3 as an int3.</summary>
        /// <param name="x">The uint3 bits to copy.</param>
        /// <returns>The int3 with the same bit pattern as the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int3 asint(uint3 x)
        {
            return int3((int)x.x, (int)x.y, (int)x.z);
        }

        /// <summary>Returns the bit pattern of a uint4 as an int4.</summary>
        /// <param name="x">The uint4 bits to copy.</param>
        /// <returns>The int4 with the same bit pattern as the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int4 asint(uint4 x)
        {
            return int4((int)x.x, (int)x.y, (int)x.z, (int)x.w);
        }

        /// <summary>Returns the bit pattern of a Fp as an int.</summary>
        /// <param name="x">The Fp bits to copy.</param>
        /// <returns>The int with the same bit pattern as the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int asint(Fp x)
        {
            return (int)x;
        }

        /// <summary>Returns the bit pattern of a Fp2 as an int2.</summary>
        /// <param name="x">The Fp2 bits to copy.</param>
        /// <returns>The int2 with the same bit pattern as the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int2 asint(Fp2 x)
        {
            return int2(asint(x.x), asint(x.y));
        }

        /// <summary>Returns the bit pattern of a Fp3 as an int3.</summary>
        /// <param name="x">The Fp3 bits to copy.</param>
        /// <returns>The int3 with the same bit pattern as the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int3 asint(Fp3 x)
        {
            return int3(asint(x.x), asint(x.y), asint(x.z));
        }

        /// <summary>Returns the bit pattern of a Fp4 as an int4.</summary>
        /// <param name="x">The Fp4 bits to copy.</param>
        /// <returns>The int4 with the same bit pattern as the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int4 asint(Fp4 x)
        {
            return int4(asint(x.x), asint(x.y), asint(x.z), asint(x.w));
        }

        /// <summary>Returns the bit pattern of an int as a uint.</summary>
        /// <param name="x">The int bits to copy.</param>
        /// <returns>The uint with the same bit pattern as the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint asuint(int x)
        {
            return (uint)x;
        }

        /// <summary>Returns the bit pattern of an int2 as a uint2.</summary>
        /// <param name="x">The int2 bits to copy.</param>
        /// <returns>The uint2 with the same bit pattern as the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint2 asuint(int2 x)
        {
            return uint2((uint)x.x, (uint)x.y);
        }

        /// <summary>Returns the bit pattern of an int3 as a uint3.</summary>
        /// <param name="x">The int3 bits to copy.</param>
        /// <returns>The uint3 with the same bit pattern as the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint3 asuint(int3 x)
        {
            return uint3((uint)x.x, (uint)x.y, (uint)x.z);
        }

        /// <summary>Returns the bit pattern of an int4 as a uint4.</summary>
        /// <param name="x">The int4 bits to copy.</param>
        /// <returns>The uint4 with the same bit pattern as the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint4 asuint(int4 x)
        {
            return uint4((uint)x.x, (uint)x.y, (uint)x.z, (uint)x.w);
        }

        /// <summary>Returns the bit pattern of a Fp as a uint.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint asuint(Fp x)
        {
            return (uint)asint((uint)x);
        }

        /// <summary>Returns the bit pattern of a Fp2 as a uint2.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint2 asuint(Fp2 x)
        {
            return uint2(asuint(x.x), asuint(x.y));
        }

        /// <summary>Returns the bit pattern of a Fp3 as a uint3.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint3 asuint(Fp3 x)
        {
            return uint3(asuint(x.x), asuint(x.y), asuint(x.z));
        }

        /// <summary>Returns the bit pattern of a Fp4 as a uint4.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint4 asuint(Fp4 x)
        {
            return uint4(asuint(x.x), asuint(x.y), asuint(x.z), asuint(x.w));
        }

        /// <summary>Returns the absolute value of a int value.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The absolute value of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int abs(int x)
        {
            return max(-x, x);
        }

        /// <summary>Returns the componentwise absolute value of a int2 vector.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The componentwise absolute value of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int2 abs(int2 x)
        {
            return max(-x, x);
        }

        /// <summary>Returns the componentwise absolute value of a int3 vector.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The componentwise absolute value of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int3 abs(int3 x)
        {
            return max(-x, x);
        }

        /// <summary>Returns the componentwise absolute value of a int4 vector.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The componentwise absolute value of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int4 abs(int4 x)
        {
            return max(-x, x);
        }

        /// <summary>Returns the absolute value of a long value.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The absolute value of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long abs(long x)
        {
            return max(-x, x);
        }

        /// <summary>Returns the absolute value of a Fp value.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp abs(Fp x)
        {
            return Fp.Abs(x);
        }

        /// <summary>Returns the componentwise absolute value of a Fp2 vector.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp2 abs(Fp2 x)
        {
            return new Fp2(Fp.Abs(x.x), Fp.Abs(x.y));
        }

        /// <summary>Returns the componentwise absolute value of a Fp3 vector.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3 abs(Fp3 x)
        {
            return new Fp3(Fp.Abs(x.x), Fp.Abs(x.y), Fp.Abs(x.z));
        }

        /// <summary>Returns the componentwise absolute value of a Fp4 vector.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4 abs(Fp4 x)
        {
            return new Fp4(Fp.Abs(x.x), Fp.Abs(x.y), Fp.Abs(x.z), Fp.Abs(x.w));
        }


        /// <summary>Returns the sine of a Fp value.</summary>
        public static Fp sin(Fp x)
        {
            return Fp.Sin(x);
        }

        /// <summary>Returns the componentwise sine of a Fp2 vector.</summary>
        public static Fp2 sin(Fp2 x)
        {
            return new Fp2(sin(x.x), sin(x.y));
        }

        /// <summary>Returns the componentwise sine of a Fp3 vector.</summary>
        public static Fp3 sin(Fp3 x)
        {
            return new Fp3(sin(x.x), sin(x.y), sin(x.z));
        }

        /// <summary>Returns the componentwise sine of a Fp4 vector.</summary>
        public static Fp4 sin(Fp4 x)
        {
            return new Fp4(sin(x.x), sin(x.y), sin(x.z), sin(x.w));
        }


        /// <summary>Returns the cosine of a Fp value.</summary>
        public static Fp cos(Fp x)
        {
            // x.value = FixedPointMathLibWrapper.fixed_cos(x.value);
            // return x;
            return Fp.Cos(x);
        }

        /// <summary>Returns the componentwise cosine of a Fp2 vector.</summary>
        public static Fp2 cos(Fp2 x)
        {
            return new Fp2(cos(x.x), cos(x.y));
        }

        /// <summary>Returns the componentwise cosine of a Fp3 vector.</summary>
        public static Fp3 cos(Fp3 x)
        {
            return new Fp3(cos(x.x), cos(x.y), cos(x.z));
        }

        /// <summary>Returns the componentwise cosine of a Fp4 vector.</summary>
        public static Fp4 cos(Fp4 x)
        {
            return new Fp4(cos(x.x), cos(x.y), cos(x.z), cos(x.w));
        }


        /// <summary>Returns the tangent of a Fp value.</summary>
        public static Fp tan(Fp x)
        {
            return Fp.Tan(x);
        }

        /// <summary>Returns the componentwise tangent of a Fp2 vector.</summary>
        public static Fp2 tan(Fp2 x)
        {
            return new Fp2(tan(x.x), tan(x.y));
        }

        /// <summary>Returns the componentwise tangent of a Fp3 vector.</summary>
        public static Fp3 tan(Fp3 x)
        {
            return new Fp3(tan(x.x), tan(x.y), tan(x.z));
        }

        /// <summary>Returns the componentwise tangent of a Fp4 vector.</summary>
        public static Fp4 tan(Fp4 x)
        {
            return new Fp4(tan(x.x), tan(x.y), tan(x.z), tan(x.w));
        }


        /// <summary>Returns the hyperbolic tangent of a Fp value.</summary>
        [System.Obsolete(NotSupportedYet, true)]
        public static Fp tanh(Fp x)
        {
            throw new System.NotImplementedException("Fp doesn't support tanh");
        }

        /// <summary>Returns the componentwise hyperbolic tangent of a Fp2 vector.</summary>
        [System.Obsolete(NotSupportedYet, true)]
        public static Fp2 tanh(Fp2 x)
        {
            return new Fp2(tanh(x.x), tanh(x.y));
        }

        /// <summary>Returns the componentwise hyperbolic tangent of a Fp3 vector.</summary>
        [System.Obsolete(NotSupportedYet, true)]
        public static Fp3 tanh(Fp3 x)
        {
            return new Fp3(tanh(x.x), tanh(x.y), tanh(x.z));
        }

        /// <summary>Returns the componentwise hyperbolic tangent of a Fp4 vector.</summary>
        [System.Obsolete(NotSupportedYet, true)]
        public static Fp4 tanh(Fp4 x)
        {
            return new Fp4(tanh(x.x), tanh(x.y), tanh(x.z), tanh(x.w));
        }


        /// <summary>Returns the dot product of two Fp values. Equivalent to multiplication.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp dot(Fp x, Fp y)
        {
            return x * y;
        }

        /// <summary>Returns the dot product of two Fp2 vectors.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp dot(Fp2 x, Fp2 y)
        {
            return x.x * y.x + x.y * y.y;
        }

        /// <summary>Returns the dot product of two Fp3 vectors.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp dot(Fp3 x, Fp3 y)
        {
            return x.x * y.x + x.y * y.y + x.z * y.z;
        }

        /// <summary>Returns the dot product of two Fp4 vectors.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp dot(Fp4 x, Fp4 y)
        {
            return x.x * y.x + x.y * y.y + x.z * y.z + x.w * y.w;
        }


        /// <summary>Returns the result of clamping the Fp value x into the interval [0, 1].</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp saturate(Fp x)
        {
            return clamp(x, 0, Fp.One);
        }

        /// <summary>Returns the result of a componentwise clamping of the Fp2 vector x into the interval [0, 1].</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp2 saturate(Fp2 x)
        {
            return clamp(x, new Fp2(0), new Fp2(Fp.One));
        }

        /// <summary>Returns the result of a componentwise clamping of the Fp3 vector x into the interval [0, 1].</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3 saturate(Fp3 x)
        {
            return clamp(x, new Fp3(0), new Fp3(Fp.One));
        }

        /// <summary>Returns the result of a componentwise clamping of the Fp4 vector x into the interval [0, 1].</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4 saturate(Fp4 x)
        {
            return clamp(x, new Fp4(0), new Fp4(Fp.One));
        }


        /// <summary>Returns the result of clamping the value x into the interval [a, b], where x, a and b are int values.</summary>
        /// <param name="x">Input value to be clamped.</param>
        /// <param name="a">Lower bound of the interval.</param>
        /// <param name="b">Upper bound of the interval.</param>
        /// <returns>The clamping of the input x into the interval [a, b].</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int clamp(int x, int a, int b)
        {
            return max(a, min(b, x));
        }

        /// <summary>Returns the result of a componentwise clamping of the int2 x into the interval [a, b], where a and b are int2 vectors.</summary>
        /// <param name="x">Input value to be clamped.</param>
        /// <param name="a">Lower bound of the interval.</param>
        /// <param name="b">Upper bound of the interval.</param>
        /// <returns>The componentwise clamping of the input x into the interval [a, b].</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int2 clamp(int2 x, int2 a, int2 b)
        {
            return max(a, min(b, x));
        }

        /// <summary>Returns the result of a componentwise clamping of the int3 x into the interval [a, b], where x, a and b are int3 vectors.</summary>
        /// <param name="x">Input value to be clamped.</param>
        /// <param name="a">Lower bound of the interval.</param>
        /// <param name="b">Upper bound of the interval.</param>
        /// <returns>The componentwise clamping of the input x into the interval [a, b].</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int3 clamp(int3 x, int3 a, int3 b)
        {
            return max(a, min(b, x));
        }

        /// <summary>Returns the result of a componentwise clamping of the value x into the interval [a, b], where x, a and b are int4 vectors.</summary>
        /// <param name="x">Input value to be clamped.</param>
        /// <param name="a">Lower bound of the interval.</param>
        /// <param name="b">Upper bound of the interval.</param>
        /// <returns>The componentwise clamping of the input x into the interval [a, b].</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int4 clamp(int4 x, int4 a, int4 b)
        {
            return max(a, min(b, x));
        }


        /// <summary>Returns the result of clamping the value x into the interval [a, b], where x, a and b are uint values.</summary>
        /// <param name="x">Input value to be clamped.</param>
        /// <param name="a">Lower bound of the interval.</param>
        /// <param name="b">Upper bound of the interval.</param>
        /// <returns>The clamping of the input x into the interval [a, b].</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint clamp(uint x, uint a, uint b)
        {
            return max(a, min(b, x));
        }

        /// <summary>Returns the result of a componentwise clamping of the value x into the interval [a, b], where x, a and b are uint2 vectors.</summary>
        /// <param name="x">Input value to be clamped.</param>
        /// <param name="a">Lower bound of the interval.</param>
        /// <param name="b">Upper bound of the interval.</param>
        /// <returns>The componentwise clamping of the input x into the interval [a, b].</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint2 clamp(uint2 x, uint2 a, uint2 b)
        {
            return max(a, min(b, x));
        }

        /// <summary>Returns the result of a componentwise clamping of the value x into the interval [a, b], where x, a and b are uint3 vectors.</summary>
        /// <param name="x">Input value to be clamped.</param>
        /// <param name="a">Lower bound of the interval.</param>
        /// <param name="b">Upper bound of the interval.</param>
        /// <returns>The componentwise clamping of the input x into the interval [a, b].</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint3 clamp(uint3 x, uint3 a, uint3 b)
        {
            return max(a, min(b, x));
        }

        /// <summary>Returns the result of a componentwise clamping of the value x into the interval [a, b], where x, a and b are uint4 vectors.</summary>
        /// <param name="x">Input value to be clamped.</param>
        /// <param name="a">Lower bound of the interval.</param>
        /// <param name="b">Upper bound of the interval.</param>
        /// <returns>The componentwise clamping of the input x into the interval [a, b].</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint4 clamp(uint4 x, uint4 a, uint4 b)
        {
            return max(a, min(b, x));
        }


        /// <summary>Returns the result of clamping the value x into the interval [a, b], where x, a and b are long values.</summary>
        /// <param name="x">Input value to be clamped.</param>
        /// <param name="a">Lower bound of the interval.</param>
        /// <param name="b">Upper bound of the interval.</param>
        /// <returns>The clamping of the input x into the interval [a, b].</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long clamp(long x, long a, long b)
        {
            return max(a, min(b, x));
        }

        /// <summary>Returns the result of clamping the value x into the interval [a, b], where x, a and b are ulong values.</summary>
        /// <param name="x">Input value to be clamped.</param>
        /// <param name="a">Lower bound of the interval.</param>
        /// <param name="b">Upper bound of the interval.</param>
        /// <returns>The clamping of the input x into the interval [a, b].</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong clamp(ulong x, ulong a, ulong b)
        {
            return max(a, min(b, x));
        }

        /// <summary>Returns the result of clamping the value x into the interval [a, b], where x, a and b are Fp values.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp clamp(Fp x, Fp a, Fp b)
        {
            return max(a, min(b, x));
        }

        /// <summary>Returns the result of a componentwise clamping of the value x into the interval [a, b], where x, a and b are Fp2 vectors.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp2 clamp(Fp2 x, Fp2 a, Fp2 b)
        {
            return max(a, min(b, x));
        }

        /// <summary>Returns the result of a componentwise clamping of the value x into the interval [a, b], where x, a and b are Fp3 vectors.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3 clamp(Fp3 x, Fp3 a, Fp3 b)
        {
            return max(a, min(b, x));
        }

        /// <summary>Returns the result of a componentwise clamping of the value x into the interval [a, b], where x, a and b are Fp4 vectors.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4 clamp(Fp4 x, Fp4 a, Fp4 b)
        {
            return max(a, min(b, x));
        }


        /// <summary>Returns the result of a multiply-add operation (a * b + c) on 3 Fp values.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp mad(Fp a, Fp b, Fp c)
        {
            return a * b + c;
        }

        /// <summary>Returns the result of a componentwise multiply-add operation (a * b + c) on 3 Fp2 vectors.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp2 mad(Fp2 a, Fp2 b, Fp2 c)
        {
            return a * b + c;
        }

        /// <summary>Returns the result of a componentwise multiply-add operation (a * b + c) on 3 Fp3 vectors.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3 mad(Fp3 a, Fp3 b, Fp3 c)
        {
            return a * b + c;
        }

        /// <summary>Returns the result of a componentwise multiply-add operation (a * b + c) on 3 Fp4 vectors.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4 mad(Fp4 a, Fp4 b, Fp4 c)
        {
            return a * b + c;
        }


        /// <summary>Returns the result of a non-clamping linear remapping of a value x from [a, b] to [c, d].</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp remap(Fp a, Fp b, Fp c, Fp d, Fp x)
        {
            return lerp(c, d, unlerp(a, b, x));
        }

        /// <summary>Returns the componentwise result of a non-clamping linear remapping of a value x from [a, b] to [c, d].</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp2 remap(Fp2 a, Fp2 b, Fp2 c, Fp2 d, Fp2 x)
        {
            return lerp(c, d, unlerp(a, b, x));
        }

        /// <summary>Returns the componentwise result of a non-clamping linear remapping of a value x from [a, b] to [c, d].</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3 remap(Fp3 a, Fp3 b, Fp3 c, Fp3 d, Fp3 x)
        {
            return lerp(c, d, unlerp(a, b, x));
        }

        /// <summary>Returns the componentwise result of a non-clamping linear remapping of a value x from [a, b] to [c, d].</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4 remap(Fp4 a, Fp4 b, Fp4 c, Fp4 d, Fp4 x)
        {
            return lerp(c, d, unlerp(a, b, x));
        }


        /// <summary>Returns the result of normalizing a fixed point value x to a range [a, b]. The opposite of lerp. Equivalent to (x - a) / (b - a).</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp unlerp(Fp a, Fp b, Fp x)
        {
            return (x - a) / (b - a);
        }

        /// <summary>Returns the componentwise result of normalizing a fixed point value x to a range [a, b]. The opposite of lerp. Equivalent to (x - a) / (b - a).</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp2 unlerp(Fp2 a, Fp2 b, Fp2 x)
        {
            return (x - a) / (b - a);
        }

        /// <summary>Returns the componentwise result of normalizing a fixed point value x to a range [a, b]. The opposite of lerp. Equivalent to (x - a) / (b - a).</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3 unlerp(Fp3 a, Fp3 b, Fp3 x)
        {
            return (x - a) / (b - a);
        }

        /// <summary>Returns the componentwise result of normalizing a fixed point value x to a range [a, b]. The opposite of lerp. Equivalent to (x - a) / (b - a).</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4 unlerp(Fp4 a, Fp4 b, Fp4 x)
        {
            return (x - a) / (b - a);
        }


        /// <summary>Returns the result of linearly interpolating from x to y using the interpolation parameter s.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp lerp(Fp x, Fp y, Fp s)
        {
            return x + s * (y - x);
        }

        /// <summary>Returns the result of a componentwise linear interpolating from x to y using the interpolation parameter s.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp2 lerp(Fp2 x, Fp2 y, Fp s)
        {
            return x + s * (y - x);
        }

        /// <summary>Returns the result of a componentwise linear interpolating from x to y using the interpolation parameter s.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3 lerp(Fp3 x, Fp3 y, Fp s)
        {
            return x + s * (y - x);
        }

        /// <summary>Returns the result of a componentwise linear interpolating from x to y using the interpolation parameter s.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4 lerp(Fp4 x, Fp4 y, Fp s)
        {
            return x + s * (y - x);
        }


        /// <summary>Returns the result of a componentwise linear interpolating from x to y using the corresponding components of the interpolation parameter s.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp2 lerp(Fp2 x, Fp2 y, Fp2 s)
        {
            return x + s * (y - x);
        }

        /// <summary>Returns the result of a componentwise linear interpolating from x to y using the corresponding components of the interpolation parameter s.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3 lerp(Fp3 x, Fp3 y, Fp3 s)
        {
            return x + s * (y - x);
        }

        /// <summary>Returns the result of a componentwise linear interpolating from x to y using the corresponding components of the interpolation parameter s.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4 lerp(Fp4 x, Fp4 y, Fp4 s)
        {
            return x + s * (y - x);
        }


        /// <summary>Returns the maximum of two int values.</summary>
        /// <param name="x">The first input value.</param>
        /// <param name="y">The second input value.</param>
        /// <returns>The maximum of the two input values.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int max(int x, int y)
        {
            return x > y ? x : y;
        }

        /// <summary>Returns the componentwise maximum of two int2 vectors.</summary>
        /// <param name="x">The first input value.</param>
        /// <param name="y">The second input value.</param>
        /// <returns>The componentwise maximum of the two input values.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int2 max(int2 x, int2 y)
        {
            return new int2(max(x.x, y.x), max(x.y, y.y));
        }

        /// <summary>Returns the componentwise maximum of two int3 vectors.</summary>
        /// <param name="x">The first input value.</param>
        /// <param name="y">The second input value.</param>
        /// <returns>The componentwise maximum of the two input values.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int3 max(int3 x, int3 y)
        {
            return new int3(max(x.x, y.x), max(x.y, y.y), max(x.z, y.z));
        }

        /// <summary>Returns the componentwise maximum of two int4 vectors.</summary>
        /// <param name="x">The first input value.</param>
        /// <param name="y">The second input value.</param>
        /// <returns>The componentwise maximum of the two input values.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int4 max(int4 x, int4 y)
        {
            return new int4(max(x.x, y.x), max(x.y, y.y), max(x.z, y.z), max(x.w, y.w));
        }


        /// <summary>Returns the maximum of two uint values.</summary>
        /// <param name="x">The first input value.</param>
        /// <param name="y">The second input value.</param>
        /// <returns>The maximum of the two input values.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint max(uint x, uint y)
        {
            return x > y ? x : y;
        }

        /// <summary>Returns the componentwise maximum of two uint2 vectors.</summary>
        /// <param name="x">The first input value.</param>
        /// <param name="y">The second input value.</param>
        /// <returns>The componentwise maximum of the two input values.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint2 max(uint2 x, uint2 y)
        {
            return new uint2(max(x.x, y.x), max(x.y, y.y));
        }

        /// <summary>Returns the componentwise maximum of two uint3 vectors.</summary>
        /// <param name="x">The first input value.</param>
        /// <param name="y">The second input value.</param>
        /// <returns>The componentwise maximum of the two input values.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint3 max(uint3 x, uint3 y)
        {
            return new uint3(max(x.x, y.x), max(x.y, y.y), max(x.z, y.z));
        }

        /// <summary>Returns the componentwise maximum of two uint4 vectors.</summary>
        /// <param name="x">The first input value.</param>
        /// <param name="y">The second input value.</param>
        /// <returns>The componentwise maximum of the two input values.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint4 max(uint4 x, uint4 y)
        {
            return new uint4(max(x.x, y.x), max(x.y, y.y), max(x.z, y.z), max(x.w, y.w));
        }


        /// <summary>Returns the maximum of two long values.</summary>
        /// <param name="x">The first input value.</param>
        /// <param name="y">The second input value.</param>
        /// <returns>The maximum of the two input values.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long max(long x, long y)
        {
            return x > y ? x : y;
        }


        /// <summary>Returns the maximum of two ulong values.</summary>
        /// <param name="x">The first input value.</param>
        /// <param name="y">The second input value.</param>
        /// <returns>The maximum of the two input values.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong max(ulong x, ulong y)
        {
            return x > y ? x : y;
        }


        /// <summary>Returns the maximum of two Fp values.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp max(Fp x, Fp y)
        {
            return x > y ? x : y;
        }

        /// <summary>Returns the componentwise maximum of two Fp2 vectors.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp2 max(Fp2 x, Fp2 y)
        {
            return new Fp2(max(x.x, y.x), max(x.y, y.y));
        }

        /// <summary>Returns the componentwise maximum of two Fp3 vectors.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3 max(Fp3 x, Fp3 y)
        {
            return new Fp3(max(x.x, y.x), max(x.y, y.y), max(x.z, y.z));
        }

        /// <summary>Returns the componentwise maximum of two Fp4 vectors.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4 max(Fp4 x, Fp4 y)
        {
            return new Fp4(max(x.x, y.x), max(x.y, y.y), max(x.z, y.z), max(x.w, y.w));
        }


        /// <summary>Returns the minimum of two int values.</summary>
        /// <param name="x">The first input value.</param>
        /// <param name="y">The second input value.</param>
        /// <returns>The minimum of the two input values.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int min(int x, int y)
        {
            return x < y ? x : y;
        }

        /// <summary>Returns the componentwise minimum of two int2 vectors.</summary>
        /// <param name="x">The first input value.</param>
        /// <param name="y">The second input value.</param>
        /// <returns>The componentwise minimum of the two input values.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int2 min(int2 x, int2 y)
        {
            return new int2(min(x.x, y.x), min(x.y, y.y));
        }

        /// <summary>Returns the componentwise minimum of two int3 vectors.</summary>
        /// <param name="x">The first input value.</param>
        /// <param name="y">The second input value.</param>
        /// <returns>The componentwise minimum of the two input values.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int3 min(int3 x, int3 y)
        {
            return new int3(min(x.x, y.x), min(x.y, y.y), min(x.z, y.z));
        }

        /// <summary>Returns the componentwise minimum of two int4 vectors.</summary>
        /// <param name="x">The first input value.</param>
        /// <param name="y">The second input value.</param>
        /// <returns>The componentwise minimum of the two input values.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int4 min(int4 x, int4 y)
        {
            return new int4(min(x.x, y.x), min(x.y, y.y), min(x.z, y.z), min(x.w, y.w));
        }


        /// <summary>Returns the minimum of two uint values.</summary>
        /// <param name="x">The first input value.</param>
        /// <param name="y">The second input value.</param>
        /// <returns>The minimum of the two input values.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint min(uint x, uint y)
        {
            return x < y ? x : y;
        }

        /// <summary>Returns the componentwise minimum of two uint2 vectors.</summary>
        /// <param name="x">The first input value.</param>
        /// <param name="y">The second input value.</param>
        /// <returns>The componentwise minimum of the two input values.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint2 min(uint2 x, uint2 y)
        {
            return new uint2(min(x.x, y.x), min(x.y, y.y));
        }

        /// <summary>Returns the componentwise minimum of two uint3 vectors.</summary>
        /// <param name="x">The first input value.</param>
        /// <param name="y">The second input value.</param>
        /// <returns>The componentwise minimum of the two input values.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint3 min(uint3 x, uint3 y)
        {
            return new uint3(min(x.x, y.x), min(x.y, y.y), min(x.z, y.z));
        }

        /// <summary>Returns the componentwise minimum of two uint4 vectors.</summary>
        /// <param name="x">The first input value.</param>
        /// <param name="y">The second input value.</param>
        /// <returns>The componentwise minimum of the two input values.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint4 min(uint4 x, uint4 y)
        {
            return new uint4(min(x.x, y.x), min(x.y, y.y), min(x.z, y.z), min(x.w, y.w));
        }


        /// <summary>Returns the minimum of two long values.</summary>
        /// <param name="x">The first input value.</param>
        /// <param name="y">The second input value.</param>
        /// <returns>The minimum of the two input values.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long min(long x, long y)
        {
            return x < y ? x : y;
        }


        /// <summary>Returns the minimum of two ulong values.</summary>
        /// <param name="x">The first input value.</param>
        /// <param name="y">The second input value.</param>
        /// <returns>The minimum of the two input values.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong min(ulong x, ulong y)
        {
            return x < y ? x : y;
        }

        /// <summary>Returns the minimum of two Fp values.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp min(Fp x, Fp y)
        {
            return x < y ? x : y;
        }

        /// <summary>Returns the componentwise minimum of two Fp2 vectors.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp2 min(Fp2 x, Fp2 y)
        {
            return new Fp2(min(x.x, y.x), min(x.y, y.y));
        }

        /// <summary>Returns the componentwise minimum of two Fp3 vectors.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3 min(Fp3 x, Fp3 y)
        {
            return new Fp3(min(x.x, y.x), min(x.y, y.y), min(x.z, y.z));
        }

        /// <summary>Returns the componentwise minimum of two Fp4 vectors.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4 min(Fp4 x, Fp4 y)
        {
            return new Fp4(min(x.x, y.x), min(x.y, y.y), min(x.z, y.z), min(x.w, y.w));
        }


        /// <summary>Returns true if the input Fp is a NaN (not a number) fixed point value, false otherwise.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool isnan(Fp x)
        {
            return Fp.IsNaN(x) != 0;
        }

        /// <summary>Returns a bool2 indicating for each component of a Fp2 whether it is a NaN (not a number) fixed point value.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool2 isnan(Fp2 x)
        {
            return bool2(isnan(x.x), isnan(x.y));
        }

        /// <summary>Returns a bool3 indicating for each component of a Fp3 whether it is a NaN (not a number) fixed point value.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool3 isnan(Fp3 x)
        {
            return bool3(isnan(x.x), isnan(x.y), isnan(x.z));
        }

        /// <summary>Returns a bool4 indicating for each component of a Fp4 whether it is a NaN (not a number) fixed point value.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool4 isnan(Fp4 x)
        {
            return bool4(isnan(x.x), isnan(x.y), isnan(x.z), isnan(x.w));
        }


        /// <summary>Returns true if the input Fp is an infinite fixed point value, false otherwise.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool isinf(Fp x)
        {
            return Fp.IsInf(x) != 0;
        }

        /// <summary>Returns a bool2 indicating for each component of a Fp2 whether it is an infinite fixed point value.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool2 isinf(Fp2 x)
        {
            return bool2(isinf(x.x), isinf(x.y));
        }

        /// <summary>Returns a bool3 indicating for each component of a Fp3 whether it is an infinite fixed point value.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool3 isinf(Fp3 x)
        {
            return bool3(isinf(x.x), isinf(x.y), isinf(x.z));
        }

        /// <summary>Returns a bool4 indicating for each component of a Fp4 whether it is an infinite fixed point value.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool4 isinf(Fp4 x)
        {
            return bool4(isinf(x.x), isinf(x.y), isinf(x.z), isinf(x.w));
        }


        /// <summary>Returns true if the input Fp is a finite fixed point value, false otherwise.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool isfinite(Fp x)
        {
            return Fp.IsInf(x) == 0;
        }

        /// <summary>Returns a bool2 indicating for each component of a Fp2 whether it is a finite fixed point value.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool2 isfinite(Fp2 x)
        {
            return bool2(isfinite(x.x), isfinite(x.y));
        }

        /// <summary>Returns a bool3 indicating for each component of a Fp3 whether it is a finite fixed point value.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool3 isfinite(Fp3 x)
        {
            return bool3(isfinite(x.x), isfinite(x.y), isfinite(x.z));
        }

        /// <summary>Returns a bool4 indicating for each component of a Fp4 whether it is a finite fixed point value.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool4 isfinite(Fp4 x)
        {
            return bool4(isfinite(x.x), isfinite(x.y), isfinite(x.z), isfinite(x.w));
        }


        /// <summary>Returns the arccosine of a Fp value.</summary>
        public static Fp asin(Fp x)
        {
            return Fp.Asin(x);
        }

        /// <summary>Returns the componentwise arccosine of a Fp2 vector.</summary>
        public static Fp2 asin(Fp2 x)
        {
            return new Fp2(asin(x.x), asin(x.y));
        }

        /// <summary>Returns the componentwise arccosine of a Fp3 vector.</summary>
        public static Fp3 asin(Fp3 x)
        {
            return new Fp3(asin(x.x), asin(x.y), asin(x.z));
        }

        /// <summary>Returns the componentwise arccosine of a Fp4 vector.</summary>
        public static Fp4 asin(Fp4 x)
        {
            return new Fp4(asin(x.x), asin(x.y), asin(x.z), asin(x.w));
        }

        /// <summary>Returns the arccosine of a Fp value.</summary>
        public static Fp acos(Fp x)
        {
            return Fp.Acos(x);
        }

        /// <summary>Returns the componentwise arccosine of a Fp2 vector.</summary>
        public static Fp2 acos(Fp2 x)
        {
            return new Fp2(acos(x.x), acos(x.y));
        }

        /// <summary>Returns the componentwise arccosine of a Fp3 vector.</summary>
        public static Fp3 acos(Fp3 x)
        {
            return new Fp3(acos(x.x), acos(x.y), acos(x.z));
        }

        /// <summary>Returns the componentwise arccosine of a Fp4 vector.</summary>
        public static Fp4 acos(Fp4 x)
        {
            return new Fp4(acos(x.x), acos(x.y), acos(x.z), acos(x.w));
        }

        /// <summary>Returns the arctangent of a Fp value.</summary>
        public static Fp atan(Fp x)
        {
            return Fp.Atan(x);
        }

        /// <summary>Returns the componentwise arctangent of a Fp2 vector.</summary>
        public static Fp2 atan(Fp2 x)
        {
            return new Fp2(atan(x.x), atan(x.y));
        }

        /// <summary>Returns the componentwise arctangent of a Fp3 vector.</summary>
        public static Fp3 atan(Fp3 x)
        {
            return new Fp3(atan(x.x), atan(x.y), atan(x.z));
        }

        /// <summary>Returns the componentwise arctangent of a Fp4 vector.</summary>
        public static Fp4 atan(Fp4 x)
        {
            return new Fp4(atan(x.x), atan(x.y), atan(x.z), atan(x.w));
        }


        /// <summary>Returns the 2-argument arctangent of a pair of Fp values.</summary>
        public static Fp atan2(Fp y, Fp x)
        {
            return Fp.Atan2(y, x);
        }

        /// <summary>Returns the componentwise 2-argument arctangent of a pair of Fps2 vectors.</summary>
        public static Fp2 atan2(Fp2 y, Fp2 x)
        {
            return new Fp2(atan2(y.x, x.x), atan2(y.y, x.y));
        }

        /// <summary>Returns the componentwise 2-argument arctangent of a pair of Fps3 vectors.</summary>
        public static Fp3 atan2(Fp3 y, Fp3 x)
        {
            return new Fp3(atan2(y.x, x.x), atan2(y.y, x.y), atan2(y.z, x.z));
        }

        /// <summary>Returns the componentwise 2-argument arctangent of a pair of Fps4 vectors.</summary>
        public static Fp4 atan2(Fp4 y, Fp4 x)
        {
            return new Fp4(atan2(y.x, x.x), atan2(y.y, x.y), atan2(y.z, x.z), atan2(y.w, x.w));
        }


        /**
         * <summary>
         * Returns the hyperbolic cosine of a Fp value.
         * </summary>
         */
        [System.Obsolete(NotSupportedYet, true)]
        public static Fp cosh(Fp x)
        {
            throw new System.NotImplementedException("Fp doesn't support cosh");
        }

        /// <summary>Returns the componentwise hyperbolic cosine of a Fp2 vector.</summary>
        [System.Obsolete(NotSupportedYet, true)]
        public static Fp2 cosh(Fp2 x)
        {
            return new Fp2(cosh(x.x), cosh(x.y));
        }

        /// <summary>Returns the componentwise hyperbolic cosine of a Fp3 vector.</summary>
        [System.Obsolete(NotSupportedYet, true)]
        public static Fp3 cosh(Fp3 x)
        {
            return new Fp3(cosh(x.x), cosh(x.y), cosh(x.z));
        }

        /// <summary>Returns the componentwise hyperbolic cosine of a Fp4 vector.</summary>
        [System.Obsolete(NotSupportedYet, true)]
        public static Fp4 cosh(Fp4 x)
        {
            return new Fp4(cosh(x.x), cosh(x.y), cosh(x.z), cosh(x.w));
        }


        /// <summary>Returns the hyperbolic sine of a Fp value.</summary>
        [System.Obsolete(NotSupportedYet, true)]
        public static Fp sinh(Fp x)
        {
            throw new System.NotImplementedException("Fp doesn't support sinh");
        }

        /// <summary>Returns the componentwise hyperbolic sine of a Fp2 vector.</summary>
        [System.Obsolete(NotSupportedYet, true)]
        public static Fp2 sinh(Fp2 x)
        {
            return new Fp2(sinh(x.x), sinh(x.y));
        }

        /// <summary>Returns the componentwise hyperbolic sine of a Fp3 vector.</summary>
        [System.Obsolete(NotSupportedYet, true)]
        public static Fp3 sinh(Fp3 x)
        {
            return new Fp3(sinh(x.x), sinh(x.y), sinh(x.z));
        }

        /// <summary>Returns the componentwise hyperbolic sine of a Fp4 vector.</summary>
        [System.Obsolete(NotSupportedYet, true)]
        public static Fp4 sinh(Fp4 x)
        {
            return new Fp4(sinh(x.x), sinh(x.y), sinh(x.z), sinh(x.w));
        }


        /// <summary>Returns the result of rounding a Fp value up to the nearest integral value less or equal to the original value.</summary>
        public static Fp floor(Fp x)
        {
            return Fp.Floor(x);
        }

        /// <summary>Returns the result of rounding each component of a Fp2 vector value down to the nearest value less or equal to the original value.</summary>
        public static Fp2 floor(Fp2 x)
        {
            return new Fp2(floor(x.x), floor(x.y));
        }

        /// <summary>Returns the result of rounding each component of a Fp3 vector value down to the nearest value less or equal to the original value.</summary>
        public static Fp3 floor(Fp3 x)
        {
            return new Fp3(floor(x.x), floor(x.y), floor(x.z));
        }

        /// <summary>Returns the result of rounding each component of a Fp4 vector value down to the nearest value less or equal to the original value.</summary>
        public static Fp4 floor(Fp4 x)
        {
            return new Fp4(floor(x.x), floor(x.y), floor(x.z), floor(x.w));
        }


        /// <summary>Returns the result of rounding a Fp value up to the nearest integral value greater or equal to the original value.</summary>
        public static Fp ceil(Fp x)
        {
            return Fp.Ceiling(x);
        }

        /// <summary>Returns the result of rounding each component of a Fp2 vector value up to the nearest value greater or equal to the original value.</summary>
        public static Fp2 ceil(Fp2 x)
        {
            return new Fp2(ceil(x.x), ceil(x.y));
        }

        /// <summary>Returns the result of rounding each component of a Fp3 vector value up to the nearest value greater or equal to the original value.</summary>
        public static Fp3 ceil(Fp3 x)
        {
            return new Fp3(ceil(x.x), ceil(x.y), ceil(x.z));
        }

        /// <summary>Returns the result of rounding each component of a Fp4 vector value up to the nearest value greater or equal to the original value.</summary>
        public static Fp4 ceil(Fp4 x)
        {
            return new Fp4(ceil(x.x), ceil(x.y), ceil(x.z), ceil(x.w));
        }


        /// <summary>Returns the result of rounding a Fp value to the nearest integral value.</summary>
        public static Fp round(Fp x)
        {
            return Fp.Round(x);
        }

        /// <summary>Returns the result of rounding each component of a Fp2 vector value to the nearest integral value.</summary>
        public static Fp2 round(Fp2 x)
        {
            return new Fp2(round(x.x), round(x.y));
        }

        /// <summary>Returns the result of rounding each component of a Fp3 vector value to the nearest integral value.</summary>
        public static Fp3 round(Fp3 x)
        {
            return new Fp3(round(x.x), round(x.y), round(x.z));
        }

        /// <summary>Returns the result of rounding each component of a Fp4 vector value to the nearest integral value.</summary>
        public static Fp4 round(Fp4 x)
        {
            return new Fp4(round(x.x), round(x.y), round(x.z), round(x.w));
        }


        /// <summary>Returns the result of truncating a Fp value to an integral Fp value.</summary>
        public static Fp trunc(Fp x)
        {
            return Fp.Truncate(x);
        }

        /// <summary>Returns the result of a componentwise truncation of a Fp2 value to an integral Fp2 value.</summary>
        public static Fp2 trunc(Fp2 x)
        {
            return new Fp2(trunc(x.x), trunc(x.y));
        }

        /// <summary>Returns the result of a componentwise truncation of a Fp3 value to an integral Fp3 value.</summary>
        public static Fp3 trunc(Fp3 x)
        {
            return new Fp3(trunc(x.x), trunc(x.y), trunc(x.z));
        }

        /// <summary>Returns the result of a componentwise truncation of a Fp4 value to an integral Fp4 value.</summary>
        public static Fp4 trunc(Fp4 x)
        {
            return new Fp4(trunc(x.x), trunc(x.y), trunc(x.z), trunc(x.w));
        }


        /// <summary>Returns the fractional part of a Fp value.</summary>
        public static Fp frac(Fp x)
        {
            return x - floor(x);
        }

        /// <summary>Returns the componentwise fractional parts of a Fp2 vector.</summary>
        public static Fp2 frac(Fp2 x)
        {
            return x - floor(x);
        }

        /// <summary>Returns the componentwise fractional parts of a Fp3 vector.</summary>
        public static Fp3 frac(Fp3 x)
        {
            return x - floor(x);
        }

        /// <summary>Returns the componentwise fractional parts of a Fp4 vector.</summary>
        public static Fp4 frac(Fp4 x)
        {
            return x - floor(x);
        }


        /// <summary>Returns the reciprocal a Fp value.</summary>
        public static Fp rcp(Fp x)
        {
            return Fp.One / x;
        }

        /// <summary>Returns the componentwise reciprocal a Fp2 vector.</summary>
        public static Fp2 rcp(Fp2 x)
        {
            return Fp.One / x;
        }

        /// <summary>Returns the componentwise reciprocal a Fp3 vector.</summary>
        public static Fp3 rcp(Fp3 x)
        {
            return Fp.One / x;
        }

        /// <summary>Returns the componentwise reciprocal a Fp4 vector.</summary>
        public static Fp4 rcp(Fp4 x)
        {
            return Fp.One / x;
        }


        public static Fp copysign(Fp a, Fp inSign)
        {
            if (sign(inSign) > 0)
            {
                return MathFp.abs(a);
            }
            else
            {
                return -MathFp.abs(a);
            }
        }

        /// <summary>Returns the sign of a Fp value. -1 if it is less than zero, 0 if it is zero and 1 if it greater than zero.</summary>
        public static Fp sign(Fp x)
        {
            return Fp.Sign(x);
        }

        /// <summary>Returns the componentwise sign of a Fp2 value. 1 for positive components, 0 for zero components and -1 for negative components.</summary>
        public static Fp2 sign(Fp2 x)
        {
            return new Fp2(sign(x.x), sign(x.y));
        }

        /// <summary>Returns the componentwise sign of a Fp3 value. 1 for positive components, 0 for zero components and -1 for negative components.</summary>
        public static Fp3 sign(Fp3 x)
        {
            return new Fp3(sign(x.x), sign(x.y), sign(x.z));
        }

        /// <summary>Returns the componentwise sign of a Fp4 value. 1 for positive components, 0 for zero components and -1 for negative components.</summary>
        public static Fp4 sign(Fp4 x)
        {
            return new Fp4(sign(x.x), sign(x.y), sign(x.z), sign(x.w));
        }


        /// <summary>Returns x raised to the power y.</summary>
        public static Fp pow(Fp x, Fp y)
        {
            return Fp.Pow(x, y);
        }

        /// <summary>Returns the componentwise result of raising x to the power y.</summary>
        public static Fp2 pow(Fp2 x, Fp2 y)
        {
            return new Fp2(pow(x.x, y.x), pow(x.y, y.y));
        }

        /// <summary>Returns the componentwise result of raising x to the power y.</summary>
        public static Fp3 pow(Fp3 x, Fp3 y)
        {
            return new Fp3(pow(x.x, y.x), pow(x.y, y.y), pow(x.z, y.z));
        }

        /// <summary>Returns the componentwise result of raising x to the power y.</summary>
        public static Fp4 pow(Fp4 x, Fp4 y)
        {
            return new Fp4(pow(x.x, y.x), pow(x.y, y.y), pow(x.z, y.z), pow(x.w, y.w));
        }


        /// <summary>Returns the base-e exponential of x.</summary>
        public static Fp exp(Fp x)
        {
            return Fp.Exp(x);
        }

        /// <summary>Returns the componentwise base-e exponential of x.</summary>
        public static Fp2 exp(Fp2 x)
        {
            return new Fp2(exp(x.x), exp(x.y));
        }

        /// <summary>Returns the componentwise base-e exponential of x.</summary>
        public static Fp3 exp(Fp3 x)
        {
            return new Fp3(exp(x.x), exp(x.y), exp(x.z));
        }

        /// <summary>Returns the componentwise base-e exponential of x.</summary>
        public static Fp4 exp(Fp4 x)
        {
            return new Fp4(exp(x.x), exp(x.y), exp(x.z), exp(x.w));
        }


        /// <summary>Returns the base-2 exponential of x.</summary>
        public static Fp exp2(Fp x)
        {
            return exp(x * Fp.Point69314718);
        }

        /// <summary>Returns the componentwise base-2 exponential of x.</summary>
        public static Fp2 exp2(Fp2 x)
        {
            return new Fp2(exp2(x.x), exp2(x.y));
        }

        /// <summary>Returns the componentwise base-2 exponential of x.</summary>
        public static Fp3 exp2(Fp3 x)
        {
            return new Fp3(exp2(x.x), exp2(x.y), exp2(x.z));
        }

        /// <summary>Returns the componentwise base-2 exponential of x.</summary>
        public static Fp4 exp2(Fp4 x)
        {
            return new Fp4(exp2(x.x), exp2(x.y), exp2(x.z), exp2(x.w));
        }


        /// <summary>Returns the base-10 exponential of x.</summary>
        public static Fp exp10(Fp x)
        {
            return exp(x * Fp.TwoPoint30258509);
        }

        /// <summary>Returns the componentwise base-10 exponential of x.</summary>
        public static Fp2 exp10(Fp2 x)
        {
            return new Fp2(exp10(x.x), exp10(x.y));
        }

        /// <summary>Returns the componentwise base-10 exponential of x.</summary>
        public static Fp3 exp10(Fp3 x)
        {
            return new Fp3(exp10(x.x), exp10(x.y), exp10(x.z));
        }

        /// <summary>Returns the componentwise base-10 exponential of x.</summary>
        public static Fp4 exp10(Fp4 x)
        {
            return new Fp4(exp10(x.x), exp10(x.y), exp10(x.z), exp10(x.w));
        }


        /// <summary>Returns the natural logarithm of a Fp value.</summary>
        public static Fp log(Fp x)
        {
            return Fp.Ln(x);
        }

        /// <summary>Returns the componentwise natural logarithm of a Fp2 vector.</summary>
        public static Fp2 log(Fp2 x)
        {
            return new Fp2(log(x.x), log(x.y));
        }

        /// <summary>Returns the componentwise natural logarithm of a Fp3 vector.</summary>
        public static Fp3 log(Fp3 x)
        {
            return new Fp3(log(x.x), log(x.y), log(x.z));
        }

        /// <summary>Returns the componentwise natural logarithm of a Fp4 vector.</summary>
        public static Fp4 log(Fp4 x)
        {
            return new Fp4(log(x.x), log(x.y), log(x.z), log(x.w));
        }


        /// <summary>Returns the base-2 logarithm of a Fp value.</summary>
        public static Fp log2(Fp x)
        {
            return Fp.Log2(x);
        }

        /// <summary>Returns the componentwise base-2 logarithm of a Fp2 vector.</summary>
        public static Fp2 log2(Fp2 x)
        {
            return new Fp2(log2(x.x), log2(x.y));
        }

        /// <summary>Returns the componentwise base-2 logarithm of a Fp3 vector.</summary>
        public static Fp3 log2(Fp3 x)
        {
            return new Fp3(log2(x.x), log2(x.y), log2(x.z));
        }

        /// <summary>Returns the componentwise base-2 logarithm of a Fp4 vector.</summary>
        public static Fp4 log2(Fp4 x)
        {
            return new Fp4(log2(x.x), log2(x.y), log2(x.z), log2(x.w));
        }


        /// <summary>Returns the base-10 logarithm of a Fp value.</summary>
        [System.Obsolete(NotSupportedYet, true)]
        public static Fp log10(Fp x)
        {
            throw new System.NotImplementedException("Fp doesn't support log10");
        }

        /// <summary>Returns the componentwise base-10 logarithm of a Fp2 vector.</summary>
        [System.Obsolete(NotSupportedYet, true)]
        public static Fp2 log10(Fp2 x)
        {
            return new Fp2(log10(x.x), log10(x.y));
        }

        /// <summary>Returns the componentwise base-10 logarithm of a Fp3 vector.</summary>
        [System.Obsolete(NotSupportedYet, true)]
        public static Fp3 log10(Fp3 x)
        {
            return new Fp3(log10(x.x), log10(x.y), log10(x.z));
        }

        /// <summary>Returns the componentwise base-10 logarithm of a Fp4 vector.</summary>
        [System.Obsolete(NotSupportedYet, true)]
        public static Fp4 log10(Fp4 x)
        {
            return new Fp4(log10(x.x), log10(x.y), log10(x.z), log10(x.w));
        }


        /// <summary>Returns the fixed point remainder of x/y.</summary>
        public static Fp fmod(Fp x, Fp y)
        {
            return x % y;
        }

        /// <summary>Returns the componentwise fixed point remainder of x/y.</summary>
        public static Fp2 fmod(Fp2 x, Fp2 y)
        {
            return new Fp2(x.x % y.x, x.y % y.y);
        }

        /// <summary>Returns the componentwise fixed point remainder of x/y.</summary>
        public static Fp3 fmod(Fp3 x, Fp3 y)
        {
            return new Fp3(x.x % y.x, x.y % y.y, x.z % y.z);
        }

        /// <summary>Returns the componentwise fixed point remainder of x/y.</summary>
        public static Fp4 fmod(Fp4 x, Fp4 y)
        {
            return new Fp4(x.x % y.x, x.y % y.y, x.z % y.z, x.w % y.w);
        }


        /// <summary>Splits a Fp value into an integral part i and a fractional part that gets returned. Both parts take the sign of the input.</summary>
        public static Fp modf(Fp x, out Fp i)
        {
            i = trunc(x);
            return x - i;
        }

        // <summary>
        // Performs a componentwise split of a Fp2 vector into an integral part i and a fractional part that gets returned.
        // Both parts take the sign of the corresponding input component.
        // </summary>
        public static Fp2 modf(Fp2 x, out Fp2 i)
        {
            i = trunc(x);
            return x - i;
        }

        // <summary>
        // Performs a componentwise split of a Fp3 vector into an integral part i and a fractional part that gets returned.
        // Both parts take the sign of the corresponding input component.
        // </summary>
        public static Fp3 modf(Fp3 x, out Fp3 i)
        {
            i = trunc(x);
            return x - i;
        }

        /** <summary>
         * Performs a componentwise split of a Fp4 vector into an integral part i and a fractional part that gets returned.
         * Both parts take the sign of the corresponding input component.
         * </summary>
         */
        public static Fp4 modf(Fp4 x, out Fp4 i)
        {
            i = trunc(x);
            return x - i;
        }


        /// <summary>Returns the square root of a Fp value.</summary>
        public static Fp sqrt(Fp x)
        {
            return Fp.Sqrt(x);
        }

        /// <summary>Returns the componentwise square root of a Fp2 vector.</summary>
        public static Fp2 sqrt(Fp2 x)
        {
            return new Fp2(sqrt(x.x), sqrt(x.y));
        }

        /// <summary>Returns the componentwise square root of a Fp3 vector.</summary>
        public static Fp3 sqrt(Fp3 x)
        {
            return new Fp3(sqrt(x.x), sqrt(x.y), sqrt(x.z));
        }

        /// <summary>Returns the componentwise square root of a Fp4 vector.</summary>
        public static Fp4 sqrt(Fp4 x)
        {
            return new Fp4(sqrt(x.x), sqrt(x.y), sqrt(x.z), sqrt(x.w));
        }


        /// <summary>Returns the reciprocal square root of a Fp value.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp rsqrt(Fp x)
        {
            return Fp.One / sqrt(x);
        }

        /// <summary>Returns the componentwise reciprocal square root of a Fp2 vector.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp2 rsqrt(Fp2 x)
        {
            return Fp.One / sqrt(x);
        }

        /// <summary>Returns the componentwise reciprocal square root of a Fp3 vector.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3 rsqrt(Fp3 x)
        {
            return Fp.One / sqrt(x);
        }

        /// <summary>Returns the componentwise reciprocal square root of a Fp4 vector</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4 rsqrt(Fp4 x)
        {
            return Fp.One / sqrt(x);
        }


        /// <summary>Returns a normalized version of the Fp2 vector x by scaling it by 1 / length(x).</summary>
        public static Fp2 normalize(Fp2 x)
        {
            return rsqrt(dot(x, x)) * x;
        }

        /// <summary>Returns a normalized version of the Fp3 vector x by scaling it by 1 / length(x).</summary>
        public static Fp3 normalize(Fp3 x)
        {
            return rsqrt(dot(x, x)) * x;
        }

        /// <summary>Returns a normalized version of the Fp4 vector x by scaling it by 1 / length(x).</summary>
        public static Fp4 normalize(Fp4 x)
        {
            return rsqrt(dot(x, x)) * x;
        }

        /// <summary>
        /// Returns a safe normalized version of the Fp2 vector x by scaling it by 1 / length(x).
        /// Returns the given default value when 1 / length(x) does not produce a finite number.
        /// </summary>
        public static Fp2 normalizesafe(Fp2 x, Fp2 defaultValue = new Fp2())
        {
            Fp len = MathFp.dot(x, x);
            return MathFp.select(defaultValue, x * MathFp.rsqrt(len), len > Fp.OneEMinus8);
        }

        /// <summary>
        /// Returns a safe normalized version of the Fp3 vector x by scaling it by 1 / length(x).
        /// Returns the given default value when 1 / length(x) does not produce a finite number.
        /// </summary>
        public static Fp3 normalizesafe(Fp3 x, Fp3 defaultValue = new Fp3())
        {
            Fp len = MathFp.dot(x, x);
            return MathFp.select(defaultValue, x * MathFp.rsqrt(len), len > Fp.OneEMinus8);
        }

        /// <summary>
        /// Returns a safe normalized version of the Fp4 vector x by scaling it by 1 / length(x).
        /// Returns the given default value when 1 / length(x) does not produce a finite number.
        /// </summary>
        public static Fp4 normalizesafe(Fp4 x, Fp4 defaultValue = new Fp4())
        {
            Fp len = MathFp.dot(x, x);
            return MathFp.select(defaultValue, x * MathFp.rsqrt(len), len > Fp.OneEMinus8);
        }


        /// <summary>Returns the length of a Fp value. Equivalent to the absolute value.</summary>
        public static Fp length(Fp x)
        {
            return abs(x);
        }

        /// <summary>Returns the length of a Fp2 vector.</summary>
        public static Fp length(Fp2 x)
        {
            return sqrt(dot(x, x));
        }

        /// <summary>Returns the length of a Fp3 vector.</summary>
        public static Fp length(Fp3 x)
        {
            return sqrt(dot(x, x));
        }

        /// <summary>Returns the length of a Fp4 vector.</summary>
        public static Fp length(Fp4 x)
        {
            return sqrt(dot(x, x));
        }


        /// <summary>Returns the squared length of a Fp value. Equivalent to squaring the value.</summary>
        public static Fp lengthSqrt(Fp x)
        {
            return x * x;
        }

        /// <summary>Returns the squared length of a Fp2 vector.</summary>
        public static Fp lengthSqrt(Fp2 x)
        {
            return dot(x, x);
        }

        /// <summary>Returns the squared length of a Fp3 vector.</summary>
        public static Fp lengthSqrt(Fp3 x)
        {
            return dot(x, x);
        }

        /// <summary>Returns the squared length of a Fp4 vector.</summary>
        public static Fp lengthSqrt(Fp4 x)
        {
            return dot(x, x);
        }


        /// <summary>Returns the distance between two Fp values.</summary>
        public static Fp distance(Fp x, Fp y)
        {
            return abs(y - x);
        }

        /// <summary>Returns the distance between two Fp2 vectors.</summary>
        public static Fp distance(Fp2 x, Fp2 y)
        {
            return length(y - x);
        }

        /// <summary>Returns the distance between two Fp3 vectors.</summary>
        public static Fp distance(Fp3 x, Fp3 y)
        {
            return length(y - x);
        }

        /// <summary>Returns the distance between two Fp4 vectors.</summary>
        public static Fp distance(Fp4 x, Fp4 y)
        {
            return length(y - x);
        }


        /// <summary>Returns the distance between two Fp values.</summary>
        public static Fp distanceSqrt(Fp x, Fp y)
        {
            return (y - x) * (y - x);
        }

        /// <summary>Returns the distance between two Fp2 vectors.</summary>
        public static Fp distanceSqrt(Fp2 x, Fp2 y)
        {
            return lengthSqrt(y - x);
        }

        /// <summary>Returns the distance between two Fp3 vectors.</summary>
        public static Fp distanceSqrt(Fp3 x, Fp3 y)
        {
            return lengthSqrt(y - x);
        }

        /// <summary>Returns the distance between two Fp4 vectors.</summary>
        public static Fp distanceSqrt(Fp4 x, Fp4 y)
        {
            return lengthSqrt(y - x);
        }


        /// <summary>Returns the cross product of two Fp3 vectors.</summary>
        public static Fp3 cross(Fp3 x, Fp3 y)
        {
            return (x * y.yzx - x.yzx * y).yzx;
        }


        /// <summary>Returns a smooth Hermite interpolation between 0 and 1 when x is in [a, b].</summary>
        public static Fp smoothStep(Fp a, Fp b, Fp x)
        {
            var t = saturate((x - a) / (b - a));
            return t * t * (3 - (2 * t));
        }

        /// <summary>Returns a componentwise smooth Hermite interpolation between 0 and 1 when x is in [a, b].</summary>
        public static Fp2 smoothStep(Fp2 a, Fp2 b, Fp2 x)
        {
            var t = saturate((x - a) / (b - a));
            return t * t * (3 - (2 * t));
        }

        /// <summary>Returns a componentwise smooth Hermite interpolation between 0 and 1 when x is in [a, b].</summary>
        public static Fp3 smoothStep(Fp3 a, Fp3 b, Fp3 x)
        {
            var t = saturate((x - a) / (b - a));
            return t * t * (3 - (2 * t));
        }

        /// <summary>Returns a componentwise smooth Hermite interpolation between 0 and 1 when x is in [a, b].</summary>
        public static Fp4 smoothStep(Fp4 a, Fp4 b, Fp4 x)
        {
            var t = saturate((x - a) / (b - a));
            return t * t * (3 - (2 * t));
        }


        /// <summary>Returns true if any component of the input bool2 vector is true, false otherwise.</summary>
        /// <param name="x">Vector of values to compare.</param>
        /// <returns>True if any the components of x are true, false otherwise.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool any(bool2 x)
        {
            return x.x || x.y;
        }

        /// <summary>Returns true if any component of the input bool3 vector is true, false otherwise.</summary>
        /// <param name="x">Vector of values to compare.</param>
        /// <returns>True if any the components of x are true, false otherwise.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool any(bool3 x)
        {
            return x.x || x.y || x.z;
        }

        /// <summary>Returns true if any components of the input bool4 vector is true, false otherwise.</summary>
        /// <param name="x">Vector of values to compare.</param>
        /// <returns>True if any the components of x are true, false otherwise.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool any(bool4 x)
        {
            return x.x || x.y || x.z || x.w;
        }


        /// <summary>Returns true if any component of the input int2 vector is non-zero, false otherwise.</summary>
        /// <param name="x">Vector of values to compare.</param>
        /// <returns>True if any the components of x are non-zero, false otherwise.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool any(int2 x)
        {
            return x.x != 0 || x.y != 0;
        }

        /// <summary>Returns true if any component of the input int3 vector is non-zero, false otherwise.</summary>
        /// <param name="x">Vector of values to compare.</param>
        /// <returns>True if any the components of x are non-zero, false otherwise.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool any(int3 x)
        {
            return x.x != 0 || x.y != 0 || x.z != 0;
        }

        /// <summary>Returns true if any components of the input int4 vector is non-zero, false otherwise.</summary>
        /// <param name="x">Vector of values to compare.</param>
        /// <returns>True if any the components of x are non-zero, false otherwise.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool any(int4 x)
        {
            return x.x != 0 || x.y != 0 || x.z != 0 || x.w != 0;
        }


        /// <summary>Returns true if any component of the input uint2 vector is non-zero, false otherwise.</summary>
        /// <param name="x">Vector of values to compare.</param>
        /// <returns>True if any the components of x are non-zero, false otherwise.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool any(uint2 x)
        {
            return x.x != 0 || x.y != 0;
        }

        /// <summary>Returns true if any component of the input uint3 vector is non-zero, false otherwise.</summary>
        /// <param name="x">Vector of values to compare.</param>
        /// <returns>True if any the components of x are non-zero, false otherwise.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool any(uint3 x)
        {
            return x.x != 0 || x.y != 0 || x.z != 0;
        }

        /// <summary>Returns true if any components of the input uint4 vector is non-zero, false otherwise.</summary>
        /// <param name="x">Vector of values to compare.</param>
        /// <returns>True if any the components of x are non-zero, false otherwise.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool any(uint4 x)
        {
            return x.x != 0 || x.y != 0 || x.z != 0 || x.w != 0;
        }

        /// <summary>Returns true if any component of the input Fp2 vector is non-zero, false otherwise.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool any(Fp2 x)
        {
            return x.x != 0 || x.y != 0;
        }

        /// <summary>Returns true if any component of the input Fp3 vector is non-zero, false otherwise.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool any(Fp3 x)
        {
            return x.x != 0 || x.y != 0 || x.z != 0;
        }

        /// <summary>Returns true if any component of the input Fp4 vector is non-zero, false otherwise.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool any(Fp4 x)
        {
            return x.x != 0 || x.y != 0 || x.z != 0 || x.w != 0;
        }


        /// <summary>Returns true if all components of the input bool2 vector are true, false otherwise.</summary>
        /// <param name="x">Vector of values to compare.</param>
        /// <returns>True if all the components of x are true, false otherwise.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool all(bool2 x)
        {
            return x.x && x.y;
        }

        /// <summary>Returns true if all components of the input bool3 vector are true, false otherwise.</summary>
        /// <param name="x">Vector of values to compare.</param>
        /// <returns>True if all the components of x are true, false otherwise.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool all(bool3 x)
        {
            return x.x && x.y && x.z;
        }

        /// <summary>Returns true if all components of the input bool4 vector are true, false otherwise.</summary>
        /// <param name="x">Vector of values to compare.</param>
        /// <returns>True if all the components of x are true, false otherwise.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool all(bool4 x)
        {
            return x.x && x.y && x.z && x.w;
        }


        /// <summary>Returns true if all components of the input int2 vector are non-zero, false otherwise.</summary>
        /// <param name="x">Vector of values to compare.</param>
        /// <returns>True if all the components of x are non-zero, false otherwise.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool all(int2 x)
        {
            return x.x != 0 && x.y != 0;
        }

        /// <summary>Returns true if all components of the input int3 vector are non-zero, false otherwise.</summary>
        /// <param name="x">Vector of values to compare.</param>
        /// <returns>True if all the components of x are non-zero, false otherwise.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool all(int3 x)
        {
            return x.x != 0 && x.y != 0 && x.z != 0;
        }

        /// <summary>Returns true if all components of the input int4 vector are non-zero, false otherwise.</summary>
        /// <param name="x">Vector of values to compare.</param>
        /// <returns>True if all the components of x are non-zero, false otherwise.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool all(int4 x)
        {
            return x.x != 0 && x.y != 0 && x.z != 0 && x.w != 0;
        }


        /// <summary>Returns true if all components of the input uint2 vector are non-zero, false otherwise.</summary>
        /// <param name="x">Vector of values to compare.</param>
        /// <returns>True if all the components of x are non-zero, false otherwise.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool all(uint2 x)
        {
            return x.x != 0 && x.y != 0;
        }

        /// <summary>Returns true if all components of the input uint3 vector are non-zero, false otherwise.</summary>
        /// <param name="x">Vector of values to compare.</param>
        /// <returns>True if all the components of x are non-zero, false otherwise.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool all(uint3 x)
        {
            return x.x != 0 && x.y != 0 && x.z != 0;
        }

        /// <summary>Returns true if all components of the input uint4 vector are non-zero, false otherwise.</summary>
        /// <param name="x">Vector of values to compare.</param>
        /// <returns>True if all the components of x are non-zero, false otherwise.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool all(uint4 x)
        {
            return x.x != 0 && x.y != 0 && x.z != 0 && x.w != 0;
        }

        /// <summary>Returns true if all components of the input Fp2 vector are non-zero, false otherwise.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool all(Fp2 x)
        {
            return x.x != 0 && x.y != 0;
        }

        /// <summary>Returns true if all components of the input Fp3 vector are non-zero, false otherwise.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool all(Fp3 x)
        {
            return x.x != 0 && x.y != 0 && x.z != 0;
        }

        /// <summary>Returns true if all components of the input Fp4 vector are non-zero, false otherwise.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool all(Fp4 x)
        {
            return x.x != 0 && x.y != 0 && x.z != 0 && x.w != 0;
        }


        /// <summary>Returns b if c is true, a otherwise.</summary>
        /// <param name="a">Value to use if c is false.</param>
        /// <param name="b">Value to use if c is true.</param>
        /// <param name="c">Bool value to choose between a and b.</param>
        /// <returns>The selection between a and b according to bool c.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int select(int a, int b, bool c)
        {
            return c ? b : a;
        }

        /// <summary>Returns b if c is true, a otherwise.</summary>
        /// <param name="a">Value to use if c is false.</param>
        /// <param name="b">Value to use if c is true.</param>
        /// <param name="c">Bool value to choose between a and b.</param>
        /// <returns>The selection between a and b according to bool c.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int2 select(int2 a, int2 b, bool c)
        {
            return c ? b : a;
        }

        /// <summary>Returns b if c is true, a otherwise.</summary>
        /// <param name="a">Value to use if c is false.</param>
        /// <param name="b">Value to use if c is true.</param>
        /// <param name="c">Bool value to choose between a and b.</param>
        /// <returns>The selection between a and b according to bool c.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int3 select(int3 a, int3 b, bool c)
        {
            return c ? b : a;
        }

        /// <summary>Returns b if c is true, a otherwise.</summary>
        /// <param name="a">Value to use if c is false.</param>
        /// <param name="b">Value to use if c is true.</param>
        /// <param name="c">Bool value to choose between a and b.</param>
        /// <returns>The selection between a and b according to bool c.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int4 select(int4 a, int4 b, bool c)
        {
            return c ? b : a;
        }


        /// <summary>
        /// Returns a componentwise selection between two Fp4 vectors a and b based on a bool4 selection mask c.
        /// Per component, the component from b is selected when c is true, otherwise the component from a is selected.
        /// </summary>
        /// <param name="a">Values to use if c is false.</param>
        /// <param name="b">Values to use if c is true.</param>
        /// <param name="c">Selection mask to choose between a and b.</param>
        /// <returns>The componentwise selection between a and b according to selection mask c.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int2 select(int2 a, int2 b, bool2 c)
        {
            return new int2(c.x ? b.x : a.x, c.y ? b.y : a.y);
        }

        /// <summary>
        /// Returns a componentwise selection between two Fp4 vectors a and b based on a bool4 selection mask c.
        /// Per component, the component from b is selected when c is true, otherwise the component from a is selected.
        /// </summary>
        /// <param name="a">Values to use if c is false.</param>
        /// <param name="b">Values to use if c is true.</param>
        /// <param name="c">Selection mask to choose between a and b.</param>
        /// <returns>The componentwise selection between a and b according to selection mask c.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int3 select(int3 a, int3 b, bool3 c)
        {
            return new int3(c.x ? b.x : a.x, c.y ? b.y : a.y, c.z ? b.z : a.z);
        }

        /// <summary>
        /// Returns a componentwise selection between two Fp4 vectors a and b based on a bool4 selection mask c.
        /// Per component, the component from b is selected when c is true, otherwise the component from a is selected.
        /// </summary>
        /// <param name="a">Values to use if c is false.</param>
        /// <param name="b">Values to use if c is true.</param>
        /// <param name="c">Selection mask to choose between a and b.</param>
        /// <returns>The componentwise selection between a and b according to selection mask c.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int4 select(int4 a, int4 b, bool4 c)
        {
            return new int4(c.x ? b.x : a.x, c.y ? b.y : a.y, c.z ? b.z : a.z, c.w ? b.w : a.w);
        }


        /// <summary>Returns b if c is true, a otherwise.</summary>
        /// <param name="a">Value to use if c is false.</param>
        /// <param name="b">Value to use if c is true.</param>
        /// <param name="c">Bool value to choose between a and b.</param>
        /// <returns>The selection between a and b according to bool c.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint select(uint a, uint b, bool c)
        {
            return c ? b : a;
        }

        /// <summary>Returns b if c is true, a otherwise.</summary>
        /// <param name="a">Value to use if c is false.</param>
        /// <param name="b">Value to use if c is true.</param>
        /// <param name="c">Bool value to choose between a and b.</param>
        /// <returns>The selection between a and b according to bool c.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint2 select(uint2 a, uint2 b, bool c)
        {
            return c ? b : a;
        }

        /// <summary>Returns b if c is true, a otherwise.</summary>
        /// <param name="a">Value to use if c is false.</param>
        /// <param name="b">Value to use if c is true.</param>
        /// <param name="c">Bool value to choose between a and b.</param>
        /// <returns>The selection between a and b according to bool c.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint3 select(uint3 a, uint3 b, bool c)
        {
            return c ? b : a;
        }

        /// <summary>Returns b if c is true, a otherwise.</summary>
        /// <param name="a">Value to use if c is false.</param>
        /// <param name="b">Value to use if c is true.</param>
        /// <param name="c">Bool value to choose between a and b.</param>
        /// <returns>The selection between a and b according to bool c.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint4 select(uint4 a, uint4 b, bool c)
        {
            return c ? b : a;
        }


        /// <summary>
        /// Returns a componentwise selection between two Fp4 vectors a and b based on a bool4 selection mask c.
        /// Per component, the component from b is selected when c is true, otherwise the component from a is selected.
        /// </summary>
        /// <param name="a">Values to use if c is false.</param>
        /// <param name="b">Values to use if c is true.</param>
        /// <param name="c">Selection mask to choose between a and b.</param>
        /// <returns>The componentwise selection between a and b according to selection mask c.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint2 select(uint2 a, uint2 b, bool2 c)
        {
            return new uint2(c.x ? b.x : a.x, c.y ? b.y : a.y);
        }

        /// <summary>
        /// Returns a componentwise selection between two Fp4 vectors a and b based on a bool4 selection mask c.
        /// Per component, the component from b is selected when c is true, otherwise the component from a is selected.
        /// </summary>
        /// <param name="a">Values to use if c is false.</param>
        /// <param name="b">Values to use if c is true.</param>
        /// <param name="c">Selection mask to choose between a and b.</param>
        /// <returns>The componentwise selection between a and b according to selection mask c.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint3 select(uint3 a, uint3 b, bool3 c)
        {
            return new uint3(c.x ? b.x : a.x, c.y ? b.y : a.y, c.z ? b.z : a.z);
        }

        /// <summary>
        /// Returns a componentwise selection between two Fp4 vectors a and b based on a bool4 selection mask c.
        /// Per component, the component from b is selected when c is true, otherwise the component from a is selected.
        /// </summary>
        /// <param name="a">Values to use if c is false.</param>
        /// <param name="b">Values to use if c is true.</param>
        /// <param name="c">Selection mask to choose between a and b.</param>
        /// <returns>The componentwise selection between a and b according to selection mask c.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint4 select(uint4 a, uint4 b, bool4 c)
        {
            return new uint4(c.x ? b.x : a.x, c.y ? b.y : a.y, c.z ? b.z : a.z, c.w ? b.w : a.w);
        }


        /// <summary>Returns b if c is true, a otherwise.</summary>
        /// <param name="a">Value to use if c is false.</param>
        /// <param name="b">Value to use if c is true.</param>
        /// <param name="c">Bool value to choose between a and b.</param>
        /// <returns>The selection between a and b according to bool c.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long select(long a, long b, bool c)
        {
            return c ? b : a;
        }

        /// <summary>Returns b if c is true, a otherwise.</summary>
        /// <param name="a">Value to use if c is false.</param>
        /// <param name="b">Value to use if c is true.</param>
        /// <param name="c">Bool value to choose between a and b.</param>
        /// <returns>The selection between a and b according to bool c.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong select(ulong a, ulong b, bool c)
        {
            return c ? b : a;
        }

        /// <summary>Returns b if c is true, a otherwise.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp select(Fp a, Fp b, bool c)
        {
            return c ? b : a;
        }

        /// <summary>Returns b if c is true, a otherwise.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp2 select(Fp2 a, Fp2 b, bool c)
        {
            return c ? b : a;
        }

        /// <summary>Returns b if c is true, a otherwise.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3 select(Fp3 a, Fp3 b, bool c)
        {
            return c ? b : a;
        }

        /// <summary>Returns b if c is true, a otherwise.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4 select(Fp4 a, Fp4 b, bool c)
        {
            return c ? b : a;
        }


        /// <summary>
        /// Returns a componentwise selection between two Fp2 vectors a and b based on a bool2 selection mask c.
        /// Per component, the component from b is selected when c is true, otherwise the component from a is selected.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp2 select(Fp2 a, Fp2 b, bool2 c)
        {
            return new Fp2(c.x ? b.x : a.x, c.y ? b.y : a.y);
        }

        /// <summary>
        /// Returns a componentwise selection between two Fp3 vectors a and b based on a bool3 selection mask c.
        /// Per component, the component from b is selected when c is true, otherwise the component from a is selected.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3 select(Fp3 a, Fp3 b, bool3 c)
        {
            return new Fp3(c.x ? b.x : a.x, c.y ? b.y : a.y, c.z ? b.z : a.z);
        }

        /// <summary>
        /// Returns a componentwise selection between two Fp4 vectors a and b based on a bool4 selection mask c.
        /// Per component, the component from b is selected when c is true, otherwise the component from a is selected.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4 select(Fp4 a, Fp4 b, bool4 c)
        {
            return new Fp4(c.x ? b.x : a.x, c.y ? b.y : a.y, c.z ? b.z : a.z, c.w ? b.w : a.w);
        }


        /// <summary>Computes a step function. Returns 1 when x >= y, 0 otherwise.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp step(Fp y, Fp x)
        {
            return select(0, Fp.One, x >= y);
        }

        /// <summary>Returns the result of a componentwise step function where each component is 1 when x >= y and 0 otherwise.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp2 step(Fp2 y, Fp2 x)
        {
            return select(Fp2(0), Fp2(Fp.One), x >= y);
        }

        /// <summary>Returns the result of a componentwise step function where each component is Fp.One when x >= y and Fp.zero otherwise.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3 step(Fp3 y, Fp3 x)
        {
            return select(Fp3(0), Fp3(Fp.One), x >= y);
        }

        /// <summary>Returns the result of a componentwise step function where each component is Fp.One when x >= y and Fp.zero otherwise.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4 step(Fp4 y, Fp4 x)
        {
            return select(Fp4(0), Fp4(Fp.One), x >= y);
        }


        /// <summary>Given an incident vector i and a normal vector n, returns the reflection vector r = i - 2.0f * dot(i, n) * n.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp2 reflect(Fp2 i, Fp2 n)
        {
            return i - 2 * n * dot(i, n);
        }

        /// <summary>Given an incident vector i and a normal vector n, returns the reflection vector r = i - 2.0f * dot(i, n) * n.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3 reflect(Fp3 i, Fp3 n)
        {
            return i - 2 * n * dot(i, n);
        }

        /// <summary>Given an incident vector i and a normal vector n, returns the reflection vector r = i - 2.0f * dot(i, n) * n.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4 reflect(Fp4 i, Fp4 n)
        {
            return i - 2 * n * dot(i, n);
        }


        /// <summary>Returns the refraction vector given the incident vector i, the normal vector n and the refraction index eta.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp2 refract(Fp2 i, Fp2 n, Fp eta)
        {
            Fp ni = dot(n, i);
            Fp k = Fp.One - eta * eta * (Fp.One - ni * ni);
            return select((Fp)0, eta * i - (eta * ni + sqrt(k)) * n, k >= 0);
        }

        /// <summary>Returns the refraction vector given the incident vector i, the normal vector n and the refraction index eta.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3 refract(Fp3 i, Fp3 n, Fp eta)
        {
            Fp ni = dot(n, i);
            Fp k = Fp.One - eta * eta * (Fp.One - ni * ni);
            return select((Fp)0, eta * i - (eta * ni + sqrt(k)) * n, k >= 0);
        }

        /// <summary>Returns the refraction vector given the incident vector i, the normal vector n and the refraction index eta.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4 refract(Fp4 i, Fp4 n, Fp eta)
        {
            Fp ni = dot(n, i);
            Fp k = Fp.One - eta * eta * (Fp.One - ni * ni);
            return select((Fp)0, eta * i - (eta * ni + sqrt(k)) * n, k >= 0);
        }


        /// <summary>
        /// Conditionally flips a vector n to face in the direction of i. Returns n if dot(i, ng) less than 0, -n otherwise.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp2 faceforward(Fp2 n, Fp2 i, Fp2 ng)
        {
            return select(n, -n, dot(ng, i) >= 0);
        }

        /// <summary>Conditionally flips a vector n to face in the direction of i. Returns n if dot(i, ng) less than 0, -n otherwise.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3 faceforward(Fp3 n, Fp3 i, Fp3 ng)
        {
            return select(n, -n, dot(ng, i) >= 0);
        }

        /// <summary>Conditionally flips a vector n to face in the direction of i. Returns n if dot(i, ng) less than 0, -n otherwise.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4 faceforward(Fp4 n, Fp4 i, Fp4 ng)
        {
            return select(n, -n, dot(ng, i) >= 0);
        }


        /// <summary>Returns the sine and cosine of the input Fp value x through the out parameters s and c.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void sincos(Fp x, out Fp s, out Fp c)
        {
            s = sin(x);
            c = cos(x);
        }

        /// <summary>Returns the componentwise sine and cosine of the input Fp2 vector x through the out parameters s and c.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void sincos(Fp2 x, out Fp2 s, out Fp2 c)
        {
            s = sin(x);
            c = cos(x);
        }

        /// <summary>Returns the componentwise sine and cosine of the input Fp3 vector x through the out parameters s and c.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void sincos(Fp3 x, out Fp3 s, out Fp3 c)
        {
            s = sin(x);
            c = cos(x);
        }

        /// <summary>Returns the componentwise sine and cosine of the input Fp4 vector x through the out parameters s and c.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void sincos(Fp4 x, out Fp4 s, out Fp4 c)
        {
            s = sin(x);
            c = cos(x);
        }

        /// <summary>Returns number of 1-bits in the binary representation of an int value. Also known as the Hamming weight, popcnt on x86, and vcnt on ARM.</summary>
        /// <param name="x">int value in which to count bits set to 1.</param>
        /// <returns>Number of bits set to 1 within x.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int countbits(int x)
        {
            return countbits((uint)x);
        }

        /// <summary>Returns component-wise number of 1-bits in the binary representation of an int2 vector. Also known as the Hamming weight, popcnt on x86, and vcnt on ARM.</summary>
        /// <param name="x">int2 value in which to count bits for each component.</param>
        /// <returns>int2 containing number of bits set to 1 within each component of x.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int2 countbits(int2 x)
        {
            return countbits((uint2)x);
        }

        /// <summary>Returns component-wise number of 1-bits in the binary representation of an int3 vector. Also known as the Hamming weight, popcnt on x86, and vcnt on ARM.</summary>
        /// <param name="x">Number in which to count bits.</param>
        /// <returns>int3 containing number of bits set to 1 within each component of x.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int3 countbits(int3 x)
        {
            return countbits((uint3)x);
        }

        /// <summary>Returns component-wise number of 1-bits in the binary representation of an int4 vector. Also known as the Hamming weight, popcnt on x86, and vcnt on ARM.</summary>
        /// <param name="x">Number in which to count bits.</param>
        /// <returns>int4 containing number of bits set to 1 within each component of x.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int4 countbits(int4 x)
        {
            return countbits((uint4)x);
        }


        /// <summary>Returns number of 1-bits in the binary representation of a uint value. Also known as the Hamming weight, popcnt on x86, and vcnt on ARM.</summary>
        /// <param name="x">Number in which to count bits.</param>
        /// <returns>Number of bits set to 1 within x.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int countbits(uint x)
        {
            x = x - ((x >> 1) & 0x55555555);
            x = (x & 0x33333333) + ((x >> 2) & 0x33333333);
            return (int)((((x + (x >> 4)) & 0x0F0F0F0F) * 0x01010101) >> 24);
        }

        /// <summary>Returns component-wise number of 1-bits in the binary representation of a uint2 vector. Also known as the Hamming weight, popcnt on x86, and vcnt on ARM.</summary>
        /// <param name="x">Number in which to count bits.</param>
        /// <returns>int2 containing number of bits set to 1 within each component of x.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int2 countbits(uint2 x)
        {
            x = x - ((x >> 1) & 0x55555555);
            x = (x & 0x33333333) + ((x >> 2) & 0x33333333);
            return int2((((x + (x >> 4)) & 0x0F0F0F0F) * 0x01010101) >> 24);
        }

        /// <summary>Returns component-wise number of 1-bits in the binary representation of a uint3 vector. Also known as the Hamming weight, popcnt on x86, and vcnt on ARM.</summary>
        /// <param name="x">Number in which to count bits.</param>
        /// <returns>int3 containing number of bits set to 1 within each component of x.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int3 countbits(uint3 x)
        {
            x = x - ((x >> 1) & 0x55555555);
            x = (x & 0x33333333) + ((x >> 2) & 0x33333333);
            return int3((((x + (x >> 4)) & 0x0F0F0F0F) * 0x01010101) >> 24);
        }

        /// <summary>Returns component-wise number of 1-bits in the binary representation of a uint4 vector. Also known as the Hamming weight, popcnt on x86, and vcnt on ARM.</summary>
        /// <param name="x">Number in which to count bits.</param>
        /// <returns>int4 containing number of bits set to 1 within each component of x.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int4 countbits(uint4 x)
        {
            x = x - ((x >> 1) & 0x55555555);
            x = (x & 0x33333333) + ((x >> 2) & 0x33333333);
            return int4((((x + (x >> 4)) & 0x0F0F0F0F) * 0x01010101) >> 24);
        }

        /// <summary>Returns number of 1-bits in the binary representation of a ulong value. Also known as the Hamming weight, popcnt on x86, and vcnt on ARM.</summary>
        /// <param name="x">Number in which to count bits.</param>
        /// <returns>Number of bits set to 1 within x.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int countbits(ulong x)
        {
            x = x - ((x >> 1) & 0x5555555555555555);
            x = (x & 0x3333333333333333) + ((x >> 2) & 0x3333333333333333);
            return (int)((((x + (x >> 4)) & 0x0F0F0F0F0F0F0F0F) * 0x0101010101010101) >> 56);
        }

        /// <summary>Returns number of 1-bits in the binary representation of a long value. Also known as the Hamming weight, popcnt on x86, and vcnt on ARM.</summary>
        /// <param name="x">Number in which to count bits.</param>
        /// <returns>Number of bits set to 1 within x.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int countbits(long x)
        {
            return countbits((ulong)x);
        }


        /// <summary>Returns the componentwise number of leading zeros in the binary representations of an int vector.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The number of leading zeros of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int lzcnt(int x)
        {
            return lzcnt((uint)x);
        }

        /// <summary>Returns the componentwise number of leading zeros in the binary representations of an int2 vector.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The componentwise number of leading zeros of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int2 lzcnt(int2 x)
        {
            return int2(lzcnt(x.x), lzcnt(x.y));
        }

        /// <summary>Returns the componentwise number of leading zeros in the binary representations of an int3 vector.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The componentwise number of leading zeros of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int3 lzcnt(int3 x)
        {
            return int3(lzcnt(x.x), lzcnt(x.y), lzcnt(x.z));
        }

        /// <summary>Returns the componentwise number of leading zeros in the binary representations of an int4 vector.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The componentwise number of leading zeros of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int4 lzcnt(int4 x)
        {
            return int4(lzcnt(x.x), lzcnt(x.y), lzcnt(x.z), lzcnt(x.w));
        }


        /// <summary>Returns number of leading zeros in the binary representations of a uint value.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The number of leading zeros of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int lzcnt(uint x)
        {
            if (x == 0)
                return 32;
            LongDoubleUnion u;
            u.doubleValue = 0.0;
            u.longValue = 0x4330000000000000L + x;
            u.doubleValue -= 4503599627370496.0;
            return 0x41E - (int)(u.longValue >> 52);
        }

        /// <summary>Returns the componentwise number of leading zeros in the binary representations of a uint2 vector.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The componentwise number of leading zeros of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int2 lzcnt(uint2 x)
        {
            return int2(lzcnt(x.x), lzcnt(x.y));
        }

        /// <summary>Returns the componentwise number of leading zeros in the binary representations of a uint3 vector.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The componentwise number of leading zeros of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int3 lzcnt(uint3 x)
        {
            return int3(lzcnt(x.x), lzcnt(x.y), lzcnt(x.z));
        }

        /// <summary>Returns the componentwise number of leading zeros in the binary representations of a uint4 vector.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The componentwise number of leading zeros of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int4 lzcnt(uint4 x)
        {
            return int4(lzcnt(x.x), lzcnt(x.y), lzcnt(x.z), lzcnt(x.w));
        }


        /// <summary>Returns number of leading zeros in the binary representations of a long value.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The number of leading zeros of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int lzcnt(long x)
        {
            return lzcnt((ulong)x);
        }


        /// <summary>Returns number of leading zeros in the binary representations of a ulong value.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The number of leading zeros of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int lzcnt(ulong x)
        {
            if (x == 0)
                return 64;

            uint xh = (uint)(x >> 32);
            uint bits = xh != 0 ? xh : (uint)x;
            int offset = xh != 0 ? 0x41E : 0x43E;

            LongDoubleUnion u;
            u.doubleValue = 0.0;
            u.longValue = 0x4330000000000000L + bits;
            u.doubleValue -= 4503599627370496.0;
            return offset - (int)(u.longValue >> 52);
        }

        /// <summary>
        /// Computes the trailing zero count in the binary representation of the input value.
        /// </summary>
        /// <remarks>
        /// Assuming that the least significant bit is on the right, the integer value 4 has a binary representation
        /// 0100 and the trailing zero count is two. The integer value 1 has a binary representation 0001 and the
        /// trailing zero count is zero.
        /// </remarks>
        /// <param name="x">Input to use when computing the trailing zero count.</param>
        /// <returns>Returns the trailing zero count of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int tzcnt(int x)
        {
            return tzcnt((uint)x);
        }

        /// <summary>
        /// Computes the component-wise trailing zero count in the binary representation of the input value.
        /// </summary>
        /// <remarks>
        /// Assuming that the least significant bit is on the right, the integer value 4 has a binary representation
        /// 0100 and the trailing zero count is two. The integer value 1 has a binary representation 0001 and the
        /// trailing zero count is zero.
        /// </remarks>
        /// <param name="x">Input to use when computing the trailing zero count.</param>
        /// <returns>Returns the component-wise trailing zero count of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int2 tzcnt(int2 x)
        {
            return int2(tzcnt(x.x), tzcnt(x.y));
        }

        /// <summary>
        /// Computes the component-wise trailing zero count in the binary representation of the input value.
        /// </summary>
        /// <remarks>
        /// Assuming that the least significant bit is on the right, the integer value 4 has a binary representation
        /// 0100 and the trailing zero count is two. The integer value 1 has a binary representation 0001 and the
        /// trailing zero count is zero.
        /// </remarks>
        /// <param name="x">Input to use when computing the trailing zero count.</param>
        /// <returns>Returns the component-wise trailing zero count of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int3 tzcnt(int3 x)
        {
            return int3(tzcnt(x.x), tzcnt(x.y), tzcnt(x.z));
        }

        /// <summary>
        /// Computes the component-wise trailing zero count in the binary representation of the input value.
        /// </summary>
        /// <remarks>
        /// Assuming that the least significant bit is on the right, the integer value 4 has a binary representation
        /// 0100 and the trailing zero count is two. The integer value 1 has a binary representation 0001 and the
        /// trailing zero count is zero.
        /// </remarks>
        /// <param name="x">Input to use when computing the trailing zero count.</param>
        /// <returns>Returns the component-wise trailing zero count of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int4 tzcnt(int4 x)
        {
            return int4(tzcnt(x.x), tzcnt(x.y), tzcnt(x.z), tzcnt(x.w));
        }


        /// <summary>
        /// Computes the trailing zero count in the binary representation of the input value.
        /// </summary>
        /// <remarks>
        /// Assuming that the least significant bit is on the right, the integer value 4 has a binary representation
        /// 0100 and the trailing zero count is two. The integer value 1 has a binary representation 0001 and the
        /// trailing zero count is zero.
        /// </remarks>
        /// <param name="x">Input to use when computing the trailing zero count.</param>
        /// <returns>Returns the trailing zero count of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int tzcnt(uint x)
        {
            if (x == 0)
                return 32;

            x &= (uint)-x;
            LongDoubleUnion u;
            u.doubleValue = 0.0;
            u.longValue = 0x4330000000000000L + x;
            u.doubleValue -= 4503599627370496.0;
            return (int)(u.longValue >> 52) - 0x3FF;
        }

        /// <summary>
        /// Computes the component-wise trailing zero count in the binary representation of the input value.
        /// </summary>
        /// <remarks>
        /// Assuming that the least significant bit is on the right, the integer value 4 has a binary representation
        /// 0100 and the trailing zero count is two. The integer value 1 has a binary representation 0001 and the
        /// trailing zero count is zero.
        /// </remarks>
        /// <param name="x">Input to use when computing the trailing zero count.</param>
        /// <returns>Returns the component-wise trailing zero count of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int2 tzcnt(uint2 x)
        {
            return int2(tzcnt(x.x), tzcnt(x.y));
        }

        /// <summary>
        /// Computes the component-wise trailing zero count in the binary representation of the input value.
        /// </summary>
        /// <remarks>
        /// Assuming that the least significant bit is on the right, the integer value 4 has a binary representation
        /// 0100 and the trailing zero count is two. The integer value 1 has a binary representation 0001 and the
        /// trailing zero count is zero.
        /// </remarks>
        /// <param name="x">Input to use when computing the trailing zero count.</param>
        /// <returns>Returns the component-wise trailing zero count of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int3 tzcnt(uint3 x)
        {
            return int3(tzcnt(x.x), tzcnt(x.y), tzcnt(x.z));
        }

        /// <summary>
        /// Computes the component-wise trailing zero count in the binary representation of the input value.
        /// </summary>
        /// <remarks>
        /// Assuming that the least significant bit is on the right, the integer value 4 has a binary representation
        /// 0100 and the trailing zero count is two. The integer value 1 has a binary representation 0001 and the
        /// trailing zero count is zero.
        /// </remarks>
        /// <param name="x">Input to use when computing the trailing zero count.</param>
        /// <returns>Returns the component-wise trailing zero count of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int4 tzcnt(uint4 x)
        {
            return int4(tzcnt(x.x), tzcnt(x.y), tzcnt(x.z), tzcnt(x.w));
        }

        /// <summary>
        /// Computes the trailing zero count in the binary representation of the input value.
        /// </summary>
        /// <remarks>
        /// Assuming that the least significant bit is on the right, the integer value 4 has a binary representation
        /// 0100 and the trailing zero count is two. The integer value 1 has a binary representation 0001 and the
        /// trailing zero count is zero.
        /// </remarks>
        /// <param name="x">Input to use when computing the trailing zero count.</param>
        /// <returns>Returns the trailing zero count of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int tzcnt(long x)
        {
            return tzcnt((ulong)x);
        }

        /// <summary>
        /// Computes the trailing zero count in the binary representation of the input value.
        /// </summary>
        /// <remarks>
        /// Assuming that the least significant bit is on the right, the integer value 4 has a binary representation
        /// 0100 and the trailing zero count is two. The integer value 1 has a binary representation 0001 and the
        /// trailing zero count is zero.
        /// </remarks>
        /// <param name="x">Input to use when computing the trailing zero count.</param>
        /// <returns>Returns the trailing zero count of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int tzcnt(ulong x)
        {
            if (x == 0)
                return 64;

            x = x & (ulong)-(long)x;
            uint xl = (uint)x;

            uint bits = xl != 0 ? xl : (uint)(x >> 32);
            int offset = xl != 0 ? 0x3FF : 0x3DF;

            LongDoubleUnion u;
            u.doubleValue = 0.0;
            u.longValue = 0x4330000000000000L + bits;
            u.doubleValue -= 4503599627370496.0;
            return (int)(u.longValue >> 52) - offset;
        }


        /// <summary>Returns the result of performing a reversal of the bit pattern of an int value.</summary>
        /// <param name="x">Value to reverse.</param>
        /// <returns>Value with reversed bits.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int reversebits(int x)
        {
            return (int)reversebits((uint)x);
        }

        /// <summary>Returns the result of performing a componentwise reversal of the bit pattern of an int2 vector.</summary>
        /// <param name="x">Value to reverse.</param>
        /// <returns>Value with componentwise reversed bits.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int2 reversebits(int2 x)
        {
            return (int2)reversebits((uint2)x);
        }

        /// <summary>Returns the result of performing a componentwise reversal of the bit pattern of an int3 vector.</summary>
        /// <param name="x">Value to reverse.</param>
        /// <returns>Value with componentwise reversed bits.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int3 reversebits(int3 x)
        {
            return (int3)reversebits((uint3)x);
        }

        /// <summary>Returns the result of performing a componentwise reversal of the bit pattern of an int4 vector.</summary>
        /// <param name="x">Value to reverse.</param>
        /// <returns>Value with componentwise reversed bits.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int4 reversebits(int4 x)
        {
            return (int4)reversebits((uint4)x);
        }


        /// <summary>Returns the result of performing a reversal of the bit pattern of a uint value.</summary>
        /// <param name="x">Value to reverse.</param>
        /// <returns>Value with reversed bits.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint reversebits(uint x)
        {
            x = ((x >> 1) & 0x55555555) | ((x & 0x55555555) << 1);
            x = ((x >> 2) & 0x33333333) | ((x & 0x33333333) << 2);
            x = ((x >> 4) & 0x0F0F0F0F) | ((x & 0x0F0F0F0F) << 4);
            x = ((x >> 8) & 0x00FF00FF) | ((x & 0x00FF00FF) << 8);
            return (x >> 16) | (x << 16);
        }

        /// <summary>Returns the result of performing a componentwise reversal of the bit pattern of an uint2 vector.</summary>
        /// <param name="x">Value to reverse.</param>
        /// <returns>Value with componentwise reversed bits.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint2 reversebits(uint2 x)
        {
            x = ((x >> 1) & 0x55555555) | ((x & 0x55555555) << 1);
            x = ((x >> 2) & 0x33333333) | ((x & 0x33333333) << 2);
            x = ((x >> 4) & 0x0F0F0F0F) | ((x & 0x0F0F0F0F) << 4);
            x = ((x >> 8) & 0x00FF00FF) | ((x & 0x00FF00FF) << 8);
            return (x >> 16) | (x << 16);
        }

        /// <summary>Returns the result of performing a componentwise reversal of the bit pattern of an uint3 vector.</summary>
        /// <param name="x">Value to reverse.</param>
        /// <returns>Value with componentwise reversed bits.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint3 reversebits(uint3 x)
        {
            x = ((x >> 1) & 0x55555555) | ((x & 0x55555555) << 1);
            x = ((x >> 2) & 0x33333333) | ((x & 0x33333333) << 2);
            x = ((x >> 4) & 0x0F0F0F0F) | ((x & 0x0F0F0F0F) << 4);
            x = ((x >> 8) & 0x00FF00FF) | ((x & 0x00FF00FF) << 8);
            return (x >> 16) | (x << 16);
        }

        /// <summary>Returns the result of performing a componentwise reversal of the bit pattern of an uint4 vector.</summary>
        /// <param name="x">Value to reverse.</param>
        /// <returns>Value with componentwise reversed bits.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint4 reversebits(uint4 x)
        {
            x = ((x >> 1) & 0x55555555) | ((x & 0x55555555) << 1);
            x = ((x >> 2) & 0x33333333) | ((x & 0x33333333) << 2);
            x = ((x >> 4) & 0x0F0F0F0F) | ((x & 0x0F0F0F0F) << 4);
            x = ((x >> 8) & 0x00FF00FF) | ((x & 0x00FF00FF) << 8);
            return (x >> 16) | (x << 16);
        }


        /// <summary>Returns the result of performing a reversal of the bit pattern of a long value.</summary>
        /// <param name="x">Value to reverse.</param>
        /// <returns>Value with reversed bits.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long reversebits(long x)
        {
            return (long)reversebits((ulong)x);
        }


        /// <summary>Returns the result of performing a reversal of the bit pattern of a ulong value.</summary>
        /// <param name="x">Value to reverse.</param>
        /// <returns>Value with reversed bits.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong reversebits(ulong x)
        {
            x = ((x >> 1) & 0x5555555555555555ul) | ((x & 0x5555555555555555ul) << 1);
            x = ((x >> 2) & 0x3333333333333333ul) | ((x & 0x3333333333333333ul) << 2);
            x = ((x >> 4) & 0x0F0F0F0F0F0F0F0Ful) | ((x & 0x0F0F0F0F0F0F0F0Ful) << 4);
            x = ((x >> 8) & 0x00FF00FF00FF00FFul) | ((x & 0x00FF00FF00FF00FFul) << 8);
            x = ((x >> 16) & 0x0000FFFF0000FFFFul) | ((x & 0x0000FFFF0000FFFFul) << 16);
            return (x >> 32) | (x << 32);
        }


        /// <summary>Returns the result of rotating the bits of an int left by bits n.</summary>
        /// <param name="x">Value to rotate.</param>
        /// <param name="n">Number of bits to rotate.</param>
        /// <returns>The rotated value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int rol(int x, int n)
        {
            return (int)rol((uint)x, n);
        }

        /// <summary>Returns the componentwise result of rotating the bits of an int2 left by bits n.</summary>
        /// <param name="x">Value to rotate.</param>
        /// <param name="n">Number of bits to rotate.</param>
        /// <returns>The componentwise rotated value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int2 rol(int2 x, int n)
        {
            return (int2)rol((uint2)x, n);
        }

        /// <summary>Returns the componentwise result of rotating the bits of an int3 left by bits n.</summary>
        /// <param name="x">Value to rotate.</param>
        /// <param name="n">Number of bits to rotate.</param>
        /// <returns>The componentwise rotated value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int3 rol(int3 x, int n)
        {
            return (int3)rol((uint3)x, n);
        }

        /// <summary>Returns the componentwise result of rotating the bits of an int4 left by bits n.</summary>
        /// <param name="x">Value to rotate.</param>
        /// <param name="n">Number of bits to rotate.</param>
        /// <returns>The componentwise rotated value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int4 rol(int4 x, int n)
        {
            return (int4)rol((uint4)x, n);
        }


        /// <summary>Returns the result of rotating the bits of a uint left by bits n.</summary>
        /// <param name="x">Value to rotate.</param>
        /// <param name="n">Number of bits to rotate.</param>
        /// <returns>The rotated value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint rol(uint x, int n)
        {
            return (x << n) | (x >> (32 - n));
        }

        /// <summary>Returns the componentwise result of rotating the bits of a uint2 left by bits n.</summary>
        /// <param name="x">Value to rotate.</param>
        /// <param name="n">Number of bits to rotate.</param>
        /// <returns>The componentwise rotated value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint2 rol(uint2 x, int n)
        {
            return (x << n) | (x >> (32 - n));
        }

        /// <summary>Returns the componentwise result of rotating the bits of a uint3 left by bits n.</summary>
        /// <param name="x">Value to rotate.</param>
        /// <param name="n">Number of bits to rotate.</param>
        /// <returns>The componentwise rotated value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint3 rol(uint3 x, int n)
        {
            return (x << n) | (x >> (32 - n));
        }

        /// <summary>Returns the componentwise result of rotating the bits of a uint4 left by bits n.</summary>
        /// <param name="x">Value to rotate.</param>
        /// <param name="n">Number of bits to rotate.</param>
        /// <returns>The componentwise rotated value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint4 rol(uint4 x, int n)
        {
            return (x << n) | (x >> (32 - n));
        }


        /// <summary>Returns the result of rotating the bits of a long left by bits n.</summary>
        /// <param name="x">Value to rotate.</param>
        /// <param name="n">Number of bits to rotate.</param>
        /// <returns>The rotated value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long rol(long x, int n)
        {
            return (long)rol((ulong)x, n);
        }


        /// <summary>Returns the result of rotating the bits of a ulong left by bits n.</summary>
        /// <param name="x">Value to rotate.</param>
        /// <param name="n">Number of bits to rotate.</param>
        /// <returns>The rotated value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong rol(ulong x, int n)
        {
            return (x << n) | (x >> (64 - n));
        }


        /// <summary>Returns the result of rotating the bits of an int right by bits n.</summary>
        /// <param name="x">Value to rotate.</param>
        /// <param name="n">Number of bits to rotate.</param>
        /// <returns>The rotated value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ror(int x, int n)
        {
            return (int)ror((uint)x, n);
        }

        /// <summary>Returns the componentwise result of rotating the bits of an int2 right by bits n.</summary>
        /// <param name="x">Value to rotate.</param>
        /// <param name="n">Number of bits to rotate.</param>
        /// <returns>The componentwise rotated value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int2 ror(int2 x, int n)
        {
            return (int2)ror((uint2)x, n);
        }

        /// <summary>Returns the componentwise result of rotating the bits of an int3 right by bits n.</summary>
        /// <param name="x">Value to rotate.</param>
        /// <param name="n">Number of bits to rotate.</param>
        /// <returns>The componentwise rotated value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int3 ror(int3 x, int n)
        {
            return (int3)ror((uint3)x, n);
        }

        /// <summary>Returns the componentwise result of rotating the bits of an int4 right by bits n.</summary>
        /// <param name="x">Value to rotate.</param>
        /// <param name="n">Number of bits to rotate.</param>
        /// <returns>The componentwise rotated value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int4 ror(int4 x, int n)
        {
            return (int4)ror((uint4)x, n);
        }


        /// <summary>Returns the result of rotating the bits of a uint right by bits n.</summary>
        /// <param name="x">Value to rotate.</param>
        /// <param name="n">Number of bits to rotate.</param>
        /// <returns>The rotated value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint ror(uint x, int n)
        {
            return (x >> n) | (x << (32 - n));
        }

        /// <summary>Returns the componentwise result of rotating the bits of a uint2 right by bits n.</summary>
        /// <param name="x">Value to rotate.</param>
        /// <param name="n">Number of bits to rotate.</param>
        /// <returns>The componentwise rotated value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint2 ror(uint2 x, int n)
        {
            return (x >> n) | (x << (32 - n));
        }

        /// <summary>Returns the componentwise result of rotating the bits of a uint3 right by bits n.</summary>
        /// <param name="x">Value to rotate.</param>
        /// <param name="n">Number of bits to rotate.</param>
        /// <returns>The componentwise rotated value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint3 ror(uint3 x, int n)
        {
            return (x >> n) | (x << (32 - n));
        }

        /// <summary>Returns the componentwise result of rotating the bits of a uint4 right by bits n.</summary>
        /// <param name="x">Value to rotate.</param>
        /// <param name="n">Number of bits to rotate.</param>
        /// <returns>The componentwise rotated value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint4 ror(uint4 x, int n)
        {
            return (x >> n) | (x << (32 - n));
        }


        /// <summary>Returns the result of rotating the bits of a long right by bits n.</summary>
        /// <param name="x">Value to rotate.</param>
        /// <param name="n">Number of bits to rotate.</param>
        /// <returns>The rotated value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long ror(long x, int n)
        {
            return (long)ror((ulong)x, n);
        }


        /// <summary>Returns the result of rotating the bits of a ulong right by bits n.</summary>
        /// <param name="x">Value to rotate.</param>
        /// <param name="n">Number of bits to rotate.</param>
        /// <returns>The rotated value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong ror(ulong x, int n)
        {
            return (x >> n) | (x << (64 - n));
        }


        /// <summary>Returns the smallest power of two greater than or equal to the input.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The smallest power of two greater than or equal to the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ceilpow2(int x)
        {
            x -= 1;
            x |= x >> 1;
            x |= x >> 2;
            x |= x >> 4;
            x |= x >> 8;
            x |= x >> 16;
            return x + 1;
        }

        /// <summary>Returns the result of a componentwise calculation of the smallest power of two greater than or equal to the input.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The componentwise smallest power of two greater than or equal to the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int2 ceilpow2(int2 x)
        {
            x -= 1;
            x |= x >> 1;
            x |= x >> 2;
            x |= x >> 4;
            x |= x >> 8;
            x |= x >> 16;
            return x + 1;
        }

        /// <summary>Returns the result of a componentwise calculation of the smallest power of two greater than or equal to the input.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The componentwise smallest power of two greater than or equal to the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int3 ceilpow2(int3 x)
        {
            x -= 1;
            x |= x >> 1;
            x |= x >> 2;
            x |= x >> 4;
            x |= x >> 8;
            x |= x >> 16;
            return x + 1;
        }

        /// <summary>Returns the result of a componentwise calculation of the smallest power of two greater than or equal to the input.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The componentwise smallest power of two greater than or equal to the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int4 ceilpow2(int4 x)
        {
            x -= 1;
            x |= x >> 1;
            x |= x >> 2;
            x |= x >> 4;
            x |= x >> 8;
            x |= x >> 16;
            return x + 1;
        }


        /// <summary>Returns the smallest power of two greater than or equal to the input.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The smallest power of two greater than or equal to the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint ceilpow2(uint x)
        {
            x -= 1;
            x |= x >> 1;
            x |= x >> 2;
            x |= x >> 4;
            x |= x >> 8;
            x |= x >> 16;
            return x + 1;
        }

        /// <summary>Returns the result of a componentwise calculation of the smallest power of two greater than or equal to the input.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The componentwise smallest power of two greater than or equal to the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint2 ceilpow2(uint2 x)
        {
            x -= 1;
            x |= x >> 1;
            x |= x >> 2;
            x |= x >> 4;
            x |= x >> 8;
            x |= x >> 16;
            return x + 1;
        }

        /// <summary>Returns the result of a componentwise calculation of the smallest power of two greater than or equal to the input.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The componentwise smallest power of two greater than or equal to the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint3 ceilpow2(uint3 x)
        {
            x -= 1;
            x |= x >> 1;
            x |= x >> 2;
            x |= x >> 4;
            x |= x >> 8;
            x |= x >> 16;
            return x + 1;
        }

        /// <summary>Returns the result of a componentwise calculation of the smallest power of two greater than or equal to the input.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The componentwise smallest power of two greater than or equal to the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint4 ceilpow2(uint4 x)
        {
            x -= 1;
            x |= x >> 1;
            x |= x >> 2;
            x |= x >> 4;
            x |= x >> 8;
            x |= x >> 16;
            return x + 1;
        }


        /// <summary>Returns the smallest power of two greater than or equal to the input.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The smallest power of two greater than or equal to the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long ceilpow2(long x)
        {
            x -= 1;
            x |= x >> 1;
            x |= x >> 2;
            x |= x >> 4;
            x |= x >> 8;
            x |= x >> 16;
            x |= x >> 32;
            return x + 1;
        }


        /// <summary>Returns the smallest power of two greater than or equal to the input.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The smallest power of two greater than or equal to the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong ceilpow2(ulong x)
        {
            x -= 1;
            x |= x >> 1;
            x |= x >> 2;
            x |= x >> 4;
            x |= x >> 8;
            x |= x >> 16;
            x |= x >> 32;
            return x + 1;
        }

        /// <summary>
        /// Computes the ceiling of the base-2 logarithm of x.
        /// </summary>
        /// <remarks>
        /// x must be greater than 0, otherwise the result is undefined.
        /// </remarks>
        /// <param name="x">Integer to be used as input.</param>
        /// <returns>Ceiling of the base-2 logarithm of x, as an integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ceillog2(int x)
        {
            return 32 - lzcnt((uint)x - 1);
        }

        /// <summary>
        /// Computes the componentwise ceiling of the base-2 logarithm of x.
        /// </summary>
        /// <remarks>
        /// Components of x must be greater than 0, otherwise the result for that component is undefined.
        /// </remarks>
        /// <param name="x">int2 to be used as input.</param>
        /// <returns>Componentwise ceiling of the base-2 logarithm of x.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int2 ceillog2(int2 x)
        {
            return new int2(ceillog2(x.x), ceillog2(x.y));
        }

        /// <summary>
        /// Computes the componentwise ceiling of the base-2 logarithm of x.
        /// </summary>
        /// <remarks>
        /// Components of x must be greater than 0, otherwise the result for that component is undefined.
        /// </remarks>
        /// <param name="x">int3 to be used as input.</param>
        /// <returns>Componentwise ceiling of the base-2 logarithm of x.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int3 ceillog2(int3 x)
        {
            return new int3(ceillog2(x.x), ceillog2(x.y), ceillog2(x.z));
        }

        /// <summary>
        /// Computes the componentwise ceiling of the base-2 logarithm of x.
        /// </summary>
        /// <remarks>
        /// Components of x must be greater than 0, otherwise the result for that component is undefined.
        /// </remarks>
        /// <param name="x">int4 to be used as input.</param>
        /// <returns>Componentwise ceiling of the base-2 logarithm of x.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int4 ceillog2(int4 x)
        {
            return new int4(ceillog2(x.x), ceillog2(x.y), ceillog2(x.z), ceillog2(x.w));
        }

        /// <summary>
        /// Computes the ceiling of the base-2 logarithm of x.
        /// </summary>
        /// <remarks>
        /// x must be greater than 0, otherwise the result is undefined.
        /// </remarks>
        /// <param name="x">Unsigned integer to be used as input.</param>
        /// <returns>Ceiling of the base-2 logarithm of x, as an integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ceillog2(uint x)
        {
            return 32 - lzcnt(x - 1);
        }

        /// <summary>
        /// Computes the componentwise ceiling of the base-2 logarithm of x.
        /// </summary>
        /// <remarks>
        /// Components of x must be greater than 0, otherwise the result for that component is undefined.
        /// </remarks>
        /// <param name="x">uint2 to be used as input.</param>
        /// <returns>Componentwise ceiling of the base-2 logarithm of x.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int2 ceillog2(uint2 x)
        {
            return new int2(ceillog2(x.x), ceillog2(x.y));
        }

        /// <summary>
        /// Computes the componentwise ceiling of the base-2 logarithm of x.
        /// </summary>
        /// <remarks>
        /// Components of x must be greater than 0, otherwise the result for that component is undefined.
        /// </remarks>
        /// <param name="x">uint3 to be used as input.</param>
        /// <returns>Componentwise ceiling of the base-2 logarithm of x.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int3 ceillog2(uint3 x)
        {
            return new int3(ceillog2(x.x), ceillog2(x.y), ceillog2(x.z));
        }

        /// <summary>
        /// Computes the componentwise ceiling of the base-2 logarithm of x.
        /// </summary>
        /// <remarks>
        /// Components of x must be greater than 0, otherwise the result for that component is undefined.
        /// </remarks>
        /// <param name="x">uint4 to be used as input.</param>
        /// <returns>Componentwise ceiling of the base-2 logarithm of x.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int4 ceillog2(uint4 x)
        {
            return new int4(ceillog2(x.x), ceillog2(x.y), ceillog2(x.z), ceillog2(x.w));
        }

        /// <summary>
        /// Computes the floor of the base-2 logarithm of x.
        /// </summary>
        /// <remarks>x must be greater than zero, otherwise the result is undefined.</remarks>
        /// <param name="x">Integer to be used as input.</param>
        /// <returns>Floor of base-2 logarithm of x.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int floorlog2(int x)
        {
            return 31 - lzcnt((uint)x);
        }

        /// <summary>
        /// Computes the componentwise floor of the base-2 logarithm of x.
        /// </summary>
        /// <remarks>Components of x must be greater than zero, otherwise the result of the component is undefined.</remarks>
        /// <param name="x">int2 to be used as input.</param>
        /// <returns>Componentwise floor of base-2 logarithm of x.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int2 floorlog2(int2 x)
        {
            return new int2(floorlog2(x.x), floorlog2(x.y));
        }

        /// <summary>
        /// Computes the componentwise floor of the base-2 logarithm of x.
        /// </summary>
        /// <remarks>Components of x must be greater than zero, otherwise the result of the component is undefined.</remarks>
        /// <param name="x">int3 to be used as input.</param>
        /// <returns>Componentwise floor of base-2 logarithm of x.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int3 floorlog2(int3 x)
        {
            return new int3(floorlog2(x.x), floorlog2(x.y), floorlog2(x.z));
        }

        /// <summary>
        /// Computes the componentwise floor of the base-2 logarithm of x.
        /// </summary>
        /// <remarks>Components of x must be greater than zero, otherwise the result of the component is undefined.</remarks>
        /// <param name="x">int4 to be used as input.</param>
        /// <returns>Componentwise floor of base-2 logarithm of x.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int4 floorlog2(int4 x)
        {
            return new int4(floorlog2(x.x), floorlog2(x.y), floorlog2(x.z), floorlog2(x.w));
        }

        /// <summary>
        /// Computes the floor of the base-2 logarithm of x.
        /// </summary>
        /// <remarks>x must be greater than zero, otherwise the result is undefined.</remarks>
        /// <param name="x">Unsigned integer to be used as input.</param>
        /// <returns>Floor of base-2 logarithm of x.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int floorlog2(uint x)
        {
            return 31 - lzcnt(x);
        }

        /// <summary>
        /// Computes the componentwise floor of the base-2 logarithm of x.
        /// </summary>
        /// <remarks>Components of x must be greater than zero, otherwise the result of the component is undefined.</remarks>
        /// <param name="x">uint2 to be used as input.</param>
        /// <returns>Componentwise floor of base-2 logarithm of x.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int2 floorlog2(uint2 x)
        {
            return new int2(floorlog2(x.x), floorlog2(x.y));
        }

        /// <summary>
        /// Computes the componentwise floor of the base-2 logarithm of x.
        /// </summary>
        /// <remarks>Components of x must be greater than zero, otherwise the result of the component is undefined.</remarks>
        /// <param name="x">uint3 to be used as input.</param>
        /// <returns>Componentwise floor of base-2 logarithm of x.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int3 floorlog2(uint3 x)
        {
            return new int3(floorlog2(x.x), floorlog2(x.y), floorlog2(x.z));
        }

        /// <summary>
        /// Computes the componentwise floor of the base-2 logarithm of x.
        /// </summary>
        /// <remarks>Components of x must be greater than zero, otherwise the result of the component is undefined.</remarks>
        /// <param name="x">uint4 to be used as input.</param>
        /// <returns>Componentwise floor of base-2 logarithm of x.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int4 floorlog2(uint4 x)
        {
            return new int4(floorlog2(x.x), floorlog2(x.y), floorlog2(x.z), floorlog2(x.w));
        }

        /// <summary>Returns the result of converting a Fp value from degrees to radians.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp radians(Fp x)
        {
            return x * Fp.PiOver180;
        }

        /// <summary>Returns the result of a componentwise conversion of a Fp2 vector from degrees to radians.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp2 radians(Fp2 x)
        {
            return x * Fp.PiOver180;
        }

        /// <summary>Returns the result of a componentwise conversion of a Fp3 vector from degrees to radians.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3 radians(Fp3 x)
        {
            return x * Fp.PiOver180;
        }

        /// <summary>Returns the result of a componentwise conversion of a Fp4 vector from degrees to radians.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4 radians(Fp4 x)
        {
            return x * Fp.PiOver180;
        }


        /// <summary>Returns the result of converting a Fp value from radians to degrees.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp degrees(Fp x)
        {
            return x * Fp.PiOver180Inverse;
        }

        /// <summary>Returns the result of a componentwise conversion of a Fp2 vector from radians to degrees.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp2 degrees(Fp2 x)
        {
            return x * Fp.PiOver180Inverse;
        }

        /// <summary>Returns the result of a componentwise conversion of a Fp3 vector from radians to degrees.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3 degrees(Fp3 x)
        {
            return x * Fp.PiOver180Inverse;
        }

        /// <summary>Returns the result of a componentwise conversion of a Fp4 vector from radians to degrees.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4 degrees(Fp4 x)
        {
            return x * Fp.PiOver180Inverse;
        }

        /// <summary>Returns the minimum component of an int2 vector.</summary>
        /// <param name="x">The vector to use when computing the minimum component.</param>
        /// <returns>The value of the minimum component of the vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int cmin(int2 x)
        {
            return min(x.x, x.y);
        }

        /// <summary>Returns the minimum component of an int3 vector.</summary>
        /// <param name="x">The vector to use when computing the minimum component.</param>
        /// <returns>The value of the minimum component of the vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int cmin(int3 x)
        {
            return min(min(x.x, x.y), x.z);
        }

        /// <summary>Returns the minimum component of an int4 vector.</summary>
        /// <param name="x">The vector to use when computing the minimum component.</param>
        /// <returns>The value of the minimum component of the vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int cmin(int4 x)
        {
            return min(min(x.x, x.y), min(x.z, x.w));
        }


        /// <summary>Returns the minimum component of a uint2 vector.</summary>
        /// <param name="x">The vector to use when computing the minimum component.</param>
        /// <returns>The value of the minimum component of the vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint cmin(uint2 x)
        {
            return min(x.x, x.y);
        }

        /// <summary>Returns the minimum component of a uint3 vector.</summary>
        /// <param name="x">The vector to use when computing the minimum component.</param>
        /// <returns>The value of the minimum component of the vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint cmin(uint3 x)
        {
            return min(min(x.x, x.y), x.z);
        }

        /// <summary>Returns the minimum component of a uint4 vector.</summary>
        /// <param name="x">The vector to use when computing the minimum component.</param>
        /// <returns>The value of the minimum component of the vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint cmin(uint4 x)
        {
            return min(min(x.x, x.y), min(x.z, x.w));
        }


        /// <summary>Returns the minimum component of a Fp2 vector.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp cmin(Fp2 x)
        {
            return min(x.x, x.y);
        }

        /// <summary>Returns the minimum component of a Fp3 vector.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp cmin(Fp3 x)
        {
            return min(min(x.x, x.y), x.z);
        }

        /// <summary>Returns the maximum component of a Fp3 vector.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp cmin(Fp4 x)
        {
            return min(min(x.x, x.y), min(x.z, x.w));
        }


        /// <summary>Returns the maximum component of an int2 vector.</summary>
        /// <param name="x">The vector to use when computing the maximum component.</param>
        /// <returns>The value of the maximum component of the vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int cmax(int2 x)
        {
            return max(x.x, x.y);
        }

        /// <summary>Returns the maximum component of an int3 vector.</summary>
        /// <param name="x">The vector to use when computing the maximum component.</param>
        /// <returns>The value of the maximum component of the vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int cmax(int3 x)
        {
            return max(max(x.x, x.y), x.z);
        }

        /// <summary>Returns the maximum component of an int4 vector.</summary>
        /// <param name="x">The vector to use when computing the maximum component.</param>
        /// <returns>The value of the maximum component of the vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int cmax(int4 x)
        {
            return max(max(x.x, x.y), max(x.z, x.w));
        }


        /// <summary>Returns the maximum component of a uint2 vector.</summary>
        /// <param name="x">The vector to use when computing the maximum component.</param>
        /// <returns>The value of the maximum component of the vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint cmax(uint2 x)
        {
            return max(x.x, x.y);
        }

        /// <summary>Returns the maximum component of a uint3 vector.</summary>
        /// <param name="x">The vector to use when computing the maximum component.</param>
        /// <returns>The value of the maximum component of the vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint cmax(uint3 x)
        {
            return max(max(x.x, x.y), x.z);
        }

        /// <summary>Returns the maximum component of a uint4 vector.</summary>
        /// <param name="x">The vector to use when computing the maximum component.</param>
        /// <returns>The value of the maximum component of the vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint cmax(uint4 x)
        {
            return max(max(x.x, x.y), max(x.z, x.w));
        }

        /// <summary>Returns the maximum component of a Fp2 vector.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp cmax(Fp2 x)
        {
            return max(x.x, x.y);
        }

        /// <summary>Returns the maximum component of a Fp3 vector.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp cmax(Fp3 x)
        {
            return max(max(x.x, x.y), x.z);
        }

        /// <summary>Returns the maximum component of a Fp4 vector.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp cmax(Fp4 x)
        {
            return max(max(x.x, x.y), max(x.z, x.w));
        }


        /// <summary>Returns the horizontal sum of components of an int2 vector.</summary>
        /// <param name="x">The vector to use when computing the horizontal sum.</param>
        /// <returns>The horizontal sum of of components of the vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int csum(int2 x)
        {
            return x.x + x.y;
        }

        /// <summary>Returns the horizontal sum of components of an int3 vector.</summary>
        /// <param name="x">The vector to use when computing the horizontal sum.</param>
        /// <returns>The horizontal sum of of components of the vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int csum(int3 x)
        {
            return x.x + x.y + x.z;
        }

        /// <summary>Returns the horizontal sum of components of an int4 vector.</summary>
        /// <param name="x">The vector to use when computing the horizontal sum.</param>
        /// <returns>The horizontal sum of of components of the vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int csum(int4 x)
        {
            return x.x + x.y + x.z + x.w;
        }


        /// <summary>Returns the horizontal sum of components of a uint2 vector.</summary>
        /// <param name="x">The vector to use when computing the horizontal sum.</param>
        /// <returns>The horizontal sum of of components of the vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint csum(uint2 x)
        {
            return x.x + x.y;
        }

        /// <summary>Returns the horizontal sum of components of a uint3 vector.</summary>
        /// <param name="x">The vector to use when computing the horizontal sum.</param>
        /// <returns>The horizontal sum of of components of the vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint csum(uint3 x)
        {
            return x.x + x.y + x.z;
        }

        /// <summary>Returns the horizontal sum of components of a uint4 vector.</summary>
        /// <param name="x">The vector to use when computing the horizontal sum.</param>
        /// <returns>The horizontal sum of of components of the vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint csum(uint4 x)
        {
            return x.x + x.y + x.z + x.w;
        }

        /// <summary>Returns the horizontal sum of components of a Fp2 vector.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp csum(Fp2 x)
        {
            return x.x + x.y;
        }

        /// <summary>Returns the horizontal sum of components of a Fp3 vector.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp csum(Fp3 x)
        {
            return x.x + x.y + x.z;
        }

        /// <summary>Returns the horizontal sum of components of a Fp4 vector.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp csum(Fp4 x)
        {
            return (x.x + x.y) + (x.z + x.w);
        }

        /// <summary>
        /// Packs components with an enabled mask to the left.
        /// </summary>
        /// <remarks>
        /// This function is also known as left packing. The effect of this function is to filter out components that
        /// are not enabled and leave an output buffer tightly packed with only the enabled components. A common use
        /// case is if you perform intersection tests on arrays of data in structure of arrays (SoA) form and need to
        /// produce an output array of the things that intersected.
        /// </remarks>
        /// <param name="output">Pointer to packed output array where enabled components should be stored to.</param>
        /// <param name="index">Index into output array where first enabled component should be stored to.</param>
        /// <param name="val">The value to to compress.</param>
        /// <param name="mask">Mask indicating which components are enabled.</param>
        /// <returns>Index to element after the last one stored.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe int compress(int* output, int index, int4 val, bool4 mask)
        {
            if (mask.x)
                output[index++] = val.x;
            if (mask.y)
                output[index++] = val.y;
            if (mask.z)
                output[index++] = val.z;
            if (mask.w)
                output[index++] = val.w;

            return index;
        }

        /// <summary>
        /// Packs components with an enabled mask to the left.
        /// </summary>
        /// <remarks>
        /// This function is also known as left packing. The effect of this function is to filter out components that
        /// are not enabled and leave an output buffer tightly packed with only the enabled components. A common use
        /// case is if you perform intersection tests on arrays of data in structure of arrays (SoA) form and need to
        /// produce an output array of the things that intersected.
        /// </remarks>
        /// <param name="output">Pointer to packed output array where enabled components should be stored to.</param>
        /// <param name="index">Index into output array where first enabled component should be stored to.</param>
        /// <param name="val">The value to to compress.</param>
        /// <param name="mask">Mask indicating which components are enabled.</param>
        /// <returns>Index to element after the last one stored.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe int compress(uint* output, int index, uint4 val, bool4 mask)
        {
            return compress((int*)output, index, *(int4*)&val, mask);
        }

        /// <summary>Returns a uint hash from a block of memory using the xxhash32 algorithm. Can only be used in an unsafe context.</summary>
        /// <param name="pBuffer">A pointer to the beginning of the data.</param>
        /// <param name="numBytes">Number of bytes to hash.</param>
        /// <param name="seed">Starting seed value.</param>
        /// <returns>The 32 bit hash of the input data buffer.</returns>
        public static unsafe uint hash(void* pBuffer, int numBytes, uint seed = 0)
        {
            unchecked
            {
                const uint Prime1 = 2654435761;
                const uint Prime2 = 2246822519;
                const uint Prime3 = 3266489917;
                const uint Prime4 = 668265263;
                const uint Prime5 = 374761393;

                uint4* p = (uint4*)pBuffer;
                uint hash = seed + Prime5;
                if (numBytes >= 16)
                {
                    uint4 state = new uint4(Prime1 + Prime2, Prime2, 0, (uint)-Prime1) + seed;

                    int count = numBytes >> 4;
                    for (int i = 0; i < count; ++i)
                    {
                        state += *p++ * Prime2;
                        state = (state << 13) | (state >> 19);
                        state *= Prime1;
                    }

                    hash = rol(state.x, 1) + rol(state.y, 7) + rol(state.z, 12) + rol(state.w, 18);
                }

                hash += (uint)numBytes;

                uint* puint = (uint*)p;
                for (int i = 0; i < ((numBytes >> 2) & 3); ++i)
                {
                    hash += *puint++ * Prime3;
                    hash = rol(hash, 17) * Prime4;
                }

                byte* pbyte = (byte*)puint;
                for (int i = 0; i < ((numBytes) & 3); ++i)
                {
                    hash += (*pbyte++) * Prime5;
                    hash = rol(hash, 11) * Prime1;
                }

                hash ^= hash >> 15;
                hash *= Prime2;
                hash ^= hash >> 13;
                hash *= Prime3;
                hash ^= hash >> 16;

                return hash;
            }
        }

        /// <summary>
        /// Unity's up axis (0, 1, 0).
        /// </summary>
        /// <remarks>Matches [https://docs.unity3d.com/ScriptReference/Vector3-up.html](https://docs.unity3d.com/ScriptReference/Vector3-up.html)</remarks>
        /// <returns>The up axis.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3 up()
        {
            return new Fp3(0, 1, 0);
        } // for compatibility

        /// <summary>
        /// Unity's down axis (0, -1, 0).
        /// </summary>
        /// <remarks>Matches [https://docs.unity3d.com/ScriptReference/Vector3-down.html](https://docs.unity3d.com/ScriptReference/Vector3-down.html)</remarks>
        /// <returns>The down axis.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3 down()
        {
            return new Fp3(0, -1, 0);
        }

        /// <summary>
        /// Unity's forward axis (0, 0, 1).
        /// </summary>
        /// <remarks>Matches [https://docs.unity3d.com/ScriptReference/Vector3-forward.html](https://docs.unity3d.com/ScriptReference/Vector3-forward.html)</remarks>
        /// <returns>The forward axis.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3 forward()
        {
            return new Fp3(0, 0, 1);
        }

        /// <summary>
        /// Unity's back axis (0, 0, -1).
        /// </summary>
        /// <remarks>Matches [https://docs.unity3d.com/ScriptReference/Vector3-back.html](https://docs.unity3d.com/ScriptReference/Vector3-back.html)</remarks>
        /// <returns>The back axis.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3 back()
        {
            return new Fp3(0, 0, -1);
        }

        /// <summary>
        /// Unity's left axis (-1, 0, 0).
        /// </summary>
        /// <remarks>Matches [https://docs.unity3d.com/ScriptReference/Vector3-left.html](https://docs.unity3d.com/ScriptReference/Vector3-left.html)</remarks>
        /// <returns>The left axis.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3 left()
        {
            return new Fp3(-1, 0, 0);
        }

        /// <summary>
        /// Unity's right axis (1, 0, 0).
        /// </summary>
        /// <remarks>Matches [https://docs.unity3d.com/ScriptReference/Vector3-right.html](https://docs.unity3d.com/ScriptReference/Vector3-right.html)</remarks>
        /// <returns>The right axis.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3 right()
        {
            return new Fp3(1, 0, 0);
        }

        // SSE shuffles
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Fp4 unpacklo(Fp4 a, Fp4 b)
        {
            return shuffle(a, b, ShuffleComponent.LeftX, ShuffleComponent.RightX, ShuffleComponent.LeftY,
                ShuffleComponent.RightY);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Fp4 unpackhi(Fp4 a, Fp4 b)
        {
            return shuffle(a, b, ShuffleComponent.LeftZ, ShuffleComponent.RightZ, ShuffleComponent.LeftW,
                ShuffleComponent.RightW);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Fp4 movelh(Fp4 a, Fp4 b)
        {
            return shuffle(a, b, ShuffleComponent.LeftX, ShuffleComponent.LeftY, ShuffleComponent.RightX,
                ShuffleComponent.RightY);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Fp4 movehl(Fp4 a, Fp4 b)
        {
            return shuffle(b, a, ShuffleComponent.LeftZ, ShuffleComponent.LeftW, ShuffleComponent.RightZ,
                ShuffleComponent.RightW);
        }

        public static Fp radius2Degree(Fp radius)
        {
            return radius * Fp.PiOver180Inverse;
        }

        public static Fp degree2Radius(Fp degree)
        {
            return degree * Fp.PiOver180;
        }

        [StructLayout(LayoutKind.Explicit)]
        internal struct IntFloatUnion
        {
            [FieldOffset(0)] public int intValue;
            [FieldOffset(0)] public float floatValue;
        }

        [StructLayout(LayoutKind.Explicit)]
        internal struct LongDoubleUnion
        {
            [FieldOffset(0)] public long longValue;
            [FieldOffset(0)] public double doubleValue;
        }

        #region Matrix

        /// <summary>
        /// Extracts a Fp3x3 from the upper left 3x3 of a Fp4x4.
        /// </summary>
        /// <param name="f4x4"> to extract a Fp3x3 from.</param>
        /// <returns>Upper left 3x3 matrix as Fp3x3.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3x3 Fp3x3(Fp4x4 f4x4)
        {
            return new Fp3x3(f4x4);
        }

        /// <summary>Returns a Fp3x3 matrix constructed from a QuaternionFp.</summary>
        /// <param name="rotation">The QuaternionFp representing a rotation.</param>
        /// <returns>The Fp3x3 constructed from a QuaternionFp.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3x3 Fp3x3(QuaternionFp rotation)
        {
            return new Fp3x3(rotation);
        }

        /// <summary>Returns a Fp4x4 constructed from a Fp3x3 rotation matrix and a Fp3 translation vector.</summary>
        /// <param name="rotation">The Fp3x3 rotation matrix.</param>
        /// <param name="translation">The translation vector.</param>
        /// <returns>The Fp4x4 constructed from a rotation and translation.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4x4 Fp4x4(Fp3x3 rotation, Fp3 translation)
        {
            return new Fp4x4(rotation, translation);
        }

        /// <summary>Returns a Fp4x4 constructed from a QuaternionFp and a Fp3 translation vector.</summary>
        /// <param name="rotation">The QuaternionFp rotation.</param>
        /// <param name="translation">The translation vector.</param>
        /// <returns>The Fp4x4 constructed from a rotation and translation.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4x4 Fp4x4(QuaternionFp rotation, Fp3 translation)
        {
            return new Fp4x4(rotation, translation);
        }

        /// <summary>Returns a Fp4x4 constructed from a RigidTransform.</summary>
        /// <param name="transform">The rigid transformation.</param>
        /// <returns>The Fp4x4 constructed from a RigidTransform.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4x4 Fp4x4(RigidTransform transform)
        {
            return new Fp4x4(transform);
        }

        /// <summary>Returns an orthonormalized version of a Fp3x3 matrix.</summary>
        /// <param name="i">The Fp3x3 to be orthonormalized.</param>
        /// <returns>The orthonormalized Fp3x3 matrix.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3x3 orthonormalize(Fp3x3 i)
        {
            Fp3x3 o;

            Fp3 u = i.c0;
            Fp3 v = i.c1 - i.c0 * MathFp.dot(i.c1, i.c0);

            Fp lenU = MathFp.length(u);
            Fp lenV = MathFp.length(v);

            bool c = lenU > Fp.OneEMinus8 && lenV > Fp.OneEMinus8;

            o.c0 = MathFp.select(Fp3(1, 0, 0), u / lenU, c);
            o.c1 = MathFp.select(Fp3(0, 1, 0), v / lenV, c);
            o.c2 = MathFp.cross(o.c0, o.c1);

            return o;
        }

        /// <summary>
        /// 将过小的值舍入到 0，使某些计算仍能继续
        /// </summary>
        public static Fp IgnoreTooSmallNumber(Fp x)
        {
            if (x.value == -Fp.INF)
            {
                x.value = 0;
                return x;
            }

            return x;
        }

        public static Fp2 IgnoreTooSmallNumber(Fp2 x)
        {
            x.x = IgnoreTooSmallNumber(x.x);
            x.y = IgnoreTooSmallNumber(x.y);
            return x;
        }

        public static Fp3 IgnoreTooSmallNumber(Fp3 x)
        {
            x.x = IgnoreTooSmallNumber(x.x);
            x.y = IgnoreTooSmallNumber(x.y);
            x.z = IgnoreTooSmallNumber(x.z);
            return x;
        }

        public static Fp4 IgnoreTooSmallNumber(Fp4 x)
        {
            x.x = IgnoreTooSmallNumber(x.x);
            x.y = IgnoreTooSmallNumber(x.y);
            x.z = IgnoreTooSmallNumber(x.z);
            x.w = IgnoreTooSmallNumber(x.w);
            return x;
        }

        public static Fp2x2 IgnoreTooSmallNumber(Fp2x2 x)
        {
            x.c0 = IgnoreTooSmallNumber(x.c0);
            x.c1 = IgnoreTooSmallNumber(x.c1);
            return x;
        }

        public static Fp3x3 IgnoreTooSmallNumber(Fp3x3 x)
        {
            x.c0 = IgnoreTooSmallNumber(x.c0);
            x.c1 = IgnoreTooSmallNumber(x.c1);
            x.c2 = IgnoreTooSmallNumber(x.c2);
            return x;
        }

        public static Fp4x4 IgnoreTooSmallNumber(Fp4x4 x)
        {
            x.c0 = IgnoreTooSmallNumber(x.c0);
            x.c1 = IgnoreTooSmallNumber(x.c1);
            x.c2 = IgnoreTooSmallNumber(x.c2);
            x.c3 = IgnoreTooSmallNumber(x.c3);
            return x;
        }

        #endregion
    }
}