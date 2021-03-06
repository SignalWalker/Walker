﻿namespace Walker.Data.Geometry.Speed.Space {
	using System;
	using System.Diagnostics.CodeAnalysis;
	using System.Runtime.InteropServices;
	using Generic.Space;

	/// <summary>
	/// A vector in 3D space, defined with floats.
	/// X increases rightward.
	/// Y increases upward.
	/// Z increases inward. (As in, into the screen / away from you.)
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct Vector3F {

		public static readonly Vector3F Zero = new Vector3F(0, 0, 0);
		public static readonly Vector3F UnitScale = new Vector3F(1, 1, 1);
		public static readonly Vector3F Right = new Vector3F(1, 0, 0);
		public static readonly Vector3F Up = new Vector3F(0, 1, 0);
		public static readonly Vector3F Down = -Up;
		public static readonly Vector3F Left = -Right;

		public float x, y, z;

		public float Length {
			get => (float) Math.Sqrt(x * x + y * y + z * z);
			set {
				float scale = value / Length;
				x *= scale;
				y *= scale;
				z *= scale;
			}
		}

		public Vector3F(float x, float y, float z) {
			this.x = x;
			this.y = y;
			this.z = z;
		}

		public Vector3F(params float[] val) {
			if (val.Length != 3) { throw new ArgumentException("Vector3F() given array of more or less than 3 floats"); }
			x = val[0];
			y = val[1];
			z = val[2];
		}

		public Vector3F Normal => new Vector3F(x, y, z) {Length = 1};

		/// <summary>
		/// Gets the dot product of two vectors
		/// </summary>
		/// <param name="other">The right side of the function</param>
		/// <returns>a [dot] b</returns>
		public float Dot(Vector3F other) {
			return x * other.x + y * other.y + z * other.z;
		}

		/// <summary>
		/// Gets the cross product of two vectors
		/// </summary>
		/// <param name="other">The right side of the function</param>
		/// <returns>a X b</returns>
		[SuppressMessage("ReSharper", "LocalVariableHidesMember")]
		public Vector3F Cross(Vector3F other) {
			float x = this.y * other.z - this.z * other.y;
			float y = this.z * other.x - this.x * other.z;
			float z = this.x * other.y - this.y * other.x;
			return new Vector3F(x, y, z);
		}

		/// <summary>
		/// Operator - overload ; returns the opposite of a vector
		/// </summary>
		/// <param name="v">Vector to negate</param>
		/// <returns>-v</returns>
		public static Vector3F operator -(Vector3F v) {
			return new Vector3F(-v.x, -v.y, -v.z);
		}

		/// <summary>
		/// Operator - overload ; subtracts two vectors
		/// </summary>
		/// <param name="v1">First vector</param>
		/// <param name="v2">Second vector</param>
		/// <returns>v1 - v2</returns>
		public static Vector3F operator -(Vector3F v1, Vector3F v2) {
			return new Vector3F(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z);
		}

		/// <summary>
		/// Operator + overload ; add two vectors
		/// </summary>
		/// <param name="v1">First vector</param>
		/// <param name="v2">Second vector</param>
		/// <returns>v1 + v2</returns>
		public static Vector3F operator +(Vector3F v1, Vector3F v2) {
			return new Vector3F(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z);
		}

		/// <summary>
		/// Operator * overload ; multiply two vectors
		/// </summary>
		/// <param name="a">Vec 1</param>
		/// <param name="b">Vec 2</param>
		/// <returns>a * b</returns>
		public static Vector3F operator *(Vector3F a, Vector3F b) {
			return new Vector3F(a.x * b.x, a.y * b.y, a.z * b.z);
		}

		/// <summary>
		/// Operator * overload ; multiply a vector by a scalar value
		/// </summary>
		/// <param name="v">Vector</param>
		/// <param name="x">Scalar value</param>
		/// <returns>v * x</returns>
		public static Vector3F operator *(Vector3F v, float x) {
			return new Vector3F(v.x * x, v.y * x, v.z * x);
		}

		/// <summary>
		/// Operator * overload ; multiply a scalar value by a vector.
		/// </summary>
		/// <param name="x">Scalar value</param>
		/// <param name="v">Vector</param>
		/// <returns>x * v</returns>
		public static Vector3F operator *(float x, Vector3F v) {
			return new Vector3F(v.x * x, v.y * x, v.z * x);
		}

		/// <summary>
		/// Operator / overload ; divide a vector by a scalar value
		/// </summary>
		/// <param name="v">Vector</param>
		/// <param name="x">Scalar value</param>
		/// <returns>v / x</returns>
		public static Vector3F operator /(Vector3F v, float x) {
			return new Vector3F(v.x / x, v.y / x, v.z / x);
		}

		/// <summary>
		/// Operator / overload ; return vector of {x / {v[i}}
		/// </summary>
		/// <param name="v">Vector</param>
		/// <param name="x">Scalar value</param>
		/// <returns>v / x</returns>
		public static Vector3F operator /(float x, Vector3F v) {
			return new Vector3F(x / v.x, x / v.y, x / v.z);
		}

		/// <summary>
		/// Operator / overload ; divide one vector by another
		/// </summary>
		/// <param name="a">First vector</param>
		/// <param name="b">Second vector</param>
		/// <returns>a / b</returns>
		public static Vector3F operator /(Vector3F a, Vector3F b) {
			return new Vector3F(a.x / b.x, a.y / b.y, a.z / b.z);
		}

		public override bool Equals(object obj) {
			if (obj is Vector3F) { return Equals((Vector3F) obj); }
			if (obj is Vector3<float>) { return Equals((Vector3<float>) obj); }
			return false;
		}

		[SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
		public override int GetHashCode() {
			unchecked {
				int hashCode = x.GetHashCode();
				hashCode = (hashCode * 397) ^ y.GetHashCode();
				hashCode = (hashCode * 397) ^ z.GetHashCode();
				return hashCode;
			}
		}

		public bool Equals(Vector3F other) {
			return this == other;
		}

		public bool Equals(Vector3<float> other) {
			return this == other;
		}

		public override string ToString() {
			return "<" + x + ", " + y + ", " + z + ">";
		}

		public static bool operator ==(Vector3F a, Vector3F b) {
			return Math.Abs(a.x - b.x) < GeoMeta.Tolerance
			       && Math.Abs(a.y - b.y) < GeoMeta.Tolerance
			       && Math.Abs(a.z - b.z) < GeoMeta.Tolerance;
		}

		public static bool operator !=(Vector3F a, Vector3F b) {
			return !(a == b);
		}

		public static bool operator ==(Vector3F a, Vector3<float> b) {
			return Math.Abs(a.x - b.X) < GeoMeta.Tolerance
			       && Math.Abs(a.y - b.Y) < GeoMeta.Tolerance
			       && Math.Abs(a.z - b.Z) < GeoMeta.Tolerance;
		}

		public static bool operator !=(Vector3F a, Vector3<float> b) {
			return !(a == b);
		}

		public static implicit operator Vector3<float>(Vector3F vec) {
			return new Vector3<float>(vec.x, vec.y, vec.z);
		}

		public static explicit operator float[](Vector3F vec) {
			return new [] {vec.x, vec.y, vec.z};
		}

		public static implicit operator Vector3F(float[] val) {
			return new Vector3F(val);
		}

	}
}