using System;
using System.Runtime.CompilerServices;

namespace Unity.Mathematics.FixedPoint
{
    /// <summary>
    /// 对 System.Numerics.Vector2 类的复制，为兼容定点数替换了部分内容
    /// https://github.com/microsoft/referencesource
    /// </summary>
    public partial struct Fp2
    {
        #region Public Instance Methods

        /// <summary>
        /// Copies the contents of the vector into the given array.
        /// </summary>
        /// <param name="array">The destination array.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void CopyTo(Fp[] array)
        {
            CopyTo(array, 0);
        }

        /// <summary>
        /// Copies the contents of the vector into the given array, starting from the given index.
        /// </summary>
        /// <exception cref="ArgumentNullException">If array is null.</exception>
        /// <exception cref="RankException">If array is multidimensional.</exception>
        /// <exception cref="ArgumentOutOfRangeException">If index is greater than end of the array or index is less than zero.</exception>
        /// <exception cref="ArgumentException">If number of elements in source vector is greater than those available in destination array
        /// or if there are not enough elements to copy.</exception>
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

            if ((array.Length - index) < 2)
            {
                throw new ArgumentException($"Arg_ElementsInSourceIsGreaterThanDestination index : {index}");
            }

            array[index] = x;
            array[index + 1] = y;
        }

        #endregion Public Instance Methods

        #region Public Static Methods

        /// <summary>
        /// Returns the dot product of two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <returns>The dot product.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp Dot(Fp2 value1, Fp2 value2)
        {
            return value1.x * value2.x +
                   value1.y * value2.y;
        }

        /// <summary>
        /// Returns a vector whose elements are the minimum of each of the pairs of elements in the two source vectors.
        /// </summary>
        /// <param name="value1">The first source vector.</param>
        /// <param name="value2">The second source vector.</param>
        /// <returns>The minimized vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp2 Min(Fp2 value1, Fp2 value2)
        {
            return new Fp2(
                (value1.x < value2.x) ? value1.x : value2.x,
                (value1.y < value2.y) ? value1.y : value2.y);
        }

        /// <summary>
        /// Returns a vector whose elements are the maximum of each of the pairs of elements in the two source vectors
        /// </summary>
        /// <param name="value1">The first source vector</param>
        /// <param name="value2">The second source vector</param>
        /// <returns>The maximized vector</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp2 Max(Fp2 value1, Fp2 value2)
        {
            return new Fp2(
                (value1.x > value2.x) ? value1.x : value2.x,
                (value1.y > value2.y) ? value1.y : value2.y);
        }

        /// <summary>
        /// Returns a vector whose elements are the absolute values of each of the source vector's elements.
        /// </summary>
        /// <param name="value">The source vector.</param>
        /// <returns>The absolute value vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp2 Abs(Fp2 value)
        {
            return new Fp2(MathFp.abs(value.x), MathFp.abs(value.y));
        }

        /// <summary>
        /// Returns a vector whose elements are the square root of each of the source vector's elements.
        /// </summary>
        /// <param name="value">The source vector.</param>
        /// <returns>The square root vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp2 SquareRoot(Fp2 value)
        {
            return new Fp2(MathFp.sqrt(value.x), MathFp.sqrt(value.y));
        }

        #endregion Public Static Methods

        #region Public Static Properties

        /// <summary>
        /// Returns the vector (0,0).
        /// </summary>
        public static Fp2 Zero
        {
            get { return new Fp2(); }
        }

        /// <summary>
        /// Returns the vector (1,1).
        /// </summary>
        public static Fp2 One
        {
            get { return new Fp2(Fp.One, Fp.One); }
        }

        /// <summary>
        /// Returns the vector (1,0).
        /// </summary>
        public static Fp2 UnitX
        {
            get { return new Fp2(Fp.One, 0); }
        }

        /// <summary>
        /// Returns the vector (0,1).
        /// </summary>
        public static Fp2 UnitY
        {
            get { return new Fp2(0, Fp.One); }
        }

        #endregion Public Static Properties

        #region Public instance methods

        /// <summary>
        /// Returns the length of the vector.
        /// </summary>
        /// <returns>The vector's length.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Fp Length()
        {
            // if (Vector.IsHardwareAccelerated)
            // {
            //     Fp ls = Fp2.Dot(this, this);
            //     return MathFp.sqrt(ls);
            // }
            // else
            // {
            Fp ls = x * x + y * y;
            return MathFp.sqrt(ls);
            // }
        }

        /// <summary>
        /// Returns the length of the vector squared. This operation is cheaper than Length().
        /// </summary>
        /// <returns>The vector's length squared.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Fp LengthSquared()
        {
            // if (Vector.IsHardwareAccelerated)
            // {
            //     return Fp2.Dot(this, this);
            // }
            // else
            // {
            return x * x + y * y;
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
        public static Fp Distance(Fp2 value1, Fp2 value2)
        {
            // if (Vector.IsHardwareAccelerated)
            // {
            //     Fp2 difference = value1 - value2;
            //     Fp ls = Fp2.Dot(difference, difference);
            //     return MathFp.sqrt(ls);
            // }
            // else
            // {
            Fp dx = value1.x - value2.x;
            Fp dy = value1.y - value2.y;

            Fp ls = dx * dx + dy * dy;

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
        public static Fp DistanceSquared(Fp2 value1, Fp2 value2)
        {
            // if (Vector.IsHardwareAccelerated)
            // {
            //     Fp2 difference = value1 - value2;
            //     return Fp2.Dot(difference, difference);
            // }
            // else
            // {
            Fp dx = value1.x - value2.x;
            Fp dy = value1.y - value2.y;

            return dx * dx + dy * dy;
            // }
        }

        /// <summary>
        /// Returns a vector with the same direction as the given vector, but with a length of 1.
        /// </summary>
        /// <param name="value">The vector to normalize.</param>
        /// <returns>The normalized vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp2 Normalize(Fp2 value)
        {
            // if (Vector.IsHardwareAccelerated)
            // {
            //     Fp length = value.Length();
            //     return value / length;
            // }
            // else
            // {
            Fp ls = value.x * value.x + value.y * value.y;
            Fp invNorm = Fp.One / MathFp.sqrt(ls);

            return new Fp2(
                value.x * invNorm,
                value.y * invNorm);
            // }
        }

        /// <summary>
        /// Returns the reflection of a vector off a surface that has the specified normal.
        /// </summary>
        /// <param name="vector">The source vector.</param>
        /// <param name="normal">The normal of the surface being reflected off.</param>
        /// <returns>The reflected vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp2 Reflect(Fp2 vector, Fp2 normal)
        {
            // if (Vector.IsHardwareAccelerated)
            // {
            //     Fp dot = Fp2.Dot(vector, normal);
            //     return vector - (2 * dot * normal);
            // }
            // else
            // {
            Fp dot = vector.x * normal.x + vector.y * normal.y;

            return new Fp2(
                vector.x - Fp.Two * dot * normal.x,
                vector.y - Fp.Two * dot * normal.y);
            // }
        }

        /// <summary>
        /// Restricts a vector between a min and max value.
        /// </summary>
        /// <param name="value1">The source vector.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp2 Clamp(Fp2 value1, Fp2 min, Fp2 max)
        {
            // This compare order is very important!!!
            // We must follow HLSL behavior in the case user specified min value is bigger than max value.
            Fp x = value1.x;
            x = (x > max.x) ? max.x : x;
            x = (x < min.x) ? min.x : x;

            Fp y = value1.y;
            y = (y > max.y) ? max.y : y;
            y = (y < min.y) ? min.y : y;

            return new Fp2(x, y);
        }

        /// <summary>
        /// Linearly interpolates between two vectors based on the given weighting.
        /// </summary>
        /// <param name="value1">The first source vector.</param>
        /// <param name="value2">The second source vector.</param>
        /// <param name="amount">Value between 0 and 1 indicating the weight of the second source vector.</param>
        /// <returns>The interpolated vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp2 Lerp(Fp2 value1, Fp2 value2, Fp amount)
        {
            return new Fp2(
                value1.x + (value2.x - value1.x) * amount,
                value1.y + (value2.y - value1.y) * amount);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp2 Transform(in Fp2 v, in Fp2x2 m)
        {
            return new Fp2(
                v.x * m.c0.x + v.y * m.c0.y,
                v.x * m.c1.x + v.y * m.c1.y);
        }

        /// <summary>
        /// Transforms a vector by the given matrix.
        /// </summary>
        /// <param name="position">The source vector.</param>
        /// <param name="matrix">The transformation matrix.</param>
        /// <returns>The transformed vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp2 Transform(Fp2 position, Fp3x2 matrix)
        {
            return new Fp2(
                position.x * matrix.c0.x + position.y * matrix.c0.y + matrix.c0.z,
                position.x * matrix.c1.x + position.y * matrix.c1.y + matrix.c1.z);
        }

        /// <summary>
        /// Transforms a vector by the given matrix.
        /// </summary>
        /// <param name="position">The source vector.</param>
        /// <param name="matrix">The transformation matrix.</param>
        /// <returns>The transformed vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp2 Transform(Fp2 position, Fp4x4 matrix)
        {
            return new Fp2(
                position.x * matrix.c0.x + position.y * matrix.c0.y + matrix.c0.w,
                position.x * matrix.c1.x + position.y * matrix.c1.y + matrix.c1.w);
        }

        /// <summary>
        /// Transforms a vector normal by the given matrix.
        /// </summary>
        /// <param name="normal">The source vector.</param>
        /// <param name="matrix">The transformation matrix.</param>
        /// <returns>The transformed vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp2 TransformNormal(Fp2 normal, Fp3x2 matrix)
        {
            return new Fp2(
                normal.x * matrix.c0.x + normal.y * matrix.c0.y,
                normal.x * matrix.c1.x + normal.y * matrix.c1.y);
        }

        /// <summary>
        /// Transforms a vector normal by the given matrix.
        /// </summary>
        /// <param name="normal">The source vector.</param>
        /// <param name="matrix">The transformation matrix.</param>
        /// <returns>The transformed vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp2 TransformNormal(Fp2 normal, Fp4x4 matrix)
        {
            return new Fp2(
                normal.x * matrix.c0.x + normal.y * matrix.c0.y,
                normal.x * matrix.c1.x + normal.y * matrix.c1.y);
        }

        /// <summary>
        /// Transforms a vector by the given Quaternion rotation value.
        /// </summary>
        /// <param name="value">The source vector to be rotated.</param>
        /// <param name="rotation">The rotation to apply.</param>
        /// <returns>The transformed vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp2 Transform(Fp2 value, QuaternionFp rotation)
        {
            Fp x2 = rotation.value.x + rotation.value.x;
            Fp y2 = rotation.value.y + rotation.value.y;
            Fp z2 = rotation.value.z + rotation.value.z;

            Fp wz2 = rotation.value.w * z2;
            Fp xx2 = rotation.value.x * x2;
            Fp xy2 = rotation.value.x * y2;
            Fp yy2 = rotation.value.y * y2;
            Fp zz2 = rotation.value.z * z2;

            return new Fp2(
                value.x * (Fp.One - yy2 - zz2) + value.y * (xy2 - wz2),
                value.x * (xy2 + wz2) + value.y * (Fp.One - xx2 - zz2));
        }

        #endregion Public Static Methods

        #region Public operator methods

        // all the below methods should be inlined as they are
        // implemented over JIT intrinsics

        /// <summary>
        /// Adds two vectors together.
        /// </summary>
        /// <param name="left">The first source vector.</param>
        /// <param name="right">The second source vector.</param>
        /// <returns>The summed vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp2 Add(Fp2 left, Fp2 right)
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
        public static Fp2 Subtract(Fp2 left, Fp2 right)
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
        public static Fp2 Multiply(Fp2 left, Fp2 right)
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
        public static Fp2 Multiply(Fp2 left, Fp right)
        {
            return left * right;
        }

        /// <summary>
        /// Multiplies a vector by the given scalar.
        /// </summary>
        /// <param name="left">The scalar value.</param>
        /// <param name="right">The source vector.</param>
        /// <returns>The scaled vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp2 Multiply(Fp left, Fp2 right)
        {
            return left * right;
        }

        /// <summary>
        /// Divides the first vector by the second.
        /// </summary>
        /// <param name="left">The first source vector.</param>
        /// <param name="right">The second source vector.</param>
        /// <returns>The vector resulting from the division.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp2 Divide(Fp2 left, Fp2 right)
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
        public static Fp2 Divide(Fp2 left, Fp divisor)
        {
            return left / divisor;
        }

        /// <summary>
        /// Negates a given vector.
        /// </summary>
        /// <param name="value">The source vector.</param>
        /// <returns>The negated vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp2 Negate(Fp2 value)
        {
            return -value;
        }

        #endregion Public operator methods

        #region Interface

        /// <summary>Returns a string representation of the Fp2.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return string.Format("<{0}, {1}>", x, y);
        }

        /// <summary>Returns a string representation of the Fp2 using a specified format and culture-specific format information.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(string format, IFormatProvider formatProvider)
        {
            return string.Format("<{0}, {1}>", x.ToString(format, formatProvider),
                y.ToString(format, formatProvider));
        }

        #endregion
    }
}