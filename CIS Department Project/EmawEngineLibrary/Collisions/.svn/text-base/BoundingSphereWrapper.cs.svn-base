using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using EmawEngineLibrary.Physics;
using Microsoft.Xna.Framework;

namespace EmawEngineLibrary.Collisions
{
    public class BoundingSphereWrapper : IBoundingObject
    {
        public BoundingSphereWrapper()
        {
        }

        public BoundingSphereWrapper(BoundingSphere sphere)
        {
            m_boundingSphere = sphere;
        }

        public BoundingSphereWrapper(Vector3 center, float radius)
        {
            m_boundingSphere = new BoundingSphere(center, radius);
        }

        public static implicit operator BoundingSphere(BoundingSphereWrapper sphere)
        {
            return sphere.BoundingSphere;
        }

        public static implicit operator BoundingSphereWrapper(BoundingSphere sphere)
        {
            return new BoundingSphereWrapper(sphere);
        }

        public ContainmentType Contains(ref BoundingBox box)
        {
            return box.Contains(m_boundingSphere);
        }

        public bool Intersects(BoundingOrientedBox other)
        {
            return other.Intersects(m_boundingSphere);
        }

        public ContainmentType Contains(ref BoundingOrientedBox other)
        {
            return other.Contains(m_boundingSphere);
        }

        public Vector3 Center { get { return m_boundingSphere.Center; } set { m_boundingSphere.Center = value; } }
        public float Radius { get { return m_boundingSphere.Radius; } set { m_boundingSphere.Radius = value; } }

        public BoundingSphere BoundingSphere
        {
            get { return m_boundingSphere; } 
            set { m_boundingSphere = value; }
        }
        private BoundingSphere m_boundingSphere;

        #region Wrapped BoundingSphere methods
        /// <summary>
        /// Determines whether the specified BoundingSphere is equal to the current BoundingSphere.
        /// </summary>
        /// <param name="other">The BoundingSphere to compare with the current BoundingSphere.</param>
        public bool Equals(BoundingSphere other)
        {
            return m_boundingSphere.Equals(other);
        }

        /// <summary>
        /// Checks whether the current BoundingSphere intersects with a specified BoundingBox.
        /// </summary>
        /// <param name="box">The BoundingBox to check for intersection with the current BoundingSphere.</param>
        public bool Intersects(BoundingBox box)
        {
            return m_boundingSphere.Intersects(box);
        }

        /// <summary>
        /// Checks whether the current BoundingSphere intersects a BoundingBox.
        /// </summary>
        /// <param name="box">The BoundingBox to check for intersection with.</param><param name="result">[OutAttribute] true if the BoundingSphere and BoundingBox intersect; false otherwise.</param>
        public void Intersects(ref BoundingBox box, out bool result)
        {
            m_boundingSphere.Intersects(ref box, out result);
        }

        /// <summary>
        /// Checks whether the current BoundingSphere intersects with a specified BoundingFrustum.
        /// </summary>
        /// <param name="frustum">The BoundingFrustum to check for intersection with the current BoundingSphere.</param>
        public bool Intersects(BoundingFrustum frustum)
        {
            return m_boundingSphere.Intersects(frustum);
        }

        /// <summary>
        /// Checks whether the current BoundingSphere intersects with a specified Plane.
        /// </summary>
        /// <param name="plane">The Plane to check for intersection with the current BoundingSphere.</param>
        public PlaneIntersectionType Intersects(Plane plane)
        {
            return m_boundingSphere.Intersects(plane);
        }

        /// <summary>
        /// Checks whether the current BoundingSphere intersects a Plane.
        /// </summary>
        /// <param name="plane">The Plane to check for intersection with.</param><param name="result">[OutAttribute] An enumeration indicating whether the BoundingSphere intersects the Plane.</param>
        public void Intersects(ref Plane plane, out PlaneIntersectionType result)
        {
            m_boundingSphere.Intersects(ref plane, out result);
        }

        /// <summary>
        /// Checks whether the current BoundingSphere intersects with a specified Ray.
        /// </summary>
        /// <param name="ray">The Ray to check for intersection with the current BoundingSphere.</param>
        public float? Intersects(Ray ray)
        {
            return m_boundingSphere.Intersects(ray);
        }

        /// <summary>
        /// Checks whether the current BoundingSphere intersects a Ray.
        /// </summary>
        /// <param name="ray">The Ray to check for intersection with.</param><param name="result">[OutAttribute] Distance at which the ray intersects the BoundingSphere or null if there is no intersection.</param>
        public void Intersects(ref Ray ray, out float? result)
        {
            m_boundingSphere.Intersects(ref ray, out result);
        }

        /// <summary>
        /// Checks whether the current BoundingSphere intersects with a specified BoundingSphere.
        /// </summary>
        /// <param name="sphere">The BoundingSphere to check for intersection with the current BoundingSphere.</param>
        public bool Intersects(BoundingSphere sphere)
        {
            return m_boundingSphere.Intersects(sphere);
        }

        /// <summary>
        /// Checks whether the current BoundingSphere intersects another BoundingSphere.
        /// </summary>
        /// <param name="sphere">The BoundingSphere to check for intersection with.</param><param name="result">[OutAttribute] true if the BoundingSphere instances intersect; false otherwise.</param>
        public void Intersects(ref BoundingSphere sphere, out bool result)
        {
            m_boundingSphere.Intersects(ref sphere, out result);
        }

        /// <summary>
        /// Checks whether the current BoundingSphere contains the specified BoundingBox.
        /// </summary>
        /// <param name="box">The BoundingBox to check against the current BoundingSphere.</param>
        public ContainmentType Contains(BoundingBox box)
        {
            return m_boundingSphere.Contains(box);
        }

        /// <summary>
        /// Checks whether the current BoundingSphere contains the specified BoundingBox.
        /// </summary>
        /// <param name="box">The BoundingBox to test for overlap.</param><param name="result">[OutAttribute] Enumeration indicating the extent of overlap.</param>
        public void Contains(ref BoundingBox box, out ContainmentType result)
        {
            m_boundingSphere.Contains(ref box, out result);
        }

        /// <summary>
        /// Checks whether the current BoundingSphere contains the specified BoundingFrustum.
        /// </summary>
        /// <param name="frustum">The BoundingFrustum to check against the current BoundingSphere.</param>
        public ContainmentType Contains(BoundingFrustum frustum)
        {
            return m_boundingSphere.Contains(frustum);
        }

        /// <summary>
        /// Checks whether the current BoundingSphere contains the specified point.
        /// </summary>
        /// <param name="point">The point to check against the current BoundingSphere.</param>
        public ContainmentType Contains(Vector3 point)
        {
            return m_boundingSphere.Contains(point);
        }

        /// <summary>
        /// Checks whether the current BoundingSphere contains the specified point.
        /// </summary>
        /// <param name="point">The point to test for overlap.</param><param name="result">[OutAttribute] Enumeration indicating the extent of overlap.</param>
        public void Contains(ref Vector3 point, out ContainmentType result)
        {
            m_boundingSphere.Contains(ref point, out result);
        }

        /// <summary>
        /// Checks whether the current BoundingSphere contains the specified BoundingSphere.
        /// </summary>
        /// <param name="sphere">The BoundingSphere to check against the current BoundingSphere.</param>
        public ContainmentType Contains(BoundingSphere sphere)
        {
            return m_boundingSphere.Contains(sphere);
        }

        /// <summary>
        /// Checks whether the current BoundingSphere contains the specified BoundingSphere.
        /// </summary>
        /// <param name="sphere">The BoundingSphere to test for overlap.</param><param name="result">[OutAttribute] Enumeration indicating the extent of overlap.</param>
        public void Contains(ref BoundingSphere sphere, out ContainmentType result)
        {
            m_boundingSphere.Contains(ref sphere, out result);
        }

        /// <summary>
        /// Translates and scales the BoundingSphere using a given Matrix.
        /// </summary>
        /// <param name="matrix">A transformation matrix that might include translation, rotation, or uniform scaling. Note that BoundingSphere.Transform will not return correct results if there are non-uniform scaling, shears, or other unusual transforms in this transformation matrix. This is because there is no way to shear or non-uniformly scale a sphere. Such an operation would cause the sphere to lose its shape as a sphere.</param>
        public BoundingSphere Transform(Matrix matrix)
        {
            return m_boundingSphere.Transform(matrix);
        }

        /// <summary>
        /// Translates and scales the BoundingSphere using a given Matrix.
        /// </summary>
        /// <param name="matrix">A transformation matrix that might include translation, rotation, or uniform scaling. Note that BoundingSphere.Transform will not return correct results if there are non-uniform scaling, shears, or other unusual transforms in this transformation matrix. This is because there is no way to shear or non-uniformly scale a sphere. Such an operation would cause the sphere to lose its shape as a sphere.</param><param name="result">[OutAttribute] The transformed BoundingSphere.</param>
        public void Transform(ref Matrix matrix, out BoundingSphere result)
        {
            m_boundingSphere.Transform(ref matrix, out result);
        }
        #endregion
    }
}
