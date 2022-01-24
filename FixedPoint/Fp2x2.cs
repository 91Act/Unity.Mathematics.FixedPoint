using System.Runtime.CompilerServices;
using static Unity.Mathematics.FixedPoint.MathFp;

namespace Unity.Mathematics.FixedPoint
{
    public partial struct Fp2x2
    {
        /// <summary>
        /// Computes a Fp2x2 matrix representing a counter-clockwise rotation by an angle in radians.
        /// </summary>
        /// <remarks>
        /// A positive rotation angle will produce a counter-clockwise rotation and a negative rotation angle will
        /// produce a clockwise rotation.
        /// </remarks>
        /// <param name="angle">Rotation angle in radians.</param>
        /// <returns>Returns the 2x2 rotation matrix.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp2x2 Rotate(Fp angle)
        {
            Fp s, c;
            sincos(angle, out s, out c);
            return Fp2x2(c, -s, s, c);
        }

        /// <summary>Returns a Fp2x2 matrix representing a uniform scaling of both axes by s.</summary>
        /// <param name="s">The scaling factor.</param>
        /// <returns>The Fp2x2 matrix representing uniform scale by s.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp2x2 Scale(Fp s)
        {
            return Fp2x2(s, 0, 0, s);
        }

        /// <summary>Returns a Fp2x2 matrix representing a non-uniform axis scaling by x and y.</summary>
        /// <param name="x">The x-axis scaling factor.</param>
        /// <param name="y">The y-axis scaling factor.</param>
        /// <returns>The Fp2x2 matrix representing a non-uniform scale.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp2x2 Scale(Fp x, Fp y)
        {
            return Fp2x2(x, 0, 0, y);
        }

        /// <summary>Returns a Fp2x2 matrix representing a non-uniform axis scaling by the components of the Fp2 vector v.</summary>
        /// <param name="v">The Fp2 containing the x and y axis scaling factors.</param>
        /// <returns>The Fp2x2 matrix representing a non-uniform scale.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp2x2 Scale(Fp2 v)
        {
            return Scale(v.x, v.y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp2x2 Inverse(in Fp2x2 m)
        {
            var denom = Fp.One / (m.c0.x * m.c1.y - m.c1.x * m.c0.y);
            var inverse = new Fp2x2();
            inverse.c0.x = m.c1.y * denom;
            inverse.c0.y = -m.c0.y * denom;
            inverse.c1.x = -m.c1.x * denom;
            inverse.c1.y = m.c0.x * denom;
            return inverse;
        }
    }
}