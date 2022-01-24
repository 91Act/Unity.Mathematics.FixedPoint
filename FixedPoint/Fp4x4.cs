using System;
using System.Runtime.CompilerServices;
using static Unity.Mathematics.FixedPoint.MathFp;

namespace Unity.Mathematics.FixedPoint
{
    /// <summary>
    /// 对 System.Numerics.Matrix4x4 类的复制，为兼容定点数替换了部分内容
    /// https://github.com/microsoft/referencesource
    /// </summary>
    public partial struct Fp4x4
    {
        #region Unity.Mathematics

        /// <summary>Constructs a Fp4x4 from a Fp3x3 rotation matrix and a Fp3 translation vector.</summary>
        /// <param name="rotation">The Fp3x3 rotation matrix.</param>
        /// <param name="translation">The translation vector.</param>
        public Fp4x4(Fp3x3 rotation, Fp3 translation)
        {
            c0 = Fp4(rotation.c0, 0);
            c1 = Fp4(rotation.c1, 0);
            c2 = Fp4(rotation.c2, 0);
            c3 = Fp4(translation, 1);
        }

        /// <summary>Constructs a Fp4x4 from a QuaternionFp and a Fp3 translation vector.</summary>
        /// <param name="rotation">The QuaternionFp rotation.</param>
        /// <param name="translation">The translation vector.</param>
        public Fp4x4(QuaternionFp rotation, Fp3 translation)
        {
            Fp3x3 rot = Fp3x3(rotation);
            c0 = Fp4(rot.c0, 0);
            c1 = Fp4(rot.c1, 0);
            c2 = Fp4(rot.c2, 0);
            c3 = Fp4(translation, 1);
        }

        /// <summary>Constructs a Fp4x4 from a RigidTransform.</summary>
        /// <param name="transform">The RigidTransform.</param>
        public Fp4x4(RigidTransform transform)
        {
            Fp3x3 rot = Fp3x3(transform.rot);
            c0 = Fp4(rot.c0, 0);
            c1 = Fp4(rot.c1, 0);
            c2 = Fp4(rot.c2, 0);
            c3 = Fp4(transform.pos, 1);
        }

        /// <summary>
        /// Returns a Fp4x4 matrix representing a rotation around a unit axis by an angle in radians.
        /// The rotation direction is clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="axis">The axis of rotation.</param>
        /// <param name="angle">The angle of rotation in radians.</param>
        /// <returns>The Fp4x4 matrix representing the rotation about an axis.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4x4 AxisAngle(Fp3 axis, Fp angle)
        {
            return Fp4x4(Fp3x3.AxisAngle(axis, angle), Fp3(0, 0, 0));

            // Fp sina, cosa;
            // MathFp.sincos(angle, out sina, out cosa);
            // Fp4 u = Fp4(axis, 0);
            // // Fp4 u_yzx = u.yzxx;
            // // Fp4 u_zxy = u.zxyx;
            // Fp4 u_inv_cosa = u - u * cosa; // u * (1 - cosa);
            // Fp4 t = Fp4(u.xyz * sina, cosa);
            //
            // uint4 ppnp = uint4(0x00000000, 0x00000000, 0x80000000, 0x00000000);
            // uint4 nppp = uint4(0x80000000, 0x00000000, 0x00000000, 0x00000000);
            // uint4 pnpp = uint4(0x00000000, 0x80000000, 0x00000000, 0x00000000);
            // uint4 mask = uint4(0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0x00000000);
            //
            // return Fp4x4(
            //     u.x * u_inv_cosa + Fp4((asuint(t.wzyx) ^ ppnp) & mask),
            //     u.y * u_inv_cosa + Fp4((asuint(t.zwxx) ^ nppp) & mask),
            //     u.z * u_inv_cosa + Fp4((asuint(t.yxwx) ^ pnpp) & mask),
            //     Fp4(0, 0, 0, 1)
            // );
        }

        /// <summary>
        /// Returns a Fp4x4 rotation matrix constructed by first performing a rotation around the x-axis, then the y-axis and finally the z-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="xyz">A Fp3 vector containing the rotation angles around the x-, y- and z-axis measures in radians.</param>
        /// <returns>The Fp4x4 rotation matrix of the Euler angle rotation in x-y-z order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4x4 EulerXYZ(Fp3 xyz)
        {
            // return mul(rotateZ(xyz.z), mul(rotateY(xyz.y), rotateX(xyz.x)));
            Fp3 s, c;
            sincos(xyz, out s, out c);
            return Fp4x4(
                c.y * c.z, c.z * s.x * s.y - c.x * s.z, c.x * c.z * s.y + s.x * s.z, 0,
                c.y * s.z, c.x * c.z + s.x * s.y * s.z, c.x * s.y * s.z - c.z * s.x, 0,
                -s.y, c.y * s.x, c.x * c.y, 0,
                0, 0, 0, 1
            );
        }

        /// <summary>
        /// Returns a Fp4x4 rotation matrix constructed by first performing a rotation around the x-axis, then the z-axis and finally the y-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="xyz">A Fp3 vector containing the rotation angles around the x-, y- and z-axis measures in radians.</param>
        /// <returns>The Fp4x4 rotation matrix of the Euler angle rotation in x-z-y order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4x4 EulerXZY(Fp3 xyz)
        {
            // return mul(rotateY(xyz.y), mul(rotateZ(xyz.z), rotateX(xyz.x))); }
            Fp3 s, c;
            sincos(xyz, out s, out c);
            return Fp4x4(
                c.y * c.z, s.x * s.y - c.x * c.y * s.z, c.x * s.y + c.y * s.x * s.z, 0,
                s.z, c.x * c.z, -c.z * s.x, 0,
                -c.z * s.y, c.y * s.x + c.x * s.y * s.z, c.x * c.y - s.x * s.y * s.z, 0,
                0, 0, 0, 1
            );
        }

        /// <summary>
        /// Returns a Fp4x4 rotation matrix constructed by first performing a rotation around the y-axis, then the x-axis and finally the z-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="xyz">A Fp3 vector containing the rotation angles around the x-, y- and z-axis measures in radians.</param>
        /// <returns>The Fp4x4 rotation matrix of the Euler angle rotation in y-x-z order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4x4 EulerYXZ(Fp3 xyz)
        {
            // return mul(rotateZ(xyz.z), mul(rotateX(xyz.x), rotateY(xyz.y)));
            Fp3 s, c;
            sincos(xyz, out s, out c);
            return Fp4x4(
                c.y * c.z - s.x * s.y * s.z, -c.x * s.z, c.z * s.y + c.y * s.x * s.z, 0,
                c.z * s.x * s.y + c.y * s.z, c.x * c.z, s.y * s.z - c.y * c.z * s.x, 0,
                -c.x * s.y, s.x, c.x * c.y, 0,
                0, 0, 0, 1
            );
        }

        /// <summary>
        /// Returns a Fp4x4 rotation matrix constructed by first performing a rotation around the y-axis, then the z-axis and finally the x-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="xyz">A Fp3 vector containing the rotation angles around the x-, y- and z-axis measures in radians.</param>
        /// <returns>The Fp4x4 rotation matrix of the Euler angle rotation in y-z-x order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4x4 EulerYZX(Fp3 xyz)
        {
            // return mul(rotateX(xyz.x), mul(rotateZ(xyz.z), rotateY(xyz.y)));
            Fp3 s, c;
            sincos(xyz, out s, out c);
            return Fp4x4(
                c.y * c.z, -s.z, c.z * s.y, 0,
                s.x * s.y + c.x * c.y * s.z, c.x * c.z, c.x * s.y * s.z - c.y * s.x, 0,
                c.y * s.x * s.z - c.x * s.y, c.z * s.x, c.x * c.y + s.x * s.y * s.z, 0,
                0, 0, 0, 1
            );
        }

        /// <summary>
        /// Returns a Fp4x4 rotation matrix constructed by first performing a rotation around the z-axis, then the x-axis and finally the y-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// This is the default order rotation order in Unity.
        /// </summary>
        /// <param name="xyz">A Fp3 vector containing the rotation angles around the x-, y- and z-axis measures in radians.</param>
        /// <returns>The Fp4x4 rotation matrix of the Euler angle rotation in z-x-y order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4x4 EulerZXY(Fp3 xyz)
        {
            // return mul(rotateY(xyz.y), mul(rotateX(xyz.x), rotateZ(xyz.z)));
            Fp3 s, c;
            sincos(xyz, out s, out c);
            return Fp4x4(
                c.y * c.z + s.x * s.y * s.z, c.z * s.x * s.y - c.y * s.z, c.x * s.y, 0,
                c.x * s.z, c.x * c.z, -s.x, 0,
                c.y * s.x * s.z - c.z * s.y, c.y * c.z * s.x + s.y * s.z, c.x * c.y, 0,
                0, 0, 0, 1
            );
        }

        /// <summary>
        /// Returns a Fp4x4 rotation matrix constructed by first performing a rotation around the z-axis, then the y-axis and finally the x-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="xyz">A Fp3 vector containing the rotation angles around the x-, y- and z-axis measures in radians.</param>
        /// <returns>The Fp4x4 rotation matrix of the Euler angle rotation in z-y-x order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4x4 EulerZYX(Fp3 xyz)
        {
            // return mul(rotateX(xyz.x), mul(rotateY(xyz.y), rotateZ(xyz.z)));
            Fp3 s, c;
            sincos(xyz, out s, out c);
            return Fp4x4(
                c.y * c.z, -c.y * s.z, s.y, 0,
                c.z * s.x * s.y + c.x * s.z, c.x * c.z - s.x * s.y * s.z, -c.y * s.x, 0,
                s.x * s.z - c.x * c.z * s.y, c.z * s.x + c.x * s.y * s.z, c.x * c.y, 0,
                0, 0, 0, 1
            );
        }

        /// <summary>
        /// Returns a Fp4x4 rotation matrix constructed by first performing a rotation around the x-axis, then the y-axis and finally the z-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="x">The rotation angle around the x-axis in radians.</param>
        /// <param name="y">The rotation angle around the y-axis in radians.</param>
        /// <param name="z">The rotation angle around the z-axis in radians.</param>
        /// <returns>The Fp4x4 rotation matrix of the Euler angle rotation in x-y-z order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4x4 EulerXYZ(Fp x, Fp y, Fp z)
        {
            return EulerXYZ(Fp3(x, y, z));
        }

        /// <summary>
        /// Returns a Fp4x4 rotation matrix constructed by first performing a rotation around the x-axis, then the z-axis and finally the y-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="x">The rotation angle around the x-axis in radians.</param>
        /// <param name="y">The rotation angle around the y-axis in radians.</param>
        /// <param name="z">The rotation angle around the z-axis in radians.</param>
        /// <returns>The Fp4x4 rotation matrix of the Euler angle rotation in x-z-y order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4x4 EulerXZY(Fp x, Fp y, Fp z)
        {
            return EulerXZY(Fp3(x, y, z));
        }

        /// <summary>
        /// Returns a Fp4x4 rotation matrix constructed by first performing a rotation around the y-axis, then the x-axis and finally the z-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="x">The rotation angle around the x-axis in radians.</param>
        /// <param name="y">The rotation angle around the y-axis in radians.</param>
        /// <param name="z">The rotation angle around the z-axis in radians.</param>
        /// <returns>The Fp4x4 rotation matrix of the Euler angle rotation in y-x-z order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4x4 EulerYXZ(Fp x, Fp y, Fp z)
        {
            return EulerYXZ(Fp3(x, y, z));
        }

        /// <summary>
        /// Returns a Fp4x4 rotation matrix constructed by first performing a rotation around the y-axis, then the z-axis and finally the x-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="x">The rotation angle around the x-axis in radians.</param>
        /// <param name="y">The rotation angle around the y-axis in radians.</param>
        /// <param name="z">The rotation angle around the z-axis in radians.</param>
        /// <returns>The Fp4x4 rotation matrix of the Euler angle rotation in y-z-x order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4x4 EulerYZX(Fp x, Fp y, Fp z)
        {
            return EulerYZX(Fp3(x, y, z));
        }

        /// <summary>
        /// Returns a Fp4x4 rotation matrix constructed by first performing a rotation around the z-axis, then the x-axis and finally the y-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// This is the default order rotation order in Unity.
        /// </summary>
        /// <param name="x">The rotation angle around the x-axis in radians.</param>
        /// <param name="y">The rotation angle around the y-axis in radians.</param>
        /// <param name="z">The rotation angle around the z-axis in radians.</param>
        /// <returns>The Fp4x4 rotation matrix of the Euler angle rotation in z-x-y order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4x4 EulerZXY(Fp x, Fp y, Fp z)
        {
            return EulerZXY(Fp3(x, y, z));
        }

        /// <summary>
        /// Returns a Fp4x4 rotation matrix constructed by first performing a rotation around the z-axis, then the y-axis and finally the x-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="x">The rotation angle around the x-axis in radians.</param>
        /// <param name="y">The rotation angle around the y-axis in radians.</param>
        /// <param name="z">The rotation angle around the z-axis in radians.</param>
        /// <returns>The Fp4x4 rotation matrix of the Euler angle rotation in z-y-x order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4x4 EulerZYX(Fp x, Fp y, Fp z)
        {
            return EulerZYX(Fp3(x, y, z));
        }

        /// <summary>
        /// Returns a Fp4x4 constructed by first performing 3 rotations around the principal axes in a given order.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// When the rotation order is known at compile time, it is recommended for performance reasons to use specific
        /// Euler rotation constructors such as EulerZXY(...).
        /// </summary>
        /// <param name="xyz">A Fp3 vector containing the rotation angles around the x-, y- and z-axis measures in radians.</param>
        /// <param name="order">The order in which the rotations are applied.</param>
        /// <returns>The Fp4x4 rotation matrix of the Euler angle rotation in given order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4x4 Euler(Fp3 xyz, RotationOrder order = RotationOrder.Default)
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
                    return Fp4x4.identity;
            }
        }

        /// <summary>
        /// Returns a Fp4x4 rotation matrix constructed by first performing 3 rotations around the principal axes in a given order.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// When the rotation order is known at compile time, it is recommended for performance reasons to use specific
        /// Euler rotation constructors such as EulerZXY(...).
        /// </summary>
        /// <param name="x">The rotation angle around the x-axis in radians.</param>
        /// <param name="y">The rotation angle around the y-axis in radians.</param>
        /// <param name="z">The rotation angle around the z-axis in radians.</param>
        /// <param name="order">The order in which the rotations are applied.</param>
        /// <returns>The Fp4x4 rotation matrix of the Euler angle rotation in given order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4x4 Euler(Fp x, Fp y, Fp z, RotationOrder order = RotationOrder.Default)
        {
            return Euler(new Fp3(x, y, z), order);
        }

        /// <summary>Returns a Fp4x4 matrix that rotates around the x-axis by a given number of radians.</summary>
        /// <param name="angle">The clockwise rotation angle when looking along the x-axis towards the origin in radians.</param>
        /// <returns>The Fp4x4 rotation matrix that rotates around the x-axis.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4x4 RotateX(Fp angle)
        {
            // {{1, 0, 0}, {0, c_0, -s_0}, {0, s_0, c_0}}
            Fp s, c;
            sincos(angle, out s, out c);
            return Fp4x4(1, 0, 0, 0,
                0, c, -s, 0,
                0, s, c, 0,
                0, 0, 0, 1);
        }

        /// <summary>Returns a Fp4x4 matrix that rotates around the y-axis by a given number of radians.</summary>
        /// <param name="angle">The clockwise rotation angle when looking along the y-axis towards the origin in radians.</param>
        /// <returns>The Fp4x4 rotation matrix that rotates around the y-axis.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4x4 RotateY(Fp angle)
        {
            // {{c_1, 0, s_1}, {0, 1, 0}, {-s_1, 0, c_1}}
            Fp s, c;
            sincos(angle, out s, out c);
            return Fp4x4(c, 0, s, 0,
                0, 1, 0, 0,
                -s, 0, c, 0,
                0, 0, 0, 1);
        }

        /// <summary>Returns a Fp4x4 matrix that rotates around the z-axis by a given number of radians.</summary>
        /// <param name="angle">The clockwise rotation angle when looking along the z-axis towards the origin in radians.</param>
        /// <returns>The Fp4x4 rotation matrix that rotates around the z-axis.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4x4 RotateZ(Fp angle)
        {
            // {{c_2, -s_2, 0}, {s_2, c_2, 0}, {0, 0, 1}}
            Fp s, c;
            sincos(angle, out s, out c);
            return Fp4x4(c, -s, 0, 0,
                s, c, 0, 0,
                0, 0, 1, 0,
                0, 0, 0, 1);
        }

        /// <summary>Returns a Fp4x4 scale matrix given 3 axis scales.</summary>
        /// <param name="s">The uniform scaling factor.</param>
        /// <returns>The Fp4x4 matrix that represents a uniform scale.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4x4 Scale(Fp s)
        {
            return Fp4x4(s, 0, 0, 0,
                0, s, 0, 0,
                0, 0, s, 0,
                0, 0, 0, 1);
        }

        /// <summary>Returns a Fp4x4 scale matrix given a Fp3 vector containing the 3 axis scales.</summary>
        /// <param name="x">The x-axis scaling factor.</param>
        /// <param name="y">The y-axis scaling factor.</param>
        /// <param name="z">The z-axis scaling factor.</param>
        /// <returns>The Fp4x4 matrix that represents a non-uniform scale.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4x4 Scale(Fp x, Fp y, Fp z)
        {
            return Fp4x4(x, 0, 0, 0,
                0, y, 0, 0,
                0, 0, z, 0,
                0, 0, 0, 1);
        }

        /// <summary>Returns a Fp4x4 scale matrix given a Fp3 vector containing the 3 axis scales.</summary>
        /// <param name="scales">The vector containing scale factors for each axis.</param>
        /// <returns>The Fp4x4 matrix that represents a non-uniform scale.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4x4 Scale(Fp3 scales)
        {
            return Scale(scales.x, scales.y, scales.z);
        }

        /// <summary>Returns a Fp4x4 translation matrix given a Fp3 translation vector.</summary>
        /// <param name="vector">The translation vector.</param>
        /// <returns>The Fp4x4 translation matrix.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4x4 Translate(Fp3 vector)
        {
            return Fp4x4(Fp4(1, 0, 0, 0),
                Fp4(0, 1, 0, 0),
                Fp4(0, 0, 1, 0),
                Fp4(vector.x, vector.y, vector.z, 1));
        }

        /// <summary>
        /// Returns a Fp4x4 view matrix given an eye position, a target point and a unit length up vector.
        /// The up vector is assumed to be unit length, the eye and target points are assumed to be distinct and
        /// the vector between them is assumes to be collinear with the up vector.
        /// If these assumptions are not met use Fp4x4.LookRotationSafe instead.
        /// </summary>
        /// <param name="eye">The eye position.</param>
        /// <param name="target">The view target position.</param>
        /// <param name="up">The eye up direction.</param>
        /// <returns>The Fp4x4 view matrix.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4x4 LookAt(Fp3 eye, Fp3 target, Fp3 up)
        {
            Fp3x3 rot = Fp3x3.LookRotation(normalize(target - eye), up);

            Fp4x4 matrix;
            matrix.c0 = Fp4(rot.c0, 0);
            matrix.c1 = Fp4(rot.c1, 0);
            matrix.c2 = Fp4(rot.c2, 0);
            matrix.c3 = Fp4(eye, 1);
            return matrix;
        }

        /// <summary>
        /// Returns a Fp4x4 centered orthographic projection matrix.
        /// </summary>
        /// <param name="width">The width of the view volume.</param>
        /// <param name="height">The height of the view volume.</param>
        /// <param name="near">The distance to the near plane.</param>
        /// <param name="far">The distance to the far plane.</param>
        /// <returns>The Fp4x4 centered orthographic projection matrix.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4x4 Ortho(Fp width, Fp height, Fp near, Fp far)
        {
            Fp rcpdx = 1 / width;
            Fp rcpdy = 1 / height;
            Fp rcpdz = 1 / (far - near);

            return Fp4x4(
                2 * rcpdx, 0, 0, 0,
                0, 2 * rcpdy, 0, 0,
                0, 0, -2 * rcpdz, -(far + near) * rcpdz,
                0, 0, 0, 1
            );
        }

        /// <summary>
        /// Returns a Fp4x4 off-center orthographic projection matrix.
        /// </summary>
        /// <param name="left">The minimum x-coordinate of the view volume.</param>
        /// <param name="right">The maximum x-coordinate of the view volume.</param>
        /// <param name="bottom">The minimum y-coordinate of the view volume.</param>
        /// <param name="top">The minimum y-coordinate of the view volume.</param>
        /// <param name="near">The distance to the near plane.</param>
        /// <param name="far">The distance to the far plane.</param>
        /// <returns>The Fp4x4 off-center orthographic projection matrix.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4x4 OrthoOffCenter(Fp left, Fp right, Fp bottom, Fp top, Fp near, Fp far)
        {
            Fp rcpdx = 1 / (right - left);
            Fp rcpdy = 1 / (top - bottom);
            Fp rcpdz = 1 / (far - near);

            return Fp4x4(
                2 * rcpdx, 0, 0, -(right + left) * rcpdx,
                0, 2 * rcpdy, 0, -(top + bottom) * rcpdy,
                0, 0, -2 * rcpdz, -(far + near) * rcpdz,
                0, 0, 0, 1
            );
        }

        /// <summary>
        /// Returns a Fp4x4 perspective projection matrix based on field of view.
        /// </summary>
        /// <param name="verticalFov">Vertical Field of view in radians.</param>
        /// <param name="aspect">X:Y aspect ratio.</param>
        /// <param name="near">Distance to near plane. Must be greater than zero.</param>
        /// <param name="far">Distance to far plane. Must be greater than zero.</param>
        /// <returns>The Fp4x4 perspective projection matrix.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4x4 PerspectiveFov(Fp verticalFov, Fp aspect, Fp near, Fp far)
        {
            Fp cotangent = 1 / tan(verticalFov * Fp.Half);
            Fp rcpdz = 1 / (near - far);

            return Fp4x4(
                cotangent / aspect, 0, 0, 0,
                0, cotangent, 0, 0,
                0, 0, (far + near) * rcpdz, 2 * near * far * rcpdz,
                0, 0, -1, 0
            );
        }

        /// <summary>
        /// Returns a Fp4x4 off-center perspective projection matrix.
        /// </summary>
        /// <param name="left">The x-coordinate of the left side of the clipping frustum at the near plane.</param>
        /// <param name="right">The x-coordinate of the right side of the clipping frustum at the near plane.</param>
        /// <param name="bottom">The y-coordinate of the bottom side of the clipping frustum at the near plane.</param>
        /// <param name="top">The y-coordinate of the top side of the clipping frustum at the near plane.</param>
        /// <param name="near">Distance to the near plane. Must be greater than zero.</param>
        /// <param name="far">Distance to the far plane. Must be greater than zero.</param>
        /// <returns>The Fp4x4 off-center perspective projection matrix.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4x4 PerspectiveOffCenter(Fp left, Fp right, Fp bottom, Fp top, Fp near, Fp far)
        {
            Fp rcpdz = 1 / (near - far);
            Fp rcpWidth = 1 / (right - left);
            Fp rcpHeight = 1 / (top - bottom);

            return Fp4x4(
                2 * near * rcpWidth, 0, (left + right) * rcpWidth, 0,
                0, 2 * near * rcpHeight, (bottom + top) * rcpHeight, 0,
                0, 0, (far + near) * rcpdz, 2 * near * far * rcpdz,
                0, 0, -1, 0
            );
        }

        /// <summary>
        /// Returns a Fp4x4 matrix representing a combined scale-, rotation- and translation transform.
        /// Equivalent to mul(translationTransform, mul(rotationTransform, scaleTransform)).
        /// </summary>
        /// <param name="translation">The translation vector.</param>
        /// <param name="rotation">The QuaternionFp rotation.</param>
        /// <param name="scale">The scaling factors of each axis.</param>
        /// <returns>The Fp4x4 matrix representing the translation, rotation, and scale by the inputs.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4x4 TRS(Fp3 translation, QuaternionFp rotation, Fp3 scale)
        {
            Fp3x3 r = Fp3x3(rotation);
            return Fp4x4(Fp4(r.c0 * scale.x, 0),
                Fp4(r.c1 * scale.y, 0),
                Fp4(r.c2 * scale.z, 0),
                Fp4(translation, 1));
        }

        #endregion

        #region From System.Numerics.Matrix4x4

        private static readonly Fp4x4 _identity = new Fp4x4
        (
            Fp.One, 0, 0, 0,
            0, Fp.One, 0, 0,
            0, 0, Fp.One, 0,
            0, 0, 0, Fp.One
        );

        /// <summary>
        /// Returns the multiplicative identity matrix.
        /// </summary>
        public static Fp4x4 Identity
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
                return c0.x == Fp.One && c1.y == Fp.One && c2.z == Fp.One &&
                       c3.w == Fp.One && // Check diagonal element first for early out.
                       c1.x == 0 && c2.x == 0 && c3.x == 0 &&
                       c0.y == 0 && c2.y == 0 && c3.y == 0 &&
                       c0.z == 0 && c1.z == 0 && c3.z == 0 &&
                       c0.w == 0 && c1.w == 0 && c2.w == 0;
            }
        }

        /// <summary>
        /// Gets or sets the translation component of this matrix.
        /// </summary>
        public Fp3 Translation
        {
            get { return new Fp3(c0.w, c1.w, c2.w); }
            set
            {
                c0.w = value.x;
                c1.w = value.y;
                c2.w = value.z;
            }
        }

        /// <summary>
        /// Constructs a Fp4x4 from the given Fp3x2.
        /// </summary>
        /// <param name="value">The source Fp3x2.</param>
        public Fp4x4(Fp3x2 value)
        {
            // ** FIX: The 'this' object cannot be used before all of its fields are assigned to
            this.c0 = new Fp4();
            this.c1 = new Fp4();
            this.c2 = new Fp4();
            this.c3 = new Fp4();
            c0.x = value.c0.x;
            c1.x = value.c1.x;
            c2.x = 0;
            c3.x = 0;
            c0.y = value.c0.y;
            c1.y = value.c1.y;
            c2.y = 0;
            c3.y = 0;
            c0.z = 0;
            c1.z = 0;
            c2.z = Fp.One;
            c3.z = 0;
            c0.w = value.c0.z;
            c1.w = value.c1.z;
            c2.w = 0;
            c3.w = Fp.One;
        }

        /// <summary>
        /// Creates a spherical billboard that rotates around a specified object position.
        /// </summary>
        /// <param name="objectPosition">Position of the object the billboard will rotate around.</param>
        /// <param name="cameraPosition">Position of the camera.</param>
        /// <param name="cameraUpVector">The up vector of the camera.</param>
        /// <param name="cameraForwardVector">The forward vector of the camera.</param>
        /// <returns>The created billboard matrix</returns>
        public static Fp4x4 CreateBillboard(Fp3 objectPosition, Fp3 cameraPosition, Fp3 cameraUpVector,
            Fp3 cameraForwardVector)
        {
            throw new NotImplementedException();
            // const Fp epsilon = 1e-4f;
            //
            // Fp3 zaxis = new Fp3(
            //     objectPosition.x - cameraPosition.x,
            //     objectPosition.y - cameraPosition.y,
            //     objectPosition.z - cameraPosition.z);
            //
            // Fp norm = zaxis.LengthSquared();
            //
            // if (norm < epsilon)
            // {
            //     zaxis = -cameraForwardVector;
            // }
            // else
            // {
            //     zaxis = Fp3.Multiply(zaxis, Fp.One / (Fp)Math.Sqrt(norm));
            // }
            //
            // Fp3 xaxis = Fp3.Normalize(Fp3.Cross(cameraUpVector, zaxis));
            //
            // Fp3 yaxis = Fp3.Cross(zaxis, xaxis);
            //
            // Fp4x4 result = new Fp4x4();
            //
            // result.M11 = xaxis.x;
            // result.M12 = xaxis.y;
            // result.M13 = xaxis.z;
            // result.M14 = 0;
            // result.M21 = yaxis.x;
            // result.M22 = yaxis.y;
            // result.M23 = yaxis.z;
            // result.M24 = 0;
            // result.M31 = zaxis.x;
            // result.M32 = zaxis.y;
            // result.M33 = zaxis.z;
            // result.M34 = 0;
            //
            // result.M41 = objectPosition.x;
            // result.M42 = objectPosition.y;
            // result.M43 = objectPosition.z;
            // result.M44 = Fp.One;
            //
            // return result;
        }

        /// <summary>
        /// Creates a cylindrical billboard that rotates around a specified axis.
        /// </summary>
        /// <param name="objectPosition">Position of the object the billboard will rotate around.</param>
        /// <param name="cameraPosition">Position of the camera.</param>
        /// <param name="rotateAxis">Axis to rotate the billboard around.</param>
        /// <param name="cameraForwardVector">Forward vector of the camera.</param>
        /// <param name="objectForwardVector">Forward vector of the object.</param>
        /// <returns>The created billboard matrix.</returns>
        public static Fp4x4 CreateConstrainedBillboard(Fp3 objectPosition, Fp3 cameraPosition,
            Fp3 rotateAxis, Fp3 cameraForwardVector, Fp3 objectForwardVector)
        {
            throw new NotImplementedException();
            // const Fp epsilon = 1e-4f;
            // const Fp minAngle = Fp.One - (0.Fp.One * ((Fp)Math.PI / 180.0)); // 0.1 degrees
            //
            // // Treat the case when object and camera positions are too close.
            // Fp3 faceDir = new Fp3(
            //     objectPosition.x - cameraPosition.x,
            //     objectPosition.y - cameraPosition.y,
            //     objectPosition.z - cameraPosition.z);
            //
            // Fp norm = faceDir.LengthSquared();
            //
            // if (norm < epsilon)
            // {
            //     faceDir = -cameraForwardVector;
            // }
            // else
            // {
            //     faceDir = Fp3.Multiply(faceDir, (Fp.One / (Fp)Math.Sqrt(norm)));
            // }
            //
            // Fp3 yaxis = rotateAxis;
            // Fp3 xaxis;
            // Fp3 zaxis;
            //
            // // Treat the case when angle between faceDir and rotateAxis is too close to 0.
            // Fp dot = Fp3.Dot(rotateAxis, faceDir);
            //
            // if (Math.Abs(dot) > minAngle)
            // {
            //     zaxis = objectForwardVector;
            //
            //     // Make sure passed values are useful for compute.
            //     dot = Fp3.Dot(rotateAxis, zaxis);
            //
            //     if (Math.Abs(dot) > minAngle)
            //     {
            //         zaxis = (Math.Abs(rotateAxis.z) > minAngle) ? new Fp3(1, 0, 0) : new Fp3(0, 0, -1);
            //     }
            //
            //     xaxis = Fp3.Normalize(Fp3.Cross(rotateAxis, zaxis));
            //     zaxis = Fp3.Normalize(Fp3.Cross(xaxis, rotateAxis));
            // }
            // else
            // {
            //     xaxis = Fp3.Normalize(Fp3.Cross(rotateAxis, faceDir));
            //     zaxis = Fp3.Normalize(Fp3.Cross(xaxis, yaxis));
            // }
            //
            // Fp4x4 result = new Fp4x4();
            //
            // result.M11 = xaxis.x;
            // result.M12 = xaxis.y;
            // result.M13 = xaxis.z;
            // result.M14 = 0;
            // result.M21 = yaxis.x;
            // result.M22 = yaxis.y;
            // result.M23 = yaxis.z;
            // result.M24 = 0;
            // result.M31 = zaxis.x;
            // result.M32 = zaxis.y;
            // result.M33 = zaxis.z;
            // result.M34 = 0;
            //
            // result.M41 = objectPosition.x;
            // result.M42 = objectPosition.y;
            // result.M43 = objectPosition.z;
            // result.M44 = Fp.One;
            //
            // return result;
        }

        /// <summary>
        /// Creates a translation matrix.
        /// </summary>
        /// <param name="position">The amount to translate in each axis.</param>
        /// <returns>The translation matrix.</returns>
        public static Fp4x4 CreateTranslation(Fp3 position)
        {
            Fp4x4 result = new Fp4x4();

            result.c0.x = Fp.One;
            result.c1.x = 0;
            result.c2.x = 0;
            result.c3.x = 0;
            result.c0.y = 0;
            result.c1.y = Fp.One;
            result.c2.y = 0;
            result.c3.y = 0;
            result.c0.z = 0;
            result.c1.z = 0;
            result.c2.z = Fp.One;
            result.c3.z = 0;

            result.c0.w = position.x;
            result.c1.w = position.y;
            result.c2.w = position.z;
            result.c3.w = Fp.One;

            return result;
        }

        /// <summary>
        /// Creates a translation matrix.
        /// </summary>
        /// <param name="xPosition">The amount to translate on the x-axis.</param>
        /// <param name="yPosition">The amount to translate on the y-axis.</param>
        /// <param name="zPosition">The amount to translate on the z-axis.</param>
        /// <returns>The translation matrix.</returns>
        public static Fp4x4 CreateTranslation(Fp xPosition, Fp yPosition, Fp zPosition)
        {
            Fp4x4 result = new Fp4x4();

            result.c0.x = Fp.One;
            result.c1.x = 0;
            result.c2.x = 0;
            result.c3.x = 0;
            result.c0.y = 0;
            result.c1.y = Fp.One;
            result.c2.y = 0;
            result.c3.y = 0;
            result.c0.z = 0;
            result.c1.z = 0;
            result.c2.z = Fp.One;
            result.c3.z = 0;

            result.c0.w = xPosition;
            result.c1.w = yPosition;
            result.c2.w = zPosition;
            result.c3.w = Fp.One;

            return result;
        }

        /// <summary>
        /// Creates a scaling matrix.
        /// </summary>
        /// <param name="xScale">Value to scale by on the x-axis.</param>
        /// <param name="yScale">Value to scale by on the y-axis.</param>
        /// <param name="zScale">Value to scale by on the z-axis.</param>
        /// <returns>The scaling matrix.</returns>
        public static Fp4x4 CreateScale(Fp xScale, Fp yScale, Fp zScale)
        {
            Fp4x4 result = new Fp4x4();

            result.c0.x = xScale;
            result.c1.x = 0;
            result.c2.x = 0;
            result.c3.x = 0;
            result.c0.y = 0;
            result.c1.y = yScale;
            result.c2.y = 0;
            result.c3.y = 0;
            result.c0.z = 0;
            result.c1.z = 0;
            result.c2.z = zScale;
            result.c3.z = 0;
            result.c0.w = 0;
            result.c1.w = 0;
            result.c2.w = 0;
            result.c3.w = Fp.One;

            return result;
        }

        /// <summary>
        /// Creates a scaling matrix with a center point.
        /// </summary>
        /// <param name="xScale">Value to scale by on the x-axis.</param>
        /// <param name="yScale">Value to scale by on the y-axis.</param>
        /// <param name="zScale">Value to scale by on the z-axis.</param>
        /// <param name="centerPoint">The center point.</param>
        /// <returns>The scaling matrix.</returns>
        public static Fp4x4 CreateScale(Fp xScale, Fp yScale, Fp zScale, Fp3 centerPoint)
        {
            Fp4x4 result = new Fp4x4();

            Fp tx = centerPoint.x * (1 - xScale);
            Fp ty = centerPoint.y * (1 - yScale);
            Fp tz = centerPoint.z * (1 - zScale);

            result.c0.x = xScale;
            result.c1.x = 0;
            result.c2.x = 0;
            result.c3.x = 0;
            result.c0.y = 0;
            result.c1.y = yScale;
            result.c2.y = 0;
            result.c3.y = 0;
            result.c0.z = 0;
            result.c1.z = 0;
            result.c2.z = zScale;
            result.c3.z = 0;
            result.c0.w = tx;
            result.c1.w = ty;
            result.c2.w = tz;
            result.c3.w = Fp.One;

            return result;
        }

        /// <summary>
        /// Creates a scaling matrix.
        /// </summary>
        /// <param name="scales">The vector containing the amount to scale by on each axis.</param>
        /// <returns>The scaling matrix.</returns>
        public static Fp4x4 CreateScale(Fp3 scales)
        {
            Fp4x4 result = new Fp4x4();

            result.c0.x = scales.x;
            result.c1.x = 0;
            result.c2.x = 0;
            result.c3.x = 0;
            result.c0.y = 0;
            result.c1.y = scales.y;
            result.c2.y = 0;
            result.c3.y = 0;
            result.c0.z = 0;
            result.c1.z = 0;
            result.c2.z = scales.z;
            result.c3.z = 0;
            result.c0.w = 0;
            result.c1.w = 0;
            result.c2.w = 0;
            result.c3.w = Fp.One;

            return result;
        }

        /// <summary>
        /// Creates a scaling matrix with a center point.
        /// </summary>
        /// <param name="scales">The vector containing the amount to scale by on each axis.</param>
        /// <param name="centerPoint">The center point.</param>
        /// <returns>The scaling matrix.</returns>
        public static Fp4x4 CreateScale(Fp3 scales, Fp3 centerPoint)
        {
            Fp4x4 result = new Fp4x4();

            Fp tx = centerPoint.x * (1 - scales.x);
            Fp ty = centerPoint.y * (1 - scales.y);
            Fp tz = centerPoint.z * (1 - scales.z);

            result.c0.x = scales.x;
            result.c1.x = 0;
            result.c2.x = 0;
            result.c3.x = 0;
            result.c0.y = 0;
            result.c1.y = scales.y;
            result.c2.y = 0;
            result.c3.y = 0;
            result.c0.z = 0;
            result.c1.z = 0;
            result.c2.z = scales.z;
            result.c3.z = 0;
            result.c0.w = tx;
            result.c1.w = ty;
            result.c2.w = tz;
            result.c3.w = Fp.One;

            return result;
        }

        /// <summary>
        /// Creates a uniform scaling matrix that scales equally on each axis.
        /// </summary>
        /// <param name="scale">The uniform scaling factor.</param>
        /// <returns>The scaling matrix.</returns>
        public static Fp4x4 CreateScale(Fp scale)
        {
            Fp4x4 result = new Fp4x4();

            result.c0.x = scale;
            result.c1.x = 0;
            result.c2.x = 0;
            result.c3.x = 0;
            result.c0.y = 0;
            result.c1.y = scale;
            result.c2.y = 0;
            result.c3.y = 0;
            result.c0.z = 0;
            result.c1.z = 0;
            result.c2.z = scale;
            result.c3.z = 0;
            result.c0.w = 0;
            result.c1.w = 0;
            result.c2.w = 0;
            result.c3.w = Fp.One;

            return result;
        }

        /// <summary>
        /// Creates a uniform scaling matrix that scales equally on each axis with a center point.
        /// </summary>
        /// <param name="scale">The uniform scaling factor.</param>
        /// <param name="centerPoint">The center point.</param>
        /// <returns>The scaling matrix.</returns>
        public static Fp4x4 CreateScale(Fp scale, Fp3 centerPoint)
        {
            Fp4x4 result = new Fp4x4();

            Fp tx = centerPoint.x * (1 - scale);
            Fp ty = centerPoint.y * (1 - scale);
            Fp tz = centerPoint.z * (1 - scale);

            result.c0.x = scale;
            result.c1.x = 0;
            result.c2.x = 0;
            result.c3.x = 0;
            result.c0.y = 0;
            result.c1.y = scale;
            result.c2.y = 0;
            result.c3.y = 0;
            result.c0.z = 0;
            result.c1.z = 0;
            result.c2.z = scale;
            result.c3.z = 0;
            result.c0.w = tx;
            result.c1.w = ty;
            result.c2.w = tz;
            result.c3.w = Fp.One;

            return result;
        }

        /// <summary>
        /// Creates a matrix for rotating points around the x-axis.
        /// </summary>
        /// <param name="radians">The amount, in radians, by which to rotate around the x-axis.</param>
        /// <returns>The rotation matrix.</returns>
        public static Fp4x4 CreateRotationX(Fp radians)
        {
            Fp4x4 result = new Fp4x4();

            Fp c = MathFp.cos(radians);
            Fp s = MathFp.sin(radians);

            // [  1  0  0  0 ]
            // [  0  c  s  0 ]
            // [  0 -s  c  0 ]
            // [  0  0  0  1 ]
            result.c0.x = Fp.One;
            result.c1.x = 0;
            result.c2.x = 0;
            result.c3.x = 0;
            result.c0.y = 0;
            result.c1.y = c;
            result.c2.y = s;
            result.c3.y = 0;
            result.c0.z = 0;
            result.c1.z = -s;
            result.c2.z = c;
            result.c3.z = 0;
            result.c0.w = 0;
            result.c1.w = 0;
            result.c2.w = 0;
            result.c3.w = Fp.One;

            return result;
        }

        /// <summary>
        /// Creates a matrix for rotating points around the x-axis, from a center point.
        /// </summary>
        /// <param name="radians">The amount, in radians, by which to rotate around the x-axis.</param>
        /// <param name="centerPoint">The center point.</param>
        /// <returns>The rotation matrix.</returns>
        public static Fp4x4 CreateRotationX(Fp radians, Fp3 centerPoint)
        {
            Fp4x4 result = new Fp4x4();

            Fp c = MathFp.cos(radians);
            Fp s = MathFp.sin(radians);

            Fp y = centerPoint.y * (1 - c) + centerPoint.z * s;
            Fp z = centerPoint.z * (1 - c) - centerPoint.y * s;

            // [  1  0  0  0 ]
            // [  0  c  s  0 ]
            // [  0 -s  c  0 ]
            // [  0  y  z  1 ]
            result.c0.x = Fp.One;
            result.c1.x = 0;
            result.c2.x = 0;
            result.c3.x = 0;
            result.c0.y = 0;
            result.c1.y = c;
            result.c2.y = s;
            result.c3.y = 0;
            result.c0.z = 0;
            result.c1.z = -s;
            result.c2.z = c;
            result.c3.z = 0;
            result.c0.w = 0;
            result.c1.w = y;
            result.c2.w = z;
            result.c3.w = Fp.One;

            return result;
        }

        /// <summary>
        /// Creates a matrix for rotating points around the y-axis.
        /// </summary>
        /// <param name="radians">The amount, in radians, by which to rotate around the y-axis.</param>
        /// <returns>The rotation matrix.</returns>
        public static Fp4x4 CreateRotationY(Fp radians)
        {
            Fp4x4 result = new Fp4x4();

            Fp c = MathFp.cos(radians);
            Fp s = MathFp.sin(radians);

            // [  c  0 -s  0 ]
            // [  0  1  0  0 ]
            // [  s  0  c  0 ]
            // [  0  0  0  1 ]
            result.c0.x = c;
            result.c1.x = 0;
            result.c2.x = -s;
            result.c3.x = 0;
            result.c0.y = 0;
            result.c1.y = Fp.One;
            result.c2.y = 0;
            result.c3.y = 0;
            result.c0.z = s;
            result.c1.z = 0;
            result.c2.z = c;
            result.c3.z = 0;
            result.c0.w = 0;
            result.c1.w = 0;
            result.c2.w = 0;
            result.c3.w = Fp.One;

            return result;
        }

        /// <summary>
        /// Creates a matrix for rotating points around the y-axis, from a center point.
        /// </summary>
        /// <param name="radians">The amount, in radians, by which to rotate around the y-axis.</param>
        /// <param name="centerPoint">The center point.</param>
        /// <returns>The rotation matrix.</returns>
        public static Fp4x4 CreateRotationY(Fp radians, Fp3 centerPoint)
        {
            Fp4x4 result = new Fp4x4();

            Fp c = MathFp.cos(radians);
            Fp s = MathFp.sin(radians);

            Fp x = centerPoint.x * (1 - c) - centerPoint.z * s;
            Fp z = centerPoint.z * (1 - c) + centerPoint.x * s;

            // [  c  0 -s  0 ]
            // [  0  1  0  0 ]
            // [  s  0  c  0 ]
            // [  x  0  z  1 ]
            result.c0.x = c;
            result.c1.x = 0;
            result.c2.x = -s;
            result.c3.x = 0;
            result.c0.y = 0;
            result.c1.y = Fp.One;
            result.c2.y = 0;
            result.c3.y = 0;
            result.c0.z = s;
            result.c1.z = 0;
            result.c2.z = c;
            result.c3.z = 0;
            result.c0.w = x;
            result.c1.w = 0;
            result.c2.w = z;
            result.c3.w = Fp.One;

            return result;
        }

        /// <summary>
        /// Creates a matrix for rotating points around the z-axis.
        /// </summary>
        /// <param name="radians">The amount, in radians, by which to rotate around the z-axis.</param>
        /// <returns>The rotation matrix.</returns>
        public static Fp4x4 CreateRotationZ(Fp radians)
        {
            Fp4x4 result = new Fp4x4();

            Fp c = MathFp.cos(radians);
            Fp s = MathFp.sin(radians);

            // [  c  s  0  0 ]
            // [ -s  c  0  0 ]
            // [  0  0  1  0 ]
            // [  0  0  0  1 ]
            result.c0.x = c;
            result.c1.x = s;
            result.c2.x = 0;
            result.c3.x = 0;
            result.c0.y = -s;
            result.c1.y = c;
            result.c2.y = 0;
            result.c3.y = 0;
            result.c0.z = 0;
            result.c1.z = 0;
            result.c2.z = Fp.One;
            result.c3.z = 0;
            result.c0.w = 0;
            result.c1.w = 0;
            result.c2.w = 0;
            result.c3.w = Fp.One;

            return result;
        }

        /// <summary>
        /// Creates a matrix for rotating points around the z-axis, from a center point.
        /// </summary>
        /// <param name="radians">The amount, in radians, by which to rotate around the z-axis.</param>
        /// <param name="centerPoint">The center point.</param>
        /// <returns>The rotation matrix.</returns>
        public static Fp4x4 CreateRotationZ(Fp radians, Fp3 centerPoint)
        {
            Fp4x4 result = new Fp4x4();

            Fp c = MathFp.cos(radians);
            Fp s = MathFp.sin(radians);

            Fp x = centerPoint.x * (1 - c) + centerPoint.y * s;
            Fp y = centerPoint.y * (1 - c) - centerPoint.x * s;

            // [  c  s  0  0 ]
            // [ -s  c  0  0 ]
            // [  0  0  1  0 ]
            // [  x  y  0  1 ]
            result.c0.x = c;
            result.c1.x = s;
            result.c2.x = 0;
            result.c3.x = 0;
            result.c0.y = -s;
            result.c1.y = c;
            result.c2.y = 0;
            result.c3.y = 0;
            result.c0.z = 0;
            result.c1.z = 0;
            result.c2.z = Fp.One;
            result.c3.z = 0;
            result.c0.w = x;
            result.c1.w = y;
            result.c2.w = 0;
            result.c3.w = Fp.One;

            return result;
        }

        /// <summary>
        /// Creates a matrix that rotates around an arbitrary vector.
        /// </summary>
        /// <param name="axis">The axis to rotate around.</param>
        /// <param name="angle">The angle to rotate around the given axis, in radians.</param>
        /// <returns>The rotation matrix.</returns>
        public static Fp4x4 CreateFromAxisAngle(Fp3 axis, Fp angle)
        {
            // a: angle
            // x, y, z: unit vector for axis.
            //
            // Rotation matrix M can compute by using below equation.
            //
            //        T               T
            //  M = uu + (cos a)( I-uu ) + (sin a)S
            //
            // Where:
            //
            //  u = ( x, y, z )
            //
            //      [  0 -z  y ]
            //  S = [  z  0 -x ]
            //      [ -y  x  0 ]
            //
            //      [ 1 0 0 ]
            //  I = [ 0 1 0 ]
            //      [ 0 0 1 ]
            //
            //
            //     [  xx+cosa*(1-xx)   yx-cosa*yx-sina*z zx-cosa*xz+sina*y ]
            // M = [ xy-cosa*yx+sina*z    yy+cosa(1-yy)  yz-cosa*yz-sina*x ]
            //     [ zx-cosa*zx-sina*y zy-cosa*zy+sina*x   zz+cosa*(1-zz)  ]
            //
            Fp x = axis.x, y = axis.y, z = axis.z;
            Fp sa = MathFp.sin(angle), ca = MathFp.cos(angle);
            Fp xx = x * x, yy = y * y, zz = z * z;
            Fp xy = x * y, xz = x * z, yz = y * z;

            Fp4x4 result = new Fp4x4();

            result.c0.x = xx + ca * (Fp.One - xx);
            result.c1.x = xy - ca * xy + sa * z;
            result.c2.x = xz - ca * xz - sa * y;
            result.c3.x = 0;
            result.c0.y = xy - ca * xy - sa * z;
            result.c1.y = yy + ca * (Fp.One - yy);
            result.c2.y = yz - ca * yz + sa * x;
            result.c3.y = 0;
            result.c0.z = xz - ca * xz + sa * y;
            result.c1.z = yz - ca * yz - sa * x;
            result.c2.z = zz + ca * (Fp.One - zz);
            result.c3.z = 0;
            result.c0.w = 0;
            result.c1.w = 0;
            result.c2.w = 0;
            result.c3.w = Fp.One;

            return result;
        }

        /// <summary>
        /// Creates a perspective projection matrix based on a field of view, aspect ratio, and near and far view plane distances.
        /// </summary>
        /// <param name="fieldOfView">Field of view in the y direction, in radians.</param>
        /// <param name="aspectRatio">Aspect ratio, defined as view space width divided by height.</param>
        /// <param name="nearPlaneDistance">Distance to the near view plane.</param>
        /// <param name="farPlaneDistance">Distance to the far view plane.</param>
        /// <returns>The perspective projection matrix.</returns>
        public static Fp4x4 CreatePerspectiveFieldOfView(Fp fieldOfView, Fp aspectRatio,
            Fp nearPlaneDistance, Fp farPlaneDistance)
        {
            if (fieldOfView <= 0 || fieldOfView >= Fp.Pi)
                throw new ArgumentOutOfRangeException("fieldOfView");

            if (nearPlaneDistance <= 0)
                throw new ArgumentOutOfRangeException("nearPlaneDistance");

            if (farPlaneDistance <= 0)
                throw new ArgumentOutOfRangeException("farPlaneDistance");

            if (nearPlaneDistance >= farPlaneDistance)
                throw new ArgumentOutOfRangeException("nearPlaneDistance");

            Fp yScale = Fp.One / MathFp.tan(fieldOfView * Fp.Half);
            Fp xScale = yScale / aspectRatio;

            Fp4x4 result = new Fp4x4();

            result.c0.x = xScale;
            result.c1.x = result.c2.x = result.c3.x = 0;

            result.c1.y = yScale;
            result.c0.y = result.c2.y = result.c3.y = 0;

            result.c0.z = result.c1.z = 0;
            result.c2.z = farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
            result.c3.z = -Fp.One;

            result.c0.w = result.c1.w = result.c3.w = 0;
            result.c2.w = nearPlaneDistance * farPlaneDistance / (nearPlaneDistance - farPlaneDistance);

            return result;
        }

        /// <summary>
        /// Creates a perspective projection matrix from the given view volume dimensions.
        /// </summary>
        /// <param name="width">Width of the view volume at the near view plane.</param>
        /// <param name="height">Height of the view volume at the near view plane.</param>
        /// <param name="nearPlaneDistance">Distance to the near view plane.</param>
        /// <param name="farPlaneDistance">Distance to the far view plane.</param>
        /// <returns>The perspective projection matrix.</returns>
        public static Fp4x4 CreatePerspective(Fp width, Fp height, Fp nearPlaneDistance,
            Fp farPlaneDistance)
        {
            if (nearPlaneDistance <= 0)
                throw new ArgumentOutOfRangeException("nearPlaneDistance");

            if (farPlaneDistance <= 0)
                throw new ArgumentOutOfRangeException("farPlaneDistance");

            if (nearPlaneDistance >= farPlaneDistance)
                throw new ArgumentOutOfRangeException("nearPlaneDistance");

            Fp4x4 result = new Fp4x4();

            result.c0.x = Fp.Two * nearPlaneDistance / width;
            result.c1.x = result.c2.x = result.c3.x = 0;

            result.c1.y = Fp.Two * nearPlaneDistance / height;
            result.c0.y = result.c2.y = result.c3.y = 0;

            result.c2.z = farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
            result.c0.z = result.c1.z = 0;
            result.c3.z = -Fp.One;

            result.c0.w = result.c1.w = result.c3.w = 0;
            result.c2.w = nearPlaneDistance * farPlaneDistance / (nearPlaneDistance - farPlaneDistance);

            return result;
        }

        /// <summary>
        /// Creates a customized, perspective projection matrix.
        /// </summary>
        /// <param name="left">Minimum x-value of the view volume at the near view plane.</param>
        /// <param name="right">Maximum x-value of the view volume at the near view plane.</param>
        /// <param name="bottom">Minimum y-value of the view volume at the near view plane.</param>
        /// <param name="top">Maximum y-value of the view volume at the near view plane.</param>
        /// <param name="nearPlaneDistance">Distance to the near view plane.</param>
        /// <param name="farPlaneDistance">Distance to of the far view plane.</param>
        /// <returns>The perspective projection matrix.</returns>
        public static Fp4x4 CreatePerspectiveOffCenter(Fp left, Fp right, Fp bottom, Fp top,
            Fp nearPlaneDistance, Fp farPlaneDistance)
        {
            if (nearPlaneDistance <= 0)
                throw new ArgumentOutOfRangeException("nearPlaneDistance");

            if (farPlaneDistance <= 0)
                throw new ArgumentOutOfRangeException("farPlaneDistance");

            if (nearPlaneDistance >= farPlaneDistance)
                throw new ArgumentOutOfRangeException("nearPlaneDistance");

            Fp4x4 result = new Fp4x4();

            result.c0.x = Fp.Two * nearPlaneDistance / (right - left);
            result.c1.x = result.c2.x = result.c3.x = 0;

            result.c1.y = Fp.Two * nearPlaneDistance / (top - bottom);
            result.c0.y = result.c2.y = result.c3.y = 0;

            result.c0.z = (left + right) / (right - left);
            result.c1.z = (top + bottom) / (top - bottom);
            result.c2.z = farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
            result.c3.z = -Fp.One;

            result.c2.w = nearPlaneDistance * farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
            result.c0.w = result.c1.w = result.c3.w = 0;

            return result;
        }

        /// <summary>
        /// Creates an orthographic perspective matrix from the given view volume dimensions.
        /// </summary>
        /// <param name="width">Width of the view volume.</param>
        /// <param name="height">Height of the view volume.</param>
        /// <param name="zNearPlane">Minimum z-value of the view volume.</param>
        /// <param name="zFarPlane">Maximum z-value of the view volume.</param>
        /// <returns>The orthographic projection matrix.</returns>
        public static Fp4x4 CreateOrthographic(Fp width, Fp height, Fp zNearPlane, Fp zFarPlane)
        {
            Fp4x4 result = new Fp4x4();

            result.c0.x = Fp.Two / width;
            result.c1.x = result.c2.x = result.c3.x = 0;

            result.c1.y = Fp.Two / height;
            result.c0.y = result.c2.y = result.c3.y = 0;

            result.c2.z = Fp.One / (zNearPlane - zFarPlane);
            result.c0.z = result.c1.z = result.c3.z = 0;

            result.c0.w = result.c1.w = 0;
            result.c2.w = zNearPlane / (zNearPlane - zFarPlane);
            result.c3.w = Fp.One;

            return result;
        }

        /// <summary>
        /// Builds a customized, orthographic projection matrix.
        /// </summary>
        /// <param name="left">Minimum x-value of the view volume.</param>
        /// <param name="right">Maximum x-value of the view volume.</param>
        /// <param name="bottom">Minimum y-value of the view volume.</param>
        /// <param name="top">Maximum y-value of the view volume.</param>
        /// <param name="zNearPlane">Minimum z-value of the view volume.</param>
        /// <param name="zFarPlane">Maximum z-value of the view volume.</param>
        /// <returns>The orthographic projection matrix.</returns>
        public static Fp4x4 CreateOrthographicOffCenter(Fp left, Fp right, Fp bottom, Fp top,
            Fp zNearPlane, Fp zFarPlane)
        {
            Fp4x4 result = new Fp4x4();

            result.c0.x = Fp.Two / (right - left);
            result.c1.x = result.c2.x = result.c3.x = 0;

            result.c1.y = Fp.Two / (top - bottom);
            result.c0.y = result.c2.y = result.c3.y = 0;

            result.c2.z = Fp.One / (zNearPlane - zFarPlane);
            result.c0.z = result.c1.z = result.c3.z = 0;

            result.c0.w = (left + right) / (left - right);
            result.c1.w = (top + bottom) / (bottom - top);
            result.c2.w = zNearPlane / (zNearPlane - zFarPlane);
            result.c3.w = Fp.One;

            return result;
        }

        /// <summary>
        /// Creates a view matrix.
        /// </summary>
        /// <param name="cameraPosition">The position of the camera.</param>
        /// <param name="cameraTarget">The target towards which the camera is pointing.</param>
        /// <param name="cameraUpVector">The direction that is "up" from the camera's point of view.</param>
        /// <returns>The view matrix.</returns>
        public static Fp4x4 CreateLookAt(Fp3 cameraPosition, Fp3 cameraTarget, Fp3 cameraUpVector)
        {
            Fp3 zaxis = Fp3.Normalize(cameraPosition - cameraTarget);
            Fp3 xaxis = Fp3.Normalize(Fp3.Cross(cameraUpVector, zaxis));
            Fp3 yaxis = Fp3.Cross(zaxis, xaxis);

            Fp4x4 result = new Fp4x4();

            result.c0.x = xaxis.x;
            result.c1.x = yaxis.x;
            result.c2.x = zaxis.x;
            result.c3.x = 0;
            result.c0.y = xaxis.y;
            result.c1.y = yaxis.y;
            result.c2.y = zaxis.y;
            result.c3.y = 0;
            result.c0.z = xaxis.z;
            result.c1.z = yaxis.z;
            result.c2.z = zaxis.z;
            result.c3.z = 0;
            result.c0.w = -Fp3.Dot(xaxis, cameraPosition);
            result.c1.w = -Fp3.Dot(yaxis, cameraPosition);
            result.c2.w = -Fp3.Dot(zaxis, cameraPosition);
            result.c3.w = Fp.One;

            return result;
        }

        /// <summary>
        /// Creates a world matrix with the specified parameters.
        /// </summary>
        /// <param name="position">The position of the object; used in translation operations.</param>
        /// <param name="forward">Forward direction of the object.</param>
        /// <param name="up">Upward direction of the object; usually [0, 1, 0].</param>
        /// <returns>The world matrix.</returns>
        public static Fp4x4 CreateWorld(Fp3 position, Fp3 forward, Fp3 up)
        {
            Fp3 zaxis = Fp3.Normalize(-forward);
            Fp3 xaxis = Fp3.Normalize(Fp3.Cross(up, zaxis));
            Fp3 yaxis = Fp3.Cross(zaxis, xaxis);

            Fp4x4 result = new Fp4x4();

            result.c0.x = xaxis.x;
            result.c1.x = xaxis.y;
            result.c2.x = xaxis.z;
            result.c3.x = 0;
            result.c0.y = yaxis.x;
            result.c1.y = yaxis.y;
            result.c2.y = yaxis.z;
            result.c3.y = 0;
            result.c0.z = zaxis.x;
            result.c1.z = zaxis.y;
            result.c2.z = zaxis.z;
            result.c3.z = 0;
            result.c0.w = position.x;
            result.c1.w = position.y;
            result.c2.w = position.z;
            result.c3.w = Fp.One;

            return result;
        }

        /// <summary>
        /// Creates a rotation matrix from the given Quaternion rotation value.
        /// </summary>
        /// <param name="QuaternionFp">The source Quaternion.</param>
        /// <returns>The rotation matrix.</returns>
        public static Fp4x4 CreateFromQuaternion(QuaternionFp QuaternionFp)
        {
            Fp4x4 result = new Fp4x4();

            Fp xx = QuaternionFp.value.x * QuaternionFp.value.x;
            Fp yy = QuaternionFp.value.y * QuaternionFp.value.y;
            Fp zz = QuaternionFp.value.z * QuaternionFp.value.z;

            Fp xy = QuaternionFp.value.x * QuaternionFp.value.y;
            Fp wz = QuaternionFp.value.z * QuaternionFp.value.w;
            Fp xz = QuaternionFp.value.z * QuaternionFp.value.x;
            Fp wy = QuaternionFp.value.y * QuaternionFp.value.w;
            Fp yz = QuaternionFp.value.y * QuaternionFp.value.z;
            Fp wx = QuaternionFp.value.x * QuaternionFp.value.w;

            result.c0.x = Fp.One - Fp.Two * (yy + zz);
            result.c1.x = Fp.Two * (xy + wz);
            result.c2.x = Fp.Two * (xz - wy);
            result.c3.x = 0;
            result.c0.y = Fp.Two * (xy - wz);
            result.c1.y = Fp.One - Fp.Two * (zz + xx);
            result.c2.y = Fp.Two * (yz + wx);
            result.c3.y = 0;
            result.c0.z = Fp.Two * (xz + wy);
            result.c1.z = Fp.Two * (yz - wx);
            result.c2.z = Fp.One - Fp.Two * (yy + xx);
            result.c3.z = 0;
            result.c0.w = 0;
            result.c1.w = 0;
            result.c2.w = 0;
            result.c3.w = Fp.One;

            return result;
        }

        /// <summary>
        /// Creates a rotation matrix from the specified yaw, pitch, and roll.
        /// </summary>
        /// <param name="yaw">Angle of rotation, in degree, around the y-axis.</param>
        /// <param name="pitch">Angle of rotation, in degree, around the x-axis.</param>
        /// <param name="roll">Angle of rotation, in degree, around the z-axis.</param>
        /// <returns>The rotation matrix.</returns>
        public static Fp4x4 CreateFromYawPitchRoll(Fp pitch, Fp yaw, Fp roll)
        {
            throw new NotImplementedException();
            // QuaternionFp q = QuaternionFp.CreateFromYawPitchRoll(pitch, yaw, roll);
            //
            // return Fp4x4.CreateFromQuaternion(q);
        }

        // /// <summary>
        // /// Creates a Matrix that flattens geometry into a specified Plane as if casting a shadow from a specified light source.
        // /// </summary>
        // /// <param name="lightDirection">The direction from which the light that will cast the shadow is coming.</param>
        // /// <param name="plane">The Plane onto which the new matrix should flatten geometry so as to cast a shadow.</param>
        // /// <returns>A new Matrix that can be used to flatten geometry onto the specified plane from the specified direction.</returns>
        // public static Fp4x4 CreateShadow(Fp3 lightDirection, Plane plane)
        // {
        //     Plane p = Plane.Normalize(plane);
        //
        //     Fp dot = p.Normal.x * lightDirection.x + p.Normal.y * lightDirection.y + p.Normal.z * lightDirection.z;
        //     Fp a = -p.Normal.x;
        //     Fp b = -p.Normal.y;
        //     Fp c = -p.Normal.z;
        //     Fp d = -p.D;
        //
        //     Fp4x4 result = new Fp4x4();
        //
        //     result.M11 = a * lightDirection.x + dot;
        //     result.M21 = b * lightDirection.x;
        //     result.M31 = c * lightDirection.x;
        //     result.M41 = d * lightDirection.x;
        //
        //     result.M12 = a * lightDirection.y;
        //     result.M22 = b * lightDirection.y + dot;
        //     result.M32 = c * lightDirection.y;
        //     result.M42 = d * lightDirection.y;
        //
        //     result.M13 = a * lightDirection.z;
        //     result.M23 = b * lightDirection.z;
        //     result.M33 = c * lightDirection.z + dot;
        //     result.M43 = d * lightDirection.z;
        //
        //     result.M14 = 0;
        //     result.M24 = 0;
        //     result.M34 = 0;
        //     result.M44 = dot;
        //
        //     return result;
        // }

        // /// <summary>
        // /// Creates a Matrix that reflects the coordinate system about a specified Plane.
        // /// </summary>
        // /// <param name="value">The Plane about which to create a reflection.</param>
        // /// <returns>A new matrix expressing the reflection.</returns>
        // public static Fp4x4 CreateReflection(Plane value)
        // {
        //     value = Plane.Normalize(value);
        //
        //     Fp a = value.Normal.x;
        //     Fp b = value.Normal.y;
        //     Fp c = value.Normal.z;
        //
        //     Fp fa = -Fp.Two * a;
        //     Fp fb = -Fp.Two * b;
        //     Fp fc = -Fp.Two * c;
        //
        //     Fp4x4 result = new Fp4x4();
        //
        //     result.M11 = fa * a + Fp.One;
        //     result.M12 = fb * a;
        //     result.M13 = fc * a;
        //     result.M14 = 0;
        //
        //     result.M21 = fa * b;
        //     result.M22 = fb * b + Fp.One;
        //     result.M23 = fc * b;
        //     result.M24 = 0;
        //
        //     result.M31 = fa * c;
        //     result.M32 = fb * c;
        //     result.M33 = fc * c + Fp.One;
        //     result.M34 = 0;
        //
        //     result.M41 = fa * value.D;
        //     result.M42 = fb * value.D;
        //     result.M43 = fc * value.D;
        //     result.M44 = Fp.One;
        //
        //     return result;
        // }

        /// <summary>
        /// Calculates the determinant of the matrix.
        /// </summary>
        /// <returns>The determinant of the matrix.</returns>
        public Fp GetDeterminant()
        {
            // | a b c d |     | f g h |     | e g h |     | e f h |     | e f g |
            // | e f g h | = a | j k l | - b | i k l | + c | i j l | - d | i j k |
            // | i j k l |     | n o p |     | m o p |     | m n p |     | m n o |
            // | m n o p |
            //
            //   | f g h |
            // a | j k l | = a ( f ( kp - lo ) - g ( jp - ln ) + h ( jo - kn ) )
            //   | n o p |
            //
            //   | e g h |
            // b | i k l | = b ( e ( kp - lo ) - g ( ip - lm ) + h ( io - km ) )
            //   | m o p |
            //
            //   | e f h |
            // c | i j l | = c ( e ( jp - ln ) - f ( ip - lm ) + h ( in - jm ) )
            //   | m n p |
            //
            //   | e f g |
            // d | i j k | = d ( e ( jo - kn ) - f ( io - km ) + g ( in - jm ) )
            //   | m n o |
            //
            // Cost of operation
            // 17 adds and 28 muls.
            //
            // add: 6 + 8 + 3 = 17
            // mul: 12 + 16 = 28

            Fp a = c0.x, b = c1.x, c = c2.x, d = c3.x;
            Fp e = c0.y, f = c1.y, g = c2.y, h = c3.y;
            Fp i = c0.z, j = c1.z, k = c2.z, l = c3.z;
            Fp m = c0.w, n = c1.w, o = c2.w, p = c3.w;

            Fp kp_lo = k * p - l * o;
            Fp jp_ln = j * p - l * n;
            Fp jo_kn = j * o - k * n;
            Fp ip_lm = i * p - l * m;
            Fp io_km = i * o - k * m;
            Fp in_jm = i * n - j * m;

            return a * (f * kp_lo - g * jp_ln + h * jo_kn) -
                   b * (e * kp_lo - g * ip_lm + h * io_km) +
                   c * (e * jp_ln - f * ip_lm + h * in_jm) -
                   d * (e * jo_kn - f * io_km + g * in_jm);
        }

        /// <summary>
        /// Attempts to calculate the inverse of the given matrix. If successful, result will contain the inverted matrix.
        /// </summary>
        /// <param name="matrix">The source matrix to invert.</param>
        /// <param name="result">If successful, contains the inverted matrix.</param>
        /// <returns>True if the source matrix could be inverted; False otherwise.</returns>
        public static bool Inverse(Fp4x4 matrix, out Fp4x4 result)
        {
            //                                       -1
            // If you have matrix M, inverse Matrix M   can compute
            //
            //     -1       1
            //    M   = --------- A
            //            det(M)
            //
            // A is adjugate (adjoint) of M, where,
            //
            //      T
            // A = C
            //
            // C is Cofactor matrix of M, where,
            //           i + j
            // C   = (-1)      * det(M  )
            //  ij                    ij
            //
            //     [ a b c d ]
            // M = [ e f g h ]
            //     [ i j k l ]
            //     [ m n o p ]
            //
            // First Row
            //           2 | f g h |
            // C   = (-1)  | j k l | = + ( f ( kp - lo ) - g ( jp - ln ) + h ( jo - kn ) )
            //  11         | n o p |
            //
            //           3 | e g h |
            // C   = (-1)  | i k l | = - ( e ( kp - lo ) - g ( ip - lm ) + h ( io - km ) )
            //  12         | m o p |
            //
            //           4 | e f h |
            // C   = (-1)  | i j l | = + ( e ( jp - ln ) - f ( ip - lm ) + h ( in - jm ) )
            //  13         | m n p |
            //
            //           5 | e f g |
            // C   = (-1)  | i j k | = - ( e ( jo - kn ) - f ( io - km ) + g ( in - jm ) )
            //  14         | m n o |
            //
            // Second Row
            //           3 | b c d |
            // C   = (-1)  | j k l | = - ( b ( kp - lo ) - c ( jp - ln ) + d ( jo - kn ) )
            //  21         | n o p |
            //
            //           4 | a c d |
            // C   = (-1)  | i k l | = + ( a ( kp - lo ) - c ( ip - lm ) + d ( io - km ) )
            //  22         | m o p |
            //
            //           5 | a b d |
            // C   = (-1)  | i j l | = - ( a ( jp - ln ) - b ( ip - lm ) + d ( in - jm ) )
            //  23         | m n p |
            //
            //           6 | a b c |
            // C   = (-1)  | i j k | = + ( a ( jo - kn ) - b ( io - km ) + c ( in - jm ) )
            //  24         | m n o |
            //
            // Third Row
            //           4 | b c d |
            // C   = (-1)  | f g h | = + ( b ( gp - ho ) - c ( fp - hn ) + d ( fo - gn ) )
            //  31         | n o p |
            //
            //           5 | a c d |
            // C   = (-1)  | e g h | = - ( a ( gp - ho ) - c ( ep - hm ) + d ( eo - gm ) )
            //  32         | m o p |
            //
            //           6 | a b d |
            // C   = (-1)  | e f h | = + ( a ( fp - hn ) - b ( ep - hm ) + d ( en - fm ) )
            //  33         | m n p |
            //
            //           7 | a b c |
            // C   = (-1)  | e f g | = - ( a ( fo - gn ) - b ( eo - gm ) + c ( en - fm ) )
            //  34         | m n o |
            //
            // Fourth Row
            //           5 | b c d |
            // C   = (-1)  | f g h | = - ( b ( gl - hk ) - c ( fl - hj ) + d ( fk - gj ) )
            //  41         | j k l |
            //
            //           6 | a c d |
            // C   = (-1)  | e g h | = + ( a ( gl - hk ) - c ( el - hi ) + d ( ek - gi ) )
            //  42         | i k l |
            //
            //           7 | a b d |
            // C   = (-1)  | e f h | = - ( a ( fl - hj ) - b ( el - hi ) + d ( ej - fi ) )
            //  43         | i j l |
            //
            //           8 | a b c |
            // C   = (-1)  | e f g | = + ( a ( fk - gj ) - b ( ek - gi ) + c ( ej - fi ) )
            //  44         | i j k |
            //
            // Cost of operation
            // 53 adds, 104 muls, and 1 div.
            Fp a = matrix.c0.x, b = matrix.c1.x, c = matrix.c2.x, d = matrix.c3.x;
            Fp e = matrix.c0.y, f = matrix.c1.y, g = matrix.c2.y, h = matrix.c3.y;
            Fp i = matrix.c0.z, j = matrix.c1.z, k = matrix.c2.z, l = matrix.c3.z;
            Fp m = matrix.c0.w, n = matrix.c1.w, o = matrix.c2.w, p = matrix.c3.w;

            Fp kp_lo = k * p - l * o;
            Fp jp_ln = j * p - l * n;
            Fp jo_kn = j * o - k * n;
            Fp ip_lm = i * p - l * m;
            Fp io_km = i * o - k * m;
            Fp in_jm = i * n - j * m;

            Fp a11 = +(f * kp_lo - g * jp_ln + h * jo_kn);
            Fp a12 = -(e * kp_lo - g * ip_lm + h * io_km);
            Fp a13 = +(e * jp_ln - f * ip_lm + h * in_jm);
            Fp a14 = -(e * jo_kn - f * io_km + g * in_jm);

            Fp det = a * a11 + b * a12 + c * a13 + d * a14;

            if (abs(det) < Fp.Epsilon)
            {
                Fp nan = Fp.NaN;
                result = new Fp4x4(nan, nan, nan, nan,
                    nan, nan, nan, nan,
                    nan, nan, nan, nan,
                    nan, nan, nan, nan);
                return false;
            }

            Fp invDet = Fp.One / det;
            result = new Fp4x4();
            result.c0.x = a11 * invDet;
            result.c0.y = a12 * invDet;
            result.c0.z = a13 * invDet;
            result.c0.w = a14 * invDet;

            result.c1.x = -(b * kp_lo - c * jp_ln + d * jo_kn) * invDet;
            result.c1.y = +(a * kp_lo - c * ip_lm + d * io_km) * invDet;
            result.c1.z = -(a * jp_ln - b * ip_lm + d * in_jm) * invDet;
            result.c1.w = +(a * jo_kn - b * io_km + c * in_jm) * invDet;

            Fp gp_ho = g * p - h * o;
            Fp fp_hn = f * p - h * n;
            Fp fo_gn = f * o - g * n;
            Fp ep_hm = e * p - h * m;
            Fp eo_gm = e * o - g * m;
            Fp en_fm = e * n - f * m;

            result.c2.x = +(b * gp_ho - c * fp_hn + d * fo_gn) * invDet;
            result.c2.y = -(a * gp_ho - c * ep_hm + d * eo_gm) * invDet;
            result.c2.z = +(a * fp_hn - b * ep_hm + d * en_fm) * invDet;
            result.c2.w = -(a * fo_gn - b * eo_gm + c * en_fm) * invDet;

            Fp gl_hk = g * l - h * k;
            Fp fl_hj = f * l - h * j;
            Fp fk_gj = f * k - g * j;
            Fp el_hi = e * l - h * i;
            Fp ek_gi = e * k - g * i;
            Fp ej_fi = e * j - f * i;

            result.c3.x = -(b * gl_hk - c * fl_hj + d * fk_gj) * invDet;
            result.c3.y = +(a * gl_hk - c * el_hi + d * ek_gi) * invDet;
            result.c3.z = -(a * fl_hj - b * el_hi + d * ej_fi) * invDet;
            result.c3.w = +(a * fk_gj - b * ek_gi + c * ej_fi) * invDet;

            return true;
        }

        // struct CanonicalBasis
        // {
        //     public Fp3 Row0;
        //     public Fp3 Row1;
        //     public Fp3 Row2;
        // };

        // [System.Security.SecuritySafeCritical]
        // struct VectorBasis
        // {
        //     public unsafe Fp3* Element0;
        //     public unsafe Fp3* Element1;
        //     public unsafe Fp3* Element2;
        // }

        /// <summary>
        /// Attempts to extract the scale, translation, and rotation components from the given scale/rotation/translation matrix.
        /// If successful, the out parameters will contained the extracted values.
        /// </summary>
        /// <param name="matrix">The source matrix.</param>
        /// <param name="scale">The scaling component of the transformation matrix.</param>
        /// <param name="rotation">The rotation component of the transformation matrix.</param>
        /// <param name="translation">The translation component of the transformation matrix</param>
        /// <returns>True if the source matrix was successfully decomposed; False otherwise.</returns>
        [System.Security.SecuritySafeCritical]
        public static bool Decompose(Fp4x4 matrix, out Fp3 scale, out QuaternionFp rotation,
            out Fp3 translation)
        {
            // bool result = true;
            //
            // unsafe
            // {
            //     fixed (Fp3* scaleBase = &scale)
            //     {
            //         Fp* pfScales = (Fp*)scaleBase;
            //         Fp det;
            //
            //         VectorBasis vectorBasis;
            //         Fp3** pVectorBasis = (Fp3**)&vectorBasis;
            //
            //         Fp4x4 matTemp = Fp4x4.Identity;
            //         CanonicalBasis canonicalBasis = new CanonicalBasis();
            //         Fp3* pCanonicalBasis = &canonicalBasis.Row0;
            //
            //         canonicalBasis.Row0 = new Fp3(Fp.One, 0, 0);
            //         canonicalBasis.Row1 = new Fp3(0, Fp.One, 0);
            //         canonicalBasis.Row2 = new Fp3(0, 0, Fp.One);
            //
            //         translation = new Fp3(
            //             matrix.M41,
            //             matrix.M42,
            //             matrix.M43);
            //
            //         pVectorBasis[0] = (Fp3*)&matTemp.M11;
            //         pVectorBasis[1] = (Fp3*)&matTemp.M21;
            //         pVectorBasis[2] = (Fp3*)&matTemp.M31;
            //
            //         *(pVectorBasis[0]) = new Fp3(matrix.M11, matrix.M12, matrix.M13);
            //         *(pVectorBasis[1]) = new Fp3(matrix.M21, matrix.M22, matrix.M23);
            //         *(pVectorBasis[2]) = new Fp3(matrix.M31, matrix.M32, matrix.M33);
            //
            //         scale.x = pVectorBasis[0]->Length();
            //         scale.y = pVectorBasis[1]->Length();
            //         scale.z = pVectorBasis[2]->Length();
            //
            //         uint a, b, c;
            //
            //         #region Ranking
            //
            //         Fp x = pfScales[0], y = pfScales[1], z = pfScales[2];
            //         if (x < y)
            //         {
            //             if (y < z)
            //             {
            //                 a = 2;
            //                 b = 1;
            //                 c = 0;
            //             }
            //             else
            //             {
            //                 a = 1;
            //
            //                 if (x < z)
            //                 {
            //                     b = 2;
            //                     c = 0;
            //                 }
            //                 else
            //                 {
            //                     b = 0;
            //                     c = 2;
            //                 }
            //             }
            //         }
            //         else
            //         {
            //             if (x < z)
            //             {
            //                 a = 2;
            //                 b = 0;
            //                 c = 1;
            //             }
            //             else
            //             {
            //                 a = 0;
            //
            //                 if (y < z)
            //                 {
            //                     b = 2;
            //                     c = 1;
            //                 }
            //                 else
            //                 {
            //                     b = 1;
            //                     c = 2;
            //                 }
            //             }
            //         }
            //
            //         #endregion
            //
            //         if (pfScales[a] < Fp.Epsilon_0x3)
            //         {
            //             *(pVectorBasis[a]) = pCanonicalBasis[a];
            //         }
            //
            //         *pVectorBasis[a] = Fp3.Normalize(*pVectorBasis[a]);
            //
            //         if (pfScales[b] < Fp.Epsilon_0x3)
            //         {
            //             uint cc;
            //             Fp fAbsX, fAbsY, fAbsZ;
            //
            //             fAbsX = (Fp)Math.Abs(pVectorBasis[a]->x);
            //             fAbsY = (Fp)Math.Abs(pVectorBasis[a]->y);
            //             fAbsZ = (Fp)Math.Abs(pVectorBasis[a]->z);
            //
            //             #region Ranking
            //
            //             if (fAbsX < fAbsY)
            //             {
            //                 if (fAbsY < fAbsZ)
            //                 {
            //                     cc = 0;
            //                 }
            //                 else
            //                 {
            //                     if (fAbsX < fAbsZ)
            //                     {
            //                         cc = 0;
            //                     }
            //                     else
            //                     {
            //                         cc = 2;
            //                     }
            //                 }
            //             }
            //             else
            //             {
            //                 if (fAbsX < fAbsZ)
            //                 {
            //                     cc = 1;
            //                 }
            //                 else
            //                 {
            //                     if (fAbsY < fAbsZ)
            //                     {
            //                         cc = 1;
            //                     }
            //                     else
            //                     {
            //                         cc = 2;
            //                     }
            //                 }
            //             }
            //
            //             #endregion
            //
            //             *pVectorBasis[b] = Fp3.Cross(*pVectorBasis[a], *(pCanonicalBasis + cc));
            //         }
            //
            //         *pVectorBasis[b] = Fp3.Normalize(*pVectorBasis[b]);
            //
            //         if (pfScales[c] < Fp.Epsilon_0x3)
            //         {
            //             *pVectorBasis[c] = Fp3.Cross(*pVectorBasis[a], *pVectorBasis[b]);
            //         }
            //
            //         *pVectorBasis[c] = Fp3.Normalize(*pVectorBasis[c]);
            //
            //         det = matTemp.GetDeterminant();
            //
            //         // use Kramer's rule to check for handedness of coordinate system
            //         if (det < 0)
            //         {
            //             // switch coordinate system by negating the scale and inverting the basis vector on the x-axis
            //             pfScales[a] = -pfScales[a];
            //             *pVectorBasis[a] = -(*pVectorBasis[a]);
            //
            //             det = -det;
            //         }
            //
            //         det -= Fp.One;
            //         det *= det;
            //
            //         if ((Fp.Epsilon_0x3 < det))
            //         {
            //             // Non-SRT matrix encountered
            //             rotation = QuaternionFp.Identity;
            //             result = false;
            //         }
            //         else
            //         {
            //             // generate the QuaternionFp from the matrix
            //             rotation = QuaternionFp.CreateFromRotationMatrix(matTemp);
            //         }
            //     }
            // }
            //
            // return result;
            throw new NotImplementedException();
        }

        /// <summary>
        /// Transforms the given matrix by applying the given Quaternion rotation.
        /// </summary>
        /// <param name="value">The source matrix to transform.</param>
        /// <param name="rotation">The rotation to apply.</param>
        /// <returns>The transformed matrix.</returns>
        public static Fp4x4 Transform(Fp4x4 value, QuaternionFp rotation)
        {
            // Compute rotation matrix.
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

            Fp q11 = Fp.One - yy2 - zz2;
            Fp q21 = xy2 - wz2;
            Fp q31 = xz2 + wy2;

            Fp q12 = xy2 + wz2;
            Fp q22 = Fp.One - xx2 - zz2;
            Fp q32 = yz2 - wx2;

            Fp q13 = xz2 - wy2;
            Fp q23 = yz2 + wx2;
            Fp q33 = Fp.One - xx2 - yy2;

            Fp4x4 result = new Fp4x4();

            // First row
            result.c0.x = value.c0.x * q11 + value.c1.x * q21 + value.c2.x * q31;
            result.c1.x = value.c0.x * q12 + value.c1.x * q22 + value.c2.x * q32;
            result.c2.x = value.c0.x * q13 + value.c1.x * q23 + value.c2.x * q33;
            result.c3.x = value.c3.x;

            // Second row
            result.c0.y = value.c0.y * q11 + value.c1.y * q21 + value.c2.y * q31;
            result.c1.y = value.c0.y * q12 + value.c1.y * q22 + value.c2.y * q32;
            result.c2.y = value.c0.y * q13 + value.c1.y * q23 + value.c2.y * q33;
            result.c3.y = value.c3.y;

            // Third row
            result.c0.z = value.c0.z * q11 + value.c1.z * q21 + value.c2.z * q31;
            result.c1.z = value.c0.z * q12 + value.c1.z * q22 + value.c2.z * q32;
            result.c2.z = value.c0.z * q13 + value.c1.z * q23 + value.c2.z * q33;
            result.c3.z = value.c3.z;

            // Fourth row
            result.c0.w = value.c0.w * q11 + value.c1.w * q21 + value.c2.w * q31;
            result.c1.w = value.c0.w * q12 + value.c1.w * q22 + value.c2.w * q32;
            result.c2.w = value.c0.w * q13 + value.c1.w * q23 + value.c2.w * q33;
            result.c3.w = value.c3.w;

            return result;
        }

        /// <summary>
        /// Transposes the rows and columns of a matrix.
        /// </summary>
        /// <param name="matrix">The source matrix.</param>
        /// <returns>The transposed matrix.</returns>
        public static Fp4x4 Transpose(Fp4x4 matrix)
        {
            Fp4x4 result = new Fp4x4();

            result.c0.x = matrix.c0.x;
            result.c1.x = matrix.c0.y;
            result.c2.x = matrix.c0.z;
            result.c3.x = matrix.c0.w;
            result.c0.y = matrix.c1.x;
            result.c1.y = matrix.c1.y;
            result.c2.y = matrix.c1.z;
            result.c3.y = matrix.c1.w;
            result.c0.z = matrix.c2.x;
            result.c1.z = matrix.c2.y;
            result.c2.z = matrix.c2.z;
            result.c3.z = matrix.c2.w;
            result.c0.w = matrix.c3.x;
            result.c1.w = matrix.c3.y;
            result.c2.w = matrix.c3.z;
            result.c3.w = matrix.c3.w;

            return result;
        }

        /// <summary>
        /// Linearly interpolates between the corresponding values of two matrices.
        /// </summary>
        /// <param name="matrix1">The first source matrix.</param>
        /// <param name="matrix2">The second source matrix.</param>
        /// <param name="amount">The relative weight of the second source matrix.</param>
        /// <returns>The interpolated matrix.</returns>
        public static Fp4x4 Lerp(Fp4x4 matrix1, Fp4x4 matrix2, Fp amount)
        {
            Fp4x4 result = new Fp4x4();

            // First row
            result.c0.x = matrix1.c0.x + (matrix2.c0.x - matrix1.c0.x) * amount;
            result.c1.x = matrix1.c1.x + (matrix2.c1.x - matrix1.c1.x) * amount;
            result.c2.x = matrix1.c2.x + (matrix2.c2.x - matrix1.c2.x) * amount;
            result.c3.x = matrix1.c3.x + (matrix2.c3.x - matrix1.c3.x) * amount;

            // Second row
            result.c0.y = matrix1.c0.y + (matrix2.c0.y - matrix1.c0.y) * amount;
            result.c1.y = matrix1.c1.y + (matrix2.c1.y - matrix1.c1.y) * amount;
            result.c2.y = matrix1.c2.y + (matrix2.c2.y - matrix1.c2.y) * amount;
            result.c3.y = matrix1.c3.y + (matrix2.c3.y - matrix1.c3.y) * amount;

            // Third row
            result.c0.z = matrix1.c0.z + (matrix2.c0.z - matrix1.c0.z) * amount;
            result.c1.z = matrix1.c1.z + (matrix2.c1.z - matrix1.c1.z) * amount;
            result.c2.z = matrix1.c2.z + (matrix2.c2.z - matrix1.c2.z) * amount;
            result.c3.z = matrix1.c3.z + (matrix2.c3.z - matrix1.c3.z) * amount;

            // Fourth row
            result.c0.w = matrix1.c0.w + (matrix2.c0.w - matrix1.c0.w) * amount;
            result.c1.w = matrix1.c1.w + (matrix2.c1.w - matrix1.c1.w) * amount;
            result.c2.w = matrix1.c2.w + (matrix2.c2.w - matrix1.c2.w) * amount;
            result.c3.w = matrix1.c3.w + (matrix2.c3.w - matrix1.c3.w) * amount;

            return result;
        }

        /// <summary>
        /// Returns a new matrix with the negated elements of the given matrix.
        /// </summary>
        /// <param name="value">The source matrix.</param>
        /// <returns>The negated matrix.</returns>
        public static Fp4x4 Negate(Fp4x4 value)
        {
            Fp4x4 result = new Fp4x4();

            result.c0.x = -value.c0.x;
            result.c1.x = -value.c1.x;
            result.c2.x = -value.c2.x;
            result.c3.x = -value.c3.x;
            result.c0.y = -value.c0.y;
            result.c1.y = -value.c1.y;
            result.c2.y = -value.c2.y;
            result.c3.y = -value.c3.y;
            result.c0.z = -value.c0.z;
            result.c1.z = -value.c1.z;
            result.c2.z = -value.c2.z;
            result.c3.z = -value.c3.z;
            result.c0.w = -value.c0.w;
            result.c1.w = -value.c1.w;
            result.c2.w = -value.c2.w;
            result.c3.w = -value.c3.w;

            return result;
        }

        /// <summary>
        /// Adds two matrices together.
        /// </summary>
        /// <param name="value1">The first source matrix.</param>
        /// <param name="value2">The second source matrix.</param>
        /// <returns>The resulting matrix.</returns>
        public static Fp4x4 Add(Fp4x4 value1, Fp4x4 value2)
        {
            Fp4x4 result = new Fp4x4();

            result.c0.x = value1.c0.x + value2.c0.x;
            result.c1.x = value1.c1.x + value2.c1.x;
            result.c2.x = value1.c2.x + value2.c2.x;
            result.c3.x = value1.c3.x + value2.c3.x;
            result.c0.y = value1.c0.y + value2.c0.y;
            result.c1.y = value1.c1.y + value2.c1.y;
            result.c2.y = value1.c2.y + value2.c2.y;
            result.c3.y = value1.c3.y + value2.c3.y;
            result.c0.z = value1.c0.z + value2.c0.z;
            result.c1.z = value1.c1.z + value2.c1.z;
            result.c2.z = value1.c2.z + value2.c2.z;
            result.c3.z = value1.c3.z + value2.c3.z;
            result.c0.w = value1.c0.w + value2.c0.w;
            result.c1.w = value1.c1.w + value2.c1.w;
            result.c2.w = value1.c2.w + value2.c2.w;
            result.c3.w = value1.c3.w + value2.c3.w;

            return result;
        }

        /// <summary>
        /// Subtracts the second matrix from the first.
        /// </summary>
        /// <param name="value1">The first source matrix.</param>
        /// <param name="value2">The second source matrix.</param>
        /// <returns>The result of the subtraction.</returns>
        public static Fp4x4 Subtract(Fp4x4 value1, Fp4x4 value2)
        {
            Fp4x4 result = new Fp4x4();

            result.c0.x = value1.c0.x - value2.c0.x;
            result.c1.x = value1.c1.x - value2.c1.x;
            result.c2.x = value1.c2.x - value2.c2.x;
            result.c3.x = value1.c3.x - value2.c3.x;
            result.c0.y = value1.c0.y - value2.c0.y;
            result.c1.y = value1.c1.y - value2.c1.y;
            result.c2.y = value1.c2.y - value2.c2.y;
            result.c3.y = value1.c3.y - value2.c3.y;
            result.c0.z = value1.c0.z - value2.c0.z;
            result.c1.z = value1.c1.z - value2.c1.z;
            result.c2.z = value1.c2.z - value2.c2.z;
            result.c3.z = value1.c3.z - value2.c3.z;
            result.c0.w = value1.c0.w - value2.c0.w;
            result.c1.w = value1.c1.w - value2.c1.w;
            result.c2.w = value1.c2.w - value2.c2.w;
            result.c3.w = value1.c3.w - value2.c3.w;

            return result;
        }

        /// <summary>
        /// Multiplies a matrix by another matrix.
        /// </summary>
        /// <param name="value1">The first source matrix.</param>
        /// <param name="value2">The second source matrix.</param>
        /// <returns>The result of the multiplication.</returns>
        public static Fp4x4 Multiply(Fp4x4 value1, Fp4x4 value2)
        {
            Fp4x4 result = new Fp4x4();

            // First row
            result.c0.x = value1.c0.x * value2.c0.x + value1.c1.x * value2.c0.y + value1.c2.x * value2.c0.z +
                          value1.c3.x * value2.c0.w;
            result.c1.x = value1.c0.x * value2.c1.x + value1.c1.x * value2.c1.y + value1.c2.x * value2.c1.z +
                          value1.c3.x * value2.c1.w;
            result.c2.x = value1.c0.x * value2.c2.x + value1.c1.x * value2.c2.y + value1.c2.x * value2.c2.z +
                          value1.c3.x * value2.c2.w;
            result.c3.x = value1.c0.x * value2.c3.x + value1.c1.x * value2.c3.y + value1.c2.x * value2.c3.z +
                          value1.c3.x * value2.c3.w;

            // Second row
            result.c0.y = value1.c0.y * value2.c0.x + value1.c1.y * value2.c0.y + value1.c2.y * value2.c0.z +
                          value1.c3.y * value2.c0.w;
            result.c1.y = value1.c0.y * value2.c1.x + value1.c1.y * value2.c1.y + value1.c2.y * value2.c1.z +
                          value1.c3.y * value2.c1.w;
            result.c2.y = value1.c0.y * value2.c2.x + value1.c1.y * value2.c2.y + value1.c2.y * value2.c2.z +
                          value1.c3.y * value2.c2.w;
            result.c3.y = value1.c0.y * value2.c3.x + value1.c1.y * value2.c3.y + value1.c2.y * value2.c3.z +
                          value1.c3.y * value2.c3.w;

            // Third row
            result.c0.z = value1.c0.z * value2.c0.x + value1.c1.z * value2.c0.y + value1.c2.z * value2.c0.z +
                          value1.c3.z * value2.c0.w;
            result.c1.z = value1.c0.z * value2.c1.x + value1.c1.z * value2.c1.y + value1.c2.z * value2.c1.z +
                          value1.c3.z * value2.c1.w;
            result.c2.z = value1.c0.z * value2.c2.x + value1.c1.z * value2.c2.y + value1.c2.z * value2.c2.z +
                          value1.c3.z * value2.c2.w;
            result.c3.z = value1.c0.z * value2.c3.x + value1.c1.z * value2.c3.y + value1.c2.z * value2.c3.z +
                          value1.c3.z * value2.c3.w;

            // Fourth row
            result.c0.w = value1.c0.w * value2.c0.x + value1.c1.w * value2.c0.y + value1.c2.w * value2.c0.z +
                          value1.c3.w * value2.c0.w;
            result.c1.w = value1.c0.w * value2.c1.x + value1.c1.w * value2.c1.y + value1.c2.w * value2.c1.z +
                          value1.c3.w * value2.c1.w;
            result.c2.w = value1.c0.w * value2.c2.x + value1.c1.w * value2.c2.y + value1.c2.w * value2.c2.z +
                          value1.c3.w * value2.c2.w;
            result.c3.w = value1.c0.w * value2.c3.x + value1.c1.w * value2.c3.y + value1.c2.w * value2.c3.z +
                          value1.c3.w * value2.c3.w;

            return result;
        }

        /// <summary>
        /// Multiplies a matrix by a scalar value.
        /// </summary>
        /// <param name="value1">The source matrix.</param>
        /// <param name="value2">The scaling factor.</param>
        /// <returns>The scaled matrix.</returns>
        public static Fp4x4 Multiply(Fp4x4 value1, Fp value2)
        {
            Fp4x4 result = new Fp4x4();

            result.c0.x = value1.c0.x * value2;
            result.c1.x = value1.c1.x * value2;
            result.c2.x = value1.c2.x * value2;
            result.c3.x = value1.c3.x * value2;
            result.c0.y = value1.c0.y * value2;
            result.c1.y = value1.c1.y * value2;
            result.c2.y = value1.c2.y * value2;
            result.c3.y = value1.c3.y * value2;
            result.c0.z = value1.c0.z * value2;
            result.c1.z = value1.c1.z * value2;
            result.c2.z = value1.c2.z * value2;
            result.c3.z = value1.c3.z * value2;
            result.c0.w = value1.c0.w * value2;
            result.c1.w = value1.c1.w * value2;
            result.c2.w = value1.c2.w * value2;
            result.c3.w = value1.c3.w * value2;

            return result;
        }

        #endregion

        #region From BepuUtilities

        /// <summary>
        /// Transforms a vector with a transposed matrix.
        /// </summary>
        /// <param name="v">Row vector to transform.</param>
        /// <param name="m">Matrix whose transpose will be applied to the vector.</param>
        /// <param name="result">Transformed vector.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void TransformTranspose(in Fp4 v, in Fp4x4 m, out Fp4 result)
        {
            var transpose = MathFp.transpose(m);
            result = new Fp4(
                Fp4.Dot(v, transpose.c0),
                Fp4.Dot(v, transpose.c1),
                Fp4.Dot(v, transpose.c2),
                Fp4.Dot(v, transpose.c3));
        }

        /// <summary>
        /// Transforms a vector with a matrix.
        /// </summary>
        /// <param name="v">Row vector to transform.</param>
        /// <param name="m">Matrix to apply to the vector.</param>
        /// <param name="result">Transformed vector.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Transform(in Fp4 v, in Fp4x4 m, out Fp4 result)
        {
            var transpose = MathFp.transpose(m);
            var x = new Fp4(v.x);
            var y = new Fp4(v.y);
            var z = new Fp4(v.z);
            var w = new Fp4(v.w);
            result = transpose.c0 * x + transpose.c1 * y + transpose.c2 * z + transpose.c3 * w;
        }

        /// <summary>
        /// Transforms a vector with a matrix. Implicitly uses 1 as the fourth component of the input vector.
        /// </summary>
        /// <param name="v">Row vector to transform.</param>
        /// <param name="m">Matrix to apply to the vector.</param>
        /// <param name="result">Transformed vector.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Transform(in Fp3 v, in Fp4x4 m, out Fp4 result)
        {
            var transpose = MathFp.transpose(m);
            var x = new Fp4(v.x);
            var y = new Fp4(v.y);
            var z = new Fp4(v.z);
            result = transpose.c0 * x + transpose.c1 * y + transpose.c2 * z + transpose.c3;
        }

        #endregion
    }
}