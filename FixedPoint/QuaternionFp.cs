using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;
using static Unity.Mathematics.FixedPoint.MathFp;

namespace Unity.Mathematics.FixedPoint
{
    /// <summary>
    /// A QuaternionFp type for representing rotations.
    /// </summary>
    [Il2CppEagerStaticClassConstruction]
    [Serializable]
    public struct QuaternionFp : IEquatable<QuaternionFp>, IFormattable
    {
        #region Unity.Mathematics

        /// <summary>
        /// The QuaternionFp component values.
        /// </summary>
        public Fp4 value;

        /// <summary>A QuaternionFp representing the identity transform.</summary>
        public static readonly QuaternionFp Identity = new QuaternionFp(0, 0, 0, 1);

        /// <summary>Constructs a QuaternionFp from four Fp values.</summary>
        /// <param name="x">The QuaternionFp x component.</param>
        /// <param name="y">The QuaternionFp y component.</param>
        /// <param name="z">The QuaternionFp z component.</param>
        /// <param name="w">The QuaternionFp w component.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public QuaternionFp(Fp x, Fp y, Fp z, Fp w)
        {
            value.x = x;
            value.y = y;
            value.z = z;
            value.w = w;
        }

        /// <summary>Constructs a QuaternionFp from Fp4 vector.</summary>
        /// <param name="value">The QuaternionFp xyzw component values.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public QuaternionFp(Fp4 value)
        {
            this.value = value;
        }

        /// <summary>Implicitly converts a Fp4 vector to a QuaternionFp.</summary>
        /// <param name="v">The QuaternionFp xyzw component values.</param>
        /// <returns>The QuaternionFp constructed from a Fp4 vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator QuaternionFp(Fp4 v)
        {
            return new QuaternionFp(v);
        }

        /// <summary>Constructs a unit QuaternionFp from a Fp3x3 rotation matrix. The matrix must be orthonormal.</summary>
        /// <param name="m">The Fp3x3 orthonormal rotation matrix.</param>
        public QuaternionFp(Fp3x3 m)
        {
            // Fp3 u = m.c0;
            // Fp3 v = m.c1;
            // Fp3 w = m.c2;
            //
            // ulong u_sign = (ulong)u.x.value & 0x8000000000000000;
            // Fp t = v.y + (long)((ulong)w.z.value ^ u_sign);
            // ulong4 u_mask = ulong4((ulong)((long)u_sign >> 63));
            // ulong4 t_mask = ulong4((ulong)((t.value) >> 63));
            //
            // Fp tr = 1 + abs(u.x);
            //
            // ulong4 sign_flips = ulong4(0x00000000, 0x8000000000000000, 0x8000000000000000, 0x8000000000000000) ^
            //                     (u_mask & ulong4(0x00000000, 0x8000000000000000, 0x00000000, 0x8000000000000000)) ^
            //                     (t_mask & ulong4(0x8000000000000000, 0x8000000000000000, 0x8000000000000000, 0x00000000));
            //
            // var with_sign = (asulong(Fp4(t, v.x, u.z, w.y)) ^ sign_flips);
            // value = Fp4(tr, u.y, w.x, v.z) +
            //         Fp4(with_sign); // +---, +++-, ++-+, +-++
            //
            // value = Fp4((asulong(value) & ~u_mask) | (asulong(value.zwxy) & u_mask));
            // value = Fp4((asulong(value.wzyx) & ~t_mask) | (asulong(value) & t_mask));
            // value = normalize(value);
            CreateFromRotationMatrix(new Fp4x4(m, (Fp)0), out var q);
            value = q.value;
        }

        /// <summary>Constructs a unit QuaternionFp from an orthonormal Fp4x4 matrix.</summary>
        /// <param name="m">The Fp4x4 orthonormal rotation matrix.</param>
        public QuaternionFp(Fp4x4 m)
        {
            CreateFromRotationMatrix(m, out var q);
            value = q.value;
        }

        /// <summary>
        /// Returns a QuaternionFp representing a rotation around a unit axis by an angle in radians.
        /// The rotation direction is clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="axis">The axis of rotation.</param>
        /// <param name="angle">The angle of rotation in radians.</param>
        /// <returns>The QuaternionFp representing a rotation around an axis.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static QuaternionFp AxisAngle(Fp3 axis, Fp angle)
        {
            Fp sina, cosa;
            MathFp.sincos(Fp.Half * angle, out sina, out cosa);
            return MathFp.QuaternionFp(MathFp.Fp4(axis * sina, cosa));
        }

        /// <summary>
        /// Returns a QuaternionFp constructed by first performing a rotation around the x-axis, then the y-axis and finally the z-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="xyz">A Fp3 vector containing the rotation angles around the x-, y- and z-axis measures in radians.</param>
        /// <returns>The QuaternionFp representing the Euler angle rotation in x-y-z order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static QuaternionFp EulerXYZ(Fp3 xyz)
        {
            // return mul(rotateZ(xyz.z), mul(rotateY(xyz.y), rotateX(xyz.x)));
            Fp3 s, c;
            MathFp.sincos(Fp.Half * xyz, out s, out c);
            return MathFp.QuaternionFp(
                // s.x * c.y * c.z - s.y * s.z * c.x,
                // s.y * c.x * c.z + s.x * s.z * c.y,
                // s.z * c.x * c.y - s.x * s.y * c.z,
                // c.x * c.y * c.z + s.y * s.z * s.x
                MathFp.Fp4(s.xyz, c.x) * c.yxxy * c.zzyz +
                s.yxxy * s.zzyz * MathFp.Fp4(c.xyz, s.x) * MathFp.Fp4(-1, 1, -1, 1)
            );
        }

        /// <summary>
        /// Returns a QuaternionFp constructed by first performing a rotation around the x-axis, then the z-axis and finally the y-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="xyz">A Fp3 vector containing the rotation angles around the x-, y- and z-axis measures in radians.</param>
        /// <returns>The QuaternionFp representing the Euler angle rotation in x-z-y order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static QuaternionFp EulerXZY(Fp3 xyz)
        {
            // return mul(rotateY(xyz.y), mul(rotateZ(xyz.z), rotateX(xyz.x)));
            Fp3 s, c;
            MathFp.sincos(Fp.Half * xyz, out s, out c);
            return MathFp.QuaternionFp(
                // s.x * c.y * c.z + s.y * s.z * c.x,
                // s.y * c.x * c.z + s.x * s.z * c.y,
                // s.z * c.x * c.y - s.x * s.y * c.z,
                // c.x * c.y * c.z - s.y * s.z * s.x
                MathFp.Fp4(s.xyz, c.x) * c.yxxy * c.zzyz +
                s.yxxy * s.zzyz * MathFp.Fp4(c.xyz, s.x) * MathFp.Fp4(1, 1, -1, -1)
            );
        }

        /// <summary>
        /// Returns a QuaternionFp constructed by first performing a rotation around the y-axis, then the x-axis and finally the z-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="xyz">A Fp3 vector containing the rotation angles around the x-, y- and z-axis measures in radians.</param>
        /// <returns>The QuaternionFp representing the Euler angle rotation in y-x-z order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static QuaternionFp EulerYXZ(Fp3 xyz)
        {
            // return mul(rotateZ(xyz.z), mul(rotateX(xyz.x), rotateY(xyz.y)));
            Fp3 s, c;
            MathFp.sincos(Fp.Half * xyz, out s, out c);
            return MathFp.QuaternionFp(
                // s.x * c.y * c.z - s.y * s.z * c.x,
                // s.y * c.x * c.z + s.x * s.z * c.y,
                // s.z * c.x * c.y + s.x * s.y * c.z,
                // c.x * c.y * c.z - s.y * s.z * s.x
                MathFp.Fp4(s.xyz, c.x) * c.yxxy * c.zzyz +
                s.yxxy * s.zzyz * MathFp.Fp4(c.xyz, s.x) * MathFp.Fp4(-1, 1, 1, -1)
            );
        }

        /// <summary>
        /// Returns a QuaternionFp constructed by first performing a rotation around the y-axis, then the z-axis and finally the x-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="xyz">A Fp3 vector containing the rotation angles around the x-, y- and z-axis measures in radians.</param>
        /// <returns>The QuaternionFp representing the Euler angle rotation in y-z-x order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static QuaternionFp EulerYZX(Fp3 xyz)
        {
            // return mul(rotateX(xyz.x), mul(rotateZ(xyz.z), rotateY(xyz.y)));
            Fp3 s, c;
            MathFp.sincos(Fp.Half * xyz, out s, out c);
            return MathFp.QuaternionFp(
                // s.x * c.y * c.z - s.y * s.z * c.x,
                // s.y * c.x * c.z - s.x * s.z * c.y,
                // s.z * c.x * c.y + s.x * s.y * c.z,
                // c.x * c.y * c.z + s.y * s.z * s.x
                MathFp.Fp4(s.xyz, c.x) * c.yxxy * c.zzyz +
                s.yxxy * s.zzyz * MathFp.Fp4(c.xyz, s.x) * MathFp.Fp4(-1, -1, 1, 1)
            );
        }

        /// <summary>
        /// Returns a QuaternionFp constructed by first performing a rotation around the z-axis, then the x-axis and finally the y-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// This is the default order rotation order in Unity.
        /// </summary>
        /// <param name="xyz">A Fp3 vector containing the rotation angles around the x-, y- and z-axis measures in radians.</param>
        /// <returns>The QuaternionFp representing the Euler angle rotation in z-x-y order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static QuaternionFp EulerZXY(Fp3 xyz)
        {
            // return mul(rotateY(xyz.y), mul(rotateX(xyz.x), rotateZ(xyz.z)));
            Fp3 s, c;
            MathFp.sincos(Fp.Half * xyz, out s, out c);
            return MathFp.QuaternionFp(
                // s.x * c.y * c.z + s.y * s.z * c.x,
                // s.y * c.x * c.z - s.x * s.z * c.y,
                // s.z * c.x * c.y - s.x * s.y * c.z,
                // c.x * c.y * c.z + s.y * s.z * s.x
                MathFp.Fp4(s.xyz, c.x) * c.yxxy * c.zzyz +
                s.yxxy * s.zzyz * MathFp.Fp4(c.xyz, s.x) * MathFp.Fp4(1, -1, -1, 1)
            );
        }

        /// <summary>
        /// Returns a QuaternionFp constructed by first performing a rotation around the z-axis, then the y-axis and finally the x-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="xyz">A Fp3 vector containing the rotation angles around the x-, y- and z-axis measures in radians.</param>
        /// <returns>The QuaternionFp representing the Euler angle rotation in z-y-x order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static QuaternionFp EulerZYX(Fp3 xyz)
        {
            // return mul(rotateX(xyz.x), mul(rotateY(xyz.y), rotateZ(xyz.z)));
            Fp3 s, c;
            MathFp.sincos(Fp.Half * xyz, out s, out c);
            return MathFp.QuaternionFp(
                // s.x * c.y * c.z + s.y * s.z * c.x,
                // s.y * c.x * c.z - s.x * s.z * c.y,
                // s.z * c.x * c.y + s.x * s.y * c.z,
                // c.x * c.y * c.z - s.y * s.x * s.z
                MathFp.Fp4(s.xyz, c.x) * c.yxxy * c.zzyz +
                s.yxxy * s.zzyz * MathFp.Fp4(c.xyz, s.x) * MathFp.Fp4(1, -1, 1, -1)
            );
        }

        /// <summary>
        /// Returns a QuaternionFp constructed by first performing a rotation around the x-axis, then the y-axis and finally the z-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="x">The rotation angle around the x-axis in radians.</param>
        /// <param name="y">The rotation angle around the y-axis in radians.</param>
        /// <param name="z">The rotation angle around the z-axis in radians.</param>
        /// <returns>The QuaternionFp representing the Euler angle rotation in x-y-z order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static QuaternionFp EulerXYZ(Fp x, Fp y, Fp z)
        {
            return EulerXYZ(MathFp.Fp3(x, y, z));
        }

        /// <summary>
        /// Returns a QuaternionFp constructed by first performing a rotation around the x-axis, then the z-axis and finally the y-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="x">The rotation angle around the x-axis in radians.</param>
        /// <param name="y">The rotation angle around the y-axis in radians.</param>
        /// <param name="z">The rotation angle around the z-axis in radians.</param>
        /// <returns>The QuaternionFp representing the Euler angle rotation in x-z-y order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static QuaternionFp EulerXZY(Fp x, Fp y, Fp z)
        {
            return EulerXZY(MathFp.Fp3(x, y, z));
        }

        /// <summary>
        /// Returns a QuaternionFp constructed by first performing a rotation around the y-axis, then the x-axis and finally the z-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="x">The rotation angle around the x-axis in radians.</param>
        /// <param name="y">The rotation angle around the y-axis in radians.</param>
        /// <param name="z">The rotation angle around the z-axis in radians.</param>
        /// <returns>The QuaternionFp representing the Euler angle rotation in y-x-z order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static QuaternionFp EulerYXZ(Fp x, Fp y, Fp z)
        {
            return EulerYXZ(MathFp.Fp3(x, y, z));
        }

        /// <summary>
        /// Returns a QuaternionFp constructed by first performing a rotation around the y-axis, then the z-axis and finally the x-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="x">The rotation angle around the x-axis in radians.</param>
        /// <param name="y">The rotation angle around the y-axis in radians.</param>
        /// <param name="z">The rotation angle around the z-axis in radians.</param>
        /// <returns>The QuaternionFp representing the Euler angle rotation in y-z-x order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static QuaternionFp EulerYZX(Fp x, Fp y, Fp z)
        {
            return EulerYZX(MathFp.Fp3(x, y, z));
        }

        /// <summary>
        /// Returns a QuaternionFp constructed by first performing a rotation around the z-axis, then the x-axis and finally the y-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// This is the default order rotation order in Unity.
        /// </summary>
        /// <param name="x">The rotation angle around the x-axis in radians.</param>
        /// <param name="y">The rotation angle around the y-axis in radians.</param>
        /// <param name="z">The rotation angle around the z-axis in radians.</param>
        /// <returns>The QuaternionFp representing the Euler angle rotation in z-x-y order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static QuaternionFp EulerZXY(Fp x, Fp y, Fp z)
        {
            return EulerZXY(MathFp.Fp3(x, y, z));
        }

        /// <summary>
        /// Returns a QuaternionFp constructed by first performing a rotation around the z-axis, then the y-axis and finally the x-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="x">The rotation angle around the x-axis in radians.</param>
        /// <param name="y">The rotation angle around the y-axis in radians.</param>
        /// <param name="z">The rotation angle around the z-axis in radians.</param>
        /// <returns>The QuaternionFp representing the Euler angle rotation in z-y-x order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static QuaternionFp EulerZYX(Fp x, Fp y, Fp z)
        {
            return EulerZYX(MathFp.Fp3(x, y, z));
        }

        /// <summary>
        /// Returns a QuaternionFp constructed by first performing 3 rotations around the principal axes in a given order.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// When the rotation order is known at compile time, it is recommended for performance reasons to use specific
        /// Euler rotation constructors such as EulerZXY(...).
        /// </summary>
        /// <param name="xyz">A Fp3 vector containing the rotation angles around the x-, y- and z-axis measures in radians.</param>
        /// <param name="order">The order in which the rotations are applied.</param>
        /// <returns>The QuaternionFp representing the Euler angle rotation in the specified order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static QuaternionFp Euler(Fp3 xyz, RotationOrder order = RotationOrder.ZXY)
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
                    return QuaternionFp.Identity;
            }
        }

        /// <summary>
        /// Returns a QuaternionFp constructed by first performing 3 rotations around the principal axes in a given order.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// When the rotation order is known at compile time, it is recommended for performance reasons to use specific
        /// Euler rotation constructors such as EulerZXY(...).
        /// </summary>
        /// <param name="x">The rotation angle around the x-axis in radians.</param>
        /// <param name="y">The rotation angle around the y-axis in radians.</param>
        /// <param name="z">The rotation angle around the z-axis in radians.</param>
        /// <param name="order">The order in which the rotations are applied.</param>
        /// <returns>The QuaternionFp representing the Euler angle rotation in the specified order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static QuaternionFp Euler(Fp x, Fp y, Fp z, RotationOrder order = RotationOrder.Default)
        {
            return Euler(new Fp3(x, y, z), order);
        }

        /// <summary>Returns a QuaternionFp that rotates around the x-axis by a given number of radians.</summary>
        /// <param name="angle">The clockwise rotation angle when looking along the x-axis towards the origin in radians.</param>
        /// <returns>The QuaternionFp representing a rotation around the x-axis.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static QuaternionFp RotateX(Fp angle)
        {
            Fp sina, cosa;
            MathFp.sincos(Fp.Half * angle, out sina, out cosa);
            return MathFp.QuaternionFp(sina, 0, 0, cosa);
        }

        /// <summary>Returns a QuaternionFp that rotates around the y-axis by a given number of radians.</summary>
        /// <param name="angle">The clockwise rotation angle when looking along the y-axis towards the origin in radians.</param>
        /// <returns>The QuaternionFp representing a rotation around the y-axis.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static QuaternionFp RotateY(Fp angle)
        {
            Fp sina, cosa;
            MathFp.sincos(Fp.Half * angle, out sina, out cosa);
            return MathFp.QuaternionFp(0, sina, 0, cosa);
        }

        /// <summary>Returns a QuaternionFp that rotates around the z-axis by a given number of radians.</summary>
        /// <param name="angle">The clockwise rotation angle when looking along the z-axis towards the origin in radians.</param>
        /// <returns>The QuaternionFp representing a rotation around the z-axis.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static QuaternionFp RotateZ(Fp angle)
        {
            Fp sina, cosa;
            MathFp.sincos(Fp.Half * angle, out sina, out cosa);
            return MathFp.QuaternionFp(0, 0, sina, cosa);
        }

        /// <summary>
        /// Returns a QuaternionFp view rotation given a unit length forward vector and a unit length up vector.
        /// The two input vectors are assumed to be unit length and not collinear.
        /// If these assumptions are not met use Fp3x3.LookRotationSafe instead.
        /// </summary>
        /// <param name="forward">The view forward direction.</param>
        /// <param name="up">The view up direction.</param>
        /// <returns>The QuaternionFp view rotation.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static QuaternionFp LookRotation(Fp3 forward, Fp3 up)
        {
            Fp3 t = MathFp.normalize(MathFp.cross(up, forward));
            return MathFp.QuaternionFp(MathFp.Fp3x3(t, MathFp.cross(forward, t), forward));
        }

        /// <summary>
        /// Returns a QuaternionFp view rotation given a forward vector and an up vector.
        /// The two input vectors are not assumed to be unit length.
        /// If the magnitude of either of the vectors is so extreme that the calculation cannot be carried out reliably or the vectors are collinear,
        /// the identity will be returned instead.
        /// </summary>
        /// <param name="forward">The view forward direction.</param>
        /// <param name="up">The view up direction.</param>
        /// <returns>The QuaternionFp view rotation or the identity QuaternionFp.</returns>
        public static QuaternionFp LookRotationSafe(Fp3 forward, Fp3 up)
        {
            Fp forwardLengthSq = MathFp.dot(forward, forward);
            if (forwardLengthSq == 0)
            {
                return QuaternionFp(MathFp.Fp4(0, 0, 0, 1));
            }

            Fp upLengthSq = MathFp.dot(up, up);
            if (upLengthSq == 0)
            {
                return QuaternionFp(MathFp.Fp4(0, 0, 0, 1));
            }

            forward *= MathFp.rsqrt(forwardLengthSq);
            up *= MathFp.rsqrt(upLengthSq);

            Fp3 t = MathFp.cross(up, forward);
            Fp tLengthSq = MathFp.dot(t, t);
            if (tLengthSq == 0)
            {
                return QuaternionFp(MathFp.Fp4(0, 0, 0, 1));
            }

            t *= MathFp.rsqrt(tLengthSq);

            Fp mn = MathFp.min(MathFp.min(forwardLengthSq, upLengthSq), tLengthSq);
            Fp mx = MathFp.max(MathFp.max(forwardLengthSq, upLengthSq), tLengthSq);

            bool accept = mn > Fp.OneEMinus8 && mx < int.MaxValue && MathFp.isfinite(forwardLengthSq) &&
                          MathFp.isfinite(upLengthSq) &&
                          MathFp.isfinite(tLengthSq);
            return MathFp.QuaternionFp(MathFp.select(MathFp.Fp4(0, 0, 0, 1),
                MathFp.QuaternionFp(MathFp.Fp3x3(t, MathFp.cross(forward, t), forward)).value, accept));
        }

        /// <summary>Returns true if the QuaternionFp is equal to a given QuaternionFp, false otherwise.</summary>
        /// <param name="x">The QuaternionFp to compare with.</param>
        /// <returns>True if the QuaternionFp is equal to the input, false otherwise.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(QuaternionFp x)
        {
            return value.x == x.value.x && value.y == x.value.y && value.z == x.value.z && value.w == x.value.w;
        }

        /// <summary>Returns whether true if the QuaternionFp is equal to a given QuaternionFp, false otherwise.</summary>
        /// <param name="x">The object to compare with.</param>
        /// <returns>True if the QuaternionFp is equal to the input, false otherwise.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object x)
        {
            return x is QuaternionFp converted && Equals(converted);
        }

        /// <summary>Returns a hash code for the QuaternionFp.</summary>
        /// <returns>The hash code of the QuaternionFp.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
        {
            return (int)MathFp.hash(this);
        }

        /// <summary>Returns a string representation of the QuaternionFp.</summary>
        /// <returns>The string representation of the QuaternionFp.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return string.Format("QuaternionFp({0}f, {1}f, {2}f, {3}f)", value.x, value.y, value.z, value.w);
        }

        /// <summary>Returns a string representation of the QuaternionFp using a specified format and culture-specific format information.</summary>
        /// <param name="format">The format string.</param>
        /// <param name="formatProvider">The format provider to use during string formatting.</param>
        /// <returns>The formatted string representation of the QuaternionFp.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(string format, IFormatProvider formatProvider)
        {
            return string.Format("QuaternionFp({0}f, {1}f, {2}f, {3}f)", value.x.ToString(format, formatProvider),
                value.y.ToString(format, formatProvider), value.z.ToString(format, formatProvider),
                value.w.ToString(format, formatProvider));
        }

        #endregion

        #region System.Numerics.QuaternionFp

        /// <summary>
        /// Constructs a QuaternionFp from the given vector and rotation parts.
        /// </summary>
        /// <param name="vectorPart">The vector part of the QuaternionFp.</param>
        /// <param name="scalarPart">The rotation part of the QuaternionFp.</param>
        public QuaternionFp(Fp3 vectorPart, Fp scalarPart)
        {
            value.x = vectorPart.x;
            value.y = vectorPart.y;
            value.z = vectorPart.z;
            value.w = scalarPart;
        }

        /// <summary>
        /// Returns whether the QuaternionFp is the identity QuaternionFp.
        /// </summary>
        public bool IsIdentity
        {
            get { return value.x == 0 && value.y == 0 && value.z == 0 && value.w == Fp.One; }
        }

        /// <summary>
        /// Adds two Quaternions element-by-element.
        /// </summary>
        /// <param name="value1">The first source QuaternionFp.</param>
        /// <param name="value2">The second source QuaternionFp.</param>
        /// <returns>The result of adding the Quaternions.</returns>
        public static QuaternionFp Add(QuaternionFp value1, QuaternionFp value2)
        {
            QuaternionFp ans;

            ans.value.x = value1.value.x + value2.value.x;
            ans.value.y = value1.value.y + value2.value.y;
            ans.value.z = value1.value.z + value2.value.z;
            ans.value.w = value1.value.w + value2.value.w;

            return ans;
        }

        /// <summary>
        /// Creates the conjugate of a specified QuaternionFp.
        /// </summary>
        /// <param name="value">The QuaternionFp of which to return the conjugate.</param>
        /// <returns>A new QuaternionFp that is the conjugate of the specified one.</returns>
        public static QuaternionFp Conjugate(QuaternionFp value)
        {
            QuaternionFp ans;

            ans.value.x = -value.value.x;
            ans.value.y = -value.value.y;
            ans.value.z = -value.value.z;
            ans.value.w = value.value.w;

            return ans;
        }

        /// <summary>
        /// Creates a QuaternionFp from a vector and an angle to rotate about the vector.
        /// </summary>
        /// <param name="axis">The vector to rotate around.</param>
        /// <param name="angle">The angle, in radians, to rotate around the vector.</param>
        /// <returns>The created QuaternionFp.</returns>
        public static QuaternionFp CreateFromAxisAngle(Fp3 axis, Fp angle)
        {
            QuaternionFp ans;

            Fp halfAngle = angle * Fp.Half;
            Fp s = MathFp.sin(halfAngle);
            Fp c = MathFp.cos(halfAngle);

            ans.value.x = axis.x * s;
            ans.value.y = axis.y * s;
            ans.value.z = axis.z * s;
            ans.value.w = c;

            return ans;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateFromRotationMatrix(in Fp4x4 matrix, out QuaternionFp q)
        {
            Fp trace = matrix.c0.x + matrix.c1.y + matrix.c2.z;

            q = new QuaternionFp();

            if (trace > 0)
            {
                Fp s = MathFp.sqrt(trace + 1);
                q.value.w = s * Fp.Half;
                s = Fp.Half / s;
                q.value.x = (matrix.c2.y - matrix.c1.z) * s;
                q.value.y = (matrix.c0.z - matrix.c2.x) * s;
                q.value.z = (matrix.c1.x - matrix.c0.y) * s;
            }
            else
            {
                if (matrix.c0.x >= matrix.c1.y && matrix.c0.x >= matrix.c2.z)
                {
                    Fp s = MathFp.sqrt(1 + matrix.c0.x - matrix.c1.y - matrix.c2.z);
                    Fp invS = Fp.Half / s;
                    q.value.x = Fp.Half * s;
                    q.value.y = (matrix.c1.x + matrix.c0.y) * invS;
                    q.value.z = (matrix.c2.x + matrix.c0.z) * invS;
                    q.value.w = (matrix.c2.y - matrix.c1.z) * invS;
                }
                else if (matrix.c1.y > matrix.c2.z)
                {
                    Fp s = MathFp.sqrt(1 + matrix.c1.y - matrix.c0.x - matrix.c2.z);
                    Fp invS = Fp.Half / s;
                    q.value.x = (matrix.c0.y + matrix.c1.x) * invS;
                    q.value.y = Fp.Half * s;
                    q.value.z = (matrix.c1.z + matrix.c2.y) * invS;
                    q.value.w = (matrix.c0.z - matrix.c2.x) * invS;
                }
                else
                {
                    Fp s = MathFp.sqrt(1 + matrix.c2.z - matrix.c0.x - matrix.c1.y);
                    Fp invS = Fp.Half / s;
                    q.value.x = (matrix.c0.z + matrix.c2.x) * invS;
                    q.value.y = (matrix.c1.z + matrix.c2.y) * invS;
                    q.value.z = Fp.Half * s;
                    q.value.w = (matrix.c1.x - matrix.c0.y) * invS;
                }
            }

            HandleQuaternionSign(ref q);
        }

        /// <summary>
        /// 对齐 四元数 符号，
        /// 这里是靠测试用例试出来的，不知道原理是什么。!>.<!
        /// </summary>
        /// <param name="q"></param>
        internal static void HandleQuaternionSign(ref QuaternionFp q)
        {
            if (q.value.w < 0)
            {
                q.value.w = -q.value.w;
            }
            else
            {
                if (q.value.x < 0)
                {
                    if (q.value.y > 0)
                    {
                        if (q.value.z > 0)
                        {
                            q.value.x = -q.value.x;
                            q.value.y = -q.value.y;
                            q.value.z = -q.value.z;
                        }
                        else
                        {
                            q.value.w = -q.value.w;
                        }
                    }
                    else if (q.value.y < 0)
                    {
                        q.value = -q.value;
                        q.value.w = MathFp.abs(q.value.w);
                    }
                }
                else
                {
                    if (q.value.y < 0)
                    {
                        if (q.value.z < 0)
                        {
                            q.value.x = -q.value.x;
                            q.value.y = -q.value.y;
                            q.value.z = -q.value.z;
                        }
                        else
                        {
                            q.value.w = -q.value.w;
                        }
                    }
                    else
                    {
                        q.value.w = -q.value.w;
                    }
                }
            }
        }

        /// <summary>
        /// Creates a new QuaternionFp from the given yaw, pitch, and roll, in radians.
        /// </summary>
        /// <param name="yaw">The yaw angle, in degree, around the y-axis.</param>
        /// <param name="pitch">The pitch angle, in degree, around the x-axis.</param>
        /// <param name="roll">The roll angle, in degree, around the z-axis.</param>
        /// <returns></returns>
        public static QuaternionFp CreateFromYawPitchRoll(Fp pitch, Fp yaw, Fp roll)
        {
            //  Roll first, about axis the object is facing, then
            //  pitch upward, then yaw to face into the new heading
            Fp sr, cr, sp, cp, sy, cy;

            // pitch α
            Fp halfPitch = MathFp.degree2Radius(pitch * Fp.Half);
            sp = MathFp.sin(halfPitch);
            cp = MathFp.cos(halfPitch);

            // yaw β
            Fp halfYaw = MathFp.degree2Radius(yaw * Fp.Half);
            sy = MathFp.sin(halfYaw);
            cy = MathFp.cos(halfYaw);

            // roll γ
            Fp halfRoll = MathFp.degree2Radius(roll * Fp.Half);
            sr = MathFp.sin(halfRoll);
            cr = MathFp.cos(halfRoll);

            QuaternionFp result;

            result.value.x = cy * sp * cr + sy * cp * sr;
            result.value.y = sy * cp * cr - cy * sp * sr;
            result.value.z = cy * cp * sr - sy * sp * cr;
            result.value.w = cy * cp * cr + sy * sp * sr;
            // result.x = cp * cy * cr + sp * sy * sr;
            // result.y = sp * cy * cr - cp * sy * sr;
            // result.z = cp * sy * cr + sp * cy * sr;
            // result.w = cp * cy * sr - sp * sy * cr;
            return result;
        }

        public static QuaternionFp Divide(QuaternionFp value1, QuaternionFp value2)
        {
            throw new NotImplementedException();
        }

        public static Fp Dot(QuaternionFp lhs, QuaternionFp rhs)
        {
            return lhs.value.x * rhs.value.x + lhs.value.y * rhs.value.y + lhs.value.z * rhs.value.z +
                   lhs.value.w * rhs.value.w;
        }


        /// <summary>
        /// Returns the inverse of a QuaternionFp.
        /// </summary>
        /// <param name="value">The source QuaternionFp.</param>
        /// <returns>The inverted QuaternionFp.</returns>
        public static QuaternionFp Inverse(QuaternionFp value)
        {
            //  -1   (       a              -v       )
            // q   = ( -------------   ------------- )
            //       (  a^2 + |v|^2  ,  a^2 + |v|^2  )


            Fp ls = value.value.x * value.value.x + value.value.y * value.value.y + value.value.z * value.value.z +
                    value.value.w * value.value.w;
            Fp invNorm = Fp.One / ls;
            return new QuaternionFp(-value.value.x * invNorm, -value.value.y * invNorm, -value.value.z * invNorm,
                value.value.w * invNorm);
        }

        /// <summary>
        /// Calculates the length of the QuaternionFp.
        /// </summary>
        /// <returns>The computed length of the QuaternionFp.</returns>
        public Fp Length()
        {
            Fp ls = value.x * value.x + value.y * value.y + value.z * value.z + value.w * value.w;
            return MathFp.sqrt(ls);
        }

        /// <summary>
        /// Calculates the length squared of the QuaternionFp. This operation is cheaper than Length().
        /// </summary>
        /// <returns>The length squared of the QuaternionFp.</returns>
        public Fp LengthSquared()
        {
            return value.x * value.x + value.y * value.y + value.z * value.z + value.w * value.w;
        }

        public static QuaternionFp Lerp(QuaternionFp quaternion1, QuaternionFp quaternion2, Fp amount)
        {
            Fp t = amount;
            Fp t1 = Fp.One - t;

            QuaternionFp r = new QuaternionFp();

            Fp dot = quaternion1.value.x * quaternion2.value.x + quaternion1.value.y * quaternion2.value.y +
                     quaternion1.value.z * quaternion2.value.z + quaternion1.value.w * quaternion2.value.w;

            if (dot >= 0)
            {
                r.value.x = t1 * quaternion1.value.x + t * quaternion2.value.x;
                r.value.y = t1 * quaternion1.value.y + t * quaternion2.value.y;
                r.value.z = t1 * quaternion1.value.z + t * quaternion2.value.z;
                r.value.w = t1 * quaternion1.value.w + t * quaternion2.value.w;
            }
            else
            {
                r.value.x = t1 * quaternion1.value.x - t * quaternion2.value.x;
                r.value.y = t1 * quaternion1.value.y - t * quaternion2.value.y;
                r.value.z = t1 * quaternion1.value.z - t * quaternion2.value.z;
                r.value.w = t1 * quaternion1.value.w - t * quaternion2.value.w;
            }

            // Normalize it.
            Fp ls = r.value.x * r.value.x + r.value.y * r.value.y + r.value.z * r.value.z + r.value.w * r.value.w;
            Fp invNorm = Fp.One / MathFp.sqrt(ls);

            r.value.x *= invNorm;
            r.value.y *= invNorm;
            r.value.z *= invNorm;
            r.value.w *= invNorm;

            return r;
        }

        public static QuaternionFp Negate(QuaternionFp value)
        {
            value.value.x = -value.value.x;
            value.value.y = -value.value.y;
            value.value.z = -value.value.z;
            value.value.w = -value.value.w;
            return value;
        }

        public static QuaternionFp Normalize(QuaternionFp value)
        {
            Fp mag = MathFp.sqrt(Dot(value, value));

            if (mag < Fp.Epsilon)
            {
                return Identity;
            }

            return new QuaternionFp(value.value.x / mag, value.value.y / mag, value.value.z / mag, value.value.w / mag);
        }

        /// <summary>
        /// Adds two Quaternions element-by-element.
        /// </summary>
        /// <param name="value1">The first source QuaternionFp.</param>
        /// <param name="value2">The second source QuaternionFp.</param>
        /// <returns>The result of adding the Quaternions.</returns>
        public static QuaternionFp operator +(QuaternionFp value1, QuaternionFp value2)
        {
            return Add(value1, value2);
        }

        public static QuaternionFp operator /(QuaternionFp value1, QuaternionFp value2)
        {
            throw new NotImplementedException();
        }

        private static bool IsEqualUsingDot(Fp dot)
        {
            // Returns false in the presence of NaN values.
            return dot > (Fp.One - Fp.Epsilon);
        }

        /// <summary>
        /// Are two quaternions equal to each other?
        /// </summary>
        public static bool operator ==(QuaternionFp lhs, QuaternionFp rhs)
        {
            return IsEqualUsingDot(Dot(lhs, rhs));
        }

        /// <summary>
        /// Are two quaternions different from each other?
        /// </summary>
        public static bool operator !=(QuaternionFp lhs, QuaternionFp rhs)
        {
            return !(lhs == rhs);
        }

        /// <summary>
        /// Combines rotations /lhs/ and /rhs/.
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static QuaternionFp operator *(QuaternionFp lhs, QuaternionFp rhs)
        {
            return new QuaternionFp(
                lhs.value.w * rhs.value.x + lhs.value.x * rhs.value.w + lhs.value.y * rhs.value.z -
                lhs.value.z * rhs.value.y,
                lhs.value.w * rhs.value.y + lhs.value.y * rhs.value.w + lhs.value.z * rhs.value.x -
                lhs.value.x * rhs.value.z,
                lhs.value.w * rhs.value.z + lhs.value.z * rhs.value.w + lhs.value.x * rhs.value.y -
                lhs.value.y * rhs.value.x,
                lhs.value.w * rhs.value.w - lhs.value.x * rhs.value.x - lhs.value.y * rhs.value.y -
                lhs.value.z * rhs.value.z);
        }

        public static QuaternionFp operator *(QuaternionFp value1, Fp value2)
        {
            return new QuaternionFp(value1.value.x * value2, value1.value.y * value2, value1.value.z * value2,
                value1.value.w * value2);
        }

        /// <summary>
        /// Subtracts one QuaternionFp from another.
        /// </summary>
        /// <param name="value1">The first source QuaternionFp.</param>
        /// <param name="value2">The second QuaternionFp, to be subtracted from the first.</param>
        /// <returns>The result of the subtraction.</returns>
        public static QuaternionFp operator -(QuaternionFp value1, QuaternionFp value2)
        {
            return Subtract(value1, value2);
        }

        public static QuaternionFp operator -(QuaternionFp value)
        {
            return new QuaternionFp(-value.value.x, -value.value.y, -value.value.z, -value.value.w);
        }

        public static QuaternionFp Slerp(QuaternionFp quaternion1, QuaternionFp quaternion2, Fp amount)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Subtracts one QuaternionFp from another.
        /// </summary>
        /// <param name="value1">The first source QuaternionFp.</param>
        /// <param name="value2">The second QuaternionFp, to be subtracted from the first.</param>
        /// <returns>The result of the subtraction.</returns>
        public static QuaternionFp Subtract(QuaternionFp value1, QuaternionFp value2)
        {
            QuaternionFp ans;

            ans.value.x = value1.value.x - value2.value.x;
            ans.value.y = value1.value.y - value2.value.y;
            ans.value.z = value1.value.z - value2.value.z;
            ans.value.w = value1.value.w - value2.value.w;

            return ans;
        }

        /// <summary>
        /// Constructs a quaternion from a rotation matrix.
        /// </summary>
        /// <param name="r">Rotation matrix to create the quaternion from.</param>
        /// <param name="q">QuaternionFp based on the rotation matrix.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateFromRotationMatrix(in Fp3x3 r, out QuaternionFp q)
        {
            Fp t;
            q = new QuaternionFp();
            if (r.c2.z < 0)
            {
                if (r.c0.x > r.c1.y)
                {
                    t = 1 + r.c0.x - r.c1.y - r.c2.z;
                    q.value.x = t;
                    q.value.y = r.c1.x + r.c0.y;
                    q.value.z = r.c0.z + r.c2.x;
                    q.value.w = r.c2.y - r.c1.z;
                }
                else
                {
                    t = 1 - r.c0.x + r.c1.y - r.c2.z;
                    q.value.x = r.c1.x + r.c0.y;
                    q.value.y = t;
                    q.value.z = r.c2.y + r.c1.z;
                    q.value.w = r.c0.z - r.c2.x;
                }
            }
            else
            {
                if (r.c0.x < -r.c1.y)
                {
                    t = 1 - r.c0.x - r.c1.y + r.c2.z;
                    q.value.x = r.c0.z + r.c2.x;
                    q.value.y = r.c2.y + r.c1.z;
                    q.value.z = t;
                    q.value.w = r.c1.x - r.c0.y;
                }
                else
                {
                    t = 1 + r.c0.x + r.c1.y + r.c2.z;
                    q.value.x = r.c2.y - r.c1.z;
                    q.value.y = r.c0.z - r.c2.x;
                    q.value.z = r.c1.x - r.c0.y;
                    q.value.w = t;
                }
            }

            q *= Fp.Half / MathFp.sqrt(t);
            HandleQuaternionSign(ref q);
        }

        #endregion

        #region BepuPhysics

        /// <summary>
        /// Transforms the vector using a quaternion, assuming that the output does not alias with the input.
        /// </summary>
        /// <param name="rotation">Rotation to apply to the vector.</param>
        /// <param name="v">Vector to transform.</param>
        /// <returns>Transformed vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3 Transform(QuaternionFp rotation, in Fp3 v)
        {
            //This operation is an optimized-down version of v' = q * v * q^-1.
            //The expanded form would be to treat v as an 'axis only' quaternion
            //and perform standard quaternion multiplication.  Assuming q is normalized,
            //q^-1 can be replaced by a conjugation.
            Fp x2 = rotation.value.x + rotation.value.x;
            Fp y2 = rotation.value.y + rotation.value.y;
            Fp z2 = rotation.value.z + rotation.value.z;
            Fp xx2 = rotation.value.x * x2;
            Fp xy2 = rotation.value.x * y2;
            Fp xz2 = rotation.value.x * z2;
            Fp yy2 = rotation.value.y * y2;
            Fp yz2 = rotation.value.y * z2;
            Fp zz2 = rotation.value.z * z2;
            Fp wx2 = rotation.value.w * x2;
            Fp wy2 = rotation.value.w * y2;
            Fp wz2 = rotation.value.w * z2;
            //Defer the component setting since they're used in computation.
            return new Fp3(v.x * (Fp.One - yy2 - zz2) + v.y * (xy2 - wz2) + v.z * (xz2 + wy2),
                v.x * (xy2 + wz2) + v.y * (Fp.One - xx2 - zz2) + v.z * (yz2 - wx2),
                v.x * (xz2 - wy2) + v.y * (yz2 + wx2) + v.z * (Fp.One - xx2 - yy2));
        }

        /// <summary>
        /// Concatenates the transforms of two quaternions together such that the resulting quaternion, applied as an orientation to a vector v, is equivalent to
        /// transformed = (v * a) * b.
        /// Assumes that neither input parameter overlaps the output parameter.
        /// </summary>
        /// <param name="a">First quaternion to concatenate.</param>
        /// <param name="b">Second quaternion to concatenate.</param>
        /// <returns>Product of the concatenation.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static QuaternionFp Concatenate(QuaternionFp a, in QuaternionFp b)
        {
            return new QuaternionFp(
                a.value.w * b.value.x + a.value.x * b.value.w + a.value.z * b.value.y - a.value.y * b.value.z,
                a.value.w * b.value.y + a.value.y * b.value.w + a.value.x * b.value.z - a.value.z * b.value.x,
                a.value.w * b.value.z + a.value.z * b.value.w + a.value.y * b.value.x - a.value.x * b.value.y,
                a.value.w * b.value.w - a.value.x * b.value.x - a.value.y * b.value.y - a.value.z * b.value.z);
        }


        /// <summary>
        /// Transforms the unit y vector using a quaternion.
        /// </summary>
        /// <param name="rotation">Rotation to apply to the vector.</param>
        /// <returns>Transformed vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3 TransformUnitY(QuaternionFp rotation)
        {
            //This operation is an optimized-down version of v' = q * v * q^-1.
            //The expanded form would be to treat v as an 'axis only' quaternion
            //and perform standard quaternion multiplication.  Assuming q is normalized,
            //q^-1 can be replaced by a conjugation.
            Fp x2 = rotation.value.x + rotation.value.x;
            Fp y2 = rotation.value.y + rotation.value.y;
            Fp z2 = rotation.value.z + rotation.value.z;
            Fp xx2 = rotation.value.x * x2;
            Fp xy2 = rotation.value.x * y2;
            Fp yz2 = rotation.value.y * z2;
            Fp zz2 = rotation.value.z * z2;
            Fp wx2 = rotation.value.w * x2;
            Fp wz2 = rotation.value.w * z2;
            return new Fp3(xy2 - wz2,
                Fp.One - xx2 - zz2,
                yz2 + wx2);
        }

        /// <summary>
        /// Transforms the unit Z vector using a quaternion.
        /// </summary>
        /// <param name="rotation">Rotation to apply to the vector.</param>
        /// <returns>Transformed vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3 TransformUnitZ(in QuaternionFp rotation)
        {
            //This operation is an optimized-down version of v' = q * v * q^-1.
            //The expanded form would be to treat v as an 'axis only' quaternion
            //and perform standard quaternion multiplication.  Assuming q is normalized,
            //q^-1 can be replaced by a conjugation.
            Fp x2 = rotation.value.x + rotation.value.x;
            Fp y2 = rotation.value.y + rotation.value.y;
            Fp z2 = rotation.value.z + rotation.value.z;
            Fp xx2 = rotation.value.x * x2;
            Fp xz2 = rotation.value.x * z2;
            Fp yy2 = rotation.value.y * y2;
            Fp yz2 = rotation.value.y * z2;
            Fp wx2 = rotation.value.w * x2;
            Fp wy2 = rotation.value.w * y2;
            var result = new Fp3();
            result.x = xz2 + wy2;
            result.y = yz2 - wx2;
            result.z = Fp.One - xx2 - yy2;
            return result;
        }

        /// <summary>
        /// Transforms the unit X and unit Y direction using a quaternion.
        /// </summary>
        /// <param name="rotation">Rotation to apply to the vectors.</param>
        /// <param name="x">Transformed unit X vector.</param>
        /// <param name="y">Transformed unit Y vector.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void TransformUnitXY(in QuaternionFp rotation, out Fp3 x, out Fp3 y)
        {
            var x2 = rotation.value.x + rotation.value.x;
            var y2 = rotation.value.y + rotation.value.y;
            var z2 = rotation.value.z + rotation.value.z;
            var xx2 = rotation.value.x * x2;
            var xy2 = rotation.value.x * y2;
            var xz2 = rotation.value.x * z2;
            var yy2 = rotation.value.y * y2;
            var yz2 = rotation.value.y * z2;
            var zz2 = rotation.value.z * z2;
            var wx2 = rotation.value.w * x2;
            var wy2 = rotation.value.w * y2;
            var wz2 = rotation.value.w * z2;
            x = new Fp3();
            y = new Fp3();
            x.x = 1 - yy2 - zz2;
            x.y = xy2 + wz2;
            x.z = xz2 - wy2;
            y.x = xy2 - wz2;
            y.y = 1 - xx2 - zz2;
            y.z = yz2 + wx2;
        }

        /// <summary>
        /// Computes the quaternion rotation between two normalized vectors.
        /// </summary>
        /// <param name="v1">First unit-length vector.</param>
        /// <param name="v2">Second unit-length vector.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static QuaternionFp GetQuaternionBetweenNormalizedVectors(in Fp3 v1, in Fp3 v2)
        {
            QuaternionFp q;
            Fp dot = Fp3.Dot(v1, v2);
            //For non-normal vectors, the multiplying the axes length squared would be necessary:
            //Fp w = dot + (Fp)MathFp.sqrt(v1.LengthSquared() * v2.LengthSquared());
            //parallel, opposing direction
            if (dot < -Fp.Point9999)
            {
                //If this occurs, the rotation required is ~180 degrees.
                //The problem is that we could choose any perpendicular axis for the rotation. It's not uniquely defined.
                //The solution is to pick an arbitrary perpendicular axis.
                //Project onto the plane which has the lowest component magnitude.
                //On that 2d plane, perform a 90 degree rotation.
                Fp absX = MathFp.abs(v1.x);
                Fp absY = MathFp.abs(v1.y);
                Fp absZ = MathFp.abs(v1.z);
                if (absX < absY && absX < absZ)
                    q = new QuaternionFp(0, -v1.z, v1.y, 0);
                else if (absY < absZ)
                    q = new QuaternionFp(-v1.z, 0, v1.x, 0);
                else
                    q = new QuaternionFp(-v1.y, v1.x, 0, 0);
            }
            else
            {
                var axis = Fp3.Cross(v1, v2);
                q = new QuaternionFp(axis.x, axis.y, axis.z, dot + 1);
            }

            q = QuaternionFp.Normalize(q);
            return q;
        }

        #endregion
    }

    public static partial class MathFp
    {
        #region QuaternionFp

        /// <summary>Returns a QuaternionFp constructed from four Fp values.</summary>
        /// <param name="x">The x component of the QuaternionFp.</param>
        /// <param name="y">The y component of the QuaternionFp.</param>
        /// <param name="z">The z component of the QuaternionFp.</param>
        /// <param name="w">The w component of the QuaternionFp.</param>
        /// <returns>The QuaternionFp constructed from individual components.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static QuaternionFp QuaternionFp(Fp x, Fp y, Fp z, Fp w)
        {
            return new QuaternionFp(x, y, z, w);
        }

        /// <summary>Returns a QuaternionFp constructed from a Fp4 vector.</summary>
        /// <param name="value">The Fp4 containing the components of the QuaternionFp.</param>
        /// <returns>The QuaternionFp constructed from a Fp4.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static QuaternionFp QuaternionFp(Fp4 value)
        {
            return new QuaternionFp(value);
        }

        /// <summary>Returns a unit QuaternionFp constructed from a Fp3x3 rotation matrix. The matrix must be orthonormal.</summary>
        /// <param name="m">The Fp3x3 rotation matrix.</param>
        /// <returns>The QuaternionFp constructed from a Fp3x3 matrix.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static QuaternionFp QuaternionFp(Fp3x3 m)
        {
            return new QuaternionFp(m);
        }

        /// <summary>Returns a unit QuaternionFp constructed from a Fp4x4 matrix. The matrix must be orthonormal.</summary>
        /// <param name="m">The Fp4x4 matrix (must be orthonormal).</param>
        /// <returns>The QuaternionFp constructed from a Fp4x4 matrix.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static QuaternionFp QuaternionFp(Fp4x4 m)
        {
            return new QuaternionFp(m);
        }

        /// <summary>Returns the conjugate of a QuaternionFp value.</summary>
        /// <param name="q">The QuaternionFp to conjugate.</param>
        /// <returns>The conjugate of the input QuaternionFp.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static QuaternionFp conjugate(QuaternionFp q)
        {
            return QuaternionFp(q.value * Fp4(-1, -1, -1, 1));
        }

        /// <summary>Returns the inverse of a QuaternionFp value.</summary>
        /// <param name="q">The QuaternionFp to invert.</param>
        /// <returns>The QuaternionFp inverse of the input QuaternionFp.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static QuaternionFp inverse(QuaternionFp q)
        {
            Fp4 x = q.value;
            return QuaternionFp(rcp(dot(x, x)) * x * Fp4(-1, -1, -1, 1));
        }

        /// <summary>Returns the dot product of two quaternions.</summary>
        /// <param name="a">The first QuaternionFp.</param>
        /// <param name="b">The second QuaternionFp.</param>
        /// <returns>The dot product of two quaternions.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp dot(QuaternionFp a, QuaternionFp b)
        {
            return dot(a.value, b.value);
        }

        /// <summary>Returns the length of a QuaternionFp.</summary>
        /// <param name="q">The input QuaternionFp.</param>
        /// <returns>The length of the input QuaternionFp.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp length(QuaternionFp q)
        {
            return sqrt(dot(q.value, q.value));
        }

        /// <summary>Returns the squared length of a QuaternionFp.</summary>
        /// <param name="q">The input QuaternionFp.</param>
        /// <returns>The length squared of the input QuaternionFp.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp lengthsq(QuaternionFp q)
        {
            return dot(q.value, q.value);
        }

        /// <summary>Returns a normalized version of a QuaternionFp q by scaling it by 1 / length(q).</summary>
        /// <param name="q">The QuaternionFp to normalize.</param>
        /// <returns>The normalized QuaternionFp.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static QuaternionFp normalize(QuaternionFp q)
        {
            Fp4 x = q.value;
            return QuaternionFp(rsqrt(dot(x, x)) * x);
        }

        /// <summary>
        /// Returns a safe normalized version of the q by scaling it by 1 / length(q).
        /// Returns the identity when 1 / length(q) does not produce a finite number.
        /// </summary>
        /// <param name="q">The QuaternionFp to normalize.</param>
        /// <returns>The normalized QuaternionFp or the identity QuaternionFp.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static QuaternionFp normalizesafe(QuaternionFp q)
        {
            Fp4 x = q.value;
            Fp len = MathFp.dot(x, x);
            return QuaternionFp(MathFp.select(FixedPoint.QuaternionFp.Identity.value,
                x * MathFp.rsqrt(len), len > Fp.MinValue));
        }

        /// <summary>
        /// Returns a safe normalized version of the q by scaling it by 1 / length(q).
        /// Returns the given default value when 1 / length(q) does not produce a finite number.
        /// </summary>
        /// <param name="q">The QuaternionFp to normalize.</param>
        /// <param name="defaultvalue">The default value.</param>
        /// <returns>The normalized QuaternionFp or the default value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static QuaternionFp normalizesafe(QuaternionFp q, QuaternionFp defaultvalue)
        {
            Fp4 x = q.value;
            Fp len = MathFp.dot(x, x);
            return QuaternionFp(MathFp.select(defaultvalue.value, x * MathFp.rsqrt(len), len > Fp.MinValue));
        }

        /// <summary>Returns the natural exponent of a QuaternionFp. Assumes w is zero.</summary>
        /// <param name="q">The QuaternionFp with w component equal to zero.</param>
        /// <returns>The natural exponent of the input QuaternionFp.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static QuaternionFp unitexp(QuaternionFp q)
        {
            Fp v_rcp_len = rsqrt(dot(q.value.xyz, q.value.xyz));
            Fp v_len = rcp(v_rcp_len);
            Fp sin_v_len, cos_v_len;
            sincos(v_len, out sin_v_len, out cos_v_len);
            return QuaternionFp(Fp4(q.value.xyz * v_rcp_len * sin_v_len, cos_v_len));
        }

        /// <summary>Returns the natural exponent of a QuaternionFp.</summary>
        /// <param name="q">The QuaternionFp.</param>
        /// <returns>The natural exponent of the input QuaternionFp.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static QuaternionFp exp(QuaternionFp q)
        {
            Fp v_rcp_len = rsqrt(dot(q.value.xyz, q.value.xyz));
            Fp v_len = rcp(v_rcp_len);
            Fp sin_v_len, cos_v_len;
            sincos(v_len, out sin_v_len, out cos_v_len);
            return QuaternionFp(Fp4(q.value.xyz * v_rcp_len * sin_v_len, cos_v_len) * MathFp.exp(q.value.w));
        }

        /// <summary>Returns the natural logarithm of a unit length QuaternionFp.</summary>
        /// <param name="q">The unit length QuaternionFp.</param>
        /// <returns>The natural logarithm of the unit length QuaternionFp.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static QuaternionFp unitlog(QuaternionFp q)
        {
            Fp w = clamp(q.value.w, -Fp.One, Fp.One);
            Fp s = acos(w) * rsqrt(1 - w * w);
            return QuaternionFp(Fp4(q.value.xyz * s, 0));
        }

        /// <summary>Returns the natural logarithm of a QuaternionFp.</summary>
        /// <param name="q">The QuaternionFp.</param>
        /// <returns>The natural logarithm of the input QuaternionFp.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static QuaternionFp log(QuaternionFp q)
        {
            Fp v_len_sq = dot(q.value.xyz, q.value.xyz);
            Fp q_len_sq = v_len_sq + q.value.w * q.value.w;

            Fp s = acos(clamp(q.value.w * rsqrt(q_len_sq), -Fp.One, Fp.One)) * rsqrt(v_len_sq);
            return QuaternionFp(Fp4(q.value.xyz * s, Fp.Half * log(q_len_sq)));
        }

        /// <summary>Returns the result of transforming the QuaternionFp b by the QuaternionFp a.</summary>
        /// <param name="a">The QuaternionFp on the left.</param>
        /// <param name="b">The QuaternionFp on the right.</param>
        /// <returns>The result of transforming QuaternionFp b by the QuaternionFp a.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static QuaternionFp mul(QuaternionFp a, QuaternionFp b)
        {
            return QuaternionFp(a.value.wwww * b.value +
                                (a.value.xyzx * b.value.wwwx + a.value.yzxy * b.value.zxyy) *
                                Fp4(1, 1, 1, -1) -
                                a.value.zxyz * b.value.yzxz);
        }

        /// <summary>Returns the result of transforming a vector by a QuaternionFp.</summary>
        /// <param name="q">The QuaternionFp transformation.</param>
        /// <param name="v">The vector to transform.</param>
        /// <returns>The transformation of vector v by QuaternionFp q.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3 mul(QuaternionFp q, Fp3 v)
        {
            Fp3 t = 2 * cross(q.value.xyz, v);
            return v + q.value.w * t + cross(q.value.xyz, t);
        }

        /// <summary>Returns the result of rotating a vector by a unit QuaternionFp.</summary>
        /// <param name="q">The QuaternionFp rotation.</param>
        /// <param name="v">The vector to rotate.</param>
        /// <returns>The rotation of vector v by QuaternionFp q.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3 rotate(QuaternionFp q, Fp3 v)
        {
            Fp3 t = 2 * cross(q.value.xyz, v);
            return v + q.value.w * t + cross(q.value.xyz, t);
        }

        /// <summary>Returns the result of a normalized linear interpolation between two quaternions q1 and a2 using an interpolation parameter t.</summary>
        /// <remarks>
        /// Prefer to use this over slerp() when you know the distance between q1 and q2 is small. This can be much
        /// higher performance due to avoiding trigonometric function evaluations that occur in slerp().
        /// </remarks>
        /// <param name="q1">The first QuaternionFp.</param>
        /// <param name="q2">The second QuaternionFp.</param>
        /// <param name="t">The interpolation parameter.</param>
        /// <returns>The normalized linear interpolation of two quaternions.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static QuaternionFp nlerp(QuaternionFp q1, QuaternionFp q2, Fp t)
        {
            Fp dt = dot(q1, q2);
            if (dt < 0)
            {
                q2.value = -q2.value;
            }

            return normalize(QuaternionFp(lerp(q1.value, q2.value, t)));
        }

        /// <summary>Returns the result of a spherical interpolation between two quaternions q1 and a2 using an interpolation parameter t.</summary>
        /// <param name="q1">The first QuaternionFp.</param>
        /// <param name="q2">The second QuaternionFp.</param>
        /// <param name="t">The interpolation parameter.</param>
        /// <returns>The spherical linear interpolation of two quaternions.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static QuaternionFp slerp(QuaternionFp q1, QuaternionFp q2, Fp t)
        {
            Fp dt = dot(q1, q2);
            if (dt < 0)
            {
                dt = -dt;
                q2.value = -q2.value;
            }

            if (dt < Fp.Point9995)
            {
                Fp angle = acos(dt);
                Fp s = rsqrt(1 - dt * dt); // 1 / sin(angle)
                Fp w1 = sin(angle * (1 - t)) * s;
                Fp w2 = sin(angle * t) * s;
                return QuaternionFp(q1.value * w1 + q2.value * w2);
            }
            else
            {
                // if the angle is small, use linear interpolation
                return nlerp(q1, q2, t);
            }
        }

        /// <summary>Returns a uint hash code of a QuaternionFp.</summary>
        /// <param name="q">The QuaternionFp to hash.</param>
        /// <returns>The hash code for the input QuaternionFp.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint hash(QuaternionFp q)
        {
            return hash(q.value);
        }

        /// <summary>
        /// Returns a uint4 vector hash code of a QuaternionFp.
        /// When multiple elements are to be hashes together, it can more efficient to calculate and combine wide hash
        /// that are only reduced to a narrow uint hash at the very end instead of at every step.
        /// </summary>
        /// <param name="q">The QuaternionFp to hash.</param>
        /// <returns>The uint4 vector hash code of the input QuaternionFp.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint4 hashwide(QuaternionFp q)
        {
            return hashwide(q.value);
        }


        /// <summary>
        /// Transforms the forward vector by a QuaternionFp.
        /// </summary>
        /// <param name="q">The QuaternionFp transformation.</param>
        /// <returns>The forward vector transformed by the input QuaternionFp.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3 forward(QuaternionFp q)
        {
            return mul(q, Fp3(0, 0, 1));
        } // for compatibility

        #endregion
    }
}