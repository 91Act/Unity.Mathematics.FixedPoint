//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Runtime.CompilerServices;

namespace Unity.Mathematics.FixedPoint
{
    partial class MathFp
    {
        /// <summary>Returns the Fp value result of a matrix multiplication between a Fp value and a Fp value.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp mul(Fp a, Fp b)
        {
            return IgnoreTooSmallNumber(a * b);
        }

        /// <summary>Returns the Fp value result of a matrix multiplication between a Fp2 row vector and a Fp2 column vector.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp mul(Fp2 a, Fp2 b)
        {
            return IgnoreTooSmallNumber(a.x * b.x) + IgnoreTooSmallNumber(a.y * b.y);
        }

        /// <summary>Returns the Fp2 row vector result of a matrix multiplication between a Fp2 row vector and a Fp2x2 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp2 mul(Fp2 a, Fp2x2 b)
        {
            return Fp2(
                IgnoreTooSmallNumber(a.x * b.c0.x) + IgnoreTooSmallNumber(a.y * b.c0.y),
                IgnoreTooSmallNumber(a.x * b.c1.x) + IgnoreTooSmallNumber(a.y * b.c1.y));
        }

        /// <summary>Returns the Fp3 row vector result of a matrix multiplication between a Fp2 row vector and a Fp2x3 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3 mul(Fp2 a, Fp2x3 b)
        {
            return Fp3(
                IgnoreTooSmallNumber(a.x * b.c0.x) + IgnoreTooSmallNumber(a.y * b.c0.y),
                IgnoreTooSmallNumber(a.x * b.c1.x) + IgnoreTooSmallNumber(a.y * b.c1.y),
                IgnoreTooSmallNumber(a.x * b.c2.x) + IgnoreTooSmallNumber(a.y * b.c2.y));
        }

        /// <summary>Returns the Fp4 row vector result of a matrix multiplication between a Fp2 row vector and a Fp2x4 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4 mul(Fp2 a, Fp2x4 b)
        {
            return Fp4(
                IgnoreTooSmallNumber(a.x * b.c0.x) + IgnoreTooSmallNumber(a.y * b.c0.y),
                IgnoreTooSmallNumber(a.x * b.c1.x) + IgnoreTooSmallNumber(a.y * b.c1.y),
                IgnoreTooSmallNumber(a.x * b.c2.x) + IgnoreTooSmallNumber(a.y * b.c2.y),
                IgnoreTooSmallNumber(a.x * b.c3.x) + IgnoreTooSmallNumber(a.y * b.c3.y));
        }

        /// <summary>Returns the Fp value result of a matrix multiplication between a Fp3 row vector and a Fp3 column vector.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp mul(Fp3 a, Fp3 b)
        {
            return IgnoreTooSmallNumber(a.x * b.x) + IgnoreTooSmallNumber(a.y * b.y) + IgnoreTooSmallNumber(a.z * b.z);
        }

        /// <summary>Returns the Fp2 row vector result of a matrix multiplication between a Fp3 row vector and a Fp3x2 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp2 mul(Fp3 a, Fp3x2 b)
        {
            return Fp2(
                IgnoreTooSmallNumber(a.x * b.c0.x) + IgnoreTooSmallNumber(a.y * b.c0.y) + IgnoreTooSmallNumber(a.z * b.c0.z),
                IgnoreTooSmallNumber(a.x * b.c1.x) + IgnoreTooSmallNumber(a.y * b.c1.y) + IgnoreTooSmallNumber(a.z * b.c1.z));
        }

        /// <summary>Returns the Fp3 row vector result of a matrix multiplication between a Fp3 row vector and a Fp3x3 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3 mul(Fp3 a, Fp3x3 b)
        {
            return Fp3(
                IgnoreTooSmallNumber(a.x * b.c0.x) + IgnoreTooSmallNumber(a.y * b.c0.y) + IgnoreTooSmallNumber(a.z * b.c0.z),
                IgnoreTooSmallNumber(a.x * b.c1.x) + IgnoreTooSmallNumber(a.y * b.c1.y) + IgnoreTooSmallNumber(a.z * b.c1.z),
                IgnoreTooSmallNumber(a.x * b.c2.x) + IgnoreTooSmallNumber(a.y * b.c2.y) + IgnoreTooSmallNumber(a.z * b.c2.z));
        }

        /// <summary>Returns the Fp4 row vector result of a matrix multiplication between a Fp3 row vector and a Fp3x4 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4 mul(Fp3 a, Fp3x4 b)
        {
            return Fp4(
                IgnoreTooSmallNumber(a.x * b.c0.x) + IgnoreTooSmallNumber(a.y * b.c0.y) + IgnoreTooSmallNumber(a.z * b.c0.z),
                IgnoreTooSmallNumber(a.x * b.c1.x) + IgnoreTooSmallNumber(a.y * b.c1.y) + IgnoreTooSmallNumber(a.z * b.c1.z),
                IgnoreTooSmallNumber(a.x * b.c2.x) + IgnoreTooSmallNumber(a.y * b.c2.y) + IgnoreTooSmallNumber(a.z * b.c2.z),
                IgnoreTooSmallNumber(a.x * b.c3.x) + IgnoreTooSmallNumber(a.y * b.c3.y) + IgnoreTooSmallNumber(a.z * b.c3.z));
        }

        /// <summary>Returns the Fp value result of a matrix multiplication between a Fp4 row vector and a Fp4 column vector.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp mul(Fp4 a, Fp4 b)
        {
            return IgnoreTooSmallNumber(a.x * b.x) + IgnoreTooSmallNumber(a.y * b.y) + IgnoreTooSmallNumber(a.z * b.z) + IgnoreTooSmallNumber(a.w * b.w);
        }

        /// <summary>Returns the Fp2 row vector result of a matrix multiplication between a Fp4 row vector and a Fp4x2 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp2 mul(Fp4 a, Fp4x2 b)
        {
            return Fp2(
                IgnoreTooSmallNumber(a.x * b.c0.x) + IgnoreTooSmallNumber(a.y * b.c0.y) + IgnoreTooSmallNumber(a.z * b.c0.z) + IgnoreTooSmallNumber(a.w * b.c0.w),
                IgnoreTooSmallNumber(a.x * b.c1.x) + IgnoreTooSmallNumber(a.y * b.c1.y) + IgnoreTooSmallNumber(a.z * b.c1.z) + IgnoreTooSmallNumber(a.w * b.c1.w));
        }

        /// <summary>Returns the Fp3 row vector result of a matrix multiplication between a Fp4 row vector and a Fp4x3 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3 mul(Fp4 a, Fp4x3 b)
        {
            return Fp3(
                IgnoreTooSmallNumber(a.x * b.c0.x) + IgnoreTooSmallNumber(a.y * b.c0.y) + IgnoreTooSmallNumber(a.z * b.c0.z) + IgnoreTooSmallNumber(a.w * b.c0.w),
                IgnoreTooSmallNumber(a.x * b.c1.x) + IgnoreTooSmallNumber(a.y * b.c1.y) + IgnoreTooSmallNumber(a.z * b.c1.z) + IgnoreTooSmallNumber(a.w * b.c1.w),
                IgnoreTooSmallNumber(a.x * b.c2.x) + IgnoreTooSmallNumber(a.y * b.c2.y) + IgnoreTooSmallNumber(a.z * b.c2.z) + IgnoreTooSmallNumber(a.w * b.c2.w));
        }

        /// <summary>Returns the Fp4 row vector result of a matrix multiplication between a Fp4 row vector and a Fp4x4 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4 mul(Fp4 a, Fp4x4 b)
        {
            return Fp4(
                IgnoreTooSmallNumber(a.x * b.c0.x) + IgnoreTooSmallNumber(a.y * b.c0.y) + IgnoreTooSmallNumber(a.z * b.c0.z) + IgnoreTooSmallNumber(a.w * b.c0.w),
                IgnoreTooSmallNumber(a.x * b.c1.x) + IgnoreTooSmallNumber(a.y * b.c1.y) + IgnoreTooSmallNumber(a.z * b.c1.z) + IgnoreTooSmallNumber(a.w * b.c1.w),
                IgnoreTooSmallNumber(a.x * b.c2.x) + IgnoreTooSmallNumber(a.y * b.c2.y) + IgnoreTooSmallNumber(a.z * b.c2.z) + IgnoreTooSmallNumber(a.w * b.c2.w),
                IgnoreTooSmallNumber(a.x * b.c3.x) + IgnoreTooSmallNumber(a.y * b.c3.y) + IgnoreTooSmallNumber(a.z * b.c3.z) + IgnoreTooSmallNumber(a.w * b.c3.w));
        }

        /// <summary>Returns the Fp2 column vector result of a matrix multiplication between a Fp2x2 matrix and a Fp2 column vector.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp2 mul(Fp2x2 a, Fp2 b)
        {
            return IgnoreTooSmallNumber(a.c0 * b.x) + IgnoreTooSmallNumber(a.c1 * b.y);
        }

        /// <summary>Returns the Fp2x2 matrix result of a matrix multiplication between a Fp2x2 matrix and a Fp2x2 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp2x2 mul(Fp2x2 a, Fp2x2 b)
        {
            return Fp2x2(
                IgnoreTooSmallNumber(a.c0 * b.c0.x) + IgnoreTooSmallNumber(a.c1 * b.c0.y),
                IgnoreTooSmallNumber(a.c0 * b.c1.x) + IgnoreTooSmallNumber(a.c1 * b.c1.y));
        }

        /// <summary>Returns the Fp2x3 matrix result of a matrix multiplication between a Fp2x2 matrix and a Fp2x3 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp2x3 mul(Fp2x2 a, Fp2x3 b)
        {
            return Fp2x3(
                IgnoreTooSmallNumber(a.c0 * b.c0.x) + IgnoreTooSmallNumber(a.c1 * b.c0.y),
                IgnoreTooSmallNumber(a.c0 * b.c1.x) + IgnoreTooSmallNumber(a.c1 * b.c1.y),
                IgnoreTooSmallNumber(a.c0 * b.c2.x) + IgnoreTooSmallNumber(a.c1 * b.c2.y));
        }

        /// <summary>Returns the Fp2x4 matrix result of a matrix multiplication between a Fp2x2 matrix and a Fp2x4 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp2x4 mul(Fp2x2 a, Fp2x4 b)
        {
            return Fp2x4(
                IgnoreTooSmallNumber(a.c0 * b.c0.x) + IgnoreTooSmallNumber(a.c1 * b.c0.y),
                IgnoreTooSmallNumber(a.c0 * b.c1.x) + IgnoreTooSmallNumber(a.c1 * b.c1.y),
                IgnoreTooSmallNumber(a.c0 * b.c2.x) + IgnoreTooSmallNumber(a.c1 * b.c2.y),
                IgnoreTooSmallNumber(a.c0 * b.c3.x) + IgnoreTooSmallNumber(a.c1 * b.c3.y));
        }

        /// <summary>Returns the Fp2 column vector result of a matrix multiplication between a Fp2x3 matrix and a Fp3 column vector.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp2 mul(Fp2x3 a, Fp3 b)
        {
            return IgnoreTooSmallNumber(a.c0 * b.x) + IgnoreTooSmallNumber(a.c1 * b.y) + IgnoreTooSmallNumber(a.c2 * b.z);
        }

        /// <summary>Returns the Fp2x2 matrix result of a matrix multiplication between a Fp2x3 matrix and a Fp3x2 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp2x2 mul(Fp2x3 a, Fp3x2 b)
        {
            return Fp2x2(
                IgnoreTooSmallNumber(a.c0 * b.c0.x) + IgnoreTooSmallNumber(a.c1 * b.c0.y) + IgnoreTooSmallNumber(a.c2 * b.c0.z),
                IgnoreTooSmallNumber(a.c0 * b.c1.x) + IgnoreTooSmallNumber(a.c1 * b.c1.y) + IgnoreTooSmallNumber(a.c2 * b.c1.z));
        }

        /// <summary>Returns the Fp2x3 matrix result of a matrix multiplication between a Fp2x3 matrix and a Fp3x3 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp2x3 mul(Fp2x3 a, Fp3x3 b)
        {
            return Fp2x3(
                IgnoreTooSmallNumber(a.c0 * b.c0.x) + IgnoreTooSmallNumber(a.c1 * b.c0.y) + IgnoreTooSmallNumber(a.c2 * b.c0.z),
                IgnoreTooSmallNumber(a.c0 * b.c1.x) + IgnoreTooSmallNumber(a.c1 * b.c1.y) + IgnoreTooSmallNumber(a.c2 * b.c1.z),
                IgnoreTooSmallNumber(a.c0 * b.c2.x) + IgnoreTooSmallNumber(a.c1 * b.c2.y) + IgnoreTooSmallNumber(a.c2 * b.c2.z));
        }

        /// <summary>Returns the Fp2x4 matrix result of a matrix multiplication between a Fp2x3 matrix and a Fp3x4 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp2x4 mul(Fp2x3 a, Fp3x4 b)
        {
            return Fp2x4(
                IgnoreTooSmallNumber(a.c0 * b.c0.x) + IgnoreTooSmallNumber(a.c1 * b.c0.y) + IgnoreTooSmallNumber(a.c2 * b.c0.z),
                IgnoreTooSmallNumber(a.c0 * b.c1.x) + IgnoreTooSmallNumber(a.c1 * b.c1.y) + IgnoreTooSmallNumber(a.c2 * b.c1.z),
                IgnoreTooSmallNumber(a.c0 * b.c2.x) + IgnoreTooSmallNumber(a.c1 * b.c2.y) + IgnoreTooSmallNumber(a.c2 * b.c2.z),
                IgnoreTooSmallNumber(a.c0 * b.c3.x) + IgnoreTooSmallNumber(a.c1 * b.c3.y) + IgnoreTooSmallNumber(a.c2 * b.c3.z));
        }

        /// <summary>Returns the Fp2 column vector result of a matrix multiplication between a Fp2x4 matrix and a Fp4 column vector.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp2 mul(Fp2x4 a, Fp4 b)
        {
            return IgnoreTooSmallNumber(a.c0 * b.x) + IgnoreTooSmallNumber(a.c1 * b.y) + IgnoreTooSmallNumber(a.c2 * b.z) + IgnoreTooSmallNumber(a.c3 * b.w);
        }

        /// <summary>Returns the Fp2x2 matrix result of a matrix multiplication between a Fp2x4 matrix and a Fp4x2 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp2x2 mul(Fp2x4 a, Fp4x2 b)
        {
            return Fp2x2(
                IgnoreTooSmallNumber(a.c0 * b.c0.x) + IgnoreTooSmallNumber(a.c1 * b.c0.y) + IgnoreTooSmallNumber(a.c2 * b.c0.z) + IgnoreTooSmallNumber(a.c3 * b.c0.w),
                IgnoreTooSmallNumber(a.c0 * b.c1.x) + IgnoreTooSmallNumber(a.c1 * b.c1.y) + IgnoreTooSmallNumber(a.c2 * b.c1.z) + IgnoreTooSmallNumber(a.c3 * b.c1.w));
        }

        /// <summary>Returns the Fp2x3 matrix result of a matrix multiplication between a Fp2x4 matrix and a Fp4x3 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp2x3 mul(Fp2x4 a, Fp4x3 b)
        {
            return Fp2x3(
                IgnoreTooSmallNumber(a.c0 * b.c0.x) + IgnoreTooSmallNumber(a.c1 * b.c0.y) + IgnoreTooSmallNumber(a.c2 * b.c0.z) + IgnoreTooSmallNumber(a.c3 * b.c0.w),
                IgnoreTooSmallNumber(a.c0 * b.c1.x) + IgnoreTooSmallNumber(a.c1 * b.c1.y) + IgnoreTooSmallNumber(a.c2 * b.c1.z) + IgnoreTooSmallNumber(a.c3 * b.c1.w),
                IgnoreTooSmallNumber(a.c0 * b.c2.x) + IgnoreTooSmallNumber(a.c1 * b.c2.y) + IgnoreTooSmallNumber(a.c2 * b.c2.z) + IgnoreTooSmallNumber(a.c3 * b.c2.w));
        }

        /// <summary>Returns the Fp2x4 matrix result of a matrix multiplication between a Fp2x4 matrix and a Fp4x4 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp2x4 mul(Fp2x4 a, Fp4x4 b)
        {
            return Fp2x4(
                IgnoreTooSmallNumber(a.c0 * b.c0.x) + IgnoreTooSmallNumber(a.c1 * b.c0.y) + IgnoreTooSmallNumber(a.c2 * b.c0.z) + IgnoreTooSmallNumber(a.c3 * b.c0.w),
                IgnoreTooSmallNumber(a.c0 * b.c1.x) + IgnoreTooSmallNumber(a.c1 * b.c1.y) + IgnoreTooSmallNumber(a.c2 * b.c1.z) + IgnoreTooSmallNumber(a.c3 * b.c1.w),
                IgnoreTooSmallNumber(a.c0 * b.c2.x) + IgnoreTooSmallNumber(a.c1 * b.c2.y) + IgnoreTooSmallNumber(a.c2 * b.c2.z) + IgnoreTooSmallNumber(a.c3 * b.c2.w),
                IgnoreTooSmallNumber(a.c0 * b.c3.x) + IgnoreTooSmallNumber(a.c1 * b.c3.y) + IgnoreTooSmallNumber(a.c2 * b.c3.z) + IgnoreTooSmallNumber(a.c3 * b.c3.w));
        }

        /// <summary>Returns the Fp3 column vector result of a matrix multiplication between a Fp3x2 matrix and a Fp2 column vector.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3 mul(Fp3x2 a, Fp2 b)
        {
            return IgnoreTooSmallNumber(a.c0 * b.x) + IgnoreTooSmallNumber(a.c1 * b.y);
        }

        /// <summary>Returns the Fp3x2 matrix result of a matrix multiplication between a Fp3x2 matrix and a Fp2x2 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3x2 mul(Fp3x2 a, Fp2x2 b)
        {
            return Fp3x2(
                IgnoreTooSmallNumber(a.c0 * b.c0.x) + IgnoreTooSmallNumber(a.c1 * b.c0.y),
                IgnoreTooSmallNumber(a.c0 * b.c1.x) + IgnoreTooSmallNumber(a.c1 * b.c1.y));
        }

        /// <summary>Returns the Fp3x3 matrix result of a matrix multiplication between a Fp3x2 matrix and a Fp2x3 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3x3 mul(Fp3x2 a, Fp2x3 b)
        {
            return Fp3x3(
                IgnoreTooSmallNumber(a.c0 * b.c0.x) + IgnoreTooSmallNumber(a.c1 * b.c0.y),
                IgnoreTooSmallNumber(a.c0 * b.c1.x) + IgnoreTooSmallNumber(a.c1 * b.c1.y),
                IgnoreTooSmallNumber(a.c0 * b.c2.x) + IgnoreTooSmallNumber(a.c1 * b.c2.y));
        }

        /// <summary>Returns the Fp3x4 matrix result of a matrix multiplication between a Fp3x2 matrix and a Fp2x4 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3x4 mul(Fp3x2 a, Fp2x4 b)
        {
            return Fp3x4(
                IgnoreTooSmallNumber(a.c0 * b.c0.x) + IgnoreTooSmallNumber(a.c1 * b.c0.y),
                IgnoreTooSmallNumber(a.c0 * b.c1.x) + IgnoreTooSmallNumber(a.c1 * b.c1.y),
                IgnoreTooSmallNumber(a.c0 * b.c2.x) + IgnoreTooSmallNumber(a.c1 * b.c2.y),
                IgnoreTooSmallNumber(a.c0 * b.c3.x) + IgnoreTooSmallNumber(a.c1 * b.c3.y));
        }

        /// <summary>Returns the Fp3 column vector result of a matrix multiplication between a Fp3x3 matrix and a Fp3 column vector.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3 mul(Fp3x3 a, Fp3 b)
        {
            return IgnoreTooSmallNumber(a.c0 * b.x) + IgnoreTooSmallNumber(a.c1 * b.y) + IgnoreTooSmallNumber(a.c2 * b.z);
        }

        /// <summary>Returns the Fp3x2 matrix result of a matrix multiplication between a Fp3x3 matrix and a Fp3x2 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3x2 mul(Fp3x3 a, Fp3x2 b)
        {
            return Fp3x2(
                IgnoreTooSmallNumber(a.c0 * b.c0.x) + IgnoreTooSmallNumber(a.c1 * b.c0.y) + IgnoreTooSmallNumber(a.c2 * b.c0.z),
                IgnoreTooSmallNumber(a.c0 * b.c1.x) + IgnoreTooSmallNumber(a.c1 * b.c1.y) + IgnoreTooSmallNumber(a.c2 * b.c1.z));
        }

        /// <summary>Returns the Fp3x3 matrix result of a matrix multiplication between a Fp3x3 matrix and a Fp3x3 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3x3 mul(Fp3x3 a, Fp3x3 b)
        {
            return Fp3x3(
                IgnoreTooSmallNumber(a.c0 * b.c0.x) + IgnoreTooSmallNumber(a.c1 * b.c0.y) + IgnoreTooSmallNumber(a.c2 * b.c0.z),
                IgnoreTooSmallNumber(a.c0 * b.c1.x) + IgnoreTooSmallNumber(a.c1 * b.c1.y) + IgnoreTooSmallNumber(a.c2 * b.c1.z),
                IgnoreTooSmallNumber(a.c0 * b.c2.x) + IgnoreTooSmallNumber(a.c1 * b.c2.y) + IgnoreTooSmallNumber(a.c2 * b.c2.z));
        }

        /// <summary>Returns the Fp3x4 matrix result of a matrix multiplication between a Fp3x3 matrix and a Fp3x4 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3x4 mul(Fp3x3 a, Fp3x4 b)
        {
            return Fp3x4(
                IgnoreTooSmallNumber(a.c0 * b.c0.x) + IgnoreTooSmallNumber(a.c1 * b.c0.y) + IgnoreTooSmallNumber(a.c2 * b.c0.z),
                IgnoreTooSmallNumber(a.c0 * b.c1.x) + IgnoreTooSmallNumber(a.c1 * b.c1.y) + IgnoreTooSmallNumber(a.c2 * b.c1.z),
                IgnoreTooSmallNumber(a.c0 * b.c2.x) + IgnoreTooSmallNumber(a.c1 * b.c2.y) + IgnoreTooSmallNumber(a.c2 * b.c2.z),
                IgnoreTooSmallNumber(a.c0 * b.c3.x) + IgnoreTooSmallNumber(a.c1 * b.c3.y) + IgnoreTooSmallNumber(a.c2 * b.c3.z));
        }

        /// <summary>Returns the Fp3 column vector result of a matrix multiplication between a Fp3x4 matrix and a Fp4 column vector.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3 mul(Fp3x4 a, Fp4 b)
        {
            return IgnoreTooSmallNumber(a.c0 * b.x) + IgnoreTooSmallNumber(a.c1 * b.y) + IgnoreTooSmallNumber(a.c2 * b.z) + IgnoreTooSmallNumber(a.c3 * b.w);
        }

        /// <summary>Returns the Fp3x2 matrix result of a matrix multiplication between a Fp3x4 matrix and a Fp4x2 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3x2 mul(Fp3x4 a, Fp4x2 b)
        {
            return Fp3x2(
                IgnoreTooSmallNumber(a.c0 * b.c0.x) + IgnoreTooSmallNumber(a.c1 * b.c0.y) + IgnoreTooSmallNumber(a.c2 * b.c0.z) + IgnoreTooSmallNumber(a.c3 * b.c0.w),
                IgnoreTooSmallNumber(a.c0 * b.c1.x) + IgnoreTooSmallNumber(a.c1 * b.c1.y) + IgnoreTooSmallNumber(a.c2 * b.c1.z) + IgnoreTooSmallNumber(a.c3 * b.c1.w));
        }

        /// <summary>Returns the Fp3x3 matrix result of a matrix multiplication between a Fp3x4 matrix and a Fp4x3 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3x3 mul(Fp3x4 a, Fp4x3 b)
        {
            return Fp3x3(
                IgnoreTooSmallNumber(a.c0 * b.c0.x) + IgnoreTooSmallNumber(a.c1 * b.c0.y) + IgnoreTooSmallNumber(a.c2 * b.c0.z) + IgnoreTooSmallNumber(a.c3 * b.c0.w),
                IgnoreTooSmallNumber(a.c0 * b.c1.x) + IgnoreTooSmallNumber(a.c1 * b.c1.y) + IgnoreTooSmallNumber(a.c2 * b.c1.z) + IgnoreTooSmallNumber(a.c3 * b.c1.w),
                IgnoreTooSmallNumber(a.c0 * b.c2.x) + IgnoreTooSmallNumber(a.c1 * b.c2.y) + IgnoreTooSmallNumber(a.c2 * b.c2.z) + IgnoreTooSmallNumber(a.c3 * b.c2.w));
        }

        /// <summary>Returns the Fp3x4 matrix result of a matrix multiplication between a Fp3x4 matrix and a Fp4x4 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp3x4 mul(Fp3x4 a, Fp4x4 b)
        {
            return Fp3x4(
                IgnoreTooSmallNumber(a.c0 * b.c0.x) + IgnoreTooSmallNumber(a.c1 * b.c0.y) + IgnoreTooSmallNumber(a.c2 * b.c0.z) + IgnoreTooSmallNumber(a.c3 * b.c0.w),
                IgnoreTooSmallNumber(a.c0 * b.c1.x) + IgnoreTooSmallNumber(a.c1 * b.c1.y) + IgnoreTooSmallNumber(a.c2 * b.c1.z) + IgnoreTooSmallNumber(a.c3 * b.c1.w),
                IgnoreTooSmallNumber(a.c0 * b.c2.x) + IgnoreTooSmallNumber(a.c1 * b.c2.y) + IgnoreTooSmallNumber(a.c2 * b.c2.z) + IgnoreTooSmallNumber(a.c3 * b.c2.w),
                IgnoreTooSmallNumber(a.c0 * b.c3.x) + IgnoreTooSmallNumber(a.c1 * b.c3.y) + IgnoreTooSmallNumber(a.c2 * b.c3.z) + IgnoreTooSmallNumber(a.c3 * b.c3.w));
        }

        /// <summary>Returns the Fp4 column vector result of a matrix multiplication between a Fp4x2 matrix and a Fp2 column vector.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4 mul(Fp4x2 a, Fp2 b)
        {
            return IgnoreTooSmallNumber(a.c0 * b.x) + IgnoreTooSmallNumber(a.c1 * b.y);
        }

        /// <summary>Returns the Fp4x2 matrix result of a matrix multiplication between a Fp4x2 matrix and a Fp2x2 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4x2 mul(Fp4x2 a, Fp2x2 b)
        {
            return Fp4x2(
                IgnoreTooSmallNumber(a.c0 * b.c0.x) + IgnoreTooSmallNumber(a.c1 * b.c0.y),
                IgnoreTooSmallNumber(a.c0 * b.c1.x) + IgnoreTooSmallNumber(a.c1 * b.c1.y));
        }

        /// <summary>Returns the Fp4x3 matrix result of a matrix multiplication between a Fp4x2 matrix and a Fp2x3 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4x3 mul(Fp4x2 a, Fp2x3 b)
        {
            return Fp4x3(
                IgnoreTooSmallNumber(a.c0 * b.c0.x) + IgnoreTooSmallNumber(a.c1 * b.c0.y),
                IgnoreTooSmallNumber(a.c0 * b.c1.x) + IgnoreTooSmallNumber(a.c1 * b.c1.y),
                IgnoreTooSmallNumber(a.c0 * b.c2.x) + IgnoreTooSmallNumber(a.c1 * b.c2.y));
        }

        /// <summary>Returns the Fp4x4 matrix result of a matrix multiplication between a Fp4x2 matrix and a Fp2x4 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4x4 mul(Fp4x2 a, Fp2x4 b)
        {
            return Fp4x4(
                IgnoreTooSmallNumber(a.c0 * b.c0.x) + IgnoreTooSmallNumber(a.c1 * b.c0.y),
                IgnoreTooSmallNumber(a.c0 * b.c1.x) + IgnoreTooSmallNumber(a.c1 * b.c1.y),
                IgnoreTooSmallNumber(a.c0 * b.c2.x) + IgnoreTooSmallNumber(a.c1 * b.c2.y),
                IgnoreTooSmallNumber(a.c0 * b.c3.x) + IgnoreTooSmallNumber(a.c1 * b.c3.y));
        }

        /// <summary>Returns the Fp4 column vector result of a matrix multiplication between a Fp4x3 matrix and a Fp3 column vector.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4 mul(Fp4x3 a, Fp3 b)
        {
            return IgnoreTooSmallNumber(a.c0 * b.x) + IgnoreTooSmallNumber(a.c1 * b.y) + IgnoreTooSmallNumber(a.c2 * b.z);
        }

        /// <summary>Returns the Fp4x2 matrix result of a matrix multiplication between a Fp4x3 matrix and a Fp3x2 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4x2 mul(Fp4x3 a, Fp3x2 b)
        {
            return Fp4x2(
                IgnoreTooSmallNumber(a.c0 * b.c0.x) + IgnoreTooSmallNumber(a.c1 * b.c0.y) + IgnoreTooSmallNumber(a.c2 * b.c0.z),
                IgnoreTooSmallNumber(a.c0 * b.c1.x) + IgnoreTooSmallNumber(a.c1 * b.c1.y) + IgnoreTooSmallNumber(a.c2 * b.c1.z));
        }

        /// <summary>Returns the Fp4x3 matrix result of a matrix multiplication between a Fp4x3 matrix and a Fp3x3 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4x3 mul(Fp4x3 a, Fp3x3 b)
        {
            return Fp4x3(
                IgnoreTooSmallNumber(a.c0 * b.c0.x) + IgnoreTooSmallNumber(a.c1 * b.c0.y) + IgnoreTooSmallNumber(a.c2 * b.c0.z),
                IgnoreTooSmallNumber(a.c0 * b.c1.x) + IgnoreTooSmallNumber(a.c1 * b.c1.y) + IgnoreTooSmallNumber(a.c2 * b.c1.z),
                IgnoreTooSmallNumber(a.c0 * b.c2.x) + IgnoreTooSmallNumber(a.c1 * b.c2.y) + IgnoreTooSmallNumber(a.c2 * b.c2.z));
        }

        /// <summary>Returns the Fp4x4 matrix result of a matrix multiplication between a Fp4x3 matrix and a Fp3x4 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4x4 mul(Fp4x3 a, Fp3x4 b)
        {
            return Fp4x4(
                IgnoreTooSmallNumber(a.c0 * b.c0.x) + IgnoreTooSmallNumber(a.c1 * b.c0.y) + IgnoreTooSmallNumber(a.c2 * b.c0.z),
                IgnoreTooSmallNumber(a.c0 * b.c1.x) + IgnoreTooSmallNumber(a.c1 * b.c1.y) + IgnoreTooSmallNumber(a.c2 * b.c1.z),
                IgnoreTooSmallNumber(a.c0 * b.c2.x) + IgnoreTooSmallNumber(a.c1 * b.c2.y) + IgnoreTooSmallNumber(a.c2 * b.c2.z),
                IgnoreTooSmallNumber(a.c0 * b.c3.x) + IgnoreTooSmallNumber(a.c1 * b.c3.y) + IgnoreTooSmallNumber(a.c2 * b.c3.z));
        }

        /// <summary>Returns the Fp4 column vector result of a matrix multiplication between a Fp4x4 matrix and a Fp4 column vector.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4 mul(Fp4x4 a, Fp4 b)
        {
            return IgnoreTooSmallNumber(a.c0 * b.x) + IgnoreTooSmallNumber(a.c1 * b.y) + IgnoreTooSmallNumber(a.c2 * b.z) + IgnoreTooSmallNumber(a.c3 * b.w);
        }

        /// <summary>Returns the Fp4x2 matrix result of a matrix multiplication between a Fp4x4 matrix and a Fp4x2 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4x2 mul(Fp4x4 a, Fp4x2 b)
        {
            return Fp4x2(
                IgnoreTooSmallNumber(a.c0 * b.c0.x) + IgnoreTooSmallNumber(a.c1 * b.c0.y) + IgnoreTooSmallNumber(a.c2 * b.c0.z) + IgnoreTooSmallNumber(a.c3 * b.c0.w),
                IgnoreTooSmallNumber(a.c0 * b.c1.x) + IgnoreTooSmallNumber(a.c1 * b.c1.y) + IgnoreTooSmallNumber(a.c2 * b.c1.z) + IgnoreTooSmallNumber(a.c3 * b.c1.w));
        }

        /// <summary>Returns the Fp4x3 matrix result of a matrix multiplication between a Fp4x4 matrix and a Fp4x3 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4x3 mul(Fp4x4 a, Fp4x3 b)
        {
            return Fp4x3(
                IgnoreTooSmallNumber(a.c0 * b.c0.x) + IgnoreTooSmallNumber(a.c1 * b.c0.y) + IgnoreTooSmallNumber(a.c2 * b.c0.z) + IgnoreTooSmallNumber(a.c3 * b.c0.w),
                IgnoreTooSmallNumber(a.c0 * b.c1.x) + IgnoreTooSmallNumber(a.c1 * b.c1.y) + IgnoreTooSmallNumber(a.c2 * b.c1.z) + IgnoreTooSmallNumber(a.c3 * b.c1.w),
                IgnoreTooSmallNumber(a.c0 * b.c2.x) + IgnoreTooSmallNumber(a.c1 * b.c2.y) + IgnoreTooSmallNumber(a.c2 * b.c2.z) + IgnoreTooSmallNumber(a.c3 * b.c2.w));
        }

        /// <summary>Returns the Fp4x4 matrix result of a matrix multiplication between a Fp4x4 matrix and a Fp4x4 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fp4x4 mul(Fp4x4 a, Fp4x4 b)
        {
            return Fp4x4(
                IgnoreTooSmallNumber(a.c0 * b.c0.x) + IgnoreTooSmallNumber(a.c1 * b.c0.y) + IgnoreTooSmallNumber(a.c2 * b.c0.z) + IgnoreTooSmallNumber(a.c3 * b.c0.w),
                IgnoreTooSmallNumber(a.c0 * b.c1.x) + IgnoreTooSmallNumber(a.c1 * b.c1.y) + IgnoreTooSmallNumber(a.c2 * b.c1.z) + IgnoreTooSmallNumber(a.c3 * b.c1.w),
                IgnoreTooSmallNumber(a.c0 * b.c2.x) + IgnoreTooSmallNumber(a.c1 * b.c2.y) + IgnoreTooSmallNumber(a.c2 * b.c2.z) + IgnoreTooSmallNumber(a.c3 * b.c2.w),
                IgnoreTooSmallNumber(a.c0 * b.c3.x) + IgnoreTooSmallNumber(a.c1 * b.c3.y) + IgnoreTooSmallNumber(a.c2 * b.c3.z) + IgnoreTooSmallNumber(a.c3 * b.c3.w));
        }

    }
}
