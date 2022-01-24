using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Unity.Mathematics.FixedPoint
{
    /// <summary>
    /// Represents a Q31.32 fixed-point number.
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Explicit, Size = 8)]
    public partial struct Fp : IEquatable<Fp>, IComparable<Fp>
    {
        #region Filed

        /// <summary>
        /// The underlying integer representation
        /// </summary>
        [FieldOffset(0)] public long value;

        #endregion

        #region Basic Values

        /// <summary>
        /// LUT大小
        /// </summary>
        public static readonly uint LUTSize = 4096;

        /// <summary>
        /// sin tan LUT 元素间隔距离
        /// </summary>
        public static Fp LUTTriangleInterval => (Fp)(LUTSize - 1) / PiOver2;

        /// <summary>
        /// asin LUT 元素间隔距离
        /// </summary>
        public static Fp LUTAsinInterval => (int)(LUTSize - 1) / Fp.Two;

        /// <summary>
        /// asin LUT 元素间隔距离
        /// </summary>
        public static Fp LUTExpInterval => (int)(LUTSize - 1) / (Fp)4;

        // Precision of this type is 2^-32, that is 2,3283064365386962890625E-10 = 0.00000000023283064365386962890625m;
        // public static decimal Precision => new Fp(1L);

        public const long INF = 0x7FFFFFFFFFFFFFFF;
        public const long NAN = -2L;
        public const long MINVALUE = -3L;
        public const long MAXVALUE = 0x7FFFFFFFFFFFFFFD;
        public const long LOG2MAX = 0x1F00000000;
        public const long LOG2MIN = -0x2000000000;
        public const long HALF = 1L << 31;
        public const long ONE = 1L << 32;

        public static Fp Inf => new Fp(INF);

        /// <summary>
        /// 用来在除 0 等特殊场合使用
        /// </summary>
        public static Fp NaN => new Fp(NAN);

        public static Fp Log2Max => new Fp(LOG2MAX);

        public static Fp Log2Min => new Fp(LOG2MIN);


        public static Fp Half => new Fp(1L << 31);
        public static Fp One => new Fp(1L << 32);
        public static Fp Two => 2;
        public static Fp Three => 3;
        public static Fp Ln2 => new Fp(0xB17217F7);

        public static Fp MaxValue => new Fp(MAXVALUE);

        public static Fp MinValue => new Fp(MINVALUE);

        public static Fp PiOver2 => new Fp(0x1921FB544);

        public static Fp PiTimes2 => new Fp(0x6487ED511);

        public static Fp Pi => new Fp(0x3243F6A88);

        /// <summary>
        /// 1e-6
        /// </summary>
        public static Fp Epsilon => new Fp(0x10C6);

        public static Fp PiOver180 => new Fp(0x477D1A8);

        public static Fp PiOver180Inverse => new Fp(0x394BB834C7);

        /// 1e-8
        public static Fp OneEMinus8 => new Fp(0x2A);

        /// 0.69314718
        public static Fp Point69314718 => new Fp(0xB17217F5);

        /// 2.30258509
        public static Fp TwoPoint30258509 => new Fp(0x24D763769);

        #endregion

        #region Operator

        /// <summary>
        /// Adds x and y. Performs saturating addition, i.e. in case of overflow,
        /// rounds to MinValue or MaxValue depending on sign of operands.
        /// </summary>
        public static Fp operator +(Fp x, Fp y)
        {
            if (SkipCalculation(x))
            {
                return x;
            }

            if (SkipCalculation(y))
            {
                return y;
            }

            bool overflow = false;
            x.value = CorrectNumber(AddOverflowHelper(x.value, y.value, ref overflow));
            // if signs of operands are equal and signs of sum and x are different
            if (overflow)
            {
                x.value = x.value > 0 ? INF : 0;//-INF;
            }

            return x;
        }

        /// <summary>
        /// Subtracts y from x. Performs saturating subtraction, i.e. in case of overflow,
        /// rounds to MinValue or MaxValue depending on sign of operands.
        /// </summary>
        public static Fp operator -(Fp x, Fp y)
        {
            if (SkipCalculation(x))
            {
                return x;
            }

            if (SkipCalculation(y))
            {
                return y;
            }

            long xValue = x.value;
            long yValue = y.value;
            long diff = xValue - yValue;
            // if signs of operands are different and signs of sum and x are different
            if (((xValue ^ yValue) & (xValue ^ diff) & long.MinValue) != 0)
            {
                x.value = xValue > 0 ? INF : 0;//-INF;
                return x;
            }

            x.value = CorrectNumber(diff);
            return x;
        }

        /// <summary>
        /// x * y 定点数乘法运算
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static Fp operator *(Fp x, Fp y)
        {
            if (SkipCalculation(x))
            {
                return x;
            }

            if (SkipCalculation(y))
            {
                return y;
            }

            ulong xFractional = (ulong)FractionalPart(x);
            long xInteger = IntegerPart(x);
            ulong yFractional = (ulong)FractionalPart(y);
            long yInteger = IntegerPart(y);

            ulong fractionalMul = xFractional * yFractional;
            long xFractionalYIntegerMul = (long)xFractional * yInteger;
            long yFractionalXIntegerMul = xInteger * (long)yFractional;
            long integerMul = xInteger * yInteger;

            ulong fractional = fractionalMul >> 32;
            long xFractionalYInteger = xFractionalYIntegerMul;
            long yFractionalXInteger = yFractionalXIntegerMul;
            long integer = integerMul << 32;

            bool overflow = false;
            long sum = AddOverflowHelper((long)fractional, xFractionalYInteger, ref overflow);
            sum = AddOverflowHelper(sum, yFractionalXInteger, ref overflow);
            sum = AddOverflowHelper(sum, integer, ref overflow);

            bool opSignsEqual = SignEqual(x.value, y.value);

            // if signs of operands are equal and sign of result is negative,
            // then multiplication overflowed positively
            // the reverse is also true
            if (opSignsEqual)
            {
                if (sum < 0 || (overflow && x > 0))
                {
                    x.value = INF;
                    return x;
                }
            }
            else if (sum > 0)
            {
                x.value = 0;//-INF;
                return x;
            }

            // if the top 32 bits of integerMul (unused in the result) are neither all 0s or 1s,
            // then this means the result overflowed.
            long topCarry = integerMul >> 32;
            if (topCarry != 0 && topCarry != -1)
            {
                x.value = opSignsEqual ? INF : 0;//-INF;
                return x;
            }

            // If signs differ, both operands' magnitudes are greater than 1,
            // and the result is greater than the negative operand, then there was negative overflow.
            if (!opSignsEqual)
            {
                long posOp = x > y ? x.value : y.value;
                long negOp = x > y ? y.value : x.value;
                if (sum > negOp && negOp < -(1L << 32) && posOp > (1L << 32))
                {
                    x.value = 0;//-INF;
                    return x;
                }
            }

            x.value = CorrectNumber(sum);
            return x;
        }

        /// <summary>
        /// x / y 定点数除法运算
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static Fp operator /(Fp x, Fp y)
        {
            if (SkipCalculation(x))
            {
                return x;
            }

            if (SkipCalculation(y))
            {
                return y;
            }

            long yValue = y.value;
            if (yValue == 0)
            {
                if (x.value == 0)
                {
                    x.value = NAN;
                    return x;
                }

                x.value = x.value > 0 ? INF : 0;//-INF;
                return x;
            }

            long xValue = x.value;
            ulong remainder = (ulong)(xValue >= 0 ? xValue : -xValue);
            ulong divider = (ulong)(yValue >= 0 ? yValue : -yValue);
            ulong quotient = 0UL;
            int bitPos = 33;


            // If the divider is divisible by 2^n, take advantage of it.
            while ((divider & 0xF) == 0 && bitPos >= 4)
            {
                divider >>= 4;
                bitPos -= 4;
            }

            while (remainder != 0 && bitPos >= 0)
            {
                int shift = CountLeadingZeroes(remainder);
                if (shift > bitPos)
                {
                    shift = bitPos;
                }

                remainder <<= shift;
                bitPos -= shift;

                ulong div = remainder / divider;
                remainder = remainder % divider;
                quotient += div << bitPos;

                // Detect overflow
                if ((div & ~(0xFFFFFFFFFFFFFFFF >> bitPos)) != 0)
                {
                    x.value = SignEqual(xValue, yValue) ? INF : 0;//-INF;
                    return x;
                }

                remainder <<= 1;
                --bitPos;
            }

            // rounding
            ++quotient;
            long result = (long)(quotient >> 1);
            if (!SignEqual(xValue, yValue))
            {
                result = -result;
            }

            x.value = CorrectNumber(result);
            return x;
        }

        /// <summary>
        /// x % y 定点数模运算
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static Fp operator %(Fp x, Fp y)
        {
            if (SkipCalculation(x))
            {
                return x;
            }

            if (SkipCalculation(y))
            {
                return y;
            }

            if (x.value == MINVALUE && y.value == -INF)
            {
                x.value = 0;
                return x;
            }

            return FastMod(x, y);
        }

        public static Fp operator -(Fp x)
        {
            if (IsNaN(x) != 0)
            {
                return x;
            }

            x.value = CorrectNumber(-x.value);
            return x;
        }

        public static Fp operator +(Fp x)
        {
            return x;
        }

        public static Fp operator ++(Fp x)
        {
            return x + One;
        }

        public static Fp operator --(Fp x)
        {
            return x - One;
        }

        public static bool operator ==(Fp x, Fp y)
        {
            return x.value == y.value;
        }

        public static bool operator !=(Fp x, Fp y)
        {
            return !(x == y);
        }

        public static bool operator >(Fp x, Fp y)
        {
            return x.value > y.value;
        }

        public static bool operator <(Fp x, Fp y)
        {
            return x.value < y.value;
        }

        public static bool operator >=(Fp x, Fp y)
        {
            return x.value >= y.value;
        }

        public static bool operator <=(Fp x, Fp y)
        {
            return x.value <= y.value;
        }

        #endregion

        #region TypeCasting

#if ENABLE_DECIMAL
        public static implicit operator Fp(decimal decimalValue)
        {
            return new Fp((long)(decimalValue * (1L << 32)));
        }
#endif

        // public static implicit operator decimal(Fp fp)
        // {
        //     return fp.value / (1L << 32);
        // }

        public static explicit operator Fp(double doubleValue)
        {
            return new Fp((long)(doubleValue * (1L << 32)));
        }

        public static explicit operator double(Fp fp)
        {
            return (double)fp.value / (1L << 32);
        }

        public static explicit operator Fp(float floatValue)
        {
            return new Fp((long)(floatValue * (1L << 32)));
        }

        public static explicit operator float(Fp fp)
        {
            return (float)((double)fp.value / (1L << 32));
        }

        public static implicit operator Fp(int intValue)
        {
            return new Fp(intValue * (1L << 32));
        }

        public static explicit operator int(Fp x)
        {
            return (int)IntegerPart(x);
        }

        public static explicit operator Fp(long longValue)
        {
            return new Fp(longValue);
        }

        public static explicit operator long(Fp x)
        {
            return x.value;
        }

        public static explicit operator Fp(uint intValue)
        {
            return new Fp((int)intValue * (1L << 32));
        }

        public static explicit operator uint(Fp x)
        {
            return (uint)IntegerPart(x);
        }

        #endregion

        #region Interface Implement

        public override bool Equals(object obj)
        {
            return obj is Fp fp && fp == this;
        }

        public override int GetHashCode()
        {
            // ReSharper disable once NonReadonlyMemberInGetHashCode
            return value.GetHashCode();
        }

        public bool Equals(Fp other)
        {
            return other == this;
        }

        public int CompareTo(Fp other)
        {
            return value.CompareTo(other.value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            // Up to 10 decimal places
#if ENABLE_DECIMAL
            return ((decimal)this.value / (1L << 32)).ToString("0.##########");
#else
            // ** NOTE:这个值在某个精度后就不准确了，仅供参考
            return $"{this.value / (double)(1L << 32)}{Fp.ValidateToString(this)}";
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(string format, IFormatProvider formatProvider)
        {
#if ENABLE_DECIMAL
            // Up to 10 decimal places
            return ((decimal)this.value / (1L << 32)).ToString("0.##########", formatProvider);
#else
            // ** NOTE:这个值在某个精度后就不准确了，仅供参考
            return $"{this.value / (double)(1L << 32)}{Fp.ValidateToString(this)}";
#endif
        }

        #endregion

        #region Internal

        /// <summary>
        /// 检查合法性
        /// </summary>
        /// <returns></returns>
        internal static string ValidateToString(Fp x)
        {
            if (IsInf(x) != 0)
            {
                string sign = x < 0 ? "-" : "";
                return $"({sign}Inf)";
            }
            else if (IsNaN(x) != 0)
            {
                return "(NaN)";
            }

            return "";
        }

        /// <summary>
        /// 修正不符合规则的结果
        /// </summary>
        /// <param name="x"></param>
        internal static long CorrectNumber(long x)
        {
            if (x > MAXVALUE)
            {
                x = INF;
            }

            if (x < 0 && x > MINVALUE)
            {
                x = 0;//-INF;
            }

            return x;
        }

        /// <summary>
        /// 对于某些特定输入，直接返回输入值
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        internal static bool SkipCalculation(Fp x)
        {
            return IsInf(x) != 0 || IsNaN(x) != 0;
        }

        /// <summary>
        /// Returns a number indicating the sign of a fixed point number.
        /// Returns 1 if the value is positive, 0 if is 0, and -1 if it is negative.
        /// </summary>
        internal static int Sign(Fp x)
        {
            return
                x.value < 0 ? -1 :
                x.value > 0 ? 1 :
                0;
        }

        /// <summary>
        /// 检查是否为 infinity
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        internal static int IsInf(Fp x)
        {
            if (x.value == INF)
            {
                return 1;
            }
            else if (x.value == -INF)
            {
                return -1;
            }

            return 0;
        }

        /// <summary>
        /// 检查是否为 NaN
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        internal static int IsNaN(Fp x)
        {
            return x.value == NAN ? 1 : 0;
        }

        /// <summary>
        /// Returns the absolute value of a fixed point number.
        /// NOTE: Abs(Fp.MinValue) == 0.
        /// </summary>
        internal static Fp Abs(Fp x)
        {
            if (IsNaN(x) != 0)
            {
                return x;
            }

            if (x.value == -INF)
            {
                x.value = 0;
                return x;
            }

            return FastAbs(x);
        }

        /// <summary>
        /// Returns the absolute value of a fixed point number.
        /// FastAbs(fixed point.MinValue) is undefined.
        /// </summary>
        internal static Fp FastAbs(Fp x)
        {
            // branch less implementation, see http://www.strchr.com/optimized_abs_function
            long mask = x.value >> 63;
            x.value = (x.value + mask) ^ mask;
            return x;
        }

        /// <summary>
        /// Returns the largest integer less than or equal to the specified number.
        /// </summary>
        internal static Fp Floor(Fp x)
        {
            // Just zero out the fractional part
            x.value = (long)((ulong)x.value & 0xFFFFFFFF00000000);
            return x;
        }

        /// <summary>
        /// Returns the smallest integer value that is greater than or equal to the specified number.
        /// </summary>
        internal static Fp Ceiling(Fp x)
        {
            if (FractionalPart(x) != 0)
            {
                return Floor(x) + One;
            }

            return x;
        }

        /// <summary>
        /// 返回定点数的小数部分
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        internal static long FractionalPart(Fp x)
        {
            return x.value & 0x00000000FFFFFFFF;
        }

        internal static long IntegerPart(Fp x)
        {
            return x.value >> 32;
        }

        /// <summary>
        /// Rounds a value to the nearest integer value.
        /// If the value is halfway between an even and an uneven value, returns the even value.
        /// </summary>
        internal static Fp Round(Fp x)
        {
            long fractionalPart = FractionalPart(x);
            Fp integerFloor = Floor(x);
            if (fractionalPart < 0x80000000)
            {
                return integerFloor;
            }

            if (fractionalPart > 0x80000000)
            {
                return integerFloor + One;
            }

            // if number is halfway between two values, round to the nearest even number
            // this is the method used by System.Math.Round().
            return (integerFloor.value & (1L << 32)) == 0
                ? integerFloor
                : integerFloor + One;
        }

        /// <summary>
        /// Rounds a value to the nearest integer value towards zero.
        /// </summary>
        internal static Fp Truncate(Fp value)
        {
            int sign = Sign(value);
            Fp integerFloor = Floor(value);

            if (sign < 0)
            {
                return integerFloor + One;
            }

            return integerFloor;
        }

        /// <summary>
        /// Adds x and y without performing overflow checking. Should be inlined by the CLR.
        /// </summary>
        internal static Fp FastAdd(Fp x, Fp y)
        {
            x.value += y.value;
            return x;
        }


        /// <summary>
        /// Subtracts y from x without performing overflow checking. Should be inlined by the CLR.
        /// </summary>
        internal static Fp FastSub(Fp x, Fp y)
        {
            x.value -= y.value;
            return x;
        }

        /// <summary>
        /// 相加并检查是否溢出，返回相加的结果
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="overflow"></param>
        /// <returns></returns>
        internal static long AddOverflowHelper(long x, long y, ref bool overflow)
        {
            long sum = x + y;
            // x + y overflows if sign(x) ^ sign(y) != sign(sum)
            overflow |= (~(x ^ y) & (x ^ sum) & long.MinValue) != 0;
            return sum;
        }

        /// <summary>
        /// 检查符号是否相反
        /// </summary>
        /// <param name="xValue"></param>
        /// <param name="yValue"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static bool SignEqual(long xValue, long yValue)
        {
            return ((xValue ^ yValue) & long.MinValue) == 0;
        }


        /// <summary>
        /// Performs multiplication without checking for overflow.
        /// Useful for performance-critical code where the values are guaranteed not to cause overflow
        /// </summary>
        internal static Fp FastMul(Fp x, Fp y)
        {
            ulong xFractional = (ulong)FractionalPart(x);
            long xInteger = IntegerPart(x);
            ulong yFractional = (ulong)FractionalPart(y);
            long yInteger = IntegerPart(y);

            ulong fractionalMul = xFractional * yFractional;
            long xFractionalYIntegerMul = (long)xFractional * yInteger;
            long yFractionalXIntegerMul = xInteger * (long)yFractional;
            long integerMul = xInteger * yInteger;

            ulong fractional = fractionalMul >> 32;
            long xFractionalYInteger = xFractionalYIntegerMul;
            long yFractionalXInteger = yFractionalXIntegerMul;
            long integer = integerMul << 32;

            x.value = (long)fractional + xFractionalYInteger + yFractionalXInteger + integer;
            return x;
        }

        /// <summary>
        /// 计算64位x的前置0的数量
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static int CountLeadingZeroes(ulong x)
        {
            int result = 0;
            while ((x & 0xF000000000000000) == 0)
            {
                result += 4;
                x <<= 4;
            }

            while ((x & 0x8000000000000000) == 0)
            {
                result += 1;
                x <<= 1;
            }

            return result;
        }


        /// <summary>
        /// Performs modulo as fast as possible;
        /// Use the operator (%) for a more reliable but slower modulo.
        /// </summary>
        internal static Fp FastMod(Fp x, Fp y)
        {
            x.value %= y.value;
            x.value = CorrectNumber(x.value);
            return x;
        }

        /// <summary>
        /// get exp by LUTExponent
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        internal static Fp Exp(Fp x)
        {
            Fp rawIndex = FastMul(x + 1, LUTExpInterval);
            Fp roundedIndex = Round(rawIndex);
            Fp indexError = FastSub(rawIndex, roundedIndex);
            int index = (int)IntegerPart(roundedIndex);
            // ** FIXME: Burst 对于 [] 可能会报错
            Fp nearestValue = new Fp(ExpLUT[index]);
            Fp secondNearestValue = new Fp(ExpLUT[index + Sign(indexError)]);

            long delta = FastMul(indexError, FastAbs(FastSub(nearestValue, secondNearestValue))).value;
            long interpolatedValue = nearestValue.value + delta;
            // var interpolatedValue = Interpolate(x, ExpLUT);
            x.value = interpolatedValue;
            return x;
        }

        /// <summary>
        /// Returns 2 raised to the specified power.
        /// Provides at least 6 decimals of accuracy.
        /// </summary>
        internal static Fp Pow2(Fp x)
        {
            if (x.value == 0)
            {
                return One;
            }

            // Avoid negative arguments by exploiting that exp(-x) = 1/exp(x).
            bool neg = x.value < 0;
            if (neg)
            {
                x = -x;
            }

            if (x == One)
            {
                return neg ? Half : Two;
            }

            if (x.value >= LOG2MAX)
            {
                return neg ? One / MaxValue : MaxValue;
            }

            if (x.value <= LOG2MIN)
            {
                x.value = neg ? MAXVALUE : 0;
                return x;
            }

            /*
             * The algorithm is based on the power series for exp(x):
             * http://en.wikipedia.org/wiki/Exponential_function#Formal_definition
             *
             * From term n, we get term n+1 by multiplying with x/n.
             * When the sum term drops to zero, we can stop summing.
             */

            int integerPart = (int)IntegerPart(Floor(x));
            // Take fractional part of exponent
            x.value = FractionalPart(x);

            Fp result = One;
            Fp term = One;
            int i = 1;
            while (term.value != 0)
            {
                term = FastMul(FastMul(x, term), Ln2) / i;
                result += term;
                i++;
            }

            result.value <<= integerPart;
            if (neg)
            {
                result = One / result;
            }

            return result;
        }

        /// <summary>
        /// Returns the base-2 logarithm of a specified number.
        /// Provides at least 9 decimals of accuracy.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The argument was non-positive
        /// </exception>
        internal static Fp Log2(Fp x)
        {
            if (x.value <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(x), $"Non-positive ({x}){x.value:X} value passed to Ln");
            }

            // This implementation is based on Clay. S. Turner's fast binary logarithm
            // algorithm (C. S. Turner,  "A Fast Binary Logarithm Algorithm", IEEE Signal
            //     Processing Mag., pp. 124,140, Sep. 2010.)

            long b = 1U << (31);
            long y = 0;

            long rawX = x.value;
            while (rawX < (1L << 32))
            {
                rawX <<= 1;
                y -= (1L << 32);
            }

            while (rawX >= ((1L << 32) << 1))
            {
                rawX >>= 1;
                y += (1L << 32);
            }

            Fp z = new Fp(rawX);

            for (int i = 0; i < 32; i++)
            {
                z = FastMul(z, z);
                if (z.value >= ((1L << 32) << 1))
                {
                    z.value >>= 1;
                    y += b;
                }

                b >>= 1;
            }

            x.value = y;
            return x;
        }

        /// <summary>
        /// Returns the natural logarithm of a specified number.
        /// Provides at least 7 decimals of accuracy.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The argument was non-positive
        /// </exception>
        internal static Fp Ln(Fp x)
        {
            return FastMul(Log2(x), Ln2);
        }

        /// <summary>
        /// Returns a specified number raised to the specified power.
        /// Provides about 5 digits of accuracy for the result.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The base was negative, with a non-zero exponent
        /// </exception>
        internal static Fp Pow(Fp b, Fp exp)
        {
            if (b == One)
            {
                return One;
            }

            if (exp.value == 0)
            {
                return One;
            }

            if (b.value == 0)
            {
                if (exp.value < 0)
                {
                    b.value = NAN;
                    return b;
                }

                return 0;
            }

            Fp log2 = Log2(b);
            return Pow2(exp * log2);
        }

        /// <summary>
        /// Returns the square root of a specified number.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The argument was negative.
        /// </exception>
        internal static Fp Sqrt(Fp x)
        {
            if (x.value < 0)
            {
                x.value = NAN;
                return x;
            }

            ulong num = (ulong)x.value;
            ulong result = 0UL;

            // second-to-top bit
            ulong bit = 1UL << 62;

            while (bit > num)
            {
                bit >>= 2;
            }

            // The main part is executed twice, in order to avoid
            // using 128 bit values in computations.
            ulong threshold = (1UL << 32) - 1;
            for (var i = 0; i < 2; ++i)
            {
                // First we get the top 48 bits of the answer.
                while (bit != 0)
                {
                    if (num >= result + bit)
                    {
                        num -= result + bit;
                        result = (result >> 1) + bit;
                    }
                    else
                    {
                        result = result >> 1;
                    }

                    bit >>= 2;
                }

                if (i == 0)
                {
                    // Then process it again to get the lowest 16 bits.
                    if (num > threshold)
                    {
                        // The remainder 'num' is too large to be shifted left
                        // by 32, so we have to add 1 to result manually and
                        // adjust 'num' accordingly.
                        // num = a - (result + 0.5)^2
                        //       = num + result^2 - (result + 0.5)^2
                        //       = num - result - 0.5
                        num -= result;
                        num = (num << 32) - 0x80000000UL;
                        result = (result << 32) + 0x80000000UL;
                    }
                    else
                    {
                        num <<= (32);
                        result <<= (32);
                    }

                    bit = 1UL << 30;
                }
            }

            // Finally, if next bit would have been 1, round the result upwards.
            if (num > result)
            {
                ++result;
            }

            x.value = (long)result;
            return x;
        }

        /// <summary>
        /// Returns the Sine of x.
        /// The relative error is less than 1E-10 for x in [-2PI, 2PI], and less than 1E-7 in the worst case.
        /// </summary>
        internal static Fp Sin(Fp x)
        {
            x.value = ClampSinValue(x.value, out var flipHorizontal, out var flipVertical);

            // Find the two closest values in the LUT and perform linear interpolation
            // This is what kills the performance of this function on x86 - x64 is fine though
            Fp rawIndex = FastMul(x, LUTTriangleInterval);
            Fp roundedIndex = Round(rawIndex);
            Fp indexError = FastSub(rawIndex, roundedIndex);
            int index = (int)IntegerPart(roundedIndex);

            Fp nearestValue =
                new Fp(SinLUT[flipHorizontal ? SinLUT.Length - 1 - index : index]);
            Fp secondNearestValue =
                new Fp(SinLUT[
                    flipHorizontal
                        ? SinLUT.Length - 1 - index - Sign(indexError)
                        : index + Sign(indexError)]);

            long delta = FastMul(indexError, FastAbs(FastSub(nearestValue, secondNearestValue))).value;
            long interpolatedValue = nearestValue.value + (flipHorizontal ? -delta : delta);
            x.value = flipVertical ? -interpolatedValue : interpolatedValue;
            return x;
        }

        /// <summary>
        /// Returns a rough approximation of the Sine of x.
        /// This is at least 3 times faster than Sin() on x86 and slightly faster than Math.Sin(),
        /// however its accuracy is limited to 4-5 decimals, for small enough values of x.
        /// </summary>
        internal static Fp FastSin(Fp x)
        {
            long clampedL = ClampSinValue(x.value, out bool flipHorizontal, out bool flipVertical);

            // Here we use the fact that the SinLUT table has a number of entries
            // equal to (PI_OVER_2 >> 15) to use the angle to index directly into it
            uint rawIndex = (uint)(clampedL >> 15);
            if (rawIndex >= LUTSize)
            {
                rawIndex = LUTSize - 1;
            }

            long nearestValue = SinLUT[flipHorizontal ? SinLUT.Length - 1 - (int)rawIndex : (int)rawIndex];
            x.value = flipVertical ? -nearestValue : nearestValue;
            return x;
        }


        /// <summary>
        /// 将 angle 对齐到[0, π/2)之间
        /// </summary>
        /// <param name="angle"></param>
        /// <param name="flipHorizontal"></param>
        /// <param name="flipVertical"></param>
        /// <returns></returns>
        private static long ClampSinValue(long angle, out bool flipHorizontal, out bool flipVertical)
        {
            long largePI = 7244019458077122842;
            // Obtained from ((fixed point)1686629713.065252369824872831112M).value
            // This is (2^29)*PI, where 29 is the largest N such that (2^N)*PI < MaxValue.
            // The idea is that this number contains way more precision than PI_TIMES_2,
            // and (((x % (2^29*PI)) % (2^28*PI)) % ... (2^1*PI) = x % (2 * PI)
            // In practice this gives us an error of about 1,25e-9 in the worst case scenario (Sin(MaxValue))
            // Whereas simply doing x % PI_TIMES_2 is the 2e-3 range.

            long clamped2Pi = angle;
            for (int i = 0; i < 29; ++i)
            {
                clamped2Pi %= (largePI >> i);
            }

            if (angle < 0)
            {
                clamped2Pi += PiTimes2.value;
            }

            // The LUT contains values for 0 - PiOver2; every other value must be obtained by
            // vertical or horizontal mirroring
            flipVertical = false;
            // obtain (angle % PI) from (angle % 2PI) - much faster than doing another modulo
            long clampedPi = clamped2Pi;
            while (clampedPi >= Pi.value)
            {
                flipVertical = true;
                clampedPi -= Pi.value;
            }

            flipHorizontal = clampedPi >= PiOver2.value;
            // obtain (angle % PI_OVER_2) from (angle % PI) - much faster than doing another modulo
            return flipHorizontal ? clampedPi - PiOver2.value : clampedPi;
        }

        /// <summary>
        /// Returns the cosine of x.
        /// The relative error is less than 1E-10 for x in [-2PI, 2PI], and less than 1E-7 in the worst case.
        /// </summary>
        internal static Fp Cos(Fp x)
        {
            x.value += (x.value > 0 ? -Pi.value - PiOver2.value : PiOver2.value);
            return Sin(x);
        }

        /// <summary>
        /// Returns a rough approximation of the cosine of x.
        /// See FastSin for more details.
        /// </summary>
        internal static Fp FastCos(Fp x)
        {
            x.value += (x.value > 0 ? -Pi.value - PiOver2.value : PiOver2.value);
            return FastSin(x);
        }

        /// <summary>
        /// Returns the tangent of x.
        /// TODO:This function is not well-tested. It may be wildly inaccurate.
        /// </summary>
        internal static Fp Tan(Fp x)
        {
            long clampedPi = x.value % Pi.value;
            bool flip = false;
            if (clampedPi < 0)
            {
                clampedPi = -clampedPi;
                flip = true;
            }

            if (clampedPi > PiOver2.value)
            {
                flip = !flip;
                clampedPi = PiOver2.value - (clampedPi - PiOver2.value);
            }

            Fp clamped = new Fp(clampedPi);
            // Find the two closest values in the LUT and perform linear interpolation
            Fp rawIndex = FastMul(clamped, LUTTriangleInterval);
            Fp roundedIndex = Round(rawIndex);
            Fp indexError = FastSub(rawIndex, roundedIndex);
            int index = (int)IntegerPart(roundedIndex);
            Fp nearestValue = new Fp(TanLUT[index]);
            Fp secondNearestValue = new Fp(TanLUT[index + Sign(indexError)]);

            long delta = FastMul(indexError, FastAbs(FastSub(nearestValue, secondNearestValue))).value;
            long interpolatedValue = nearestValue.value + delta;
            x.value = flip ? -interpolatedValue : interpolatedValue;
            return x;
        }

        /// <summary>
        /// 根据 x 查找 LUT 并在临近的查找中插值
        /// </summary>
        /// <param name="x"></param>
        /// <param name="LUT"></param>
        /// <returns></returns>
        internal static long Interpolate(Fp x, long[] LUT)
        {
            Fp rawIndex = FastMul(x, LUTTriangleInterval);
            Fp roundedIndex = Round(rawIndex);
            Fp indexError = FastSub(rawIndex, roundedIndex);
            int index = (int)IntegerPart(roundedIndex);
            Fp nearestValue = new Fp(LUT[index]);
            Fp secondNearestValue = new Fp(LUT[index + Sign(indexError)]);

            long delta = FastMul(indexError, FastAbs(FastSub(nearestValue, secondNearestValue))).value;
            long interpolatedValue = nearestValue.value + delta;
            return interpolatedValue;
        }

        /// <summary>
        /// 返回 ArcSine(x)， x 范围 [-1,1]
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        internal static Fp Asin(Fp x)
        {
            if (x < -One || x > One)
            {
                throw new ArgumentOutOfRangeException(nameof(x), $"value ({x}){x.value:X} out of range [-1, 1]");
            }

            if (x.value == 0)
            {
                return x;
            }

            Fp rawIndex = FastMul(x + 1, LUTAsinInterval);
            Fp roundedIndex = Round(rawIndex);
            Fp indexError = FastSub(rawIndex, roundedIndex);
            int index = (int)IntegerPart(roundedIndex);
            // ** FIXME: Burst 对于 [] 可能会报错
            Fp nearestValue = new Fp(ASinLUT[index]);
            int nextIndex = index + Sign(indexError);
            if (nextIndex >= ASinLUT.Length || nextIndex < 0)
            {
                return nearestValue;
            }
            Fp secondNearestValue = new Fp(ASinLUT[index + Sign(indexError)]);

            long delta = FastMul(indexError, FastAbs(FastSub(nearestValue, secondNearestValue))).value;
            long interpolatedValue = nearestValue.value + delta;
            // var interpolatedValue = Interpolate(x, ASinLUT);
            x.value = interpolatedValue;
            return x;
        }

        /// <summary>
        /// Returns the arccosine of the specified number, calculated using Atan and Sqrt
        /// This function has at least 7 decimals of accuracy.
        /// </summary>
        internal static Fp Acos(Fp x)
        {
            if (x < -One || x > One)
            {
                throw new ArgumentOutOfRangeException(nameof(x), $"value ({x}){x.value:X} out of range [-1, 1]");
            }

            if (x.value == 0)
            {
                return PiOver2;
            }

            // ** 使用Asin LUT
            return PiOver2 - Asin(x);
        }

        /// <summary>
        /// Returns the arctangent of the specified number, calculated using Euler series
        /// This function has at least 7 decimals of accuracy.
        /// </summary>
        internal static Fp Atan(Fp z)
        {
            if (z.value == 0)
            {
                return z;
            }

            // Force positive values for argument
            // Atan(-z) = -Atan(z).
            bool neg = z.value < 0;
            if (neg)
            {
                z = -z;
            }

            bool invert = z > One;
            if (invert)
            {
                z = One / z;
            }

            Fp result = One;
            Fp term = One;

            Fp zSq = z * z;
            Fp zSq2 = zSq * Two;
            Fp zSqPlusOne = zSq + One;
            Fp zSq12 = zSqPlusOne * Two;
            Fp dividend = zSq2;
            Fp divisor = zSqPlusOne * Three;

            for (var i = 2; i < 30; ++i)
            {
                term *= dividend / divisor;
                result += term;

                dividend += zSq2;
                divisor += zSq12;

                if (term.value == 0)
                {
                    break;
                }
            }

            result = result * z / zSqPlusOne;

            if (invert)
            {
                result = PiOver2 - result;
            }

            if (neg)
            {
                result = -result;
            }

            return result;
        }

        /// <summary>
        /// Atan2
        /// </summary>
        /// <param name="y"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        internal static Fp Atan2(Fp y, Fp x)
        {
            if (x.value == 0)
            {
                if (y.value > 0)
                {
                    return PiOver2;
                }

                if (y.value == 0)
                {
                    return y;
                }

                return -PiOver2;
            }

            Fp atan;
            Fp z = y / x;

            Fp zSq = z * z;
            Fp zSqMulVal = new Fp(0x47AE147A) * zSq;
            // Deal with overflow
            if ((One + zSqMulVal).value == MAXVALUE)
            {
                return y.value < 0 ? -PiOver2 : PiOver2;
            }

            if (Abs(z) < One)
            {
                atan = z / (One + zSqMulVal);
                if (x.value < 0)
                {
                    if (y.value < 0)
                    {
                        return atan - Pi;
                    }

                    return atan + Pi;
                }
            }
            else
            {
                atan = PiOver2 - z / (zSq + new Fp(0x47AE147A));
                if (y.value < 0)
                {
                    return atan - Pi;
                }
            }

            return atan;
        }

        #endregion

        #region Constructor

        /// <summary>
        /// This is the constructor from raw value
        /// </summary>
        public Fp(long rawValue)
        {
            value = rawValue;
        }

        #endregion
    }
}