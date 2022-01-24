using System;

namespace Unity.Mathematics.FixedPoint
{
    /// <summary>
    /// 对 System.Numerics.Matrix3x2 类的复制，为兼容定点数替换了部分内容
    /// https://github.com/microsoft/referencesource
    /// </summary>
    public partial struct Fp3x2
    {
        private static readonly Fp3x2 _identity = new Fp3x2
        (
            Fp.One, 0,
            0, Fp.One,
            0, 0
        );

        /// <summary>
        /// Returns the multiplicative identity matrix.
        /// </summary>
        public static Fp3x2 Identity
        {
            get { return _identity; }
        }

        /// <summary>
        /// Returns whether the matrix is the identity matrix.
        /// </summary>
        public bool IsIdentity
        {
            get
            {
                return c0.x == Fp.One && c1.y == Fp.One && // Check diagonal element first for early out.
                       c1.x == 0 &&
                       c0.y == 0 &&
                       c0.z == 0 && c1.z == 0;
            }
        }

        /// <summary>
        /// Gets or sets the translation component of this matrix.
        /// </summary>
        public Fp2 Translation
        {
            get { return new Fp2(c0.z, c1.z); }

            set
            {
                c0.z = value.x;
                c1.z = value.y;
            }
        }

        /// <summary>
        /// Creates a translation matrix from the given vector.
        /// </summary>
        /// <param name="position">The translation position.</param>
        /// <returns>A translation matrix.</returns>
        public static Fp3x2 CreateTranslation(Fp2 position)
        {
            Fp3x2 result = new Fp3x2();

            result.c0.x = Fp.One;
            result.c1.x = 0;
            result.c0.y = 0;
            result.c1.y = Fp.One;

            result.c0.z = position.x;
            result.c1.z = position.y;

            return result;
        }

        /// <summary>
        /// Creates a translation matrix from the given x and y components.
        /// </summary>
        /// <param name="xPosition">The x position.</param>
        /// <param name="yPosition">The y position.</param>
        /// <returns>A translation matrix.</returns>
        public static Fp3x2 CreateTranslation(Fp xPosition, Fp yPosition)
        {
            Fp3x2 result = new Fp3x2();

            result.c0.x = Fp.One;
            result.c1.x = 0;
            result.c0.y = 0;
            result.c1.y = Fp.One;

            result.c0.z = xPosition;
            result.c1.z = yPosition;

            return result;
        }

        /// <summary>
        /// Creates a scale matrix from the given x and y components.
        /// </summary>
        /// <param name="xScale">Value to scale by on the x-axis.</param>
        /// <param name="yScale">Value to scale by on the y-axis.</param>
        /// <returns>A scaling matrix.</returns>
        public static Fp3x2 CreateScale(Fp xScale, Fp yScale)
        {
            Fp3x2 result = new Fp3x2();

            result.c0.x = xScale;
            result.c1.x = 0;
            result.c0.y = 0;
            result.c1.y = yScale;
            result.c0.z = 0;
            result.c1.z = 0;

            return result;
        }

        /// <summary>
        /// Creates a scale matrix that is offset by a given center point.
        /// </summary>
        /// <param name="xScale">Value to scale by on the x-axis.</param>
        /// <param name="yScale">Value to scale by on the y-axis.</param>
        /// <param name="centerPoint">The center point.</param>
        /// <returns>A scaling matrix.</returns>
        public static Fp3x2 CreateScale(Fp xScale, Fp yScale, Fp2 centerPoint)
        {
            Fp3x2 result = new Fp3x2();

            Fp tx = centerPoint.x * (1 - xScale);
            Fp ty = centerPoint.y * (1 - yScale);

            result.c0.x = xScale;
            result.c1.x = 0;
            result.c0.y = 0;
            result.c1.y = yScale;
            result.c0.z = tx;
            result.c1.z = ty;

            return result;
        }

        /// <summary>
        /// Creates a scale matrix from the given vector scale.
        /// </summary>
        /// <param name="scales">The scale to use.</param>
        /// <returns>A scaling matrix.</returns>
        public static Fp3x2 CreateScale(Fp2 scales)
        {
            Fp3x2 result = new Fp3x2();

            result.c0.x = scales.x;
            result.c1.x = 0;
            result.c0.y = 0;
            result.c1.y = scales.y;
            result.c0.z = 0;
            result.c1.z = 0;

            return result;
        }

        /// <summary>
        /// Creates a scale matrix from the given vector scale with an offset from the given center point.
        /// </summary>
        /// <param name="scales">The scale to use.</param>
        /// <param name="centerPoint">The center offset.</param>
        /// <returns>A scaling matrix.</returns>
        public static Fp3x2 CreateScale(Fp2 scales, Fp2 centerPoint)
        {
            Fp3x2 result = new Fp3x2();

            Fp tx = centerPoint.x * (1 - scales.x);
            Fp ty = centerPoint.y * (1 - scales.y);

            result.c0.x = scales.x;
            result.c1.x = 0;
            result.c0.y = 0;
            result.c1.y = scales.y;
            result.c0.z = tx;
            result.c1.z = ty;

            return result;
        }

        /// <summary>
        /// Creates a scale matrix that scales uniformly with the given scale.
        /// </summary>
        /// <param name="scale">The uniform scale to use.</param>
        /// <returns>A scaling matrix.</returns>
        public static Fp3x2 CreateScale(Fp scale)
        {
            Fp3x2 result = new Fp3x2();

            result.c0.x = scale;
            result.c1.x = 0;
            result.c0.y = 0;
            result.c1.y = scale;
            result.c0.z = 0;
            result.c1.z = 0;

            return result;
        }

        /// <summary>
        /// Creates a scale matrix that scales uniformly with the given scale with an offset from the given center.
        /// </summary>
        /// <param name="scale">The uniform scale to use.</param>
        /// <param name="centerPoint">The center offset.</param>
        /// <returns>A scaling matrix.</returns>
        public static Fp3x2 CreateScale(Fp scale, Fp2 centerPoint)
        {
            Fp3x2 result = new Fp3x2();

            Fp tx = centerPoint.x * (1 - scale);
            Fp ty = centerPoint.y * (1 - scale);

            result.c0.x = scale;
            result.c1.x = 0;
            result.c0.y = 0;
            result.c1.y = scale;
            result.c0.z = tx;
            result.c1.z = ty;

            return result;
        }

        /// <summary>
        /// Creates a skew matrix from the given angles in radians.
        /// </summary>
        /// <param name="radiansX">The x angle, in radians.</param>
        /// <param name="radiansY">The y angle, in radians.</param>
        /// <returns>A skew matrix.</returns>
        public static Fp3x2 CreateSkew(Fp radiansX, Fp radiansY)
        {
            Fp3x2 result = new Fp3x2();

            Fp xTan = MathFp.tan(radiansX);
            Fp yTan = MathFp.tan(radiansY);

            result.c0.x = Fp.One;
            result.c1.x = yTan;
            result.c0.y = xTan;
            result.c1.y = Fp.One;
            result.c0.z = 0;
            result.c1.z = 0;

            return result;
        }

        /// <summary>
        /// Creates a skew matrix from the given angles in radians and a center point.
        /// </summary>
        /// <param name="radiansX">The x angle, in radians.</param>
        /// <param name="radiansY">The y angle, in radians.</param>
        /// <param name="centerPoint">The center point.</param>
        /// <returns>A skew matrix.</returns>
        public static Fp3x2 CreateSkew(Fp radiansX, Fp radiansY, Fp2 centerPoint)
        {
            Fp3x2 result = new Fp3x2();

            Fp xTan = MathFp.tan(radiansX);
            Fp yTan = MathFp.tan(radiansY);

            Fp tx = -centerPoint.y * xTan;
            Fp ty = -centerPoint.x * yTan;

            result.c0.x = Fp.One;
            result.c1.x = yTan;
            result.c0.y = xTan;
            result.c1.y = Fp.One;
            result.c0.z = tx;
            result.c1.z = ty;

            return result;
        }

        /// <summary>
        /// Creates a rotation matrix using the given rotation in radians.
        /// </summary>
        /// <param name="radians">The amount of rotation, in radians.</param>
        /// <returns>A rotation matrix.</returns>
        public static Fp3x2 CreateRotation(Fp radians)
        {
            // ** IEEERemainder NotImplemented
            // ** see https://stackoverflow.com/questions/1971645/is-math-ieeeremainderx-y-equivalent-to-xy
            throw new NotImplementedException();
            // Fp3x2 result = new Fp3x2();
            //
            // radians = (Fp)MathFp.IEEERemainder(radians, Fp.Pi);
            //
            // Fp c, s;
            //
            // const Fp epsilon = 0.001f * (Fp)MathFp.PI / 180f; // 0.1% of a degree
            //
            // if (radians > -epsilon && radians < epsilon)
            // {
            //     // Exact case for zero rotation.
            //     c = 1;
            //     s = 0;
            // }
            // else if (radians > MathFp.PI / 2 - epsilon && radians < MathFp.PI / 2 + epsilon)
            // {
            //     // Exact case for 90 degree rotation.
            //     c = 0;
            //     s = 1;
            // }
            // else if (radians < -MathFp.PI + epsilon || radians > MathFp.PI - epsilon)
            // {
            //     // Exact case for 180 degree rotation.
            //     c = -1;
            //     s = 0;
            // }
            // else if (radians > -MathFp.PI / 2 - epsilon && radians < -MathFp.PI / 2 + epsilon)
            // {
            //     // Exact case for 270 degree rotation.
            //     c = 0;
            //     s = -1;
            // }
            // else
            // {
            //     // Arbitrary rotation.
            //     c = (Fp)MathFp.Cos(radians);
            //     s = (Fp)MathFp.Sin(radians);
            // }
            //
            // // [  c  s ]
            // // [ -s  c ]
            // // [  0  0 ]
            // result.M11 = c;
            // result.M12 = s;
            // result.M21 = -s;
            // result.M22 = c;
            // result.M31 = 0;
            // result.M32 = 0;
            //
            // return result;
        }

        /// <summary>
        /// Creates a rotation matrix using the given rotation in radians and a center point.
        /// </summary>
        /// <param name="radians">The amount of rotation, in radians.</param>
        /// <param name="centerPoint">The center point.</param>
        /// <returns>A rotation matrix.</returns>
        public static Fp3x2 CreateRotation(Fp radians, Fp2 centerPoint)
        {
            // ** IEEERemainder NotImplemented
            // ** see https://stackoverflow.com/questions/1971645/is-math-ieeeremainderx-y-equivalent-to-xy
            throw new NotImplementedException();
            // Fp3x2 result = new Fp3x2();
            //
            // radians = (Fp)MathFp.IEEERemainder(radians, MathFp.PI * 2);
            //
            // Fp c, s;
            //
            // const Fp epsilon = 0.001f * (Fp)MathFp.PI / 180f; // 0.1% of a degree
            //
            // if (radians > -epsilon && radians < epsilon)
            // {
            //     // Exact case for zero rotation.
            //     c = 1;
            //     s = 0;
            // }
            // else if (radians > MathFp.PI / 2 - epsilon && radians < MathFp.PI / 2 + epsilon)
            // {
            //     // Exact case for 90 degree rotation.
            //     c = 0;
            //     s = 1;
            // }
            // else if (radians < -MathFp.PI + epsilon || radians > MathFp.PI - epsilon)
            // {
            //     // Exact case for 180 degree rotation.
            //     c = -1;
            //     s = 0;
            // }
            // else if (radians > -MathFp.PI / 2 - epsilon && radians < -MathFp.PI / 2 + epsilon)
            // {
            //     // Exact case for 270 degree rotation.
            //     c = 0;
            //     s = -1;
            // }
            // else
            // {
            //     // Arbitrary rotation.
            //     c = (Fp)MathFp.Cos(radians);
            //     s = (Fp)MathFp.Sin(radians);
            // }
            //
            // Fp x = centerPoint.x * (1 - c) + centerPoint.y * s;
            // Fp y = centerPoint.y * (1 - c) - centerPoint.x * s;
            //
            // // [  c  s ]
            // // [ -s  c ]
            // // [  x  y ]
            // result.M11 = c;
            // result.M12 = s;
            // result.M21 = -s;
            // result.M22 = c;
            // result.M31 = x;
            // result.M32 = y;
            //
            // return result;
        }

        /// <summary>
        /// Calculates the determinant for this matrix.
        /// The determinant is calculated by expanding the matrix with a third column whose values are (0,0,1).
        /// </summary>
        /// <returns>The determinant.</returns>
        public Fp GetDeterminant()
        {
            // There isn't actually any such thing as a determinant for a non-square matrix,
            // but this 3x2 type is really just an optimization of a 3x3 where we happen to
            // know the rightmost column is always (0, 0, 1). So we expand to 3x3 format:
            //
            //  [ M11, M12, 0 ]
            //  [ M21, M22, 0 ]
            //  [ M31, M32, 1 ]
            //
            // Sum the diagonal products:
            //  (M11 * M22 * 1) + (M12 * 0 * M31) + (0 * M21 * M32)
            //
            // Subtract the opposite diagonal products:
            //  (M31 * M22 * 0) + (M32 * 0 * M11) + (1 * M21 * M12)
            //
            // Collapse out the constants and oh look, this is just a 2x2 determinant!

            return (c0.x * c1.y) - (c0.y * c1.x);
        }

        /// <summary>
        /// Attempts to invert the given matrix. If the operation succeeds, the inverted matrix is stored in the result parameter.
        /// </summary>
        /// <param name="matrix">The source matrix.</param>
        /// <param name="result">The output matrix.</param>
        /// <returns>True if the operation succeeded, False otherwise.</returns>
        public static bool Invert(Fp3x2 matrix, out Fp3x2 result)
        {
            Fp det = (matrix.c0.x * matrix.c1.y) - (matrix.c0.y * matrix.c1.x);
            if (MathFp.abs(det) < Fp.Epsilon)
            {
                // result = new Fp3x2(Fp.NaN, Fp.NaN, Fp.NaN, Fp.NaN, Fp.NaN, Fp.NaN);
                // 由于 Fp 中没有实现 NaN，这里返回原矩阵
                result = matrix;
                return false;
            }

            Fp invDet = Fp.One / det;

            result = new Fp3x2();
            result.c0.x = matrix.c1.y * invDet;
            result.c1.x = -matrix.c1.x * invDet;
            result.c0.y = -matrix.c0.y * invDet;
            result.c1.y = matrix.c0.x * invDet;
            result.c0.z = (matrix.c0.y * matrix.c1.z - matrix.c0.z * matrix.c1.y) * invDet;
            result.c1.z = (matrix.c0.z * matrix.c1.x - matrix.c0.x * matrix.c1.z) * invDet;

            return true;
        }

        /// <summary>
        /// Linearly interpolates from matrix1 to matrix2, based on the third parameter.
        /// </summary>
        /// <param name="matrix1">The first source matrix.</param>
        /// <param name="matrix2">The second source matrix.</param>
        /// <param name="amount">The relative weighting of matrix2.</param>
        /// <returns>The interpolated matrix.</returns>
        public static Fp3x2 Lerp(Fp3x2 matrix1, Fp3x2 matrix2, Fp amount)
        {
            Fp3x2 result = new Fp3x2();

            // First row
            result.c0.x = matrix1.c0.x + (matrix2.c0.x - matrix1.c0.x) * amount;
            result.c1.x = matrix1.c1.x + (matrix2.c1.x - matrix1.c1.x) * amount;

            // Second row
            result.c0.y = matrix1.c0.y + (matrix2.c0.y - matrix1.c0.y) * amount;
            result.c1.y = matrix1.c1.y + (matrix2.c1.y - matrix1.c1.y) * amount;

            // Third row
            result.c0.z = matrix1.c0.z + (matrix2.c0.z - matrix1.c0.z) * amount;
            result.c1.z = matrix1.c1.z + (matrix2.c1.z - matrix1.c1.z) * amount;

            return result;
        }

        /// <summary>
        /// Negates the given matrix by multiplying all values by -1.
        /// </summary>
        /// <param name="value">The source matrix.</param>
        /// <returns>The negated matrix.</returns>
        public static Fp3x2 Negate(Fp3x2 value)
        {
            Fp3x2 result = new Fp3x2();

            result.c0.x = -value.c0.x;
            result.c1.x = -value.c1.x;
            result.c0.y = -value.c0.y;
            result.c1.y = -value.c1.y;
            result.c0.z = -value.c0.z;
            result.c1.z = -value.c1.z;

            return result;
        }

        /// <summary>
        /// Adds each matrix element in value1 with its corresponding element in value2.
        /// </summary>
        /// <param name="value1">The first source matrix.</param>
        /// <param name="value2">The second source matrix.</param>
        /// <returns>The matrix containing the summed values.</returns>
        public static Fp3x2 Add(Fp3x2 value1, Fp3x2 value2)
        {
            Fp3x2 result = new Fp3x2();

            result.c0.x = value1.c0.x + value2.c0.x;
            result.c1.x = value1.c1.x + value2.c1.x;
            result.c0.y = value1.c0.y + value2.c0.y;
            result.c1.y = value1.c1.y + value2.c1.y;
            result.c0.z = value1.c0.z + value2.c0.z;
            result.c1.z = value1.c1.z + value2.c1.z;

            return result;
        }

        /// <summary>
        /// Subtracts each matrix element in value2 from its corresponding element in value1.
        /// </summary>
        /// <param name="value1">The first source matrix.</param>
        /// <param name="value2">The second source matrix.</param>
        /// <returns>The matrix containing the resulting values.</returns>
        public static Fp3x2 Subtract(Fp3x2 value1, Fp3x2 value2)
        {
            Fp3x2 result = new Fp3x2();

            result.c0.x = value1.c0.x - value2.c0.x;
            result.c1.x = value1.c1.x - value2.c1.x;
            result.c0.y = value1.c0.y - value2.c0.y;
            result.c1.y = value1.c1.y - value2.c1.y;
            result.c0.z = value1.c0.z - value2.c0.z;
            result.c1.z = value1.c1.z - value2.c1.z;

            return result;
        }

        /// <summary>
        /// Multiplies two matrices together and returns the resulting matrix.
        /// </summary>
        /// <param name="value1">The first source matrix.</param>
        /// <param name="value2">The second source matrix.</param>
        /// <returns>The product matrix.</returns>
        public static Fp3x2 Multiply(Fp3x2 value1, Fp3x2 value2)
        {
            Fp3x2 result = new Fp3x2();

            // First row
            result.c0.x = value1.c0.x * value2.c0.x + value1.c1.x * value2.c0.y;
            result.c1.x = value1.c0.x * value2.c1.x + value1.c1.x * value2.c1.y;

            // Second row
            result.c0.y = value1.c0.y * value2.c0.x + value1.c1.y * value2.c0.y;
            result.c1.y = value1.c0.y * value2.c1.x + value1.c1.y * value2.c1.y;

            // Third row
            result.c0.z = value1.c0.z * value2.c0.x + value1.c1.z * value2.c0.y + value2.c0.z;
            result.c1.z = value1.c0.z * value2.c1.x + value1.c1.z * value2.c1.y + value2.c1.z;

            return result;
        }

        /// <summary>
        /// Scales all elements in a matrix by the given scalar factor.
        /// </summary>
        /// <param name="value1">The source matrix.</param>
        /// <param name="value2">The scaling value to use.</param>
        /// <returns>The resulting matrix.</returns>
        public static Fp3x2 Multiply(Fp3x2 value1, Fp value2)
        {
            Fp3x2 result = new Fp3x2();

            result.c0.x = value1.c0.x * value2;
            result.c1.x = value1.c1.x * value2;
            result.c0.y = value1.c0.y * value2;
            result.c1.y = value1.c1.y * value2;
            result.c0.z = value1.c0.z * value2;
            result.c1.z = value1.c1.z * value2;

            return result;
        }
    }
}