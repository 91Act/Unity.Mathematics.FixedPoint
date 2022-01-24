using System;
using System.Runtime.CompilerServices;

namespace Unity.Mathematics.FixedPoint
{
    /// <summary>
    /// Fp3 扩展方法
    /// </summary>
    public partial struct Fp3
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

            if ((array.Length - index) < 3)
            {
                throw new ArgumentException($"Arg_ElementsInSourceIsGreaterThanDestination index : {index}");
            }

            array[index] = x;
            array[index + 1] = y;
            array[index + 2] = z;
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
        public static Fp Dot(Fp3 vector1, Fp3 Fp2)
        {
            return vector1.x * Fp2.x +
                   vector1.y * Fp2.y +
                   vector1.z * Fp2.z;
        }

        /// <summary>
        /// Returns a vector whose elements are the minimum of each of the pairs of elements in the two source vectors.
        /// </summary>
        /// <param name="value1">The first source vector.</param>
        /// <param name="value2">The second source vector.</param>
        /// <returns>The minimized vector.</returns>
        public static Fp3 Min(Fp3 value1, Fp3 value2)
        {
            return new Fp3(
                (value1.x < value2.x) ? value1.x : value2.x,
                (value1.y < value2.y) ? value1.y : value2.y,
                (value1.z < value2.z) ? value1.z : value2.z);
        }

        /// <summary>
        /// Returns a vector whose elements are the maximum of each of the pairs of elements in the two source vectors.
        /// </summary>
        /// <param name="value1">The first source vector.</param>
        /// <param name="value2">The second source vector.</param>
        /// <returns>The maximized vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3 Max(Fp3 value1, Fp3 value2)
        {
            return new Fp3(
                (value1.x > value2.x) ? value1.x : value2.x,
                (value1.y > value2.y) ? value1.y : value2.y,
                (value1.z > value2.z) ? value1.z : value2.z);
        }

        /// <summary>
        /// Returns a vector whose elements are the absolute values of each of the source vector's elements.
        /// </summary>
        /// <param name="value">The source vector.</param>
        /// <returns>The absolute value vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3 Abs(Fp3 value)
        {
            return new Fp3(MathFp.abs(value.x), MathFp.abs(value.y), MathFp.abs(value.z));
        }

        /// <summary>
        /// Returns a vector whose elements are the square root of each of the source vector's elements.
        /// </summary>
        /// <param name="value">The source vector.</param>
        /// <returns>The square root vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3 SquareRoot(Fp3 value)
        {
            return new Fp3(MathFp.sqrt(value.x), MathFp.sqrt(value.y), MathFp.sqrt(value.z));
        }

        #endregion Public Static Methods

        #region Public Static Properties

        /// <summary>
        /// Returns the vector (0,0,0).
        /// </summary>
        public static Fp3 Zero
        {
            get { return new Fp3(); }
        }

        /// <summary>
        /// Returns the vector (1,1,1).
        /// </summary>
        public static Fp3 One
        {
            get { return new Fp3(Fp.One, Fp.One, Fp.One); }
        }

        /// <summary>
        /// Returns the vector (1,0,0).
        /// </summary>
        public static Fp3 UnitX
        {
            get { return new Fp3(Fp.One, 0, 0); }
        }

        /// <summary>
        /// Returns the vector (0,1,0).
        /// </summary>
        public static Fp3 UnitY
        {
            get { return new Fp3(0, Fp.One, 0); }
        }

        /// <summary>
        /// Returns the vector (0,0,1).
        /// </summary>
        public static Fp3 UnitZ
        {
            get { return new Fp3(0, 0, Fp.One); }
        }

        #endregion Public Static Properties

        #region Public Instance Methods

        /// <summary>
        /// Returns the length of the vector.
        /// </summary>
        /// <returns>The vector's length.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Fp Length()
        {
            // if (Vector.IsHardwareAccelerated)
            // {
            //     Fp ls = Fp3.Dot(this, this);
            //     return (Fp)System.MathFp.sqrt(ls);
            // }
            // else
            // {
            Fp ls = x * x + y * y + z * z;
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
            //     return Fp3.Dot(this, this);
            // }
            // else
            // {
            return x * x + y * y + z * z;
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
        public static Fp Distance(Fp3 value1, Fp3 value2)
        {
            // if (Vector.IsHardwareAccelerated)
            // {
            //     Fp3 difference = value1 - value2;
            //     Fp ls = Fp3.Dot(difference, difference);
            //     return (Fp)System.MathFp.sqrt(ls);
            // }
            // else
            // {
            Fp dx = value1.x - value2.x;
            Fp dy = value1.y - value2.y;
            Fp dz = value1.z - value2.z;

            Fp ls = dx * dx + dy * dy + dz * dz;

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
        public static Fp DistanceSquared(Fp3 value1, Fp3 value2)
        {
            // if (Vector.IsHardwareAccelerated)
            // {
            //     Fp3 difference = value1 - value2;
            //     return Fp3.Dot(difference, difference);
            // }
            // else
            // {
            Fp dx = value1.x - value2.x;
            Fp dy = value1.y - value2.y;
            Fp dz = value1.z - value2.z;

            return dx * dx + dy * dy + dz * dz;
            // }
        }

        /// <summary>
        /// Returns a vector with the same direction as the given vector, but with a length of 1.
        /// </summary>
        /// <param name="value">The vector to normalize.</param>
        /// <returns>The normalized vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3 Normalize(Fp3 value)
        {
            // if (Vector.IsHardwareAccelerated)
            // {
            //     Fp length = value.Length();
            //     return value / length;
            // }
            // else
            // {
            Fp ls = value.x * value.x + value.y * value.y + value.z * value.z;
            Fp length = MathFp.sqrt(ls);
            return new Fp3(value.x / length, value.y / length, value.z / length);
            // }
        }

        /// <summary>
        /// Computes the cross product of two vectors.
        /// </summary>
        /// <param name="vector1">The first vector.</param>
        /// <param name="vector2">The second vector.</param>
        /// <returns>The cross product.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3 Cross(Fp3 vector1, Fp3 vector2)
        {
            return new Fp3(
                vector1.y * vector2.z - vector1.z * vector2.y,
                vector1.z * vector2.x - vector1.x * vector2.z,
                vector1.x * vector2.y - vector1.y * vector2.x);
        }

        /// <summary>
        /// Returns the reflection of a vector off a surface that has the specified normal.
        /// </summary>
        /// <param name="vector">The source vector.</param>
        /// <param name="normal">The normal of the surface being reflected off.</param>
        /// <returns>The reflected vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3 Reflect(Fp3 vector, Fp3 normal)
        {
            // if (Vector.IsHardwareAccelerated)
            // {
            //     Fp dot = Fp3.Dot(vector, normal);
            //     Fp3 temp = normal * dot * Fp.Two;
            //     return vector - temp;
            // }
            // else
            // {
            Fp dot = vector.x * normal.x + vector.y * normal.y + vector.z * normal.z;
            Fp tempX = normal.x * dot * Fp.Two;
            Fp tempY = normal.y * dot * Fp.Two;
            Fp tempZ = normal.z * dot * Fp.Two;
            return new Fp3(vector.x - tempX, vector.y - tempY, vector.z - tempZ);
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
        public static Fp3 Clamp(Fp3 value1, Fp3 min, Fp3 max)
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

            return new Fp3(x, y, z);
        }

        /// <summary>
        /// Linearly interpolates between two vectors based on the given weighting.
        /// </summary>
        /// <param name="value1">The first source vector.</param>
        /// <param name="value2">The second source vector.</param>
        /// <param name="amount">Value between 0 and 1 indicating the weight of the second source vector.</param>
        /// <returns>The interpolated vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3 Lerp(Fp3 value1, Fp3 value2, Fp amount)
        {
            // if (Vector.IsHardwareAccelerated)
            // {
            //     Fp3 firstInfluence = value1 * (1f - amount);
            //     Fp3 secondInfluence = value2 * amount;
            //     return firstInfluence + secondInfluence;
            // }
            // else
            // {
            return new Fp3(
                value1.x + (value2.x - value1.x) * amount,
                value1.y + (value2.y - value1.y) * amount,
                value1.z + (value2.z - value1.z) * amount);
            // }
        }

        /// <summary>
        /// Transforms a vector by the given matrix.
        /// </summary>
        /// <param name="position">The source vector.</param>
        /// <param name="matrix">The transformation matrix.</param>
        /// <returns>The transformed vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3 Transform(Fp3 position, Fp4x4 matrix)
        {
            return new Fp3(
                position.x * matrix.c0.x + position.y * matrix.c0.y + position.z * matrix.c0.z + matrix.c0.w,
                position.x * matrix.c1.x + position.y * matrix.c1.y + position.z * matrix.c1.z + matrix.c1.w,
                position.x * matrix.c2.x + position.y * matrix.c2.y + position.z * matrix.c2.z + matrix.c2.w);
        }

        /// <summary>
        /// Transforms a vector normal by the given matrix.
        /// </summary>
        /// <param name="normal">The source vector.</param>
        /// <param name="matrix">The transformation matrix.</param>
        /// <returns>The transformed vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3 TransformNormal(Fp3 normal, Fp4x4 matrix)
        {
            return new Fp3(
                normal.x * matrix.c0.x + normal.y * matrix.c0.y + normal.z * matrix.c0.z,
                normal.x * matrix.c1.x + normal.y * matrix.c1.y + normal.z * matrix.c1.z,
                normal.x * matrix.c2.x + normal.y * matrix.c2.y + normal.z * matrix.c2.z);
        }

        /// <summary>
        /// Transforms the vector by the matrix.
        /// </summary>
        /// <param name="v">Fp3 to transform.</param>
        /// <param name="m">Matrix to use as the transformation.</param>
        /// <returns>Product of the transformation.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3 Transform(in Fp3 v, in Fp3x3 m)
        {
            return new Fp3(
                v.x * m.c0.x + v.y * m.c0.y + v.z * m.c0.z,
                v.x * m.c1.x + v.y * m.c1.y + v.z * m.c1.z,
                v.x * m.c2.x + v.y * m.c2.y + v.z * m.c2.z);
        }

        /// <summary>
        /// Transforms the vector by the matrix's transpose.
        /// </summary>
        /// <param name="v">Fp3 to transform.</param>
        /// <param name="m">Matrix to use as the transformation transpose.</param>
        /// <returns>Product of the transformation.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3 TransformTranspose(in Fp3 v, in Fp3x3 m)
        {
            var transpose = MathFp.transpose(m);
            return new Fp3(
                Fp3.Dot(v, transpose.c0),
                Fp3.Dot(v, transpose.c1),
                Fp3.Dot(v, transpose.c2));
        }

        /// <summary>
        /// Transforms a vector by the given Quaternion rotation value.
        /// </summary>
        /// <param name="value">The source vector to be rotated.</param>
        /// <param name="rotation">The rotation to apply.</param>
        /// <returns>The transformed vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3 Transform(Fp3 value, QuaternionFp rotation)
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

            return new Fp3(
                value.x * (Fp.One - yy2 - zz2) + value.y * (xy2 - wz2) + value.z * (xz2 + wy2),
                value.x * (xy2 + wz2) + value.y * (Fp.One - xx2 - zz2) + value.z * (yz2 - wx2),
                value.x * (xz2 - wy2) + value.y * (yz2 + wx2) + value.z * (Fp.One - xx2 - yy2));
        }

        #endregion Public Static Methods

        #region Public operator methods

        // All these methods should be inlined as they are implemented
        // over JIT intrinsics

        /// <summary>
        /// Adds two vectors together.
        /// </summary>
        /// <param name="left">The first source vector.</param>
        /// <param name="right">The second source vector.</param>
        /// <returns>The summed vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3 Add(Fp3 left, Fp3 right)
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
        public static Fp3 Subtract(Fp3 left, Fp3 right)
        {
            return left - right;
        }

        /// <summary>
        /// Divides the first vector by the second.
        /// </summary>
        /// <param name="left">The first source vector.</param>
        /// <param name="right">The second source vector.</param>
        /// <returns>The vector resulting from the division.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3 Divide(Fp3 left, Fp3 right)
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
        public static Fp3 Divide(Fp3 left, Fp divisor)
        {
            return left / divisor;
        }

        /// <summary>
        /// Negates a given vector.
        /// </summary>
        /// <param name="value">The source vector.</param>
        /// <returns>The negated vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3 Negate(Fp3 value)
        {
            return -value;
        }

        #endregion Public operator methods

        #region Interface

        /// <summary>Returns a string representation of the Fp3.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return string.Format("<{0}, {1}, {2}>", x, y, z);
        }

        /// <summary>Returns a string representation of the Fp3 using a specified format and culture-specific format information.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(string format, IFormatProvider formatProvider)
        {
            return string.Format("<{0}, {1}, {2}>", x.ToString(format, formatProvider),
                y.ToString(format, formatProvider), z.ToString(format, formatProvider));
        }

        #endregion

        #region Public Static Function From Unity

        // Returns the angle in degrees between /from/ and /to/. This is always the smallest
        public static Fp Angle(Fp3 from, Fp3 to)
        {
            // sqrt(a) * sqrt(b) = sqrt(a * b) -- valid for real numbers
            Fp denominator = MathFp.sqrt(from.LengthSquared() * to.LengthSquared());
            if (denominator < Fp.OneEMinus8)
                return 0;

            Fp dot = MathFp.clamp(Fp3.Dot(from, to) / denominator, -Fp.One, Fp.One);
            return (MathFp.acos(dot)) * Fp.PiOver180Inverse;
        }

        #endregion
    }
}