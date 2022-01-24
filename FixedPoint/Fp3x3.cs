using System.Runtime.CompilerServices;
using static Unity.Mathematics.FixedPoint.MathFp;

namespace Unity.Mathematics.FixedPoint
{
    public partial struct Fp3x3
    {
        #region Unity.Mathematics

        /// <summary>
        /// Constructs a Fp3x3 from the upper left 3x3 of a Fp4x4.
        /// </summary>
        /// <param name="f4x4"><see cref="Fp4x4"/> to extract a Fp3x3 from.</param>
        public Fp3x3(Fp4x4 f4x4)
        {
            c0 = f4x4.c0.xyz;
            c1 = f4x4.c1.xyz;
            c2 = f4x4.c2.xyz;
        }

        /// <summary>Constructs a Fp3x3 matrix from a unit QuaternionFp.</summary>
        /// <param name="q">The QuaternionFp rotation.</param>
        public Fp3x3(QuaternionFp q)
        {
            // Fp4 v = q.value;
            // Fp4 v2 = v + v;
            //
            // uint3 npn = uint3(0x80000000, 0x00000000, 0x80000000);
            // uint3 nnp = uint3(0x80000000, 0x80000000, 0x00000000);
            // uint3 pnn = uint3(0x00000000, 0x80000000, 0x80000000);
            // c0 = v2.y * Fp3(asuint(v.yxw) ^ npn) - v2.z * Fp3(asuint(v.zwx) ^ pnn) + Fp3(1, 0, 0);
            // c1 = v2.z * Fp3(asuint(v.wzy) ^ nnp) - v2.x * Fp3(asuint(v.yxw) ^ npn) + Fp3(0, 1, 0);
            // c2 = v2.x * Fp3(asuint(v.zwx) ^ pnn) - v2.y * Fp3(asuint(v.wzy) ^ nnp) + Fp3(0, 0, 1);
            CreateFromQuaternion(q, out var mat);
            mat = transpose(mat);
            // ** NOTE:忽略极小值
            c0 = IgnoreTooSmallNumber(mat.c0);
            c1 = IgnoreTooSmallNumber(mat.c1);
            c2 = IgnoreTooSmallNumber(mat.c2);
        }

        /// <summary>
        /// Returns a Fp3x3 matrix representing a rotation around a unit axis by an angle in radians.
        /// The rotation direction is clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="axis">The rotation axis.</param>
        /// <param name="angle">The angle of rotation in radians.</param>
        /// <returns>The Fp3x3 matrix representing the rotation around an axis.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3x3 AxisAngle(Fp3 axis, Fp angle)
        {
            Fp sina, cosa;
            MathFp.sincos(angle, out sina, out cosa);

            Fp3 u = axis;
            // Fp3 u_yzx = u.yzx;
            // Fp3 u_zxy = u.zxy;
            // Fp3 u_inv_cosa = u - u * cosa; // u * (1 - cosa);
            // Fp4 t = Fp4(u * sina, cosa);

            // uint3 ppn = uint3(0x00000000, 0x00000000, 0x80000000);
            // uint3 npp = uint3(0x80000000, 0x00000000, 0x00000000);
            // uint3 pnp = uint3(0x00000000, 0x80000000, 0x00000000);
            //
            // return Fp3x3(
            //     u.x * u_inv_cosa + Fp3(asuint(t.wzy) ^ ppn),
            //     u.y * u_inv_cosa + Fp3(asuint(t.zwx) ^ npp),
            //     u.z * u_inv_cosa + Fp3(asuint(t.yxw) ^ pnp)
            // );
            return Fp3x3(
                cosa + u.x * u.x * (1 - cosa), u.y * u.x * (1 - cosa) - u.z * sina, u.z * u.x * (1 - cosa) + u.y * sina,
                u.x * u.y * (1 - cosa) + u.z * sina, cosa + u.y * u.y * (1 - cosa), u.y * u.z * (1 - cosa) - u.x * sina,
                u.x * u.z * (1 - cosa) - u.y * sina, u.y * u.z * (1 - cosa) + u.x * sina, cosa + u.z * u.z * (1 - cosa)
            );
        }

        /// <summary>
        /// Returns a Fp3x3 rotation matrix constructed by first performing a rotation around the x-axis, then the y-axis and finally the z-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="xyz">A Fp3 vector containing the rotation angles around the x-, y- and z-axis measures in radians.</param>
        /// <returns>The Fp3x3 rotation matrix representing the rotation by Euler angles in x-y-z order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3x3 EulerXYZ(Fp3 xyz)
        {
            // return mul(rotateZ(xyz.z), mul(rotateY(xyz.y), rotateX(xyz.x)));
            Fp3 s, c;
            sincos(xyz, out s, out c);
            return Fp3x3(
                c.y * c.z, c.z * s.x * s.y - c.x * s.z, c.x * c.z * s.y + s.x * s.z,
                c.y * s.z, c.x * c.z + s.x * s.y * s.z, c.x * s.y * s.z - c.z * s.x,
                -s.y, c.y * s.x, c.x * c.y
            );
        }

        /// <summary>
        /// Returns a Fp3x3 rotation matrix constructed by first performing a rotation around the x-axis, then the z-axis and finally the y-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="xyz">A Fp3 vector containing the rotation angles around the x-, y- and z-axis measures in radians.</param>
        /// <returns>The Fp3x3 rotation matrix representing the rotation by Euler angles in x-z-y order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3x3 EulerXZY(Fp3 xyz)
        {
            // return mul(rotateY(xyz.y), mul(rotateZ(xyz.z), rotateX(xyz.x))); }
            Fp3 s, c;
            sincos(xyz, out s, out c);
            return Fp3x3(
                c.y * c.z, s.x * s.y - c.x * c.y * s.z, c.x * s.y + c.y * s.x * s.z,
                s.z, c.x * c.z, -c.z * s.x,
                -c.z * s.y, c.y * s.x + c.x * s.y * s.z, c.x * c.y - s.x * s.y * s.z
            );
        }

        /// <summary>
        /// Returns a Fp3x3 rotation matrix constructed by first performing a rotation around the y-axis, then the x-axis and finally the z-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="xyz">A Fp3 vector containing the rotation angles around the x-, y- and z-axis measures in radians.</param>
        /// <returns>The Fp3x3 rotation matrix representing the rotation by Euler angles in y-x-z order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3x3 EulerYXZ(Fp3 xyz)
        {
            // return mul(rotateZ(xyz.z), mul(rotateX(xyz.x), rotateY(xyz.y)));
            Fp3 s, c;
            sincos(xyz, out s, out c);
            return Fp3x3(
                c.y * c.z - s.x * s.y * s.z, -c.x * s.z, c.z * s.y + c.y * s.x * s.z,
                c.z * s.x * s.y + c.y * s.z, c.x * c.z, s.y * s.z - c.y * c.z * s.x,
                -c.x * s.y, s.x, c.x * c.y
            );
        }

        /// <summary>
        /// Returns a Fp3x3 rotation matrix constructed by first performing a rotation around the y-axis, then the z-axis and finally the x-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="xyz">A Fp3 vector containing the rotation angles around the x-, y- and z-axis measures in radians.</param>
        /// <returns>The Fp3x3 rotation matrix representing the rotation by Euler angles in y-z-x order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3x3 EulerYZX(Fp3 xyz)
        {
            // return mul(rotateX(xyz.x), mul(rotateZ(xyz.z), rotateY(xyz.y)));
            Fp3 s, c;
            sincos(xyz, out s, out c);
            return Fp3x3(
                c.y * c.z, -s.z, c.z * s.y,
                s.x * s.y + c.x * c.y * s.z, c.x * c.z, c.x * s.y * s.z - c.y * s.x,
                c.y * s.x * s.z - c.x * s.y, c.z * s.x, c.x * c.y + s.x * s.y * s.z
            );
        }

        /// <summary>
        /// Returns a Fp3x3 rotation matrix constructed by first performing a rotation around the z-axis, then the x-axis and finally the y-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// This is the default order rotation order in Unity.
        /// </summary>
        /// <param name="xyz">A Fp3 vector containing the rotation angles around the x-, y- and z-axis measures in radians.</param>
        /// <returns>The Fp3x3 rotation matrix representing the rotation by Euler angles in z-x-y order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3x3 EulerZXY(Fp3 xyz)
        {
            // return mul(rotateY(xyz.y), mul(rotateX(xyz.x), rotateZ(xyz.z)));
            Fp3 s, c;
            sincos(xyz, out s, out c);
            return Fp3x3(
                c.y * c.z + s.x * s.y * s.z, c.z * s.x * s.y - c.y * s.z, c.x * s.y,
                c.x * s.z, c.x * c.z, -s.x,
                c.y * s.x * s.z - c.z * s.y, c.y * c.z * s.x + s.y * s.z, c.x * c.y
            );
        }

        /// <summary>
        /// Returns a Fp3x3 rotation matrix constructed by first performing a rotation around the z-axis, then the y-axis and finally the x-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="xyz">A Fp3 vector containing the rotation angles around the x-, y- and z-axis measures in radians.</param>
        /// <returns>The Fp3x3 rotation matrix representing the rotation by Euler angles in z-y-x order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3x3 EulerZYX(Fp3 xyz)
        {
            // return mul(rotateX(xyz.x), mul(rotateY(xyz.y), rotateZ(xyz.z)));
            Fp3 s, c;
            sincos(xyz, out s, out c);
            return Fp3x3(
                c.y * c.z, -c.y * s.z, s.y,
                c.z * s.x * s.y + c.x * s.z, c.x * c.z - s.x * s.y * s.z, -c.y * s.x,
                s.x * s.z - c.x * c.z * s.y, c.z * s.x + c.x * s.y * s.z, c.x * c.y
            );
        }

        /// <summary>
        /// Returns a Fp3x3 rotation matrix constructed by first performing a rotation around the x-axis, then the y-axis and finally the z-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="x">The rotation angle around the x-axis in radians.</param>
        /// <param name="y">The rotation angle around the y-axis in radians.</param>
        /// <param name="z">The rotation angle around the z-axis in radians.</param>
        /// <returns>The Fp3x3 rotation matrix representing the rotation by Euler angles in x-y-z order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3x3 EulerXYZ(Fp x, Fp y, Fp z)
        {
            return EulerXYZ(Fp3(x, y, z));
        }

        /// <summary>
        /// Returns a Fp3x3 rotation matrix constructed by first performing a rotation around the x-axis, then the z-axis and finally the y-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="x">The rotation angle around the x-axis in radians.</param>
        /// <param name="y">The rotation angle around the y-axis in radians.</param>
        /// <param name="z">The rotation angle around the z-axis in radians.</param>
        /// <returns>The Fp3x3 rotation matrix representing the rotation by Euler angles in x-z-y order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3x3 EulerXZY(Fp x, Fp y, Fp z)
        {
            return EulerXZY(Fp3(x, y, z));
        }

        /// <summary>
        /// Returns a Fp3x3 rotation matrix constructed by first performing a rotation around the y-axis, then the x-axis and finally the z-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="x">The rotation angle around the x-axis in radians.</param>
        /// <param name="y">The rotation angle around the y-axis in radians.</param>
        /// <param name="z">The rotation angle around the z-axis in radians.</param>
        /// <returns>The Fp3x3 rotation matrix representing the rotation by Euler angles in y-x-z order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3x3 EulerYXZ(Fp x, Fp y, Fp z)
        {
            return EulerYXZ(Fp3(x, y, z));
        }

        /// <summary>
        /// Returns a Fp3x3 rotation matrix constructed by first performing a rotation around the y-axis, then the z-axis and finally the x-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="x">The rotation angle around the x-axis in radians.</param>
        /// <param name="y">The rotation angle around the y-axis in radians.</param>
        /// <param name="z">The rotation angle around the z-axis in radians.</param>
        /// <returns>The Fp3x3 rotation matrix representing the rotation by Euler angles in y-z-x order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3x3 EulerYZX(Fp x, Fp y, Fp z)
        {
            return EulerYZX(Fp3(x, y, z));
        }

        /// <summary>
        /// Returns a Fp3x3 rotation matrix constructed by first performing a rotation around the z-axis, then the x-axis and finally the y-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// This is the default order rotation order in Unity.
        /// </summary>
        /// <param name="x">The rotation angle around the x-axis in radians.</param>
        /// <param name="y">The rotation angle around the y-axis in radians.</param>
        /// <param name="z">The rotation angle around the z-axis in radians.</param>
        /// <returns>The Fp3x3 rotation matrix representing the rotation by Euler angles in z-x-y order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3x3 EulerZXY(Fp x, Fp y, Fp z)
        {
            return EulerZXY(Fp3(x, y, z));
        }

        /// <summary>
        /// Returns a Fp3x3 rotation matrix constructed by first performing a rotation around the z-axis, then the y-axis and finally the x-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="x">The rotation angle around the x-axis in radians.</param>
        /// <param name="y">The rotation angle around the y-axis in radians.</param>
        /// <param name="z">The rotation angle around the z-axis in radians.</param>
        /// <returns>The Fp3x3 rotation matrix representing the rotation by Euler angles in z-y-x order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3x3 EulerZYX(Fp x, Fp y, Fp z)
        {
            return EulerZYX(Fp3(x, y, z));
        }

        /// <summary>
        /// Returns a Fp3x3 rotation matrix constructed by first performing 3 rotations around the principal axes in a given order.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// When the rotation order is known at compile time, it is recommended for performance reasons to use specific
        /// Euler rotation constructors such as EulerZXY(...).
        /// </summary>
        /// <param name="xyz">A Fp3 vector containing the rotation angles around the x-, y- and z-axis measures in radians.</param>
        /// <param name="order">The order in which the rotations are applied.</param>
        /// <returns>The Fp3x3 rotation matrix representing the rotation by Euler angles in the given order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3x3 Euler(Fp3 xyz, RotationOrder order = RotationOrder.Default)
        {
            switch (order)
            {
                case RotationOrder.XYZ:
                    return EulerXYZ(xyz);
                case RotationOrder.XZY:
                    return EulerXZY(xyz);
                case RotationOrder.YXZ:
                    return EulerYXZ(xyz);
                case RotationOrder.YZX:
                    return EulerYZX(xyz);
                case RotationOrder.ZXY:
                    return EulerZXY(xyz);
                case RotationOrder.ZYX:
                    return EulerZYX(xyz);
                default:
                    return Fp3x3.identity;
            }
        }

        /// <summary>
        /// Returns a Fp3x3 rotation matrix constructed by first performing 3 rotations around the principal axes in a given order.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// When the rotation order is known at compile time, it is recommended for performance reasons to use specific
        /// Euler rotation constructors such as EulerZXY(...).
        /// </summary>
        /// <param name="x">The rotation angle around the x-axis in radians.</param>
        /// <param name="y">The rotation angle around the y-axis in radians.</param>
        /// <param name="z">The rotation angle around the z-axis in radians.</param>
        /// <param name="order">The order in which the rotations are applied.</param>
        /// <returns>The Fp3x3 rotation matrix representing the rotation by Euler angles in the given order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3x3 Euler(Fp x, Fp y, Fp z, RotationOrder order = RotationOrder.Default)
        {
            return Euler(new Fp3(x, y, z), order);
        }

        /// <summary>Returns a Fp3x3 matrix that rotates around the x-axis by a given number of radians.</summary>
        /// <param name="angle">The clockwise rotation angle when looking along the x-axis towards the origin in radians.</param>
        /// <returns>The Fp3x3 rotation matrix representing a rotation around the x-axis.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3x3 RotateX(Fp angle)
        {
            // {{1, 0, 0}, {0, c_0, -s_0}, {0, s_0, c_0}}
            Fp s, c;
            sincos(angle, out s, out c);
            return Fp3x3(1, 0, 0,
                0, c, -s,
                0, s, c);
        }

        /// <summary>Returns a Fp3x3 matrix that rotates around the y-axis by a given number of radians.</summary>
        /// <param name="angle">The clockwise rotation angle when looking along the y-axis towards the origin in radians.</param>
        /// <returns>The Fp3x3 rotation matrix representing a rotation around the y-axis.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3x3 RotateY(Fp angle)
        {
            // {{c_1, 0, s_1}, {0, 1, 0}, {-s_1, 0, c_1}}
            Fp s, c;
            sincos(angle, out s, out c);
            return Fp3x3(c, 0, s,
                0, 1, 0,
                -s, 0, c);
        }

        /// <summary>Returns a Fp3x3 matrix that rotates around the z-axis by a given number of radians.</summary>
        /// <param name="angle">The clockwise rotation angle when looking along the z-axis towards the origin in radians.</param>
        /// <returns>The Fp3x3 rotation matrix representing a rotation around the z-axis.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3x3 RotateZ(Fp angle)
        {
            // {{c_2, -s_2, 0}, {s_2, c_2, 0}, {0, 0, 1}}
            Fp s, c;
            sincos(angle, out s, out c);
            return Fp3x3(c, -s, 0,
                s, c, 0,
                0, 0, 1);
        }

        /// <summary>Returns a Fp3x3 matrix representing a uniform scaling of all axes by s.</summary>
        /// <param name="s">The uniform scaling factor.</param>
        /// <returns>The Fp3x3 matrix representing a uniform scale.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3x3 Scale(Fp s)
        {
            return Fp3x3(s, 0, 0,
                0, s, 0,
                0, 0, s);
        }

        /// <summary>Returns a Fp3x3 matrix representing a non-uniform axis scaling by x, y and z.</summary>
        /// <param name="x">The x-axis scaling factor.</param>
        /// <param name="y">The y-axis scaling factor.</param>
        /// <param name="z">The z-axis scaling factor.</param>
        /// <returns>The Fp3x3 rotation matrix representing a non-uniform scale.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3x3 Scale(Fp x, Fp y, Fp z)
        {
            return Fp3x3(x, 0, 0,
                0, y, 0,
                0, 0, z);
        }

        /// <summary>Returns a Fp3x3 matrix representing a non-uniform axis scaling by the components of the Fp3 vector v.</summary>
        /// <param name="v">The vector containing non-uniform scaling factors.</param>
        /// <returns>The Fp3x3 rotation matrix representing a non-uniform scale.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3x3 Scale(Fp3 v)
        {
            return Scale(v.x, v.y, v.z);
        }

        /// <summary>
        /// Returns a Fp3x3 view rotation matrix given a unit length forward vector and a unit length up vector.
        /// The two input vectors are assumed to be unit length and not collinear.
        /// If these assumptions are not met use Fp3x3.LookRotationSafe instead.
        /// </summary>
        /// <param name="forward">The forward vector to align the center of view with.</param>
        /// <param name="up">The up vector to point top of view toward.</param>
        /// <returns>The Fp3x3 view rotation matrix.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3x3 LookRotation(Fp3 forward, Fp3 up)
        {
            Fp3 t = normalize(cross(up, forward));
            return Fp3x3(t, cross(forward, t), forward);
        }

        /// <summary>
        /// Returns a Fp3x3 view rotation matrix given a forward vector and an up vector.
        /// The two input vectors are not assumed to be unit length.
        /// If the magnitude of either of the vectors is so extreme that the calculation cannot be carried out reliably or the vectors are collinear,
        /// the identity will be returned instead.
        /// </summary>
        /// <param name="forward">The forward vector to align the center of view with.</param>
        /// <param name="up">The up vector to point top of view toward.</param>
        /// <returns>The Fp3x3 view rotation matrix or the identity matrix.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3x3 LookRotationSafe(Fp3 forward, Fp3 up)
        {
            Fp forwardLengthSq = dot(forward, forward);
            if (forwardLengthSq == 0)
            {
                return Fp3x3.Identity;
            }

            Fp upLengthSq = dot(up, up);
            if (upLengthSq == 0)
            {
                return Fp3x3.Identity;
            }

            forward *= rsqrt(forwardLengthSq);
            up *= rsqrt(upLengthSq);

            Fp3 t = cross(up, forward);
            Fp tLengthSq = dot(t, t);
            if (tLengthSq == 0)
            {
                return Fp3x3.Identity;
            }
            t *= rsqrt(tLengthSq);

            Fp mn = min(min(forwardLengthSq, upLengthSq), tLengthSq);
            Fp mx = max(max(forwardLengthSq, upLengthSq), tLengthSq);

            bool accept = mn > Fp.OneEMinus8 && mx < int.MaxValue && isfinite(forwardLengthSq) &&
                          isfinite(upLengthSq) &&
                          isfinite(tLengthSq);
            return Fp3x3(
                select(Fp3(1, 0, 0), t, accept),
                select(Fp3(0, 1, 0), cross(forward, t), accept),
                select(Fp3(0, 0, 1), forward, accept));
        }

        /// <summary>
        /// Converts a Fp4x4 to a Fp3x3.
        /// </summary>
        /// <param name="f4x4">The Fp4x4 to convert to a Fp3x3.</param>
        /// <returns>The Fp3x3 constructed from the upper left 3x3 of the input Fp4x4 matrix.</returns>
        public static explicit operator Fp3x3(Fp4x4 f4x4) => new Fp3x3(f4x4);

        #endregion

        #region BepuUtilities Matrix3x3

        /// <summary>
        /// Gets the 3x3 identity matrix.
        /// </summary>
        public static Fp3x3 Identity
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => Fp3x3.identity;
        }

        /// <summary>
        /// Adds the components of two matrices together.
        /// </summary>
        /// <param name="a">First matrix to add.</param>
        /// <param name="b">Second matrix to add.</param>
        /// <param name="result">Sum of the two input matrices.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Add(in Fp3x3 a, in Fp3x3 b, out Fp3x3 result)
        {
            result = a + b;
        }

        /// <summary>
        /// Scales the components of a matrix by a scalar.
        /// </summary>
        /// <param name="matrix">Matrix to scale.</param>
        /// <param name="scale">Scale to apply to the matrix's components.</param>
        /// <param name="result">Scaled matrix.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Scale(Fp3x3 matrix, Fp scale, out Fp3x3 result)
        {
            result = matrix * scale;
        }

        /// <summary>
        /// Subtracts the components of one matrix from another.
        /// </summary>
        /// <param name="a">Matrix to be subtracted from.</param>
        /// <param name="b">Matrix to subtract from a.</param>
        /// <param name="result">Difference of the two input matrices.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Subtract(in Fp3x3 a, in Fp3x3 b, out Fp3x3 result)
        {
            result = a - b;
        }


        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        // unsafe static void Transpose(M* m, M* transposed)
        // {
        //     //A weird function! Why?
        //     //1) Missing some helpful instructions for actual SIMD accelerated transposition.
        //     //2) Difficult to get SIMD types to generate competitive codegen due to lots of componentwise access.
        //
        //     Fp m12 = m->M12;
        //     Fp m13 = m->M13;
        //     Fp m23 = m->M23;
        //     transposed->M11 = m->M11;
        //     transposed->M12 = m->M21;
        //     transposed->M13 = m->M31;
        //
        //     transposed->M21 = m12;
        //     transposed->M22 = m->M22;
        //     transposed->M23 = m->M32;
        //
        //     transposed->M31 = m13;
        //     transposed->M32 = m23;
        //     transposed->M33 = m->M33;
        // }
        //
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        // public unsafe static void Transpose(Fp3x3* m, Fp3x3* transposed)
        // {
        //     Transpose((M*)m, (M*)transposed);
        // }

        /// <summary>
        /// Computes the transposed matrix of a matrix.
        /// </summary>
        /// <param name="m">Matrix to transpose.</param>
        /// <param name="transposed">Transposed matrix.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Transpose(in Fp3x3 m, out Fp3x3 transposed)
        {
            transposed = MathFp.transpose(m);
        }

        /// <summary>
        /// Calculates the determinant of the matrix.
        /// </summary>
        /// <returns>The matrix's determinant.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Fp Determinant()
        {
            var transpose = MathFp.transpose(this);
            return Fp3.Dot(transpose.c0, Fp3.Cross(transpose.c1, transpose.c2));
        }

        /// <summary>
        /// Inverts the given matrix.
        /// </summary>
        /// <param name="m">Matrix to be inverted.</param>
        /// <returns>Inverted matrix.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3x3 Inverse(in Fp3x3 m)
        {
            var transposedM = transpose(m);
            //Current implementation of cross far from optimal without shuffles, and even then this has some room for improvement.
            //Inverts should be really rare, so it's not too concerning. Use the scalar version when possible until ryujit improves (and we improve this implementation).
            var yz = Fp3.Cross(transposedM.c1, transposedM.c2);
            var zx = Fp3.Cross(transposedM.c2, transposedM.c0);
            var xy = Fp3.Cross(transposedM.c0, transposedM.c1);
            var inverseDeterminant = Fp.One / Fp3.Dot(transposedM.c0, yz);
            Fp3x3 inverse = new Fp3x3();
            inverse.c0 = yz * inverseDeterminant;
            inverse.c1 = zx * inverseDeterminant;
            inverse.c2 = xy * inverseDeterminant;
            return inverse;
        }

        /// <summary>
        /// Multiplies the two matrices.
        /// </summary>
        /// <param name="a">First matrix to multiply.</param>
        /// <param name="b">Second matrix to multiply.</param>
        /// <returns>Product of the multiplication.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3x3 Multiply(in Fp3x3 a, in Fp3x3 b)
        {
            var result = new Fp3x3();
            result.c0.x = a.c0.x * b.c0.x + a.c1.x * b.c0.y + a.c2.x * b.c0.z;
            result.c0.y = a.c0.y * b.c0.x + a.c1.y * b.c0.y + a.c2.y * b.c0.z;
            result.c0.z = a.c0.z * b.c0.x + a.c1.z * b.c0.y + a.c2.z * b.c0.z;

            result.c1.x = a.c0.x * b.c1.x + a.c1.x * b.c1.y + a.c2.x * b.c1.z;
            result.c1.y = a.c0.y * b.c1.x + a.c1.y * b.c1.y + a.c2.y * b.c1.z;
            result.c1.z = a.c0.z * b.c1.x + a.c1.z * b.c1.y + a.c2.z * b.c1.z;

            result.c2.x = a.c0.x * b.c2.x + a.c1.x * b.c2.y + a.c2.x * b.c2.z;
            result.c2.y = a.c0.y * b.c2.x + a.c1.y * b.c2.y + a.c2.y * b.c2.z;
            result.c2.z = a.c0.z * b.c2.x + a.c1.z * b.c2.y + a.c2.z * b.c2.z;
            return result;
        }

        /// <summary>
        /// Multiplies the two matrices, where b is treated as transposed: result = a * transpose(b)
        /// </summary>
        /// <param name="a">First matrix to multiply that will be transposed.</param>
        /// <param name="b">Second matrix to multiply.</param>
        /// <returns>Product of the multiplication.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3x3 MultiplyTransposed(in Fp3x3 a, in Fp3x3 b)
        {
            var result = new Fp3x3();
            result.c0.x = a.c0.x * b.c0.x + a.c1.x * b.c1.x + a.c2.x * b.c2.x;
            result.c1.x = a.c0.x * b.c0.y + a.c1.x * b.c1.y + a.c2.x * b.c2.y;
            result.c2.x = a.c0.x * b.c0.z + a.c1.x * b.c1.z + a.c2.x * b.c2.z;

            result.c0.y = a.c0.y * b.c0.x + a.c1.y * b.c1.x + a.c2.y * b.c2.x;
            result.c1.y = a.c0.y * b.c0.y + a.c1.y * b.c1.y + a.c2.y * b.c2.y;
            result.c2.y = a.c0.y * b.c0.z + a.c1.y * b.c1.z + a.c2.y * b.c2.z;

            result.c0.z = a.c0.z * b.c0.x + a.c1.z * b.c1.x + a.c2.z * b.c2.x;
            result.c1.z = a.c0.z * b.c0.y + a.c1.z * b.c1.y + a.c2.z * b.c2.y;
            result.c2.z = a.c0.z * b.c0.z + a.c1.z * b.c1.z + a.c2.z * b.c2.z;
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateFromMatrix(in Fp4x4 matrix4x4, out Fp3x3 matrix3x3)
        {
            matrix3x3 = new Fp3x3();
            matrix3x3.c0 = matrix4x4.c0.xyz;
            matrix3x3.c1 = matrix4x4.c1.xyz;
            matrix3x3.c2 = matrix4x4.c2.xyz;
            // matrix3x3.x = new Fp3(matrix.x.x, matrix.x.y, matrix.x.z);
            // matrix3x3.y = new Fp3(matrix.y.x, matrix.y.y, matrix.y.z);
            // matrix3x3.z = new Fp3(matrix.z.x, matrix.z.y, matrix.z.z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateFromQuaternion(in QuaternionFp QuaternionFp, out Fp3x3 result)
        {
            result = new Fp3x3();

            Fp xx = QuaternionFp.value.x * QuaternionFp.value.x;
            Fp yy = QuaternionFp.value.y * QuaternionFp.value.y;
            Fp zz = QuaternionFp.value.z * QuaternionFp.value.z;

            Fp xy = QuaternionFp.value.x * QuaternionFp.value.y;
            Fp wz = QuaternionFp.value.z * QuaternionFp.value.w;
            Fp xz = QuaternionFp.value.z * QuaternionFp.value.x;
            Fp wy = QuaternionFp.value.y * QuaternionFp.value.w;
            Fp yz = QuaternionFp.value.y * QuaternionFp.value.z;
            Fp wx = QuaternionFp.value.x * QuaternionFp.value.w;

            result.c0.x = 1 - 2 * (yy + zz);
            result.c1.x = 2 * (xy + wz);
            result.c2.x = 2 * (xz - wy);
            result.c0.y = 2 * (xy - wz);
            result.c1.y = 1 - 2 * (zz + xx);
            result.c2.y = 2 * (yz + wx);
            result.c0.z = 2 * (xz + wy);
            result.c1.z = 2 * (yz - wx);
            result.c2.z = 1 - 2 * (yy + xx);
            // result = new Fp3x3();
            // Fp qX2 = QuaternionFp.value.x + QuaternionFp.value.x;
            // Fp qY2 = QuaternionFp.value.y + QuaternionFp.value.y;
            // Fp qZ2 = QuaternionFp.value.z + QuaternionFp.value.z;
            // Fp XX = qX2 * QuaternionFp.value.x;
            // Fp YY = qY2 * QuaternionFp.value.y;
            // Fp ZZ = qZ2 * QuaternionFp.value.z;
            // Fp XY = qX2 * QuaternionFp.value.y;
            // Fp XZ = qX2 * QuaternionFp.value.z;
            // Fp XW = qX2 * QuaternionFp.value.w;
            // Fp YZ = qY2 * QuaternionFp.value.z;
            // Fp YW = qY2 * QuaternionFp.value.w;
            // Fp ZW = qZ2 * QuaternionFp.value.w;
            //
            // result.c0 = new Fp3(
            //     1 - YY - ZZ,
            //     XY + ZW,
            //     XZ - YW);
            //
            // result.c1 = new Fp3(
            //     XY - ZW,
            //     1 - XX - ZZ,
            //     YZ + XW);
            //
            // result.c2 = new Fp3(
            //     XZ + YW,
            //     YZ - XW,
            //     1 - XX - YY);
            // // ** NOTE:列转行
            // result = MathFp.transpose(result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3x3 CreateFromQuaternion(in QuaternionFp QuaternionFp)
        {
            CreateFromQuaternion(QuaternionFp, out var toReturn);
            return toReturn;
        }


        /// <summary>
        /// Creates a 3x3 matrix representing the given scale along its local axes.
        /// </summary>
        /// <param name="scale">Scale to represent.</param>
        /// <param name="linearTransform">Matrix representing a scale.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateScale(in Fp3 scale, out Fp3x3 linearTransform)
        {
            linearTransform.c0 = new Fp3(scale.x, 0, 0);
            linearTransform.c1 = new Fp3(0, scale.y, 0);
            linearTransform.c2 = new Fp3(0, 0, scale.z);
        }

        /// <summary>
        /// Creates a matrix representing a rotation derived from an axis and angle.
        /// </summary>
        /// <param name="axis">Axis of the rotation.</param>
        /// <param name="angle">Angle of the rotation.</param>
        /// <param name="result">Resulting rotation matrix.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateFromAxisAngle(in Fp3 axis, Fp angle, out Fp3x3 result)
        {
            //TODO: Could be better simdified.
            Fp xx = axis.x * axis.x;
            Fp yy = axis.y * axis.y;
            Fp zz = axis.z * axis.z;
            Fp xy = axis.x * axis.y;
            Fp xz = axis.x * axis.z;
            Fp yz = axis.y * axis.z;

            Fp sinAngle = MathFp.sin(angle);
            Fp oneMinusCosAngle = Fp.One - MathFp.cos(angle);

            result.c0 = new Fp3(
                1 + oneMinusCosAngle * (xx - 1),
                axis.z * sinAngle + oneMinusCosAngle * xy,
                -axis.y * sinAngle + oneMinusCosAngle * xz);

            result.c1 = new Fp3(
                -axis.z * sinAngle + oneMinusCosAngle * xy,
                1 + oneMinusCosAngle * (yy - 1),
                axis.x * sinAngle + oneMinusCosAngle * yz);

            result.c2 = new Fp3(
                axis.y * sinAngle + oneMinusCosAngle * xz,
                -axis.x * sinAngle + oneMinusCosAngle * yz,
                1 + oneMinusCosAngle * (zz - 1));
            // ** NOTE:列转行
            result = MathFp.transpose(result);
        }

        /// <summary>
        /// Creates a matrix representing a rotation derived from an axis and angle.
        /// </summary>
        /// <param name="axis">Axis of the rotation.</param>
        /// <param name="angle">Angle of the rotation.</param>
        /// <returns>Resulting rotation matrix.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3x3 CreateFromAxisAngle(in Fp3 axis, Fp angle)
        {
            CreateFromAxisAngle(axis, angle, out var result);
            return result;
        }

        /// <summary>
        /// Creates a matrix such that a x v = a * result.
        /// </summary>
        /// <param name="v">Vector to build the skew symmetric matrix from.</param>
        /// <param name="result">Skew symmetric matrix representing the cross product.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateCrossProduct(in Fp3 v, out Fp3x3 result)
        {
            result.c0 = new Fp3(0, v.z, -v.y);
            result.c1 = new Fp3(-v.z, 0, v.x);
            result.c2 = new Fp3(v.y, -v.x, 0);
            // result.x.x = 0f;
            // result.y.x = v.z;
            // result.z.x = -v.y;
            // result.x.y = -v.z;
            // result.y.y = 0f;
            // result.z.y = v.x;
            // result.x.z = v.y;
            // result.y.z = -v.x;
            // result.z.z = 0f;
        }

        // /// <summary>
        // /// Concatenates two matrices.
        // /// </summary>
        // /// <param name="m1">First input matrix.</param>
        // /// <param name="m2">Second input matrix.</param>
        // /// <returns>Concatenated transformation of the form m1 * m2.</returns>
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        // public static Fp3x3 operator *(in Fp3x3 m1, in Fp3x3 m2)
        // {
        //     Multiply(m1, m2, out var toReturn);
        //     return toReturn;
        // }

        #endregion
    }
}