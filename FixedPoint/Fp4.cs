using System;
using System.Runtime.CompilerServices;

namespace Unity.Mathematics.FixedPoint
{
    /// <summary>
    /// 对 System.Numerics.Vector4 类的复制，为兼容定点数替换了部分内容
    /// https://github.com/microsoft/referencesource
    /// </summary>
    public partial struct Fp4
    {

        #region Public Instance Methods

        /// <summary>
        /// Copies the contents of the vector into the given array.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void CopyTo(Fp[] array)
        {
            CopyTo(array, 0);
        }

        /// <summary>
        /// Copies the contents of the vector into the given array, starting from index.
        /// </summary>
        /// <exception cref="ArgumentNullException">If array is null.</exception>
        /// <exception cref="RankException">If array is multidimensional.</exception>
        /// <exception cref="ArgumentOutOfRangeException">If index is greater than end of the array or index is less than zero.</exception>
        /// <exception cref="ArgumentException">If number of elements in source vector is greater than those available in destination array.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void CopyTo(Fp[] array, int index)
        {
            if (array == null)
            {
                // Match the JIT's exception type here. For perf, a NullReference is thrown instead of an ArgumentNull.
                throw new NullReferenceException("Arg_NullArgumentNullRef");
            }

            if (index < 0 || index >= array.Length)
            {
                throw new ArgumentOutOfRangeException($"Arg_ArgumentOutOfRangeException index : {index}");
            }

            if ((array.Length - index) < 4)
            {
                throw new ArgumentException("Arg_ElementsInSourceIsGreaterThanDestination index : {index}");
            }

            array[index] = x;
            array[index + 1] = y;
            array[index + 2] = z;
            array[index + 3] = w;
        }

        #endregion Public Instance Methods

        #region Public Static Methods

        /// <summary>
        /// Returns the dot product of two vectors.
        /// </summary>
        /// <param name="vector1">The first vector.</param>
        /// <param name="Fp2">The second vector.</param>
        /// <returns>The dot product.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp Dot(Fp4 vector1, Fp4 Fp2)
        {
            return vector1.x * Fp2.x +
                   vector1.y * Fp2.y +
                   vector1.z * Fp2.z +
                   vector1.w * Fp2.w;
        }

        /// <summary>
        /// Returns a vector whose elements are the minimum of each of the pairs of elements in the two source vectors.
        /// </summary>
        /// <param name="value1">The first source vector.</param>
        /// <param name="value2">The second source vector.</param>
        /// <returns>The minimized vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4 Min(Fp4 value1, Fp4 value2)
        {
            return new Fp4(
                (value1.x < value2.x) ? value1.x : value2.x,
                (value1.y < value2.y) ? value1.y : value2.y,
                (value1.z < value2.z) ? value1.z : value2.z,
                (value1.w < value2.w) ? value1.w : value2.w);
        }

        /// <summary>
        /// Returns a vector whose elements are the maximum of each of the pairs of elements in the two source vectors.
        /// </summary>
        /// <param name="value1">The first source vector.</param>
        /// <param name="value2">The second source vector.</param>
        /// <returns>The maximized vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4 Max(Fp4 value1, Fp4 value2)
        {
            return new Fp4(
                (value1.x > value2.x) ? value1.x : value2.x,
                (value1.y > value2.y) ? value1.y : value2.y,
                (value1.z > value2.z) ? value1.z : value2.z,
                (value1.w > value2.w) ? value1.w : value2.w);
        }

        /// <summary>
        /// Returns a vector whose elements are the absolute values of each of the source vector's elements.
        /// </summary>
        /// <param name="value">The source vector.</param>
        /// <returns>The absolute value vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4 Abs(Fp4 value)
        {
            return new Fp4(MathFp.abs(value.x), MathFp.abs(value.y), MathFp.abs(value.z), MathFp.abs(value.w));
        }

        /// <summary>
        /// Returns a vector whose elements are the square root of each of the source vector's elements.
        /// </summary>
        /// <param name="value">The source vector.</param>
        /// <returns>The square root vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4 SquareRoot(Fp4 value)
        {
            return new Fp4(MathFp.sqrt(value.x), MathFp.sqrt(value.y), MathFp.sqrt(value.z),
                MathFp.sqrt(value.w));
        }

        #endregion Public Static Methods

        #region Public Static Properties

        /// <summary>
        /// Returns the vector (0,0,0,0).
        /// </summary>
        public static Fp4 Zero
        {
            get { return new Fp4(); }
        }

        /// <summary>
        /// Returns the vector (1,1,1,1).
        /// </summary>
        public static Fp4 One
        {
            get { return new Fp4(Fp.One, Fp.One, Fp.One, Fp.One); }
        }

        /// <summary>
        /// Returns the vector (1,0,0,0).
        /// </summary>
        public static Fp4 UnitX
        {
            get { return new Fp4(Fp.One, 0, 0, 0); }
        }

        /// <summary>
        /// Returns the vector (0,1,0,0).
        /// </summary>
        public static Fp4 UnitY
        {
            get { return new Fp4(0, Fp.One, 0, 0); }
        }

        /// <summary>
        /// Returns the vector (0,0,1,0).
        /// </summary>
        public static Fp4 UnitZ
        {
            get { return new Fp4(0, 0, Fp.One, 0); }
        }

        /// <summary>
        /// Returns the vector (0,0,0,1).
        /// </summary>
        public static Fp4 UnitW
        {
            get { return new Fp4(0, 0, 0, Fp.One); }
        }

        #endregion Public Static Properties

        #region Public instance methods

        /// <summary>
        /// Returns the length of the vector. This operation is cheaper than Length().
        /// </summary>
        /// <returns>The vector's length.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Fp Length()
        {
            // if (Vector.IsHardwareAccelerated)
            // {
            //     Fp ls = Fp4.Dot(this, this);
            //     return (Fp)System.Math.Sqrt(ls);
            // }
            // else
            // {
            Fp ls = x * x + y * y + z * z + w * w;

            return MathFp.sqrt(ls);
            // }
        }

        /// <summary>
        /// Returns the length of the vector squared.
        /// </summary>
        /// <returns>The vector's length squared.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Fp LengthSquared()
        {
            // if (Vector.IsHardwareAccelerated)
            // {
            //     return Fp4.Dot(this, this);
            // }
            // else
            // {
            return x * x + y * y + z * z + w * w;
            // }
        }

        #endregion Public Instance Methods

        #region Public Static Methods

        /// <summary>
        /// Returns the Euclidean distance between the two given points.
        /// </summary>
        /// <param name="value1">The first point.</param>
        /// <param name="value2">The second point.</param>
        /// <returns>The distance.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp Distance(Fp4 value1, Fp4 value2)
        {
            // if (Vector.IsHardwareAccelerated)
            // {
            //     Fp4 difference = value1 - value2;
            //     Fp ls = Fp4.Dot(difference, difference);
            //     return (Fp)System.Math.Sqrt(ls);
            // }
            // else
            // {
            Fp dx = value1.x - value2.x;
            Fp dy = value1.y - value2.y;
            Fp dz = value1.z - value2.z;
            Fp dw = value1.w - value2.w;

            Fp ls = dx * dx + dy * dy + dz * dz + dw * dw;

            return MathFp.sqrt(ls);
            // }
        }

        /// <summary>
        /// Returns the Euclidean distance squared between the two given points.
        /// </summary>
        /// <param name="value1">The first point.</param>
        /// <param name="value2">The second point.</param>
        /// <returns>The distance squared.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp DistanceSquared(Fp4 value1, Fp4 value2)
        {
            // if (Vector.IsHardwareAccelerated)
            // {
            //     Fp4 difference = value1 - value2;
            //     return Fp4.Dot(difference, difference);
            // }
            // else
            // {
            Fp dx = value1.x - value2.x;
            Fp dy = value1.y - value2.y;
            Fp dz = value1.z - value2.z;
            Fp dw = value1.w - value2.w;

            return dx * dx + dy * dy + dz * dz + dw * dw;
            // }
        }

        /// <summary>
        /// Returns a vector with the same direction as the given vector, but with a length of 1.
        /// </summary>
        /// <param name="vector">The vector to normalize.</param>
        /// <returns>The normalized vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4 Normalize(Fp4 vector)
        {
            // if (Vector.IsHardwareAccelerated)
            // {
            //     Fp length = vector.Length();
            //     return vector / length;
            // }
            // else
            // {
            Fp ls = vector.x * vector.x + vector.y * vector.y + vector.z * vector.z + vector.w * vector.w;
            Fp invNorm = Fp.One / MathFp.sqrt(ls);

            return new Fp4(
                vector.x * invNorm,
                vector.y * invNorm,
                vector.z * invNorm,
                vector.w * invNorm);
            // }
        }

        /// <summary>
        /// Restricts a vector between a min and max value.
        /// </summary>
        /// <param name="value1">The source vector.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <returns>The restricted vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4 Clamp(Fp4 value1, Fp4 min, Fp4 max)
        {
            // This compare order is very important!!!
            // We must follow HLSL behavior in the case user specified min value is bigger than max value.

            Fp x = value1.x;
            x = (x > max.x) ? max.x : x;
            x = (x < min.x) ? min.x : x;

            Fp y = value1.y;
            y = (y > max.y) ? max.y : y;
            y = (y < min.y) ? min.y : y;

            Fp z = value1.z;
            z = (z > max.z) ? max.z : z;
            z = (z < min.z) ? min.z : z;

            Fp w = value1.w;
            w = (w > max.w) ? max.w : w;
            w = (w < min.w) ? min.w : w;

            return new Fp4(x, y, z, w);
        }

        /// <summary>
        /// Linearly interpolates between two vectors based on the given weighting.
        /// </summary>
        /// <param name="value1">The first source vector.</param>
        /// <param name="value2">The second source vector.</param>
        /// <param name="amount">Value between 0 and 1 indicating the weight of the second source vector.</param>
        /// <returns>The interpolated vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4 Lerp(Fp4 value1, Fp4 value2, Fp amount)
        {
            return new Fp4(
                value1.x + (value2.x - value1.x) * amount,
                value1.y + (value2.y - value1.y) * amount,
                value1.z + (value2.z - value1.z) * amount,
                value1.w + (value2.w - value1.w) * amount);
        }

        /// <summary>
        /// Transforms a vector by the given matrix.
        /// </summary>
        /// <param name="position">The source vector.</param>
        /// <param name="matrix">The transformation matrix.</param>
        /// <returns>The transformed vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4 Transform(Fp2 position, Fp4x4 matrix)
        {
            return new Fp4(
                position.x * matrix.c0.x + position.y * matrix.c0.y + matrix.c0.w,
                position.x * matrix.c1.x + position.y * matrix.c1.y + matrix.c1.w,
                position.x * matrix.c2.x + position.y * matrix.c2.y + matrix.c2.w,
                position.x * matrix.c3.x + position.y * matrix.c3.y + matrix.c3.w);
        }

        /// <summary>
        /// Transforms a vector by the given matrix.
        /// </summary>
        /// <param name="position">The source vector.</param>
        /// <param name="matrix">The transformation matrix.</param>
        /// <returns>The transformed vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4 Transform(Fp3 position, Fp4x4 matrix)
        {
            return new Fp4(
                position.x * matrix.c0.x + position.y * matrix.c0.y + position.z * matrix.c0.z + matrix.c0.w,
                position.x * matrix.c1.x + position.y * matrix.c1.y + position.z * matrix.c1.z + matrix.c1.w,
                position.x * matrix.c2.x + position.y * matrix.c2.y + position.z * matrix.c2.z + matrix.c2.w,
                position.x * matrix.c3.x + position.y * matrix.c3.y + position.z * matrix.c3.z + matrix.c3.w);
        }

        /// <summary>
        /// Transforms a vector by the given matrix.
        /// </summary>
        /// <param name="vector">The source vector.</param>
        /// <param name="matrix">The transformation matrix.</param>
        /// <returns>The transformed vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4 Transform(Fp4 vector, Fp4x4 matrix)
        {
            return new Fp4(
                vector.x * matrix.c0.x + vector.y * matrix.c0.y + vector.z * matrix.c0.z + vector.w * matrix.c0.w,
                vector.x * matrix.c1.x + vector.y * matrix.c1.y + vector.z * matrix.c1.z + vector.w * matrix.c1.w,
                vector.x * matrix.c2.x + vector.y * matrix.c2.y + vector.z * matrix.c2.z + vector.w * matrix.c2.w,
                vector.x * matrix.c3.x + vector.y * matrix.c3.y + vector.z * matrix.c3.z + vector.w * matrix.c3.w);
        }

        /// <summary>
        /// Transforms a vector by the given Quaternion rotation value.
        /// </summary>
        /// <param name="value">The source vector to be rotated.</param>
        /// <param name="rotation">The rotation to apply.</param>
        /// <returns>The transformed vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4 Transform(Fp2 value, QuaternionFp rotation)
        {
            Fp x2 = rotation.value.x + rotation.value.x;
            Fp y2 = rotation.value.y + rotation.value.y;
            Fp z2 = rotation.value.z + rotation.value.z;

            Fp wx2 = rotation.value.w * x2;
            Fp wy2 = rotation.value.w * y2;
            Fp wz2 = rotation.value.w * z2;
            Fp xx2 = rotation.value.x * x2;
            Fp xy2 = rotation.value.x * y2;
            Fp xz2 = rotation.value.x * z2;
            Fp yy2 = rotation.value.y * y2;
            Fp yz2 = rotation.value.y * z2;
            Fp zz2 = rotation.value.z * z2;

            return new Fp4(
                value.x * (Fp.One - yy2 - zz2) + value.y * (xy2 - wz2),
                value.x * (xy2 + wz2) + value.y * (Fp.One - xx2 - zz2),
                value.x * (xz2 - wy2) + value.y * (yz2 + wx2),
                Fp.One);
        }

        /// <summary>
        /// Transforms a vector by the given Quaternion rotation value.
        /// </summary>
        /// <param name="value">The source vector to be rotated.</param>
        /// <param name="rotation">The rotation to apply.</param>
        /// <returns>The transformed vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4 Transform(Fp3 value, QuaternionFp rotation)
        {
            Fp x2 = rotation.value.x + rotation.value.x;
            Fp y2 = rotation.value.y + rotation.value.y;
            Fp z2 = rotation.value.z + rotation.value.z;

            Fp wx2 = rotation.value.w * x2;
            Fp wy2 = rotation.value.w * y2;
            Fp wz2 = rotation.value.w * z2;
            Fp xx2 = rotation.value.x * x2;
            Fp xy2 = rotation.value.x * y2;
            Fp xz2 = rotation.value.x * z2;
            Fp yy2 = rotation.value.y * y2;
            Fp yz2 = rotation.value.y * z2;
            Fp zz2 = rotation.value.z * z2;

            return new Fp4(
                value.x * (Fp.One - yy2 - zz2) + value.y * (xy2 - wz2) + value.z * (xz2 + wy2),
                value.x * (xy2 + wz2) + value.y * (Fp.One - xx2 - zz2) + value.z * (yz2 - wx2),
                value.x * (xz2 - wy2) + value.y * (yz2 + wx2) + value.z * (Fp.One - xx2 - yy2),
                Fp.One);
        }

        /// <summary>
        /// Transforms a vector by the given Quaternion rotation value.
        /// </summary>
        /// <param name="value">The source vector to be rotated.</param>
        /// <param name="rotation">The rotation to apply.</param>
        /// <returns>The transformed vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4 Transform(Fp4 value, QuaternionFp rotation)
        {
            Fp x2 = rotation.value.x + rotation.value.x;
            Fp y2 = rotation.value.y + rotation.value.y;
            Fp z2 = rotation.value.z + rotation.value.z;

            Fp wx2 = rotation.value.w * x2;
            Fp wy2 = rotation.value.w * y2;
            Fp wz2 = rotation.value.w * z2;
            Fp xx2 = rotation.value.x * x2;
            Fp xy2 = rotation.value.x * y2;
            Fp xz2 = rotation.value.x * z2;
            Fp yy2 = rotation.value.y * y2;
            Fp yz2 = rotation.value.y * z2;
            Fp zz2 = rotation.value.z * z2;

            return new Fp4(
                value.x * (Fp.One - yy2 - zz2) + value.y * (xy2 - wz2) + value.z * (xz2 + wy2),
                value.x * (xy2 + wz2) + value.y * (Fp.One - xx2 - zz2) + value.z * (yz2 - wx2),
                value.x * (xz2 - wy2) + value.y * (yz2 + wx2) + value.z * (Fp.One - xx2 - yy2),
                value.w);
        }

        #endregion Public Static Methods

        #region Public operator methods

        // All these methods should be inlines as they are implemented
        // over JIT intrinsics

        /// <summary>
        /// Adds two vectors together.
        /// </summary>
        /// <param name="left">The first source vector.</param>
        /// <param name="right">The second source vector.</param>
        /// <returns>The summed vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4 Add(Fp4 left, Fp4 right)
        {
            return left + right;
        }

        /// <summary>
        /// Subtracts the second vector from the first.
        /// </summary>
        /// <param name="left">The first source vector.</param>
        /// <param name="right">The second source vector.</param>
        /// <returns>The difference vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4 Subtract(Fp4 left, Fp4 right)
        {
            return left - right;
        }

        /// <summary>
        /// Multiplies two vectors together.
        /// </summary>
        /// <param name="left">The first source vector.</param>
        /// <param name="right">The second source vector.</param>
        /// <returns>The product vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4 Multiply(Fp4 left, Fp4 right)
        {
            return left * right;
        }

        /// <summary>
        /// Multiplies a vector by the given scalar.
        /// </summary>
        /// <param name="left">The source vector.</param>
        /// <param name="right">The scalar value.</param>
        /// <returns>The scaled vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4 Multiply(Fp4 left, Fp right)
        {
            return left * new Fp4(right, right, right, right);
        }

        /// <summary>
        /// Multiplies a vector by the given scalar.
        /// </summary>
        /// <param name="left">The scalar value.</param>
        /// <param name="right">The source vector.</param>
        /// <returns>The scaled vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4 Multiply(Fp left, Fp4 right)
        {
            return new Fp4(left, left, left, left) * right;
        }

        /// <summary>
        /// Divides the first vector by the second.
        /// </summary>
        /// <param name="left">The first source vector.</param>
        /// <param name="right">The second source vector.</param>
        /// <returns>The vector resulting from the division.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4 Divide(Fp4 left, Fp4 right)
        {
            return left / right;
        }

        /// <summary>
        /// Divides the vector by the given scalar.
        /// </summary>
        /// <param name="left">The source vector.</param>
        /// <param name="divisor">The scalar value.</param>
        /// <returns>The result of the division.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4 Divide(Fp4 left, Fp divisor)
        {
            return left / divisor;
        }

        /// <summary>
        /// Negates a given vector.
        /// </summary>
        /// <param name="value">The source vector.</param>
        /// <returns>The negated vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4 Negate(Fp4 value)
        {
            return -value;
        }

        #endregion Public operator methods

        #region Interface

        /// <summary>Returns a string representation of the Fp4.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return string.Format("<{0}, {1}, {2}, {3}>", x, y, z, w);
        }

        /// <summary>Returns a string representation of the Fp4 using a specified format and culture-specific format information.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(string format, IFormatProvider formatProvider)
        {
            return string.Format("<{0}, {1}, {2}, {3}>", x.ToString(format, formatProvider), y.ToString(format, formatProvider), z.ToString(format, formatProvider), w.ToString(format, formatProvider));
        }
        #endregion
    }
}